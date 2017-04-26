using System;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Catalogue
{
    public partial class frm_Family_Add : RadForm
    {
        private static frm_Family_Add s_Instance;
        private readonly int _RelationTypeId;

        private frm_Family _ff;
        private string _OldContent = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_Family_Add()
        {
            InitializeComponent();

            _RelationTypeId = 0;
        }

        public frm_Family_Add(frm_Family ff, int RelationTypeId)
        {
            InitializeComponent();

            _ff = ff;
            _RelationTypeId = RelationTypeId;
        }

        public static frm_Family_Add Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Family_Add();
                return s_Instance;
            }
        }

        private void frm_Family_Add_Load(object sender, EventArgs e)
        {
            FormClosed += Frm_Family_Add_FormClosed;

            BS_Type.DataSource = Constants.GetAllRType();

            var dr = RelationTypesBLL.GetById(_RelationTypeId);
            if (dr != null)
            {
                txtRelationName.Text = dr["RelationTypeName"].ToString();
                ddlRelationType.SelectedValue = dr["Type"];
                txtDescription.Text = dr["Description"].ToString();

                _OldContent =
                    $"RelationTypeName: N'{dr["RelationTypeName"]}', Description: N'{dr["Description"]}', Type: {dr["Type"]}, RelationTypeId: {_RelationTypeId}";
            }
        }


        private void SetDefaultValues()
        {
            txtRelationName.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Frm_Family_Add_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
            if (Application.OpenForms["frm_Family"] != null)
                (Application.OpenForms["frm_Family"] as frm_Family).InitData();
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            try
            {
                var RelationName = txtRelationName.Text.Trim();
                var RelationType = Convert.ToInt32(ddlRelationType.SelectedValue);
                var Description = txtDescription.Text.Trim();

                var objBLL = new RelationTypesBLL(_RelationTypeId, RelationName, Description, RelationType);
                try
                {
                    objBLL.Save();
                    _SP = objBLL.ReturnSP();
                    _SPValue = objBLL.ReturnSPValue();

                    MessageBox.Show("Lưu thành công!", "Announcement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    Utilities.Utilities.SaveHRMLog("H0_RelationTypes", _SP, _SPValue, _OldContent);
                }
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}