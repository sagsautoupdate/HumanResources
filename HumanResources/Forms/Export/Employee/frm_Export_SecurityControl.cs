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
    public partial class frm_Export_SecurityControl : RadForm
    {
        private static frm_Export_SecurityControl s_Instance;
        private readonly RadWaitingBar rwb = new RadWaitingBar();

        public frm_Export_SecurityControl()
        {
            InitializeComponent();
        }

        public static frm_Export_SecurityControl Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Export_SecurityControl();
                return s_Instance;
            }
        }

        private void frm_Export_SecurityControl_Load(object sender, EventArgs e)
        {
            Utilities.Utilities.GridFormatting(KSAN);
            KSAN.DataSource = SecurityControlBLL.GetAllForExport();

            FormClosed += Frm_Export_SecurityControl_FormClosed;
            KSAN.ContextMenuOpening += KSAN_ContextMenuOpening;
        }

        private void KSAN_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            var customMenuItem = new RadMenuItem();
            customMenuItem.Text = "Xuất Excel";
            customMenuItem.Image = Resources.ExportToXLSX_16x16;
            customMenuItem.Click += RmiExcel_Click;
            var separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);
        }

        private void RmiExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!backgroundWorker1.IsBusy)
                {
                    var fullName = string.Empty;
                    backgroundWorker1.RunWorkerAsync(fullName);

                    Utilities.Utilities.ShowWaiting(rwb, KSAN);
                }
            }
            catch
            {
                MessageBox.Show("Lỗi trích xuất dữ liệu!");
            }
        }

        private void Frm_Export_SecurityControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker) sender;
            for (var i = 1; i <= 5; ++i)
            {
                worker.ReportProgress(i);


                Thread.Sleep(100);
            }
            var fullName = (string) e.Argument;
            e.Result = runExport(fullName);
        }

        private _Application runExport(string fullName)
        {
            return null;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
                Utilities.Utilities.SetWaitingText(rwb, e.ProgressPercentage + "%");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Utilities.Utilities.StopWaiting(rwb, KSAN);
        }
    }
}