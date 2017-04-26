using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using AESm;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMDAL.Utilities;
using HRMUtil;
using HumanResources.Progress;
using HumanResources.Properties;
using Telerik.WinControls.UI;

namespace HumanResources.Login
{
    public partial class frm_DataServer : RadForm
    {
        private frm_Login _frmLogin;
        private frm_RLogin _frmRLogin;
        private readonly string _password;
        private readonly string _userName;
        public frm_DataServer()
        {
            InitializeComponent();
        }

        public frm_DataServer(frm_Login frmLogin, string userName, string password)
        {
            InitializeComponent();

            _frmLogin = frmLogin;
            _userName = userName;
            _password = password;
        }

        public frm_DataServer(frm_RLogin frmRLogin, string userName, string password)
        {
            InitializeComponent();

            _frmRLogin = frmRLogin;
            _userName = userName;
            _password = password;
        }

        private void LoginSystem()
        {
            try
            {
                if (rDDLConn.SelectedValue != null)
                {
                    WriteSetting("ConnectionString",
                        Utilities.Utilities.GetValueByKey(rDDLConn.SelectedValue.ToString()));

                    if (_userName == string.Empty)
                    {
                        MessageBox.Show("Vui lòng nhập tên đăng nhập.", "UserName ?", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);

                        return;
                    }
                    var emp = EmployeesBLL.LoginNew(_userName, _password);
                    if (emp != null)
                    {
                        clsGlobal.IsLoggedIn = true;
                        clsGlobal.UserId = int.Parse(emp["UserId"].ToString());
                        clsGlobal.UserName = emp["UserName"].ToString();
                        clsGlobal.FullName = emp["FullName"].ToString();
                        try
                        {
                            var drDR = EmployeesBLL.DR_GetEmployeeByUserName(_userName);
                            clsGlobal.DepartmentFullName = drDR["DepartmentFullName"].ToString();
                            clsGlobal.DepartmentName = drDR["DepartmentName"].ToString();
                            clsGlobal.DepartmentLevel = Convert.ToInt32(drDR["Level"]);
                        }
                        catch
                        {
                        }
                        clsGlobal.ConnectionString = Utilities.Utilities.GetValueByKey(rDDLConn.SelectedValue.ToString());
                        clsGlobal.Server = rDDLConn.SelectedValue.ToString();
                        switch (rDDLConn.SelectedValue.ToString())
                        {
                            case "Server_SAGS":
                                clsGlobal.CompanyName = "Công ty Phục vụ Mặt đất Sài Gòn";
                                clsGlobal.Representation = "NGUYỄN ĐÌNH HÙNG";
                                break;
                            case "Server_DAD":
                                clsGlobal.CompanyName = "Công ty Phục vụ Mặt đất Sài Gòn - Chi nhánh Đã Nẵng";
                                clsGlobal.Representation = "BÙI QUỐC ANH";
                                break;


                            case "Server_CXR":
                                clsGlobal.CompanyName = "Công ty Phục vụ Mặt đất Sài Gòn - Cam Ranh";
                                clsGlobal.Representation = "NGUYỄN THÁI HOÀ";
                                break;
                            case "Server_LOCAL":
                                clsGlobal.CompanyName = "Test";
                                clsGlobal.Representation = "TESTER";
                                break;
                        }
                        clsGlobal.IsAdmin = Utilities.Utilities.IsAdmin();
                        clsGlobal.IsHRAdmin = Utilities.Utilities.IsHRAdmin();
                    }
                    else
                    {
                        MessageBox.Show("Thông tin đăng nhập không chính xác. Vui lòng đăng nhập lại!", "UserName?",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return;
                    }

                    Settings.Default.UserName = AES.Encrypt(_userName);
                    Settings.Default.Password = AES.Encrypt(_password);
                    Settings.Default.Server = AES.Encrypt(rDDLConn.SelectedValue.ToString());
                    Settings.Default.DataSource = Utilities.Utilities.GetValueByKey(rDDLConn.SelectedValue.ToString());
                    Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("Lỗi thông tin. Vui lòng đăng nhập lại!", "UserName?", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                    return;
                }
            }
            catch (HRMException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
            Dispose();
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

        private void radButton3_Click(object sender, EventArgs e)
        {
            clsGlobal.IsLoggedIn = false;
            Utilities.Utilities.DisposeApplication();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            LoginSystem();
            if (Application.OpenForms["RadRibbonForm"] != null)
                (Application.OpenForms["RadRibbonForm"] as RadRibbonForm).UnHide();
        }

        private void frm_DataServer_Load(object sender, EventArgs e)
        {
            rDDLConn.DataSource = HumanResources.Utilities.Utilities.GetCouldBeConnectedServerName(_userName.Trim(), _password.Trim());
        }

        private void StartForm(sf_Background form)
        {
            var result = form.ShowDialog();
            if (result == DialogResult.Cancel)
                MessageBox.Show("Operation has been cancelled");
            if (result == DialogResult.Abort)
                MessageBox.Show("Exception:" + Environment.NewLine + form.Result.Error.Message);
        }
    }
}