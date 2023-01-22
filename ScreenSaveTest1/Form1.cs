using System.Media;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace ScreenSaveTest1
{
    //https://www.codeproject.com/Articles/31376/Making-a-C-screensaver
    public partial class Form1 : Form
    {
        private Point MouseXY;
        private int ScreenNumber;
        SoundPlayer simpleSound;
        bool IsPreviewMode;


        #region Preview API's

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion

        public Form1(Rectangle Bounds)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; //Убираем рамку вокруг формы
            pictureBox1.Dock= DockStyle.Fill; //заполняем форму по всей ширине
            //ScreenNumber = scr; //это пригодится если у вас заставка будет иметь какие либо параметры
            this.Bounds = Bounds;
            Cursor.Hide();

        }

        public Form1(IntPtr PreviewHandle)
        {
            InitializeComponent();

            //
            SetParent(this.Handle, PreviewHandle);

            //
            //
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle,-16) | 0x40000000));

            //
            Rectangle parentRec;
            GetClientRect(PreviewHandle, out parentRec);
            this.Size = parentRec.Size;


            //
            this.Location = new Point(0, 0);
            IsPreviewMode = true;

        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Bounds = Screen.AllScreens[ScreenNumber].Bounds; //размер  во весь экран
            //Cursor.Hide();//прячем курсор
            //TopMost = true; //отображаем форму поверх других
            
            
            //simpleSound = new SoundPlayer(Resource1.test);
            //simpleSound.Play();

        }

        private void OnMouseEvent(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //if (!MouseXY.IsEmpty) //вслучаи событий на мышь закрываем форму и приложение
            //{
            //    if (MouseXY !=new Point(e.X, e.Y))
            //    {
            //        Close();
            //        simpleSound.Stop(); //останавливаем звук
            //    }

            //    if (e.Clicks>0)
            //    {
            //        Close();
            //        simpleSound.Stop(); //останавливаем звук
            //    }

            //}

            //MouseXY = new Point(e.X, e.Y);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsPreviewMode)
            {
                Application.Exit();
            }

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (!IsPreviewMode)
            {
                Application.Exit();
            }
        }

        Point OriginalLocation = new Point(int.MaxValue, int.MaxValue);
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPreviewMode)
            {
                if (OriginalLocation.X == int.MaxValue & OriginalLocation.Y ==int.MaxValue)
                {
                    OriginalLocation = e.Location;
                }

                if (Math.Abs(e.X - OriginalLocation.X)>20 | Math.Abs(e.Y-OriginalLocation.Y)>20)
                {
                    Application.Exit();
                }
            }
        }
    }
}