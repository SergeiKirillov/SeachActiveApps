using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public static class ScreenSaver
{
    //https://translated.turbopages.org/proxy_u/en-ru.ru.68c3861d-63c01374-5f5e3d5f-74722d776562/https/www.codeproject.com/Articles/17067/Controlling-The-Screen-Saver-With-C


    // Signatures for unmanaged calls
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool SystemParametersInfo(int uAction, int uParam, ref int lpvParam, int flags );

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool SystemParametersInfo(int uAction, int uParam, ref bool lpvParam, int flags );

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int PostMessage( IntPtr hWnd, int wMsg, int wParam, int lParam );

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr OpenDesktop(string hDesktop, int Flags, bool Inherit, uint DesiredAccess );

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool CloseDesktop( IntPtr hDesktop );

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool EnumDesktopWindows( IntPtr hDesktop, EnumDesktopWindowsProc callback, IntPtr lParam );

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool IsWindowVisible( IntPtr hWnd );

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetForegroundWindow( );

     // Callbacks
       private delegate bool EnumDesktopWindowsProc(IntPtr hDesktop, IntPtr lParam);

     // Constants
        private const int SPI_GETSCREENSAVERACTIVE = 16;
        private const int SPI_SETSCREENSAVERACTIVE = 17;
        private const int SPI_GETSCREENSAVERTIMEOUT = 14;
        private const int SPI_SETSCREENSAVERTIMEOUT = 15;
        private const int SPI_GETSCREENSAVERRUNNING = 114;
        private const int SPIF_SENDWININICHANGE = 2;

        private const uint DESKTOP_WRITEOBJECTS = 0x0080;
        private const uint DESKTOP_READOBJECTS = 0x0001;
        private const int WM_CLOSE = 16;


        // Returns TRUE if the screen saver is active 
        // (enabled, but not necessarily running).
        public static bool GetScreenSaverActive()
        {
            bool isActive = false;

            SystemParametersInfo(SPI_GETSCREENSAVERACTIVE, 0,
            ref isActive, 0);
            return isActive;
        }

        // Pass in TRUE(1) to activate or FALSE(0) to deactivate
        // the screen saver.
        public static void SetScreenSaverActive(int Active)
        {
            int nullVar = 0;

            SystemParametersInfo(SPI_SETSCREENSAVERACTIVE,
            Active, ref nullVar, SPIF_SENDWININICHANGE);
        }

        // Returns the screen saver timeout setting, in seconds
        public static Int32 GetScreenSaverTimeout()
        {
            Int32 value = 0;

            SystemParametersInfo(SPI_GETSCREENSAVERTIMEOUT, 0,
            ref value, 0);
            return value;
        }

        // Pass in the number of seconds to set the screen saver
        // timeout value.
        public static void SetScreenSaverTimeout(Int32 Value)
        {
            int nullVar = 0;

            SystemParametersInfo(SPI_SETSCREENSAVERTIMEOUT,
            Value, ref nullVar, SPIF_SENDWININICHANGE);
        }

        // Returns TRUE if the screen saver is actually running
        public static bool GetScreenSaverRunning()
        {
            bool isRunning = false;

            SystemParametersInfo(SPI_GETSCREENSAVERRUNNING, 0,
            ref isRunning, 0);
            return isRunning;
        }

        // From Microsoft's Knowledge Base article #140723: 
        // http://support.microsoft.com/kb/140723
        // "How to force a screen saver to close once started 
        // in Windows NT, Windows 2000, and Windows Server 2003"

        public static void KillScreenSaver()
        {
            //отключение экранной заставки

            IntPtr hDesktop = OpenDesktop("Screen-saver", 0,
            false, DESKTOP_READOBJECTS | DESKTOP_WRITEOBJECTS);
            if (hDesktop != IntPtr.Zero)
            {
                EnumDesktopWindows(hDesktop, new
                EnumDesktopWindowsProc(KillScreenSaverFunc),
                IntPtr.Zero);
                CloseDesktop(hDesktop);
            }
            else
            {
                PostMessage(GetForegroundWindow(), WM_CLOSE,
                0, 0);
            }
        }

        private static bool KillScreenSaverFunc(IntPtr hWnd,
        IntPtr lParam)
        {
            if (IsWindowVisible(hWnd))
                PostMessage(hWnd, WM_CLOSE, 0, 0);
            return true;
        }

    private static int intTimeCountScreenSave;
    public static void CheckScreenSave()
    {
       while (true)
       {
            if (Globals.blDisableScreenSave)
            {
                //Отключение экранной заставки  если галочка "Отключение экранной заставки" поднята

                if (ScreenSaver.GetScreenSaverRunning())
                {
                    if (intTimeCountScreenSave!=Globals.intTimeDisableScreenSave)
                    {
                        intTimeCountScreenSave = intTimeCountScreenSave + 1;
                        System.Diagnostics.Debug.WriteLine("Кол-во циклов: "+ intTimeCountScreenSave);

                        #region Передаем значение внешней программе - скринсерверу
                        
                        char[] message = intTimeCountScreenSave.ToString().ToCharArray();
                        message = "Кол-во иставшихся минут:".ToCharArray();
                        int size = message.Length;
                        using (MemoryMappedViewAccessor writer = Globals.SharedMemory.CreateViewAccessor(0, size * 2 + 4 + 4))
                        {
                            writer.Write(0, size); //размер сообщения
                            writer.Write(4, Globals.intTimeDisableScreenSave - intTimeCountScreenSave); //число сколько осталось мин до выключения заставки 
                            writer.WriteArray<Char>(8, message, 0, message.Length); //Пример передачи сообшения
                            //foreach (char item in message)
                            //{
                            //    System.Diagnostics.Debug.WriteLine("Передача числа: " + item);
                            //}

                        }
                        #endregion



                    }
                    else
                    {
                        intTimeCountScreenSave = 0;
                        ScreenSaver.KillScreenSaver(); //не всегда срабатывает
                        ScreenSaver.SetScreenSaverActive(0); //не отключает
                        
                        System.Diagnostics.Debug.WriteLine(DateTime.Now);
                        System.Diagnostics.Debug.WriteLine("Кол-во циклов: " + intTimeCountScreenSave);

                    }
                    
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


    //private static void MoveCursor()
    //{
    //    // Set the Current cursor, move the cursor's Position,
    //    // and set its clipping rectangle to the form. 

    //    this.Cursor = new Cursor(Cursor.Current.Handle);
    //    Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);
    //    Cursor.Clip = new Rectangle(this.Location, this.Size);
    //}





}


