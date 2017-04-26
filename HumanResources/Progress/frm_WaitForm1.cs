using System;
using DevExpress.XtraWaitForm;

namespace HumanResources.Progress
{
    public partial class frm_WaitForm1 : WaitForm
    {
        public enum WaitFormCommand
        {
        }

        public frm_WaitForm1()
        {
            InitializeComponent();
            progressPanel1.AutoHeight = true;
        }


        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            progressPanel1.Caption = caption;
        }

        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            progressPanel1.Description = description;
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }
    }
}