using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using HRMBLL.H1;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Workingday
{
    public partial class frm_ImportWorkingday : RadForm
    {
        public static string SelectedTable = string.Empty;

        public frm_ImportWorkingday()
        {
            InitializeComponent();
        }

        private void rbtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!backgroundWorkerImport.IsBusy)
                {
                    var fullName = string.Empty;
                    progressBarImport.Maximum = rGVData.RowCount;
                    backgroundWorkerImport.RunWorkerAsync(fullName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void rbtnBrowse_Click(object sender, EventArgs e)
        {
            var fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = rtxtFileName.Text;
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                rtxtFileName.Text = fdlg.FileName;
                Import();
            }
        }

        private void Import()
        {
            if (rtxtFileName.Text.Trim() != string.Empty)
                try
                {
                    var sheetNames = ExcelFile.GetAllSheetsName(rtxtFileName.Text);

                    var objSelectTable = new ListSheet(this, sheetNames);
                    objSelectTable.ShowDialog(this);
                    objSelectTable.Dispose();
                    if ((SelectedTable != string.Empty) && (SelectedTable != null))
                    {
                        var dt = ExcelFile.GetDataTableExcel(rtxtFileName.Text, SelectedTable);
                        rGVData.DataSource = dt.DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }


        /// <summary>
        ///     this method run only for 2013 data
        /// </summary>
        /// <param name="fullName"></param>
        private void runImportToDataBase(string fullName)
        {
            var ACVId = string.Empty;
            var UserId = 0;
            var dataDate = rDTPDate.Value;

            string day1;
            string day2;
            string day3;
            string day4;
            string day5;
            string day6;
            string day7;
            string day8;
            string day9;
            string day10;
            string day11;
            string day12;
            string day13;
            string day14;
            string day15;
            string day16;
            string day17;
            string day18;
            string day19;
            string day20;
            string day21;
            string day22;
            string day23;
            string day24;
            string day25;
            string day26;
            string day27;
            string day28;
            string day29;
            string day30;
            string day31;

            double NCQD;
            double NCDC;
            double X;
            double OmDNBHXH;
            double Om;
            double OmDN;
            double KHH;
            double Co;
            double TS;
            double ST;
            double Khamthai;
            double TNLD;
            double F;
            double Diduong;
            double CTac;
            double Fdb;
            double H1;
            double H2;
            double H3;
            double H4;
            double H5;
            double H6;
            double H7;
            double DinhChiCT;
            double Ro;
            double Ko;
            double LamthemNTbngay;
            double LamthemCNbngay;
            double LamthemLTbngay;
            double LamthemNTbdem;
            double LamthemCNbdem;
            double LamthemLTbdem;
            double Lamdem;

            double HSLNS = 0;
            double HSLNSPCTN = 0;
            double HSLCB = 0;
            double HSPCDH = 0;
            double HSPCTN = 0;
            double HSPCKV = 0;
            double HSPCCV = 0;
            double dtNopthue = 0;
            double nguoiPthuoc = 0;
            double HSK = 0;

            var contract = string.Empty;
            double BSLNgayCong = 0;
            double BSLHSLNS = 0;
            double BLSQHSLNS = 0;
            double ThuongNgayCong = 0;
            double ThuongHSLNS = 0;
            double ThuongQHSLNS = 0;
            double ATHKNgayCong = 0;
            double ATHKHSLNS = 0;
            double ATHKQHSLNS = 0;
            double ThangCongLeTet = 0;

            var remark = "Import Excel";

            var createUserId = clsGlobal.UserId;
            var updateUserId = clsGlobal.UserId;
            var updateDate = DateTime.Now;
            var createDate = DateTime.Now;


            for (var i = 0; i < rGVData.Rows.Count; i++)
            {
                var row = rGVData.Rows[i];


                ACVId =
                    Convert.ToString(row.Cells["Mã SAGS"] == null ? string.Empty : row.Cells["Mã SAGS"].Value).Trim();

                if (ACVId.Trim().Length > 0)
                {
                    UserId = int.Parse(ACVId);

                    day1 = Convert.ToString(row.Cells["Day1"] == null ? string.Empty : row.Cells["Day1"].Value);
                    day2 = Convert.ToString(row.Cells["Day2"] == null ? string.Empty : row.Cells["Day2"].Value);
                    day3 = Convert.ToString(row.Cells["Day3"] == null ? string.Empty : row.Cells["Day3"].Value);
                    day4 = Convert.ToString(row.Cells["Day4"] == null ? string.Empty : row.Cells["Day4"].Value);
                    day5 = Convert.ToString(row.Cells["Day5"] == null ? string.Empty : row.Cells["Day5"].Value);
                    day6 = Convert.ToString(row.Cells["Day6"] == null ? string.Empty : row.Cells["Day6"].Value);
                    day7 = Convert.ToString(row.Cells["Day7"] == null ? string.Empty : row.Cells["Day7"].Value);
                    day8 = Convert.ToString(row.Cells["Day8"] == null ? string.Empty : row.Cells["Day8"].Value);
                    day9 = Convert.ToString(row.Cells["Day9"] == null ? string.Empty : row.Cells["Day9"].Value);
                    day10 = Convert.ToString(row.Cells["Day10"] == null ? string.Empty : row.Cells["Day10"].Value);
                    day11 = Convert.ToString(row.Cells["Day11"] == null ? string.Empty : row.Cells["Day11"].Value);
                    day12 = Convert.ToString(row.Cells["Day12"] == null ? string.Empty : row.Cells["Day12"].Value);
                    day13 = Convert.ToString(row.Cells["Day13"] == null ? string.Empty : row.Cells["Day13"].Value);
                    day14 = Convert.ToString(row.Cells["Day14"] == null ? string.Empty : row.Cells["Day14"].Value);
                    day15 = Convert.ToString(row.Cells["Day15"] == null ? string.Empty : row.Cells["Day15"].Value);
                    day16 = Convert.ToString(row.Cells["Day16"] == null ? string.Empty : row.Cells["Day16"].Value);
                    day17 = Convert.ToString(row.Cells["Day17"] == null ? string.Empty : row.Cells["Day17"].Value);
                    day18 = Convert.ToString(row.Cells["Day18"] == null ? string.Empty : row.Cells["Day18"].Value);
                    day19 = Convert.ToString(row.Cells["Day19"] == null ? string.Empty : row.Cells["Day19"].Value);
                    day20 = Convert.ToString(row.Cells["Day20"] == null ? string.Empty : row.Cells["Day20"].Value);
                    day21 = Convert.ToString(row.Cells["Day21"] == null ? string.Empty : row.Cells["Day21"].Value);
                    day22 = Convert.ToString(row.Cells["Day22"] == null ? string.Empty : row.Cells["Day22"].Value);
                    day23 = Convert.ToString(row.Cells["Day23"] == null ? string.Empty : row.Cells["Day23"].Value);
                    day24 = Convert.ToString(row.Cells["Day24"] == null ? string.Empty : row.Cells["Day24"].Value);
                    day25 = Convert.ToString(row.Cells["Day25"] == null ? string.Empty : row.Cells["Day25"].Value);
                    day26 = Convert.ToString(row.Cells["Day26"] == null ? string.Empty : row.Cells["Day26"].Value);
                    day27 = Convert.ToString(row.Cells["Day27"] == null ? string.Empty : row.Cells["Day27"].Value);
                    day28 = Convert.ToString(row.Cells["Day28"] == null ? string.Empty : row.Cells["Day28"].Value);
                    day29 = Convert.ToString(row.Cells["Day29"] == null ? string.Empty : row.Cells["Day29"].Value);
                    day30 = Convert.ToString(row.Cells["Day30"] == null ? string.Empty : row.Cells["Day30"].Value);
                    day31 = Convert.ToString(row.Cells["Day31"] == null ? string.Empty : row.Cells["Day31"].Value);

                    NCQD =
                        Convert.ToDouble(row.Cells["Ngày công quy định"].Value == DBNull.Value
                            ? 0
                            : row.Cells["Ngày công quy định"].Value);
                    NCDC =
                        Convert.ToDouble(row.Cells["Ngày công được chấm"].Value == DBNull.Value
                            ? 0
                            : row.Cells["Ngày công được chấm"].Value);
                    X = Convert.ToDouble(row.Cells["X"].Value == DBNull.Value ? 0 : row.Cells["X"].Value);
                    OmDNBHXH =
                        Convert.ToDouble(row.Cells["Om DNBHXH"].Value == DBNull.Value ? 0 : row.Cells["Om DNBHXH"].Value);
                    Om = Convert.ToDouble(row.Cells["Om"].Value == DBNull.Value ? 0 : row.Cells["Om"].Value);
                    OmDN = Convert.ToDouble(row.Cells["OmDN"].Value == DBNull.Value ? 0 : row.Cells["OmDN"].Value);
                    KHH = Convert.ToDouble(row.Cells["KHH"].Value == DBNull.Value ? 0 : row.Cells["KHH"].Value);
                    Co = Convert.ToDouble(row.Cells["Co"].Value == DBNull.Value ? 0 : row.Cells["Co"].Value);
                    TS = Convert.ToDouble(row.Cells["TS"].Value == DBNull.Value ? 0 : row.Cells["TS"].Value);
                    ST = Convert.ToDouble(row.Cells["ST"].Value == DBNull.Value ? 0 : row.Cells["ST"].Value);
                    Khamthai =
                        Convert.ToDouble(row.Cells["Khám thai"].Value == DBNull.Value ? 0 : row.Cells["Khám thai"].Value);
                    TNLD = Convert.ToDouble(row.Cells["TNLD"].Value == DBNull.Value ? 0 : row.Cells["TNLD"].Value);
                    F = Convert.ToDouble(row.Cells["F"].Value == DBNull.Value ? 0 : row.Cells["F"].Value);
                    Diduong =
                        Convert.ToDouble(row.Cells["Đi đường"].Value == DBNull.Value ? 0 : row.Cells["Đi đường"].Value);
                    CTac = Convert.ToDouble(row.Cells["CTác"].Value == DBNull.Value ? 0 : row.Cells["CTác"].Value);
                    Fdb = Convert.ToDouble(row.Cells["Fdb"].Value == DBNull.Value ? 0 : row.Cells["Fdb"].Value);
                    H1 = Convert.ToDouble(row.Cells["H1"].Value == DBNull.Value ? 0 : row.Cells["H1"].Value);
                    H2 = Convert.ToDouble(row.Cells["H2"].Value == DBNull.Value ? 0 : row.Cells["H2"].Value);
                    H3 = Convert.ToDouble(row.Cells["H3"].Value == DBNull.Value ? 0 : row.Cells["H3"].Value);
                    H4 = Convert.ToDouble(row.Cells["H4"].Value == DBNull.Value ? 0 : row.Cells["H4"].Value);
                    H5 = Convert.ToDouble(row.Cells["H5"].Value == DBNull.Value ? 0 : row.Cells["H5"].Value);
                    H6 = Convert.ToDouble(row.Cells["H6"].Value == DBNull.Value ? 0 : row.Cells["H6"].Value);
                    H7 = Convert.ToDouble(row.Cells["H7"].Value == DBNull.Value ? 0 : row.Cells["H7"].Value);
                    DinhChiCT =
                        Convert.ToDouble(row.Cells["Đình chỉ CT"].Value == DBNull.Value
                            ? 0
                            : row.Cells["Đình chỉ CT"].Value);
                    Ro = Convert.ToDouble(row.Cells["Ro"].Value == DBNull.Value ? 0 : row.Cells["Ro"].Value);
                    Ko = Convert.ToDouble(row.Cells["Ko"].Value == DBNull.Value ? 0 : row.Cells["Ko"].Value);
                    LamthemNTbngay = 0;

                    LamthemCNbngay = 0;

                    LamthemLTbngay = 0;

                    LamthemNTbdem = 0;

                    LamthemCNbdem = 0;

                    LamthemLTbdem = 0;

                    Lamdem =
                        Convert.ToDouble(row.Cells["Làm đêm"].Value == DBNull.Value ? 0 : row.Cells["Làm đêm"].Value);


                    HSLNS =
                        Convert.ToDouble(row.Cells["Hệ số LNS"].Value == DBNull.Value ? 0 : row.Cells["Hệ số LNS"].Value);
                    HSLNSPCTN =
                        Convert.ToDouble(row.Cells["HS TN LCD"].Value == DBNull.Value ? 0 : row.Cells["HS TN LCD"].Value);

                    HSLCB =
                        Convert.ToDouble(row.Cells["Hệ số LCB"].Value == DBNull.Value ? 0 : row.Cells["Hệ số LCB"].Value);
                    HSPCCV =
                        Convert.ToDouble(row.Cells["Hệ số PCCV"].Value == DBNull.Value
                            ? 0
                            : row.Cells["Hệ số PCCV"].Value);
                    HSPCTN =
                        Convert.ToDouble(row.Cells["Hệ số PCTN"].Value == DBNull.Value
                            ? 0
                            : row.Cells["Hệ số PCTN"].Value);
                    HSPCKV =
                        Convert.ToDouble(row.Cells["Hệ số PCKV"].Value == DBNull.Value
                            ? 0
                            : row.Cells["Hệ số PCKV"].Value);
                    HSPCDH =
                        Convert.ToDouble(row.Cells["Hệ số PCĐH"].Value == DBNull.Value
                            ? "0"
                            : row.Cells["Hệ số PCĐH"].Value);

                    HSK = Convert.ToDouble(row.Cells["Hệ số K"].Value == DBNull.Value ? 0 : row.Cells["Hệ số K"].Value);

                    dtNopthue =
                        Convert.ToDouble(row.Cells["Giảm trừ cá nhân"].Value == DBNull.Value
                            ? 0
                            : row.Cells["Giảm trừ cá nhân"].Value);
                    nguoiPthuoc =
                        Convert.ToDouble(row.Cells["Giảm trừ người phụ thuộc"].Value == DBNull.Value
                            ? 0
                            : row.Cells["Giảm trừ người phụ thuộc"].Value);


                    contract = row.Cells["Hợp đồng lao động"].Value == DBNull.Value
                        ? string.Empty
                        : row.Cells["Hợp đồng lao động"].Value.ToString();

                    BSLNgayCong = 0;
                    BSLHSLNS = 0;

                    BLSQHSLNS = 0;


                    ThuongNgayCong = 0;

                    ThuongHSLNS = 0;

                    ThuongQHSLNS = 0;


                    ATHKNgayCong = 0;

                    ATHKHSLNS = 0;

                    ATHKQHSLNS = 0;


                    ThangCongLeTet = 0;


                    WorkdayCoefficientEmployeesFinalBLL.ImportFromExcelACV(ACVId, dataDate, day1, day2, day3, day4, day5,
                        day6, day7, day8, day9, day10,
                        day11, day12, day13, day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24,
                        day25, day26, day27, day28, day29, day30, day31,
                        NCQD, NCDC, X, OmDNBHXH, Om, OmDN, KHH, Co, TS, ST, Khamthai, TNLD, F, Diduong, CTac, Fdb, H1,
                        H2, H3, H4, H5, H6, H7, DinhChiCT, Ro, Ko, LamthemNTbngay, LamthemCNbngay, LamthemLTbngay,
                        LamthemNTbdem,
                        LamthemCNbdem, LamthemLTbdem, Lamdem, HSLNS, HSLNSPCTN, HSLCB, HSPCDH, HSPCTN, HSPCKV, HSPCCV,
                        HSK, dtNopthue, nguoiPthuoc, createDate, createUserId, updateDate, updateUserId, remark, UserId,
                        contract, BSLNgayCong, BSLHSLNS, BLSQHSLNS, ThuongNgayCong, ThuongHSLNS, ThuongQHSLNS,
                        ATHKNgayCong, ATHKHSLNS, ATHKQHSLNS, ThangCongLeTet);
                }
                fullName = ACVId;
                backgroundWorkerImport.ReportProgress(i + 1, fullName);
                Thread.Sleep(10);
            }
        }

        private void backgroundWorkerImport_DoWork(object sender, DoWorkEventArgs e)
        {
            var fullName = (string) e.Argument;
            runImportToDataBase(fullName);
        }

        private void backgroundWorkerImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorkerImport.CancellationPending)
            {
                progressBarImport.Value1 = e.ProgressPercentage;
                progressBarImport.Text = "Đang nạp dữ liệu: " + e.ProgressPercentage + "/" + rGVData.RowCount;
            }
        }

        private void backgroundWorkerImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarImport.Text = "Nạp dữ liệu thành công";
        }

        private void ImportWorkingday_Load(object sender, EventArgs e)
        {
            rDTPDate.Value = DateTime.Now.AddMonths(-1);
        }
    }
}