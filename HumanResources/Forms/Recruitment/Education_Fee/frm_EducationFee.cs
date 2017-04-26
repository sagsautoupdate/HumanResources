using System;
using System.Windows.Forms;
using HRMBLL.H2;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Recruitment.Education_Fee
{
    public partial class frm_EducationFee : RadForm
    {
        private static frm_EducationFee s_Instance;

        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();

        public frm_EducationFee()
        {
            InitializeComponent();

            InitData();
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        public static frm_EducationFee Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_EducationFee();
                return s_Instance;
            }
        }

        private void frm_EducationFee_Load(object sender, EventArgs e)
        {
            _rcm.Items["rmiDelete"].Enabled = false;
            _rcm.Items["rmiAdd"].Click += RmiAdd_Click;
            _rcm.Items["rmiEdit"].Click += RmiEdit_Click;
            _rcm.Items["rmiRefresh"].Click += Frm_EducationFee_Click;

            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            FormClosed += Frm_EducationFee_FormClosed;
        }

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "Fee")
            {
                decimal i;
                decimal.TryParse(e.CellElement.Text, out i);
                e.CellElement.Text = StringFormat.SetFormatMoneyFinal(i);
            }
        }


        private void InitData()
        {
            BS_EducationFee.DataSource = EducationFeeBLL.GetAll();
        }

        public void RefreshData()
        {
            Cursor.Current = Cursors.AppStarting;

            BS_EducationFee.DataSource = EducationFeeBLL.GetAll();
            Utilities.Utilities.GridFormatting(radGridView1);

            Cursor.Current = Cursors.Default;
        }


        private void Frm_EducationFee_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void Frm_EducationFee_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }

        private void RmiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var f_NewFee = new frm_EducationFee_Add(this,
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["FeeId"].Value),
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["SessionId"].Value),
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["PositionId"].Value), 1);

                f_NewFee.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Lỗi chưa chọn!");
            }
        }

        private void RmiAdd_Click(object sender, EventArgs e)
        {
            var f_NewFee = new frm_EducationFee_Add();

            f_NewFee.ShowDialog();
        }
    }
}