using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxFilesTranslator.Translators
{
    public class YandexLanguages
    {
        [JsonProperty(PropertyName="dirs")]
        public List<String> AvailableDirections { get; set; }

        [JsonProperty(PropertyName = "langs")]
        public Dictionary<String, String> Languages { get; set; }
    }
}
