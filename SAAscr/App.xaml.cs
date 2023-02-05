using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Shapes;

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

                        MainWindow mainWindow = new MainWindow();

                        if (Screen.AllScreens.Length > 1)
                        {
                            Screen s2 = Screen.AllScreens[1];
                            Rectangle r2 = s2.WorkingArea;
                            mainWindow.Top = r2.Top;
                            mainWindow.Left = r2.Left;
                            mainWindow.Show();
                        }

                        else
                        {
                            Screen s1 = Screen.AllScreens[0];
                            Rectangle r1 = s1.WorkingArea;
                            mainWindow.Top = r1.Top;
                            mainWindow.Left = r1.Left;
                            mainWindow.Show();
                        }

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
