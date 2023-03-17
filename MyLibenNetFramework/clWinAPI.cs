using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyLibenNetFramework
{
    public class clWinAPI
    {
        #region Hide Console App
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        //And then if you want to hide it use this command:

        public static void HideConsoleApp(bool blHide)
        {
            if (blHide)
            {
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);
            }
            else
            {
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_SHOW);
            }


        }


        #endregion

        #region #region Active App V1

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, IntPtr msg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        public static void FindActiveWindows()
        {
            IntPtr handle = GetActiveWindow();
            //SendMessage(handle, WM_SYSCOMMAND, SC_CLOSE, 0);

        }
        #endregion

        #region Active App V2 


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        public static string GetCaptionOfActiveWindow()
        {
            var strTitle = string.Empty;
            var handle = GetForegroundWindow();
            // Obtain the length of the text   
            var intLength = GetWindowTextLength(handle) + 1;
            var stringBuilder = new StringBuilder(intLength);
            if (GetWindowText(handle, stringBuilder, intLength) > 0)
            {
                strTitle = stringBuilder.ToString();
            }

            return strTitle;
        }

        #endregion

        #region  Пoиск активного приложения(вариант 3)
        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindows();




        #endregion

        //https://www.codeproject.com/Articles/468566/Logging-Active-Window-Titles   --- Logging Active Window Titles

        //books.google.kz/books?id=hR3JipLEAaYC&pg=PA185&lpg=PA185&dq=c%23+%D0%BF%D1%80%D0%BE%D0%B2%D0%B5%D1%80%D0%BA%D0%B0+%D1%87%D1%82%D0%BE+%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0+%D1%83%D0%B6%D0%B5+%D0%B7%D0%B0%D0%BF%D1%83%D1%89%D0%B5%D0%BD%D0%B0&source=bl&ots=0LBHvyuqly&sig=ACfU3U25kC-FJaa-WH-IK14LiGjOVGW4Ig&hl=ru&sa=X&ved=2ahUKEwi0pqfTzuvsAhVDl4sKHcSBCvc4FBDoATAFegQIAhAC#v=onepage&q=c%23%20%D0%BF%D1%80%D0%BE%D0%B2%D0%B5%D1%80%D0%BA%D0%B0%20%D1%87%D1%82%D0%BE%20%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0%20%D1%83%D0%B6%D0%B5%20%D0%B7%D0%B0%D0%BF%D1%83%D1%89%D0%B5%D0%BD%D0%B0&f=false
        //https://habr.com/ru/post/352096/
        //https://www.c-sharpcorner.com/article/working-with-win32-api-in-net/
        //https://stackoverflow.com/questions/12019524/get-active-window-of-net-application  --- поиск и закрытие активного приложения



        #region Закрытие приложения

        #endregion


    }
}

