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
    public partial class frm_CalculateConversionCoefficient : RadForm
    {
        private readonly int _IsVCQLNN;
        private readonly DateTime dataDate = DateTime.Now;
        private decimal _TotalHSK;
        private decimal _TotalHSLNS;
        private decimal _TotalHSLNSPCTN;
        private DataTable tbWorkdayCoefficientEmployeesFinal;

        public frm_CalculateConversionCoefficient(int month, int year, int isvcqlnn)
        {
            InitializeComponent();
            dataDate = new DateTime(year, month, 1);
            _IsVCQLNN = isvcqlnn;
            radPanel1.Text = "Tính hệ số quy đổi cho tháng " + month + " năm " + year;
        }

        private void frm_CalculateConversionCoefficient_Load(object sender, EventArgs e)
        {
            var tbTotal = WorkdayCoefficientEmployeesFinalBLL.GetByDataDateForTotal(dataDate, _IsVCQLNN,
                Constants.DataType_Import);

            if (tbTotal.Rows.Count == 1)
            {
                _TotalHSLNS =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalHSLNS"] == DBNull.Value ? 0 : tbTotal.Rows[0]["TotalHSLNS"]);
                _TotalHSLNSPCTN =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalHSLNSPCTN"] == DBNull.Value
                        ? 0
                        : tbTotal.Rows[0]["TotalHSLNSPCTN"]);
                _TotalHSK =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalHSK"] == DBNull.Value ? 0 : tbTotal.Rows[0]["TotalHSK"]);

                txtTotalHSLNS.Text = _TotalHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                txtTotalHSPCTNLNS.Text = _TotalHSLNSPCTN.ToString(StringFormat.FormatCoefficient3Digit);
                txtTotalHSK.Text = _TotalHSK.ToString(StringFormat.FormatCoefficient3Digit);
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
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalNCHLCB"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalNCHLCB"].ToString());
                txtTotalNCHLCB.Text = totalNCHLCB.ToString(StringFormat.FormatCoefficient3Digit);
                var totalNANGC =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalNANGC"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalNANGC"].ToString());
                txtTotalNANGC.Text = totalNANGC.ToString(StringFormat.FormatCoefficient3Digit);

                var totalThuongHSLNS =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalThuongHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalThuongHSLNS"].ToString());
                txtTotalThuongHSLNS.Text = totalThuongHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                var TotalThuongNgayCong =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalThuongNgayCong"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalThuongNgayCong"].ToString());
                txtTotalThuongNgayCong.Text = TotalThuongNgayCong.ToString(StringFormat.FormatCoefficient);
                var TotalThuongQHSLNS =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalThuongQHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalThuongQHSLNS"].ToString());
                txtTotalQuyThuongHSLNS.Text = TotalThuongQHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                /// KET QUA TINH PHUONG AN TIEN BSL
                /// 
                var TotalBSLHSLNS =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalBSLHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalBSLHSLNS"].ToString());
                txtTotalBSLHSLNS.Text = TotalBSLHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                var TotalBSLNgayCong =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalBSLNgayCong"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalBSLNgayCong"].ToString());
                txtTotalBSLNgayCong.Text = TotalBSLNgayCong.ToString(StringFormat.FormatCoefficient);
                var TotalBSLQHSLNS =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalBSLQHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalBSLQHSLNS"].ToString());
                txtTotalBSLQHSLNS.Text = TotalBSLQHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                /// KET QUA TINH PHUONG AN TIEN ATHK
                /// 
                var TotalATHKHSLNS =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalATHKHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalATHKHSLNS"].ToString());
                txtTotalATHKHSLNS.Text = TotalATHKHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                var TotalATHKNgayCong =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalATHKNgayCong"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalATHKNgayCong"].ToString());
                txtTotalATHKNgayCong.Text = TotalATHKNgayCong.ToString(StringFormat.FormatCoefficient);
                var TotalATHKQHSLNS =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalATHKQHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalATHKQHSLNS"].ToString());
                txtTotalATHKQHSLNS.Text = TotalATHKQHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                /// KET QUA TINH PHUONG AN TIEN NGAY LE, TET
                /// 
                var TotalThangCongLeTet =
                    Convert.ToDecimal(tbTotal.Rows[0]["TotalThangCongLeTet"].ToString().Trim().Length <= 0
                        ? "0"
                        : tbTotal.Rows[0]["TotalThangCongLeTet"].ToString());
                txtTotalThangCongLeTet.Text = TotalThangCongLeTet.ToString(StringFormat.FormatCoefficient);

                tbWorkdayCoefficientEmployeesFinal = WorkdayCoefficientEmployeesFinalBLL.GetByDataDateForDetail(
                    dataDate, _IsVCQLNN, Constants.DataType_Import);
            }
        }

        private void Calculate(string fullName)
        {
            var _UserId = 0;
            double _NCQD;
            double _NCDC;
            double _X;
            double _OmDNBHXH;
            double _Om;
            double _OmDN;
            double _KHH;
            double _Co;
            double _TS;
            double _ST;
            double _Khamthai;
            double _TNLD;
            double _F;
            double _Diduong;
            double _CTac;
            double _Fdb;
            double _H1;
            double _H2;
            double _H3;
            double _H4;
            double _H5;
            double _H6;
            double _H7;
            double _DinhChiCT;
            double _Ro;
            double _Ko;
            double _LamthemNTbngay;
            double _LamthemCNbngay;
            double _LamthemLTbngay;
            double _LamthemNTbdem;
            double _LamthemCNbdem;
            double _LamthemLTbdem;
            double _Lamdem;

            double _HSLCB = 0;
            double _HSPCCV = 0;
            double _HSPCTN = 0;
            double _HSPCKV = 0;
            double _HSTNLCD = 0;
            double _HSLNS = 0;
            double _HSK = 0;

            double _NCHLNS = 0;
            double _HSQDNCHL = 0;
            double _HSTLNS = 0;

            double _NCHLCB = 0;
            double _NANGC = 0;

            var _Contract = string.Empty;
            double _BSLNgayCong = 0;
            double _BSLHSLNS = 0;
            double _BLSQHSLNS = 0;
            double _ThuongNgayCong = 0;
            double _ThuongHSLNS = 0;
            double _ThuongQHSLNS = 0;
            double _ATHKNgayCong = 0;
            double _ATHKHSLNS = 0;
            double _ATHKQHSLNS = 0;
            double _ThangCongLeTet = 0;
            double _ThangCongLeTet_TV = 0;

            var _CountBlank_1_15 = 0;
            var _CountBlank_16_31 = 0;

            var _Day1 = string.Empty;
            var _Day2 = string.Empty;
            var _Day3 = string.Empty;
            var _Day4 = string.Empty;
            var _Day5 = string.Empty;
            var _Day6 = string.Empty;
            var _Day7 = string.Empty;
            var _Day8 = string.Empty;
            var _Day9 = string.Empty;
            var _Day10 = string.Empty;
            var _Day11 = string.Empty;
            var _Day12 = string.Empty;
            var _Day13 = string.Empty;
            var _Day14 = string.Empty;
            var _Day15 = string.Empty;
            var _Day16 = string.Empty;
            var _Day17 = string.Empty;
            var _Day18 = string.Empty;
            var _Day19 = string.Empty;
            var _Day20 = string.Empty;
            var _Day21 = string.Empty;
            var _Day22 = string.Empty;
            var _Day23 = string.Empty;
            var _Day24 = string.Empty;
            var _Day25 = string.Empty;
            var _Day26 = string.Empty;
            var _Day27 = string.Empty;
            var _Day28 = string.Empty;
            var _Day29 = string.Empty;
            var _Day30 = string.Empty;
            var _Day31 = string.Empty;


            for (var i = 0; i < tbWorkdayCoefficientEmployeesFinal.Rows.Count; i++)
            {
                var dr = tbWorkdayCoefficientEmployeesFinal.Rows[i];

                _UserId = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId] ==
                          DBNull.Value
                    ? 0
                    : int.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId].ToString());

                _Day1 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day1] ==
                        DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day1].ToString();
                _Day2 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day2] ==
                        DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day2].ToString();
                _Day3 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day3] ==
                        DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day3].ToString();
                _Day4 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day4] ==
                        DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day4].ToString();
                _Day5 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day5] ==
                        DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day5].ToString();
                _Day6 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day6] ==
                        DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day6].ToString();
                _Day7 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day7] ==
                        DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day7].ToString();
                _Day8 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day8] ==
                        DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day8].ToString();
                _Day9 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day9] ==
                        DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day9].ToString();
                _Day10 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day10] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day10].ToString();
                _Day11 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day11] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day11].ToString();
                _Day12 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day12] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day12].ToString();
                _Day13 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day13] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day13].ToString();
                _Day14 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day14] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day14].ToString();
                _Day15 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day15] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day15].ToString();
                _Day16 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day16] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day16].ToString();
                _Day17 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day17] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day17].ToString();
                _Day18 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day18] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day18].ToString();
                _Day19 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day19] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day19].ToString();
                _Day20 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day20] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day20].ToString();
                _Day21 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day21] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day21].ToString();
                _Day22 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day22] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day22].ToString();
                _Day23 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day23] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day23].ToString();
                _Day24 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day24] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day24].ToString();
                _Day25 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day25] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day25].ToString();
                _Day26 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day26] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day26].ToString();
                _Day27 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day27] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day27].ToString();
                _Day28 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day28] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day28].ToString();
                _Day29 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day29] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day29].ToString();
                _Day30 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day30] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day30].ToString();
                _Day31 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day31] ==
                         DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day31].ToString();


                _NCQD = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCQD] ==
                        DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCQD].ToString());
                _NCDC = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCDC] ==
                        DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCDC].ToString());
                _X = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_X] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_X].ToString());
                _OmDNBHXH = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDNBHXH] ==
                            DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDNBHXH]
                            .ToString());
                _Om = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Om] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Om].ToString());
                _OmDN = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDN] ==
                        DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDN].ToString());
                _KHH = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_KHH] ==
                       DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_KHH].ToString());
                _Co = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Co] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Co].ToString());
                _TS = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TS] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TS].ToString());
                _ST = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_ST] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_ST].ToString());
                _Khamthai = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Khamthai] ==
                            DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Khamthai]
                            .ToString());
                _TNLD = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TNLD] ==
                        DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TNLD].ToString());
                _F = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_F] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_F].ToString());
                _Diduong = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Diduong] ==
                           DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Diduong].ToString
                            ());
                _CTac = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CTac] ==
                        DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CTac].ToString());
                _Fdb = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Fdb] ==
                       DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Fdb].ToString());
                _H1 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H1] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H1].ToString());
                _H2 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H2] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H2].ToString());
                _H3 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H3] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H3].ToString());
                _H4 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H4] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H4].ToString());
                _H5 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H5] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H5].ToString());
                _H6 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H6] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H6].ToString());
                _H7 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H7] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H7].ToString());
                _DinhChiCT =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DinhChiCT] ==
                    DBNull.Value
                        ? 0
                        : double.Parse(
                            dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DinhChiCT]
                                .ToString());
                _Ro = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ro] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ro].ToString());
                _Ko = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ko] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ko].ToString());
                _LamthemNTbngay =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemNTbngay] ==
                    DBNull.Value
                        ? 0
                        : double.Parse(
                            dr[
                                WorkdayCoefficientEmployeesFinalKeys
                                    .Field_WorkdayCoefficientEmployeesFinal_LamthemNTbngay].ToString());
                _LamthemCNbngay =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemCNbngay] ==
                    DBNull.Value
                        ? 0
                        : double.Parse(
                            dr[
                                WorkdayCoefficientEmployeesFinalKeys
                                    .Field_WorkdayCoefficientEmployeesFinal_LamthemCNbngay].ToString());
                _LamthemLTbngay =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemLTbngay] ==
                    DBNull.Value
                        ? 0
                        : double.Parse(
                            dr[
                                WorkdayCoefficientEmployeesFinalKeys
                                    .Field_WorkdayCoefficientEmployeesFinal_LamthemLTbngay].ToString());
                _LamthemNTbdem =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemNTbdem] ==
                    DBNull.Value
                        ? 0
                        : double.Parse(
                            dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemNTbdem
                            ].ToString());
                _LamthemCNbdem =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemCNbdem] ==
                    DBNull.Value
                        ? 0
                        : double.Parse(
                            dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemCNbdem
                            ].ToString());
                _LamthemLTbdem =
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemLTbdem] ==
                    DBNull.Value
                        ? 0
                        : double.Parse(
                            dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemLTbdem
                            ].ToString());
                _Lamdem = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Lamdem] ==
                          DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Lamdem].ToString());


                _HSLCB = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLCB] ==
                         DBNull.Value
                    ? 0
                    : Convert.ToDouble(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLCB].ToString());
                _HSPCCV = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCCV] ==
                          DBNull.Value
                    ? 0
                    : Convert.ToDouble(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCCV].ToString());
                _HSPCTN = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCTN] ==
                          DBNull.Value
                    ? 0
                    : Convert.ToDouble(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCTN].ToString());
                _HSPCKV = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCKV] ==
                          DBNull.Value
                    ? 0
                    : Convert.ToDouble(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCKV].ToString());
                _HSTNLCD = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNSPCTN] ==
                           DBNull.Value
                    ? 0
                    : Convert.ToDouble(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNSPCTN]
                            .ToString());
                _HSLNS = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNS] ==
                         DBNull.Value
                    ? 0
                    : Convert.ToDouble(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNS].ToString());
                _HSK = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSK] ==
                       DBNull.Value
                    ? 0
                    : Convert.ToDouble(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSK].ToString());

                _Contract = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Contract] ==
                            DBNull.Value
                    ? string.Empty
                    : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Contract].ToString();


                /////
                ///// CONG THUC TINH CAC HE SO QUY DOI HUONG LNS
                /////
                //// 1.	Ngày công hưởng lương năng suất (NCHLNS)
                ///	    CT Cũ: NCHLNS	=X + TS*0.5	+ ST*0.5 + CTác	+ H1 + H2 + H3*0.75 + H4*0.5 + H5*0.5
                ///	    CT mới: NCHLNS	=X + TS*0.5	+ ST*0.5 + CTác	+ H1 + H2 + H3*0.75 + H4*0.5 + H5*0.5 + F + Fdb
                _NCHLNS = _X + (_TS * 0.5) + (_ST * 0.5) + _CTac + _H1 + _H2 + (_H3 * 0.75) + (_H4 * 0.5) + (_H5 * 0.5) + _F + _Fdb;
                _NCHLNS = HRMUtil.StringFormat.SetRoundCoefficient3Digit(_NCHLNS);

                //// 2.	Hệ số quy đổi ngày công hưởng lương (HSQDNCHL)
                ///HSQDNCHL = NCHLNS/ NCQD( Ngày công quy định)   
                ///
                if (_NCQD > 0)
                {
                    _HSQDNCHL = _NCHLNS / _NCQD;
                    _HSQDNCHL = HRMUtil.StringFormat.SetRoundCoefficient3Digit(_HSQDNCHL);
                }
                else
                {
                    _HSQDNCHL = 0;
                }

                //// 3.	Hệ số tính lương năng suất (HSTLNS)
                ///If (HSQDNCHL > 1)
                ///{ HSTLNS = ( Hệ số LNS + HS TN LCD) * K }
                ///ELSE { HSTLNS = ( Hệ số LNS + HS TN LCD) * K  * HSQDNCHL }

                if (_HSQDNCHL > 1)
                {
                    _HSTLNS = (_HSLNS + _HSTNLCD) * _HSK;
                }
                else
                {
                    _HSTLNS = (_HSLNS + _HSTNLCD) * _HSK * _HSQDNCHL;
                }
                _HSTLNS = HRMUtil.StringFormat.SetRoundCoefficient3Digit(_HSTLNS);

                //////////////////////////////////////////////////////////////////////////////////////////////////
                /////
                ///// CONG THUC TINH CAC HE SO QUY DOI HUONG LCB
                /////
                //// 1.	Ngày công hưởng lương co ban (NCHLCB)
                ////    IF(HSK>0)
                ////       CT cũ  { NCHLCB = TNLD + F + DiDuong + Fdb + (DinhChiCT * 0.5)  + H6}
                ////       CT mới { NCHLCB = TNLD + DiDuong + (DinhChiCT * 0.5)  + H6}
                ////    ELSE
                ////       CT cũ  { NCHLCB = X + TNLD+ F + DiDuong + CTac + Fdb + H1 + H2 + H3 + H4 + H5 + H6 }
                ////       CT mới { NCHLCB = X + TNLD+ DiDuong + CTac + H1 + H2 + H3 + H4 + H5 + H6 + (DinhChiCT * 0.5)}

                if (_HSK > 0)
                {
                    _NCHLCB = _TNLD + _Diduong + (_DinhChiCT * 0.5) + _H6;
                }
                else
                {
                    _NCHLCB = _X + _TNLD + _Diduong + _CTac + _H1 + _H2 + _H3 + _H4 + _H5 + _H6 + (_DinhChiCT * 0.5);
                }


                //////////////////////////////////////////////////////////////////////////////////////////////////
                /// CONG THUC TINH TIEN NGAY AN GIUA CA
                ///	CT: NANGC = X + CTac
                ///
                ///
                _NANGC = _X + _CTac;
                //////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////////////////////////////
                /////
                ///// PHUONG AN TINH TIEN THUONG NAM
                ////
                ////1.Ngày công LV tính thưởng
                if (_Contract.ToUpper().Trim().Equals("HDHN") || _Contract.ToUpper().Trim().Equals("HDTV"))
                {
                    _ThuongNgayCong = 0;
                }
                else
                {
                    if (_HSK <= 0.75)
                    {
                        _ThuongNgayCong = 0;
                    }
                    else
                    {
                        _ThuongNgayCong = _HSK * (_X + _CTac + _H1 + (_H2 * 0.75) + (_H3 * 0.5) + (_H4 * 0.5) + _F + _Fdb);
                    }
                }
                /////
                /// 2. Tinh he so luong nang suat tinh thuong
                if (_Contract.ToUpper().Trim().Equals("HDHN") || _Contract.ToUpper().Trim().Equals("HDTV"))
                {
                    _ThuongHSLNS = 0;
                }
                else
                {
                    if (_HSK <= 0.75)
                    {
                        _ThuongHSLNS = 0;
                    }
                    else
                    {
                        if (_NCQD > 0)
                        {
                            _ThuongHSLNS = (_HSLNS + _HSTNLCD) * _HSK * ((_X + _CTac + _H1 + (_H2 * 0.75) + (_H3 * 0.5) + (_H4 * 0.5) + _F + _Fdb) / _NCQD);
                            _ThuongHSLNS = HRMUtil.StringFormat.SetRoundCoefficient3Digit(_ThuongHSLNS);
                        }
                        else
                        {
                            _ThuongHSLNS = 0;
                        }
                    }
                }
                /////
                /// 3. Tinh he so luong nang suat tinh quy thuong
                if (_Contract.ToUpper().Trim().Equals("HDHN") || _Contract.ToUpper().Trim().Equals("HDTV"))
                {
                    _ThuongQHSLNS = 0;
                }
                else
                {
                    if (_HSK <= 0.75)
                    {
                        _ThuongQHSLNS = 0;
                    }
                    else
                    {
                        if (_NCQD > 0)
                        {
                            _ThuongQHSLNS = (_HSLNS) * ((_X + _CTac + _H1 + (_H2 * 0.75) + (_H3 * 0.5) + (_H4 * 0.5) + _F + _Fdb) / _NCQD);
                            _ThuongQHSLNS = HRMUtil.StringFormat.SetRoundCoefficient3Digit(_ThuongQHSLNS);
                        }
                        else
                        {
                            _ThuongQHSLNS = 0;
                        }
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////
                /////
                ///// PHUONG AN TINH TIEN LE, TET
                ////
                if ((_DinhChiCT + _Ro + _Ko) > 0)
                {
                    _ThangCongLeTet = 0;
                    _ThangCongLeTet_TV = 0;
                }
                else if (_NCDC < _NCQD)
                {
                    double tile = (_NCDC / _NCQD) * 100;

                    if (tile >= 90)
                    {
                        if (_Contract.ToUpper().Trim().Equals("HDVK3T") || _Contract.ToUpper().Trim().Equals("HDVK6T") || _Contract.ToUpper().Trim().Equals("HDVK12T"))
                        {
                            _ThangCongLeTet = 0;
                            _ThangCongLeTet_TV = 1;
                        }
                        else
                        {
                            _ThangCongLeTet = 1;
                            _ThangCongLeTet_TV = 0;
                        }
                    }
                    else
                    {
                        _ThangCongLeTet = 0;
                        _ThangCongLeTet_TV = 0;
                    }
                }

                else if (_Contract.ToUpper().Trim().Equals("HDVK3T") || _Contract.ToUpper().Trim().Equals("HDVK6T") || _Contract.ToUpper().Trim().Equals("HDVK12T"))
                {
                    _ThangCongLeTet = 0;
                    _ThangCongLeTet_TV = 1;
                }
                else
                {
                    _ThangCongLeTet = 1;
                    _ThangCongLeTet_TV = 0;
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////
                /////
                ///// PHUONG AN TINH TIEN BO SUNG DIEU TIET
                ////                
                if (_Contract.ToUpper().Trim().Equals("HDKX") || _Contract.ToUpper().Trim().Equals("HD1N") || _Contract.ToUpper().Trim().Equals("HD2N") || _Contract.ToUpper().Trim().Equals("HD3N"))
                {
                    _BSLHSLNS = _HSTLNS;
                    _BLSQHSLNS = _HSTLNS;
                }
                else if (_HSTLNS > 0 && _Contract.ToUpper().Trim().Equals("HD6T"))
                {
                    _BSLHSLNS = _HSTLNS;
                    _BLSQHSLNS = _HSTLNS;
                }
                else
                {
                    _BSLHSLNS = 0;
                    _BLSQHSLNS = 0;
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////
                /////
                ///// PHUONG AN TINH TIEN AN TOAN HANG KHONG
                /////
                _ATHKNgayCong = _HSK * _X;
                if (_HSK <= 0.75)
                {
                    _ATHKHSLNS = 0;
                    _ATHKQHSLNS = 0;
                }
                else
                {
                    if (_NCQD > 0)
                    {
                        _ATHKHSLNS = ((_HSLNS + _HSTNLCD) * _HSK * (_X + _CTac + _F + _Fdb)) / _NCQD;
                        _ATHKHSLNS = HRMUtil.StringFormat.SetRoundCoefficient3Digit(_ATHKHSLNS);
                        _ATHKQHSLNS = (_HSLNS * _X) / _NCQD;
                        _ATHKQHSLNS = HRMUtil.StringFormat.SetRoundCoefficient3Digit(_ATHKQHSLNS);
                    }
                    else
                    {
                        _ATHKHSLNS = 0;
                        _ATHKQHSLNS = 0;
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////
                ///// Tinh CountBlank_1_15, CountBlank_16_31
                ///// IF (Từ ngày 1 đến ngày 15 có số ngày chưa làm việc >= 15) {CountBlank_1_15 = 1}                
                ///// ELSE IF (Từ ngày 16 đến ngày 31 có số ngày chưa làm việc >= 16) {CountBlank_16_31 = 1}
                /////
                if (_Day1.Length <= 0 && _Day2.Length <= 0 && _Day3.Length <= 0 && _Day4.Length <= 0 && _Day5.Length <= 0
                    && _Day6.Length <= 0 && _Day7.Length <= 0 && _Day8.Length <= 0 && _Day9.Length <= 0 && _Day10.Length <= 0
                    && _Day11.Length <= 0 && _Day12.Length <= 0 && _Day13.Length <= 0 && _Day14.Length <= 0 && _Day15.Length <= 0
                    )
                {
                    _CountBlank_1_15 = 1;
                }
                else
                {
                    _CountBlank_1_15 = 0;
                }
                if (_Day16.Length <= 0 && _Day17.Length <= 0 && _Day18.Length <= 0 && _Day19.Length <= 0 && _Day20.Length <= 0
                    && _Day21.Length <= 0 && _Day22.Length <= 0 && _Day23.Length <= 0 && _Day24.Length <= 0 && _Day25.Length <= 0
                    && _Day26.Length <= 0 && _Day27.Length <= 0 && _Day28.Length <= 0 && _Day29.Length <= 0 && _Day30.Length <= 0 && _Day31.Length <= 0
                    )
                {
                    _CountBlank_16_31 = 1;
                }
                else
                {
                    _CountBlank_16_31 = 0;
                }


                ////////////////////////////////////////////////////////////////////////////////////////////////
                ////
                //////////////////////////////////////////////////////////////////////////////////////////////////
                WorkdayCoefficientEmployeesFinalBLL.UpdateByCalculateConversionCoefficient(_NCHLNS, _HSQDNCHL, _HSTLNS, _NCHLCB, _NANGC, dataDate, DateTime.Now, clsGlobal.UserId, "", _UserId, _BSLNgayCong, _BSLHSLNS, _BLSQHSLNS, _ThuongNgayCong,
                    _ThuongHSLNS, _ThuongQHSLNS, _ATHKNgayCong, _ATHKHSLNS, _ATHKQHSLNS, _ThangCongLeTet, _ThangCongLeTet_TV, _CountBlank_1_15, _CountBlank_16_31, Constants.DataType_Import);

                backgroundWorkerCalculate.ReportProgress(i + 1, fullName);
                Thread.Sleep(10);
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
            var dtResult = WorkdayCoefficientEmployeesFinalBLL.GetByDataDateForTotal(dataDate, _IsVCQLNN,
                Constants.DataType_Import);
            if (dtResult.Rows.Count == 1)
            {
                var totalNCHLNS =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalNCHLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalNCHLNS"].ToString());
                var totalHSQDNCHL =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalHSQDNCHL"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalHSQDNCHL"].ToString());
                var totalHSTLNS =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalHSTLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalHSTLNS"].ToString());

                var totalNCHLCB =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalNCHLCB"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalNCHLCB"].ToString());
                var totalNANGC =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalNANGC"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalNANGC"].ToString());

                txtTotalNCHLNS.Text = totalNCHLNS.ToString(StringFormat.FormatCoefficient);
                txtTotalHSQDNCHL.Text = totalHSQDNCHL.ToString(StringFormat.FormatCoefficient3Digit);
                txtTotalHSTLNS.Text = totalHSTLNS.ToString(StringFormat.FormatCoefficient3Digit);


                txtTotalNCHLCB.Text = totalNCHLCB.ToString(StringFormat.FormatCoefficient3Digit);
                txtTotalNANGC.Text = totalNANGC.ToString(StringFormat.FormatCoefficient3Digit);


                /// KET QUA TINH PHUONG AN TIEN THUONG NAM
                /// 
                var TotalThuongHSLNS =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalThuongHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalThuongHSLNS"].ToString());
                txtTotalThuongHSLNS.Text = TotalThuongHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                var TotalThuongNgayCong =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalThuongNgayCong"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalThuongNgayCong"].ToString());
                txtTotalThuongNgayCong.Text = TotalThuongNgayCong.ToString(StringFormat.FormatCoefficient);
                var TotalThuongQHSLNS =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalThuongQHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalThuongQHSLNS"].ToString());
                txtTotalQuyThuongHSLNS.Text = TotalThuongQHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                /// KET QUA TINH PHUONG AN TIEN BSL
                /// 
                var TotalBSLHSLNS =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalBSLHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalBSLHSLNS"].ToString());
                txtTotalBSLHSLNS.Text = TotalBSLHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                var TotalBSLNgayCong =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalBSLNgayCong"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalBSLNgayCong"].ToString());
                txtTotalBSLNgayCong.Text = TotalBSLNgayCong.ToString(StringFormat.FormatCoefficient);
                var TotalBSLQHSLNS =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalBSLQHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalBSLQHSLNS"].ToString());
                txtTotalBSLQHSLNS.Text = TotalBSLQHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                /// KET QUA TINH PHUONG AN TIEN ATHK
                /// 
                var TotalATHKHSLNS =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalATHKHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalATHKHSLNS"].ToString());
                txtTotalATHKHSLNS.Text = TotalATHKHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                var TotalATHKNgayCong =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalATHKNgayCong"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalATHKNgayCong"].ToString());
                txtTotalATHKNgayCong.Text = TotalATHKNgayCong.ToString(StringFormat.FormatCoefficient);
                var TotalATHKQHSLNS =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalATHKQHSLNS"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalATHKQHSLNS"].ToString());
                txtTotalATHKQHSLNS.Text = TotalATHKQHSLNS.ToString(StringFormat.FormatCoefficient3Digit);
                /// KET QUA TINH PHUONG AN TIEN NGAY LE, TET
                /// 
                var TotalThangCongLeTet =
                    Convert.ToDecimal(dtResult.Rows[0]["TotalThangCongLeTet"].ToString().Trim().Length <= 0
                        ? "0"
                        : dtResult.Rows[0]["TotalThangCongLeTet"].ToString());
                txtTotalThangCongLeTet.Text = TotalThangCongLeTet.ToString(StringFormat.FormatCoefficient);
            }
        }

        private void rbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var isrun = true;
                if ((double.Parse(txtTotalNCHLNS.Text.Trim()) > 0) && (double.Parse(txtTotalHSQDNCHL.Text.Trim()) > 0) &&
                    (double.Parse(txtTotalHSTLNS.Text.Trim()) > 0))
                    if (
                        MessageBox.Show(this, "Bảng hệ số quy đổi đã được tính. Bạn có muốn tính lại không ?",
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
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }
    }
}