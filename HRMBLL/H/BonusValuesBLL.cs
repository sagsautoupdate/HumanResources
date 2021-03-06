using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.BLLHelper;
using HRMDAL.H;
using HRMUtil.KeyNames.H;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H
{
    public class BonusValuesBLL
    {
        #region constructors

        public BonusValuesBLL(int bonusValueId, double bonusValue, int bonusYear)
        {
            BonusValueId = bonusValueId;
            BonusValue = bonusValue;
            BonusYear = bonusYear;
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public int BonusValueId { get; set; }

        public double BonusValue { get; set; }

        public int BonusYear { get; set; }

        public int BonusNameId { get; set; }

        public string BonusName { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public string UserCode { get; set; }

        public int Type { get; set; }

        public string FullName { get; set; }

        public string PositionName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string AccountNo { get; set; }

        public string ATMNo { get; set; }

        #endregion

        #region public methods insert, update, delete

        public long Save()
        {
            var objDAL = new BonusValuesDAL();
            if (BonusValueId <= 0)
                return objDAL.Insert(BonusNameId, UserCode, BonusValue, BonusYear);
            return -1;
        }

        public static int ImportBonus(int bonusNameId, int year, string fileName, string sheetName)
        {
            int returnValue = 1, countColumn = 0;
            var helper = new ExcelHelper(fileName);

            try
            {
                var dt = helper.ReadDataFromExcelToDataTable(sheetName);
                for (var i = 0; i < dt.Columns.Count; i++)
                    if (dt.Columns[i].ColumnName.Equals("Mã NV"))
                        countColumn++;
                    else if (dt.Columns[i].ColumnName.Equals("Họ và tên"))
                        countColumn++;
                    else if (dt.Columns[i].ColumnName.Equals("Thực lĩnh"))
                        countColumn++;
                if (countColumn < 3)
                    returnValue = -1;
                else
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        var dr = dt.Rows[i];
                        var userCode = dr["Mã NV"].ToString().Trim();
                        var sbonusValue = dr["Thực lĩnh"].ToString().Trim();
                        var bonusValue = sbonusValue.Length <= 0 ? 0 : double.Parse(sbonusValue);


                        /////////////////////////////////////////////////////////
                        /// import so thang duoc huong, va Thue Thu Nhap
                        /// 
                        //string sMonths = dr["Sthang"].ToString().Trim();
                        //string sThueTN = dr["Thuế TN"].ToString().Trim();
                        //double months = sMonths.Length <= 0 ? 0 : double.Parse(sMonths);
                        //double thueTN = sThueTN.Length <= 0 ? 0 : double.Parse(sThueTN);
                        /////////////////////////////////////////////////////////
                        if ((userCode.Trim().Length > 0) && (sbonusValue.Length > 0))
                            new BonusValuesDAL().Insert(bonusNameId, userCode, bonusValue, year);
                        else if (userCode.Trim().ToLower().Equals("stop"))
                            break;
                    }
            }
            catch
            {
                returnValue = -2;
            }

            return returnValue;
        }

        public static int ImportLastYearBonus(int bonusNameId, int year, string fileName, string sheetName)
        {
            var returnValue = 1;
            var helper = new ExcelHelper(fileName);


            var BonusNameId_DieuTiecLuong_DuocLinh = 9;
            // su dung cho khoan tien thuong cuoi nam 2011 ngay nhan 06/01/2012
            var BonusNameId_DieuTiecLuong_DaLinh = 10;
            // su dung cho khoan tien thuong cuoi nam 2011 ngay nhan 06/01/2012
            var BonusNameId_DieuTiecLuong_BSDT = 11; // su dung cho khoan tien thuong cuoi nam 2011 ngay nhan 06/01/2012
            var BonusNameId_HoanThanhNhiemVu_DuocLinh = 12;
            // su dung cho khoan tien thuong cuoi nam 2011 ngay nhan 06/01/2012
            var BonusNameId_HoanThanhNhiemVu_DaLinh = 13;
            // su dung cho khoan tien thuong cuoi nam 2011 ngay nhan 06/01/2012
            var BonusNameId_HoanThanhNhiemVu_BSDT = 14;
            // su dung cho khoan tien thuong cuoi nam 2011 ngay nhan 06/01/2012
            var BonusNameId_TongCong = 18; // su dung cho khoan tien thuong cuoi nam 2011 ngay nhan 06/01/2012
            var BonusNameId_ThueThuNhapTamTinh_TongSoPhaiNop = 19;
            // su dung cho khoan tien thuong cuoi nam 2011 ngay nhan 06/01/2012

            // || int BonusNameId_ThueThuNhapTamTinh_DaNop = 20;
            // || int BonusNameId_ThueThuNhapTamTinh_ConPhaiNop = 21;
            // || int BonusNameId_ThanhTien = 22;
            // || int BonusNameId_TTNQTThueConPhaiNop = 23;
            // || int BonusNameId_ThuNhapSauThue = 24;

            try
            {
                var dt = helper.ReadDataFromExcelToDataTable(sheetName);


                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var userCode = dr["Mã NV"].ToString().Trim();


                    var DieuTiecLuong_DuocLinh_9 = dr["T1"].ToString().Trim(); //1
                    var DieuTiecLuong_DaLinh_10 = dr["T2"].ToString().Trim(); //2
                    var DieuTiecLuong_BSDT_11 = dr["T3"].ToString().Trim(); //3
                    var HoanThanhNhiemVu_DuocLinh_12 = dr["T4"].ToString().Trim(); //4
                    var HoanThanhNhiemVu_DaLinh_13 = dr["T5"].ToString().Trim(); //5
                    var HoanThanhNhiemVu_BSDT_14 = dr["T6"].ToString().Trim(); //6
                    var TongCong_18 = dr["T7"].ToString().Trim(); //10
                    var ThueThuNhapTamTinh_TongSoPhaiNop_19 = dr["T8"].ToString().Trim();

                    //string ATHK_DuocLinh_15 = dr["T15"].ToString().Trim();//7
                    //string ATHK_DaLinh_16 = dr["T16"].ToString().Trim();//8                                       
                    // || string ATHK_BSDT_17 = dr["T17"].ToString().Trim();                    
                    // || string ThueThuNhapTamTinh_DaNop_20 = dr["T20"].ToString().Trim();
                    // || string ThueThuNhapTamTinh_ConPhaiNop_21 = dr["T21"].ToString().Trim();
                    // || string ThanhTien_22 = dr["T22"].ToString().Trim();
                    // || string TTNQTThueConPhaiNop_23 = dr["T23"].ToString().Trim();
                    // || string ThuNhapSauThue_24 = dr["T24"].ToString().Trim();

                    if (userCode.Trim().ToLower().Equals("stop"))
                        break;

                    ///// dieu tiet
                    if ((userCode.Trim().Length > 0) && (DieuTiecLuong_DuocLinh_9.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_DieuTiecLuong_DuocLinh, userCode,
                            Convert.ToDouble(DieuTiecLuong_DuocLinh_9), year);
                    if ((userCode.Trim().Length > 0) && (DieuTiecLuong_DaLinh_10.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_DieuTiecLuong_DaLinh, userCode,
                            Convert.ToDouble(DieuTiecLuong_DaLinh_10), year);
                    if ((userCode.Trim().Length > 0) && (DieuTiecLuong_BSDT_11.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_DieuTiecLuong_BSDT, userCode,
                            Convert.ToDouble(DieuTiecLuong_BSDT_11), year);

                    if ((userCode.Trim().Length > 0) && (HoanThanhNhiemVu_DuocLinh_12.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_HoanThanhNhiemVu_DuocLinh, userCode,
                            Convert.ToDouble(HoanThanhNhiemVu_DuocLinh_12), year);
                    if ((userCode.Trim().Length > 0) && (HoanThanhNhiemVu_DaLinh_13.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_HoanThanhNhiemVu_DaLinh, userCode,
                            Convert.ToDouble(HoanThanhNhiemVu_DaLinh_13), year);
                    if ((userCode.Trim().Length > 0) && (HoanThanhNhiemVu_BSDT_14.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_HoanThanhNhiemVu_BSDT, userCode,
                            Convert.ToDouble(HoanThanhNhiemVu_BSDT_14), year);
                    if ((userCode.Trim().Length > 0) && (TongCong_18.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_TongCong, userCode, Convert.ToDouble(TongCong_18), year);
                    if ((userCode.Trim().Length > 0) && (ThueThuNhapTamTinh_TongSoPhaiNop_19.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_ThueThuNhapTamTinh_TongSoPhaiNop, userCode,
                            Convert.ToDouble(ThueThuNhapTamTinh_TongSoPhaiNop_19), year);

                    //if (userCode.Trim().Length > 0 && ThueThuNhapTamTinh_DaNop_20.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_ThueThuNhapTamTinh_DaNop, userCode, Convert.ToDouble(ThueThuNhapTamTinh_DaNop_20), year);
                    //}
                    //if (userCode.Trim().Length > 0 && ThueThuNhapTamTinh_ConPhaiNop_21.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_ThueThuNhapTamTinh_ConPhaiNop, userCode, Convert.ToDouble(ThueThuNhapTamTinh_ConPhaiNop_21), year);
                    //}

                    //if (userCode.Trim().Length > 0 && ThanhTien_22.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_ThanhTien, userCode, Convert.ToDouble(ThanhTien_22), year);
                    //}

                    //if (userCode.Trim().Length > 0 && TTNQTThueConPhaiNop_23.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_TTNQTThueConPhaiNop, userCode, Convert.ToDouble(TTNQTThueConPhaiNop_23), year);
                    //}

                    //if (userCode.Trim().Length > 0 && ThuNhapSauThue_24.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_ThuNhapSauThue, userCode, Convert.ToDouble(ThuNhapSauThue_24), year);
                    //}
                }
            }
            catch
            {
                returnValue = -2;
            }

            return returnValue;
        }

        public static int ImportLastYearBSDT(int year, string fileName, string sheetName)
        {
            var returnValue = 1;
            var helper = new ExcelHelper(fileName);
            //int BonusNameId_BSDT_DuocLinh = 26;// BSDT 2011 Dot II linh thang 16/2/2012
            //int BonusNameId_BSDT_DaLinh = 27;//BSDT 2011 Dot II linh thang 16/2/2012
            //int BonusNameId_BSDT = 28;//BSDT 2011 Dot II linh thang 16/2/2012

            //int BonusNameId_BSDT_TongCong = 29;//BSDT 2011 Dot III linh thang 22/2/2012
            //int BonusNameId_BSDT_TongSoPhaiNop = 30;//BSDT 2011 Dot III linh thang 22/2/2012
            //int BonusNameId_BSDT_DaNop = 31;//BSDT 2011 Dot III linh thang 22/2/2012
            //int BonusNameId_BSDT_ConPhaiNop = 32;//BSDT 2011 Dot III linh thang 22/2/2012
            //int BonusNameId_BSDT_ThanhTien = 33;//BSDT 2011 Dot III linh thang 22/2/2012

            //int BonusNameId_BSDTLT_2008 = 42; //BSDT 2011 Dot  4 linh thang 2/3/2012
            //int BonusNameId_QuyetToanTTN_2008 = 43;//BSDT 2011 Dot 4 linh thang 2/3/2012
            //int BonusNameId_BSDTLTDaNhan = 44;//BSDT 2011 Dot 4 linh thang 2/3/2012

            var BonusNameId_BSDTLT = 1; //BSDT 2011 Dot  5 linh thang 8/3/2012
            var BonusNameId_TTN2005 = 3; //BSDT 2011 Dot 5 linh thang 8/3/2012
            var BonusNameId_TTN2006 = 4; //BSDT 2011 Dot 5 linh thang 8/3/2012

            try
            {
                var dt = helper.ReadDataFromExcelToDataTable(sheetName);


                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var userCode = dr["Mã NV"].ToString().Trim();

                    //string BSDT_DuocLinh_26 = dr["T1"].ToString().Trim();
                    //string BSDT_DaLinh_27 = dr["T2"].ToString().Trim();
                    //string BSDT_28 = dr["T3"].ToString().Trim();
                    //string BSDT_TongCong_29 = dr["T1"].ToString().Trim();
                    //string BSDT_TongSoPhaiNop_30 = dr["T2"].ToString().Trim();
                    //string BSDT_DaNop_31 = dr["T3"].ToString().Trim();
                    //string BSDT_ConPhaiNop_32 = dr["T4"].ToString().Trim();
                    //string BSDT_ThanhTien_33 = dr["T5"].ToString().Trim();

                    //string BSDTLT2008_42 = dr["T1"].ToString().Trim();
                    //string QuyetToanTTN2008_43 = dr["T2"].ToString().Trim();
                    //string BSDTLTDaNhan_44 = dr["T3"].ToString().Trim();

                    var BSDT_BSDTLT_1 = dr["T1"].ToString().Trim();
                    var BSDT_TTN2005_3 = dr["T2"].ToString().Trim();
                    var BSDT_TTN2006_4 = dr["T3"].ToString().Trim();

                    if (userCode.Trim().ToLower().Equals("stop"))
                        break;


                    //if (userCode.Trim().Length > 0 && BSDT_DuocLinh_26.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_BSDT_DuocLinh, userCode, Convert.ToDouble(BSDT_DuocLinh_26), year);
                    //}
                    //if (userCode.Trim().Length > 0 && BSDT_DaLinh_27.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_BSDT_DaLinh, userCode, Convert.ToDouble(BSDT_DaLinh_27), year);
                    //}
                    //if (userCode.Trim().Length > 0 && BSDT_28.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_BSDT, userCode, Convert.ToDouble(BSDT_28), year);
                    //}

                    //if (userCode.Trim().Length > 0 && BSDT_TongCong_29.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_BSDT_TongCong, userCode, Convert.ToDouble(BSDT_TongCong_29), year);
                    //}
                    //if (userCode.Trim().Length > 0 && BSDT_TongSoPhaiNop_30.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_BSDT_TongSoPhaiNop, userCode, Convert.ToDouble(BSDT_TongSoPhaiNop_30), year);
                    //}
                    //if (userCode.Trim().Length > 0 && BSDT_DaNop_31.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_BSDT_DaNop, userCode, Convert.ToDouble(BSDT_DaNop_31), year);
                    //}
                    //if (userCode.Trim().Length > 0 && BSDT_ConPhaiNop_32.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_BSDT_ConPhaiNop, userCode, Convert.ToDouble(BSDT_ConPhaiNop_32), year);
                    //}
                    //if (userCode.Trim().Length > 0 && BSDT_ThanhTien_33.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_BSDT_ThanhTien, userCode, Convert.ToDouble(BSDT_ThanhTien_33), year);
                    //}

                    //if (userCode.Trim().Length > 0 && BSDTLT2008_42.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_BSDTLT_2008, userCode, Convert.ToDouble(BSDTLT2008_42), year);
                    //}
                    //if (userCode.Trim().Length > 0 && QuyetToanTTN2008_43.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_QuyetToanTTN_2008, userCode, Convert.ToDouble(QuyetToanTTN2008_43), year);
                    //}
                    //if (userCode.Trim().Length > 0 && BSDTLTDaNhan_44.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(BonusNameId_BSDTLTDaNhan, userCode, Convert.ToDouble(BSDTLTDaNhan_44), year);
                    //}

                    if ((userCode.Trim().Length > 0) && (BSDT_BSDTLT_1.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_BSDTLT, userCode, Convert.ToDouble(BSDT_BSDTLT_1), year);
                    if ((userCode.Trim().Length > 0) && (BSDT_TTN2005_3.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_TTN2005, userCode, Convert.ToDouble(BSDT_TTN2005_3),
                            year);
                    if ((userCode.Trim().Length > 0) && (BSDT_TTN2006_4.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_TTN2006, userCode, Convert.ToDouble(BSDT_TTN2006_4),
                            year);
                }
            }
            catch
            {
                returnValue = -2;
            }

            return returnValue;
        }

        public static int ImportMiddleYearBS(int year, string fileName, string sheetName)
        {
            var returnValue = 1;
            var helper = new ExcelHelper(fileName);
            var BonusNameId_BS_DuocLinh = 34;
            var BonusNameId_BS_DaLinh = 35;
            var BonusNameId_BS = 36;
            var BonusNameId_BSTTNTT_PhaiNop = 37;
            var BonusNameId_BSTTNTT_DaNop = 38;
            var BonusNameId_BSTTNTT_ConPhaiNop = 39;
            var BonusNameId_BS_ThanhTien = 40;

            try
            {
                var dt = helper.ReadDataFromExcelToDataTable(sheetName);


                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var userCode = dr["Mã NV"].ToString().Trim();

                    var BS_DuocLinh = dr["BS_DuocLinh"].ToString().Trim();
                    var BS_DaLinh = dr["BS_DaLinh"].ToString().Trim();
                    var BS = dr["BS"].ToString().Trim();
                    var BSTTNTT_PhaiNop = dr["BSTTNTT_PhaiNop"].ToString().Trim();
                    var BSTTNTT_DaNop = dr["BSTTNTT_DaNop"].ToString().Trim();
                    var BSTTNTT_ConPhaiNop = dr["BSTTNTT_ConPhaiNop"].ToString().Trim();
                    var BS_ThanhTien = dr["BS_ThanhTien"].ToString().Trim();


                    if (userCode.Trim().ToLower().Equals("stop"))
                        break;


                    if ((userCode.Trim().Length > 0) && (BS_DuocLinh.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_BS_DuocLinh, userCode, Convert.ToDouble(BS_DuocLinh),
                            year);
                    if ((userCode.Trim().Length > 0) && (BS_DaLinh.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_BS_DaLinh, userCode, Convert.ToDouble(BS_DaLinh), year);
                    if ((userCode.Trim().Length > 0) && (BS.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_BS, userCode, Convert.ToDouble(BS), year);

                    if ((userCode.Trim().Length > 0) && (BSTTNTT_PhaiNop.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_BSTTNTT_PhaiNop, userCode,
                            Convert.ToDouble(BSTTNTT_PhaiNop), year);
                    if ((userCode.Trim().Length > 0) && (BSTTNTT_DaNop.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_BSTTNTT_DaNop, userCode, Convert.ToDouble(BSTTNTT_DaNop),
                            year);
                    if ((userCode.Trim().Length > 0) && (BSTTNTT_ConPhaiNop.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_BSTTNTT_ConPhaiNop, userCode,
                            Convert.ToDouble(BSTTNTT_ConPhaiNop), year);

                    if ((userCode.Trim().Length > 0) && (BS_ThanhTien.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_BS_ThanhTien, userCode, Convert.ToDouble(BS_ThanhTien),
                            year);
                }
            }
            catch
            {
                returnValue = -2;
            }

            return returnValue;
        }

        public static int ImportChenhLechThuong(int year, string fileName, string sheetName)
        {
            var returnValue = 1;
            var helper = new ExcelHelper(fileName);
            var BonusNameId_BSDTLT_2008 = 42;
            var BonusNameId_QuyetToanTTN_2008 = 43;
            var BonusNameId_BSDTLTDaNhan = 44;
            var BonusNameId_ChenhLech = 45;

            try
            {
                var dt = helper.ReadDataFromExcelToDataTable(sheetName);


                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var userCode = dr["Mã NV"].ToString().Trim();

                    var BSDTLT2008 = dr["BSDTLT2008"].ToString().Trim();
                    var QuyetToanTTN2008 = dr["QuyetToanTTN2008 "].ToString().Trim();
                    var BSDTLTDaNhan = dr["BSDTLTDaNhan"].ToString().Trim();
                    var ChenhLech = dr["ChenhLech"].ToString().Trim();


                    if (userCode.Trim().ToLower().Equals("stop"))
                        break;


                    if ((userCode.Trim().Length > 0) && (BSDTLT2008.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_BSDTLT_2008, userCode, Convert.ToDouble(BSDTLT2008),
                            year);
                    if ((userCode.Trim().Length > 0) && (QuyetToanTTN2008.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_QuyetToanTTN_2008, userCode,
                            Convert.ToDouble(QuyetToanTTN2008), year);
                    if ((userCode.Trim().Length > 0) && (BSDTLTDaNhan.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_BSDTLTDaNhan, userCode, Convert.ToDouble(BSDTLTDaNhan),
                            year);

                    if ((userCode.Trim().Length > 0) && (ChenhLech.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_ChenhLech, userCode, Convert.ToDouble(ChenhLech), year);
                }
            }
            catch
            {
                returnValue = -2;
            }

            return returnValue;
        }

        public static int ImportTTNCNTraLai(int year, string fileName, string sheetName)
        {
            var returnValue = 1;
            var helper = new ExcelHelper(fileName);
            var BonusNameId_KhauTruThang12_2008 = 46;
            var BonusNameId_KhauTruBSLThuongATHK2008 = 47;
            var BonusNameId_TongThueKhauTruTraLai = 48;


            try
            {
                var dt = helper.ReadDataFromExcelToDataTable(sheetName);


                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var userCode = dr["Mã NV"].ToString().Trim();

                    var BonusNameId_KhauTruThang12_2008Value = dr["T1"].ToString().Trim();
                    var BonusNameId_KhauTruBSLThuongATHK2008Value = dr["T2"].ToString().Trim();
                    var BonusNameId_TongThueKhauTruTraLaiValue = dr["T3"].ToString().Trim();


                    if (userCode.Trim().ToLower().Equals("stop"))
                        break;

                    if ((userCode.Trim().Length > 0) && (BonusNameId_KhauTruThang12_2008Value.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_KhauTruThang12_2008, userCode,
                            Convert.ToDouble(BonusNameId_KhauTruThang12_2008Value), year);
                    if ((userCode.Trim().Length > 0) && (BonusNameId_KhauTruBSLThuongATHK2008Value.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_KhauTruBSLThuongATHK2008, userCode,
                            Convert.ToDouble(BonusNameId_KhauTruBSLThuongATHK2008Value), year);
                    if ((userCode.Trim().Length > 0) && (BonusNameId_TongThueKhauTruTraLaiValue.Length > 0))
                        new BonusValuesDAL().Insert(BonusNameId_TongThueKhauTruTraLai, userCode,
                            Convert.ToDouble(BonusNameId_TongThueKhauTruTraLaiValue), year);
                }
            }
            catch
            {
                returnValue = -2;
            }

            return returnValue;
        }

        public static int ImportSafeAviation(int year, string fileName, string sheetName)
        {
            var returnValue = 1;
            var helper = new ExcelHelper(fileName);

            ///////////////////////////////////////////////////////////

            //int C1 = 34; // tien thuong nam 2011 va BSDT dot 5, thuc linh ngay 28/03/2012
            //int C2 = 35; // tien thuong nam 2011 va BSDT dot 5, thuc linh ngay 28/03/2012
            //int C3 = 36; // tien thuong nam 2011 va BSDT dot 5, thuc linh ngay 28/03/2012
            //int C4 = 37;// tien thuong nam 2011 va BSDT dot 5, thuc linh ngay 28/03/2012
            //int C5 = 38; // tien thuong nam 2011 va BSDT dot 5, thuc linh ngay 28/03/2012
            //int C6 = 39; // tien thuong nam 2011 va BSDT dot 5, thuc linh ngay 28/03/2012
            //int C7 = 41; // tien thuong nam 2011 va BSDT dot 5, thuc linh ngay 28/03/2012
            //int C8 = 53; // tien thuong nam 2011 va BSDT dot 5, thuc linh ngay 28/03/2012 
            //int C9 = 54; // tien thuong nam 2011 va BSDT dot 5, thuc linh ngay 28/03/2012
            //int C10 = 55;// hien thi thuc linh thanh tien

            //int C20 = 20; // tien thuong quy phuc loi, thuc linh ngay 18/04/2012
            //int C21 = 21; // tien thuong quy phuc loi, thuc linh ngay 18/04/2012 
            //int C22 = 22; // tien thuong quy phuc loi, thuc linh ngay 18/04/2012

            //int C9 = 9; // tien thuong nam 2011, thuc linh ngay 20/04/2012
            //int C10 = 10; // tien thuong nam 2011, thuc linh ngay 20/04/2012 
            //int C11 = 11; // tien thuong nam 2011, thuc linh ngay 20/04/2012

            ///////////////////////////////////////////////////////////
            //ATHK từ tháng 1 đến tháng 6 nam 2011
            //int C4 = 56; 
            //int C5 = 57;
            //int C6 = 58;
            ///////////////////////////////////////////////////////////
            // ATHK 6 thang dau nam 2012
            var C15 = 15;
            var C16 = 16;
            var C17 = 17;
            try
            {
                var dt = helper.ReadDataFromExcelToDataTable(sheetName);


                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var userCode = dr["Mã NV"].ToString().Trim();

                    //string sC1 = dr["C1"].ToString().Trim();
                    //string sC2 = dr["C2"].ToString().Trim();
                    //string sC3 = dr["C3"].ToString().Trim();
                    //string sC4 = dr["C4"].ToString().Trim();
                    //string sC5 = dr["C5"].ToString().Trim();
                    //string sC6 = dr["C6"].ToString().Trim();
                    //string sC7 = dr["C7"].ToString().Trim();
                    //string sC8 = dr["C8"].ToString().Trim();
                    //string sC9 = dr["C9"].ToString().Trim();
                    //string sC10 = dr["C10"].ToString().Trim();

                    var sC15 = dr["C15"].ToString().Trim();
                    var sC16 = dr["C16"].ToString().Trim();
                    var sC17 = dr["C17"].ToString().Trim();

                    //string sC20 = dr["C1"].ToString().Trim();
                    //string sC21 = dr["C2"].ToString().Trim();
                    //string sC22 = dr["C3"].ToString().Trim();

                    //string sC9 = dr["C1"].ToString().Trim();
                    //string sC10 = dr["C2"].ToString().Trim();
                    //string sC11 = dr["C3"].ToString().Trim();

                    if (userCode.Trim().ToLower().Equals("stop"))
                        break;

                    //if (userCode.Trim().Length > 0 && sC1.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C1, userCode, Convert.ToDouble(sC1), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC2.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C2, userCode, Convert.ToDouble(sC2), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC3.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C3, userCode, Convert.ToDouble(sC3), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC4.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C4, userCode, Convert.ToDouble(sC4), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC5.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C5, userCode, Convert.ToDouble(sC5), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC6.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C6, userCode, Convert.ToDouble(sC6), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC7.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C7, userCode, Convert.ToDouble(sC7), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC8.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C8, userCode, Convert.ToDouble(sC8), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC9.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C9, userCode, Convert.ToDouble(sC9), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC10.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C10, userCode, Convert.ToDouble(sC10), year);
                    //}
                    if ((userCode.Trim().Length > 0) && (sC15.Length > 0))
                        new BonusValuesDAL().Insert(C15, userCode, Convert.ToDouble(sC15), year);
                    if ((userCode.Trim().Length > 0) && (sC16.Length > 0))
                        new BonusValuesDAL().Insert(C16, userCode, Convert.ToDouble(sC16), year);
                    if ((userCode.Trim().Length > 0) && (sC17.Length > 0))
                        new BonusValuesDAL().Insert(C17, userCode, Convert.ToDouble(sC17), year);

                    //if (userCode.Trim().Length > 0 && sC9.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C9, userCode, Convert.ToDouble(sC9), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC10.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C10, userCode, Convert.ToDouble(sC10), year);
                    //}
                    //if (userCode.Trim().Length > 0 && sC11.Length > 0)
                    //{
                    //    new BonusValuesDAL().Insert(C11, userCode, Convert.ToDouble(sC11), year);
                    //}
                }
            }
            catch
            {
                returnValue = -2;
            }

            return returnValue;
        }

        #endregion

        #region methods Get

        public static List<BonusValuesBLL> GetAll(int userId, int year, int type)
        {
            return GenerateListBonusValuesBLLFromDataTable(new BonusValuesDAL().GetByUserId_Year(userId, year, type));
        }

        public static List<BonusValuesBLL> GetByYearBonusNameIdsUserId(int year, string bonusNameIds, int userId)
        {
            return
                GenerateListBonusValuesBLLFromDataTable(new BonusValuesDAL().GetByYearBonusNameIdsUserId(year,
                    bonusNameIds, userId));
        }

        public static List<BonusValuesBLL> GetByFilter(string fullName, int departmentId, int year, int bonusNameId)
        {
            return
                GenerateListBonusValuesBLLFromDataTable(new BonusValuesDAL().GetByFilter(fullName, departmentId, year,
                    bonusNameId));
        }

        public static DataTable GetStatisticTotalByFilter(int departmentId, int year, int bonusNameId)
        {
            return new BonusValuesDAL().GetStatisticTotalByFilter(departmentId, year, bonusNameId);
        }

        #endregion

        #region private static methods

        private static List<BonusValuesBLL> GenerateListBonusValuesBLLFromDataTable(DataTable dt)
        {
            var list = new List<BonusValuesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateBonusValuesBLLFromDataRow(dr));

            return list;
        }

        private static BonusValuesBLL GenerateBonusValuesBLLFromDataRow(DataRow dr)
        {
            var objBLL = new BonusValuesBLL(
                dr[BonusValueKeys.FIELD_BONUS_VALUE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[BonusValueKeys.FIELD_BONUS_VALUE_ID].ToString()),
                dr[BonusValueKeys.FIELD_BONUS_VALUE_VALUE] == DBNull.Value
                    ? 0
                    : Convert.ToDouble(dr[BonusValueKeys.FIELD_BONUS_VALUE_VALUE].ToString()),
                dr[BonusValueKeys.FIELD_BONUS_VALUE_YEAR] == DBNull.Value
                    ? 0
                    : int.Parse(dr[BonusValueKeys.FIELD_BONUS_VALUE_YEAR].ToString())
            );

            objBLL.BonusNameId = dr[BonusNameKeys.FIELD_BONUS_NAME_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[BonusNameKeys.FIELD_BONUS_NAME_ID].ToString());
            objBLL.BonusName = dr[BonusNameKeys.FIELD_BONUS_NAME_NAME] == DBNull.Value
                ? string.Empty
                : dr[BonusNameKeys.FIELD_BONUS_NAME_NAME].ToString();
            objBLL.Description = dr[BonusNameKeys.FIELD_BONUS_NAME_DESCRIPTION] == DBNull.Value
                ? string.Empty
                : dr[BonusNameKeys.FIELD_BONUS_NAME_DESCRIPTION].ToString();

            objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            try
            {
                objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
            }
            catch
            {
            }
            objBLL.DepartmentId = dr[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ID].ToString());
            objBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                ? string.Empty
                : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
            objBLL.AccountNo = dr[EmployeeKeys.FIELD_EMPLOYEES_ACCOUNT_NO] == DBNull.Value
                ? string.Empty
                : dr[EmployeeKeys.FIELD_EMPLOYEES_ACCOUNT_NO].ToString();
            objBLL.ATMNo = dr[EmployeeKeys.FIELD_EMPLOYEES_ATM_NO] == DBNull.Value
                ? string.Empty
                : dr[EmployeeKeys.FIELD_EMPLOYEES_ATM_NO].ToString();

            return objBLL;
        }

        #endregion
    }
}