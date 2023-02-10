namespace CADinDB
{
    partial class Form1
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
            this.lblPathDB = new System.Windows.Forms.Label();
            this.btnPath = new System.Windows.Forms.Button();
            this.chkHolidays = new System.Windows.Forms.CheckBox();
            this.chkMemoryHome = new System.Windows.Forms.CheckBox();
            this.btnCADHolyday = new System.Windows.Forms.Button();
            this.btnMemoryHome = new System.Windows.Forms.Button();
            this.btnMemoryWork = new System.Windows.Forms.Button();
            this.chkMemoryWork = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblPathDB
            // 
            this.lblPathDB.AutoSize = true;
            this.lblPathDB.Location = new System.Drawing.Point(12, 17);
            this.lblPathDB.Name = "lblPathDB";
            this.lblPathDB.Size = new System.Drawing.Size(174, 13);
            this.lblPathDB.TabIndex = 1000;
            this.lblPathDB.Text = "путь к файлам БД по умолчанию";
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(570, 12);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(137, 23);
            this.btnPath.TabIndex = 1;
            this.btnPath.Text = "Изменить путь к Базам Данных";
            this.btnPath.UseVisualStyleBackColor = true;
            // 
            // chkHolidays
            // 
            this.chkHolidays.AutoSize = true;
            this.chkHolidays.Location = new System.Drawing.Point(13, 44);
            this.chkHolidays.Name = "chkHolidays";
            this.chkHolidays.Size = new System.Drawing.Size(224, 17);
            this.chkHolidays.TabIndex = 1001;
            this.chkHolidays.Text = "Вывод из БД праздничные дни страны";
            this.chkHolidays.UseVisualStyleBackColor = true;
            this.chkHolidays.CheckedChanged += new System.EventHandler(this.chkHolidays_CheckedChanged);
            // 
            // chkMemoryHome
            // 
            this.chkMemoryHome.AutoSize = true;
            this.chkMemoryHome.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkMemoryHome.Location = new System.Drawing.Point(13, 73);
            this.chkMemoryHome.Name = "chkMemoryHome";
            this.chkMemoryHome.Size = new System.Drawing.Size(244, 17);
            this.chkMemoryHome.TabIndex = 1002;
            this.chkMemoryHome.Text = "Вывести из БД памятные семейные даты ";
            this.chkMemoryHome.UseVisualStyleBackColor = true;
            // 
            // btnCADHolyday
            // 
            this.btnCADHolyday.Location = new System.Drawing.Point(256, 44);
            this.btnCADHolyday.Name = "btnCADHolyday";
            this.btnCADHolyday.Size = new System.Drawing.Size(246, 22);
            this.btnCADHolyday.TabIndex = 1003;
            this.btnCADHolyday.Text = "Добавить/Редактировать/Удалить из БД";
            this.btnCADHolyday.UseVisualStyleBackColor = true;
            // 
            // btnMemoryHome
            // 
            this.btnMemoryHome.Location = new System.Drawing.Point(256, 73);
            this.btnMemoryHome.Name = "btnMemoryHome";
            this.btnMemoryHome.Size = new System.Drawing.Size(246, 21);
            this.btnMemoryHome.TabIndex = 1004;
            this.btnMemoryHome.Text = "Добавить/Редактировать/Удалить из БД";
            this.btnMemoryHome.UseVisualStyleBackColor = true;
            // 
            // btnMemoryWork
            // 
            this.btnMemoryWork.Location = new System.Drawing.Point(258, 97);
            this.btnMemoryWork.Name = "btnMemoryWork";
            this.btnMemoryWork.Size = new System.Drawing.Size(246, 23);
            this.btnMemoryWork.TabIndex = 1006;
            this.btnMemoryWork.Text = "Добавить/Редактировать/Удалить из БД";
            this.btnMemoryWork.UseVisualStyleBackColor = true;
            // 
            // chkMemoryWork
            // 
            this.chkMemoryWork.AutoSize = true;
            this.chkMemoryWork.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkMemoryWork.Location = new System.Drawing.Point(15, 97);
            this.chkMemoryWork.Name = "chkMemoryWork";
            this.chkMemoryWork.Size = new System.Drawing.Size(203, 17);
            this.chkMemoryWork.TabIndex = 1005;
            this.chkMemoryWork.Text = "Вывести из БД памятки по работе";
            this.chkMemoryWork.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnMemoryWork);
            this.Controls.Add(this.chkMemoryWork);
            this.Controls.Add(this.btnMemoryHome);
            this.Controls.Add(this.btnCADHolyday);
            this.Controls.Add(this.chkMemoryHome);
            this.Controls.Add(this.chkHolidays);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.lblPathDB);
            this.Name = "Form1";
            this.Text = "Редактируем данные которые будут выводиться на экранной заставке";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPathDB;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox chkHolidays;
        private System.Windows.Forms.CheckBox chkMemoryHome;
        private System.Windows.Forms.Button btnCADHolyday;
        private System.Windows.Forms.Button btnMemoryHome;
        private System.Windows.Forms.Button btnMemoryWork;
        private System.Windows.Forms.CheckBox chkMemoryWork;
    }
}

