using System;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace HumanResources.Progress
{
    public partial class frm_SplashScreen1 : SplashScreen
    {
        public enum SplashScreenCommand
        {
        }

        private static frm_SplashScreen1 s_Instance;

        public frm_SplashScreen1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
        }

        public static frm_SplashScreen1 Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_SplashScreen1();
                return s_Instance;
            }
        }


        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }
    }
}