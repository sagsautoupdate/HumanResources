using System;
using System.Windows.Forms;
using HRMBLL.H0;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Catalogue
{
    public partial class frm_Positions : RadForm
    {
        private static frm_Positions s_Instance;

        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();

        public frm_Positions()
        {
            InitializeComponent();

            BS_ScaleOfSalary.DataSource = PositionsBLL.GetAllView();
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        public static frm_Positions Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Positions();
                return s_Instance;
            }
        }

        private void frm_Positions_Load(object sender, EventArgs e)
        {
            FormClosed += Frm_Positions_FormClosed;
            radGridView1.RowFormatting += RadGridView1_RowFormatting;
            _rcm.Items["rmiDelete"].Click += RmiDelete_Click;
            _rcm.Items["rmiAdd"].Click += RmiAdd_Click;
            _rcm.Items["rmiEdit"].Click += RmiEdit_Click;
            _rcm.Items["rmiRefresh"].Click += Frm_Positions_Click;
            ;
            radGridView1.CellBeginEdit += RadGridView1_CellBeginEdit;

            radGridView1.MasterView.SummaryRows[0].PinPosition = PinnedRowPosition.Top;
        }

        private void Frm_Positions_Click(object sender, EventArgs e)
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
                    PositionsBLL.Delete(Convert.ToInt32(radGridView1.CurrentRow.Cells["PositionId"].Value));
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
            var fp = new frm_Positions_Add(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["PositionId"].Value));
            Utilities.Utilities.SetScreenColor(fp);
            Cursor.Current = Cursors.Default;

            fp.ShowDialog();
        }

        private void RmiAdd_Click(object sender, EventArgs e)
        {
            var fp = frm_Positions_Add.Instance;
            Utilities.Utilities.SetScreenColor(fp);
            fp.ShowDialog();
        }

        private void RadGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo is GridViewRowInfo)
                e.RowElement.RowInfo.MinHeight = 35;
        }

        private void Frm_Positions_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        public void InitData()
        {
            Cursor.Current = Cursors.AppStarting;
            BS_ScaleOfSalary.DataSource = PositionsBLL.GetAllView();
            Utilities.Utilities.GridFormatting(radGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void radGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }
    }
}