using System;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Bonus
{
    public partial class frm_Bonus : RadForm
    {
        private static frm_Bonus s_Instance;

        public frm_Bonus()
        {
            InitializeComponent();
        }

        public static frm_Bonus Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Bonus();
                return s_Instance;
            }
        }

        private void frm_Bonus_Load(object sender, EventArgs e)
        {
            rDTPBonusYear.DataSource = Constants.GetAllYears();
            rDTPBonusYear.DisplayMember = "UnitName";
            rDTPBonusYear.ValueMember = "UnitId";
            rDTPBonusYear.SelectedValue = DateTime.Now.Year;

            rDTPPayDate.Value = DateTime.Now;
            BindDataToDDLBonusTitle();

            Utilities.Utilities.PopulateRootLevel(rtrvDepartment);
            if (rtrvDepartment.Nodes.Count > 0)
                rtrvDepartment.Nodes[0].Selected = true;
            rgwListBonus.DataSource = BonusEmployeeConditionBLL.GetByFilter(
                Convert.ToInt32(rDTPBonusYear.SelectedValue), rDTPPayDate.Value,
                int.Parse(rddlBonusTitle.SelectedValue.ToString()), string.Empty);
            Utilities.Utilities.GridFormatting(rgwListBonus);
        }

        private void BindDataToDDLBonusTitle()
        {
            rddlBonusTitle.DisplayMember = BonusTitleKeys.Field_BonusTitle_BonusTitle;
            rddlBonusTitle.ValueMember = BonusTitleKeys.Field_BonusTitle_BonusTitleId;
            rddlBonusTitle.DataSource = BonusTitle.GetByType(1);
        }

        private void frm_Bonus_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void rtrvDepartment_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            Cursor.Current = Cursors.AppStarting;
            if (rtrvDepartment.SelectedNode != null)
            {
                var deptSelected = int.Parse(rtrvDepartment.SelectedNode.Value.ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;

                rgwListBonus.DataSource =
                    BonusEmployeeConditionBLL.GetByFilter(Convert.ToInt32(rDTPBonusYear.SelectedValue),
                        rDTPPayDate.Value, int.Parse(rddlBonusTitle.SelectedValue.ToString()), departmentIds);
                Utilities.Utilities.GridFormatting(rgwListBonus);
            }
            else
            {
                var deptSelected = int.Parse(rtrvDepartment.SelectedNode.Value.ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);

                rgwListBonus.DataSource =
                    BonusEmployeeConditionBLL.GetByFilter(Convert.ToInt32(rDTPBonusYear.SelectedValue),
                        rDTPPayDate.Value, int.Parse(rddlBonusTitle.SelectedValue.ToString()), string.Empty);
                Utilities.Utilities.GridFormatting(rgwListBonus);
            }
            Cursor.Current = Cursors.Default;
        }

        private void rbtnView_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void rddlBonusTitle_SelectedValueChanged(object sender, EventArgs e)
        {
            rpnlHeaderText.Text = "BẢNG THƯỞNG - " + rddlBonusTitle.SelectedItem.Text.ToUpper();
        }
    }
}