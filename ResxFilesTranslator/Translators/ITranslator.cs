using ResxFilesTranslator.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxFilesTranslator.Translators
{
    public interface ITranslator
    {
        void GetSupportedLanguagesAsync();
        void TranslateStringAsync(String originalString, String originalLanguage, String translationLanguage);

        String TranslateString(String originalString, String originalLanguage, String translationLanguage);

        event EventHandler<LanguagesReceivedEventArgs> SupportedLanguagesReceived;
        event EventHandler<TranslationEventArgs> TranslationCompleted;
    }
}
