﻿namespace SeachActiveAppSCR
{
    partial class frmScreenSaver
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblStopTimeScreenSaver = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.Label();
            this.MoveTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblStopTimeScreenSaver
            // 
            this.lblStopTimeScreenSaver.AutoSize = true;
            this.lblStopTimeScreenSaver.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStopTimeScreenSaver.ForeColor = System.Drawing.Color.Yellow;
            this.lblStopTimeScreenSaver.Location = new System.Drawing.Point(13, 13);
            this.lblStopTimeScreenSaver.Name = "lblStopTimeScreenSaver";
            this.lblStopTimeScreenSaver.Size = new System.Drawing.Size(98, 108);
            this.lblStopTimeScreenSaver.TabIndex = 0;
            this.lblStopTimeScreenSaver.Text = "0";
            // 
            // txtLabel
            // 
            this.txtLabel.AutoSize = true;
            this.txtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtLabel.ForeColor = System.Drawing.Color.Red;
            this.txtLabel.Location = new System.Drawing.Point(491, 161);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(233, 73);
            this.txtLabel.TabIndex = 1;
            this.txtLabel.Text = "ЛПЦ-2";
            // 
            // MoveTimer
            // 
            this.MoveTimer.Tick += new System.EventHandler(this.MoveTimer_Tick);
            // 
            // frmScreenSaver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.lblStopTimeScreenSaver);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmScreenSaver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.frmScreenSaver_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmScreenSaver_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmScreenSaver_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmScreenSaver_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStopTimeScreenSaver;
        private System.Windows.Forms.Label txtLabel;
        private System.Windows.Forms.Timer MoveTimer;
    }
}
