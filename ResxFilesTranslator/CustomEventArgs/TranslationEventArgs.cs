using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxFilesTranslator.CustomEventArgs
{
    public class TranslationEventArgs : EventArgs
    {
        public Boolean IsError { get; set; }
        public String ErrorMessage { get; set; }
        public String OriginalValue { get; set; }
        public String Translation { get; set; }
        public String OriginalLanguage { get; set; }
        public String TranslationLanguage { get; set; }
    }
}
