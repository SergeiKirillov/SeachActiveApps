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
    public partial class frmViewResult : Form
    {
        public frmViewResult()
        {
            InitializeComponent();

            var AppsData = new clRW();

            RefreshGridView(AppsData.GetAll());

            GridViewApps.Columns[0].Visible = false;

            GridViewApps.Columns[1].HeaderText = "Дата/Время";
            GridViewApps.Columns[1].DefaultCellStyle.Format = "dd.MM HH:mm:ss"; //Формат данных по умолчанию

            GridViewApps.Columns[2].HeaderText = "Что было запущено";

            GridViewApps.Columns[3].Visible = false;

            GridViewApps.AllowUserToAddRows = false;
            GridViewApps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridViewApps.MultiSelect = false;




        }


        private void RefreshGridView(IList<clData1Hour> AppRezult)
        {
            var bindList = new BindingList<clData1Hour>(AppRezult);
            var source = new BindingSource(bindList, null);

            GridViewApps.DataSource = source;
        }
    }
}
