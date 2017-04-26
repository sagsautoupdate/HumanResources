using System;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using HRMBLL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Salary
{
    public partial class frm_CalculateSalary : RadForm
    {
        private DateTime DataDate = DateTime.MinValue;
        private bool IsExistsIncome;
        private int IsVCQLNN;
        private DataTable tbWorkdayCoefficientEmployeesFinal;

        public frm_CalculateSalary()
        {
            InitializeComponent();
        }

        private void frm_CalculateSalary_Load(object sender, EventArgs e)
        {
            if (clsGlobal.SalaryDataDate != DateTime.MinValue)
            {
                DataDate = clsGlobal.SalaryDataDate;
                IsVCQLNN = clsGlobal.SalaryIsVCQLNN;
                rlbDataDate.Text = "TÍNH LƯƠNG THÁNG " + clsGlobal.SalaryDataDate.Month + " NĂM " +
                                   clsGlobal.SalaryDataDate.Year;
                rlbSalaryIsVCQLNN.Text = Constants.GetVCQLNN_NameById(IsVCQLNN).ToUpper();
                LoadDataDefault();
                rbtnCalculateSalary.Enabled = true;
            }
            else
            {
                rbtnCalculateSalary.Enabled = false;
            }
        }

        private void LoadDataDefault()
        {
            var dt = WageFundsBLL.GetByDate(DataDate.Month, DataDate.Year);
            DataRow row = null;
            if (dt.Rows.Count == 1)
            {
                row = dt.Rows[0];

                txtTLTTCLCB.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_TLTTCLCB] == DBNull.Value
                            ? "0"
                            : row[WageFundKeys.Field_Wage_Fund_TLTTCLCB].ToString())
                        .ToString(StringFormat.FormatCoefficient);
                txtTLTTCKPN.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_TLTTCKPN] == DBNull.Value
                            ? "0"
                            : row[WageFundKeys.Field_Wage_Fund_TLTTCKPN].ToString())
                        .ToString(StringFormat.FormatCoefficient);
                txtDGLNS.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_DGLNS] == DBNull.Value
                        ? "0"
                        : row[WageFundKeys.Field_Wage_Fund_DGLNS].ToString()).ToString(StringFormat.FormatCoefficient);
                txtDGAnGiuaCa.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_DGAnGiuaCa] == DBNull.Value
                            ? "0"
                            : row[WageFundKeys.Field_Wage_Fund_DGAnGiuaCa].ToString())
                        .ToString(StringFormat.FormatCoefficient);
                txtGTGC.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_GTGC] == DBNull.Value
                        ? "0"
                        : row[WageFundKeys.Field_Wage_Fund_GTGC].ToString()).ToString(StringFormat.FormatCoefficient);
                txtGTCN.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_GTCN] == DBNull.Value
                        ? "0"
                        : row[WageFundKeys.Field_Wage_Fund_GTCN].ToString()).ToString(StringFormat.FormatCoefficient);

                txtTotalHSTLNS.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_TotalHSTLNS] == DBNull.Value
                            ? "0"
                            : row[WageFundKeys.Field_Wage_Fund_TotalHSTLNS].ToString())
                        .ToString(StringFormat.FormatCoefficient);
                txtLNSWageFund.Text =
                    Convert.ToDecimal(row[WageFundKeys.Field_Wage_Fund_LNSWageFund] == DBNull.Value
                            ? "0"
                            : row[WageFundKeys.Field_Wage_Fund_LNSWageFund].ToString())
                        .ToString(StringFormat.FormatCoefficient);
            }

            var tbTotal = WorkdayCoefficientEmployeesFinalBLL.GetByDataDateForTotal(DataDate, IsVCQLNN,
                Constants.DataType_Import);
            if (tbTotal.Rows.Count == 1)
            {
                var totalHSLNS =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalHSLNS"] == DBNull.Value ? 0 : tbTotal.Rows[0]["TotalHSLNS"]);
                var totalHSLNSPCTN =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalHSLNSPCTN"] == DBNull.Value
                        ? 0
                        : tbTotal.Rows[0]["TotalHSLNSPCTN"]);
                var totalHSK =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalHSK"] == DBNull.Value ? 0 : tbTotal.Rows[0]["TotalHSK"]);
                txtTotalHSLNS.Text = totalHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                txtTotalHSPCTNLNS.Text = totalHSLNSPCTN.ToString(StringFormat.FormatCoefficient3Digit);
                txtTotalHSK.Text = totalHSK.ToString(StringFormat.FormatCoefficient3Digit);
                txtTotalEmployees.Text = tbTotal.Rows[0]["Total"].ToString();

                var totalNCHLNS =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalNCHLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalNCHLNS"].ToString());
                var totalHSQDNCHL =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalHSQDNCHL"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalHSQDNCHL"].ToString());
                var totalHSTLNS =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalHSTLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalHSTLNS"].ToString());
                txtTotalNCHLNS.Text = totalNCHLNS.ToString(StringFormat.FormatCoefficient3Digit);
                txtTotalHSQDNCHL.Text = totalHSQDNCHL.ToString(StringFormat.FormatCoefficient3Digit);
                txtTotalHSTLNS.Text = totalHSTLNS.ToString(StringFormat.FormatCoefficient3Digit);

                var totalNCHLCB =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalNCHLCB"] == DBNull.Value
                        ? 0
                        : tbTotal.Rows[0]["TotalNCHLCB"]);
                var totalNANGC =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalNANGC"] == DBNull.Value ? 0 : tbTotal.Rows[0]["TotalNANGC"]);
                txtTotalNCHLCB.Text = totalNCHLCB.ToString(StringFormat.FormatCoefficient3Digit);
                txtTotalNANGC.Text = totalNANGC.ToString(StringFormat.FormatCoefficient3Digit);

                tbWorkdayCoefficientEmployeesFinal = WorkdayCoefficientEmployeesFinalBLL.GetByDataDateForDetail(
                    DataDate, IsVCQLNN, Constants.DataType_Import);
            }

            IsExistsIncome =
                IncomeEmployeesBLL.GetDataTableByFilter(DataDate, Constants.DataType_Run, 0, string.Empty).Rows.Count >
                0;
        }

        private void rbtnCalculateSalary_Click(object sender, EventArgs e)
        {
            try
            {
                var isrun = true;

                if (
                    MessageBox.Show(this,
                        "Vui lòng kiểm tra các đơn giá lương, hệ số quy đổi và tổng nhân sự tính lương. Bạn có chắc các thông số trên đã chính xác và bắt đầu tính lương không ?",
                        "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    isrun = false;
                if (isrun)
                {
                    if (IsExistsIncome)
                        if (
                            MessageBox.Show(this, "Bảng lương đã có, bạn có muốn chạy lại bảng lương không ?",
                                "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            isrun = false;
                    if (isrun)
                        if (!backgroundWorkerCalculate.IsBusy)
                        {
                            var fullName = string.Empty;
                            progressBarCalculate.Maximum = tbWorkdayCoefficientEmployeesFinal.Rows.Count;
                            backgroundWorkerCalculate.RunWorkerAsync(fullName);
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void backgroundWorkerCalculate_DoWork(object sender, DoWorkEventArgs e)
        {
            var fullName = (string) e.Argument;
            Calculate(fullName);
        }

        private void backgroundWorkerCalculate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorkerCalculate.CancellationPending)
            {
                progressBarCalculate.Value1 = e.ProgressPercentage;
                progressBarCalculate.Text = "Đang tính: " + e.ProgressPercentage + "/" +
                                            tbWorkdayCoefficientEmployeesFinal.Rows.Count;
            }
        }

        private void backgroundWorkerCalculate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarCalculate.Text = "Tính các hệ số quy đổi lương chức danh hoàn tất";
        }

        private void Calculate(string fullName)
        {
            var _UserId = 0;
            decimal _NCQD = 0;
            decimal _NCDC = 0;
            decimal _X = 0;
            decimal _OmDNBHXH = 0;
            decimal _Om = 0;
            decimal _OmDN = 0;
            decimal _KHH = 0;
            decimal _Co = 0;
            decimal _TS = 0;
            decimal _ST = 0;
            decimal _Khamthai = 0;
            decimal _TNLD = 0;
            decimal _F = 0;
            decimal _Diduong = 0;
            decimal _CTac = 0;
            decimal _Fdb = 0;
            decimal _H1 = 0;
            decimal _H2 = 0;
            decimal _H3 = 0;
            decimal _H4 = 0;
            decimal _H5 = 0;
            decimal _H6 = 0;
            decimal _H7 = 0;
            decimal _DinhChiCT = 0;
            decimal _Ro = 0;
            decimal _Ko = 0;

            decimal _LamthemNTbngay = 0;
            decimal _LamthemCNbngay = 0;
            decimal _LamthemLTbngay = 0;
            decimal _LamthemNTbdem = 0;
            decimal _LamthemCNbdem = 0;
            decimal _LamthemLTbdem = 0;
            decimal _Lamdem = 0;

            decimal _HSLCB = 0;
            decimal _HSLCB_TinhBu = 0;
            decimal _HSPCCV = 0;
            decimal _HSPCTN = 0;
            decimal _HSPCKV = 0;
            decimal _HSTNLCD = 0;
            decimal _HSLNS = 0;
            decimal _HSK = 0;
            decimal _LNS = 0;
            var _HSTLNS = 0m;
            decimal _DGGC_LCB = 0;

            var _Contract = string.Empty;
            decimal _LCB = 0;
            decimal _NCHLCB = 0;
            decimal _TLTTC_LCB = 0;

            decimal _PCCV = 0;
            decimal _PCTN = 0;
            decimal _NANGC = 0;
            decimal _TienAnGiuaCa = 0;
            decimal _DonGiaAnGiuaCa = 0;

            decimal _BHXH = 0;
            decimal _BHYT = 0;
            decimal _BHTN = 0;
            decimal _BSL = 0;
            var _CountBlank_1_15 = 0;
            var _CountBlank_16_31 = 0;
            decimal _TLTTC_KPN = 0;
            decimal _TienThemGio_BNgay = 0;
            decimal _TienThemGio_BNgayChiuThue = 0;
            decimal _TienThemGio_BDem = 0;
            decimal _TienThemGio_BDemChiuThue = 0;
            decimal _TienThemGio = 0;
            decimal _TienLamDem = 0;
            decimal _NguoiPThuoc = 0;
            decimal _DoanPhiCD = 0;
            decimal _GTGC = 0;
            decimal _GTCN = 0;
            decimal _LuongChiuThue;
            decimal _ThueThuNhap = 0;


            decimal _TotalIncome = 0;
            decimal _ThucLinh = 0;

            var DGLNS = decimal.Parse(txtDGLNS.Text);
            var TLTTC_LCB = decimal.Parse(txtTLTTCLCB.Text);

            for (var i = 0; i < tbWorkdayCoefficientEmployeesFinal.Rows.Count; i++)
            {
                var dr = tbWorkdayCoefficientEmployeesFinal.Rows[i];

                _UserId = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId] ==
                          DBNull.Value
                    ? 0
                    : int.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId].ToString());

                _NCQD = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCQD] ==
                        DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCQD].ToString());
                _NCDC = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCDC] ==
                        DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCDC].ToString());
                _X = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_X] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_X].ToString());
                _OmDNBHXH = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDNBHXH] ==
                            DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDNBHXH]
                            .ToString());
                _Om = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Om] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Om].ToString());
                _OmDN = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDN] ==
                        DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDN].ToString());
                _KHH = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_KHH] ==
                       DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_KHH].ToString());
                _Co = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Co] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Co].ToString());
                _TS = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TS] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TS].ToString());
                _ST = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_ST] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_ST].ToString());
                _Khamthai = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Khamthai] ==
                            DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Khamthai]
                            .ToString());
                _TNLD = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TNLD] ==
                        DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TNLD].ToString());
                _F = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_F] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_F].ToString());
                _Diduong = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Diduong] ==
                           DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Diduong].ToString
                            ());
                _CTac = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CTac] ==
                        DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CTac].ToString());
                _Fdb = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Fdb] ==
                       DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Fdb].ToString());
                _H1 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H1] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H1].ToString());
                _H2 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H2] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H2].ToString());
                _H3 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H3] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H3].ToString());
                _H4 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H4] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H4].ToString());
                _H5 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H5] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H5].ToString());
                _H6 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H6] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H6].ToString());
                _H7 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H7] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H7].ToString());
                _DinhChiCT =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DinhChiCT] ==
                    DBNull.Value
                        ? 0
                        : decimal.Parse(
                            dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DinhChiCT]
                                .ToString());
                _Ro = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ro] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ro].ToString());
                _Ko = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ko] == DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ko].ToString());
                _LamthemNTbngay =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemNTbngay] ==
                    DBNull.Value
                        ? 0
                        : decimal.Parse(
                            dr[
                                WorkdayCoefficientEmployeesFinalKeys
                                    .Field_WorkdayCoefficientEmployeesFinal_LamthemNTbngay].ToString());
                _LamthemCNbngay =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemCNbngay] ==
                    DBNull.Value
                        ? 0
                        : decimal.Parse(
                            dr[
                                WorkdayCoefficientEmployeesFinalKeys
                                    .Field_WorkdayCoefficientEmployeesFinal_LamthemCNbngay].ToString());
                _LamthemLTbngay =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemLTbngay] ==
                    DBNull.Value
                        ? 0
                        : decimal.Parse(
                            dr[
                                WorkdayCoefficientEmployeesFinalKeys
                                    .Field_WorkdayCoefficientEmployeesFinal_LamthemLTbngay].ToString());
                _LamthemNTbdem =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemNTbdem] ==
                    DBNull.Value
                        ? 0
                        : decimal.Parse(
                            dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemNTbdem
                            ].ToString());
                _LamthemCNbdem =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemCNbdem] ==
                    DBNull.Value
                        ? 0
                        : decimal.Parse(
                            dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemCNbdem
                            ].ToString());
                _LamthemLTbdem =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemLTbdem] ==
                    DBNull.Value
                        ? 0
                        : decimal.Parse(
                            dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemLTbdem
                            ].ToString());
                _Lamdem = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Lamdem] ==
                          DBNull.Value
                    ? 0
                    : decimal.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Lamdem].ToString());


                _HSLCB = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLCB] ==
                         DBNull.Value
                    ? 0
                    : Convert.ToDecimal(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLCB].ToString());
                _HSLCB_TinhBu =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLCB_TinhBu] ==
                    DBNull.Value
                        ? 0
                        : Convert.ToDecimal(
                            dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLCB_TinhBu]
                                .ToString());
                _HSPCCV = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCCV] ==
                          DBNull.Value
                    ? 0
                    : Convert.ToDecimal(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCCV].ToString());
                _HSPCTN = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCTN] ==
                          DBNull.Value
                    ? 0
                    : Convert.ToDecimal(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCTN].ToString());
                _HSPCKV = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCKV] ==
                          DBNull.Value
                    ? 0
                    : Convert.ToDecimal(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCKV].ToString());
                _HSTNLCD = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNSPCTN] ==
                           DBNull.Value
                    ? 0
                    : Convert.ToDecimal(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNSPCTN]
                            .ToString());
                _HSLNS = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNS] ==
                         DBNull.Value
                    ? 0
                    : Convert.ToDecimal(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNS].ToString());
                _HSK = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSK] ==
                       DBNull.Value
                    ? 0
                    : Convert.ToDecimal(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSK].ToString());


                _Contract = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Contract] ==
                            DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Contract].ToString();
                _NCHLCB = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCHLCB] ==
                          DBNull.Value
                    ? 0
                    : Convert.ToDecimal(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCHLCB].ToString());
                _NANGC = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NANGC] ==
                         DBNull.Value
                    ? 0
                    : Convert.ToDecimal(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NANGC].ToString());
                _NguoiPThuoc =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NguoiPThuoc] ==
                    DBNull.Value
                        ? 0
                        : Convert.ToDecimal(
                            dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NguoiPThuoc]
                                .ToString());

                _TLTTC_LCB = txtTLTTCLCB.Text.Trim().Length <= 0 ? 0 : decimal.Parse(txtTLTTCLCB.Text.Trim());
                _TLTTC_KPN = txtTLTTCKPN.Text.Trim().Length <= 0 ? 0 : decimal.Parse(txtTLTTCKPN.Text.Trim());
                _GTGC = txtGTGC.Text.Trim().Length <= 0 ? 0 : decimal.Parse(txtGTGC.Text.Trim());
                _GTCN = txtGTCN.Text.Trim().Length <= 0 ? 0 : decimal.Parse(txtGTCN.Text.Trim());
                _DonGiaAnGiuaCa = txtDGAnGiuaCa.Text.Trim().Length <= 0 ? 0 : decimal.Parse(txtDGAnGiuaCa.Text.Trim());


                _CountBlank_1_15 =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CountBlank_1_15] ==
                    DBNull.Value
                        ? 0
                        : int.Parse(
                            dr[
                                WorkdayCoefficientEmployeesFinalKeys
                                    .Field_WorkdayCoefficientEmployeesFinal_CountBlank_1_15].ToString());
                _CountBlank_16_31 =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CountBlank_16_31] ==
                    DBNull.Value
                        ? 0
                        : int.Parse(
                            dr[
                                WorkdayCoefficientEmployeesFinalKeys
                                    .Field_WorkdayCoefficientEmployeesFinal_CountBlank_16_31].ToString());


                _LNS = DGLNS*_HSTLNS;


                _LCB = _HSLCB*_TLTTC_LCB*_NCHLCB/_NCQD;


                _PCCV = _HSPCCV*_TLTTC_LCB;


                _PCTN = _HSPCTN*_TLTTC_LCB;


                _TienAnGiuaCa = _DonGiaAnGiuaCa/_NCQD*_NANGC;


                if (_HSLCB == 0)
                {
                    _BHXH = 0;
                }
                else
                {
                    if (_Contract.ToUpper().Trim().Equals("HDTV"))
                    {
                        _BHXH = 0;
                    }
                    else
                    {
                        if ((_CountBlank_1_15 == 1) || (_CountBlank_16_31 == 1))
                        {
                            _BHXH = 0;
                        }
                        else
                        {
                            if (_TS + _ST + _KHH + _Om + _OmDN + _OmDNBHXH > 13)
                                _BHXH = 0;
                            else
                                _BHXH = (_HSLCB + _HSLCB_TinhBu + _HSPCCV)*_TLTTC_KPN*(decimal) 0.08;
                        }
                    }
                }


                if (_HSLCB == 0)
                {
                    _BHYT = 0;
                }
                else
                {
                    if (_Contract.ToUpper().Trim().Equals("HDTV"))
                    {
                        _BHYT = 0;
                    }
                    else
                    {
                        if ((_CountBlank_1_15 == 1) || (_CountBlank_16_31 == 1))
                        {
                            _BHYT = 0;
                        }
                        else
                        {
                            if (_TS + _ST + _KHH + _Om + _OmDN + _OmDNBHXH > 13)
                                _BHYT = 0;
                            else
                                _BHYT = (_HSLCB + _HSLCB_TinhBu + _HSPCCV)*_TLTTC_KPN*(decimal) 0.015;
                        }
                    }
                }


                if (_HSLCB == 0)
                {
                    _BHTN = 0;
                }
                else
                {
                    if ((_Contract.ToUpper().Trim().Equals("HDKX") == false) &&
                        (_Contract.ToUpper().Trim().Equals("HD3N") == false) &&
                        (_Contract.ToUpper().Trim().Equals("HD2N") == false) &&
                        (_Contract.ToUpper().Trim().Equals("HD1N") == false))
                    {
                        _BHTN = 0;
                    }
                    else
                    {
                        if ((_CountBlank_1_15 == 1) || (_CountBlank_16_31 == 1))
                        {
                            _BHTN = 0;
                        }
                        else
                        {
                            if (_TS + _ST + _KHH + _Om + _OmDN + _OmDNBHXH > 13)
                                _BHTN = 0;
                            else
                                _BHTN = (_HSLCB + _HSLCB_TinhBu + _HSPCCV)*_TLTTC_KPN*(decimal) 0.01;
                        }
                    }
                }


                if (_DinhChiCT + _Ro + _Ko >= 14)
                {
                    _BSL = 0;
                }
                else
                {
                    if (_OmDNBHXH + _Om + _OmDNBHXH + _Co >= 14)
                        _BSL = 0;
                    else
                        _BSL = _BHXH + _BHYT + _BHTN;
                }
                _DGGC_LCB = (_HSLCB + _HSPCCV)*_TLTTC_LCB/(_NCQD*8);


                _TienThemGio_BNgay = _DGGC_LCB*(_LamthemNTbngay*(decimal) 1.5 + _LamthemCNbngay*2 + _LamthemLTbngay*3);


                _TienThemGio_BNgayChiuThue = _DGGC_LCB*(_LamthemNTbngay + _LamthemCNbngay + _LamthemLTbngay);


                _TienThemGio_BDem = _DGGC_LCB*(_LamthemNTbdem*(decimal) 1.5 + _LamthemCNbdem*2 + _LamthemLTbdem*3)*
                                    (decimal) 1.3;


                _TienThemGio_BDemChiuThue = _DGGC_LCB*(_LamthemNTbdem + _LamthemCNbdem + _LamthemLTbdem);


                _TienThemGio = _TienThemGio_BNgay + _TienThemGio_BDem;


                _TienLamDem = _DGGC_LCB*_Lamdem*(decimal) 0.3;


                _DoanPhiCD = (_LNS + _LCB + _PCCV + _PCTN + _BSL)/100;


                _LuongChiuThue = _LNS + _LCB + _PCCV + _PCTN + _BSL + _TienThemGio_BNgayChiuThue +
                                 _TienThemGio_BDemChiuThue - _NguoiPThuoc*_GTGC - _GTCN - _BHXH - _BHYT - _BHTN;


                if (_LuongChiuThue <= 0)
                {
                    _ThueThuNhap = 0;
                }
                else
                {
                    if (_LuongChiuThue <= 5000000)
                    {
                        _ThueThuNhap = _LuongChiuThue*(decimal) 0.05;
                    }
                    else
                    {
                        if (_LuongChiuThue <= 10000000)
                        {
                            _ThueThuNhap = (_LuongChiuThue - 5000000)*(decimal) 0.1 + 250000;
                        }
                        else
                        {
                            if (_LuongChiuThue <= 18000000)
                            {
                                _ThueThuNhap = (_LuongChiuThue - 10000000)*(decimal) 0.15 + 750000;
                            }
                            else
                            {
                                if (_LuongChiuThue <= 32000000)
                                {
                                    _ThueThuNhap = (_LuongChiuThue - 18000000)*(decimal) 0.2 + 1950000;
                                }
                                else
                                {
                                    if (_LuongChiuThue <= 52000000)
                                    {
                                        _ThueThuNhap = (_LuongChiuThue - 32000000)*(decimal) 0.25 + 4750000;
                                    }
                                    else
                                    {
                                        if (_LuongChiuThue <= 80000000)
                                        {
                                            _ThueThuNhap = (_LuongChiuThue - 52000000)*(decimal) 0.3 + 9750000;
                                        }
                                        else
                                        {
                                            if (_LuongChiuThue > 80000000)
                                                _ThueThuNhap = (_LuongChiuThue - 80000000)*(decimal) 0.35 + 18150000;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                if (_HSK == 0)
                    _TotalIncome = _LCB + _PCCV + _PCTN + _TienAnGiuaCa + _BSL + _TienThemGio + _TienLamDem;
                else
                    _TotalIncome = _LNS + _LCB + _PCCV + _PCTN + _TienAnGiuaCa + _BSL + _TienThemGio + _TienLamDem;

                _ThucLinh = _TotalIncome - _BHXH - _BHYT - _BHTN - _DoanPhiCD - _ThueThuNhap;

                if (IsExistsIncome)
                    IncomeEmployeesBLL.Update(DataDate, _UserId, _LNS, _DGGC_LCB, _LCB, _PCCV, _PCTN, _TienAnGiuaCa,
                        _BSL, _TienThemGio_BNgay, _TienThemGio_BNgayChiuThue, _TienThemGio_BDem,
                        _TienThemGio_BDemChiuThue, _TienThemGio, _TienLamDem, _TotalIncome, _LuongChiuThue, _BHXH, _BHYT,
                        _BHTN, _DoanPhiCD, _LuongChiuThue, _ThueThuNhap, _ThucLinh, 0, _ThucLinh, DateTime.Now,
                        clsGlobal.UserId, DateTime.Now, clsGlobal.UserId, false, Constants.DataType_Run, string.Empty,
                        IsVCQLNN);
                else
                    IncomeEmployeesBLL.Insert(DataDate, _UserId, _LNS, _DGGC_LCB, _LCB, _PCCV, _PCTN, _TienAnGiuaCa,
                        _BSL, _TienThemGio_BNgay, _TienThemGio_BNgayChiuThue, _TienThemGio_BDem,
                        _TienThemGio_BDemChiuThue, _TienThemGio, _TienLamDem, _TotalIncome, _LuongChiuThue, _BHXH, _BHYT,
                        _BHTN, _DoanPhiCD, _LuongChiuThue, _ThueThuNhap, _ThucLinh, 0, _ThucLinh, DateTime.Now,
                        clsGlobal.UserId, Constants.DataType_Run, string.Empty, IsVCQLNN);
                backgroundWorkerCalculate.ReportProgress(i + 1, fullName);
                Thread.Sleep(10);
            }
        }
    }
}