using System;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H2;
using Telerik.WinControls.UI;
using ValueChangedEventArgs = Telerik.WinControls.UI.Data.ValueChangedEventArgs;

namespace HumanResources.Forms.Recruitment.Education_Fee
{
    public partial class frm_EducationFee_Add : RadForm
    {
        private readonly int _FeeId;
        private readonly int _PositionId;
        private readonly int _SessionId;
        private readonly int _Type;


        private frm_EducationFee _ef;
        private string _OldContent = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_EducationFee_Add()
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);
        }

        public frm_EducationFee_Add(frm_EducationFee ef, int FeeId, int SessionId, int PositionId, int Type)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            _ef = ef;
            _FeeId = FeeId;
            _PositionId = PositionId;
            _SessionId = SessionId;
            _Type = Type;
        }

        private void frm_Add_EducationFee_Load(object sender, EventArgs e)
        {
            InitData();

            if (_Type == 0)
            {
                Text = "Thêm mới chi phí đào tạo";
            }
            else
            {
                Text = "Chỉnh sửa chi phí đào tạo";
                var dr = EducationFeeBLL.GetById(_FeeId);
                radDropDownList1.SelectedValue = dr["SessionId"];
                radDropDownList2.SelectedValue = dr["PositionId"];
                txtFee.Text = dr["Fee"].ToString();
                txtFeeVN.Text = dr["FeeInVietNamese"].ToString();

                _OldContent =
                    $"SessionId: {_SessionId}, PositionId: {_PositionId}, Fee: '{dr["Fee"]}', FeeInVietnNamese: N'{dr["FeeInVietNamese"]}', CreatedBy: {clsGlobal.UserId}, FeeId: {_FeeId}";
            }

            FormClosed += Frm_Add_EducationFee_FormClosed;
            radDropDownList1.SelectedValueChanged += RadDropDownList1_SelectedValueChanged;
            radMenuButtonItem1.Click += RadMenuButtonItem1_Click;
            radMenuButtonItem2.Click += RadMenuButtonItem2_Click;
            radMenuComboItem1.ComboBoxElement.SelectedValueChanged += ComboBoxElement_SelectedValueChanged;
        }


        private void InitData()
        {
            BS_Position.DataSource = PositionsBLL.GetAllToDT_V1();
            BS_Session.DataSource = SessionsBLL.DT_GetAll();

            BS_OldSession.DataSource = EducationFeeBLL.GetAll();
            radMenuComboItem1.ComboBoxElement.SelectedIndex = 0;
            BS_OldPosition.DataSource =
                EducationFeeBLL.GetBySessionId(Convert.ToInt32(radMenuComboItem1.ComboBoxElement.SelectedValue));
        }


        private void ComboBoxElement_SelectedValueChanged(object sender, ValueChangedEventArgs e)
        {
            BS_OldPosition.DataSource =
                EducationFeeBLL.GetBySessionId(Convert.ToInt32(radMenuComboItem1.ComboBoxElement.SelectedValue));
        }

        private void RadMenuButtonItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cancel!");
        }

        private void RadMenuButtonItem1_Click(object sender, EventArgs e)
        {
            var _OldSessionId = 0;
            try
            {
                _OldSessionId = Convert.ToInt32(radMenuComboItem1.ComboBoxElement.SelectedValue);
            }
            catch
            {
            }
            var _OldPositionId = 0;
            try
            {
                _OldPositionId = Convert.ToInt32(radMenuComboItem2.ComboBoxElement.SelectedValue);
            }
            catch
            {
            }

            var dr = EducationFeeBLL.GetBySessionIdPositionId(_OldSessionId, _OldPositionId);
            if (dr != null)
            {
                txtFee.Text = dr["Fee"].ToString();
                txtFeeVN.Text = dr["FeeInVietNamese"].ToString();
            }
        }

        private void RadDropDownList1_SelectedValueChanged(object sender, EventArgs e)
        {
            var dr = EducationFeeBLL.GetBySessionIdPositionId(Convert.ToInt32(radDropDownList1.SelectedValue),
                Convert.ToInt32(radDropDownList2.SelectedValue));
            if (dr != null)
            {
                var OldSessionId = Convert.ToInt32(radDropDownList1.SelectedValue);
                var OldPositionId = Convert.ToInt32(radDropDownList2.SelectedValue);
                txtFee.Text = dr["Fee"].ToString();
                txtFeeVN.Text = dr["FeeInVietNamese"].ToString();

                _OldContent =
                    $"SessionId: {OldSessionId}, PositionId: {OldPositionId}, Fee: '{dr["Fee"]}', FeeInVietnNamese: N'{dr["FeeInVietNamese"]}', CreatedBy: {clsGlobal.UserId}, FeeId: {_FeeId}";
            }
            else
            {
                txtFee.Text = string.Empty;
                txtFeeVN.Text = string.Empty;
            }
        }

        private void Frm_Add_EducationFee_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["frm_EducationFee"] != null)
                (Application.OpenForms["frm_EducationFee"] as frm_EducationFee).RefreshData();
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            try
            {
                var _SessionId = Convert.ToInt32(radDropDownList1.SelectedValue);
                var _PositionId = Convert.ToInt32(radDropDownList2.SelectedValue);
                var _Fee = txtFee.Text.Trim();
                var _FeeInVietNamese = txtFeeVN.Text.Trim();

                if (_Type == 0)
                {
                    _SP = $"Ins_H2_EducationFee";
                    _SPValue =
                        $"SessionId: {_SessionId}, PositionId: {_PositionId}, Fee: '{_Fee}', FeeInVietNamese: N'{_FeeInVietNamese}', CreatedBy: {clsGlobal.UserId}";
                    Utilities.Utilities.SaveHRMLog("H2_EducationFee", _SP, _SPValue, _OldContent);
                    EducationFeeBLL.Insert(_SessionId, _PositionId, _Fee, _FeeInVietNamese, clsGlobal.UserId);
                    MessageBox.Show("Thành công!");
                }
                else
                {
                    _SP = $"Ins_H2_EducationFee";
                    _SPValue =
                        $"SessionId: {_SessionId}, PositionId: {_PositionId}, Fee: '{_Fee}', FeeInVietnNamese: N'{_FeeInVietNamese}', CreatedBy: {clsGlobal.UserId}, FeeId: {_FeeId}";
                    Utilities.Utilities.SaveHRMLog("H2_EducationFee", _SP, _SPValue, _OldContent);
                    EducationFeeBLL.Update(_SessionId, _PositionId, _Fee, _FeeInVietNamese, clsGlobal.UserId, _FeeId);
                    MessageBox.Show("Thành công!");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi cập nhật!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}