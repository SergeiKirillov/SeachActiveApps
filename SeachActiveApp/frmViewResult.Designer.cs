namespace SeachActiveApp
{
    partial class frmViewResult
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
            this.GridViewApps = new System.Windows.Forms.DataGridView();
            this.DataSelect = new System.Windows.Forms.DateTimePicker();
            this.chkSelect = new System.Windows.Forms.ComboBox();
            this.btnRunningSelectQuery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewApps)).BeginInit();
            this.SuspendLayout();
            // 
            // GridViewApps
            // 
            this.GridViewApps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewApps.Location = new System.Drawing.Point(12, 39);
            this.GridViewApps.Name = "GridViewApps";
            this.GridViewApps.Size = new System.Drawing.Size(776, 399);
            this.GridViewApps.TabIndex = 4;
            // 
            // DataSelect
            // 
            this.DataSelect.Location = new System.Drawing.Point(174, 13);
            this.DataSelect.Name = "DataSelect";
            this.DataSelect.Size = new System.Drawing.Size(173, 20);
            this.DataSelect.TabIndex = 2;
            this.DataSelect.Visible = false;
            // 
            // chkSelect
            // 
            this.chkSelect.FormattingEnabled = true;
            this.chkSelect.Items.AddRange(new object[] {
            "Отчет за день",
            "Отчет за месяц",
            "All"});
            this.chkSelect.Location = new System.Drawing.Point(12, 12);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(141, 21);
            this.chkSelect.TabIndex = 1;
            this.chkSelect.SelectedIndexChanged += new System.EventHandler(this.chkSelect_SelectedIndexChanged);
            // 
            // btnRunningSelectQuery
            // 
            this.btnRunningSelectQuery.Location = new System.Drawing.Point(449, 10);
            this.btnRunningSelectQuery.Name = "btnRunningSelectQuery";
            this.btnRunningSelectQuery.Size = new System.Drawing.Size(339, 23);
            this.btnRunningSelectQuery.TabIndex = 3;
            this.btnRunningSelectQuery.Text = "применить выбранный критерий";
            this.btnRunningSelectQuery.UseVisualStyleBackColor = true;
            this.btnRunningSelectQuery.Click += new System.EventHandler(this.btnRunningSelectQuery_Click);
            // 
            // frmViewResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRunningSelectQuery);
            this.Controls.Add(this.chkSelect);
            this.Controls.Add(this.DataSelect);
            this.Controls.Add(this.GridViewApps);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewResult";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотр результатов работы программы";
            ((System.ComponentModel.ISupportInitialize)(this.GridViewApps)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridViewApps;
        private System.Windows.Forms.DateTimePicker DataSelect;
        private System.Windows.Forms.ComboBox chkSelect;
        private System.Windows.Forms.Button btnRunningSelectQuery;
    }
}