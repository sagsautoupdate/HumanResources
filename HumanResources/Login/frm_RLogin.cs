using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraSplashScreen;
using HRMBLL.H0;
using HRMDAL.Utilities;
using HRMUtil;
using HumanResources.Properties;
using HumanResources.Utilities;
using Telerik.WinControls;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.UI;
using AESm;

namespace HumanResources.Login
{
    public partial class frm_RLogin : RadForm
    {
        private readonly BackgroundWorker bgw = new BackgroundWorker();
        private readonly RadButtonElement button = new RadButtonElement();
        private readonly RadWaitingBar rwb = new RadWaitingBar();
        private bool _Accepted;

        public frm_RLogin()
        {
            //Properties.Settings.Default.Reset();
            InitializeComponent();
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;

            AddButton();

            bgw.DoWork += Bgw_DoWork;
            bgw.ProgressChanged += Bgw_ProgressChanged;
            bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;
        }

        private void Blur()
        {
            var bmp = Screenshot.TakeSnapshot(panel1);
            BitmapFilter.GaussianBlur(bmp, 4);

            pictureBox2.Image = bmp;
            pictureBox2.BringToFront();
        }

        private void UnBlur()
        {
            pictureBox2.Image = null;
            pictureBox2.SendToBack();
        }

        private void RLogin_Load(object sender, EventArgs e)
        {
            var _UserName = AES.Decrypt(Settings.Default.UserName);// clsEncDec.Decrypt(Settings.Default.UserName);
            var _Password = AES.Decrypt(Settings.Default.Password);// clsEncDec.Decrypt(Settings.Default.Password);
            var _Server = AES.Decrypt(Settings.Default.Server); //clsEncDec.Decrypt(Settings.Default.Server);

            var emp = EmployeesBLL.LoginNew(_UserName, _Password);
            if (emp != null)
            {
                lblUser.Text = emp["FullName"].ToString().Trim();
                lblInfo.Text = string.Format("{0}", _Server);

                var DataSource = Utilities.Utilities.GetServerByKeyWithoutDecrypt(Settings.Default.DataSource);


                FormElement.TitleBar.Visibility = ElementVisibility.Collapsed;
                Focus();
                txtPassword.Select();
            }
            else
            {
                MessageBox.Show("Lỗi kết nối, chương trình sẽ tự động tắt", "?", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                Settings.Default.Reset();
                Utilities.Utilities.DisposeApplication();
                if (Application.MessageLoop)
                    Application.Exit();
                else
                    Environment.Exit(1);
            }
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
                button_Click(this, e);
            if (e.KeyCode == Keys.Escape)
                rBExit_Click(this, e);
        }

        private void rBExit_Click(object sender, EventArgs e)
        {
            clsGlobal.IsLoggedIn = false;
            Utilities.Utilities.DisposeApplication();
        }

        private void LoginSystem()
        {
            try
            {
                {
                    Cursor.Current = Cursors.AppStarting;
                    if ((txtPassword.Text.Trim() != string.Empty) &&
                        (txtPassword.Text.Trim() == AES.Decrypt(Settings.Default.Password))) //clsEncDec.Decrypt(Settings.Default.Password)))
                    {
                        var _UserName = AES.Decrypt(Settings.Default.UserName);
                        var _Password = AES.Decrypt(Settings.Default.Password);
                        var _Server = AES.Decrypt(Settings.Default.Server);

                        WriteSetting("ConnectionString", Settings.Default.DataSource);

                        var emp = EmployeesBLL.LoginNew(_UserName, _Password);
                        if (emp != null)
                        {
                            clsGlobal.IsLoggedIn = true;
                            clsGlobal.UserId = int.Parse(emp["UserId"].ToString());
                            clsGlobal.UserName = emp["UserName"].ToString();
                            clsGlobal.FullName = emp["FullName"].ToString();
                            try
                            {
                                var drDR = EmployeesBLL.DR_GetEmployeeByUserName(_UserName);
                                clsGlobal.DepartmentFullName = drDR["DepartmentFullName"].ToString();
                                clsGlobal.DepartmentName = drDR["DepartmentName"].ToString();
                                clsGlobal.DepartmentLevel = Convert.ToInt32(drDR["Level"]);
                            }
                            catch
                            {
                            }
                            clsGlobal.ConnectionString = Settings.Default.DataSource;
                            clsGlobal.Server = _Server;
                            switch (_Server)
                            {
                                case "Server_SAGS":
                                    clsGlobal.CompanyName = "Công ty Phục vụ Mặt đất Sài Gòn";
                                    clsGlobal.Representation = "NGUYỄN ĐÌNH HÙNG";
                                    break;
                                case "Server_DAD 1":
                                    clsGlobal.CompanyName = "Công ty Phục vụ Mặt đất Sài Gòn - Chi nhánh Đã Nẵng";
                                    clsGlobal.Representation = "BÙI QUỐC ANH";
                                    break;
                                case "Server_DAD 2":
                                    clsGlobal.CompanyName = "Công ty Phục vụ Mặt đất Sài Gòn - Chi nhánh Đã Nẵng";
                                    clsGlobal.Representation = "BÙI QUỐC ANH";
                                    break;
                                case "Server_CXR":
                                    clsGlobal.CompanyName = "Công ty Phục vụ Mặt đất Sài Gòn - Chi nhánh Cam Ranh";
                                    clsGlobal.Representation = "NGUYỄN THÁI HOÀ";
                                    break;
                                case "Server_LOCAL":
                                    clsGlobal.CompanyName = "Test";
                                    clsGlobal.Representation = "TESTER";
                                    break;
                            }
                        }
                        _Accepted = true;
                        clsGlobal.IsAdmin = Utilities.Utilities.IsAdmin();
                        clsGlobal.IsHRAdmin = Utilities.Utilities.IsHRAdmin();
                    }
                    else
                    {
                        MessageBox.Show("Thông tin đăng nhập không chính xác. Vui lòng đăng nhập lại!", "UserName?",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                            (MessageBoxOptions) 0x40000);
                        return;
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (HRMException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            LoginSystem();
            if (Application.OpenForms["RadRibbonForm"] != null)
                (Application.OpenForms["RadRibbonForm"] as RadRibbonForm).LoadData();
        }

        public static string ReadSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private void WriteSetting(string key, string value)
        {
            var doc = loadConfigDocument();


            var node = doc.SelectSingleNode("//appSettings");

            if (node == null)
                throw new InvalidOperationException("appSettings section not found in config file.");
            try
            {
                var elem = (XmlElement) node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

                if (elem != null)
                {
                    elem.SetAttribute("value", value);
                }

                else
                {
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", value);
                    node.AppendChild(elem);
                }
                doc.Save(getConfigFilePath());
                ConfigurationManager.RefreshSection("appSettings");
                HRMConfig.ConnectionString = value;
            }
            catch
            {
                throw;
            }
        }

        private XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;

            try
            {
                doc = new XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }

        private string getConfigFilePath()
        {
            return Assembly.GetExecutingAssembly().Location + ".config";
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private void AddButton()
        {
            button.Click += button_Click;
            button.Padding = new Padding(0, 0, 2, -2);
            button.Margin = new Padding(0, 0, 0, 0);
            button.DisplayStyle = DisplayStyle.Image;
            button.Image = Resources.LeftRight_32x32;
            button.ImageAlignment = ContentAlignment.MiddleCenter;
            button.AutoSize = false;
            button.Size = new Size(35 + 10, txtPassword.Size.Height);
            button.ShouldApplyTheme = true;

            var stackPanel = new StackLayoutElement
            {
                Orientation = Orientation.Horizontal,
                Margin = new Padding(0, 0, 1, 0)
            };
            stackPanel.Children.Add(button);

            var tbItem = txtPassword.TextBoxElement.TextBoxItem;
            txtPassword.TextBoxElement.Children.Remove(tbItem);
            var dockPanel = new DockLayoutPanel();
            dockPanel.Children.Add(stackPanel);
            dockPanel.Children.Add(tbItem);
            DockLayoutPanel.SetDock(tbItem, Telerik.WinControls.Layouts.Dock.Left);
            DockLayoutPanel.SetDock(stackPanel, Telerik.WinControls.Layouts.Dock.Right);
            txtPassword.TextBoxElement.Children.Add(dockPanel);
        }

        private void txtPassword_TextChanging(object sender, TextChangingEventArgs e)
        {
            var _Password = AES.Decrypt(Settings.Default.Password);
            var _PasswordCount = _Password.Length;

            var tb = (RadTextBox) sender;
            var textCount = tb.Text.Length;
            if (textCount == _PasswordCount)
                try
                {
                    if (!bgw.IsBusy)
                    {
                        bgw.RunWorkerAsync(string.Empty);
                        Utilities.Utilities.ShowWaiting(rwb, txtPassword);
                    }
                }
                catch
                {
                }
        }

        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_Accepted)
            {
                Utilities.Utilities.StopWaiting(rwb, txtPassword);
                Close();
                Dispose();
            }
            else
            {
                Utilities.Utilities.StopWaiting(rwb, txtPassword);
            }
        }

        private void Bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!bgw.CancellationPending)
            {
            }
        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            button_Click(this, e);
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            button_Click(this, e);
        }
    }
}