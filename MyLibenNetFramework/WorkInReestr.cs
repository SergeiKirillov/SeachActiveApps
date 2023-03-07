﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyLibenNetFramework
{
    public class WorkInReestr
    {
        private static string NameApp = "SergeiAKirApp";
        public static string strToAPP(string NameKey)
        {

            using (RegistryKey strTextSS = Registry.CurrentUser.OpenSubKey(NameApp, true))
            {
                if (strTextSS != null)
                {

                    string DSS = strTextSS.GetValue(NameKey) as string;
                    if (DSS != null)
                    {
                        return Regex.Replace(DSS, @"\\n", "\n").Replace("\n", Environment.NewLine);
                    }
                    else
                    {
                        string strText = "Screen Saver \nдля программы SeachActiveApp";
                        //string strText = "1";
                        strAPPTo(NameKey, strText);
                        return strText;
                    }


                }
                else
                {
                    //Запись в реестр
                    string strText = "Screen Saver \nдля программы SeachActiveApp";
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
            if (((ValueKey.Length * 2) < 1048576) && (ValueKey != null))
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
                        intText = Convert.ToInt32(intTSS.GetValue(NameKey, 1));
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

        public static DateTime dtToApp(string NameKey)
        {
            return DateTime.Now;
        }
        public static void dtAppTo(String NameKey, DateTime ValueKey)
        {
            if (ValueKey != null)
            {
                using (RegistryKey strTextSS = Registry.CurrentUser.CreateSubKey(NameApp))
                {

                    strTextSS.SetValue(NameKey, ValueKey.ToString());

                }
            }
            else
            {
                using (RegistryKey strTextSS = Registry.CurrentUser.CreateSubKey(NameApp))
                {

                    strTextSS.SetValue(NameKey, DateTime.Now.ToString());

                }
            }


        }

    }
}