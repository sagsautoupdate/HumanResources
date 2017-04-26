using System;
using System.Windows.Forms;
using HRMBLL.H0;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Catalogue
{
    public partial class frm_Department : RadForm
    {
        private static frm_Department s_Instance;
        private string _OldContent = string.Empty;

        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_Department()
        {
            InitializeComponent();
        }

        public static frm_Department Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Department();
                return s_Instance;
            }
        }

        private void frm_Department_Load(object sender, EventArgs e)
        {
            Utilities.Utilities.PopulateRootLevel(radTreeView1);
            radTreeView1.Nodes["SAGS"].Expanded = true;
            radTreeView1.Nodes["SAGS"].Selected = true;

            BindData();

            FormClosed += Frm_Department_FormClosed;
        }


        private void BindData()
        {
            if (radTreeView1.SelectedNode != null)
            {
                var DepartmentId = Convert.ToInt32(radTreeView1.SelectedNode.Value);
                var objBLL = DepartmentsBLL.GetById(DepartmentId);
                txtparentDepartmentCode.Text = objBLL.DepartmentCode;
                txtparentDepartmentName.Text = objBLL.DepartmentName;
                txtparentDepartmentNameEnglish.Text = objBLL.DepartmentNameE;
                txtparentDepartmentDescription.Text = objBLL.Description;

                radGroupBox1.HeaderText = "Cập Nhật " + objBLL.DepartmentName.ToUpper();
                radGroupBox2.HeaderText = "Thêm Mới Tổ Hoặc Đội Vào " + objBLL.DepartmentName.ToUpper();

                _OldContent =
                    $"DepartmentId: N'{objBLL.DepartmentCode}', DepartmentName: N'{objBLL.DepartmentName}', DepartmentNameE: N'{objBLL.DepartmentNameE}', Description: N'{objBLL.Description}', DepartmentId: {DepartmentId}";
            }
        }


        private void Frm_Department_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void radTreeView1_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            BindData();
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            try
            {
                var DepartmentId = Convert.ToInt32(radTreeView1.SelectedNode.Value);

                Cursor.Current = Cursors.AppStarting;
                var objBLL = new DepartmentsBLL(DepartmentId, txtparentDepartmentName.Text);
                objBLL.DepartmentCode = txtparentDepartmentCode.Text;
                objBLL.DepartmentNameE = txtparentDepartmentNameEnglish.Text;
                objBLL.Description = txtparentDepartmentDescription.Text;

                objBLL.Save();
                _SP = objBLL.ReturnSP();
                _SPValue = objBLL.ReturnSPValue();

                radTreeView1.Nodes.Clear();
                Utilities.Utilities.PopulateRootLevel(radTreeView1);
                Cursor.Current = Cursors.Default;

                radTreeView1.Nodes["SAGS"].Expanded = true;
                radTreeView1.Nodes["SAGS"].Selected = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Utilities.Utilities.SaveHRMLog("H0_Departments", _SP, _SPValue, _OldContent);
            }
        }

        private void btnDeletetvtg_Click(object sender, EventArgs e)
        {
            try
            {
                var DepartmentId = Convert.ToInt32(radTreeView1.SelectedNode.Value);
                if (
                    MessageBox.Show("Bạn muốn xóa thông tin này?", "Alert", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.AppStarting;
                    DepartmentsBLL.Delete(DepartmentId);
                    _SP = $"Del_H0_Department";
                    _SPValue = $"{DepartmentId}";

                    radTreeView1.Nodes.Clear();
                    Utilities.Utilities.PopulateRootLevel(radTreeView1);
                    Cursor.Current = Cursors.Default;

                    radTreeView1.Nodes["SAGS"].Expanded = true;
                    radTreeView1.Nodes["SAGS"].Selected = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Utilities.Utilities.SaveHRMLog("H0_Departments", _SP, _SPValue, _OldContent);
            }
        }

        private void btnSaveChildtvtg_Click(object sender, EventArgs e)
        {
            try
            {
                var DepartmentId = Convert.ToInt32(radTreeView1.SelectedNode.Value);

                long Id = 0;
                var objBLL = new DepartmentsBLL(0, txtchildDepartmentName.Text);
                objBLL.ParentId = DepartmentId;
                objBLL.DepartmentCode = txtchildDepartmentCode.Text;
                objBLL.DepartmentNameE = txtchildDepartmentNameEnglish.Text;
                objBLL.Description = txtchildDepartmentDescription.Text;

                Cursor.Current = Cursors.AppStarting;
                Id = objBLL.Save();
                _SP = objBLL.ReturnSP();
                _SPValue = objBLL.ReturnSPValue();

                if (Id == -1)
                {
                    MessageBox.Show("Tên bị trùng", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    radTreeView1.Nodes.Clear();
                    Utilities.Utilities.PopulateRootLevel(radTreeView1);
                    Cursor.Current = Cursors.Default;

                    radTreeView1.Nodes["SAGS"].Expanded = true;
                    radTreeView1.Nodes["SAGS"].Selected = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Utilities.Utilities.SaveHRMLog("H0_Departments", _SP, _SPValue, _OldContent);
            }
        }
    }
}