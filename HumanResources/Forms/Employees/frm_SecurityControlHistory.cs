using System;
using System.Windows.Forms;
using HRMBLL.H0;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Employees
{
    public partial class frm_SecurityControlHistory : RadForm
    {
        private readonly int _UserId;

        public frm_SecurityControlHistory()
        {
            InitializeComponent();
        }

        public frm_SecurityControlHistory(int UserId)
        {
            InitializeComponent();

            _UserId = UserId;
        }

        private void frm_SecurityControlHistory_Load(object sender, EventArgs e)
        {
            radGridView1.DataSource = SecurityControlBLL.GetAllHistory(_UserId);
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            var _Active = Convert.ToInt32(radGridView1.CurrentRow.Cells["Active"].Value);
            var _IsLost = Convert.ToInt32(radGridView1.CurrentRow.Cells["IsLost"].Value);
            var _RemarkLost = radGridView1.CurrentRow.Cells["RemarkLost"].Value == null
                ? string.Empty
                : radGridView1.CurrentRow.Cells["RemarkLost"].Value.ToString();
            try
            {
                SecurityControlBLL.UpdateLostCard(
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["SecurityControlId"].Value), _Active, _IsLost,
                    _RemarkLost, clsGlobal.UserId);
            }
            catch
            {
            }
            MessageBox.Show("Cập nhật thành công!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            radGridView1.DataSource = SecurityControlBLL.GetAllHistory(_UserId);
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void radGridView1_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (radGridView1.CurrentColumn.Name == "IsLost")
                if (e.NewValue != e.OldValue)
                    radGridView1.Columns["RemarkLost"].IsVisible = true;
                else
                    radGridView1.Columns["RemarkLost"].IsVisible = false;
        }
    }
}