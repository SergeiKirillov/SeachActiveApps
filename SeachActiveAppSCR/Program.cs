using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibenNetFramework;


namespace SeachActiveAppSCR
{
    static class Program
    {
        /// <summary>
        /// blUpdate - bool нужно ли обновлять StrMessage, взводится в true внешней программой
        /// dtUpdate - DateTime формирования Списка формирования сообщения
        ///blCheck - Выполненено ли мероприятие, выставляется внешней программой.
        ///
        ///StrMessageGov - государственные праздники
        ///StrMessageHome - семейная памятка
        ///StrMessageWork - рабочая памятка
        /// </summary>

        public static bool blUpdate
        {
            get { return  WorkInReestr.blToAPP("blUpdate"); }
            set { WorkInReestr.strAPPTo("blText", value.ToString()); }
        }

        public static string dtUpdate
        {
            get { return WorkInReestr.strToAPP("dtUpdate"); }
            set { WorkInReestr.strAPPTo("dtUpdate", value); }
        }
        public static bool blCheck
        {
            get { return WorkInReestr.blToAPP("blCheck"); }
            set { WorkInReestr.strAPPTo("blCheck", value.ToString()); }
        }

        public static string StrMessageGov
        {
            get { return WorkInReestr.strToAPP("StrMessageGov"); }
            set { WorkInReestr.strAPPTo("StrMessageGov", value); }
        }

        public static string StrMessageHome
        {
            get { return WorkInReestr.strToAPP("StrMessageHome"); }
            set { WorkInReestr.strAPPTo("StrMessageHome", value); }
        }
        public static string StrMessageWork
        {
            get { return WorkInReestr.strToAPP("StrMessageWork"); }
            set { WorkInReestr.strAPPTo("StrMessageWork", value); }
        }





        public static bool blTxtScreenSaver
        {
            get
            {
                #region Variant 1 - Read Bool
                //bool blSS;
                //using (RegistryKey blTextSS = Registry.CurrentUser.OpenSubKey("SergeiAKirApp", true))
                //{
                //    if (blTextSS!=null)
                //    {
                //        blSS = Convert.ToBoolean(blTextSS.GetValue("blText") as string);
                //    }
                //    else
                //    {
                //        blSS = false;
                //    }


                //}
                //return blSS;
                #endregion

                return WorkInReestr.blToAPP("blText");
            }
            set
            {
                #region Variant 1 - write Bool
                //Запись в реестр значения value
                //using (RegistryKey blTextSS = Registry.CurrentUser.OpenSubKey("SergeiAKirApp", true))
                //{
                //    blTextSS.SetValue("blText", value);
                //}
                #endregion

                WorkInReestr.strAPPTo("blText", value.ToString());

            }
        }

        public static bool blDisableScreenSave
        {
            get
            {
                return WorkInReestr.blToAPP("DisableScreenSave");
            }
            set
            {
                WorkInReestr.strAPPTo("DisableScreenSave", value.ToString());

            }
        }

        public static bool blDesktopSaveForSceenShot
        {
            get{return WorkInReestr.blToAPP("DesktopSaveForScreenSave");}
            set{WorkInReestr.strAPPTo("DesktopSaveForScreenSave", value.ToString());}
        }

        public static bool blSaveDesktopToJPG
        {
            get { return WorkInReestr.blToAPP("SaveDesktopToJPG"); }
            set { WorkInReestr.strAPPTo("SaveDesktopToJPG", value.ToString()); }
        }

        public static string strTxtScreenSaver
        {
            get
            {
                #region Variant 1 - Read
                //string strSS;
                //using (RegistryKey strTextSS = Registry.CurrentUser.OpenSubKey("SergeiAKirApp", true))
                //{
                //    if (strTextSS!=null)
                //    {
                //        strSS = strTextSS.GetValue("Text") as string;

                //        if (strSS != null)
                //        {
                //            return strSS;

                //        }
                //        else
                //        {
                //            return "Screen Saver для программы SeachActiveApp";
                //        }
                //    }
                //    else
                //    {
                //        return "Screen Saver для программы SeachActiveApp";
                //    }

                //}


                #endregion

                return WorkInReestr.strToAPP("Text");

            }
            set
            {
                #region Variant 1 - Write
                //Запись в реестр значения value
                //using (RegistryKey strTextSS = Registry.CurrentUser.OpenSubKey("SergeiAKirApp", true))
                //{
                //    strTextSS.SetValue("Text", value);
                //}
                #endregion

                WorkInReestr.strAPPTo("Text", value);
            }
        }



        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                //MyScreenShot.MakeScreenshot();
                //.MyIOFile.WriteFileTXT("main", "SeachActiveAppSCR");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new frmScreenSaver());

                #region Проверка значений в реестре и при их отсутствии значение по умолчанию


                #endregion

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
                        
                        Application.Run(new frmSeachActiveAppScrSetting());
                    }

                    else if (firstArgument == "/p")
                    {
                        

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
                        //MyLibenNetFramework.MyIOFile.WriteFileTXT("/s", "SeachActiveAppSCR");
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
                    ShowScreenSaver();
                    Application.Run();

                    //Application.Run(new frmSeachActiveAppScrSetting());
                }
            }
            catch (Exception)
            {

                MyLibenNetFramework.MyIOFile.WriteFileTXT("error Main", "SeachActiveAppSCR");
            }
           
        }

        static void ShowScreenSaver()
        {
            //MyLibenNetFramework.MyIOFile.WriteFileTXT("ShowScreenSaver", "SeachActiveAppSCR");
            foreach (Screen item in Screen.AllScreens)
            {
                frmScreenSaver Screen = new frmScreenSaver(item.Bounds);
                Screen.Show();
            }
        }
    }
}
