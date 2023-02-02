using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

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
            #region меняем экранную заставку
            //Image f = MyScreenShot.CaptureScreen.GetDesktopImage();
            //f.Save("d:\\screenshot.jpg");
            #endregion



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
                    //перенесены в класс MyScreenShot
                    //MyScreenShot.MakeScreenshot();
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
                //перенесены в класс MyScreenShot

                MyScreenShot.MakeScreenshot();
                //MakeScreenshot();

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

    }
}
