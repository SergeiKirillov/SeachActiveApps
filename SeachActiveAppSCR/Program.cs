using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SeachActiveAppSCR
{
    static class Program
    {


        public static bool blTxtScreenSaver
        {
            get
            {
                bool blSS;
                using (RegistryKey blTextSS = Registry.CurrentUser.OpenSubKey("SergeiAKirApp", true))
                {
                    blSS = Convert.ToBoolean(blTextSS.GetValue("blText") as string);
                }
                return blSS;
            }
            set
            {
                //Запись в реестр значения value
                using (RegistryKey blTextSS = Registry.CurrentUser.OpenSubKey("SergeiAKirApp", true))
                {
                    blTextSS.SetValue("blText", value);
                }

            }
        }

        public static string strTxtScreenSaver
        {
            get
            {
                string strSS;
                using (RegistryKey strTextSS = Registry.CurrentUser.OpenSubKey("SergeiAKirApp", true))
                {
                    strSS = strTextSS.GetValue("Text") as string;
                }
                if (strSS == null)
                {
                    return "Screen Saver для программы SeachActiveApp";
                }
                else
                {
                    return strSS;
                }



            }
            set
            {
                //Запись в реестр значения value
                using (RegistryKey strTextSS = Registry.CurrentUser.OpenSubKey("SergeiAKirApp", true))
                {
                    strTextSS.SetValue("Text", value);
                }

            }
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmScreenSaver());

            if (args.Length > 0)
            {
                string firstArgument = args[0].ToLower().Trim();
                string secondArgument = null;

                if (firstArgument.Length > 2)
                {
                    secondArgument = firstArgument.Substring(3).Trim();
                    firstArgument = firstArgument.Substring(0, 2);
                }
                else if (args.Length > 1)
                {
                    secondArgument = args[1];
                }

                if (firstArgument == "/c")
                {
                    //TODO configuration mode
                    Application.Run(new frmSeachActiveAppScrSetting());
                }

                else if (firstArgument == "/p")
                {
                    //TODO Preview mode

                    if (secondArgument == null)
                    {
                        MessageBox.Show("Не был передан Handle windows окна", "ScreenSaver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    IntPtr PreviewWndHandle = new IntPtr(long.Parse(secondArgument));
                    Application.Run(new frmScreenSaver(PreviewWndHandle));
                }

                else if (firstArgument == "/s")
                {
                    ////ver1
                    //MakeScreenshot();

                    //ver1.1
                    //MakeScreenshot2();

                    ////Ver2
                    //Image f = CaptureScreen.GetDesktopImage();
                    //f.Save("d:\\screenshot.jpg");

                    ShowScreenSaver();
                    Application.Run();
                }

                else
                {
                    MessageBox.Show("Аргумент командной строки \"" + firstArgument + "\" не подходит под условия.", "ScreenSaver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                //MakeScreenshot2();

                //Image f = CaptureScreen.GetDesktopImage();
                //f.Save("d:\\screenshot.jpg");

                ShowScreenSaver();
                Application.Run();

                //Application.Run(new frmSeachActiveAppScrSetting());
            }
        }

        static void ShowScreenSaver()
        {
            foreach (Screen item in Screen.AllScreens)
            {
                frmScreenSaver Screen = new frmScreenSaver(item.Bounds);
                Screen.Show();
            }
        }

        #region Версия 1 - MakeScreenshot(Создаем скриншот рабочего стола) - на скриншоте черный экран
        //https://myrusakov.ru/csharp-create-screenshot.html
        public static void MakeScreenshot()
        {
            // получаем размеры окна рабочего стола
            Rectangle bounds = Screen.GetBounds(Point.Empty);

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
            Rectangle bounds = Screen.GetBounds(Point.Empty);

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
                //В размер переменной мы должны сохранить размер экрана.
                SIZE size;

                //В размер переменной мы должны сохранить размер экрана.
                IntPtr hBitmap;

                //Здесь мы получаем дескриптор контекста устройства рабочего стола.
                IntPtr hDC = PlatformInvokeUSER32.GetDC
                              (PlatformInvokeUSER32.GetDesktopWindow());

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
                    //Мы копируем Битовый массив к контексту устройства памяти.
                    PlatformInvokeGDI32.BitBlt(hMemDC, 0, 0, size.cx, size.cy, hDC,
                                               0, 0, PlatformInvokeGDI32.SRCCOPY);
                    //Мы выбираем старый битовый массив назад к контексту устройства памяти.
                    PlatformInvokeGDI32.SelectObject(hMemDC, hOld);
                    //Мы удаляем контекст устройства памяти.
                    PlatformInvokeGDI32.DeleteDC(hMemDC);
                    //Мы выпускаем контекст устройства экрана.
                    PlatformInvokeUSER32.ReleaseDC(PlatformInvokeUSER32.
                                                   GetDesktopWindow(), hDC);
                    //Изображение создано и сохранено в локальную переменную

                    Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
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

        }


        #endregion
    }
}
