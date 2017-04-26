using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HumanResources.Utilities
{
    public class LongRunningTask
    {
        public Thread LongRunningTaskThread;

        public event EventHandler Complete;

        public void CreateFonts()
        {
            for (var i = 0; i < 400000; i++)
            {
                var f = new Font("Arial", 12.0f, FontStyle.Strikeout);
            }


            if (Complete.Target is Control)
            {
                var target = Complete.Target as Control;
                target.BeginInvoke(Complete, new object[] {});
            }
            else
            {
                Complete(this, EventArgs.Empty);
            }
        }

        public void StartLongRunningTask()
        {
            LongRunningTaskThread = new Thread(CreateFonts);
            LongRunningTaskThread.Start();
        }
    }
}