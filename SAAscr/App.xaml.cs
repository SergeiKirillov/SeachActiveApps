using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace SAAscr
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string[] args = e.Args;
            if (args.Length>0)
            {
                string arg = args[0].ToLower(CultureInfo.InvariantCulture).Trim().Substring(0, 2);

                switch (arg)
                {
                    case "/c":
                        System.Windows.MessageBox.Show("Этот сринсервер не имеет пока конфигурации.", "ScreenSaver", MessageBoxButton.OK, MessageBoxImage.Information);
                        System.Windows.Application.Current.Shutdown();
                        break;
                    case "/s":
                        MainWindow Main = new MainWindow();
                        Main.Show();
                        break;
                    case "/p":

                        break;
                    default:
                        break;
                }


            }
            else
            {

            }
        }

        


    }
}
