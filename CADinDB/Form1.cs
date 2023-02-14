using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CADinDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string FileName = "MyDB.db"; //Имя Файла где храниться файлы из БД

            if (MyIO.PathAPP(FileName))
            {
                lblPathDB.Enabled = true;
                
                lblPathDB.Text = MyIO.myPath+ FileName;

            }
            else
            {
                //если путь+файл не существует то выводим окно с предложением выбора пути(по умолчанию путь из MyIO.myPath )
                //
                var result = MessageBox.Show("Файл баз данных не найден. Укажите путь где они находяться или где будут храниться.","Ошибка в пути файла БД", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (DBfolderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        MyIO.myPath = DBfolderBrowserDialog.SelectedPath;

                        string pathDB1 = MyIO.myPath + "\\" + FileName;

                        if (File.Exists(pathDB1))
                        {
                            lblPathDB.Text = pathDB1;
                        }
                        else
                        {
                            if (MyDBsqlite.CreateDB(MyIO.myPath, FileName))
                            {
                                lblPathDB.Text = pathDB1;
                            }

                        }

                       

                        lblPathDB.Enabled = true;
                    }
                }
                else
                {
                    Application.Exit();
                }

               
               
                lblPathDB.Enabled = false;
            }
            
            btnPath.Left =  lblPathDB.Width+30;

            //открывам базу и проверяем на наличие таблиц.
            //Если таблица есть то checkbox enable
            //Если таблицы нет то checkbox не активный

            string pathDB = MyIO.myPath + "\\" + FileName;

            //using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source="+ pathDB +"; Version=3;"))
            //{
            //    Connect.Open();
            //    #region Var1

            //    //string commandText = "SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name='tbHoliday'";
            //    //SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
            //    //using (SQLiteDataReader dataReader = Command.ExecuteReader())
            //    //{
            //    //    if (dataReader.HasRows)
            //    //    {
            //    //        //Таблица найдена
            //    //        while (dataReader.Read())
            //    //        {
            //    //            string str1= dataReader.GetString(0);
            //    //        }
            //    //    }
            //    //    else
            //    //    {
            //    //    //Таблицы нет, поэтому мы создаем её
            //    //    //https://developmentsolutionsjunction.blogspot.com/2020/09/sqlite-check-if-table-exists.html

            //    //    }

            //    //}
            //    #endregion

            //    #region Variant2 - При отсутствии таблицы создаем её
            //    //CREATE TABLE IF NOT EXISTS table_name (table_definition); 
            //    //создаем таблицу если ее нет

            //    //https://tableplus.com/blog/2018/07/sqlite-how-to-use-datetime-value.html 
            //    //https://metanit.com/sql/sqlite/6.2.php

            //    //string commandText = "CREATE TABLE IF NOT EXISTS tbHoliday ([id] INTEGER NOT NULL,[dtEvent] REAL,[strEvent] NVARCHAR(128), PRIMARY KEY ([id] AUTOINCREMENT))";

            //    


            //    SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
            //    Connect.Open();
            //    Command.ExecuteNonQuery();
            //    Connect.Close();

            //   #endregion




            //   Connect.Close();
            //}



            //string sqlExpression = "CREATE TABLE IF NOT EXISTS tbHoliday " +
            //    "(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
            //    "dtEvent REAL," +
            //    "strEvent NVARCHAR(128))";


            //using (var connection = new SQLiteConnection("Data Source="+ pathDB + "; Version=3;"))
            //{
            //    connection.Open();

            //    SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
            //    command.ExecuteNonQuery();

            //}

            MyDBsqlite.CreateTabMemory(pathDB, "tbHoliday");
            MyDBsqlite.CreateTabMemory(pathDB, "tbMemoryHome");
            MyDBsqlite.CreateTabMemory(pathDB, "tbMemoryWork");
        }

        private void chkHolidays_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
