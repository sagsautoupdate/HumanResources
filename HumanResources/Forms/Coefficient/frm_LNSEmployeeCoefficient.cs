using System;
using System.Data;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Coefficient
{
    public partial class frm_LNSEmployeeCoefficient : RadForm
    {
        private static frm_LNSEmployeeCoefficient s_Instance;
        private double _ActualValue;
        private int _CoefficientLevel;
        private string _OldContent = string.Empty;
        private double _Ratio = 2406;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;
        private double _TheoreticalValue;

        private bool allowEdit;
        private DataRow dr;

        public frm_LNSEmployeeCoefficient()
        {
            InitializeComponent();
        }

        public static frm_LNSEmployeeCoefficient Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_LNSEmployeeCoefficient();
                return s_Instance;
            }
        }

        private void Init()
        {
            Utilities.Utilities.PopulateRootLevel(radTreeView1);
            BS_Employees.DataSource = EmployeesBLL.GetByDeptIdsToDT(string.Empty, 0, string.Empty, "0");

            try
            {
                radGridView1.DataSource = LNSEmployeeCoefficientBLL.Get_ByFilter(0, 0, string.Empty);
            }
            catch (Exception ex)
            {
            }
            Utilities.Utilities.GridFormatting(radGridView1);

            BS_Value.DataSource = Constants.GetScaleOfSalary_Level();
            BS_ScaleOfSalary.DataSource = ScaleOfSalariesBLL.GetAllWithFilter(11);

            acbRatio.AutoCompleteDataSource = GetAutoCompleteDataSource();
            acbRatio.AutoCompleteDisplayMember = "Name";
            acbRatio.AutoCompleteValueMember = "Ratio";
            acbRatio.Text = "1";
            dtpFromDate.Value = FormatDate.GetSQLDateMinValue;
            dtpToDate.Value = FormatDate.GetSQLDateMinValue;
        }

        private DataTable GetAutoCompleteDataSource()
        {
            var table = new DataTable("Ratio");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Ratio", typeof(string));

            table.Rows.Add("0.7", "0.7");
            table.Rows.Add("0.9", "0.9");
            table.Rows.Add("1", "1");

            return table;
        }

        private void frm_LNSEmployeeCoefficient_Load(object sender, EventArgs e)
        {
            dtpFromDate.Text = string.Empty;
            dtpToDate.Text = string.Empty;
            Init();
            if (radTreeView1.Nodes.Count > 0)
            {
                radTreeView1.Nodes[0].Selected = true;
                documentWindow2.Text =
                    documentWindow2.Text =
                        DepartmentsBLL.GetById(int.Parse(radTreeView1.SelectedNode.Value.ToString()))
                            .DepartmentFullName.Trim();
            }
        }

        private void GetOldContent()
        {
            if (radGridView1.CurrentRow != null)
            {
                var _UserId = Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value);
                var dr = LNSEmployeeCoefficientBLL.Get_ById(_UserId, 0, -1).Rows[0];
                if (dr != null)
                    _OldContent =
                        $"LNSCoefficientEmployeesId: {dr["LNSCoefficientEmployeesId"]}, UserId: {dr["UserId"]}, ScaleOfSalaryId: {dr["ScaleOfSalaryId"]}, CoefficientLevel: {dr["CoefficientLevel"]}, Ratio: {dr["Ratio"]}, TheoreticalValue: {dr["TheoreticalValue"]}, ActualValue: {dr["ActualValue"]}, FromDate: {dr["FromDate"]}, ToDate: {dr["ToDate"]}, CreatedDate: {dr["CreatedDate"]}, Active: {dr["Active"]}, Remark: {dr["Remark"]}";
            }
        }

        private void radGridView1_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            allowEdit = true;
        }

        private void radGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.F2)
                allowEdit = true;
        }

        private void radGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
        }

        private void frm_LNSEmployeeCoefficient_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            BindData(0);
        }

        private void BindData(long Id)
        {
            if (radTreeView1.SelectedNode != null)
            {
                Cursor.Current = Cursors.AppStarting;

                var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;

                DataTable dt = LNSEmployeeCoefficientBLL.Get_ByFilter(0, 0, departmentIds);
                radGridView1.DataSource = dt;

                GridViewRowInfo CurrentRow = GetCurrentRow(Id);
                if (CurrentRow != null)
                    radGridView1.CurrentRow = CurrentRow;
                else
                {
                    if (radGridView1.Rows.Count > 0)
                        radGridView1.CurrentRow = radGridView1.Rows[0];
                }
                //if (Id > 0)
                //    radGridView1.CurrentRow = radGridView1.Rows.where

                Utilities.Utilities.GridFormatting(radGridView1);
                radLabelElement1.Text = "Tổng số nhân viên: " +
                                        Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);

                documentWindow2.Text =
                    DepartmentsBLL.GetById(int.Parse(radTreeView1.SelectedNode.Value.ToString()))
                        .DepartmentFullName.Trim();
                Cursor.Current = Cursors.Default;
            }
        }

        private GridViewRowInfo GetCurrentRow(long Id)
        {
            GridViewRowInfo rowReturn = null;
            foreach (var item in radGridView1.Rows)
            {
                if (long.Parse(item.Cells["LNSCoefficientEmployeesId"].Value.ToString()) == Id)
                {
                    rowReturn = item;
                }
            }
            return rowReturn;
        }

        private void radGridView1_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
        }

        private void radGridView1_EditorRequired(object sender, EditorRequiredEventArgs e)
        {
        }

        private void radGridView1_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
        }

        private void MasterTemplate_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
        }

        private void MasterTemplate_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
        }

        private void MasterTemplate_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null)
            {
                var _LNSCoefficientEmployeesId = 0;
                try
                {
                    _LNSCoefficientEmployeesId = Convert.ToInt32(e.CurrentRow.Cells["LNSCoefficientEmployeesId"].Value);
                }
                catch
                {
                }
                if (_LNSCoefficientEmployeesId > 0)
                {
                    var dr = LNSEmployeeCoefficientBLL.Get_ByFilter(0, _LNSCoefficientEmployeesId, string.Empty).Rows[0];
                    if (dr != null)
                    {
                        radMultiColumnComboBox1.SelectedValue = dr["UserId"];
                        txtLNSCoefficientEmployeesId.Text = _LNSCoefficientEmployeesId.ToString();
                        mccPositionName.SelectedValue = dr["LNSScaleOfSalaryId"];
                        ddlCoefficientLevel.SelectedValue = dr["CoefficientLevel"];
                        acbRatio.Text = dr["Ratio"] == DBNull.Value ? "0.7" : dr["Ratio"].ToString();
                        txtTheoreticalValue.Text = dr["TheoreticalValue"] == DBNull.Value
                            ? "0"
                            : dr["TheoreticalValue"].ToString();
                        txtActualValue.Text = dr["ActualValue"] == DBNull.Value ? "0" : dr["ActualValue"].ToString();
                        dtpFromDate.Text = dr["FromDate"] == DBNull.Value ? string.Empty : dr["FromDate"].ToString();
                        dtpToDate.Text = dr["ToDate"] == DBNull.Value ? string.Empty : dr["ToDate"].ToString();
                        txtRemark.Text = dr["Remark"] == DBNull.Value ? string.Empty : dr["Remark"].ToString();
                    }
                    else
                    {
                        radMultiColumnComboBox1.SelectedValue = 1;
                        txtLNSCoefficientEmployeesId.Text = "0";
                        mccPositionName.SelectedValue = null;
                        ddlCoefficientLevel.SelectedValue = 1;
                        acbRatio.Text = "0.7";
                        txtTheoreticalValue.Text = "0";
                        txtActualValue.Text = "0";
                        dtpFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        dtpToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtRemark.Text = string.Empty;
                    }
                }
                else
                {
                    txtLNSCoefficientEmployeesId.Text = "0";
                }
                GetOldContent();
            }
        }

        private void CalCulateCoefficient()
        {
            if (mccPositionName.SelectedValue != null)
            {
                dr = ScaleOfSalariesBLL.GetOne(Convert.ToInt32(mccPositionName.SelectedValue));
                if (ddlCoefficientLevel.SelectedValue != null)
                {
                    _CoefficientLevel = Convert.ToInt32(ddlCoefficientLevel.SelectedValue);
                    if (_CoefficientLevel > 0)
                        switch (_CoefficientLevel)
                        {
                            case 1:
                                _TheoreticalValue = Convert.ToDouble(dr["Value1"]);
                                break;
                            case 2:
                                _TheoreticalValue = Convert.ToDouble(dr["Value2"]);
                                break;
                            case 3:
                                _TheoreticalValue = Convert.ToDouble(dr["Value3"]);
                                break;
                        }
                    if (_TheoreticalValue > 0)
                        try
                        {
                            txtTheoreticalValue.Text = _TheoreticalValue.ToString().Trim();
                        }
                        catch (Exception ex)
                        {
                        }
                }
            }
            if (acbRatio.Text != string.Empty)
            {
                double _tryRatio = 0;
                try
                {
                    _tryRatio = Convert.ToDouble(acbRatio.Text.Replace(";", string.Empty));
                }
                catch
                {
                    MessageBox.Show("Vui lòng nhập đúng số. VD: 1,2,3, 0.7, 0.9");
                }
                _Ratio = _tryRatio;
            }

            if ((dr != null) && (_CoefficientLevel > 0) && (_Ratio != 2406))
            {
                _ActualValue = _TheoreticalValue*_Ratio;
                try
                {
                    txtActualValue.Text = _ActualValue.ToString().Trim();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void MasterTemplate_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if ((e.CellElement.ColumnInfo.Name == "FromDate") || (e.CellElement.ColumnInfo.Name == "ToDate"))
                if (Convert.ToDateTime(e.CellElement.Value).Year == 1753)
                    e.CellElement.Text = string.Empty;
        }

        private void radMultiColumnComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            CalCulateCoefficient();
        }

        private void radDropDownList1_SelectedValueChanged(object sender, EventArgs e)
        {
            CalCulateCoefficient();
        }

        private void radAutoCompleteBox1_TextChanged(object sender, EventArgs e)
        {
            CalCulateCoefficient();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            var LNSCoefficientEmployeesId = txtLNSCoefficientEmployeesId.Text == string.Empty
                ? 0
                : Convert.ToInt32(txtLNSCoefficientEmployeesId.Text);
            var UserId = Convert.ToInt32(radMultiColumnComboBox1.SelectedValue);
            var ScaleOfSalaryId = Convert.ToInt32(mccPositionName.SelectedValue);
            var CoefficientLevel = Convert.ToInt32(ddlCoefficientLevel.SelectedValue);
            var Ratio = acbRatio.Text == string.Empty ? 0.7 : Convert.ToDouble(acbRatio.Text.Replace(";", string.Empty));
            var TheoreticalValue = Convert.ToDouble(txtTheoreticalValue.Text);
            var ActualValue = txtActualValue.Text == string.Empty ? 0 : Convert.ToDouble(txtActualValue.Text);
            DateTime FromDate = dtpFromDate.Value.Year == 1753 || dtpFromDate.Value.Year == 0001 || dtpFromDate.Text == "" ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dtpFromDate.Value);
            DateTime ToDate = dtpToDate.Value.Year == 1753 || dtpToDate.Value.Year == 0001 || dtpToDate.Text == "" ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dtpToDate.Value);
            var CreatedDate = DateTime.Now;
            var Active = 1;
            var Remark = txtRemark.Text.Trim();

            var Id = LNSEmployeeCoefficientBLL.Save(LNSCoefficientEmployeesId, UserId, ScaleOfSalaryId, CoefficientLevel,
                Ratio, TheoreticalValue, ActualValue, FromDate, ToDate,
                CreatedDate, Active, Remark);
            _SP = "Save_H1_LNS_CoefficientEmployees";
            _SPValue =
                $"LNSCoefficientEmployeesId: {LNSCoefficientEmployeesId}, UserId: {UserId}, ScaleOfSalaryId: {ScaleOfSalaryId}, CoefficientLevel: {CoefficientLevel}, Ratio: {Ratio}, TheoreticalValue: {TheoreticalValue}, ActualValue: {ActualValue}, FromDate: {FromDate}, ToDate: {ToDate}, CreatedDate: {CreatedDate}, Active: {Active}, Remark: {Remark}";
            Utilities.Utilities.SaveHRMLog("H1_LNS_CoefficientEmployees", _SP, _SPValue, _OldContent);
            BindData(Id);

            radMultiColumnComboBox1.MultiColumnComboBoxElement.ShowPopup();
        }

        private void radButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                radButton1_Click(null, null);
        }

        private void radMultiColumnComboBox1_SelectedValueChanged_1(object sender, EventArgs e)
        {
            txtLNSCoefficientEmployeesId.Text = "0";
        }
    }
}