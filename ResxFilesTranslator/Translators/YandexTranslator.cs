using Newtonsoft.Json;
using ResxFilesTranslator.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ResxFilesTranslator.Translators
{
    public class YandexTranslator : ITranslator
    {
        private static readonly String YA_TRANSLATION_DIRECTIONS_URL_TMPLT = "YA_TRANSLATION_DIRECTIONS_URL_TMPLT";
        private static readonly String YA_TRANSLATION_URL_TMPLT = "YA_TRANSLATION_URL_TMPLT";
        private static readonly String YA_TRANSLATION_API_KEY = "YA_TRANSLATION_API_KEY";
        private static readonly Int32 BUFFER_SIZE = 8200;
        private static readonly String LANG_PAIR_TEMPLATE = "{0}-{1}";
        private static readonly String PLAIN_FORMAT_IDENTIFIER = "plain";
        private static ManualResetEvent allDone = new ManualResetEvent(false);

        private String TranslationDirectionsUrlTemplate { get; set; }
        private String TranslationUrlTemplate { get; set; }
        private String ApiKey { get; set; }

        public event EventHandler<LanguagesReceivedEventArgs> SupportedLanguagesReceived;
        public event EventHandler<TranslationEventArgs> TranslationCompleted;

        public YandexTranslator()
        {
            this.TranslationDirectionsUrlTemplate = ConfigurationManager.AppSettings[YA_TRANSLATION_DIRECTIONS_URL_TMPLT];
            this.TranslationUrlTemplate = ConfigurationManager.AppSettings[YA_TRANSLATION_URL_TMPLT];
            this.ApiKey = ConfigurationManager.AppSettings[YA_TRANSLATION_API_KEY];
        }

        public void GetSupportedLanguagesAsync()
        {
            String langsUrl = String.Format(this.TranslationDirectionsUrlTemplate, this.ApiKey);
            allDone.Reset();
            String resultString = this.MakeRequest(new Uri(langsUrl));
            YandexLanguages result = JsonConvert.DeserializeObject<YandexLanguages>(resultString);

            if (this.SupportedLanguagesReceived != null)
                this.SupportedLanguagesReceived(this, new LanguagesReceivedEventArgs() { IsError = false, Languages = result.Languages });
        }

        public void TranslateStringAsync(String originalString, String originalLanguage, String translationLanguage)
        {
            
        }

        public String TranslateString(String originalString, String originalLanguage, String translationLanguage)
        {
            String translationUrl = String.Format(this.TranslationUrlTemplate,
                                                  this.ApiKey,
                                                  HttpUtility.UrlEncode(originalString),
                                                  String.Format(LANG_PAIR_TEMPLATE, originalLanguage, translationLanguage),
                                                  PLAIN_FORMAT_IDENTIFIER);
            allDone.Reset();
            String result = this.MakeRequest(new Uri(translationUrl));
            YandexTranslation translation = JsonConvert.DeserializeObject<YandexTranslation>(result);
            translation.OriginalText = originalString;
            return translation.TranslatedText[0];
        }

        private String MakeRequest(Uri targetUri)
        {
            // Create the request object.
            WebRequest wreq = WebRequest.Create(targetUri);

            // Create the state object.
            RequestState rs = new RequestState();

            // Put the request into the state object so it can be passed around.
            rs.Request = wreq;

            // Issue the async request.
            IAsyncResult r = (IAsyncResult)wreq.BeginGetResponse(
               new AsyncCallback(RespCallback), rs);

            // Wait until the ManualResetEvent is set so that the application 
            // does not exit until after the callback is called.
            allDone.WaitOne();

            return rs.RequestData.ToString();
        }

        private void RespCallback(IAsyncResult ar)
        {
            // Get the RequestState object from the async result.
            RequestState rs = (RequestState)ar.AsyncState;

            // Get the WebRequest from RequestState.
            WebRequest req = rs.Request;

            // Call EndGetResponse, which produces the WebResponse object
            //  that came from the request issued above.
            WebResponse resp = req.EndGetResponse(ar);

            //  Start reading data from the response stream.
            Stream ResponseStream = resp.GetResponseStream();

            // Store the response stream in RequestState to read 
            // the stream asynchronously.
            rs.ResponseStream = ResponseStream;

            //  Pass rs.BufferRead to BeginRead. Read data into rs.BufferRead
            IAsyncResult iarRead = ResponseStream.BeginRead(rs.BufferRead, 0,
               BUFFER_SIZE, new AsyncCallback(ReadCallBack), rs);
        }

        private void ReadCallBack(IAsyncResult asyncResult)
        {
            // Get the RequestState object from AsyncResult.
            RequestState rs = (RequestState)asyncResult.AsyncState;

            // Retrieve the ResponseStream that was set in RespCallback. 
            Stream responseStream = rs.ResponseStream;

            // Read rs.BufferRead to verify that it contains data. 
            int read = responseStream.EndRead(asyncResult);
            if (read > 0)
            {
                // Prepare a Char array buffer for converting to Unicode.
                Char[] charBuffer = new Char[BUFFER_SIZE];

                // Convert byte stream to Char array and then to String.
                // len contains the number of characters converted to Unicode.
                int len =
                   rs.StreamDecode.GetChars(rs.BufferRead, 0, read, charBuffer, 0);

                String str = new String(charBuffer, 0, len);

                // Append the recently read data to the RequestData stringbuilder
                // object contained in RequestState.
                rs.RequestData.Append(
                   Encoding.UTF8.GetString(rs.BufferRead, 0, read));

                // Continue reading data until 
                // responseStream.EndRead returns –1.
                IAsyncResult ar = responseStream.BeginRead(
                   rs.BufferRead, 0, BUFFER_SIZE,
                   new AsyncCallback(ReadCallBack), rs);
            }
            else
            {
                if (rs.RequestData.Length > 0)
                {
                    //  Display data to the console.
                    string strContent;
                    strContent = rs.RequestData.ToString();
                }
                // Close down the response stream.
                responseStream.Close();
                // Set the ManualResetEvent so the main thread can exit.
                allDone.Set();
            }
            return;
        }
    }
}
