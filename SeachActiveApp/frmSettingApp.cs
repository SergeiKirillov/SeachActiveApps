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
            if (e.KeyCode==Keys.Enter || e.KeyCode==Keys.Tab)
            {
                Globals.intTimeDisableScreenSave = Convert.ToInt32(txtTimeDisableScreenSave.Text);
            }
        }

        private void txtTimeDisableScreenSave_Leave(object sender, EventArgs e)
        {
            txtTimeDisableScreenSave.Text = Globals.intTimeDisableScreenSave.ToString();
        }
    }
}
