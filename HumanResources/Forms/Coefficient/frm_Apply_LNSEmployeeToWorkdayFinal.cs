using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HumanResources.Progress;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Coefficient
{
    public partial class frm_Apply_LNSEmployeeToWorkdayFinal : RadForm
    {
        private static frm_Apply_LNSEmployeeToWorkdayFinal s_Instance;
        private int _flag;
        private string _OldContent = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_Apply_LNSEmployeeToWorkdayFinal()
        {
            InitializeComponent();
        }

        public static frm_Apply_LNSEmployeeToWorkdayFinal Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Apply_LNSEmployeeToWorkdayFinal();
                return s_Instance;
            }
        }

        private void frm_Apply_LNSEmployeeToWorkdayFinal_Load(object sender, EventArgs e)
        {
            InitData();
            if (radTreeView1.Nodes.Count > 0)
            {
                radTreeView1.Nodes[0].Selected = true;
                documentWindow2.Text =
                    DepartmentsBLL.GetById(int.Parse(radTreeView1.SelectedNode.Value.ToString()))
                        .DepartmentFullName.Trim();
            }
        }

        private void InitData()
        {
            Utilities.Utilities.PopulateRootLevel(radTreeView1);
            var dt = WorkdayCoefficientEmployeesFinalBLL.GetByDataTable_For_WorkdayFinal_LNSCoefficientV1(
                radDateTimePicker1.Value, radDateTimePicker1.Value.AddMonths(-1), string.Empty, 0);
            radGridView1.DataSource = dt;

            Utilities.Utilities.GridFormatting(radGridView1);
            ValidateData(dt);
            radLabelElement1.Text = "Tổng số nhân viên: " +
                                    Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
        }

        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            ViewData();
        }

        private void StartForm(sf_Background form)
        {
            var result = form.ShowDialog();
            if (result == DialogResult.Cancel)
                MessageBox.Show("Operation has been cancelled");
            if (result == DialogResult.Abort)
                MessageBox.Show("Exception:" + Environment.NewLine + form.Result.Error.Message);
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            var dt =
                WorkdayCoefficientEmployeesFinalBLL.GetByDataTable_For_WorkdayFinal_LNSCoefficientV1(
                    radDateTimePicker1.Value, radDateTimePicker1.Value.AddMonths(-1), string.Empty, 0);

            var form = new sf_Background();
            form.Text = "Updating...";

            form.DoWork += form_DoWork;
            StartForm(form);

            Cursor.Current = Cursors.Default;

            radGridView1.Columns["LNSCurrent"].HeaderText = $"HSLNS {radDateTimePicker1.Value.ToString("MM/yyyy")}";
            radGridView1.Columns["LNSPrevious"].HeaderText =
                $"HSLNS {radDateTimePicker1.Value.AddMonths(-1).ToString("MM/yyyy")}";

            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void form_DoWork(sf_Background sender, DoWorkEventArgs e)
        {
            var dt =
                WorkdayCoefficientEmployeesFinalBLL.GetByDataTable_For_WorkdayFinal_LNSCoefficientV1(
                    radDateTimePicker1.Value, radDateTimePicker1.Value.AddMonths(-1), string.Empty, 0);
            var i = 0;
            sender.SetMinimum(0);
            sender.SetMaximum(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                _OldContent =
                    $"DataDate: {dr["DataDate"]}, HSLNS: {dr["HSLNS"]}, UserId: {dr["UserId"]}";

                var _UserId = Convert.ToInt32(dr["UserId"]);
                var _FromDate = dr["LNSFromDate"] == DBNull.Value ? HRMUtil.FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr["LNSFromDate"]);
                var _ToDate = dr["LNSToDate"] == DBNull.Value ? HRMUtil.FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr["LNSToDate"]);
                var _CurrentDate = radDateTimePicker1.Value;
                if (_FromDate.Year != 1753)
                {
                    if (_FromDate.Month == _CurrentDate.Month)
                    {
                        var _HSLNS = dr["ActualValue"] == DBNull.Value
                            ? 0
                            : Convert.ToDouble(dr["ActualValue"]);
                        WorkdayCoefficientEmployeesFinalBLL.UpdateHSLNS(radDateTimePicker1.Value, _HSLNS, _UserId);

                        _SP = "Upd_H1_WorkdayCoefficientEmployeeFinal_HSLNS";
                        _SPValue =
                            $"DataDate: {radDateTimePicker1.Value}, HSLNS: {_HSLNS}, UserId: {_UserId}";
                        Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue, _OldContent);
                    }
                    else
                    {
                        DateTime _tmpDt = _CurrentDate;
                        DataRow userRow = null;
                        double hsLNS = 0;

                        userRow = WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDateToDR(_UserId, _tmpDt, 2);
                        hsLNS = userRow == null || userRow["HSLNS"] == DBNull.Value ? 0 : Convert.ToDouble(userRow["HSLNS"]);

                        try
                        {
                            while (hsLNS <= 0)
                            {
                                try
                                {
                                    userRow = WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDateToDR(_UserId,
                                        _tmpDt.AddMonths(-1), 2);
                                }
                                catch
                                {
                                    userRow = null;
                                }
                                try
                                {
                                    hsLNS = userRow == null || userRow["HSLNS"] == DBNull.Value
                                        ? 0
                                        : Convert.ToDouble(userRow["HSLNS"]);
                                }
                                catch
                                {
                                    hsLNS = 0;
                                }
                                _tmpDt = _tmpDt.AddMonths(-1);
                            }
                        }
                        catch
                        {

                        }
                        if (userRow != null && hsLNS > 0)
                        {
                            WorkdayCoefficientEmployeesFinalBLL.UpdateHSLNS(radDateTimePicker1.Value, hsLNS, _UserId);

                            _SP = "Upd_H1_WorkdayCoefficientEmployeeFinal_HSLNS";
                            _SPValue =
                                $"DataDate: {radDateTimePicker1.Value}, HSLNS: {hsLNS}, UserId: {_UserId}";
                            Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue, _OldContent);
                        }
                    }
                }
                else
                {
                    var _HSLNS = dr["ActualValue"] == DBNull.Value
                            ? 0
                            : Convert.ToDouble(dr["ActualValue"]);
                    WorkdayCoefficientEmployeesFinalBLL.UpdateHSLNS(radDateTimePicker1.Value, _HSLNS, _UserId);

                    _SP = "Upd_H1_WorkdayCoefficientEmployeeFinal_HSLNS";
                    _SPValue =
                        $"DataDate: {radDateTimePicker1.Value}, HSLNS: {_HSLNS}, UserId: {_UserId}";
                    Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue, _OldContent);
                }
                sender.SetProgress(i, "Updating .... " + i + "/" + dt.Rows.Count);

                i++;
            }
        }

        //private DataRow GetWorkdayUserRow(DateTime workDate, int userId)
        //{
        //    DataRow returnRow = null;
        //    DataRow userRow = null;
        //    userRow = WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDateToDR(userId, workDate, 1);
        //    if (userRow == null)
        //        userRow = GetWorkdayUserRow(workDate.AddMonths(-1), userId);
        //}

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "LNSCurrent")
                if (e.CellElement.Value == null)
                    e.CellElement.Text = "0";
            if (e.CellElement.ColumnInfo.Name == "LNSPrevious")
                if (e.CellElement.Value == null)
                    e.CellElement.Text = "0";

            if ((e.CellElement.ColumnInfo.Name == "LNSFromDate") || (e.CellElement.ColumnInfo.Name == "LNSToDate"))
            {
                if ((e.CellElement.Value != null) && (e.CellElement.Value.ToString().Length > 0) &&
                    (e.CellElement.Value.ToString() != ""))
                {
                    e.CellElement.DrawFill = true;
                    e.CellElement.ForeColor = Color.FromArgb(0x00, 0x00, 0xFF);
                    if (Convert.ToDateTime(e.CellElement.Value).Year == 1753)
                        e.CellElement.Text = "";
                }
            }
            else if ((e.CellElement.ColumnInfo.Name == "ActualValue") || (e.CellElement.ColumnInfo.Name == "LNSRatio"))
            {
                if ((e.CellElement.Value != null) && (e.CellElement.Value.ToString().Length > 0) &&
                    (e.CellElement.Value.ToString() != ""))
                {
                    e.CellElement.DrawFill = true;
                    e.CellElement.ForeColor = Color.FromArgb(0xC4, 0x4E, 0x00);
                }
            }
            else if (e.CellElement.ColumnInfo.Name == "colTotal")
            {
                if ((e.CellElement.Value != null) && (e.CellElement.Value.ToString().Length > 0) &&
                    (e.CellElement.Value.ToString() != ""))
                {
                    e.CellElement.DrawFill = true;
                    if (Convert.ToDouble(e.CellElement.Value) < 0)
                        e.CellElement.ForeColor = Color.Red;
                    else
                        e.CellElement.ForeColor = Color.FromArgb(166, 0, 199);
                }
            }
            else
            {
                e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
                e.CellElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            ViewData();
        }

        private void ViewData()
        {
            Cursor.Current = Cursors.AppStarting;

            var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
            var objD = new DepartmentsBLL();
            objD.GetAllChildId(deptSelected);
            var departmentIds = objD.ChildNodeIds;

            radGridView1.Columns["LNSCurrent"].HeaderText = $"HSLNS {radDateTimePicker1.Value.ToString("MM/yyyy")}";
            radGridView1.Columns["LNSPrevious"].HeaderText =
                $"HSLNS {radDateTimePicker1.Value.AddMonths(-1).ToString("MM/yyyy")}";

            var dt =
                WorkdayCoefficientEmployeesFinalBLL.GetByDataTable_For_WorkdayFinal_LNSCoefficientV1(
                    radDateTimePicker1.Value, radDateTimePicker1.Value.AddMonths(-1), departmentIds, 0);
            radGridView1.DataSource = dt;
            Utilities.Utilities.GridFormatting(radGridView1);
            ValidateData(dt);
            radLabelElement1.Text = "Tổng số nhân viên: " +
                                    Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
            documentWindow2.Text =
                DepartmentsBLL.GetById(int.Parse(radTreeView1.SelectedNode.Value.ToString())).DepartmentFullName.Trim();


            Cursor.Current = Cursors.Default;
        }

        private void ValidateData(DataTable dt)
        {
            var _flag = true;
            foreach (DataRow row in dt.Rows)
                if ((row["ActualValue"] == null) || (row["ActualValue"] == DBNull.Value))
                    _flag = false;
            //return _flag;
            if (_flag)
                radButton2.Enabled = true;
            else
                radButton2.Enabled = false;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            foreach (var item in radGridView1.Rows)
                item.Cells["colTotal"].Value = Convert.ToDouble(item.Cells["LNSCurrent"].Value) -
                                               Convert.ToDouble(item.Cells["LNSPrevious"].Value);
        }

        private void frm_Apply_LNSEmployeeToWorkdayFinal_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }
    }
}