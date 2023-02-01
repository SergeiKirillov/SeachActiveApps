using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeachActiveAppSCR
{
    public partial class frmScreenSaver : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        private bool previewMode = false;



        public frmScreenSaver()
        {
            InitializeComponent();
        }

        public frmScreenSaver(Rectangle bounds)
        {
            InitializeComponent();

            
            //Bitmap imgScreenShot = new Bitmap(@"d:\screenshot.jpg");
            //this.BackgroundImage = imgScreenShot;

            this.BackgroundImage = MyScreenShot.CaptureScreen.GetDesktopImage();
            
            txtLabel.BackColor = Color.Transparent;
            lblStopTimeScreenSaver.BackColor = Color.Transparent;
            this.Bounds = bounds;
        }

        public frmScreenSaver(IntPtr PreviewWndHandle)
        {
            InitializeComponent();

           
            //
            // Set the preview window as the parent of this window
            SetParent(this.Handle, PreviewWndHandle);

            // Make this a child window so it will close when the parent dialog closes
            // GWL_STYLE = -16, WS_CHILD = 0x40000000
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            // Place our window inside the parent
            Rectangle ParentRect;
            GetClientRect(PreviewWndHandle, out ParentRect);
            Size = ParentRect.Size;
            Location = new Point(0, 0);

            // Make text smaller
            txtLabel.Font = new System.Drawing.Font("Arial", 6);

            previewMode = true;
        }

        private Random rand = new Random();

        private void frmScreenSaver_Load(object sender, EventArgs e)
        {
            
            

            Cursor.Hide();
            TopMost = true;
            //this.TransparencyKey = this.BackColor;

            //RegistryKey key = Registry.CurrentUser.OpenSubKey("SergeiAKirApp");
            //string ssText = (string)key.GetValue("text");
            //if (ssText == null)
            //{
            //    txtLabel.Text = "C# Screen Saver";
            //}
            //else
            //{
            //    txtLabel.Text = (string)key.GetValue("text");
            //}

            if (Program.blTxtScreenSaver)
            {
                txtLabel.Text = Program.strTxtScreenSaver;
            }
            else
            {
                txtLabel.Text = DateTime.Now.ToString("HH:mm");
            }


            MoveTimer.Interval = 60000; //1мин  
            MoveTimer.Start();




        }

      

        private void frmScreenSaver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!previewMode) Application.Exit();
            //Application.Exit();
        }

        private void frmScreenSaver_MouseClick(object sender, MouseEventArgs e)
        {
            if (!previewMode) Application.Exit();
            //Application.Exit();
        }

        private Point mouseLocation;
        private void frmScreenSaver_MouseMove(object sender, MouseEventArgs e)
        {
            //if (!previewMode)
            //{
            //    Application.Exit();
            //}
            //else
            //{
            if (!mouseLocation.IsEmpty)
            {
                if (Math.Abs(mouseLocation.X - e.X) > 5 || Math.Abs(mouseLocation.Y - e.Y) > 5) Application.Exit();

            }

            mouseLocation = e.Location;
            //}
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            txtLabel.Left = rand.Next(Math.Max(1, Bounds.Width - txtLabel.Width));
            txtLabel.Top = rand.Next(Math.Max(1, Bounds.Height - txtLabel.Height));

            if (Program.blTxtScreenSaver)
            {
                txtLabel.Text = Program.strTxtScreenSaver;
            }
            else
            {
                txtLabel.Text = DateTime.Now.ToString("HH:mm");
            }


            #region Прием данных с SeachActiveApp
            try
            {
                //Массив для сообщений из общей памяти
                char[] message1;

                //Размер введенного сообщения
                int size;

                //Отсчет до выключния
                int message2;

                //получение существующего участка разделенной памяти 
                //параметр - название участка

                MemoryMappedFile shareMemory = MemoryMappedFile.OpenExisting("TimeDisableScreenSave");

                //Сначала считываем размер сообщния, чтобы создать массив данного размера
                //Integer занимает 4 байта, начинается с первого байта, поэтому передаем цифры 0 и 4

                using (MemoryMappedViewAccessor reader = shareMemory.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read))
                {
                    size = reader.ReadInt32(0);
                }


                using (MemoryMappedViewAccessor reader = shareMemory.CreateViewAccessor(4, 4, MemoryMappedFileAccess.Read))
                {
                    message2 = reader.ReadInt32(0);
                }

                //Считываем сообщение, используя полученный выше размер
                //Сообщение - это строка или массив объектов char, каждый из которых занимает два байта
                //Поэтому вторым параметром передаем число символов умножив на из размер в байтах плюс
                //А первый параметр - смещение - 4 байта, которое занимает размер сообщения
                using (MemoryMappedViewAccessor rear = shareMemory.CreateViewAccessor(8, size * 2, MemoryMappedFileAccess.Read))
                {
                    //Массив символов сообщения
                    message1 = new char[size];
                    rear.ReadArray<char>(0, message1, 0, size);
                }

                if (message2==0)
                {
                    Application.Exit();
                }

                lblStopTimeScreenSaver.Text = message2.ToString();

                //Console.Write(DateTime.Now + " -1- ");
                //Console.Write(message1);
                //Console.Write('\n');
                //Console.WriteLine(DateTime.Now + " -2- " + message2);
                ////Console.WriteLine("Для выхода из программы нажмите любую клавишу");
                ////Console.ReadLine();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }


            #endregion


        }


      
    }
}
