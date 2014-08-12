using ResxFilesTranslator.CustomEventArgs;
using ResxFilesTranslator.Translators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ResxFilesTranslator
{
    public partial class MainForm : Form
    {
        private static readonly String SELECT_TEXT = "Select...";
        private static readonly String ERROR_TEXT = "Error";
        private static readonly String SUCCESS_TEXT = "Success";
        private static readonly String CANCELLED_TEXT = "Cancelled";
        private static readonly String CANCELLED_MSG = "The translation was cancelled. No results were saved.";
        private static readonly String SUCCESSFULLY_TRANSLATED_MESSAGE = "Resx file [{0}]\r\n was translated and saved to [{1}].";
        private ITranslator _translator;
        private String _originalLanguage;
        private String _translationLanguage;
        private String _sourceFileName;
        private String _destFileName;
        private TranslationProgressIndicator _translationProgress;        
        private Boolean isCancelled = false;

        public MainForm()
        {
            InitializeComponent();
        }

        #region MainForm lifecycle event handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            this._translator = new YandexTranslator();
            this._translator.SupportedLanguagesReceived += _translator_SupportedLanguagesReceived;
            this._translator.TranslationCompleted += _translator_TranslationCompleted;
        }        

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this._translator.GetSupportedLanguagesAsync();            
        }

        #endregion

        #region Buttons Event Handlers

        private void btnBrowseForSource_Click(object sender, EventArgs e)
        {
            this.dlgSourceFile.ShowDialog();
        }

        private void btnBrowseForDestination_Click(object sender, EventArgs e)
        {
            this.dlgDestinationFile.ShowDialog();
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(this._sourceFileName)) && 
                (File.Exists(this._sourceFileName)) && 
                (!String.IsNullOrEmpty(this._destFileName)))
            {
                this.isCancelled = false;
                this.StartTranslateFile(this._sourceFileName);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        #endregion

        #region Textboxes Event Handlers

        private void txt_TextChanged(object sender, EventArgs e)
        {
            this.btnTranslate.Enabled = (this.cbOriginalLanguage.SelectedIndex > 0) &&
                                        (this.cbTranslationLanguage.SelectedIndex > 0) &&
                                        !String.IsNullOrEmpty(this.txtSourceFilePath.Text) &&
                                        !String.IsNullOrEmpty(this.txtDestFilePath.Text);
        }

        #endregion

        #region Combo Boxes Event Handlers

        private void cbOriginalLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnTranslate.Enabled = (this.cbOriginalLanguage.SelectedIndex > 0) &&
                                        (this.cbTranslationLanguage.SelectedIndex > 0) &&
                                        !String.IsNullOrEmpty(this.txtSourceFilePath.Text) && 
                                        !String.IsNullOrEmpty(this.txtDestFilePath.Text);

            this._originalLanguage = this.cbOriginalLanguage.SelectedValue.ToString();
        }

        private void cbTranslationLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnTranslate.Enabled = (this.cbOriginalLanguage.SelectedIndex > 0) &&
                                        (this.cbTranslationLanguage.SelectedIndex > 0) &&
                                        !String.IsNullOrEmpty(this.txtSourceFilePath.Text) && 
                                        !String.IsNullOrEmpty(this.txtDestFilePath.Text);

            this._translationLanguage = this.cbTranslationLanguage.SelectedValue.ToString();
        }

        #endregion

        #region File Dialogs Event Handlers

        private void dlgSourceFile_FileOk(object sender, CancelEventArgs e)
        {
            this._sourceFileName = this.dlgSourceFile.FileName;
            this.txtSourceFilePath.Text = this._sourceFileName;
        }

        private void dlgDestinationFile_FileOk(object sender, CancelEventArgs e)
        {
            this._destFileName = this.dlgDestinationFile.FileName;
            this.txtDestFilePath.Text = this._destFileName;
        }

        #endregion

        #region Translator Event Handlers

        void _translator_TranslationCompleted(object sender, TranslationEventArgs e)
        {
            throw new NotImplementedException();
        }

        void _translator_SupportedLanguagesReceived(object sender, LanguagesReceivedEventArgs e)
        {
            this.BindComboBoxes(e.Languages);
        }

        #endregion

        #region Background Worker Event Handlers

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            XDocument resultXDoc = new XDocument();
            XDocument sourseXDoc = XDocument.Load(this._sourceFileName);

            XElement root = new XElement("root");
            resultXDoc.Add(root);

            Int32 nodesTranslated = 0;

            foreach (XNode element in sourseXDoc.Document.Element("root").Nodes())
            {
                if (element.NodeType == XmlNodeType.Comment)
                {
                    root.Add(new XComment(element as XComment));
                }
                else
                {
                    XElement el = element as XElement;
                    if (!el.Name.LocalName.Equals("data"))
                    {
                        root.Add(new XElement(el));
                    }
                    else
                    {
                        XElement dataElement = new XElement(el);
                        dataElement.Element("value").Value = this._translator.TranslateString(el.Element("value").Value, this._originalLanguage, this._translationLanguage);
                        root.Add(dataElement);
                        nodesTranslated++;
                        worker.ReportProgress(nodesTranslated);

                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }

            e.Result = resultXDoc;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //This is called on GUI/main thread, so you can access the controls properly
            this._translationProgress.ProgressBar.Value = e.ProgressPercentage;
            this._translationProgress.ProcessingStatus = e.ProgressPercentage.ToString();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    if (!this.isCancelled)
                    {
                        this.isCancelled = true;
                        MessageBox.Show(CANCELLED_MSG, CANCELLED_TEXT);
                        this._translationProgress.Close();                        
                        return;
                    }
                }

                if (e.Error != null)
                {
                    throw e.Error;
                }

                XDocument translatedXDoc = e.Result as XDocument;
                translatedXDoc.Save(this._destFileName);
                MessageBox.Show(String.Format(SUCCESSFULLY_TRANSLATED_MESSAGE, this._sourceFileName, this._destFileName), SUCCESS_TEXT);
            }
            catch (Exception ex)
            {
                if (!this.isCancelled)
                    MessageBox.Show(ex.Message, ERROR_TEXT);
            }

            this._translationProgress.Close();
        }

        #endregion

        #region Utilities

        private void BindComboBoxes(Dictionary<String, String> dataSources)
        {
            Dictionary<String, String> ds = new Dictionary<String, String>();
            ds.Add("-1", SELECT_TEXT);

            foreach (var item in dataSources)
                ds.Add(item.Key, item.Value);
            
            this.cbOriginalLanguage.DataSource = new BindingSource(ds, null);
            this.cbTranslationLanguage.DataSource = new BindingSource(ds, null);

            this.cbOriginalLanguage.DisplayMember = this.cbTranslationLanguage.DisplayMember = "Value";
            this.cbOriginalLanguage.ValueMember = this.cbTranslationLanguage.ValueMember = "Key";

            this.cbTranslationLanguage.DisplayMember = this.cbTranslationLanguage.DisplayMember = "Value";
            this.cbTranslationLanguage.ValueMember = this.cbTranslationLanguage.ValueMember = "Key";
        }

        private void StartTranslateFile(String sourseFileName)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;            

            XDocument sourseXDoc = XDocument.Load(sourseFileName);
            Int32 dataNodesCount = sourseXDoc.Document.Element("root").Elements("data").Count();
            
            this._translationProgress = new TranslationProgressIndicator();
            this._translationProgress.TranslationWorker = bw;
            this._translationProgress.ProgressBar.Minimum = 0;
            this._translationProgress.ProgressBar.Maximum = dataNodesCount;
            this._translationProgress.TotalItemsToTranslate = dataNodesCount;
            
            bw.RunWorkerAsync();

            this._translationProgress.ShowDialog();            
        }        

        #endregion        
    }
}
