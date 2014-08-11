using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxFilesTranslator.Translators
{
    public class YandexTranslation
    {
        [JsonProperty(PropertyName = "code")]
        public Int32 StatusCode { get; set; }
        [JsonProperty(PropertyName = "lang")]
        public String LanguagesPair { get; set; }
        public String OriginalText { get; set; }
        [JsonProperty(PropertyName = "text")]
        public List<String> TranslatedText { get; set; }
    }
}
