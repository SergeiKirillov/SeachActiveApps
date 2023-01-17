namespace SeachActiveApp
{
    partial class frmSettingApp
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
            this.chkSaveToFiles = new System.Windows.Forms.CheckBox();
            this.chkSaveToBD = new System.Windows.Forms.CheckBox();
            this.chkDisableScreenSave = new System.Windows.Forms.CheckBox();
            this.txtTimeDisableScreenSave = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkSaveToFiles
            // 
            this.chkSaveToFiles.AutoSize = true;
            this.chkSaveToFiles.Location = new System.Drawing.Point(59, 32);
            this.chkSaveToFiles.Name = "chkSaveToFiles";
            this.chkSaveToFiles.Size = new System.Drawing.Size(189, 17);
            this.chkSaveToFiles.TabIndex = 0;
            this.chkSaveToFiles.Text = "сохранение результатов в файл";
            this.chkSaveToFiles.UseVisualStyleBackColor = true;
            // 
            // chkSaveToBD
            // 
            this.chkSaveToBD.AutoSize = true;
            this.chkSaveToBD.Location = new System.Drawing.Point(59, 55);
            this.chkSaveToBD.Name = "chkSaveToBD";
            this.chkSaveToBD.Size = new System.Drawing.Size(226, 17);
            this.chkSaveToBD.TabIndex = 1;
            this.chkSaveToBD.Text = "сохранение результатов в базу данных";
            this.chkSaveToBD.UseVisualStyleBackColor = true;
            // 
            // chkDisableScreenSave
            // 
            this.chkDisableScreenSave.AutoSize = true;
            this.chkDisableScreenSave.Location = new System.Drawing.Point(59, 113);
            this.chkDisableScreenSave.Name = "chkDisableScreenSave";
            this.chkDisableScreenSave.Size = new System.Drawing.Size(311, 17);
            this.chkDisableScreenSave.TabIndex = 2;
            this.chkDisableScreenSave.Text = "Отключение Экранной заставки при работе программы";
            this.chkDisableScreenSave.UseVisualStyleBackColor = true;
            this.chkDisableScreenSave.CheckedChanged += new System.EventHandler(this.chkDisableScreenSave_CheckedChanged);
            // 
            // txtTimeDisableScreenSave
            // 
            this.txtTimeDisableScreenSave.Location = new System.Drawing.Point(59, 136);
            this.txtTimeDisableScreenSave.Name = "txtTimeDisableScreenSave";
            this.txtTimeDisableScreenSave.Size = new System.Drawing.Size(32, 20);
            this.txtTimeDisableScreenSave.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Через какое время экранная заставка будет отключена";
            // 
            // frmSettingApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTimeDisableScreenSave);
            this.Controls.Add(this.chkDisableScreenSave);
            this.Controls.Add(this.chkSaveToBD);
            this.Controls.Add(this.chkSaveToFiles);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettingApp";
            this.ShowIcon = false;
            this.Text = "Настройка программы";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSaveToFiles;
        private System.Windows.Forms.CheckBox chkSaveToBD;
        private System.Windows.Forms.CheckBox chkDisableScreenSave;
        private System.Windows.Forms.TextBox txtTimeDisableScreenSave;
        private System.Windows.Forms.Label label1;
    }
}