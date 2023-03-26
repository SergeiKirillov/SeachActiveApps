using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiteDB;

namespace SeachActiveApp
{
    public partial class frmSettingApp : Form
    {
        public frmSettingApp()
        {
            //При инициализации формы считываем значения из класса и по нему взводим элемент checkbox
            
            InitializeComponent();
            chkDisableScreenSave.Checked = Globals.blDisableScreenSave;
            txtTimeDisableScreenSave.Text = Globals.intTimeDisableScreenSave.ToString();
            chkScreenShotDesktop.Checked = Globals.blScreenShotDesktop;
            chkSaveToBD.Checked = Globals.blSaveDateToBD;
            chkSaveToFiles.Checked = Globals.blSaveDateToFile;

        }

        private void chkDisableScreenSave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisableScreenSave.Checked==true)
            {
                Globals.blDisableScreenSave = true;
            }
            else if (chkDisableScreenSave.Checked==false)
            {
                Globals.blDisableScreenSave = false;
            }
        }

        private void txtTimeDisableScreenSave_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number!= 8)
            {
                e.Handled = true;
            }


        }

        private void txtTimeDisableScreenSave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                Globals.intTimeDisableScreenSave = Convert.ToInt32(txtTimeDisableScreenSave.Text);
            }
        }

        private void txtTimeDisableScreenSave_Leave(object sender, EventArgs e)
        {
            Globals.intTimeDisableScreenSave = Convert.ToInt32(txtTimeDisableScreenSave.Text);
        }

        private void chkScreenShotDesktop_CheckedChanged(object sender, EventArgs e)
        {
            if (chkScreenShotDesktop.Checked)
            {
                Globals.blScreenShotDesktop = true;
            }
            else
            {
                Globals.blScreenShotDesktop = false;
            }
        }

        private void chkEnableSeachActiveApp_CheckedChanged(object sender, EventArgs e)
        {
            //Активация WWW сервера 
        }

        
        private void lnkWWW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkWWW.LinkVisited= true;
            System.Diagnostics.Process.Start("http://localhost:8000");
        }

        private void chkSaveToFiles_CheckedChanged(object sender, EventArgs e)
        {
            //Запись результатов в файл
            if (chkSaveToFiles.Checked)
            {
                Globals.blSaveDateToFile = true;
            }
            else
            {
                Globals.blSaveDateToFile= false;
            }
        }

        private void chkSaveToBD_CheckedChanged(object sender, EventArgs e)
        {
            //Запись результатов в БД
            if (chkSaveToBD.Checked)
            {
                Globals.blSaveDateToBD = true;
            }
            else
            {
                Globals.blSaveDateToBD = false;
            }
        }
    }
}
