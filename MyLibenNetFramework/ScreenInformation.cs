using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyLibenNetFramework
{
    public class ScreenInformation
    {
        #region Применение
        //public partial class App : Application
        //{
        //    protected override void OnStartup(StartupEventArgs e)
        //    {
        //        base.OnStartup(e);
        //        LinkedList<ScreenInformation.WpfScreen> screens = ScreenInformation.GetAllScreens();
        //        foreach (var screen in screens)
        //        {
        //            var window = new MainWindow();

        //            Console.WriteLine("Metrics {0} {1}", screen.metrics.top, screen.metrics.left);

        //            window.Top = screen.metrics.top;
        //            window.Left = screen.metrics.left;
        //            window.Show();
        //        }
        //    }
        //}
        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public struct ScreenRect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [DllImport("user32.dll")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lpRect, MonitorEnumProc callback, int dwData);

        private delegate bool MonitorEnumProc(IntPtr hDesktop, IntPtr hdc, ref ScreenRect pRect, int dwData);

        public class wpfScreen
        {
            public ScreenRect metrics;
            public wpfScreen(ScreenRect prect)
            {
                metrics = prect;
            }

            static LinkedList<wpfScreen> AllScreens = new LinkedList<wpfScreen>();

            public static LinkedList<wpfScreen> GetAllScreens()
            {
                ScreenInformation.wpfScreen.GetMonitorCount();
                return AllScreens;
            }

            public static int GetMonitorCount()
            {
                AllScreens.Clear();
                int monCount = 0;
                MonitorEnumProc callback = (IntPtr hDesktop, IntPtr hdc, ref ScreenRect prect, int d) =>
                {
                    Console.WriteLine("Left {0}", prect.Left);
                    Console.WriteLine("Right {0}", prect.Right);
                    Console.WriteLine("Top {0}", prect.Top);
                    Console.WriteLine("Bottom {0}", prect.Bottom);
                    AllScreens.AddLast(new wpfScreen(prect));
                    return ++monCount > 0;
                };

                if (EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, 0))
                    Console.WriteLine("You have {0} monitors", monCount);
                else
                    Console.WriteLine("An error occured while enumerating monitors");

                return monCount;
            }
        }

    }
}
