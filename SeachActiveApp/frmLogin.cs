using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeachActiveApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string day = DateTime.Now.ToString("ddMMyyyy");
            if (txtPassword.Text==day)
            {
                frmSettingApp frmSetting = new frmSettingApp();
                if (frmSetting.Visible)
                {
                    frmSetting.Focus();
                }
                else
                {
                    frmSetting.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("День милиции 10111917");
            }
        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            frmViewResult frmView = new frmViewResult();
            if (frmView.Visible)
            {
                frmView.Focus();
            }
            else
            {
                frmView.ShowDialog();
            }
            
        }
    }
}
