using System;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace HumanResources.Login
{
    public partial class frm_Login : RadForm
    {
        public frm_Login()
        {
            InitializeComponent();
            TopMost = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            LoadData();
            FormElement.TitleBar.Visibility = ElementVisibility.Collapsed;
            Focus();
            txtUserName.Select();
            try
            {
                SplashScreenManager.CloseForm();
            }
            catch
            {
            }
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rBLogin_Click(this, e);
            if (e.KeyCode == Keys.Escape)
                rBExit_Click(this, e);
        }

        private void LoadData()
        {
        }

        private void rBExit_Click(object sender, EventArgs e)
        {
            clsGlobal.IsLoggedIn = false;
            Utilities.Utilities.DisposeApplication();
        }

        private void rBLogin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            Hide();
            var _fds = new frm_DataServer(this, txtUserName.Text.Trim(), txtPassword.Text.Trim());
            _fds.ShowDialog();
            Cursor.Current = Cursors.Default;


            if (Application.OpenForms["RadRibbonForm"] != null)
            {
                (Application.OpenForms["RadRibbonForm"] as RadRibbonForm).LoadData();
                (Application.OpenForms["RadRibbonForm"] as RadRibbonForm).RefreshAll();
            }
        }
    }
}