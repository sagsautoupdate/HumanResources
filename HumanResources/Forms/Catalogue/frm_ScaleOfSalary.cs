using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using HRMBLL.H1;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Catalogue
{
    public partial class frm_ScaleOfSalary : RadForm
    {
        private static frm_ScaleOfSalary s_Instance;
        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();

        private DataTable _TempTable;
        private DataTable _UniqueTable;

        public frm_ScaleOfSalary()
        {
            InitializeComponent();

            BS_ScaleOfSalary.DataSource = ScaleOfSalariesBLL.GetAll();
            InitTempTableStructure();
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        public static frm_ScaleOfSalary Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_ScaleOfSalary();
                return s_Instance;
            }
        }

        private void frm_ScaleOfSalary_Load(object sender, EventArgs e)
        {
            dtpAppliedDate.Value = DateTime.Now;

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
            if (e.Column.Name == "PositionName2")
                e.Cancel = true;
        }

        private void RmiEdit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            var fsosa = new frm_ScaleOfSalary_Add(this,
                Convert.ToInt32(radGridView1.CurrentRow.Cells["ScaleOfSalaryId"].Value));
            Utilities.Utilities.SetScreenColor(fsosa);
            Cursor.Current = Cursors.Default;

            fsosa.ShowDialog();
        }

        private void RmiAdd_Click(object sender, EventArgs e)
        {
            var fsosa = frm_ScaleOfSalary_Add.Instance;
            Utilities.Utilities.SetScreenColor(fsosa);
            fsosa.ShowDialog();
        }

        private DataTable GetLastRowOfUserId()
        {
            _UniqueTable = new DataTable();
            _UniqueTable = _TempTable.Clone();

            for (var i = 0; i < _TempTable.Rows.Count; i++)
            {
                var drEmp =
                    _TempTable.Select("Convert(ScaleOfSalaryId, 'System.String') = " +
                                      _TempTable.Rows[i]["ScaleOfSalaryId"]);
                var lastRow = drEmp.LastOrDefault();

                var insertRow = _UniqueTable.NewRow();

                insertRow["ScaleOfSalaryId"] = lastRow["ScaleOfSalaryId"];
                insertRow["PositionName"] = lastRow["PositionName"];
                insertRow["Code"] = lastRow["Code"];
                insertRow["Value1"] = lastRow["Value1"];
                insertRow["Value2"] = lastRow["Value2"];
                insertRow["Value3"] = lastRow["Value3"];
                insertRow["JobDescription"] = lastRow["JobDescription"];
                insertRow["PositionName2"] = lastRow["PositionName2"];
                insertRow["AppliedDate"] = lastRow["AppliedDate"];

                _UniqueTable.Rows.Add(insertRow);
            }

            return _UniqueTable;
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
            BS_ScaleOfSalary.DataSource = ScaleOfSalariesBLL.GetAll();
            Utilities.Utilities.GridFormatting(radGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void InitTempTableStructure()
        {
            _TempTable = new DataTable();
            foreach (var column in radGridView1.Columns)
                _TempTable.Columns.Add(column.Name, column.DataType);
        }

        private void radGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }
    }
}