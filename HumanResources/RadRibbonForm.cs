using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using HRMBLL.H0;
using HumanResources.Administrations;
using HumanResources.Forms.Bonus;
using HumanResources.Forms.Catalogue;
using HumanResources.Forms.Coefficient;
using HumanResources.Forms.Contract.Contract;
using HumanResources.Forms.Contract.SubContract;
using HumanResources.Forms.Employees;
using HumanResources.Forms.Export;
using HumanResources.Forms.Export.Employee;
using HumanResources.Forms.Export.Workingday;
using HumanResources.Forms.Leave;
using HumanResources.Forms.Recruitment.Candidate;
using HumanResources.Forms.Recruitment.Candidate.PreEmployee;
using HumanResources.Forms.Recruitment.Education_Fee;
using HumanResources.Forms.Regulation;
using HumanResources.Forms.Salary;
using HumanResources.Forms.Statistic;
using HumanResources.Forms.Workingday;
using HumanResources.Properties;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using StringFormat = HRMUtil.StringFormat;
using Telerik.WinControls.Primitives;

namespace HumanResources
{
    public partial class RadRibbonForm : Telerik.WinControls.UI.RadRibbonForm
    {
        private List<Control> _ControlList;
        private DataTable list;
        private bool m_bLayoutCalled;
        private DateTime m_dt;


        private long sizeOfUpdate;

        public RadRibbonForm()
        {
            InitializeComponent();
            RefreshAll();
        }

        public void RefreshAll()
        {
            _ControlList = new List<Control>();
            list = EmployeesBLL.GetByDeptIdsToDT(string.Empty, 0, string.Empty, "0");

            var _lstRights = Utilities.Utilities.GetUserRights();
            if (clsGlobal.IsAdmin || clsGlobal.IsHRAdmin)
            {
                IsActiveAll(true);
            }
            else
            {
                if (_lstRights.Count <= 0)
                {
                    IsActiveAll(false);
                }
                else
                {
                    IsActiveAll(false);
                    foreach (var item in _lstRights)
                    {
                        var dt = UserRolesBLL.GetWinForm(item);
                        if (dt.Rows.Count > 0)
                            foreach (var right in dt.Rows[0]["FormName"].ToString().Split(';'))
                                if (right != string.Empty)
                                    IsActiveOne(right);
                    }
                }
            }

            Init();
            LoadData();
            Layout += RadRibbonForm_Layout;


            if (!backgroundWorker1.IsBusy)
            {
                var fullName = string.Empty;
                radProgressBarElement1.Maximum = list.Rows.Count;
                backgroundWorker1.RunWorkerAsync(fullName);
            }
        }

        private void IsActiveAll(bool flag)
        {
            for (var i = 0; i < rRBMain.CommandTabs.Count; i++)
            {
                var ribbonTab = rRBMain.CommandTabs[i] as RibbonTab;

                for (var j = 0; j < ribbonTab.Items.Count; j++)
                {
                    var ribbonBarGroup = ribbonTab.Items[j] as RadRibbonBarGroup;

                    for (var k = 0; k < ribbonBarGroup.Items.Count; k++)
                    {
                        var currentGroup = ribbonBarGroup.Items[k];

                        if (currentGroup != null)
                        {
                            var buttonGroup = currentGroup as RadRibbonBarButtonGroup;
                            if (buttonGroup == null)
                            {
                            }
                            else
                            {
                                buttonGroup.ShowBorder = false;
                                for (var l = 0; l < buttonGroup.Items.Count; l++)
                                {
                                    var currentItem = buttonGroup.Items[l];
                                    if (currentItem != null)
                                        currentItem.Enabled = flag;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void IsActiveOne(string name)
        {
            for (var i = 0; i < rRBMain.CommandTabs.Count; i++)
            {
                var ribbonTab = rRBMain.CommandTabs[i] as RibbonTab;

                for (var j = 0; j < ribbonTab.Items.Count; j++)
                {
                    var ribbonBarGroup = ribbonTab.Items[j] as RadRibbonBarGroup;

                    for (var k = 0; k < ribbonBarGroup.Items.Count; k++)
                    {
                        var currentGroup = ribbonBarGroup.Items[k];

                        if (currentGroup != null)
                        {
                            var buttonGroup = currentGroup as RadRibbonBarButtonGroup;

                            if (buttonGroup == null)
                            {
                            }
                            else
                            {
                                buttonGroup.ShowBorder = false;
                                for (var l = 0; l < buttonGroup.Items.Count; l++)
                                {
                                    var currentItem = buttonGroup.Items[l];
                                    if (currentItem != null)
                                        if (currentItem.Name == name)
                                            currentItem.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }


        private void rBEListWageFund_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_ListWageFund.Instance);
        }

        private void rBEListSalary_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_ListIncomeEmployees.Instance);
        }

        private void rbeSalaryCalculate_Click(object sender, EventArgs e)
        {
            var dlg = new frm_CalculateSalary();
            dlg.ShowDialog(this);
        }

        private void rBExportSalary_Click(object sender, EventArgs e)
        {
            var dlg = new frm_ExportSalary();
            dlg.ShowDialog(this);
        }

        private void rBEImportSalary_Click(object sender, EventArgs e)
        {
            var dlg = new frm_ImportSalary();
            dlg.ShowDialog(this);
        }

        private void rbeBonusUnit_Click(object sender, EventArgs e)
        {
        }

        private void rbeImportBonus_Click(object sender, EventArgs e)
        {
            var dlg = new frm_ImportBonus();
            dlg.ShowDialog(this);
        }

        private void rBEImportWorkingday_Click(object sender, EventArgs e)
        {
            var dlg = new frm_ImportWorkingday();
            dlg.ShowDialog(this);
        }

        private void btnBonusTable_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Bonus.Instance);
        }

        private void btnExportBonus_Click(object sender, EventArgs e)
        {
            var dlg = new frm_ExportBonus();
            dlg.ShowDialog(this);
        }

        private void btnExportWorkingday_Click(object sender, EventArgs e)
        {
            var dlg = new frm_ExportWorkingday();
            dlg.ShowDialog(this);
        }

        private void radButtonElement4_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_ContractType.Instance);
        }

        private void btnLNSCoefficient_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_LNSCoefficient.Instance);
        }

        private void btnCoefficient_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_LNSEmployeeCoefficient.Instance);
        }

        private void btnLNSDifferences_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Apply_LNSEmployeeToWorkdayFinal.Instance);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var fullName = (string) e.Argument;
            LoadEmployeeImage(fullName);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
            {
                radProgressBarElement1.Value1 = e.ProgressPercentage;
                radProgressBarElement1.Text = "Downloaded: " + e.ProgressPercentage + "/" + list.Rows.Count;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            radProgressBarElement1.Text = "Download completed";
        }

        private void radButtonElement3_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Export_SecurityControl.Instance);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Utilities.Utilities.DisposeApplication();
        }

        private void RadRibbonForm_Layout(object sender, LayoutEventArgs e)
        {
            if (m_bLayoutCalled == false)
            {
                m_bLayoutCalled = true;
                m_dt = DateTime.Now;
                Activate();
            }
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Leave_Management.Instance);
        }

        private void rBEParty_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Party_Management.Instance);
        }

        private void rBEScaleOfSalary_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_ScaleOfSalary.Instance);
        }

        private void rBELastRound_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_CandidateListFinalRound.Instance);
        }

        private void rbeSubContract_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_List_SubContract.Instance);
        }

        private void rbeChangedLogDetail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(ChangedLog.Instance);
        }

        private void rbeContractList_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_List_Contract.Instance);
        }

        private void rbeEducationFee_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_EducationFee.Instance);
        }

        private void rbeExportEmployeeList_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Export_EmployeeContractList.Instance);
        }

        private void rbeExportEmployeeSubContractList_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Export_EmployeeSubContractList.Instance);
        }

        private void rbeWorkingdayFinal_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_WorkingdayFinal.Instance);
        }

        private void rbePositions_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Positions.Instance);
        }

        private void rbeFamily_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Family.Instance);
        }

        private void rbeWorking_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Employee_Working.Instance);
        }

        private void rMIServer_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            if (radDock1.HasChildren)
            {
                radDock1.CloseAllWindows();
                Thread.Sleep(200);
            }
            Thread.Sleep(100);
            Hide();
            var f_Login =
                Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType("HumanResources.Login.frm_Login")) as
                    Form;

            Thread.Sleep(100);

            Cursor.Current = Cursors.Default;
            f_Login.ShowDialog();
        }

        public void UnHide()
        {
            Show();
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Export_Employees.Instance);
        }

        private void rbeUpdate_Click(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var ad = ApplicationDeployment.CurrentDeployment;
                ad.CheckForUpdateCompleted += ad_CheckForUpdateCompleted;
                ad.CheckForUpdateProgressChanged += ad_CheckForUpdateProgressChanged;

                ad.CheckForUpdateAsync();
            }
        }

        private void ad_CheckForUpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            if (!radProgressBarElement1.IsFocused)
                radProgressBarElement1.Text = string.Format("Downloading: {0}. {1:D}K of {2:D}K downloaded.",
                    GetProgressString(e.State), e.BytesCompleted/1024, e.BytesTotal/1024);
        }

        private string GetProgressString(DeploymentProgressState state)
        {
            if (state == DeploymentProgressState.DownloadingApplicationFiles)
                return "Application files";
            if (state == DeploymentProgressState.DownloadingApplicationInformation)
                return "Application manifest";
            return "Deployment manifest";
        }

        private void ad_CheckForUpdateCompleted(object sender, CheckForUpdateCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("ERROR: Could not retrieve new version of the application. Reason: \n" + e.Error.Message +
                                "\nPlease report this error to the system administrator.");
                return;
            }
            if (e.Cancelled)
                MessageBox.Show("The update was cancelled.");
            if (e.UpdateAvailable)
            {
                sizeOfUpdate = e.UpdateSizeBytes;

                BeginUpdate();
            }
        }

        private void BeginUpdate()
        {
            var ad = ApplicationDeployment.CurrentDeployment;
            ad.UpdateCompleted += ad_UpdateCompleted;


            ad.UpdateProgressChanged += ad_UpdateProgressChanged;
            ad.UpdateAsync();
        }

        private void ad_UpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            var progressText = string.Format("{0:D}K out of {1:D}K downloaded - {2:D}% complete", e.BytesCompleted/1024,
                e.BytesTotal/1024, e.ProgressPercentage);
            if (!radProgressBarElement1.IsFocused)
                radProgressBarElement1.Text = progressText;
        }

        private void ad_UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("The update of the application's latest version was cancelled.");
                return;
            }
            if (e.Error != null)
            {
                MessageBox.Show("ERROR: Could not install the latest version of the application. Reason: \n" +
                                e.Error.Message + "\nPlease report this error to the system administrator.");
                return;
            }

            var dr =
                MessageBox.Show(
                    "The application has been updated. Restart? (If you do not restart now, the new version will not take effect until after you quit and launch the application again.)",
                    "Restart Application", MessageBoxButtons.OKCancel);
            if (DialogResult.OK == dr)
                Application.Restart();
        }


        private void rbeAbout_Click(object sender, EventArgs e)
        {
        }

        private void rbeDepartment_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Department.Instance);
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Export_Workingday.Instance);
        }

        private void rbeSecurityControl_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_SecurityControl.Instance);
        }

        private void rbeCandidateToEmployee_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Pre_Employee.Instance);
        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(CustomExport.Instance);
        }

        private void btnBonusCalculate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Calculate_Bonus.Instance);
        }


        private void GetAllControls(Control container)
        {
            foreach (Control c in container.Controls)
            {
                GetAllControls(c);
                if (c is RadButton)
                    _ControlList.Add(c);
            }
        }

        private void LoadEmployeeImage(string fullname)
        {
            clsGlobal.EmployeeImageList = new List<EmployeeImageList>();
            var DataSource = Utilities.Utilities.GetServerByKeyWithoutDecrypt(clsGlobal.ConnectionString);
            var ImageLink = string.Empty;
            switch (clsGlobal.Server)
            {
                case "Server_SAGS":
                    if (DataSource.Equals("10.10.55.5"))
                        DataSource = "10.10.55.5";
                    ImageLink = string.Format("http://{0}/HRM/Employee/Images/", DataSource);
                    break;
                case "Server_DAD":
                    if (DataSource.Equals("172.16.234.5"))
                        DataSource = "172.16.234.5";
                    ImageLink = string.Format("http://{0}/HRM/Employee/Images/", DataSource);
                    break;
                case "Server_CXR":
                    if (DataSource.Equals("172.16.112.5"))
                        DataSource = "172.16.112.8";
                    ImageLink = string.Format("http://{0}/HRM/Employee/Images/", DataSource);
                    break;
            }

            list = EmployeesBLL.GetByDeptIdsToDT(string.Empty, 0, string.Empty, "0");
            var i = 0;
            foreach (DataRow item in list.Rows)
            {
                var _ImageName = StringFormat.GetUserCode(int.Parse(item["UserId"].ToString()));
                var _ImageUrl = string.Format("{0}{1}.jpg", ImageLink,
                    StringFormat.GetUserCode(int.Parse(item["UserId"].ToString())));

                try
                {
                    clsGlobal.EmployeeImageList.Add(new EmployeeImageList(_ImageName,
                        new Bitmap(new MemoryStream(new WebClient().DownloadData(_ImageUrl)))));
                }
                catch
                {
                    clsGlobal.EmployeeImageList.Add(new EmployeeImageList(_ImageName, Resources.no_image));
                }
                backgroundWorker1.ReportProgress(i++, string.Empty);
                Thread.Sleep(10);
            }
        }

        private Image DownloadImage(string _URL)
        {
            Image _tmpImage = null;
            try
            {
                var _HttpWebRequest = (HttpWebRequest) WebRequest.Create(_URL);
                _HttpWebRequest.AllowWriteStreamBuffering = true;

                _HttpWebRequest.Timeout = 20000;

                var _WebResponse = _HttpWebRequest.GetResponse();

                var _WebStream = _WebResponse.GetResponseStream();

                _tmpImage = Image.FromStream(_WebStream);

                _WebResponse.Close();
                _WebResponse.Close();
            }
            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}", _Exception);
                return null;
            }
            return _tmpImage;
        }

        private void Init()
        {
            rBELog.Image = Resources.Login_out;
            rBELog.Text = "Logout";

            rMILog.Text = "Đăng xuất";
        }

        public void LoadData()
        {
            rLEFullName.Text = string.Format("Người dùng: {0} ({1})", clsGlobal.FullName.ToUpper(), clsGlobal.UserName);
            rLEDepartmentFullName.Text = Utilities.Utilities.GetDepartmentFullName(clsGlobal.DepartmentFullName,
                clsGlobal.DepartmentLevel);
            rLEServer.Text = clsGlobal.Server;
        }

        public void DisplayForm(Form form)
        {
            try
            {
                if (Contains(form))
                {
                    var host = (HostWindow) form.Parent;
                    radDock1.ActivateWindow(host);
                }
                else
                {
                    form.MdiParent = this;
                    form.Show();
                    var host = (HostWindow) form.Parent;
                    radDock1.ActivateWindow(host);
                }
                form.Focus();
                try
                {
                    Cursor.Current = Cursors.Default;
                }
                catch
                {
                }
            }
            catch
            {
            }
        }

        private void DisplayVersion()
        {
            var version = string.Format("assembly: {0}",
                ((AssemblyFileVersionAttribute)
                        Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)
                            [0])
                    .Version);
            var appPath = Assembly.GetExecutingAssembly().Location;
            var winPath = Environment.GetEnvironmentVariable("WINDIR");

            var FileName = winPath + @"\Microsoft.NET\Framework\v2.0.50727\ngen.exe";
            var Arguments = string.Format("uninstall {0} /nologo /silent", Application.ProductName);
            var iconSourcePath = Path.Combine(Assembly.GetExecutingAssembly().Location, "icon-hrm2014.ico");
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var ad = ApplicationDeployment.CurrentDeployment;
                version = string.Format("ClickOnce: {0}", ad.CurrentVersion);
                MessageBox.Show(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{5}", version, appPath, winPath,
                    FileName, Arguments, iconSourcePath));
            }
        }


        private void btnStatistic_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            DisplayForm(frm_Statistic.Instance);
        }

        private void radDock1_ActiveWindowChanged(object sender, DockWindowEventArgs e)
        {
            //if (e.DockWindow != null)
                //SizeAndColorSelectedDocWindowTab((FillPrimitive)e.DockWindow.TabStripItem.Children[0], Color.FromArgb(161, 211, 244), Color.FromArgb(161, 211, 244));
        }

        private void radDock1_ActiveWindowChanging(object sender, DockWindowCancelEventArgs e)
        {
            //SizeAndColorUnSelectedDocWindowTab((FillPrimitive)e.OldWindow.TabStripItem.Children[0], Color.White, Color.White);
        }

        private void SizeAndColorSelectedDocWindowTab(FillPrimitive fill, Color lightColour, Color darkColour)
        {
            fill.BackColor = lightColour;
            fill.BackColor2 = lightColour;
            fill.BackColor3 = lightColour;
            fill.BackColor4 = lightColour;
        }

        private void SizeAndColorUnSelectedDocWindowTab(FillPrimitive fill, Color lightColour, Color darkColour)
        {
            fill.BackColor = darkColour;
            fill.BackColor2 = darkColour;
            fill.BackColor3 = darkColour;
            fill.BackColor4 = darkColour;
        }
    }
}