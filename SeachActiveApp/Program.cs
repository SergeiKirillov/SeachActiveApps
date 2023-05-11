using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibenNetFramework;

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

    public static bool blScreenShotDesktop //Создание скриншота
    {
        get
        {
            return WorkInReestr.blToAPP("SaveDesktopToJPG");
        }
        set
        {
            WorkInReestr.strAPPTo("SaveDesktopToJPG", value.ToString());
        }
    }

    public static bool blSaveDateToFile //запись названия активного приложения в файл
    {
        //читаем/записываем в реестр значение переменной 
        get
        {
            return WorkInReestr.blToAPP("SaveDateToFile");
        }
        set
        {
            WorkInReestr.strAPPTo("SaveDateToFile", value.ToString());
        }
    } 

    public static bool blSaveDateToBD //запись навзвания активного приложения в БД
    {
        get
        {
            return WorkInReestr.blToAPP("SaveDateToBD");
        }
        set
        {
            WorkInReestr.strAPPTo("SaveDateToBD", value.ToString());
        }
    } 

    public static bool blWindowsAutoStart // автозапуск приложения при старте windows
    {
        get {return WorkInReestr.GetAutostartWindows("SeachActiveApp");}
        set {WorkInReestr.SetAutostartWindows(value,Assembly.GetExecutingAssembly().Location,"SeachActiveApp");}
    } 

    public static bool blEnableActiveAppSaving //True - производим запись имени активного приложения или в БД или в текстовй файл
    {
        
        get
        {
            return WorkInReestr.blToAPP("EnableActiveAppSaving");
        }
        set
        {
            WorkInReestr.strAPPTo("EnableActiveAppSaving", value.ToString());
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



            //string dd1 = System.Environment.Version.ToString(); //версия в системе

            //string dd3 = MyNetFramework.ShowFrameworkVersionApp(); //версия приложения



            //MyIO.WriteFileTXT(DateTime.Now, dd1+" -- "+ dd3, "NFw");


            if (!MyNetFramework.blOKFrameworkVersionApp())
            {
                System.Windows.Forms.MessageBox.Show("Проблема с Net FrameWork");
                Application.Exit();
            }


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

                    ////Ver 1
                    //server www = new server();
                    //Thread serverWWW = new Thread(www.start);
                    //serverWWW.Start();

                    ////Ver 2
                    //Thread wwwServer = new Thread(new ThreadStart(server.start));
                    //wwwServer.Start();

                    //Ver 3
                    server www = new server();
                    Thread serverWWW = new Thread(www.start);
                    serverWWW.Start();

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
                    Thread AppActive = new Thread(clRWLiteDB.AciveApp);
                    AppActive.IsBackground = true;
                    AppActive.Start();

                    #endregion

                    #region Проверка - если экранная заставка включена то выключаем её (Внутренний цикл 1мин * 10 раз)
                    //23042023
                    //Thread NotSleep = new Thread(new ThreadStart(ScreenSaver.CheckScreenSave()));
                    Thread NotSleep = new Thread(CheckScreenSave);
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

                    ////версия 2 - Внутри потока при каждом запуске проверяем значение глобальной переменной и если включена то отключаем экранную заставку или включаем её 
                    NotSleep.Start();

                    #endregion

                    //23042023
                    #region Если включена настройка скиншот, то тогда каждую минуту делаем скриншот работчего стола.пока не включена экранная заставка
                   
                    Thread ScreenShot = new Thread(ToDoScreenShot);
                    ScreenShot.IsBackground = true;
                    ScreenShot.Start();

                    //MyScreenShot.MakeScreenshot2();
                    #endregion




                    //Thread.Sleep(TimeSpan.FromHours(1)); //спим 1 час
                    Thread.Sleep(TimeSpan.FromMinutes(10)); //спим 10 min
                    NotSleep.Abort();
                    ScreenShot.Abort();
                    AppActive.Abort();

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


        private static int intTimeCountScreenSave;
        /// <summary>
        /// blDisableScreenSave - bool отключение заставки
        /// intTimeDisableScreenSave - int Время когда будет происходить разблокировка
        /// Globals.SharedMemory - Создание участка разделяемой памяти
        /// </summary>
        private static void CheckScreenSave()
        {
            while (true)
            {
                if (Globals.blDisableScreenSave)
                {
                    //Отключение экранной заставки  если галочка "Отключение экранной заставки" поднята

                    if (ScreenSaver.GetScreenSaverRunning())
                    {
                        if (intTimeCountScreenSave < Globals.intTimeDisableScreenSave)
                        {
                            intTimeCountScreenSave = intTimeCountScreenSave + 1;
                            System.Diagnostics.Debug.WriteLine("Кол-во циклов: " + intTimeCountScreenSave);
                            int ObratniOtchet = Globals.intTimeDisableScreenSave - intTimeCountScreenSave;
                            //if (ObratniOtchet!=0)
                            //{
                            #region Передаем значение внешней программе - скринсерверу
                            try
                            {
                                char[] message = intTimeCountScreenSave.ToString().ToCharArray();
                                message = "Кол-во иставшихся минут:".ToCharArray();
                                int size = message.Length;

                                using (MemoryMappedViewAccessor writer = Globals.SharedMemory.CreateViewAccessor(0, size * 2 + 4 + 4))
                                {
                                    writer.Write(0, size); //размер сообщения
                                    writer.Write(4, ObratniOtchet); //число сколько осталось мин до выключения заставки 
                                    writer.WriteArray<Char>(8, message, 0, message.Length);
                                    //Пример передачи сообшения
                                    //foreach (char item in message)
                                    //{
                                    //    System.Diagnostics.Debug.WriteLine("Передача числа: " + item);
                                    //}

                                }
                            }
                            catch (Exception)
                            {


                            }

                            #endregion
                        




                        }
                        else
                        {

                            intTimeCountScreenSave = 0;


                            //вариант 3
                            //Посылаем бит в память. Экранная заставка его считывает и завершает работу
                            #region Передаем значение внешней программе - скринсерверу
                            //try
                            //{
                            //    char[] message = intTimeCountScreenSave.ToString().ToCharArray();
                            //    message = "Кол-во иставшихся минут:".ToCharArray();
                            //    int size = message.Length;

                            //    using (MemoryMappedViewAccessor writer = Globals.SharedMemory.CreateViewAccessor(0, size * 2 + 4 + 4))
                            //    {
                            //        writer.Write(0, size); //размер сообщения
                            //        writer.Write(4, 0); //число сколько осталось мин до выключения заставки 
                            //        writer.WriteArray<Char>(8, message, 0, message.Length);
                            //        //Пример передачи сообшения
                            //        //foreach (char item in message)
                            //        //{
                            //        //    System.Diagnostics.Debug.WriteLine("Передача числа: " + item);
                            //        //}

                            //    }
                            //}
                            //catch (Exception)
                            //{
                            //}

                            #endregion

                            ////Вер 2 - ищем приложенние SeachActiveAppScr3 и посылаем ему символы
                            //IntPtr hWndSS = FindWindow(null, "SeachActiveAppScr3.5");
                            //IntPtr hWndSS = FindWindow(null, "ScreenSaver");
                            //SetForegroundWindow(hWndSS);
                            //SendKeys.SendWait("hh");//Error отказано в доступе


                            //Вер 1 - убиваем экранную заставку
                            //ScreenSaver.KillScreenSaver(); //не всегда срабатывает, для SeachActiveApp3.5 вызывает зависание
                            //ScreenSaver.SetScreenSaverActive(0); //не отключает



                            //Вариант 4.1 - не работает
                            //Перемещение указателя мышки или нажатие на кнопку мышки 
                            //mouse_event(ABSOLUTE | MOVE, 32000, 32000, 0, IntPtr.Zero);
                            //mouse_event(ABSOLUTE| RIGHTDOWN,32000,32000,0, IntPtr.Zero);
                            //mouse_event(ABSOLUTE | RIGHTUP, 32000, 32000, 0, IntPtr.Zero);

                            //Вариант 4.2 - не работает
                            //Перемещение указателя мышки или нажатие на кнопку мышки
                            //SetCursorPos(1,1);

                            //Вариант 4.3 - не работает SendKeys не может выполняться в рамках этого приложения, так как приложение не обрабатывает сообщения Windows. 
                            //Ищем программу калькулятор и посылаем ей значение
                            //IntPtr cWindow = FindWindow(null, "Calculator");
                            //if (Convert.ToBoolean(SetForegroundWindow(calcWindow))) SendKeys.Send("10{+}10=");


                            //Вариант 4.4 - не работает.Нужно имя процесса заставки
                            //IntPtr cWindow = FindWindow(null, "SeachActiveAppSCR"); //0
                            //System.Diagnostics.Debug.WriteLine(cWindow); 
                            //if (Convert.ToBoolean(SetForegroundWindow(cWindow))) PostMessage(cWindow, WM_CLOSE, 0, 0); ;

                            System.Diagnostics.Debug.WriteLine(DateTime.Now);
                            System.Diagnostics.Debug.WriteLine("Кол-во циклов: " + intTimeCountScreenSave);

                        }

                    }
                    else
                    {
                        intTimeCountScreenSave = 0; //если заставка не включена то кол-во мин до выключения равно 0
                    }
                }
                else
                {
                    ////Включение экранной заставки если снять галочку "Отключение экранной заставки"

                    //if (!ScreenSaver.GetScreenSaverRunning()) //Если экранная заставка не запущена 
                    //{

                    //    if (ScreenSaver.GetScreenSaverActive()) //Если экранная завтавка Активирована, но не запущена
                    //    {
                    //        ScreenSaver.SetScreenSaverActive(1); //Активируем экранную заставку
                    //        //Console.WriteLine(DateTime.Now);
                    //        System.Diagnostics.Debug.WriteLine(DateTime.Now + "Экранная заставка запущена и активирована");
                    //    }
                    //    else
                    //    {
                    //        System.Diagnostics.Debug.WriteLine("Экранная заставка не запущена");
                    //    }



                    //}

                }



                Thread.Sleep(TimeSpan.FromMinutes(1));
            }


        }

        /// <summary>
        /// ToDoScreenShot() - функция которая с промежутком около 1 мин будет делать скриншот
        /// </summary>
        private static void ToDoScreenShot() 
        {
            while (true)
            {
                if (Globals.blScreenShotDesktop) //Если в настройках экрана стоит что делаем скриншот, то тогда запускаем скриншот каждую 1мин
                {
                    
                    //if (!ScreenSaver.GetScreenSaverRunning()) //Если Хранитель экрана не запущен то делаем скриншот
                    //{
                        MyScreenShot.MakeScreenshot(); //Делаем скринШот с помощью функции библиотеки 
                        //MyScreenShot.MakeScreenshot2(); //Делаем скринШот с помощью функции библиотеки 
                        Thread.Sleep(TimeSpan.FromMinutes(1));
                    //}
                }

            }


        }



    }
}
