using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxFilesTranslator.CustomEventArgs
{
    public class LanguagesReceivedEventArgs : EventArgs
    {
        public Boolean IsError { get; set; }
        public String ErrorMessage { get; set; }
        public Dictionary<String, String> Languages { get; set; }
    }
}
