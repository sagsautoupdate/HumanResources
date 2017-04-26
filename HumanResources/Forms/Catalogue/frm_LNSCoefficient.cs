using System;
using System.Windows.Forms;
using HRMBLL.H1;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Catalogue
{
    public partial class frm_LNSCoefficient : RadForm
    {
        private static frm_LNSCoefficient s_Instance;
        private readonly string _OldContent = string.Empty;
        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_LNSCoefficient()
        {
            InitializeComponent();
        }

        public static frm_LNSCoefficient Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_LNSCoefficient();
                return s_Instance;
            }
        }

        private void frm_LNSCoefficient_Load(object sender, EventArgs e)
        {
            InitData();
            dtpAppliedDate.Value = new DateTime(2016, 7, 1);

            FormClosed += Frm_ScaleOfSalary_FormClosed;
            radGridView1.RowFormatting += RadGridView1_RowFormatting;
            _rcm.Items["rmiDelete"].Click += RmiDelete_Click;
            _rcm.Items["rmiAdd"].Click += RmiAdd_Click;
            _rcm.Items["rmiEdit"].Click += RmiEdit_Click;
            _rcm.Items["rmiRefresh"].Click += Frm_ScaleOfSalary_Click;
            ;
            radGridView1.CellBeginEdit += RadGridView1_CellBeginEdit;

            radGridView1.MasterView.SummaryRows[0].PinPosition = PinnedRowPosition.Top;
        }

        private void Frm_ScaleOfSalary_Click(object sender, EventArgs e)
        {
            InitData();
        }

        private void RmiDelete_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Bạn có muốn xoá thông tin này không?", "Confirm", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                try
                {
                    ScaleOfSalariesBLL.Delete(Convert.ToInt32(radGridView1.CurrentRow.Cells["ScaleOfSalaryId"].Value));
                    InitData();
                }
                catch
                {
                    MessageBox.Show("Không thể xoá!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }

        private void RadGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
        }

        private void RmiEdit_Click(object sender, EventArgs e)
        {
        }

        private void RmiAdd_Click(object sender, EventArgs e)
        {
        }

        private void RadGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo is GridViewRowInfo)
                e.RowElement.RowInfo.MinHeight = 35;
        }

        private void Frm_ScaleOfSalary_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        public void InitData()
        {
            Cursor.Current = Cursors.AppStarting;
            try
            {
                BS_ScaleOfSalary.DataSource = ScaleOfSalariesBLL.GetAllWithFilter(11);
            }
            catch (Exception ex)
            {
            }
            Utilities.Utilities.GridFormatting(radGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void radGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            try
            {
                var PositionName = txtPositionName.Text.Trim();
                var Code = txtCode.Text.Trim();
                var Value1 = Convert.ToDouble(txtValue1.Text.Trim());
                var Value2 = Convert.ToDouble(txtValue2.Text.Trim());
                var Value3 = Convert.ToDouble(txtValue3.Text.Trim());
                var JobDescription = string.Empty;
                var AppliedDate = Convert.ToDateTime(dtpAppliedDate.Value);
                var _ScaleOfSalaryId = radTextBox1.Text.Trim().Length <= 0
                    ? 0
                    : Convert.ToInt32(radTextBox1.Text.Trim());

                if (_ScaleOfSalaryId == 0)
                {
                }
                else
                {
                    _SP = "Upd_H1_SOS_Coefficient";
                    _SPValue =
                        $"ScaleOfSalaryId: {_ScaleOfSalaryId}, PositionName: N'{PositionName}', Code: N'{Code}', Value1: {Value1}, Value2: {Value2}, Value3: {Value3}, JobDescription: N'{JobDescription}', AppliedDate: '{AppliedDate}'";
                    ScaleOfSalariesBLL.Update_Coefficient(_ScaleOfSalaryId, PositionName, Code, Value1, Value2, Value3,
                        JobDescription, AppliedDate);
                }

                MessageBox.Show("Lưu thành công!");
            }
            catch
            {
                MessageBox.Show("Trùng dữ liệu");
            }
            finally
            {
                BS_ScaleOfSalary.DataSource = ScaleOfSalariesBLL.GetAllWithFilter(11);
                Utilities.Utilities.GridFormatting(radGridView1);
                Utilities.Utilities.SaveHRMLog("H1_ScaleOfSalaries", _SP, _SPValue, _OldContent);
            }
        }

        private void GetData(int SOSId)
        {
            var AppliedDate = Convert.ToDateTime(dtpAppliedDate.Value);

            if (SOSId > 0)
            {
                var dr = ScaleOfSalariesBLL.GetOne(SOSId);

                txtPositionName.Text = dr["PositionName"].ToString().Trim();
                txtCode.Text = dr["Code"].ToString().Trim();
                txtValue1.Text = dr["Value1"].ToString().Trim();
                txtValue2.Text = dr["Value2"].ToString().Trim();
                txtValue3.Text = dr["Value3"].ToString().Trim();
                dtpAppliedDate.Value = Convert.ToDateTime(dr["AppliedDate"]);
                radTextBox1.Text = SOSId.ToString();
            }
        }

        private void radGridView1_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            try
            {
                GetData(Convert.ToInt32(e.CurrentRow.Cells["ScaleOfSalaryId"].Value));
            }
            catch
            {
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            try
            {
                var PositionName = txtPositionName.Text.Trim();
                var Code = txtCode.Text.Trim();
                var Value1 = Convert.ToDouble(txtValue1.Text.Trim());
                var Value2 = Convert.ToDouble(txtValue2.Text.Trim());
                var Value3 = Convert.ToDouble(txtValue3.Text.Trim());
                var JobDescription = string.Empty;
                var AppliedDate = Convert.ToDateTime(dtpAppliedDate.Value);
                var _ScaleOfSalaryId = 0;

                if (_ScaleOfSalaryId == 0)
                {
                    _SP = "Ins_H1_SOS_Coefficient";
                    _SPValue =
                        $"PositionName: N'{PositionName}', Code: N'{Code}', Value1: {Value1}, Value2: {Value2}, Value3: {Value3}, JobDescription: N'{JobDescription}', AppliedDate: '{AppliedDate}'";
                    ScaleOfSalariesBLL.Insert_Coefficient(PositionName, Code, Value1, Value2, Value3, JobDescription,
                        AppliedDate);
                }

                MessageBox.Show("Lưu thành công!");
            }
            catch
            {
                MessageBox.Show("Trùng dữ liệu");
            }
            finally
            {
                BS_ScaleOfSalary.DataSource = ScaleOfSalariesBLL.GetAllWithFilter(11);
                Utilities.Utilities.GridFormatting(radGridView1);
                Utilities.Utilities.SaveHRMLog("H1_ScaleOfSalaries", _SP, _SPValue, _OldContent);
            }
        }

        private void frm_LNSCoefficient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                radButton2.PerformClick();
            }
            else
            {
                if (e.KeyCode == Keys.F2)
                    radButton1.PerformClick();
            }
        }
    }
}