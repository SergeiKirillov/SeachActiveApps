using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeachActiveApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string strActivApp;
            string strActivAppOld=null;
            DateTime dtMessageOld=DateTime.Now;
            TimeSpan ts;


            clWinAPI.HideConsoleApp(true); //Прячем программу

            while (true)
            {
                strActivApp = clWinAPI.GetCaptionOfActiveWindow();

                if (strActivAppOld!=strActivApp)
                {
                    ts = DateTime.Now.Subtract(dtMessageOld);
                    if (ts.TotalMinutes > 1)
                    {
                        clFileRW.WriteFileTXT(DateTime.Now, strActivApp, ts);
                        strActivAppOld = strActivApp;
                        dtMessageOld = DateTime.Now;
                    }
                    else if (ts.TotalHours> 1)
                    {
                        clFileRW.WriteFileTXT(DateTime.Now, "(Работает приложение более 1 часа)"+strActivApp, ts);
                        
                    }
                    
                }
                
                //if (message0 != message)
                //{

                //    tmptxt = String.Format("{0:dd.MM.yyyy HH:mm:ss} {1}", currenttime, message);


                //}
                //else
                //{
                //    tmptxt = String.Format("{0:dd.MM.yyyy HH:mm:ss} {1}", currenttime, "-----Повтор---- " + message);

                //}


                

                Thread.Sleep(60000);
            }

        }
    }
}
