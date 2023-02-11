using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.Versioning;
using System.Runtime.Remoting.Lifetime;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Data.SQLite;

internal class clMyLiben
{
    //класс MyIO для вывода в файл

    //класс MyScreenShot для создания снимка экрана

    //ScreenInformation для получения информации о мониторах

    //работа с рееестром

}
/// <summary>
/// MyIO - класс для работы с файловой системой
/// </summary>
class MyIO
{
    /// <summary>
    /// Конструктор переменной класса для ввода пути. Вытвскивает значение из ветки реестра и при ее отсутствии создает 
    /// </summary>

    public static string myPath
    {
        ///<summary>
        ///Если ключа в реестре нет то используем значение пути по умолчанию,
        ///ели есть то передаемего
        /// </summary>
        /// 

        get 
        {
            return WorkInReestr.strToAPP("MyPath", System.AppDomain.CurrentDomain.BaseDirectory.ToString()); 
        }
        set 
        {
            WorkInReestr.strAPPTo("MyPath",value); 
        }
    }


    /// <summary>
    /// Вывод информации в файл на диске D
    /// </summary>
    /// <param name="dtMessage">Дата и время вывода сообщения.</param>
    /// <param name="Message">Тесктовое сообщение</param>
    /// <param name="NameFile">Имя файла куда будет записываться сообщение</param>
    /// <returns>Нет</returns>
    #region Вывод в файл

    //MyIO.WriteFileTXT(DateTime.Now, " X:" + Screen.PrimaryScreen.Bounds.X.ToString() + " Y:" + Screen.PrimaryScreen.Bounds.Y.ToString() + " Size:" + Screen.PrimaryScreen.Bounds.Size.ToString(), "SceenShot"); //вывод в текстовы файл
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
    #endregion


    ///<summary>
    ///Получение пути к Базе Данных
    /// </summary>
    /// <param name="FileName">Имя файла БД</param>
    /// 
    #region PathAPP - проверка существования файла по заданному пути

   
    public static bool PathAPP(string FileName)
    {
        //if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory+FileName))
        string pathBD = MyIO.myPath + "\\" +FileName;
        if (File.Exists(pathBD))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
    

}

class MyDBsqlite
{
    ///<summary>
    ///Класс для работы с библиотекой БД 
    /// </summary>
    /// 

    ///<summary>
    ///Функция для создания файла БД по заданному пути и имени файла
    ///</summary>
    ///<param name="strPath">Путь к БД</param>
    ///<param name="strNameFile">Имя БД</param>
    ///
    public static bool CreateDB(string strPath, string strNameFile)
    {
        try
        {
            string pathDB = strPath + "\\" + strNameFile;
            SQLiteConnection.CreateFile(pathDB);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        
    }

    ///<summary>
    ///
    /// </summary>
    /// <param name="pathDB">Путь к БД</param>
    /// <param name="strNameTab">Имя таблицы</param>
    public static bool CreateTab(string pathDB, string strNameTab)
    {
        string sqlExpression = "CREATE TABLE IF NOT EXISTS " + strNameTab +
                "(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
                "dtEvent REAL," +
                "strEvent NVARCHAR(128))";


        using (var connection = new SQLiteConnection("Data Source=" + pathDB + "; Version=3;"))
        {
            connection.Open();

            SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();

            return true;
        }

    }

    //
}

class MyScreenShot
{


    #region Версия 1 - MakeScreenshot(Создаем скриншот рабочего стола) - на скриншоте черный экран
    //https://myrusakov.ru/csharp-create-screenshot.html
    public static void MakeScreenshot()
    {
        // получаем размеры окна рабочего стола
        Rectangle bounds = Screen.GetBounds(System.Drawing.Point.Empty);

        // создаем пустое изображения размером с экран устройства
        //using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
        using (var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb))
        {
            // создаем объект на котором можно рисовать
            using (var g = Graphics.FromImage(bitmap))
            {
                // перерисовываем экран на наш графический объект
                //g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            }

            // сохраняем в файл с форматом JPG
            bitmap.Save("d:\\screenshot.jpg", ImageFormat.Jpeg);
        }
    }
    #endregion


    #region Версия 1.1 - MakeScreenshot2(Создаем скриншот MULTI рабочего стола) - Черный экран
    //https://myrusakov.ru/csharp-create-screenshot.html
    public static void MakeScreenshot2()
    {
        // получаем размеры окна рабочего стола
        Rectangle bounds = Screen.GetBounds(System.Drawing.Point.Empty);

        // создаем пустое изображения размером с экран устройства
        //using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
        using (var bitmap = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height, PixelFormat.Format32bppArgb))
        {

            // создаем объект на котором можно рисовать
            using (var g = Graphics.FromImage(bitmap))
            {
                // перерисовываем экран на наш графический объект
                //g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                g.CopyFromScreen(SystemInformation.VirtualScreen.X, SystemInformation.VirtualScreen.Y, 0, 0, SystemInformation.VirtualScreen.Size, CopyPixelOperation.SourceCopy);
            }

            // сохраняем в файл с форматом JPG
            bitmap.Save("d:\\screenshot.jpg", ImageFormat.Jpeg);
        }
    }


    #endregion

    #region Версия 2 - CaptureScreen (Скриншот рабочего стола) - на скриншоте черный экран
    //https://www.nookery.ru/how-to-take-a-screenshot-programmatically/
    #region Примененее
    //Image f = MyScreenShot.CaptureScreen.GetDesktopImage();
    //f.Save("d:\\screenshot.jpg");
    #endregion
    /// <summary>
    /// черный экран в заставке, в режиме приложения всё нормально
    
    /// </summary>


    public class PlatformInvokeGDI32
    {
        public const int SRCCOPY = 13369376;

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest,
            int yDest, int wDest, int hDest, IntPtr hdcSource,
            int xSrc, int ySrc, int RasterOp);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc,
            int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

    }

    public class PlatformInvokeUSER32
    {

        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;



        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int abc);

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(Int32 ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);


    }

    //Эта структура должна использоваться, чтобы держать размер экрана.
    public struct SIZE
    {
        public int cx;
        public int cy;
    }
    public class CaptureScreen
    {

        protected static IntPtr m_HBitmap;

        public static Bitmap GetDesktopImage()
        {
            //Jgbc
            try
            {
                //В размер переменной мы должны сохранить размер экрана.
                SIZE size;

                //В размер переменной мы должны сохранить размер экрана.
                IntPtr hBitmap;

                //Здесь мы получаем дескриптор контекста устройства рабочего стола.
                IntPtr hDC = PlatformInvokeUSER32.GetDC
                              (PlatformInvokeUSER32.GetDesktopWindow());
                MyIO.WriteFileTXT(DateTime.Now, "Рабочий стол:" + hDC.ToString(), "SceenShot");

                //Здесь мы делаем контекст устройства

                IntPtr hMemDC = PlatformInvokeGDI32.CreateCompatibleDC(hDC);

                //Мы передаем SM_CXSCREEN константа GetSystemMetrics               // и получить X координаты экрана.
                size.cx = PlatformInvokeUSER32.GetSystemMetrics
                          (PlatformInvokeUSER32.SM_CXSCREEN);

                //Мы передаем SM_CYSCREEN константа GetSystemMetrics и получить Y координаты экрана.
                size.cy = PlatformInvokeUSER32.GetSystemMetrics
                          (PlatformInvokeUSER32.SM_CYSCREEN);

                

                //Мы создаем совместимое растровое изображение экрана с размером с помощью
                //контекст устройства экрана.
                hBitmap = PlatformInvokeGDI32.CreateCompatibleBitmap
                            (hDC, size.cx, size.cy);
               
                //Как hBitmap IntPtr, мы не можем проверить его значение null.
                //Для этой цели используется значение IntPtr.Zero.
                if (hBitmap != IntPtr.Zero)
                {
                    //Здесь мы выбираем совместимое растровое изображение в памяти устройства
                    //контекст и держит ссылку на старый битовый массив.
                    IntPtr hOld = (IntPtr)PlatformInvokeGDI32.SelectObject
                                           (hMemDC, hBitmap);
                    MyIO.WriteFileTXT(DateTime.Now, "Память:" + hOld.ToString(), "SceenShot"); //вывод в текстовы файл
                    //Мы копируем Битовый массив к контексту устройства памяти.
                    bool rrr = PlatformInvokeGDI32.BitBlt(hMemDC, 0, 0, size.cx, size.cy, hDC,
                                               0, 0, PlatformInvokeGDI32.SRCCOPY);
                    MyIO.WriteFileTXT(DateTime.Now, "Копирование изображение:" + rrr, "SceenShot"); //вывод в текстовы файл

                    //Мы выбираем старый битовый массив назад к контексту устройства памяти.
                    IntPtr SO = PlatformInvokeGDI32.SelectObject(hMemDC, hOld);
                    MyIO.WriteFileTXT(DateTime.Now, "Возвращаемое значение после выбирания:" + SO, "SceenShot");

                    //Мы удаляем контекст устройства памяти.
                    PlatformInvokeGDI32.DeleteDC(hMemDC);
                    //Мы выпускаем контекст устройства экрана.
                    IntPtr RIU = PlatformInvokeUSER32.ReleaseDC(PlatformInvokeUSER32.GetDesktopWindow(), hDC);
                    //Изображение создано и сохранено в локальную переменную
                    MyIO.WriteFileTXT(DateTime.Now, "ReleaseDC(1-освобожден):" + RIU, "SceenShot");

                    Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
                    MyIO.WriteFileTXT(DateTime.Now, bmp.RawFormat.ToString(), "SceenShot"); //вывод в текстовы файл

                    //Освободим память, чтобы избежать утечек памяти.
                    PlatformInvokeGDI32.DeleteObject(hBitmap);
                    //Вызовим сборщик мусора.
                    GC.Collect();
                    //Вернем изображение
                    return bmp;
                }
                //Если hBitmap пустой, возвратите пустой указатель.
                return null;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }


    #endregion

    #region Версия 3 - DrawToBitmap 





    #endregion

    #region Версия 4 - OleDraw () 

    #endregion


    #region Версия 5 - Capture a Screen Shot (https://www.developerfusion.com/code/4630/capture-a-screen-shot/) 
    
    #region Скриншот 2 - применение
    ///MyScreenShot.ScreenCapture sc = new MyScreenShot.ScreenCapture();
    ///Image img = sc.CaptureScreen();
    ///this.BackgroundImage = img;        
    //////sc.CaptureWindowToFile(this.Handle, "d:\\temp2.gif", ImageFormat.Gif);
    #endregion
    
    public class ScreenCapture
    {
        /// <summary>
        /// Creates an Image object containing a screen shot of the entire desktop
        /// </summary>
        /// <returns></returns>
        public Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }
        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
        /// <returns></returns>
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }
        /// <summary>
        /// Captures a screen shot of a specific window, and saves it to a file
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }
        /// <summary>
        /// Captures a screen shot of the entire desktop, and saves it to a file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }

        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
    }

    #endregion

    #region Версия 5.1 (https://stackoverflow.com/questions/1163761/capture-screenshot-of-active-window) - не работает - черный экран и закрывается заставка. В режим приложения нормально 

    #region Скриншот 3 - Применение
    //MyScreenShot.ScreenCapturer2 sc = new MyScreenShot.ScreenCapturer2();
    //Image img = sc.Capture();
    //this.BackgroundImage = img;
    #endregion

    public enum enmScreenCaptureMode
    {
        Screen,
        Window
    }
    public class ScreenCapturer2
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public Bitmap Capture(enmScreenCaptureMode screenCaptureMode = enmScreenCaptureMode.Window)
        {
            Rectangle bounds;

            if (screenCaptureMode == enmScreenCaptureMode.Screen)
            {
                bounds = Screen.GetBounds(System.Drawing.Point.Empty);
                CursorPosition = Cursor.Position;
            }
            else
            {
                var foregroundWindowsHandle = GetForegroundWindow();
                var rect = new Rect();
                GetWindowRect(foregroundWindowsHandle, ref rect);
                bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
                CursorPosition = new System.Drawing.Point(Cursor.Position.X - rect.Left, Cursor.Position.Y - rect.Top);
            }

            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var g = Graphics.FromImage(result))
            {
                g.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
            }

            return result;
        }

        public System.Drawing.Point CursorPosition
        {
            get;
            protected set;
        }
    }

    #endregion

    #region Версия 5.2 (https://stackoverflow.com/questions/1163761/capture-screenshot-of-active-window) - на заставке CaptureDesktop - черный квадрат, а CaptureActiveWindow - скорее всего вылетает с ошибкой

    #region Применение 
    //Image img = MyScreenShot.ScreenCapture3.CaptureDesktop();
    //img.Save(@"D:\CaptureDesktop.jpg", ImageFormat.Jpeg);

    //Image img2 = MyScreenShot.ScreenCapture3.CaptureActiveWindow();
    //img.Save(@"D:\CaptureActiveWindow.jpg", ImageFormat.Jpeg);
    #endregion

    public class ScreenCapture3
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static Image CaptureDesktop()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        public static Bitmap CaptureActiveWindow()
        {
            return CaptureWindow(GetForegroundWindow());
        }

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
            }

            return result;
        }
    }

    #endregion
}


public class ScreenInformation
{
    #region Применение
    //public partial class App : Application
    //{
    //    protected override void OnStartup(StartupEventArgs e)
    //    {
    //        base.OnStartup(e);
    //        LinkedList<ScreenInformation.WpfScreen> screens = ScreenInformation.GetAllScreens();
    //        foreach (var screen in screens)
    //        {
    //            var window = new MainWindow();

    //            Console.WriteLine("Metrics {0} {1}", screen.metrics.top, screen.metrics.left);

    //            window.Top = screen.metrics.top;
    //            window.Left = screen.metrics.left;
    //            window.Show();
    //        }
    //    }
    //}
    #endregion
        
    [StructLayout(LayoutKind.Sequential)]
    public struct ScreenRect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
    [DllImport("user32.dll")]
    private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lpRect, MonitorEnumProc callback, int dwData);

    private delegate bool MonitorEnumProc(IntPtr hDesktop, IntPtr hdc, ref ScreenRect pRect, int dwData);

    public class wpfScreen
    {
        public ScreenRect metrics;
        public wpfScreen(ScreenRect prect)
        {
            metrics = prect;
        }

        static LinkedList<wpfScreen> AllScreens = new LinkedList<wpfScreen>();

        public static LinkedList<wpfScreen> GetAllScreens() 
        {
            ScreenInformation.wpfScreen.GetMonitorCount();
            return AllScreens;
        }

        public static int GetMonitorCount()
        {
            AllScreens.Clear();
            int monCount = 0;
            MonitorEnumProc callback = (IntPtr hDesktop, IntPtr hdc, ref ScreenRect prect, int d) =>
            {
                Console.WriteLine("Left {0}", prect.Left);
                Console.WriteLine("Right {0}", prect.Right);
                Console.WriteLine("Top {0}", prect.Top);
                Console.WriteLine("Bottom {0}", prect.Bottom);
                AllScreens.AddLast(new wpfScreen(prect));
                return ++monCount > 0;
            };

            if (EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, 0))
                Console.WriteLine("You have {0} monitors", monCount);
            else
                Console.WriteLine("An error occured while enumerating monitors");

            return monCount;
        }
    }

}


public class WorkInReestr
{
    /// <summary>
    /// Класс для работы с реестром
    /// 
    /// </summary>
    /// 

    private static string NameApp = "SergeiAKirApp";
    public static string strToAPP(string NameKey,string strText = "Screen Saver \nдля программы SeachActiveApp")
    {

        using (RegistryKey strTextSS = Registry.CurrentUser.OpenSubKey(NameApp, true))
        {
            if (strTextSS != null)
            {

                string DSS = strTextSS.GetValue(NameKey) as string;
                if (DSS!=null)
                {
                    return Regex.Replace(DSS, @"\\n", "\n").Replace("\n", Environment.NewLine);
                }
                else
                {
                    //string strText = "Screen Saver \nдля программы SeachActiveApp";
                    //string strText = "1";
                    strAPPTo(NameKey, strText);
                    return strText;
                }
               

            }
            else
            {
                //Запись в реестр
                //string strText = "Screen Saver \nдля программы SeachActiveApp"; //Параметр передан по умолчанию
                //string strText = "1";
                strAPPTo(NameKey, strText);
                return strText;

            }

        }

    }

   

    public static bool blToAPP(string NameKey)
    {

        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(NameApp, true))
        {
            if (key != null)
            {
                if (Convert.ToBoolean(key.GetValue(NameKey)))
                {
                    return Convert.ToBoolean(key.GetValue(NameKey) as string);
                }
                else
                {
                    string strText = "False";
                    strAPPTo(NameKey, strText);
                    return false;
                }
                
            }
            else
            {
                
                string strText = "False";
                strAPPTo(NameKey, strText);
                return false;
            }


        }
        


    }

    public static void strAPPTo(string NameKey, string ValueKey)
    {
        if (((ValueKey.Length*2)< 1048576)&&(ValueKey!=null))
        {
            //Запись в реестр значения value
            //Microsoft.Win32.RegistryKey key;
            //key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(NameApp);
            //key.SetValue(NameKey, ValueKey);
            //key.Close();

            using (RegistryKey strTextSS = Registry.CurrentUser.CreateSubKey(NameApp))
            {
                
                strTextSS.SetValue(NameKey, ValueKey);

            }
        }
        
    }

    public static int intToAPP(string NameKey)
    {
        int intText = 1;
        using (RegistryKey intTSS = Registry.CurrentUser.OpenSubKey(NameApp, true))
        {
            if (intTSS != null)
            {


                if (intTSS.GetValue(NameKey) != null)
                {
                    intText = Convert.ToInt32(intTSS.GetValue(NameKey,1));
                    return intText;
                }
                else
                {
                    intText = 1;
                    intAPPTo(NameKey, 1);
                    return intText;
                }

            }
            else
            {

                intText = 1;
                intAPPTo(NameKey, 1);
                return intText;

            }

        }

    }

    public static void intAPPTo(string NameKey, int ValueKey)
    {
        using (RegistryKey strTextSS = Registry.CurrentUser.CreateSubKey(NameApp))
        {

            strTextSS.SetValue(NameKey, ValueKey, RegistryValueKind.DWord);

        }
        

    }

}

public class MyNetFramework
{
    #region Version 1 - Получение списка версий Frameworkа из реестра
    public static void WhichVersion()
    {
        //Определение установленных версий .NET Framework программным путём
        //http://net-framework.ru/article/programmno-opredelit-kakie-versii-ustanovleny

        string strVersionNet = null;
        using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
        {
            foreach (string versionName in ndpKey.GetSubKeyNames())
            {
                if (versionName.StartsWith("v"))
                {
                    RegistryKey versionKey = ndpKey.OpenSubKey(versionName);
                    string name = (string)versionKey.GetValue("version", "");
                    string sp = versionKey.GetValue("SP", "").ToString();
                    string install = versionKey.GetValue("Install", "").ToString();

                    if (install == "")
                    {
                        strVersionNet = strVersionNet + versionName + " " + name;
                        MyIO.WriteFileTXT(DateTime.Now, "VersionName -" + versionName + " -- name-" + name, "NFw");
                    }
                    else
                    {
                        if (sp != "" && install == "1")
                        {
                            strVersionNet = strVersionNet + versionName + " " + name + " SP " + sp;
                            MyIO.WriteFileTXT(DateTime.Now, "VersionName-" + versionName + " -- name-" + name + " -- SP-" + sp, "NFw");
                        }
                    }

                    if (name != "")
                    {
                        continue;
                    }

                    foreach (string SubKeyName in versionKey.GetSubKeyNames())
                    {
                        RegistryKey subKey = versionKey.OpenSubKey(SubKeyName);

                        name = (string)subKey.GetValue("Version", "");

                        if (name != "") sp = subKey.GetValue("SP", "").ToString();
                        install = subKey.GetValue("Install", "").ToString();

                        if (install == "")
                        {
                            strVersionNet = strVersionNet + versionName + " " + name;
                            MyIO.WriteFileTXT(DateTime.Now, "VersionName -" + versionName + " -- name-" + name, "NFw");
                        }
                        else
                        {
                            if (sp != "" && install == "1")
                            {
                                strVersionNet = strVersionNet + " " + SubKeyName + " " + name + " SP" + sp;
                                MyIO.WriteFileTXT(DateTime.Now, "VersionName-" + versionName + " -- name-" + name + " -- SP-" + sp, "NFw");
                            }
                            else if (install == "1")
                            {
                                strVersionNet = strVersionNet + " " + SubKeyName + " " + name;
                                MyIO.WriteFileTXT(DateTime.Now, "VersionName -" + versionName + " -- name-" + name, "NFw");
                            }
                        }

                    }
                }
            }
        }
    }
    #endregion

    #region Version2 - Получение версии начиная с 4.. - Доработать
    public static void Get45PlusFromRegistry()
    {
        const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
        using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
        {
            if (ndpKey != null && ndpKey.GetValue("Release") != null)
            {
                Console.WriteLine(".NET Framework Version: " + CheckFor45PlusVersion((int)ndpKey.GetValue("Release")));
            }
            else
            {
                Console.WriteLine(".NET Framework Version 4.5 or later is not detected.");
            }
        }
    }

    // Checking the version using >= will enable forward compatibility.
    private static string CheckFor45PlusVersion(int releaseKey)
    {
        if (releaseKey >= 394802)
            return "4.6.2 or later";
        if (releaseKey >= 394254)
        {
            return "4.6.1";
        }
        if (releaseKey >= 393295)
        {
            return "4.6";
        }
        if ((releaseKey >= 379893))
        {
            return "4.5.2";
        }
        if ((releaseKey >= 378675))
        {
            return "4.5.1";
        }
        if ((releaseKey >= 378389))
        {
            return "4.5";
        }
        // This code should never execute. 
        // that 4.5 or later is installed.
        return "No 4.5 or later version detected";
    }
    #endregion



    //public static Version EnsureSupportedDotNetFrameworkVersion(Version supportedVersion)
    //{
    //    var fileVersion = typeof(int).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
    //    var currentVersion = new Version(fileVersion.Version);
    //    if (currentVersion < supportedVersion)
    //        throw new NotSupportedException($"Microsoft .NET Framework {supportedVersion} or newer is required. Current version ({currentVersion}) is not supported.");
    //    return currentVersion;
    //}

    #region Определяем на какую версию заточено приложение

    public static bool blOKFrameworkVersionApp() 
    {
        //https://question-it.com/questions/769879/kak-uznat-imja-i-versiju-tselevogo-frejmvorka-iz-exe

        //var attributes = assembly.CustomAttributes; //получаем имя файла из параметров функции //public static string ShowFrameworkVersionApp(Assembly assembly) 
        bool blNetFrameWork = false;
        var fxAssembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
        var attributes = fxAssembly.CustomAttributes; //тередаем текущее имя файла
        string sfv="";
        foreach (var attribute in attributes)
        {
            if (attribute.AttributeType == typeof(TargetFrameworkAttribute))
            {
                var arg = attribute.ConstructorArguments.FirstOrDefault();
                if (arg == null)
                {
                    sfv= "";
                    blNetFrameWork = false;
                    throw new NullReferenceException("Unable to read framework version");
                    
                    
                }
                else
                {
                    //sfv= arg.Value.ToString();
                    #region Получаем массив чисел выражающий версию приложения
                    
                    string sfv0 = arg.Value.ToString();
                    int sfv1 = sfv0.IndexOf("=v") + 2;
                    string sfv2 = sfv0.Substring(sfv1, sfv0.Length - sfv1);

                    int[] array = sfv2.Select(x => Convert.ToInt32(char.GetNumericValue(x))).ToArray(); //Преобразукм строку в массив сисел
                    array = array.Where(x => x != -1).ToArray();//Удаляем из массива (-1)
                    #endregion

                    #region Преобраем числовой массив версии Framework в число для поиска в рестре
                    int intMinVerApp = 0;
                    if (array[0] == 4)
                    {
                        switch (array[1])
                        {
                            case 5:
                                switch (array[2])
                                {
                                    case 1:
                                        intMinVerApp = 378675;
                                        break;
                                    case 2:
                                        intMinVerApp = 379893;
                                        break;
                                    default:
                                        intMinVerApp = 378389;
                                        break;
                                }
                                break;
                            case 6:
                                switch (array[2])
                                {
                                    case 1:
                                        intMinVerApp = 394254;
                                        break;
                                    case 2:
                                        intMinVerApp = 394802;
                                        break;
                                    default:
                                        intMinVerApp = 393295;
                                        break;
                                }
                                break;
                            case 7:
                                switch (array[2])
                                {
                                    case 1:
                                        intMinVerApp = 461308;
                                        break;
                                    case 2:
                                        intMinVerApp = 461808;
                                        break;
                                    default:
                                        intMinVerApp = 460798;
                                        break;
                                }
                                break;
                            case 8:
                                switch (array[2])
                                {
                                    case 1:
                                        intMinVerApp = 533320;
                                        break;
                                    default:
                                        intMinVerApp = 528040;
                                        break;
                                }
                                break;

                            default:
                                intMinVerApp = 0;
                                break;
                        }
                    }
                    else if (array[1] < 4)
                    {
                        blNetFrameWork = false;
                    }
                    #endregion

                    #region по заданной ветке реестра находим нинимальная версия в системе она должна быть больше чем версия АРР
                    const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
                    int intRegVerNet;
                    using (RegistryKey intTSS = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
                    {
                        if (intTSS != null)
                        {
                            if (intTSS.GetValue("Release") != null)
                            {
                                intRegVerNet = Convert.ToInt32(intTSS.GetValue("Release", 0));
                            }
                            else
                            {
                                intRegVerNet = 0;
                            }
                        }
                        else
                        {
                            intRegVerNet = 0;
                        }
                    }

                    #endregion

                    Console.WriteLine(intRegVerNet.ToString());
                    Console.WriteLine(intMinVerApp.ToString());

                    MyIO.WriteFileTXT(DateTime.Now, "System-" + intRegVerNet+ " -- App-" + intMinVerApp + "("+ sfv0 + ")", "NFw");

                    if (intRegVerNet > intMinVerApp) blNetFrameWork =  true; 
                    else blNetFrameWork = false;


                    //if (sfv2>"4.5")
                    //{
                    //    
                    //}
                    //else
                    //{
                    //    if (sfv2<"4.5")
                    //    {

                    //    }     
                    //}

                    sfv = sfv2; 
                }
                

            }

            

        }
        WhichVersion();
        return blNetFrameWork;
    }

    #endregion
}

