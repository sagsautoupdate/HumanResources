using System;
using System.ComponentModel;
using System.Threading;
using HumanResources.Properties;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Workingday.AnimatedProgressIndicator
{
    public partial class frm_AnimatedProgressIndicator : RadForm
    {
        private bool simulateError;

        public frm_AnimatedProgressIndicator()
        {
            InitializeComponent();

            pictureBox.Image = Resources.Information;
        }

        private void OnStartClick(object sender, EventArgs e)
        {
            pictureBox.Image = Resources.Animation;

            buttonStart.Enabled = false;
            buttonCancel.Enabled = true;
            buttonError.Enabled = true;

            backgroundWorker.RunWorkerAsync();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void OnSimulateErrorClick(object sender, EventArgs e)
        {
            simulateError = true;
        }

        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            var rand = new Random();
            for (var i = 0; i < 10; i++)
            {
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                backgroundWorker.ReportProgress(-1, string.Format("Performing step {0}...", i + 1));

                Thread.Sleep(rand.Next(100, 1000));
                if (simulateError)
                {
                    simulateError = false;
                    throw new Exception("Unexpected error!");
                }
            }
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is string)
                labelProgress.Text = (string) e.UserState;
        }

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox.Image = null;

            if (e.Cancelled)
            {
                labelProgress.Text = "Operation cancelled by the user!";
                pictureBox.Image = Resources.Warning;
            }
            else
            {
                if (e.Error != null)
                {
                    labelProgress.Text = "Operation failed: " + e.Error.Message;
                    pictureBox.Image = Resources.Error;
                }
                else
                {
                    labelProgress.Text = "Operation finished successfuly!";
                    pictureBox.Image = Resources.Information;
                }
            }

            buttonStart.Enabled = true;
            buttonCancel.Enabled = false;
            buttonError.Enabled = false;
        }
    }
}