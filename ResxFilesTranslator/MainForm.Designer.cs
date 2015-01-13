namespace ResxFilesTranslator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowseForSource = new System.Windows.Forms.Button();
            this.txtSourceFilePath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBrowseForDestination = new System.Windows.Forms.Button();
            this.txtDestFilePath = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbOriginalLanguage = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbTranslationLanguage = new System.Windows.Forms.ComboBox();
            this.dlgSourceFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgDestinationFile = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowseForSource);
            this.groupBox1.Controls.Add(this.txtSourceFilePath);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source Resource File";
            // 
            // btnBrowseForSource
            // 
            this.btnBrowseForSource.Location = new System.Drawing.Point(339, 19);
            this.btnBrowseForSource.Name = "btnBrowseForSource";
            this.btnBrowseForSource.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseForSource.TabIndex = 1;
            this.btnBrowseForSource.Text = "Browse...";
            this.btnBrowseForSource.UseVisualStyleBackColor = true;
            this.btnBrowseForSource.Click += new System.EventHandler(this.btnBrowseForSource_Click);
            // 
            // txtSourceFilePath
            // 
            this.txtSourceFilePath.Location = new System.Drawing.Point(6, 21);
            this.txtSourceFilePath.Name = "txtSourceFilePath";
            this.txtSourceFilePath.Size = new System.Drawing.Size(327, 20);
            this.txtSourceFilePath.TabIndex = 0;
            this.txtSourceFilePath.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBrowseForDestination);
            this.groupBox2.Controls.Add(this.txtDestFilePath);
            this.groupBox2.Location = new System.Drawing.Point(13, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 55);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination Resource File";
            // 
            // btnBrowseForDestination
            // 
            this.btnBrowseForDestination.Location = new System.Drawing.Point(339, 19);
            this.btnBrowseForDestination.Name = "btnBrowseForDestination";
            this.btnBrowseForDestination.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseForDestination.TabIndex = 1;
            this.btnBrowseForDestination.Text = "Browse...";
            this.btnBrowseForDestination.UseVisualStyleBackColor = true;
            this.btnBrowseForDestination.Click += new System.EventHandler(this.btnBrowseForDestination_Click);
            // 
            // txtDestFilePath
            // 
            this.txtDestFilePath.Location = new System.Drawing.Point(6, 21);
            this.txtDestFilePath.Name = "txtDestFilePath";
            this.txtDestFilePath.Size = new System.Drawing.Size(327, 20);
            this.txtDestFilePath.TabIndex = 0;
            this.txtDestFilePath.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(358, 197);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Close";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnTranslate
            // 
            this.btnTranslate.Enabled = false;
            this.btnTranslate.Location = new System.Drawing.Point(277, 197);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(75, 23);
            this.btnTranslate.TabIndex = 4;
            this.btnTranslate.Text = "Translate";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbOriginalLanguage);
            this.groupBox3.Location = new System.Drawing.Point(12, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(205, 56);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Original Language";
            // 
            // cbOriginalLanguage
            // 
            this.cbOriginalLanguage.FormattingEnabled = true;
            this.cbOriginalLanguage.Location = new System.Drawing.Point(7, 20);
            this.cbOriginalLanguage.Name = "cbOriginalLanguage";
            this.cbOriginalLanguage.Size = new System.Drawing.Size(192, 21);
            this.cbOriginalLanguage.TabIndex = 0;
            this.cbOriginalLanguage.SelectedIndexChanged += new System.EventHandler(this.cbOriginalLanguage_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbTranslationLanguage);
            this.groupBox4.Location = new System.Drawing.Point(228, 135);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(205, 56);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Translation Language";
            // 
            // cbTranslationLanguage
            // 
            this.cbTranslationLanguage.FormattingEnabled = true;
            this.cbTranslationLanguage.Location = new System.Drawing.Point(7, 20);
            this.cbTranslationLanguage.Name = "cbTranslationLanguage";
            this.cbTranslationLanguage.Size = new System.Drawing.Size(192, 21);
            this.cbTranslationLanguage.TabIndex = 1;
            this.cbTranslationLanguage.SelectedIndexChanged += new System.EventHandler(this.cbTranslationLanguage_SelectedIndexChanged);
            // 
            // dlgSourceFile
            // 
            this.dlgSourceFile.Filter = "Resx files|*.resx";
            this.dlgSourceFile.Title = "Source Resx File to translate";
            this.dlgSourceFile.FileOk += new System.ComponentModel.CancelEventHandler(this.dlgSourceFile_FileOk);
            // 
            // dlgDestinationFile
            // 
            this.dlgDestinationFile.DefaultExt = "resx";
            this.dlgDestinationFile.Filter = "Resx Files|*.resx";
            this.dlgDestinationFile.Title = "Translation Destination Resx file";
            this.dlgDestinationFile.FileOk += new System.ComponentModel.CancelEventHandler(this.dlgDestinationFile_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 228);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resource Files Translator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowseForSource;
        private System.Windows.Forms.TextBox txtSourceFilePath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBrowseForDestination;
        private System.Windows.Forms.TextBox txtDestFilePath;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbOriginalLanguage;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbTranslationLanguage;
        private System.Windows.Forms.OpenFileDialog dlgSourceFile;
        private System.Windows.Forms.SaveFileDialog dlgDestinationFile;
    }
}

