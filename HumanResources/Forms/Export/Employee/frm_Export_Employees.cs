using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Excel;
using HRMBLL.H0;
using HumanResources.Properties;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Export.Employee
{
    public partial class frm_Export_Employees : RadForm
    {
        private static frm_Export_Employees s_Instance;
        private readonly RadWaitingBar rwb = new RadWaitingBar();

        public frm_Export_Employees()
        {
            InitializeComponent();
        }

        public static frm_Export_Employees Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Export_Employees();
                return s_Instance;
            }
        }

        private void frm_Export_Employees_Load(object sender, EventArgs e)
        {
            FormClosed += Frm_Export_EmployeeContractList_FormClosed;
            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            radGridView1.FilterChanged += RadGridView1_FilterChanged;

            BindData();

            radLabelElement1.Text = "Tổng số nhân viên: " + radGridView1.ChildRows.Count;
            Utilities.Utilities.GridFormatting(radGridView1);

            radGridView1.ShowColumnChooser();
        }

        private void BindData()
        {
            radGridView1.DataSource = EmployeesBLL.GetAllExp();
        }

        private void RadGridView1_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            radLabelElement1.Text = "Tổng số nhân viên: " + Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
        }

        private void RmiExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!backgroundWorker1.IsBusy)
                {
                    var fullName = string.Empty;
                    backgroundWorker1.RunWorkerAsync(fullName);

                    Utilities.Utilities.ShowWaiting(rwb, radGridView1);
                }
            }
            catch
            {
                MessageBox.Show("Lỗi trích xuất dữ liệu!");
            }
        }

        private _Application runExport(string fullName)
        {
            return null;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker) sender;
            for (var i = 1; i <= 100; ++i)
            {
                worker.ReportProgress(i);


                Thread.Sleep(100);
            }
            var fullName = (string) e.Argument;
            e.Result = runExport(fullName);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Utilities.Utilities.StopWaiting(rwb, radGridView1);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
                Utilities.Utilities.SetWaitingText(rwb, e.ProgressPercentage + "%");
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            var customMenuItem = new RadMenuItem();
            customMenuItem.Text = "Xuất Excel";
            customMenuItem.Image = Resources.ExportToXLSX_16x16;
            customMenuItem.Click += RmiExcel_Click;
            var separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);
        }

        private void Frm_Export_EmployeeContractList_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void rbAll_Click(object sender, EventArgs e)
        {
            radGridView1.FilterDescriptors.Clear();
        }

        private void rbLeft_Click(object sender, EventArgs e)
        {
            radGridView1.FilterDescriptors.Clear();

            var filter = new FilterDescriptor();
            filter.PropertyName = "Status";
            filter.Operator = FilterOperator.IsEqualTo;
            filter.Value = "0";
            filter.IsFilterEditor = true;
            radGridView1.FilterDescriptors.Add(filter);
        }

        private void rbWorking_Click(object sender, EventArgs e)
        {
            radGridView1.FilterDescriptors.Clear();

            var filter = new FilterDescriptor();
            filter.PropertyName = "Status";
            filter.Operator = FilterOperator.IsEqualTo;
            filter.Value = "1";
            filter.IsFilterEditor = true;
            radGridView1.FilterDescriptors.Add(filter);
        }

        private void rbWorking_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            Filter();
        }

        private void Filter()
        {
            radGridView1.FilterDescriptors.Clear();

            var filterWorking = new FilterDescriptor();
            filterWorking.PropertyName = "Status";
            filterWorking.Operator = FilterOperator.IsEqualTo;
            filterWorking.Value = "1";
            filterWorking.IsFilterEditor = true;

            if (rbWorking.Checked)
                radGridView1.FilterDescriptors.Add(filterWorking);
            else
                radGridView1.FilterDescriptors.Remove(filterWorking);
        }
    }
}