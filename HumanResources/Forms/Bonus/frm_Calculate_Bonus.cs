using System;
using System.Windows.Forms;
using HRMBLL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Bonus
{
    public partial class frm_Calculate_Bonus : RadForm
    {
        private static frm_Calculate_Bonus s_Instance;

        public frm_Calculate_Bonus()
        {
            InitializeComponent();
        }

        public static frm_Calculate_Bonus Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Calculate_Bonus();
                return s_Instance;
            }
        }

        private void rbtnView_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            rgwListBonus.DataSource =
                BonusEmployeeConditionBLL.GetByFilter(Convert.ToInt32(rDTPBonusYear.SelectedValue),
                    rDTPPayDate.Value, int.Parse(rddlBonusTitle.SelectedValue.ToString()), string.Empty);
            Utilities.Utilities.GridFormatting(rgwListBonus);
            Cursor.Current = Cursors.AppStarting;

            txtHSThuong.Text = Calculate_HSThuong().ToString("#,###0.000");
        }

        private decimal Calculate_HSThuong()
        {
            decimal returnValue = 0;
            if (rgwListBonus.Rows.Count > 0)
            {
                var summaryRowItem = rgwListBonus.SummaryRowsTop[0];
                var summaryItem = summaryRowItem["HSLNSThuong"][0];
                var summary = summaryItem.Evaluate(rgwListBonus.MasterTemplate);

                returnValue = Convert.ToDecimal(summary);
            }
            return returnValue;
        }

        private void frm_Calculate_Bonus_Load(object sender, EventArgs e)
        {
            rDTPBonusYear.DataSource = Constants.GetAllYears();
            rDTPBonusYear.DisplayMember = "UnitName";
            rDTPBonusYear.ValueMember = "UnitId";
            rDTPBonusYear.SelectedValue = DateTime.Now.Year;

            rDTPPayDate.Value = DateTime.Now;
            BindDataToDDLBonusTitle();

            txtHSThuong.Text = Calculate_HSThuong().ToString("#,###0.000");
        }

        private void BindDataToDDLBonusTitle()
        {
            rddlBonusTitle.DisplayMember = BonusTitleKeys.Field_BonusTitle_BonusTitle;
            rddlBonusTitle.ValueMember = BonusTitleKeys.Field_BonusTitle_BonusTitleId;
            rddlBonusTitle.DataSource = BonusTitle.GetByType(1);
        }

        private void txtTongQuyThuong_TextChanged(object sender, EventArgs e)
        {
            decimal _TongQuyThuong = 0;

            try
            {
                txtTongQuyThuong.Text = Convert.ToDecimal(txtTongQuyThuong.Text).ToString("#,###");
                _TongQuyThuong = Convert.ToDecimal(txtTongQuyThuong.Text);
            }
            catch
            {
            }

            if ((_TongQuyThuong > 0) && (Convert.ToDecimal(txtHSThuong.Text) > 0))
                txtDonGiaThuong.Text = (_TongQuyThuong/Convert.ToDecimal(txtHSThuong.Text)).ToString("#,###0.000");
            else
                txtDonGiaThuong.Text = "0";
        }

        private void txtDonGiaThuong_TextChanged(object sender, EventArgs e)
        {
            btCalculate.Enabled = Convert.ToDecimal(txtDonGiaThuong.Text) > 0;
        }

        private void btCalculate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;

            var _DonGiaThuong = Convert.ToDecimal(txtDonGiaThuong.Text);
            foreach (var VARIABLE in  rgwListBonus.Rows)
                VARIABLE.Cells["BonusValue"].Value = Convert.ToDecimal(VARIABLE.Cells["HSLNSThuong"].Value)*
                                                     _DonGiaThuong;
            Cursor.Current = Cursors.Default;
        }

        private void frm_Calculate_Bonus_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }
    }
}