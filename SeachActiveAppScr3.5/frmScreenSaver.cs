using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SeachActiveAppScr3._5
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

        private bool previewMode=false;




        public frmScreenSaver()
        {
            InitializeComponent();
        }

        public frmScreenSaver(Rectangle bounds)
        {
            InitializeComponent();
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

            RegistryKey key = Registry.CurrentUser.OpenSubKey("SergeiAKirApp");
            string ssText = (string)key.GetValue("text");
            if (ssText == null)
            {
                txtLabel.Text = "C# Screen Saver";
            }
            else
            {
                txtLabel.Text = (string)key.GetValue("text");
            }

            MoveTimer.Interval = 1000;
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
            string TimeNow = DateTime.Now.ToString("HH:mm");
            txtLabel.Text = TimeNow;
            
        }
    }
}
