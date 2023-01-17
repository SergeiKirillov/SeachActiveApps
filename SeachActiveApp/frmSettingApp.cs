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
    }
}
