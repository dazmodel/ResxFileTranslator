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
        private static readonly String SUCCESSFULLY_TRANSLATED_MESSAGE = "Resx file [{0}]\r\n was translated and saved to [{1}].";
        private ITranslator _translator;
        private String _originalLanguage;
        private String _translationLanguage;
        private String _sourceFileName;
        private String _destFileName;

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
                try
                {
                    XDocument translatedXDoc = this.TranslateFile(this._sourceFileName);
                    translatedXDoc.Save(this._destFileName);
                    MessageBox.Show(String.Format(SUCCESSFULLY_TRANSLATED_MESSAGE, this._sourceFileName, this._destFileName), SUCCESS_TEXT);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ERROR_TEXT);
                }
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

        private XDocument TranslateFile(String sourseFileName)
        {
            XDocument resultXDoc = new XDocument();
            XDocument sourseXDoc = XDocument.Load(sourseFileName);

            XElement root = new XElement("root");
            resultXDoc.Add(root);
           
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
                    }
                }                
            }

            return resultXDoc;
        }

        #endregion        
    }
}
