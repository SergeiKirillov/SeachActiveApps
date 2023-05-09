using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibenNetFramework;

namespace MyLibenNetFramework
{
    public class MyScreenShot
    {


        #region Версия 1 - MakeScreenshot(Создаем скриншот рабочего стола) - на скриншоте черный экран
        //https://myrusakov.ru/csharp-create-screenshot.html
        public static void MakeScreenshot()
        {

            //MyIOFile.WriteFileTXT("сТАРТ", "errScreenShot");
            try
            {

                if (!ScreenSaver.GetScreenSaverRunning())
                {
                    //MyIOFile.WriteFileTXT("BEGIN TRY", "errScreenShot");

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
                        string dtNow = DateTime.Now.ToString("-HHmmss-fff -ddMM");
                        //bitmap.Save("d:\\screenshot"+dtNow+".jpg", ImageFormat.Jpeg);
                        bitmap.Save("d:\\screenshot.jpg", ImageFormat.Jpeg);
                    }
                }
                
            }
            catch (Exception ex)
            {
               MyIOFile.WriteFileTXT("Error MakeScreenshot = " + ex.Message, "errScreenShot");
                
            }
        }
        #endregion


        #region Версия 1.1 -Error MakeScreenshot = В GDI+ возникла ошибка общего вида- MakeScreenshot2(Создаем скриншот MULTI рабочего стола) - Черный экран -
        //https://myrusakov.ru/csharp-create-screenshot.html
        public static void MakeScreenshot2()
        {
            try
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
            catch (Exception ex)
            {
                // Error MakeScreenshot = В GDI + возникла ошибка общего вида.
                MyIOFile.WriteFileTXT("Error MakeScreenshot = " + ex.Message, "errScreenShot");
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
                    MyIOFile.WriteFileTXT("Рабочий стол:" + hDC.ToString(), "SceenShot");

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
                        MyIOFile.WriteFileTXT("Память:" + hOld.ToString(), "SceenShot"); //вывод в текстовы файл
                                                                                         //Мы копируем Битовый массив к контексту устройства памяти.
                        bool rrr = PlatformInvokeGDI32.BitBlt(hMemDC, 0, 0, size.cx, size.cy, hDC,
                                                   0, 0, PlatformInvokeGDI32.SRCCOPY);
                        MyIOFile.WriteFileTXT(DateTime.Now, "Копирование изображение:" + rrr, "SceenShot"); //вывод в текстовы файл

                        //Мы выбираем старый битовый массив назад к контексту устройства памяти.
                        IntPtr SO = PlatformInvokeGDI32.SelectObject(hMemDC, hOld);
                        MyIOFile.WriteFileTXT("Возвращаемое значение после выбирания:" + SO, "SceenShot");

                        //Мы удаляем контекст устройства памяти.
                        PlatformInvokeGDI32.DeleteDC(hMemDC);
                        //Мы выпускаем контекст устройства экрана.
                        IntPtr RIU = PlatformInvokeUSER32.ReleaseDC(PlatformInvokeUSER32.GetDesktopWindow(), hDC);
                        //Изображение создано и сохранено в локальную переменную
                        MyIOFile.WriteFileTXT("ReleaseDC(1-освобожден):" + RIU, "SceenShot");

                        Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
                        MyIOFile.WriteFileTXT(bmp.RawFormat.ToString(), "SceenShot"); //вывод в текстовы файл

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
                catch (Exception ex)
                {
                    MyIOFile.WriteFileTXT("Error GetDesktopImage = " + ex.Message, "errScreenShot");
                    return null;
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
}
