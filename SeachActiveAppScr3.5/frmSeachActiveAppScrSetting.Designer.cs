namespace SeachActiveAppScr3._5
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
            this.txtBox = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSettingText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBox
            // 
            this.txtBox.Location = new System.Drawing.Point(17, 64);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(337, 20);
            this.txtBox.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(17, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(160, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(194, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(349, 26);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Настройки Экранной заставки";
            // 
            // lblSettingText
            // 
            this.lblSettingText.AutoSize = true;
            this.lblSettingText.Location = new System.Drawing.Point(14, 48);
            this.lblSettingText.Name = "lblSettingText";
            this.lblSettingText.Size = new System.Drawing.Size(342, 13);
            this.lblSettingText.TabIndex = 4;
            this.lblSettingText.Text = "Введите текст который будет отображаться в экранной заставке";
            // 
            // frmSeachActiveAppScrSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblSettingText);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtBox);
            this.Name = "frmSeachActiveAppScrSetting";
            this.Text = "Параметры настройки программы";
            this.Load += new System.EventHandler(this.frmSeachActiveAppScrSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSettingText;
    }
}