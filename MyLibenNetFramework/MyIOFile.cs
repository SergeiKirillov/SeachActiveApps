using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibenNetFramework
{
    public class MyIOFile
    {
        /// <summary>
        /// MyIOFile - класс работы с файловой системой
        ///
        /// </summary>
        /// 


        ///<summary>
        ///WriteFileTXT - запись сообщения в текстовый файл
        /// MyIOFile.WriteFileTXT(DateTime.Now, " X:" + Screen.PrimaryScreen.Bounds.X.ToString() + " Y:" + Screen.PrimaryScreen.Bounds.Y.ToString() + " Size:" + Screen.PrimaryScreen.Bounds.Size.ToString(), "SceenShot"); //вывод в текстовы файл
        /// </summary>
        /// <param name="dtMessage"></param>
        /// <param name="Message"></param>
        /// <param name="NameFile"></param>
        #region Вывод в файл WriteFileTXT(DateTime dtMessage, string Message, string NameFile) и  WriteFileTXT(string Message, string NameFile)

        public static void WriteFileTXT(DateTime dtMessage, string Message, string NameFile)
        {
            try
            {
                if (Message != "" || Message != null || Message != " ")
                {
                    string tmptxt;
                    DateTime TimeWrite = dtMessage;

                    tmptxt = dtMessage.ToString("dd.MM.yyyy HH:mm:ss") + ";" + Message;

                    //Если не удачно то записываем в локальный файл
                    //string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + NameFile+".txt";
                    string pathProg = "D://" + NameFile + ".txt";
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

        ///<summary>
        ///WriteFileTXT - запись сообщения в текстовый файл
        ///MyIOFile.WriteFileTXT(DateTime.Now, " X:" + Screen.PrimaryScreen.Bounds.X.ToString() + " Y:" + Screen.PrimaryScreen.Bounds.Y.ToString() + " Size:" + Screen.PrimaryScreen.Bounds.Size.ToString(), "SceenShot"); //вывод в текстовы файл
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="NameFile"></param>
        public static void WriteFileTXT(string Message, string NameFile)
        {
            try
            {
                if (Message != "" || Message != null || Message != " ")
                {
                    string tmptxt;
                    DateTime TimeWrite = DateTime.Now;

                    tmptxt = TimeWrite.ToString("dd.MM.yyyy HH:mm:ss") + ";" + Message;

                    //Если не удачно то записываем в локальный файл
                    //string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + NameFile+".txt";
                    string pathProg = "D://" + NameFile + ".txt";
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
