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
using MyLibenNetFramework;

namespace SeachActiveApp
{
    //TODO Форма настроек приложения SeachActiveApp
    public partial class frmSettingApp : Form
    {
        public frmSettingApp()
        {
            //При инициализации формы считываем значения из класса и по нему взводим элемент checkbox
            
            InitializeComponent();
           

        }

        private void frmSettingApp_Load(object sender, EventArgs e)
        {
            chkDisableScreenSave.Checked = Globals.blDisableScreenSave;
            txtTimeDisableScreenSave.Text = Globals.intTimeDisableScreenSave.ToString();
            chkScreenShotDesktop.Checked = Globals.blScreenShotDesktop;
            chkSaveToBD.Checked = Globals.blSaveDateToBD;
            chkSaveToFiles.Checked = Globals.blSaveDateToFile;
            //chkAutoStartInWindows.Checked = Globals.blWindowsAutoStart;
            if (Globals.blWindowsAutoStart)
            {
                chkAutoStartInWindows.Checked = true;
                lblAutoStartInWindows.Text = WorkInReestr.strGetAutostartWindows("SeachActiveApp");
            }
            else
            {
                chkAutoStartInWindows.Checked = false;
                lblAutoStartInWindows.Text = "";
            }


            if (Globals.blEnableActiveAppSaving)
            {
                chkEnableSeachActiveApp.Checked = true;
                chkSaveToBD.Enabled = true;
                chkSaveToFiles.Enabled = true;
            }
            else
            {
                chkEnableSeachActiveApp.Checked = false;

                //Globals.blSaveDateToBD = false;
                //chkSaveToBD.Checked = false;
                chkSaveToBD.Enabled = false;

                //Globals.blSaveDateToFile = false;
                //chkSaveToFiles.Checked = false;
                chkSaveToFiles.Enabled = false;
                
                
            }

        }

        

        

       

        private void chkEnableSeachActiveApp_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        
        private void lnkWWW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkWWW.LinkVisited= true;
            System.Diagnostics.Process.Start("http://localhost:8000");
        }

       

        
//--------------------------------------------------------------------------------------------------------
        private void chkAutoStartInWindows_Click(object sender, EventArgs e)
        {
            if (!chkAutoStartInWindows.Checked) 
            {
                Globals.blWindowsAutoStart = false;
                lblAutoStartInWindows.Text = "";

            }
            else
            {
                Globals.blWindowsAutoStart = true;
                lblAutoStartInWindows.Text = WorkInReestr.strGetAutostartWindows("SeachActiveApp");

            }
        }

        private void chkScreenShotDesktop_Click(object sender, EventArgs e)
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

        #region Оключение заставки

        
        private void chkDisableScreenSave_Click(object sender, EventArgs e)
        {
            if (chkDisableScreenSave.Checked)
            {
                Globals.blDisableScreenSave = true;
            }
            else
            {
                Globals.blDisableScreenSave = false;
            }
        }

        private void txtTimeDisableScreenSave_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }


        }

        private void txtTimeDisableScreenSave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Globals.intTimeDisableScreenSave = Convert.ToInt32(txtTimeDisableScreenSave.Text);
                Globals.blDisableScreenSave = true;
                chkScreenShotDesktop.Checked= true;
            }
        }

        private void txtTimeDisableScreenSave_Leave(object sender, EventArgs e)
        {
            Globals.intTimeDisableScreenSave = Convert.ToInt32(txtTimeDisableScreenSave.Text);
        }

        #endregion

      

        private void chkSaveToFiles_Click(object sender, EventArgs e)
        {
            //Запись результатов в файл
            if (chkSaveToFiles.Checked)
            {
                Globals.blSaveDateToFile = true;
            }
            else
            {
                Globals.blSaveDateToFile = false;
            }
        }

        private void chkSaveToBD_Click(object sender, EventArgs e)
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

        private void chkEnableSeachActiveApp_Click(object sender, EventArgs e)
        {
            #region Нужно ли производить контроль за активным приложением и если да то куда писать результат в БД и/или в тестовый файл
            if (chkEnableSeachActiveApp.Checked)
            {
                Globals.blEnableActiveAppSaving = true;
                chkSaveToBD.Enabled = true;
                chkSaveToFiles.Enabled = true;

            }
            else
            {
                Globals.blEnableActiveAppSaving = false;
                
                //Globals.blSaveDateToBD = false;
                //chkSaveToBD.Checked = false;
                chkSaveToBD.Enabled = false;

                //Globals.blSaveDateToFile = false;
                //chkSaveToFiles.Checked = false;
                chkSaveToFiles.Enabled = false;

            }
            #endregion
        }
    }
}
