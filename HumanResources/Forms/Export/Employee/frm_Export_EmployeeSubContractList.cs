using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Excel;
using HRMBLL.H0;
using HumanResources.Properties;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Export.Employee
{
    public partial class frm_Export_EmployeeSubContractList : RadForm
    {
        private static frm_Export_EmployeeSubContractList s_Instance;
        private readonly RadWaitingBar rwb = new RadWaitingBar();

        public frm_Export_EmployeeSubContractList()
        {
            InitializeComponent();
        }

        public static frm_Export_EmployeeSubContractList Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Export_EmployeeSubContractList();
                return s_Instance;
            }
        }

        private void frm_Export_EmployeeSubContractList_Load(object sender, EventArgs e)
        {
            FormClosed += Frm_Export_EmployeeContractList_FormClosed;
            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            radGridView1.FilterChanged += RadGridView1_FilterChanged;

            BindData();

            radLabelElement1.Text = "Tổng số nhân viên: " + radGridView1.ChildRows.Count;
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void BindData()
        {
            radGridView1.DataSource = EmployeeSubContractBLL.GetAll();
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
            if (saveFileDialogExportExcel.ShowDialog() == DialogResult.OK)
                MessageBox.Show("OK!");
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
    }
}