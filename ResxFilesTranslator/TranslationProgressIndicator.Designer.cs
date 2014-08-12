namespace ResxFilesTranslator
{
    partial class TranslationProgressIndicator
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
            this.pbTranslationProgress = new System.Windows.Forms.ProgressBar();
            this.btnCancelTranslation = new System.Windows.Forms.Button();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbTranslationProgress
            // 
            this.pbTranslationProgress.Location = new System.Drawing.Point(12, 21);
            this.pbTranslationProgress.Name = "pbTranslationProgress";
            this.pbTranslationProgress.Size = new System.Drawing.Size(478, 37);
            this.pbTranslationProgress.TabIndex = 0;
            this.pbTranslationProgress.UseWaitCursor = true;
            // 
            // btnCancelTranslation
            // 
            this.btnCancelTranslation.Location = new System.Drawing.Point(12, 104);
            this.btnCancelTranslation.Name = "btnCancelTranslation";
            this.btnCancelTranslation.Size = new System.Drawing.Size(478, 29);
            this.btnCancelTranslation.TabIndex = 1;
            this.btnCancelTranslation.Text = "Cancel Translation";
            this.btnCancelTranslation.UseVisualStyleBackColor = true;
            this.btnCancelTranslation.UseWaitCursor = true;
            this.btnCancelTranslation.Click += new System.EventHandler(this.btnCancelTranslation_Click);
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(158, 74);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentStatus.TabIndex = 2;
            this.lblCurrentStatus.UseWaitCursor = true;
            // 
            // TranslationProgressIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 145);
            this.ControlBox = false;
            this.Controls.Add(this.lblCurrentStatus);
            this.Controls.Add(this.btnCancelTranslation);
            this.Controls.Add(this.pbTranslationProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TranslationProgressIndicator";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Translation Progress...";
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbTranslationProgress;
        private System.Windows.Forms.Button btnCancelTranslation;
        private System.Windows.Forms.Label lblCurrentStatus;
    }
}