using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace MyLibenNetFramework
{
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
                            MyIOFile.WriteFileTXT("VersionName -" + versionName + " -- name-" + name, "NFw");
                        }
                        else
                        {
                            if (sp != "" && install == "1")
                            {
                                strVersionNet = strVersionNet + versionName + " " + name + " SP " + sp;
                                MyIOFile.WriteFileTXT("VersionName-" + versionName + " -- name-" + name + " -- SP-" + sp, "NFw");
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
                                MyIOFile.WriteFileTXT("VersionName -" + versionName + " -- name-" + name, "NFw");
                            }
                            else
                            {
                                if (sp != "" && install == "1")
                                {
                                    strVersionNet = strVersionNet + " " + SubKeyName + " " + name + " SP" + sp;
                                    MyIOFile.WriteFileTXT("VersionName-" + versionName + " -- name-" + name + " -- SP-" + sp, "NFw");
                                }
                                else if (install == "1")
                                {
                                    strVersionNet = strVersionNet + " " + SubKeyName + " " + name;
                                    MyIOFile.WriteFileTXT("VersionName -" + versionName + " -- name-" + name, "NFw");
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
            string sfv = "";
            foreach (var attribute in attributes)
            {
                if (attribute.AttributeType == typeof(TargetFrameworkAttribute))
                {
                    var arg = attribute.ConstructorArguments.FirstOrDefault();
                    if (arg == null)
                    {
                        sfv = "";
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

                        MyIOFile.WriteFileTXT("System-" + intRegVerNet + " -- App-" + intMinVerApp + "(" + sfv0 + ")", "NFw");

                        if (intRegVerNet > intMinVerApp) blNetFrameWork = true;
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
}
