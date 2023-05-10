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
            this.chkScreenShotDesktop = new System.Windows.Forms.CheckBox();
            this.chkEnableSeachActiveApp = new System.Windows.Forms.CheckBox();
            this.lnkWWW = new System.Windows.Forms.LinkLabel();
            this.chkAutoStartInWindows = new System.Windows.Forms.CheckBox();
            this.lblAutoStartInWindows = new System.Windows.Forms.Label();
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
            this.chkSaveToFiles.Click += new System.EventHandler(this.chkSaveToFiles_Click);
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
            this.chkSaveToBD.Click += new System.EventHandler(this.chkSaveToBD_Click);
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
            this.chkDisableScreenSave.Click += new System.EventHandler(this.chkDisableScreenSave_Click);
            // 
            // txtTimeDisableScreenSave
            // 
            this.txtTimeDisableScreenSave.Location = new System.Drawing.Point(59, 136);
            this.txtTimeDisableScreenSave.Name = "txtTimeDisableScreenSave";
            this.txtTimeDisableScreenSave.Size = new System.Drawing.Size(32, 20);
            this.txtTimeDisableScreenSave.TabIndex = 3;
            this.txtTimeDisableScreenSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimeDisableScreenSave_KeyDown);
            this.txtTimeDisableScreenSave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimeDisableScreenSave_KeyPress);
            this.txtTimeDisableScreenSave.Leave += new System.EventHandler(this.txtTimeDisableScreenSave_Leave);
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
            // chkScreenShotDesktop
            // 
            this.chkScreenShotDesktop.AutoSize = true;
            this.chkScreenShotDesktop.Location = new System.Drawing.Point(59, 204);
            this.chkScreenShotDesktop.Name = "chkScreenShotDesktop";
            this.chkScreenShotDesktop.Size = new System.Drawing.Size(351, 17);
            this.chkScreenShotDesktop.TabIndex = 5;
            this.chkScreenShotDesktop.Text = "Применение скриншота рабочего стола как экранная заставка";
            this.chkScreenShotDesktop.UseVisualStyleBackColor = true;
            this.chkScreenShotDesktop.Click += new System.EventHandler(this.chkScreenShotDesktop_Click);
            // 
            // chkEnableSeachActiveApp
            // 
            this.chkEnableSeachActiveApp.AutoSize = true;
            this.chkEnableSeachActiveApp.Location = new System.Drawing.Point(59, 9);
            this.chkEnableSeachActiveApp.Name = "chkEnableSeachActiveApp";
            this.chkEnableSeachActiveApp.Size = new System.Drawing.Size(468, 17);
            this.chkEnableSeachActiveApp.TabIndex = 6;
            this.chkEnableSeachActiveApp.Text = "Активировать модуль сбора информации об активном запущенном приложении(1мин)";
            this.chkEnableSeachActiveApp.UseVisualStyleBackColor = true;
            this.chkEnableSeachActiveApp.CheckedChanged += new System.EventHandler(this.chkEnableSeachActiveApp_CheckedChanged);
            // 
            // lnkWWW
            // 
            this.lnkWWW.AutoSize = true;
            this.lnkWWW.Location = new System.Drawing.Point(59, 79);
            this.lnkWWW.Name = "lnkWWW";
            this.lnkWWW.Size = new System.Drawing.Size(185, 13);
            this.lnkWWW.TabIndex = 7;
            this.lnkWWW.TabStop = true;
            this.lnkWWW.Text = "Открыть локальный WWW сервер";
            this.lnkWWW.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWWW_LinkClicked);
            // 
            // chkAutoStartInWindows
            // 
            this.chkAutoStartInWindows.AutoSize = true;
            this.chkAutoStartInWindows.Location = new System.Drawing.Point(59, 253);
            this.chkAutoStartInWindows.Name = "chkAutoStartInWindows";
            this.chkAutoStartInWindows.Size = new System.Drawing.Size(315, 17);
            this.chkAutoStartInWindows.TabIndex = 8;
            this.chkAutoStartInWindows.Text = "Автоматический старт программы при запуске Windows";
            this.chkAutoStartInWindows.UseVisualStyleBackColor = true;
            this.chkAutoStartInWindows.Click += new System.EventHandler(this.chkAutoStartInWindows_Click);
            // 
            // lblAutoStartInWindows
            // 
            this.lblAutoStartInWindows.AutoSize = true;
            this.lblAutoStartInWindows.Location = new System.Drawing.Point(59, 277);
            this.lblAutoStartInWindows.Name = "lblAutoStartInWindows";
            this.lblAutoStartInWindows.Size = new System.Drawing.Size(49, 13);
            this.lblAutoStartInWindows.TabIndex = 9;
            this.lblAutoStartInWindows.Text = "_______";
            // 
            // frmSettingApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblAutoStartInWindows);
            this.Controls.Add(this.chkAutoStartInWindows);
            this.Controls.Add(this.lnkWWW);
            this.Controls.Add(this.chkEnableSeachActiveApp);
            this.Controls.Add(this.chkScreenShotDesktop);
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
            this.Load += new System.EventHandler(this.frmSettingApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSaveToFiles;
        private System.Windows.Forms.CheckBox chkSaveToBD;
        private System.Windows.Forms.CheckBox chkDisableScreenSave;
        private System.Windows.Forms.TextBox txtTimeDisableScreenSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkScreenShotDesktop;
        private System.Windows.Forms.CheckBox chkEnableSeachActiveApp;
        private System.Windows.Forms.LinkLabel lnkWWW;
        private System.Windows.Forms.CheckBox chkAutoStartInWindows;
        private System.Windows.Forms.Label lblAutoStartInWindows;
    }
}