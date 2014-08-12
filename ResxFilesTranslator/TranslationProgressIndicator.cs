using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResxFilesTranslator
{
    public partial class TranslationProgressIndicator : Form
    {
        private static readonly String CANCEL_TEXT = "Cancel";
        private static readonly String CANCELLATION_MESSAGE = "Are you sere you want to cancel the translation?\r\nAll the results will be lost.";
        private static readonly String PROCESS_MESSAGE = "Processing {0} of {1} items";

        public TranslationProgressIndicator()
        {
            InitializeComponent();
        }

        public ProgressBar ProgressBar
        {
            get { return this.pbTranslationProgress; }
        }

        public Int32 TotalItemsToTranslate { get; set; }

        public BackgroundWorker TranslationWorker { get; set; }

        public String ProcessingStatus
        {
            get
            {
                return this.lblCurrentStatus.Text;
            }
            set 
            {
                this.lblCurrentStatus.Text = String.Format(PROCESS_MESSAGE, value, this.TotalItemsToTranslate);
            }
        }

        private void btnCancelTranslation_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(CANCELLATION_MESSAGE, CANCEL_TEXT, MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                this.TranslationWorker.CancelAsync();                
            }
        }
    }
}
