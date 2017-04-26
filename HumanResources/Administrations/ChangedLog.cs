using System;
using Telerik.WinControls.UI;

namespace HumanResources.Administrations
{
    public partial class ChangedLog : RadForm
    {
        private static ChangedLog s_Instance;

        public ChangedLog()
        {
            InitializeComponent();
        }

        public static ChangedLog Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new ChangedLog();
                return s_Instance;
            }
        }

        private void ChangedLog_Load(object sender, EventArgs e)
        {
        }
    }
}