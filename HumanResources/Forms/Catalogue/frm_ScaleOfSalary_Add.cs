using System;
using System.Windows.Forms;
using HRMBLL.H1;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Catalogue
{
    public partial class frm_ScaleOfSalary_Add : RadForm
    {
        private static frm_ScaleOfSalary_Add s_Instance;
        private readonly int _ScaleOfSalaryId;

        private frm_ScaleOfSalary _fsos;
        private string _OldContent = string.Empty;


        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_ScaleOfSalary_Add()
        {
            InitializeComponent();

            _ScaleOfSalaryId = 0;
        }

        public frm_ScaleOfSalary_Add(frm_ScaleOfSalary fsos, int ScaleOfSalaryId)
        {
            InitializeComponent();

            _fsos = fsos;
            _ScaleOfSalaryId = ScaleOfSalaryId;
        }

        public static frm_ScaleOfSalary_Add Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_ScaleOfSalary_Add();
                return s_Instance;
            }
        }

        private void frm_ScaleOfSalary_Add_Load(object sender, EventArgs e)
        {
            FormClosed += Frm_ScaleOfSalary_Add_FormClosed;

            var dr = ScaleOfSalariesBLL.GetOne(_ScaleOfSalaryId);
            if (dr != null)
            {
                txtPositionName.Text = dr["PositionName"].ToString();
                txtCode.Text = dr["Code"].ToString();
                txtValue1.Text = StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(dr["Value1"].ToString()));
                txtValue2.Text = StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(dr["Value2"].ToString()));
                txtValue3.Text = StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(dr["Value3"].ToString()));
                dtpAppliedDate.Value = Convert.ToDateTime(dr["AppliedDate"]);
                txtJobDescription.Text = dr["JobDescription"].ToString();

                _OldContent =
                    $"ScaleOfSalaryId: {_ScaleOfSalaryId}, PositionName: N'{dr["PositionName"]}', Code: N'{dr["Code"]}', Value1: {dr["Value1"]}, Value2: {dr["Value2"]}, Value3: {dr["Value3"]}, JobDescription: N'{dr["JobDescription"]}', AppliedDate: '{dr["AppliedDate"]}'";
            }
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
                var JobDescription = txtJobDescription.Text.Trim();
                var AppliedDate = Convert.ToDateTime(dtpAppliedDate.Value);

                if (_ScaleOfSalaryId == 0)
                {
                    _SP = "Ins_H1_ScaleOfSalaries";
                    _SPValue =
                        $"PositionName: N'{PositionName}', Code: N'{Code}', Value1: {Value1}, Value2: {Value2}, Value3: {Value3}, JobDescription: N'{JobDescription}', AppliedDate: '{AppliedDate}'";
                    ScaleOfSalariesBLL.Insert(PositionName, Code, Value1, Value2, Value3, JobDescription, AppliedDate);
                }
                else
                {
                    _SP = "Upd_H1_ScaleOfSalaries";
                    _SPValue =
                        $"ScaleOfSalaryId: {_ScaleOfSalaryId}, PositionName: N'{PositionName}', Code: N'{Code}', Value1: {Value1}, Value2: {Value2}, Value3: {Value3}, JobDescription: N'{JobDescription}', AppliedDate: '{AppliedDate}'";
                    ScaleOfSalariesBLL.Update(_ScaleOfSalaryId, PositionName, Code, Value1, Value2, Value3,
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
                Utilities.Utilities.SaveHRMLog("H1_ScaleOfSalaries", _SP, _SPValue, _OldContent);
            }
        }

        private void Frm_ScaleOfSalary_Add_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
            if (Application.OpenForms["frm_ScaleOfSalary"] != null)
                (Application.OpenForms["frm_ScaleOfSalary"] as frm_ScaleOfSalary).InitData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}