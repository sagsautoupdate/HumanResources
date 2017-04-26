using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using HumanResources.Forms.Workingday.Helper;
using HumanResources.Progress;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Workingday
{
    public partial class frm_CheckSend : RadForm
    {
        private static string _SP = string.Empty;
        private static string _SPValue = string.Empty;
        private readonly frm_WaitForm1 _wf1 = new frm_WaitForm1();
        private frm_WorkingdayFinal _fwf;

        public frm_CheckSend()
        {
            InitializeComponent();
        }

        public frm_CheckSend(frm_WorkingdayFinal fwf, int RootId, DateTime DataDate)
        {
            InitializeComponent();

            _fwf = fwf;
            this.DataDate = DataDate;
            this.RootId = RootId;

            Text = string.Format("Danh sách phòng đã gửi bảng chấm công tháng {0} năm {1}", this.DataDate.Month,
                this.DataDate.Year);
        }

        public DateTime DataDate { get; set; }

        public int RootId { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker) sender;
            PerformComplexComputations(worker, e);
        }

        private void PerformComplexComputations(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                var _departmentIds = string.Empty;
                var _DepIds = string.Empty;
                var obj = new DepartmentsBLL();
                foreach (var row in radGridView1.Rows)
                    if (Convert.ToBoolean(row.Cells["column1"].Value))
                    {
                        obj.GetAllChildId(Convert.ToInt32(row.Cells["DepartmentId"].Value));
                        _departmentIds = obj.ChildNodeIds;

                        _DepIds += _departmentIds + ",";
                    }
                LeaveDay_WorkingDay_Checker_All(DataDate.Month, DataDate.Year, _DepIds.Remove(_DepIds.Length-1));
            }
        }

        public List<DateTime> getSundays()
        {
            var lstSundays = new List<DateTime>();
            var intMonth = DataDate.Month;
            var intYear = DataDate.Year;
            var ci = new CultureInfo("en-US");
            for (var i = 1; i <= ci.Calendar.GetDaysInMonth(intYear, intMonth); i++)
                if (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Sunday)
                    lstSundays.Add(new DateTime(intYear, intMonth, i));
            return lstSundays;
        }

        private List<DateTime> GetDateRange(DateTime StartingDate, DateTime EndingDate)
        {
            if (StartingDate > EndingDate)
                return null;
            var rv = new List<DateTime>();
            var tmpDate = StartingDate;
            do
            {
                rv.Add(tmpDate);
                tmpDate = tmpDate.AddDays(1);
            } while (tmpDate <= EndingDate);
            return rv;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is string)
                _wf1.Invoke(new MethodInvoker(() => { _wf1.SetDescription((string) e.UserState); }));
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                _wf1.Invoke(new MethodInvoker(() =>
                {
                    _wf1.SetDescription("Operation cancelled by the user!");
                    Thread.Sleep(2000);
                }));
            }
            else
            {
                if (e.Error != null)
                    _wf1.Invoke(new MethodInvoker(() =>
                    {
                        _wf1.SetDescription("Operation failed: " + e.Error.Message);
                        Thread.Sleep(2000);
                    }));
                else
                    try
                    {
                        _wf1.Invoke(new MethodInvoker(() => { _wf1.SetDescription("Completed!"); }));

                        _wf1.Invoke(new MethodInvoker(() => { _wf1.Dispose(); }));
                    }
                    finally
                    {
                        MessageBox.Show("Kiểm tra thành công! Chọn thoát để trở lại bảng chấm công.");

                        radButton1.Enabled = true;
                        radButton2.Enabled = true;
                    }
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_CheckSend_Load(object sender, EventArgs e)
        {
            _wf1.BringToFront();
            _wf1.ShowOnTopMode = ShowFormOnTopMode.AboveAll;
            var list = WorkdayEmployeesBLL.CheckSend(clsGlobal.UserId, RootId, DataDate.Month, DataDate.Year);
            radGridView1.DataSource = list;

            radGridView1.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            radGridView1.BestFitColumns(BestFitColumnMode.AllCells);
        }

        private void frm_CheckSend_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["frm_WorkingdayFinal"] != null)
                (Application.OpenForms["frm_WorkingdayFinal"] as frm_WorkingdayFinal).radButton1_Click(null, null);
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                radButton1.Enabled = false;
                radButton2.Enabled = false;

                backgroundWorker1.RunWorkerAsync();
                Task.Factory.StartNew(() => { _wf1.ShowDialog(); });
                Thread.Sleep(1000);
            }
        }

        public void LeaveDay_WorkingDay_Checker_All(int Month, int Year, string DeptIds)
        {
            var DataDate = new DateTime(Year, Month, 1);

            var DTWD = WorkdayCoefficientEmployeesFinalBLL.GetDTByUserIdDataDate(DataDate, Constants.DataType_Run,
                DeptIds);

            for (var i = 0; i < DTWD.Rows.Count; i++)
            {
                var _ERROR = string.Empty;
                var _WhatToSave = string.Empty;
                var DRWD = DTWD.Rows[i];
                {
                    if (DRWD != null)
                    {
                        try
                        {
                            var Status = DRWD["WDStatus"] == DBNull.Value
                                ? 9999
                                : int.Parse(DRWD["WDStatus"].ToString());

                            if ((Status == 2))// || (Status == 3))
                            {
                            }
                            else
                            {
                                var UserId = int.Parse(DRWD["UserId"].ToString());
                                {
                                    var DirectWorking = Convert.ToInt32(EmployeesBLL.DR_GetEmployeeById(UserId)["DirectWorking"]);
                                    var NTInMonth = Checker.getWeekend(Month, Year, DirectWorking).Count;
                                    var LeaveRow = EmployeeLeaveBLL.GetDTByUserId_Date(UserId, Month, Year);

                                    var NTTotal = 0;
                                    try
                                    {
                                        NTTotal = Convert.ToInt32(DRWD["NghiTuan"]);
                                    }
                                    catch{ }

                                    var NTCount = 0;
                                    try
                                    {
                                        NTCount = Checker.CountCompare(DRWD, "NT");
                                    }catch{ }

                                    if ((NTTotal == NTCount) && (NTTotal == NTInMonth) && (NTCount == NTInMonth))
                                    {
                                        _ERROR = Checker.WriteERROR(_ERROR, -1);
                                        _WhatToSave = Checker.WriteStatus(_WhatToSave, "1");
                                    }
                                    else
                                    {
                                        _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_NGHITUAN_NAME));
                                        _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                    }

                                    var DaysInMonth = DateTime.DaysInMonth(Year, Month);
                                    var _flag = true;
                                    for (var ii = 1; ii <= DaysInMonth; ii++)
                                        if ((DRWD["Day" + ii] == DBNull.Value) ||
                                            DRWD["Day" + ii].ToString().Contains("--"))
                                            _flag = false;
                                    if (_flag == false)
                                    {
                                        _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_WORKING_DAYS_NAME));
                                        _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                    }

                                    double EmpLeave_WD = 0;
                                    try
                                    {
                                        EmpLeave_WD = Checker.GetDataByDR(DRWD, "TotalLeave", Month, Year);
                                    }
                                    catch
                                    {
                                    }

                                    var WorkdayCoefficientEmployeeIdFinal =
                                        Convert.ToInt32(DRWD["WorkdayCoefficientEmployeeIdFinal"]);

                                    var X = Convert.ToDouble(DRWD["X"]);

                                    if (Convert.ToInt32(DRWD["NCQD"]).Equals(Convert.ToInt32(DRWD["NCDC"])) == false)
                                    {
                                        _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_NCQD_NCDC_NAME));
                                        _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                    }

                                    if ((X + EmpLeave_WD < Convert.ToInt32(DRWD["NCDC"])) || (X + EmpLeave_WD > Convert.ToInt32(DRWD["NCDC"])))
                                    {
                                        _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_NCQD_NCDC_NAME));
                                        _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                    }

                                    if (LeaveRow.Rows.Count > 0)
                                    {
                                        var Count = 0;
                                        var EmpLeave_Leave = 0d;

                                        try
                                        {
                                            EmpLeave_Leave = Checker.GetTotalLeaveOnLeaveDayTable(UserId, Month, Year);
                                        }
                                        catch
                                        {
                                        }

                                        if (EmpLeave_Leave - EmpLeave_WD == 0)
                                        {
                                            {
                                                foreach (DataRow LeaveDR in LeaveRow.Rows)
                                                    foreach (var date in GetDateRange(DateTime.Parse(LeaveDR["FromDate"].ToString()), DateTime.Parse(LeaveDR["ToDate"].ToString())))
                                                    {
                                                        var LeaveCode = Constants.GetSymbolTimekeeping(int.Parse(LeaveDR["LeaveTypeId"].ToString()));
                                                        if ((LeaveCode != "DD") || (LeaveCode != "CT") ||
                                                            (LeaveCode != "Ko") ||
                                                            (LeaveCode != "Ho") || (LeaveCode != "H1") ||
                                                            (LeaveCode != "H2") ||
                                                            (LeaveCode != "H3") || (LeaveCode != "H4") ||
                                                            (LeaveCode != "H5") ||
                                                            (LeaveCode != "H6") || (LeaveCode != "H7") ||
                                                            (LeaveCode != "DC"))
                                                            if (bool.Parse(EmployeesBLL.GetDataRowEmployeeById(UserId)["Direct"].ToString()))
                                                            {
                                                                if (DRWD["Day" + date.Day].ToString() == LeaveCode)
                                                                    Count++;
                                                                else
                                                                    Count--;
                                                            }
                                                            else
                                                            {
                                                                if ((date.DayOfWeek != DayOfWeek.Sunday) && (date.DayOfWeek != DayOfWeek.Saturday))
                                                                    if (DRWD["Day" + date.Day].ToString() == LeaveCode)
                                                                        Count++;
                                                                    else
                                                                        Count--;
                                                            }
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_LEAVE_LEAVEWD_NAME));
                                            _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                        }
                                        if (Count == EmpLeave_Leave)
                                        {
                                            _ERROR = Checker.WriteERROR(_ERROR, -1);
                                            _WhatToSave = Checker.WriteStatus(_WhatToSave, "1");
                                        }
                                    }
                                    else
                                    {
                                        var EmpLeave_Leave = Checker.GetTotalLeaveOnLeaveDayTable(UserId, Month, Year);
                                        var NCDC = DRWD["NCDC"] == DBNull.Value ? 0 : Convert.ToDouble(DRWD["NCDC"]);

                                        if ((EmpLeave_WD > 0) || (X + EmpLeave_WD != NCDC))
                                        {
                                            _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_LEAVE_LEAVEWD_NAME));
                                            _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                        }
                                        else
                                        {
                                            _ERROR = Checker.WriteERROR(_ERROR, -1);
                                            _WhatToSave = Checker.WriteStatus(_WhatToSave, "1");
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_WORKING_DAYS_NAME));
                        _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                    }
                    var _WorkdayCoefficientEmployeeIdFinal =
                        Convert.ToInt32(DRWD["WorkdayCoefficientEmployeeIdFinal"]);
                    var _UserId = int.Parse(DRWD["UserId"].ToString());

                    if (_WhatToSave.Contains("0"))
                    {
                        _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                        _SPValue =
                            $"UserId: {_UserId}, DataDate: '{DataDate}', WDStatus: {0}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {_WorkdayCoefficientEmployeeIdFinal}";
                        Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue,
                            string.Empty);
                        WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(_UserId,
                            DataDate, 0,
                            _ERROR, _WorkdayCoefficientEmployeeIdFinal);
                    }
                    else
                    {
                        _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                        _SPValue =
                            $"UserId: {_UserId}, DataDate: '{DataDate}', WDStatus: {1}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {_WorkdayCoefficientEmployeeIdFinal}";
                        Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue,
                            string.Empty);
                        WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(_UserId,
                            DataDate, 1,
                            _ERROR, _WorkdayCoefficientEmployeeIdFinal);
                    }
                }
                backgroundWorker1.ReportProgress(-1, string.Format("Checking {0}...", i + 1));
            }
        }
    }
}