using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Locations
{
    public partial class sf_Background : RadForm
    {
        public delegate void DoWorkEventHandler(sf_Background sender, DoWorkEventArgs e);

        private int lastPercent;
        private string lastStatus;
        private int max;
        private int min;
        private readonly BackgroundWorker worker;

        public sf_Background(string label)
        {
            InitializeComponent();

            DefaultStatusText = "Please wait...";
            CancellingText = "Cancelling operation...";

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            label1.Text = label;
        }

        public sf_Background()
        {
            InitializeComponent();

            DefaultStatusText = "Please wait...";
            CancellingText = "Cancelling operation...";

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        public object Argument { get; set; }
        public RunWorkerCompletedEventArgs Result { get; private set; }

        public bool CancellationPending
        {
            get { return worker.CancellationPending; }
        }

        public string CancellingText { get; set; }
        public string DefaultStatusText { get; set; }
        public event DoWorkEventHandler DoWork;

        private void sf_Background_Load(object sender, EventArgs e)
        {
            Result = null;


            lastStatus = DefaultStatusText;


            worker.RunWorkerAsync(Argument);
        }

        public void SetProgress(string status)
        {
            if ((status != lastStatus) && !worker.CancellationPending)
            {
                lastStatus = status;
                worker.ReportProgress(min - 1, status);
            }
        }

        public void SetMaximum(int value)
        {
            if ((value > 0) && !worker.CancellationPending)
                max = value;
        }

        public void SetMinimum(int value)
        {
            if ((value > 0) && !worker.CancellationPending)
                min = value;
        }


        public void SetProgress(int percent)
        {
            if (percent != lastPercent)
            {
                lastPercent = percent;
                worker.ReportProgress(percent);
            }
        }

        public void SetProgress(int percent, string status)
        {
            if ((percent != lastPercent) || ((status != lastStatus) && !worker.CancellationPending))
            {
                lastPercent = percent;
                lastStatus = status;
                worker.ReportProgress(percent, status);
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (DoWork != null)
                DoWork(this, e);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if ((e.ProgressPercentage >= min) && (e.ProgressPercentage <= max))
                label1.Text = "Updating .... " + e.ProgressPercentage;
            if ((e.UserState != null) && !worker.CancellationPending)
                label1.Text = e.UserState.ToString();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Result = e;
            if (e.Error != null)
            {
                DialogResult = DialogResult.Abort;
            }
            else
            {
                if (e.Cancelled)
                    DialogResult = DialogResult.Cancel;
                else
                    DialogResult = DialogResult.OK;
            }
            Close();
        }
    }
}