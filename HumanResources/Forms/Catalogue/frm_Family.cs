using System;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Catalogue
{
    public partial class frm_Family : RadForm
    {
        private static frm_Family s_Instance;
        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();

        public frm_Family()
        {
            InitializeComponent();

            BS_Type.DataSource = Constants.GetAllRType();
            BS_Family.DataSource = RelationTypesBLL.GetAllDT();
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        public static frm_Family Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Family();
                return s_Instance;
            }
        }

        private void frm_Family_Load(object sender, EventArgs e)
        {
            FormClosed += Frm_Family_FormClosed;
            radGridView1.RowFormatting += RadGridView1_RowFormatting;
            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            radGridView1.CellBeginEdit += RadGridView1_CellBeginEdit;
            _rcm.Items["rmiDelete"].Click += RmiDelete_Click;
            _rcm.Items["rmiAdd"].Click += RmiAdd_Click;
            _rcm.Items["rmiEdit"].Click += RmiEdit_Click;
            _rcm.Items["rmiRefresh"].Click += Frm_Family_Click;

            radGridView1.MasterView.SummaryRows[0].PinPosition = PinnedRowPosition.Top;
        }

        private void Frm_Family_Click(object sender, EventArgs e)
        {
            InitData();
        }

        private void RmiEdit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            var ffa = new frm_Family_Add(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["RelationTypeId"].Value));
            Utilities.Utilities.SetScreenColor(ffa);
            Cursor.Current = Cursors.Default;
            ffa.ShowDialog();
        }

        private void RmiAdd_Click(object sender, EventArgs e)
        {
            var ffa = frm_Family_Add.Instance;
            Utilities.Utilities.SetScreenColor(ffa);
            ffa.ShowDialog();
        }

        private void RmiDelete_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Bạn có muốn xoá thông tin này không?", "Confirm", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                try
                {
                    RelationTypesBLL.Delete(Convert.ToInt32(radGridView1.CurrentRow.Cells["RelationTypeId"].Value));
                    InitData();
                }
                catch
                {
                    MessageBox.Show("Lỗi dữ liệu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }

        private void RadGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (e.Column.Name == "Type")
                e.Cancel = true;
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }

        private void RadGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo is GridViewRowInfo)
                e.RowElement.RowInfo.MinHeight = 35;
        }

        public void InitData()
        {
            Cursor.Current = Cursors.AppStarting;
            BS_Family.DataSource = RelationTypesBLL.GetAllDT();
            Utilities.Utilities.GridFormatting(radGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void Frm_Family_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }
    }
}