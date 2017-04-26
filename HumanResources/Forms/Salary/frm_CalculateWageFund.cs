using System;
using System.Data;
using System.Windows.Forms;
using HRMBLL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Salary
{
    public partial class frm_CalculateWageFund : RadForm
    {
        private readonly int _IsVCQLNC;
        private DateTime _DataDate = DateTime.Now;
        private int _WageFundId;

        public frm_CalculateWageFund(int month, int year, int isvcqlnn)
        {
            _DataDate = new DateTime(year, month, 1);
            _IsVCQLNC = isvcqlnn;
            InitializeComponent();
        }

        private void BindDataToGrid()
        {
            rlbDataDate.Text = _DataDate.Month + "/" + _DataDate.Year;
            rlbIsVCQLNN.Text = Constants.GetVCQLNN_NameById(_IsVCQLNC);
            var list = WorkdayCoefficientEmployeesFinalBLL.GetByDataDate(_DataDate, _IsVCQLNC, Constants.DataType_Import);
            rgwListCoefficient.DataSource = list;
            GridFormatting();

            DataRow row = null;
            var dt = WageFundsBLL.GetByDate(_DataDate.Month, _DataDate.Year);
            if (dt.Rows.Count == 1)
            {
                row = dt.Rows[0];
                _WageFundId =
                    Convert.ToInt32(row[WageFundKeys.Field_Wage_Fund_ID] == DBNull.Value
                        ? "0"
                        : row[WageFundKeys.Field_Wage_Fund_ID].ToString());

                setDataToTextBox(row);
                txtTotalHSTLNS.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_TotalHSTLNS] == DBNull.Value
                            ? "0"
                            : row[WageFundKeys.Field_Wage_Fund_TotalHSTLNS].ToString())
                        .ToString(StringFormat.FormatCoefficient);
                txtDGLNS.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_DGLNS] == DBNull.Value
                        ? "0"
                        : row[WageFundKeys.Field_Wage_Fund_DGLNS].ToString()).ToString(StringFormat.FormatCoefficient);
                txtLNSWageFund.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_LNSWageFund] == DBNull.Value
                            ? "0"
                            : row[WageFundKeys.Field_Wage_Fund_LNSWageFund].ToString())
                        .ToString(StringFormat.FormatCoefficient);

                chkIsLock.Checked = row[WageFundKeys.Field_Wage_Fund_IsLock] == DBNull.Value
                    ? false
                    : Convert.ToBoolean(row[WageFundKeys.Field_Wage_Fund_IsLock].ToString());
                if (chkIsLock.Checked)
                {
                    chkIsLock.Enabled = false;
                    btnSave.Enabled = false;
                    btnCalculateConverion.Enabled = false;
                }
                else
                {
                    chkIsLock.Enabled = true;
                    btnSave.Enabled = true;
                    btnCalculateConverion.Enabled = true;
                }
            }
            else
            {
                var dtTemp = _DataDate.AddMonths(-1);
                dt = WageFundsBLL.GetByDate(dtTemp.Month, dtTemp.Year);
                if (dt.Rows.Count == 1)
                {
                    row = dt.Rows[0];
                    setDataToTextBox(row);
                }
                else
                {
                    txtDGLNS.Text = Constants.DG_LNS.ToString(StringFormat.FormatCoefficient);
                    txtTLTTCLCB.Text = Constants.DG_TLTTCLCB.ToString(StringFormat.FormatCoefficient);
                    txtTLTTCKPN.Text = Constants.DG_TLTTCKPN.ToString(StringFormat.FormatCoefficient);
                    txtDGAnGiuaCa.Text = Constants.DG_AnGiuaCa.ToString(StringFormat.FormatCoefficient);
                    txtGTGC.Text = Constants.DG_GTGC.ToString(StringFormat.FormatCoefficient);
                    txtGTCN.Text = Constants.DG_GTCN.ToString(StringFormat.FormatCoefficient);
                    chkIsLock.Enabled = true;
                    btnSave.Enabled = true;
                    btnCalculateConverion.Enabled = true;
                }
            }
        }

        private void setDataToTextBox(DataRow row)
        {
            txtTLTTCLCB.Text =
                Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_TLTTCLCB] == DBNull.Value
                    ? "0"
                    : row[WageFundKeys.Field_Wage_Fund_TLTTCLCB].ToString()).ToString(StringFormat.FormatCoefficient);
            txtTLTTCKPN.Text =
                Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_TLTTCKPN] == DBNull.Value
                    ? "0"
                    : row[WageFundKeys.Field_Wage_Fund_TLTTCKPN].ToString()).ToString(StringFormat.FormatCoefficient);
            txtDGAnGiuaCa.Text =
                Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_DGAnGiuaCa] == DBNull.Value
                    ? "0"
                    : row[WageFundKeys.Field_Wage_Fund_DGAnGiuaCa].ToString()).ToString(StringFormat.FormatCoefficient);
            txtGTGC.Text =
                Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_GTGC] == DBNull.Value
                    ? "0"
                    : row[WageFundKeys.Field_Wage_Fund_GTGC].ToString()).ToString(StringFormat.FormatCoefficient);
            txtGTCN.Text =
                Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_GTCN] == DBNull.Value
                    ? "0"
                    : row[WageFundKeys.Field_Wage_Fund_GTCN].ToString()).ToString(StringFormat.FormatCoefficient);
            txtDGLNS.Text =
                Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_DGLNS] == DBNull.Value
                    ? "0"
                    : row[WageFundKeys.Field_Wage_Fund_DGLNS].ToString()).ToString(StringFormat.FormatCoefficient);
        }

        private void GridFormatting()
        {
            rgwListCoefficient.MasterTemplate.Columns[0].IsPinned = true;
            rgwListCoefficient.MasterTemplate.Columns[1].IsPinned = true;
            rgwListCoefficient.MasterTemplate.Columns[2].IsPinned = true;
            rgwListCoefficient.Columns[0].FormatString = StringFormat.FormatGridUserId;
            rgwListCoefficient.Columns[3].FormatString = StringFormat.FormatMonthYear;
            rgwListCoefficient.Columns[4].FormatString = StringFormat.FormatCoefficient3DigitGrid;
            rgwListCoefficient.Columns[5].FormatString = StringFormat.FormatCoefficient3DigitGrid;
            rgwListCoefficient.Columns[6].FormatString = StringFormat.FormatCoefficient3DigitGrid;
            rgwListCoefficient.Columns[7].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[8].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[9].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[10].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[11].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[12].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[13].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[14].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[15].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[16].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[17].FormatString = StringFormat.FormatCurrency;
        }

        private void frm_CalculateWageFund_Load(object sender, EventArgs e)
        {
            BindDataToGrid();
        }

        private void rgwListCoefficient_CellClick(object sender, GridViewCellEventArgs e)
        {
            if ((e.ColumnIndex > 3) && (e.ColumnIndex < 24))
            {
                var total =
                    Convert.ToDecimal(
                        rgwListCoefficient.Rows[rgwListCoefficient.RowCount - 1].Cells[e.Column.Name].Value);
                rlbDescription.Text = Util.GetExplainForH1_WorkdayCoefficientEmployeesFinal(e.Column.Name) + " (Total: " +
                                      total.ToString(StringFormat.FormatCoefficient) + ")";
            }
        }

        private void btnCalculateConverion_Click(object sender, EventArgs e)
        {
            var dlg = new frm_CalculateConversionCoefficient(_DataDate.Month, _DataDate.Year, _IsVCQLNC);
            dlg.ShowDialog(this);
            var dtTotal = WorkdayCoefficientEmployeesFinalBLL.GetByDataDateForTotal(_DataDate, _IsVCQLNC,
                Constants.DataType_Import);
            if (dtTotal.Rows.Count == 1)
            {
                var row = dtTotal.Rows[0];
                txtTotalHSTLNS.Text =
                    Convert.ToDecimal(row["TotalHSTLNS"] == DBNull.Value ? "0" : row["TotalHSTLNS"].ToString())
                        .ToString(StringFormat.FormatCoefficient3Digit);
            }

            if ((txtTotalHSTLNS.Text.Trim().Length > 0) && (txtDGLNS.Text.Trim().Length > 0))
            {
                var quyLNS = Convert.ToDecimal(txtTotalHSTLNS.Text.Trim())*Convert.ToDecimal(txtDGLNS.Text.Trim());
                txtLNSWageFund.Text = quyLNS.ToString(StringFormat.FormatCoefficient);
            }
            BindDataToGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_WageFundId == 0)
                    WageFundsBLL.Insert(_DataDate, Convert.ToDecimal(txtDGLNS.Text.Trim()),
                        Convert.ToDouble(txtTotalHSTLNS.Text.Trim()), Convert.ToDecimal(txtLNSWageFund.Text.Trim()),
                        Convert.ToDecimal(txtTLTTCLCB.Text.Trim()), Convert.ToDecimal(txtTLTTCKPN.Text.Trim()),
                        Convert.ToDecimal(txtDGAnGiuaCa.Text.Trim()), Convert.ToDecimal(txtGTGC.Text.Trim()),
                        Convert.ToDecimal(txtGTCN.Text.Trim()), chkIsLock.Checked);
                else
                    WageFundsBLL.Update(_DataDate, Convert.ToDecimal(txtDGLNS.Text.Trim()),
                        Convert.ToDouble(txtTotalHSTLNS.Text.Trim()), Convert.ToDecimal(txtLNSWageFund.Text.Trim()),
                        Convert.ToDecimal(txtTLTTCLCB.Text.Trim()), Convert.ToDecimal(txtTLTTCKPN.Text.Trim()),
                        Convert.ToDecimal(txtDGAnGiuaCa.Text.Trim()), Convert.ToDecimal(txtGTGC.Text.Trim()),
                        Convert.ToDecimal(txtGTCN.Text.Trim()), chkIsLock.Checked, _WageFundId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}