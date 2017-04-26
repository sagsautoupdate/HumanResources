using System;
using System.Linq;
using System.Windows.Forms;
using HRMBLL.H0;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Catalogue
{
    public partial class frm_ContractType : RadForm
    {
        private static frm_ContractType s_Instance;

        private string _OldContent = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_ContractType()
        {
            InitializeComponent();
        }

        public static frm_ContractType Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_ContractType();
                return s_Instance;
            }
        }

        public int ContractTypeId { get; set; }

        public string ContractTypeCode { get; set; }

        public string ContractTypeName { get; set; }

        public string ContractFullName { get; set; }

        public double ContractTypeValue { get; set; }

        public string ContractDescription { get; set; }

        public int DataType { get; set; }


        private void InitData(int _ContractId)
        {
            try
            {
                var _Item = ContractTypesBLL.GetById(_ContractId);

                txtContractTypeName.Text = _Item.ContractTypeName.Trim();
                txtContractFullName.Text = _Item.ContractFullName.Trim();
                txtContractTypeCode.Text = _Item.ContractTypeCode.Trim();
                txtContractTypeValue.Text = _Item.ContractTypeValue.ToString();
                txtContractTypeDescription.Text = _Item.ContractTypeDescription.Trim();
                txtContractTypeId.Text = _Item.ContractTypeId.ToString();

                _OldContent =
                    $"ContractTypeCode: {_Item.ContractTypeCode.Trim()}, ContractTypeName: {_Item.ContractTypeName.Trim()}, ContractFullName: {_Item.ContractFullName.Trim()}, ContractTypeValue: {_Item.ContractTypeValue}, ContractTypeDescription: {_Item.ContractTypeDescription.Trim()}, DataType: 1";
            }
            catch
            {
                txtContractTypeName.Text = string.Empty;
                txtContractFullName.Text = string.Empty;
                txtContractTypeCode.Text = string.Empty;
                txtContractTypeValue.Text = "0";
                txtContractTypeDescription.Text = string.Empty;
                txtContractTypeId.Text = "0";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
        }

        private void frm_ContractType_Load(object sender, EventArgs e)
        {
            radGridView1.DataSource = ContractTypesBLL.GetAllToDT(1);
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void radGridView1_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            InitData(Convert.ToInt32(e.CurrentRow.Cells["ContractTypeId"].Value));
        }

        private void frm_ContractType_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var _ContractTypeName = txtContractTypeName.Text.Trim();
            var _ContractFullName = txtContractFullName.Text.Trim();
            var _ContractTypeCode = txtContractTypeCode.Text.Trim();
            var _ContractTypeValue = txtContractTypeValue.Text.Trim();
            var _ContractTypeDescription = txtContractTypeDescription.Text.Trim();
            var _ContractTypeId = "24061991";

            if (ValidateDataByColumn(radGridView1, "ContractTypeName", txtContractTypeName.Text.Trim()))
            {
                _ContractTypeId = txtContractTypeId.Text.Trim();

                _SP = "Upd_H0_ContractTypeV1";
                _SPValue =
                    $"ContractTypeCode: {_ContractTypeCode}, ContractTypeName: {_ContractTypeName}, ContractFullName: {_ContractFullName}, ContractTypeValue: {_ContractTypeValue}, ContractTypeDescription: {_ContractTypeDescription}, ContractTypeId: {_ContractTypeId}, DataType: 1";
            }
            else
            {
                _SP = "Ins_H0_ContractTypeV1";
                _SPValue =
                    $"ContractTypeCode: {_ContractTypeCode}, ContractTypeName: {_ContractTypeName}, ContractFullName: {_ContractFullName}, ContractTypeValue: {_ContractTypeValue}, ContractTypeDescription: {_ContractTypeDescription}, DataType: 1";

                _ContractTypeId = "0";
            }

            if (!_ContractTypeId.Contains("24061991"))
                if ((_ContractTypeName.Length <= 0) || (_ContractTypeName == string.Empty) ||
                    (_ContractFullName.Length <= 0) || (_ContractFullName == string.Empty) ||
                    (_ContractTypeCode.Length <= 0) || (_ContractTypeCode == string.Empty) ||
                    (_ContractTypeDescription.Length <= 0) || (_ContractTypeDescription == string.Empty))
                {
                    var _objBLL = new ContractTypesBLL(Convert.ToInt32(_ContractTypeId), _ContractTypeCode,
                        _ContractTypeName, _ContractTypeDescription);
                    _objBLL.ContractFullName = _ContractFullName;
                    _objBLL.DataType = 1;

                    _objBLL.SaveV1();
                    Utilities.Utilities.SaveHRMLog("H0_ContractTypes", _SP, _SPValue, _OldContent);
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi lưu!", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }

            radGridView1.DataSource = ContractTypesBLL.GetAllToDT(1);
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private bool ValidateDataByColumn(RadGridView rgv, string ColumnName, string DataForCompare)
        {
            var _flag = false;
            var _string = DataForCompare.Trim().Split(' ');
            for (var i = 0; i < rgv.Rows.Count; i++)
            {
                var _temp = rgv.Rows[i].Cells[ColumnName].Value.ToString().Trim().Split(' ');
                if (_string.SequenceEqual(_temp))
                    _flag = true;
            }

            return _flag;
        }
    }
}