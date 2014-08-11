using System;
using System.IO;
using System.Net;
using System.Text;

namespace ResxFilesTranslator.Translators
{
    // The RequestState class passes data across async calls.
    public class RequestState
    {
        const int BufferSize = 8200;
        public StringBuilder RequestData;
        public byte[] BufferRead;
        public WebRequest Request;
        public Stream ResponseStream;
        // Create Decoder for appropriate enconding type.
        public Decoder StreamDecode = Encoding.UTF8.GetDecoder();

        public RequestState()
        {
            BufferRead = new byte[BufferSize];
            RequestData = new StringBuilder(String.Empty);
            Request = null;
            ResponseStream = null;
        }
    }
}
