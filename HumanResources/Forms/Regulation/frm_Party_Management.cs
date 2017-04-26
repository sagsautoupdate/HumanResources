using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using HRMBLL.H0;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Regulation
{
    public partial class frm_Party_Management : RadForm
    {
        private static frm_Party_Management s_Instance;
        private string _OldContent = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;
        private DataTable TempTable;
        private DataTable UniqueTable;

        public frm_Party_Management()
        {
            InitializeComponent();

            InitData();
            InitTempTableStructure();
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        public static frm_Party_Management Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Party_Management();
                return s_Instance;
            }
        }

        private void frm_Party_Management_Load(object sender, EventArgs e)
        {
            rtrvDepartment.Nodes[0].Selected = true;

            FormClosed += Frm_Party_Management_FormClosed;
            radGridView1.RowFormatting += RadGridView1_RowFormatting;
            radGridView1.CellValueChanged += RadGridView1_CellValueChanged;
            radGridView1.CellBeginEdit += RadGridView1_CellBeginEdit;
            radGridView1.CellFormatting += RadGridView1_CellFormatting;

            documentWindow2.Text = DepartmentsBLL.GetById(int.Parse(rtrvDepartment.SelectedNode.Value.ToString()))
                .DepartmentFullName.Trim();
        }

        private void RadGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
        }

        private void RadGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if ((e.Column.Name == "UserId") || (e.Column.Name == "FullName") || (e.Column.Name == "FullName2") ||
                (e.Column.Name == "PositionName"))
                e.Cancel = true;
        }

        private void Frm_Party_Management_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void RadGridView1_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.Value != e.ActiveEditor.Value)
                if ((TempTable.Rows.Count > 0) &&
                    TempTable.Select("Convert(UserId, 'System.String') = " + e.Row.Cells["UserId"].Value).Any())
                {
                    var drEmp =
                        TempTable.Select("Convert(UserId, 'System.String') = " + e.Row.Cells["UserId"].Value)
                            .FirstOrDefault();
                    drEmp["FullName"] = e.Row.Cells["FullName"].Value;
                    drEmp["PositionName"] = e.Row.Cells["PositionName"].Value;
                    drEmp["PartyNumber"] = e.Row.Cells["PartyNumber"].Value;
                    drEmp["DateJoinParty"] = e.Row.Cells["DateJoinParty"].Value;
                    drEmp["DateJoinPartyOfficial"] = e.Row.Cells["DateJoinPartyOfficial"].Value;
                    drEmp["PlaceJoinParty"] = e.Row.Cells["PlaceJoinParty"].Value;
                    drEmp["FullName2"] = e.Row.Cells["FullName2"].Value;
                }
                else
                {
                    var dr = TempTable.NewRow();
                    dr["UserId"] = e.Row.Cells["UserId"].Value;
                    dr["FullName"] = e.Row.Cells["FullName"].Value;
                    dr["PositionName"] = e.Row.Cells["PositionName"].Value;
                    dr["PartyNumber"] = e.Row.Cells["PartyNumber"].Value;
                    dr["DateJoinParty"] = e.Row.Cells["DateJoinParty"].Value;
                    dr["DateJoinPartyOfficial"] = e.Row.Cells["DateJoinPartyOfficial"].Value;
                    dr["PlaceJoinParty"] = e.Row.Cells["PlaceJoinParty"].Value;
                    dr["FullName2"] = e.Row.Cells["FullName2"].Value;

                    TempTable.Rows.Add(dr);
                }
        }

        private void RadGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in GetLastRowOfUserId().Rows)
                {
                    var UserId = Convert.ToInt32(dr["UserId"]);
                    var FullName = dr["FullName"].ToString();
                    var PositionName = dr["PositionName"].ToString();
                    var PartyNumber = dr["PartyNumber"].ToString();
                    var DateJoinParty = Convert.ToDateTime(dr["DateJoinParty"]);
                    var DateJoinPartyOfficial = Convert.ToDateTime(dr["DateJoinPartyOfficial"]);
                    var PlaceJoinParty = dr["PlaceJoinParty"].ToString();
                    var FullName2 = dr["FullName2"].ToString();

                    var drOld = EmployeesBLL.DR_GetEmployeeById(UserId);
                    _SP = $"Upd_H0_Employee_Party_Info";
                    _SPValue =
                        $"DateJoinParty: '{DateJoinParty}', DateJoinPartyOfficial: '{DateJoinPartyOfficial}', PartyNumber: '{PartyNumber}', PlaceJoinParty: N'{PlaceJoinParty}', UserId: {UserId}";
                    _OldContent =
                        $"DateJoinParty: '{drOld["DateJoinParty"]}', DateJoinPartyOfficial: '{drOld["DateJoinPartyOfficial"]}', PartyNumber: '{drOld["PartyNumber"]}', PlaceJoinParty: N'{drOld["PlaceJoinParty"]}', UserId: {drOld["UserId"]}";
                    Utilities.Utilities.SaveHRMLog("Employees", _SP, _SPValue, _OldContent);
                    EmployeesBLL.UpdateEmployeePartyInfo(DateJoinParty, DateJoinPartyOfficial, PartyNumber,
                        PlaceJoinParty, UserId);
                }
                MessageBox.Show("Lưu thành công!");
                BS_Employees.DataSource = EmployeesBLL.DT_GetByDeptIds(string.Empty, 0, string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RtrvDepartment_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            var deptSelected = int.Parse(rtrvDepartment.SelectedNode.Value.ToString());
            var objD = new DepartmentsBLL();
            objD.GetAllChildId(deptSelected);
            var departmentIds = objD.ChildNodeIds;

            BS_Employees.DataSource = EmployeesBLL.DT_GetByDeptIds(departmentIds, 0, string.Empty, string.Empty);
            Utilities.Utilities.GridFormatting(radGridView1);
            documentWindow2.Text = DepartmentsBLL.GetById(int.Parse(rtrvDepartment.SelectedNode.Value.ToString()))
                .DepartmentFullName.Trim();
        }

        private void InitData()
        {
            Utilities.Utilities.PopulateRootLevel(rtrvDepartment);
            BS_Employees.DataSource = EmployeesBLL.DT_GetByDeptIds(string.Empty, 0, string.Empty, string.Empty);
        }

        private void InitTempTableStructure()
        {
            TempTable = new DataTable();

            TempTable.Columns.Add("UserId");
            TempTable.Columns.Add("FullName");
            TempTable.Columns.Add("PositionName");
            TempTable.Columns.Add("PartyNumber");
            TempTable.Columns.Add("DateJoinParty");
            TempTable.Columns.Add("DateJoinPartyOfficial");
            TempTable.Columns.Add("PlaceJoinParty");
            TempTable.Columns.Add("FullName2");
        }

        private DataTable GetLastRowOfUserId()
        {
            UniqueTable = new DataTable();
            UniqueTable = TempTable.Clone();

            for (var i = 0; i < TempTable.Rows.Count; i++)
            {
                var drEmp = TempTable.Select("Convert(UserId, 'System.String') = " + TempTable.Rows[i]["UserId"]);
                var lastRow = drEmp.LastOrDefault();

                var insertRow = UniqueTable.NewRow();

                insertRow["UserId"] = lastRow["UserId"];
                insertRow["FullName"] = lastRow["FullName"];
                insertRow["PositionName"] = lastRow["PositionName"];
                insertRow["PartyNumber"] = lastRow["PartyNumber"];
                insertRow["DateJoinParty"] = lastRow["DateJoinParty"];
                insertRow["DateJoinPartyOfficial"] = lastRow["DateJoinPartyOfficial"];
                insertRow["PlaceJoinParty"] = lastRow["PlaceJoinParty"];
                insertRow["FullName2"] = lastRow["FullName2"];

                UniqueTable.Rows.Add(insertRow);
            }

            return UniqueTable;
        }
    }
}