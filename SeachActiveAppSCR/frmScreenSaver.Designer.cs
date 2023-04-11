namespace SeachActiveAppSCR
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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStopTimeScreenSaver
            // 
            this.lblStopTimeScreenSaver.AutoSize = true;
            this.lblStopTimeScreenSaver.BackColor = System.Drawing.Color.Transparent;
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
            this.txtLabel.BackColor = System.Drawing.Color.Transparent;
            this.txtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtLabel.ForeColor = System.Drawing.Color.Red;
            this.txtLabel.Location = new System.Drawing.Point(368, 13);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(344, 108);
            this.txtLabel.TabIndex = 1;
            this.txtLabel.Text = "ЛПЦ-2";
            // 
            // MoveTimer
            // 
            this.MoveTimer.Tick += new System.EventHandler(this.MoveTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(27, 275);
            this.label1.MaximumSize = new System.Drawing.Size(600, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 2;
            // 
            // frmScreenSaver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.lblStopTimeScreenSaver);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmScreenSaver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ftmScreenSave";
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
        private System.Windows.Forms.Label label1;
    }
}

