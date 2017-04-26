using System;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Catalogue
{
    public partial class frm_Positions_Add : RadForm
    {
        private static frm_Positions_Add s_Instance;
        private readonly int _PositionId;

        private frm_Positions _fp;
        private string _OldContent = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_Positions_Add()
        {
            InitializeComponent();

            _PositionId = 0;
        }

        public frm_Positions_Add(frm_Positions fp, int PositionId)
        {
            InitializeComponent();

            _fp = fp;
            _PositionId = PositionId;
        }

        public static frm_Positions_Add Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Positions_Add();
                return s_Instance;
            }
        }

        private void frm_ScaleOfSalary_Add_Load(object sender, EventArgs e)
        {
            FormClosed += Frm_Positions_Add_FormClosed;

            BS_Departments.DataSource = DepartmentsBLL.GetAllRoots();
            BS_LevelPositions.DataSource = Constants.GetAllLevelPosition();

            var dr = PositionsBLL.GetPositionId(_PositionId);
            if (dr != null)
            {
                txtPositionName.Text = dr["PositionName"].ToString();
                ddlDepartments.SelectedValue = dr["DepartmentId"];
                ddlLevelPositions.SelectedValue = dr["LevelPosition"];
                txtDescription.Text = dr["Description"].ToString();
                txtJobDescription.Text = dr["JobDescription"].ToString();
                txtF.Text = dr["F"].ToString();
                txtOm.Text = dr["Om"].ToString();

                _OldContent =
                    $"PositionId: {_PositionId}, PositionName: N'{dr["PositionName"]}', N'{dr["Description"]}', LevelPosition: {dr["LevelPosition"]}, DepartmentId: {dr["DepartmentId"]}, JobDescription: N'{dr["JobDescription"]}', F: {dr["F"]}, Om: {dr["Om"]}";
            }
        }


        private void SetDefaultValues()
        {
            txtPositionName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtJobDescription.Text = string.Empty;
            txtF.Text = string.Empty;
            txtOm.Text = string.Empty;
        }


        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            try
            {
                var PositionName = txtPositionName.Text.Trim();
                var DepartmentId = Convert.ToInt32(ddlDepartments.SelectedValue);
                var LevelPosition = Convert.ToInt32(ddlLevelPositions.SelectedValue);
                var Description = txtDescription.Text.Trim();
                var JobDescription = txtJobDescription.Text.Trim();
                var F = string.Empty;
                var Om = string.Empty;
                F = txtF.Text.Trim();
                Om = txtOm.Text.Trim();

                var objBLL = new PositionsBLL(_PositionId, PositionName, Description);
                objBLL.DepartmentId = DepartmentId;
                objBLL.LevelPosition = LevelPosition;
                objBLL.JobDecription = JobDescription;
                objBLL.F = Convert.ToDouble(F);
                objBLL.Om = Convert.ToDouble(Om);
                try
                {
                    objBLL.Save_V1();
                    _SP = objBLL.ReturnSP();
                    _SPValue = objBLL.ReturnSPValue();
                    MessageBox.Show("Lưu thành công!", "Announcement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (HRMException he)
                {
                    if (he.ErrorCode == 1300100)
                        MessageBox.Show("Chức danh này đã tồn tại.", "Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    else
                        MessageBox.Show(he.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Utilities.Utilities.SaveHRMLog("Positions", _SP, _SPValue, _OldContent);
            }
        }

        private void Frm_Positions_Add_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
            if (Application.OpenForms["frm_Positions"] != null)
                (Application.OpenForms["frm_Positions"] as frm_Positions).InitData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}