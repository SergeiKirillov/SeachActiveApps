using System.Runtime.InteropServices;

namespace SeachActiveAppScreenSaver
{

    //https://sites.harding.edu/fmccown/screensaver/screensaver.html

    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        private bool previewMode;


        public Form1(Rectangle Bounds)
        {
            InitializeComponent();
            this.Bounds = Bounds;
        }

        public Form1(IntPtr PreviewWndHandle)
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
            TextLabel.Font = new System.Drawing.Font("Arial", 6);

            previewMode = true;

        }

        private Random rand = new Random();
        private void Form1_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
            TopMost = true;

            moveTimer.Interval = 3000;
            //moveTimer.Tick += new EventHandler(moveTimer_Tick); 
            moveTimer.Start();
        }

        private Point mouseLocation;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!previewMode) 
            { 
               Application.Exit(); 
            }
            else
            {
                if (!mouseLocation.IsEmpty)
                {
                    if (Math.Abs(mouseLocation.X - e.X) > 5 || Math.Abs(mouseLocation.Y - e.Y) > 5) Application.Exit();

                }

                mouseLocation = e.Location;
            }
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!previewMode) Application.Exit();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!previewMode) Application.Exit();
                       
        }

        private void moveTimer_Tick(object sender, EventArgs e)
        {
            TextLabel.Left = rand.Next(Math.Max(1, Bounds.Width - TextLabel.Width));
            TextLabel.Top = rand.Next(Math.Max(1, Bounds.Height - TextLabel.Height));
        }
    }
}