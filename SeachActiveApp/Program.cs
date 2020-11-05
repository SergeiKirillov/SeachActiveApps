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
            DateTime dtAppOld;
            DateTime dtActiveApp;
            TimeSpan ts;

            //считывание текущих значений из реестра только в момент запуска приложения и после этого используем локальную переменную
            strActivAppOld = clReg.ReadAppParam("NameAppOld");
            dtAppOld = Convert.ToDateTime(clReg.ReadAppParam("dtAppOld"));


            clWinAPI.HideConsoleApp(true); //Прячем программу

            

            while (true)
            {
                
                strActivApp = clWinAPI.GetCaptionOfActiveWindow();
                dtActiveApp = DateTime.Now;


                ts = dtActiveApp.Subtract(dtAppOld);

                if (strActivAppOld != strActivApp)
                {

                    if (ts.TotalMinutes > 1)
                    {
                        clFileRW.WriteFileTXT(dtActiveApp, strActivApp, ts);
                        
                        strActivAppOld = strActivApp;
                        dtAppOld = dtActiveApp;

                        //запись в реестр текущих значений
                        clReg.WriteAppParam("ActiveApp", strActivApp);
                        clReg.WriteAppParam("TimeActiveApp", dtActiveApp.ToString());

                    }


                }
                else if (ts.TotalHours > 1)
                {
                    clFileRW.WriteFileTXT(dtActiveApp, "(Работает приложение более 1 часа)" + strActivApp, ts);

                    if (clReg.WriteMaxAppReg(dtAppOld, dtActiveApp, ts, strActivApp))
                    {
                        //сброс значений если произошла запись 
                        
                    }


                }
                else
                {
                    //если приложение не меняется то записываем в реестр значение времени его работы
                    clReg.WriteAppParam("SpanActiveApp", ts.TotalMinutes.ToString());
                }
                   
                

                
                               
                Thread.Sleep(60000); //спим 1 мин
            }

        }
    }
}
