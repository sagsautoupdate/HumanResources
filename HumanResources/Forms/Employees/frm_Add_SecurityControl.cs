using System;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Employees
{
    public partial class frm_Add_SecurityControl : RadForm
    {
        private readonly int _SecurityControlId;
        private string _Area1;
        private string _Area2;
        private string _Area3;
        private string _Area4;
        private string _Area5;
        private string _Area6;
        private frm_SecurityControl _fsc;
        private string _FullName;
        private string _OldContent = string.Empty;
        private DateTime _Period;
        private string _Remark;
        private string _SecurityControl;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;
        private DateTime _StartDate;
        private int _UserId;

        public frm_Add_SecurityControl()
        {
            InitializeComponent();
        }

        public frm_Add_SecurityControl(int SecurityControlId, int UserId, string FullName, frm_SecurityControl fsc)
        {
            InitializeComponent();

            _UserId = UserId;
            _FullName = FullName;
            _fsc = fsc;
            _SecurityControlId = SecurityControlId;
        }

        private void frm_Add_SecurityControl_Load(object sender, EventArgs e)
        {
            BS_Employees.DataSource = EmployeesBLL.GetDTByStatus(string.Empty, "1,2,3");

            LoadData();
            acbArea1.AutoCompleteDataSource = Constants.GetAllSecurityControl();
            acbArea2.AutoCompleteDataSource = Constants.GetAllSecurityControl();
            acbArea3.AutoCompleteDataSource = Constants.GetAllSecurityControl();
            acbArea4.AutoCompleteDataSource = Constants.GetAllSecurityControl();
            acbArea5.AutoCompleteDataSource = Constants.GetAllSecurityControl();
            acbArea6.AutoCompleteDataSource = Constants.GetAllSecurityControl();

            FormClosed += Frm_Add_SecurityControl_FormClosed;
            rmbEmployees.SelectedValueChanged += RmbEmployees_SelectedValueChanged;
            acbArea1.ListElement.VisualItemFormatting += ListElement_VisualItemFormatting;
            acbArea2.ListElement.VisualItemFormatting += ListElement_VisualItemFormatting;
            acbArea3.ListElement.VisualItemFormatting += ListElement_VisualItemFormatting;
            acbArea4.ListElement.VisualItemFormatting += ListElement_VisualItemFormatting;
            acbArea5.ListElement.VisualItemFormatting += ListElement_VisualItemFormatting;
            acbArea6.ListElement.VisualItemFormatting += ListElement_VisualItemFormatting;
        }


        private void LoadData()
        {
            var dr = SecurityControlBLL.GetOneById(_UserId);
            if (dr != null)
            {
                rmbEmployees.SelectedValue = _UserId;
                txtSecurityControlNumber.Text = dr["CurrentSCI"] == DBNull.Value
                    ? string.Empty
                    : dr["CurrentSCI"].ToString();
                dtpPeriod.Value = dr["Expired"] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr["Expired"]);
                dtpStartDate.Value = dr["StartDate"] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr["StartDate"]);
                acbArea1.Text = dr["Area1"] == DBNull.Value ? string.Empty : dr["Area1"].ToString();
                acbArea2.Text = dr["Area2"] == DBNull.Value ? string.Empty : dr["Area2"].ToString();
                acbArea3.Text = dr["Area3"] == DBNull.Value ? string.Empty : dr["Area3"].ToString();
                acbArea4.Text = dr["Area4"] == DBNull.Value ? string.Empty : dr["Area4"].ToString();
                acbArea5.Text = dr["Area5"] == DBNull.Value ? string.Empty : dr["Area5"].ToString();
                acbArea6.Text = dr["Area6"] == DBNull.Value ? string.Empty : dr["Area6"].ToString();
                txtRemark.Text = dr["Remark"].ToString();

                _OldContent =
                    $"UserId: {dr["UserId"]}, SecurityControl: '{dr["CurrentSCI"]}', Period: '{dr["Expired"]}', Area1: '{dr["Area1"]}', Area2: '{dr["Area2"]}', Area3: '{dr["Area3"]}', Area4: '{dr["Area4"]}', Area5: '{dr["Area5"]}', Area6: '{dr["Area6"]}', Remark: N'{dr["Remark"]}', UpdateBy: {clsGlobal.UserId}, StartDate: '{dr["StartDate"]}'";
            }
            else
            {
                txtSecurityControlNumber.Text = string.Empty;
                dtpPeriod.Value = DateTime.Now;
                dtpStartDate.Value = DateTime.Now;
                acbArea1.Text = string.Empty;
                acbArea2.Text = string.Empty;
                acbArea3.Text = string.Empty;
                acbArea4.Text = string.Empty;
                acbArea5.Text = string.Empty;
                acbArea6.Text = string.Empty;
                txtRemark.Text = string.Empty;
            }
        }

        private void SaveData()
        {
            _UserId = Convert.ToInt32(rmbEmployees.SelectedValue);
            _SecurityControl = txtSecurityControlNumber.Text.Trim();
            _Period = Convert.ToDateTime(dtpPeriod.Value);
            _StartDate = Convert.ToDateTime(dtpStartDate.Value);
            _Area1 = acbArea1.Text.Trim();
            _Area2 = acbArea2.Text.Trim();
            _Area3 = acbArea3.Text.Trim();
            _Area4 = acbArea4.Text.Trim();
            _Area5 = acbArea5.Text.Trim();
            _Area6 = acbArea6.Text.Trim();
            _Remark = txtRemark.Text.Trim();
            var dr = SecurityControlBLL.GetOneById(_UserId);
            if (_SecurityControlId > 0)
            {
                SecurityControlBLL.Update(_UserId, _SecurityControl, _Period, _Area1, _Area2, _Area3, _Area4, _Area5,
                    _Area6, _Remark, _SecurityControlId, clsGlobal.UserId, _StartDate);
                _SP = "Upd_H0_SecurityControl";
                _SPValue =
                    $"UserId: {_UserId}, SecurityControl: N'{_SecurityControl}', Period: '{_Period}', Area1: '{_Area1}', Area2: '{_Area2}', Area3: '{_Area3}', Area4: '{_Area4}', Area5: '{_Area5}', Area6: '{_Area6}', Remark: N'{_Remark}', SecurityControlId: {_SecurityControlId}, UpdateBy: {clsGlobal.UserId}, StartDate: '{_StartDate}'";
                Utilities.Utilities.SaveHRMLog("H0_EmployeeSecurityControl", _SP, _SPValue, _OldContent);
            }
            else
            {
                SecurityControlBLL.Insert(_UserId, _SecurityControl, _Period, _Area1, _Area2, _Area3, _Area4, _Area5,
                    _Area6, _Remark, clsGlobal.UserId, _StartDate);
                _SP = "Ins_H0_SecurityControl";
                _SPValue =
                    $"UserId: {_UserId}, SecurityControl: '{_SecurityControl}', Period: '{_Period}', Area1: '{_Area1}', Area2: '{_Area2}', Area3: '{_Area3}', Area4: '{_Area4}', Area5: '{_Area5}', Area6: '{_Area6}', Remark: N'{_Remark}', UpdateBy: {clsGlobal.UserId}, StartDate: '{_StartDate}'";
                Utilities.Utilities.SaveHRMLog("H0_EmployeeSecurityControl", _SP, _SPValue, _OldContent);
            }
        }


        private void RmbEmployees_SelectedValueChanged(object sender, EventArgs e)
        {
            _UserId = Convert.ToInt32(rmbEmployees.SelectedValue);
            LoadData();
        }

        private void ListElement_VisualItemFormatting(object sender, VisualItemFormattingEventArgs e)
        {
            var dataItem = e.VisualItem.Data;
            e.VisualItem.Text = string.Format("{0} <{1}>", dataItem.Value, dataItem.Text);
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
                MessageBox.Show("Thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Frm_Add_SecurityControl_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}