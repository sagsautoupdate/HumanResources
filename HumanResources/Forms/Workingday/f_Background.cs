using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using HRMBLL.H1;
using HumanResources.Forms.Coefficient;

namespace HumanResources.Forms.Workingday
{
    public partial class f_Background : Form
    {
        private readonly DateTime _datetime;
        private readonly DataTable _dt = new DataTable();
        private frm_Apply_LNSEmployeeToWorkdayFinal _fal = new frm_Apply_LNSEmployeeToWorkdayFinal();

        public f_Background()
        {
            InitializeComponent();
        }

        public f_Background(DataTable dt, frm_Apply_LNSEmployeeToWorkdayFinal fal, DateTime datetime)
        {
            InitializeComponent();

            _dt = dt;
            _fal = fal;
            _datetime = datetime;
        }

        private void f_Background_Load(object sender, EventArgs e)
        {
            try
            {
                if (!backgroundWorker1.IsBusy)
                {
                    var fullName = string.Empty;
                    radProgressBar1.Maximum = _dt.Rows.Count;
                    backgroundWorker1.RunWorkerAsync(fullName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var fullName = (string) e.Argument;
            Run(fullName);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
                while (e.ProgressPercentage <= _dt.Rows.Count)
                {
                    radProgressBar1.Value1 = e.ProgressPercentage;
                    radProgressBar1.Text = "Đang nạp dữ liệu: " + e.ProgressPercentage + "/" + _dt.Rows.Count;
                }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            radProgressBar1.Text = "Nạp dữ liệu thành công";
        }

        private void Run(string fullname)
        {
            var i = 0;
            foreach (DataRow dr in _dt.Rows)
            {
                var _HSLNS = dr["ActualValue"] == DBNull.Value
                    ? 0
                    : Convert.ToDouble(dr["ActualValue"]);
                var _UserId = Convert.ToInt32(dr["UserId"]);
                var Id = WorkdayCoefficientEmployeesFinalBLL.UpdateHSLNS(_datetime, _HSLNS, _UserId);
                backgroundWorker1.ReportProgress(i);
                i++;
            }
        }
    }
}