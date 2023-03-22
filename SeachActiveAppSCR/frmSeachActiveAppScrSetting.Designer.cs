namespace SeachActiveAppSCR
{
    partial class frmSeachActiveAppScrSetting
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.chkTimeNow = new System.Windows.Forms.CheckBox();
            this.chkText = new System.Windows.Forms.CheckBox();
            this.txtBox = new System.Windows.Forms.TextBox();
            this.lblSettingText = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkDesktopToScreenShot = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(349, 26);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Настройки Экранной заставки";
            // 
            // chkTimeNow
            // 
            this.chkTimeNow.AutoSize = true;
            this.chkTimeNow.Location = new System.Drawing.Point(17, 49);
            this.chkTimeNow.Name = "chkTimeNow";
            this.chkTimeNow.Size = new System.Drawing.Size(169, 17);
            this.chkTimeNow.TabIndex = 6;
            this.chkTimeNow.Text = "Отображать текущее время";
            this.chkTimeNow.UseVisualStyleBackColor = true;
            this.chkTimeNow.CheckedChanged += new System.EventHandler(this.chkTimeNow_CheckedChanged);
            // 
            // chkText
            // 
            this.chkText.AutoSize = true;
            this.chkText.Location = new System.Drawing.Point(17, 72);
            this.chkText.Name = "chkText";
            this.chkText.Size = new System.Drawing.Size(119, 17);
            this.chkText.TabIndex = 7;
            this.chkText.Text = "Отображать текст";
            this.chkText.UseVisualStyleBackColor = true;
            this.chkText.CheckedChanged += new System.EventHandler(this.chkText_CheckedChanged);
            // 
            // txtBox
            // 
            this.txtBox.Location = new System.Drawing.Point(142, 69);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(337, 20);
            this.txtBox.TabIndex = 8;
            this.txtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // lblSettingText
            // 
            this.lblSettingText.AutoSize = true;
            this.lblSettingText.Location = new System.Drawing.Point(14, 110);
            this.lblSettingText.Name = "lblSettingText";
            this.lblSettingText.Size = new System.Drawing.Size(342, 13);
            this.lblSettingText.TabIndex = 9;
            this.lblSettingText.Text = "Введите текст который будет отображаться в экранной заставке";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 188);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(160, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(196, 188);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkDesktopToScreenShot
            // 
            this.chkDesktopToScreenShot.AutoSize = true;
            this.chkDesktopToScreenShot.Location = new System.Drawing.Point(17, 135);
            this.chkDesktopToScreenShot.Name = "chkDesktopToScreenShot";
            this.chkDesktopToScreenShot.Size = new System.Drawing.Size(239, 17);
            this.chkDesktopToScreenShot.TabIndex = 12;
            this.chkDesktopToScreenShot.Text = "Рабочий стол, как фоновое изображение ";
            this.chkDesktopToScreenShot.UseVisualStyleBackColor = true;
            this.chkDesktopToScreenShot.CheckedChanged += new System.EventHandler(this.chkDesktopToScreenShot_CheckedChanged);
            // 
            // frmSeachActiveAppScrSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chkDesktopToScreenShot);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblSettingText);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.chkText);
            this.Controls.Add(this.chkTimeNow);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmSeachActiveAppScrSetting";
            this.Text = "Настройка программы";
            this.Load += new System.EventHandler(this.frmSeachActiveAppScrSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox chkTimeNow;
        private System.Windows.Forms.CheckBox chkText;
        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Label lblSettingText;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkDesktopToScreenShot;
    }
}