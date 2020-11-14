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




        private void chkSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkSelect.SelectedIndex == 2)
            {
                DataSelect.Visible = false;
            }
            else
            {
                if (chkSelect.SelectedIndex == 0)
                {
                    DataSelect.Format = DateTimePickerFormat.Long;
                    DataSelect.Visible = true;
                }
                else
                {
                    DataSelect.Format = DateTimePickerFormat.Custom;
                    DataSelect.CustomFormat = "MMMM yyyy";
                    DataSelect.ShowUpDown = true;
                    DataSelect.Visible = true;
                }
                
                
            }
        }

        private void btnRunningSelectQuery_Click(object sender, EventArgs e)
        {
            var AppsData = new clRW();

            if (chkSelect.SelectedIndex == 2)
            {
                RefreshGridView(AppsData.GetAll());
            }
            else
            {
                if ((chkSelect.SelectedIndex == 0) && (DataSelect.Value <= DateTime.Now))
                {
                    int day = DataSelect.Value.Day;
                    int mount = DataSelect.Value.Month;
                    int year = DataSelect.Value.Year;
                    ViewSelectDay();

                }
                else if ((chkSelect.SelectedIndex == 1) && (DataSelect.Value.Month <= DateTime.Now.Month) && (DataSelect.Value.Year <= DateTime.Now.Year))
                {
                    int mount = DataSelect.Value.Month;
                    int year = DataSelect.Value.Year;
                    ViewSelectMount();

                }
                else
                {
                    MessageBox.Show("Вы выбрали дату больше сегодняшней!");
                }

                
            }
        }

        private void ViewSelectDay()
        {
            //MessageBox.Show("За день");

            var SelectDay = new clRW();
            var source = SelectDay.Get(true, DataSelect.Value);
            RefreshGridView(source);
        }

        private void ViewSelectMount()
        {
            var SelectMount = new clRW();
            var source = SelectMount.Get(false, DataSelect.Value);
            RefreshGridView(source);
        }

    }
}
