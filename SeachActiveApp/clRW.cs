﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace SeachActiveApp
{
    class clRW
    {
        public clRW()
        {

        }


        public clRW(DateTime dt, string message, int time1min)
        {
            if (Properties.Settings.Default.blWriteFile)
            {
                WriteFileTXT(dt, message, time1min);
            }

            if (Properties.Settings.Default.blWriteBD)
            {
                WriteBD(dt, message, time1min);
            }

        }

        #region считываем значения с БД 

        public IList<clData1Hour> GetAll()
        {
            string NameDB = DateTime.Now.ToString("dd-MM-yyyy");
            string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + NameDB + ".db";

            var rezult = new List<clData1Hour>();

            using (var db = new LiteDatabase(pathProg))
            {
                var apps = db.GetCollection<clData1Hour>("Hour1");
                var resultsLDB = apps.FindAll().OrderByDescending(x => x.dtApp);

                foreach (clData1Hour item in resultsLDB)
                {
                    rezult.Add(item);
                }

                return rezult;
            }
        }

        #endregion


        #region Вывод в файл
        private static void WriteFileTXT(DateTime dt, string message, int time1min)
        {
            try
            {
                if (message != "" || message != null || message != " ")
                {
                    string tmptxt;
                    DateTime TimeWrite = dt;

                    tmptxt = dt.ToString("dd.MM.yyyy HH:mm:ss") + ";" + message;

                    //Если не удачно то записываем в локальный файл
                    string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log.txt";
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathProg, true))
                    {

                        file.WriteLine(tmptxt);
                        file.Close();
                    }


                }

            }
            catch
            { }
        }
        #endregion

        #region вывод в Базу данных
        private static void WriteBD(DateTime dt, string message, int time1min)
        {
            try
            {
                string NameDB = DateTime.Now.ToString("dd-MM-yyyy");
                string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + NameDB + ".db";


                //TODO LiteDB 5.0.9
                var DataWrite = new clData1Hour
                {
                    ID = Guid.NewGuid(),
                    dtApp = dt,
                    strApp = message,
                    Raz1Minut = time1min
                };

                using (var db = new LiteDatabase(pathProg))
                {
                    var DBWrite = db.GetCollection<clData1Hour>("Hour1");

                    DBWrite.Insert(DataWrite);
                    IndexIsWrite(DBWrite);

                }


            }
            catch (Exception exc)
            {
                // в случае ошибки записываем её в файл логов
                WriteFileTXT(DateTime.Now,exc.Message,1);
            }
            
        }

        private static void IndexIsWrite(ILiteCollection<clData1Hour> dBWrite)
        {
            dBWrite.EnsureIndex(x => x.ID);
            dBWrite.EnsureIndex(x => x.dtApp);
            dBWrite.EnsureIndex(x => x.strApp);
        }

        #endregion


        


    }
}
