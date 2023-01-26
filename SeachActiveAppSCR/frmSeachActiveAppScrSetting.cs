using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeachActiveAppSCR
{
    public partial class frmSeachActiveAppScrSetting : Form
    {
        public frmSeachActiveAppScrSetting()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            //// Get the value stored in the Registry
            //RegistryKey key = Registry.CurrentUser.OpenSubKey("SergeiAKirApp");
            //if (key == null)
            //    txtBox.Text = "ScreenSaver от программы SeachApctiveApp";
            //else
            //    txtBox.Text = (string)key.GetValue("Text");

            if (Program.blTxtScreenSaver)
            {

                chkText.Checked = true;
                txtBox.Enabled = true;
                chkTimeNow.Checked = false;

                //// Get the value stored in the Registry
                //RegistryKey key = Registry.CurrentUser.OpenSubKey("SergeiAKirApp");
                //if (key == null)
                //    txtBox.Text = "ScreenSaver от программы SeachApctiveApp";
                //else
                //    txtBox.Text = (string)key.GetValue("Text");

                txtBox.Text = Program.strTxtScreenSaver;
            }
            else
            {
                chkText.Checked = false;
                txtBox.Enabled = false;
                chkTimeNow.Checked = true;


            }

        }

        private void SaveSettings()
        {
            // Create or get existing Registry subkey
            //RegistryKey key = Registry.CurrentUser.CreateSubKey("SergeiAKirApp");
            //key.SetValue("Text", txtBox.Text);

            Program.strTxtScreenSaver = txtBox.Text;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmSeachActiveAppScrSetting_Load(object sender, EventArgs e)
        {
            //RegistryKey key = Registry.CurrentUser.OpenSubKey("SergeiAKirApp");

            //string ssText = (string)key.GetValue("text");

            ////if (key == null) txtBox.Text = "C# Screen Saver";

            //if (ssText == null)
            //{
            //    txtBox.Text = "C# Screen Saver";
            //}
            //else
            //{
            //    txtBox.Text = (string)key.GetValue("text");
            //}
        }

        private void chkTimeNow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTimeNow.Checked)
            {
                chkText.Checked = false;
            }
            else
            {
                chkText.Checked = true;
            }

            //Program.blTxtScreenSaver = chkText.Checked;
        }

        private void chkText_CheckedChanged(object sender, EventArgs e)
        {
            if (chkText.Checked)
            {
                txtBox.Enabled = true;
                chkTimeNow.Checked = false;
            }
            else
            {
                txtBox.Enabled = false;
                chkTimeNow.Checked = true;
            }

            Program.blTxtScreenSaver = chkText.Checked;
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Program.strTxtScreenSaver = txtBox.Text;
            }
        }
    }
}
