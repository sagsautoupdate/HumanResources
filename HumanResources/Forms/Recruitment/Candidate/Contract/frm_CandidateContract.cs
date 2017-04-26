using System;
using System.Data;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H2;
using HRMUtil;
using HumanResources.Reports.Report_Preview;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Recruitment.Candidate.Contract
{
    public partial class frm_CandidateContract : RadForm
    {
        private static frm_CandidateContract s_Instance;
        private readonly int _PositionId;

        private readonly int _SessionId;
        private frm_CandidateListFinalRound _candidateLFR;
        private string _OldContent = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;
        private DataTable _TempTable;
        private bool isColumnAdded;

        public frm_CandidateContract()
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);
        }

        public frm_CandidateContract(frm_CandidateListFinalRound candidateLFR, int candidateId, string candidateName,
            int sessionId, int positionId)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            _candidateLFR = candidateLFR;
            CandidateId = candidateId;
            FullName = candidateName;
            _SessionId = sessionId;
            _PositionId = positionId;


            Text = string.Format("HỢP ĐỒNG ĐÀO TẠO CỦA {0} (MÃ HỌC VIÊN: {1})", candidateName.ToUpper(), candidateId);
        }

        public static frm_CandidateContract Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_CandidateContract();
                return s_Instance;
            }
        }

        public int CandidateId { get; set; }

        public string FullName { get; set; }

        private void frm_CandidateContract_Load(object sender, EventArgs e)
        {
            InitData();

            FormClosed += Frm_CandidateContract_FormClosed;
            KeyDown += Frm_CandidateContract_KeyDown;
            radGridView1.CellBeginEdit += RadGridView1_CellBeginEdit;
            radGridView1.CellEditorInitialized += RadGridView1_CellEditorInitialized;

            radDateTimePicker2.Value = radDateTimePicker1.Value.AddMonths(3);

            radDropDownList1.SelectedValue = _PositionId;
            ddlRecruitmentSeason.SelectedValue = _SessionId;
            radMultiColumnComboBox2.SelectedValue = CandidateId;

            radMultiColumnComboBox2.SelectedValue = CandidateId;
            ChangeData(CandidateId);
            Utilities.Utilities.GridFormatting(radGridView1);

            if (radGridView1.Rows.Count >= 1)
                radCollapsiblePanel1.IsExpanded = false;
            else
                radCollapsiblePanel1.IsExpanded = true;
        }


        private void InitData()
        {
            BS_CandidateList.DataSource = CandidatesBLL.GetDTForTrainingByFilter(string.Empty, _SessionId);
            BS_Positions.DataSource = PositionsBLL.GetAllToDT();

            BS_RecruitmentSeason.DataSource = SessionsBLL.DT_GetAll();
            BS_HighestLevel.DataSource = CandidateTrainingJobHistoryBLL.GetByCandidateId_Type(CandidateId, "EDU");
            BS_HighestLevelGrid.DataSource = CandidateTrainingJobHistoryBLL.GetByCandidateId_Type(CandidateId, "EDU");
            radGridView1.DataSource = CandidateContractionsBLL.DT_GetOne(CandidateId);
        }

        private void ChangeData(int Id)
        {
            var dr = CandidateContractionsBLL.DR_GetOne(Id);
            if (dr != null)
            {
                ddlRecruitmentSeason.SelectedValue = dr["SessionId"];
                radDropDownList1.SelectedValue = dr["PositionIdFee"];
                radDateTimePicker1.Value = Convert.ToDateTime(dr["FromDate"]);
                radDateTimePicker2.Value = Convert.ToDateTime(dr["ToDate"]);
                radDropDownList2.SelectedValue = dr["CandidateTrainingJobHistoryId"];
                radTextBox1.Text = dr["Remark"].ToString();
            }
        }

        private void InitTempTableStructure()
        {
            _TempTable = new DataTable();
            foreach (var column in radGridView1.Columns)
                _TempTable.Columns.Add(column.Name, column.DataType);
        }

        private void AddTempTableRows()
        {
            if (radGridView1.Rows.Count > 0)
                for (var i = 0; i < radGridView1.Rows.Count; i++)
                {
                    _TempTable.Rows.Add();
                    for (var j = 0; j < radGridView1.Columns.Count; j++)
                        _TempTable.Rows[i][j] = radGridView1.Rows[i].Cells[j].Value == DBNull.Value
                            ? DBNull.Value
                            : radGridView1.Rows[i].Cells[j].Value;
                }
        }


        private void RadGridView1_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            var editor = e.ActiveEditor as RadDropDownListEditor;
            if (editor != null)
            {
                var el = editor.EditorElement as RadDropDownListEditorElement;
                el.AutoCompleteMode = AutoCompleteMode.Suggest;
                el.AutoCompleteSuggest.SuggestMode = SuggestMode.Contains;
            }
        }

        private void RadGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (radGridView1.CurrentColumn is GridViewMultiComboBoxColumn)
                if (!isColumnAdded)
                {
                    isColumnAdded = true;

                    var editor = (RadMultiColumnComboBoxElement) radGridView1.ActiveEditor;
                    editor.EditorControl.MasterTemplate.AutoGenerateColumns = false;
                    editor.DropDownAnimationEnabled = true;

                    editor.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                    editor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    editor.AutoSizeDropDownToBestFit = true;
                    editor.AutoFilter = true;
                    editor.DisplayMember = "FullName";

                    var column = new GridViewTextBoxColumn("CandidateId") {HeaderText = "Mã học viên"};
                    editor.EditorControl.Columns.Add(column);

                    column = new GridViewTextBoxColumn("FullName");
                    column.HeaderText = "Họ tên";
                    editor.EditorControl.Columns.Add(column);

                    var filter = new FilterDescriptor
                    {
                        PropertyName = "FullName",
                        Operator = FilterOperator.Contains
                    };
                    editor.EditorControl.MasterTemplate.FilterDescriptors.Add(filter);
                }
            if ((e.Column.Name == "CandidateId") || (e.Column.Name == "ContractNo"))
                e.Cancel = true;
        }

        private void Frm_CandidateContract_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                rbtAdd.PerformClick();
            if (e.KeyCode == Keys.F2)
                btnSavetvtg.PerformClick();
            if (e.KeyCode == Keys.F3)
                rbtPrint.PerformClick();
            if (e.KeyCode == Keys.F4)
                rbtPreview.PerformClick();
        }

        private void Frm_CandidateContract_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void rbtAdd_Click(object sender, EventArgs e)
        {
            if ((_TempTable == null) || (_TempTable.Columns.Count <= 0) || (_TempTable.Rows.Count <= 0))
            {
                InitTempTableStructure();
                AddTempTableRows();
            }

            var newRow = _TempTable.NewRow();

            newRow["CandidateId"] = radMultiColumnComboBox2.SelectedValue;
            newRow["ContractNo"] = 0;
            newRow["FullName"] = radMultiColumnComboBox2.SelectedValue;

            newRow["FromDate"] = radDateTimePicker1.Value;
            newRow["ToDate"] = radDateTimePicker2.Value;
            newRow["Remark"] = radTextBox1.Text;
            newRow["CandidateContractId"] = 0;
            newRow["CandidateTrainingJobHistoryId"] = radDropDownList2.SelectedValue;
            newRow["PositionIdFee"] = radDropDownList1.SelectedValue;
            newRow["SessionIdFee"] = ddlRecruitmentSeason.SelectedValue;


            _TempTable.Rows.InsertAt(newRow, 0);
            radGridView1.DataSource = _TempTable;
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void radDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            radDateTimePicker2.Value = radDateTimePicker1.Value.AddMonths(3);
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var dr in radGridView1.Rows)
                {
                    DataRow drOld = null;
                    try
                    {
                        drOld =
                            CandidateContractionsBLL.DR_GetOne(Convert.ToInt32(dr.Cells["CandidateContractId"].Value));
                    }
                    catch
                    {
                    }
                    if (drOld != null)
                        _OldContent =
                            $"CandidateContractId: {drOld["CandidateContractId"]}, CandidateId: {drOld["CandidateId"]}, FromDate: '{drOld["FromDate"]}', ToDate: '{drOld["ToDate"]}', Remark: N'{drOld["Remark"]}', HighestLevelId: {drOld["HighestLevelId"]}, PositionIdFee: {drOld["PositionIdFee"]}, SessionIdFee: {drOld["SessionIdFee"]}";
                    else
                        _OldContent = string.Empty;
                    var objBLL = CandidatesBLL.GetById(Convert.ToInt32(dr.Cells["CandidateId"].Value));
                    var MinValue = FormatDate.GetSQLDateMinValue;

                    var CandidateId = dr.Cells["CandidateId"].Value == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr.Cells["CandidateId"].Value);
                    var FromDate = Convert.ToDateTime(dr.Cells["FromDate"].Value) == MinValue
                        ? MinValue
                        : Convert.ToDateTime(dr.Cells["FromDate"].Value);
                    var ToDate = Convert.ToDateTime(dr.Cells["ToDate"].Value) == MinValue
                        ? MinValue
                        : Convert.ToDateTime(dr.Cells["ToDate"].Value);
                    var Remark = dr.Cells["Remark"].Value.ToString() == string.Empty
                        ? string.Empty
                        : dr.Cells["Remark"].Value.ToString();
                    var CandidateContractId = dr.Cells["CandidateContractId"].Value == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr.Cells["CandidateContractId"].Value);
                    var HighestLevelId = dr.Cells["CandidateTrainingJobHistoryId"].Value == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr.Cells["CandidateTrainingJobHistoryId"].Value);
                    var PositionIdFee = dr.Cells["PositionIdFee"].Value == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr.Cells["PositionIdFee"].Value);
                    var SessionIdFee = dr.Cells["SessionIdFee"].Value == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr.Cells["SessionIdFee"].Value);

                    if (CandidateContractId <= 0)
                    {
                        _SP = $"Ins_H2_CandidateContract_By_CandidateId";
                        _SPValue =
                            $"CandidateId: {CandidateId}, FromDate: '{FromDate}', ToDate: '{ToDate}', Remark: N'{Remark}', HighestLevelId: {HighestLevelId}, PositionIdFee: {PositionIdFee}, SessionIdFee: {SessionIdFee}";
                        Utilities.Utilities.SaveHRMLog("H2_CandidateEducatedContraction", _SP, _SPValue, _OldContent);
                        CandidateContractionsBLL.Insert(CandidateId, FromDate, ToDate, Remark, HighestLevelId,
                            PositionIdFee, SessionIdFee);
                    }
                    else
                    {
                        _SP = $"Upd_H2_CandidateContract_By_CandidateId";
                        _SPValue =
                            $"CandidateContractId: {CandidateContractId}, CandidateId: {CandidateId}, FromDate: '{FromDate}', ToDate: '{ToDate}', Remark: N'{Remark}', HighestLevelId: {HighestLevelId}, PositionIdFee: {PositionIdFee}, SessionIdFee: {SessionIdFee}";
                        Utilities.Utilities.SaveHRMLog("H2_CandidateEducatedContraction", _SP, _SPValue, _OldContent);
                        CandidateContractionsBLL.Update(CandidateContractId, FromDate, ToDate, Remark, HighestLevelId,
                            PositionIdFee, SessionIdFee);
                    }
                }


                MessageBox.Show("Lưu thành công");
                if (radGridView1.Rows.Count > 1)
                    radGridView1.DataSource = CandidateContractionsBLL.DT_GetAll();
                else
                    radGridView1.DataSource = CandidateContractionsBLL.DT_GetOne(CandidateId);
            }
            catch
            {
                MessageBox.Show("Lưu thất bại");
            }
        }

        private void rbtPrint_Click(object sender, EventArgs e)
        {
            var FullName2 = radGridView1.CurrentRow.Cells["LastName"].Value.ToString().Trim() +
                            radGridView1.CurrentRow.Cells["FirstName"].Value.ToString().Trim();
            try
            {
                var rp = new ReportPreview(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["CandidateId"].Value),
                    FullName2, "Prt");
            }
            catch
            {
                MessageBox.Show("Crystal report missing");
            }
        }

        private void rbtPreview_Click(object sender, EventArgs e)
        {
            var FullName2 = radGridView1.CurrentRow.Cells["LastName"].Value.ToString().Trim() +
                            radGridView1.CurrentRow.Cells["FirstName"].Value.ToString().Trim();
            try
            {
                var rp = new ReportPreview(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["CandidateId"].Value),
                    FullName2, "Pre");
                rp.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Crystal report missing");
            }
        }
    }
}