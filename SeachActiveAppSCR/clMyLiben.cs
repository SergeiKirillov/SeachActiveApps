using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

internal class clMyLiben
{


}
class MyIO
{
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
}

class MyScreenShot
{


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
                MyIO.WriteFileTXT(DateTime.Now, "hBitmap:" + hBitmap.ToString(), "SceenShot"); //вывод в текстовы файл
                //Как hBitmap IntPtr, мы не можем проверить его значение null.
                //Для этой цели используется значение IntPtr.Zero.
                if (hBitmap != IntPtr.Zero)
                {
                    //Здесь мы выбираем совместимое растровое изображение в памяти устройства
                    //контекст и держит ссылку на старый битовый массив.
                    IntPtr hOld = (IntPtr)PlatformInvokeGDI32.SelectObject
                                           (hMemDC, hBitmap);
                    MyIO.WriteFileTXT(DateTime.Now, "hOld:" + hOld.ToString(), "SceenShot"); //вывод в текстовы файл
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

}


