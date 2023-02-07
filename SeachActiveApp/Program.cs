using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class Globals
{
    //private static bool _blDisableScreenSave;

    //Создание участка разделяемой памяти
    //Первый параметр - название участка, 
    //второй - длина участка памяти в байтах: тип char  занимает 2 байта 
    //плюс четыре байта для одного объекта типа Integer(size)
    //плюс четыре байта для одного объекта типа Integer(Кол-во оставшихся минут)
    public static MemoryMappedFile SharedMemory = MemoryMappedFile.CreateOrOpen("TimeDisableScreenSave", 4 * 2 + 4 + 4);

    public static bool blDisableScreenSave //bool отключение заставки
    {
        //если в реестре нет записи о настройке программы, то принимаем значение False;
        //если значение есть то возвращаем значение из реестра
        get 
        {
            #region Var-1 read bool 
            //Microsoft.Win32.RegistryKey key;
            //key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SergeiAKirApp");
            //if (key != null)
            //{
            //    string DSS = key.GetValue("DisableScreenSave") as string;
            //    return Convert.ToBoolean(DSS);
            //}
            //else
            //{
            //    return false;
            //}
            #endregion
            #region Var-2 read bool 
            return WorkInReestr.blToAPP("DisableScreenSave");
            #endregion
            
 
        }

        //Записываем значение в реестр если значение удовлетворяет условию 
        set 
        {
            #region Var-1 Write bool
            ////Запись в реестр значения value
            //Microsoft.Win32.RegistryKey key;
            //key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SergeiAKirApp");
            //key.SetValue("DisableScreenSave", value);
            //key.Close();
            #endregion

            #region Var-2 Write bool
            WorkInReestr.strAPPTo("DisableScreenSave", value.ToString());
            #endregion

        }
    }

    public static int intTimeDisableScreenSave //int Время когда будет происходить разблокировка 
    {
        get 
        {

            #region Var-1 Read Int

            //Microsoft.Win32.RegistryKey key;
            //key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SergeiAKirApp");

            //if (key != null)
            //{
            //    try
            //    {
            //        int TDSS = (int)key.GetValue("TimeDisableScreenSave");
            //        if (TDSS != 0)
            //        {
            //            return TDSS;
            //        }
            //        else
            //        {
            //            return ScreenSaver.GetScreenSaverTimeout() / 60;
            //        }
            //    }
            //    catch (Exception)
            //    {

            //        return ScreenSaver.GetScreenSaverTimeout() / 60;
            //    }



            //}
            //else
            //{
            //    return ScreenSaver.GetScreenSaverTimeout() / 60;
            //}
            #endregion

            return (int)WorkInReestr.intToAPP("TimeDisableScreenSave");

        }
        set 
        {
            if (value > 0) 
            {
                #region Var-1 Write int 
                ////Запись в реестр значения value
                //Microsoft.Win32.RegistryKey key;
                //key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SergeiAKirApp");
                //key.SetValue("TimeDisableScreenSave", value);
                //key.Close();
                #endregion

                #region Var-2 Write int 
                WorkInReestr.strAPPTo("TimeDisableScreenSave", value.ToString());
                #endregion
            }
            else
            {
                //Запись в реестр значения value
                #region Var-1 Write default
                //Microsoft.Win32.RegistryKey key;
                //key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SergeiAKirApp");
                //key.SetValue("TimeDisableScreenSave", ScreenSaver.GetScreenSaverTimeout() / 60);
                //key.Close();
                #endregion

                #region Var-2 Write default
                WorkInReestr.strAPPTo("TimeDisableScreenSave", (ScreenSaver.GetScreenSaverTimeout() / 60).ToString());
                #endregion

            }
        }
    }

    


}

namespace SeachActiveApp
{
   
    class Program
    {
        //public DateTime dtStart;        //Время запуска программы
        //public DateTime dtStop;         //Время остановки программы. Если оно есть значит программу не перезагружали экстренно
        //public Boolean blSession;  //1 в начале цикла программы, а 0 вконце цикла программы - если при запуске программы это значение равно 1 то значит цикл не закончил свою работу
        


        static void Main(string[] args)
        {


            //string strActivApp;
            //string strActivAppOld=null;
            //DateTime dtAppOld;
            //DateTime dtActiveApp;

            

            
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

                #region Запуск WWW сервера

                    //Ver 1
                    server www = new server();
                    Thread serverWWW = new Thread(www.start);
                    serverWWW.Start();

                    //Ver 2
                    //Thread wwwServer = new Thread(new ThreadStart(server.start));
                    //wwwServer.Start();

                #endregion

                while (true)
                {

                    #region Сбор данных о активном приложении

                    ////Ver 1
                    ////Внутри потока опрос с промежутком в 1 мин
                    //strActivApp = clWinAPI.GetCaptionOfActiveWindow();
                    //dtActiveApp = DateTime.Now;

                    //new clRW(dtActiveApp, strActivApp, 1);
                    ////new server(80);
                    ///

                    //Ver2 - C помощью фонового потока
                    Thread AppActive = new Thread(clRW.AciveApp);
                    AppActive.IsBackground = true;
                    AppActive.Start();

                    #endregion

                    #region Проверка - если экранная заставка включена то выключаем её (Внутренний цикл 1мин * 10 раз)

                    Thread NotSleep = new Thread(new ThreadStart(ScreenSaver.CheckScreenSave));
                    NotSleep.IsBackground = true;

                    ////версия 1  -  прерываем поток для отключения заставки
                    //if (Globals.blDisableScreenSave)
                    //{
                    //    System.Diagnostics.Debug.WriteLine("--Start--"+DateTime.Now);
                    //    NotSleep.Start();
                    //}
                    //else
                    //{
                    //    System.Diagnostics.Debug.WriteLine("--Abort--"+DateTime.Now);
                    //    NotSleep.Abort();
                    //    //NotSleep.Join(TimeSpan.FromSeconds(10));
                    //}

                    //версия 2 - Внутри потока при каждом запуске проверяем значение глобальной переменной и если включена то отключаем экранную заставку или включаем её 
                    NotSleep.Start();

                    #endregion

                    


                    Thread.Sleep(TimeSpan.FromHours(1)); //спим 1 час
                    


                }

            }

            
        }

        static bool isStillRunning() //Проверка есть ли приложение в памяти
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
