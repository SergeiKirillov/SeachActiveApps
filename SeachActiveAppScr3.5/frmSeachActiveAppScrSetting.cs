using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SeachActiveAppScr3._5
{
    public partial class frmSeachActiveAppScrSetting : Form
    {
        public frmSeachActiveAppScrSetting()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void SaveSettings()
        {
            // Create or get existing Registry subkey
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SergeiAKirApp");

            key.SetValue("Text", txtBox.Text);

        }

        private void LoadSettings() 
        {
            // Get the value stored in the Registry
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SergeiAKirApp");
            if (key == null)
                txtBox.Text = "ScreenSaver от программы SeachApctiveApp";
            else
                txtBox.Text = (string)key.GetValue("Text");

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
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SergeiAKirApp");

            string ssText = (string)key.GetValue("text");
            
            //if (key == null) txtBox.Text = "C# Screen Saver";
            
            if (ssText == null)
            {
                txtBox.Text = "C# Screen Saver";
            }
            else
            {
                txtBox.Text = (string)key.GetValue("text");
            }
                
        }
    }
}
