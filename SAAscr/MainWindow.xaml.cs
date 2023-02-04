using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAAscr
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer _timer = new Timer(1000);
        public MainWindow()
        {
            InitializeComponent();

            SetTime();
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Start();

        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(SetTime));
        }

        private void SetTime() => timeText.Content = DateTime.Now.ToLongTimeString();

        bool _isActive;
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isActive)
            {
                _isActive = true;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
