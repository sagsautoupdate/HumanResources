using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HumanResources.Progress.ProgressTest
{
    /// <summary>
    ///     Simple progress form.
    /// </summary>
    public partial class ProgressForm : Form
    {
        /// <summary>
        ///     Delegate for the DoWork event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Contains the event data.</param>
        public delegate void DoWorkEventHandler(ProgressForm sender, DoWorkEventArgs e);

        private int lastPercent;
        private string lastStatus;

        private readonly BackgroundWorker worker;

        /// <summary>
        ///     Constructor.
        /// </summary>
        public ProgressForm()
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

        /// <summary>
        ///     Gets the progress bar so it is possible to customize it
        ///     before displaying the form.
        ///     Do not use it directly from the background worker function!
        /// </summary>
        public ProgressBar ProgressBar { get; private set; }

        /// <summary>
        ///     Will be passed to the background worker.
        /// </summary>
        public object Argument { get; set; }

        /// <summary>
        ///     Background worker's result.
        ///     You may also check ShowDialog return value
        ///     to know how the background worker finished.
        /// </summary>
        public RunWorkerCompletedEventArgs Result { get; private set; }

        /// <summary>
        ///     True if the user clicked the Cancel button
        ///     and the background worker is still running.
        /// </summary>
        public bool CancellationPending
        {
            get { return worker.CancellationPending; }
        }

        /// <summary>
        ///     Text displayed once the Cancel button is clicked.
        /// </summary>
        public string CancellingText { get; set; }

        /// <summary>
        ///     Default status text.
        /// </summary>
        public string DefaultStatusText { get; set; }

        /// <summary>
        ///     Occurs when the background worker starts.
        /// </summary>
        public event DoWorkEventHandler DoWork;

        /// <summary>
        ///     Changes the status text only.
        /// </summary>
        /// <param name="status">New status text.</param>
        public void SetProgress(string status)
        {
            if ((status != lastStatus) && !worker.CancellationPending)
            {
                lastStatus = status;
                worker.ReportProgress(ProgressBar.Minimum - 1, status);
            }
        }

        /// <summary>
        ///     Changes the progress bar value only.
        /// </summary>
        /// <param name="percent">New value for the progress bar.</param>
        public void SetProgress(int percent)
        {
            if (percent != lastPercent)
            {
                lastPercent = percent;
                worker.ReportProgress(percent);
            }
        }

        /// <summary>
        ///     Changes both progress bar value and status text.
        /// </summary>
        /// <param name="percent">New value for the progress bar.</param>
        /// <param name="status">New status text.</param>
        public void SetProgress(int percent, string status)
        {
            if ((percent != lastPercent) || ((status != lastStatus) && !worker.CancellationPending))
            {
                lastPercent = percent;
                lastStatus = status;
                worker.ReportProgress(percent, status);
            }
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            Result = null;
            buttonCancel.Enabled = true;
            ProgressBar.Value = ProgressBar.Minimum;
            labelStatus.Text = DefaultStatusText;
            lastStatus = DefaultStatusText;
            lastPercent = ProgressBar.Minimum;

            worker.RunWorkerAsync(Argument);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();

            buttonCancel.Enabled = false;
            labelStatus.Text = CancellingText;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (DoWork != null)
                DoWork(this, e);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if ((e.ProgressPercentage >= ProgressBar.Minimum) && (e.ProgressPercentage <= ProgressBar.Maximum))
                ProgressBar.Value = e.ProgressPercentage;
            if ((e.UserState != null) && !worker.CancellationPending)
                labelStatus.Text = e.UserState.ToString();
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