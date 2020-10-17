﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeachActiveApp
{
    class clFileRW
    {
        #region Вывод в файл
        public static void WriteFileTXT(DateTime dt, string message, TimeSpan timeSpan)
        {
            try
            {
                if (message != "" || message != null || message != " ")
                {
                    string tmptxt;
                    DateTime TimeWrite = dt;

                    tmptxt = dt.ToString("dd.MM.yyyy HH:mm:ss") + " - " + message;

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

    }
}
