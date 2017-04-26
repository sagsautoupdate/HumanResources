using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMBLL.H1.Helper;
using HRMUtil;
using HumanResources.Forms.Workingday.Helper;
using Telerik.WinControls.UI;
using MessageBox = System.Windows.MessageBox;

namespace HumanResources.Forms.Workingday
{
    public partial class frm_WorkingdayFinalDetail : RadForm
    {
        private frm_WorkingdayFinal _fwf;
        private string _OldContent = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_WorkingdayFinalDetail()
        {
            InitializeComponent();
        }

        public frm_WorkingdayFinalDetail(frm_WorkingdayFinal fwf, int UserId, string FullName, DateTime DataDate)
        {
            InitializeComponent();

            _fwf = fwf;
            this.UserId = UserId;
            this.FullName = FullName;
            this.DataDate = DataDate;
            Text = string.Format("Bảng công hưởng lương của {0} (Mã nhân viên: {1}) tháng {2} năm {3}",
                FullName.ToUpper(), UserId, DataDate.Month, DataDate.Year);
        }

        public DateTime DataDate { get; set; }

        public string FullName { get; set; }

        public int UserId { get; set; }

        private void frm_WorkingdayFinalDetail_Load(object sender, EventArgs e)
        {
            InitDataSource();
            InitData(UserId);

            Utilities.Utilities.GridFormatting(radGridView1);
            Utilities.Utilities.GridFormatting(radGridView5);
            Utilities.Utilities.GridFormatting(radGridView3);
            Utilities.Utilities.GridFormatting(radGridView4);
        }

        private void RadGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridCellElement)
                if ((e.CellElement.Value.ToString().Equals("TNLD") == false) &&
                    (e.CellElement.Value.ToString().Equals("DD") == false) &&
                    (e.CellElement.Value.ToString().Equals("CT") == false) &&
                    (e.CellElement.Value.ToString().Equals("DC") == false) &&
                    (e.CellElement.Value.ToString().Equals("Ho") == false) &&
                    (e.CellElement.Value.ToString().Equals("H1") == false) &&
                    (e.CellElement.Value.ToString().Equals("H2") == false) &&
                    (e.CellElement.Value.ToString().Equals("H3") == false) &&
                    (e.CellElement.Value.ToString().Equals("H4") == false) &&
                    (e.CellElement.Value.ToString().Equals("H5") == false) &&
                    (e.CellElement.Value.ToString().Equals("H6") == false) &&
                    (e.CellElement.Value.ToString().Equals("H7") == false) &&
                    (e.CellElement.Value.ToString().Equals("NT") == false) &&
                    (e.CellElement.Value.ToString().Equals("NB") == false) &&
                    (e.CellElement.Value.ToString().Equals("X") == false) &&
                    (e.CellElement.Value.ToString().Equals("--") == false))
                {
                    e.CellElement.ForeColor = Color.Blue;
                    e.CellElement.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                }
                else
                {
                    if (e.CellElement.Value.ToString().Equals("--"))
                    {
                        e.CellElement.ForeColor = Color.Red;
                        e.CellElement.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                    }
                }
        }

        private void Frm_WorkingdayFinalDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            #region Days
            var Day1 = radGridView1.Rows[0].Cells["Day1"].Value.ToString();
            var Day2 = radGridView1.Rows[0].Cells["Day2"].Value.ToString();
            var Day3 = radGridView1.Rows[0].Cells["Day3"].Value.ToString();
            var Day4 = radGridView1.Rows[0].Cells["Day4"].Value.ToString();
            var Day5 = radGridView1.Rows[0].Cells["Day5"].Value.ToString();
            var Day6 = radGridView1.Rows[0].Cells["Day6"].Value.ToString();
            var Day7 = radGridView1.Rows[0].Cells["Day7"].Value.ToString();
            var Day8 = radGridView1.Rows[0].Cells["Day8"].Value.ToString();
            var Day9 = radGridView1.Rows[0].Cells["Day9"].Value.ToString();
            var Day10 = radGridView1.Rows[0].Cells["Day10"].Value.ToString();
            var Day11 = radGridView1.Rows[0].Cells["Day11"].Value.ToString();
            var Day12 = radGridView1.Rows[0].Cells["Day12"].Value.ToString();
            var Day13 = radGridView1.Rows[0].Cells["Day13"].Value.ToString();
            var Day14 = radGridView1.Rows[0].Cells["Day14"].Value.ToString();
            var Day15 = radGridView1.Rows[0].Cells["Day15"].Value.ToString();
            var Day16 = radGridView1.Rows[0].Cells["Day16"].Value.ToString();
            var Day17 = radGridView1.Rows[0].Cells["Day17"].Value.ToString();
            var Day18 = radGridView1.Rows[0].Cells["Day18"].Value.ToString();
            var Day19 = radGridView1.Rows[0].Cells["Day19"].Value.ToString();
            var Day20 = radGridView1.Rows[0].Cells["Day20"].Value.ToString();
            var Day21 = radGridView1.Rows[0].Cells["Day21"].Value.ToString();
            var Day22 = radGridView1.Rows[0].Cells["Day22"].Value.ToString();
            var Day23 = radGridView1.Rows[0].Cells["Day23"].Value.ToString();
            var Day24 = radGridView1.Rows[0].Cells["Day24"].Value.ToString();
            var Day25 = radGridView1.Rows[0].Cells["Day25"].Value.ToString();
            var Day26 = radGridView1.Rows[0].Cells["Day26"].Value.ToString();
            var Day27 = radGridView1.Rows[0].Cells["Day27"].Value.ToString();
            var Day28 = radGridView1.Rows[0].Cells["Day28"].Value.ToString();
            var Day29 = string.Empty;
            var Day30 = string.Empty;
            var Day31 = string.Empty;
            try
            {
                Day29 = radGridView1.Rows[0].Cells["Day29"].Value.ToString();
            }
            catch
            {
                Day29 = string.Empty;
            }
            try
            {
                Day30 = radGridView1.Rows[0].Cells["Day30"].Value.ToString();
            }
            catch
            {
                Day30 = string.Empty;
            }
            try
            {
                Day31 = radGridView1.Rows[0].Cells["Day31"].Value.ToString();
            }
            catch
            {
                Day31 = string.Empty;
            }
            var f_Om = Constants.WorkdayEmployee_DefaultValue;
            var f_OmDaiNgay = Constants.WorkdayEmployee_DefaultValue;
            var f_ThaiSan = Constants.WorkdayEmployee_DefaultValue;
            var f_TNLD = Constants.WorkdayEmployee_DefaultValue;
            var f_Nam = Constants.WorkdayEmployee_DefaultValue;
            var f_db = Constants.WorkdayEmployee_DefaultValue;
            var f_KoLuongCLD = Constants.WorkdayEmployee_DefaultValue;
            var f_KoLuongKLD = Constants.WorkdayEmployee_DefaultValue;
            var f_DiDuong = Constants.WorkdayEmployee_DefaultValue;
            var f_CongTac = Constants.WorkdayEmployee_DefaultValue;

            var f_HocSAGS = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc1 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc2 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc3 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc4 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc5 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc6 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc7 = Constants.WorkdayEmployee_DefaultValue;

            var f_Con_Om = Constants.WorkdayEmployee_DefaultValue;
            var f_KHHDS = Constants.WorkdayEmployee_DefaultValue;
            var f_SayThai = Constants.WorkdayEmployee_DefaultValue;
            var f_KhamThai = Constants.WorkdayEmployee_DefaultValue;
            var f_ConChet = Constants.WorkdayEmployee_DefaultValue;
            var f_DinhChiCongTac = Constants.WorkdayEmployee_DefaultValue;
            var f_TamHoanHD = Constants.WorkdayEmployee_DefaultValue;
            var f_HoiHop = Constants.WorkdayEmployee_DefaultValue;
            var f_Le = Constants.WorkdayEmployee_DefaultValue;
            var nghiTuan = Constants.WorkdayEmployee_DefaultValue;
            var nghiBu = Constants.WorkdayEmployee_DefaultValue;
            var nghiMat = Constants.WorkdayEmployee_DefaultValue;
            var nghiViec = Constants.WorkdayEmployee_DefaultValue;
            var chuaDiLam = Constants.WorkdayEmployee_DefaultValue;
            var f_OmDNBHXH = Constants.WorkdayEmployee_DefaultValue;


            f_OmDNBHXH = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH);

            f_Om = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_BAN_THAN);

            f_OmDaiNgay = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_DAI_NGAY);

            f_KHHDS = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_KHHDS);

            f_Con_Om = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CON_OM);

            f_ThaiSan = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_THAI_SAN);

            f_SayThai = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_SAY_THAI);

            f_KhamThai = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_KHAM_THAI);

            f_TNLD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_TNLD);

            f_Nam = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_NAM);

            f_DiDuong = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_DI_DUONG);

            f_CongTac = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_CONG_TAC);

            f_db = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_FDB);

            f_Hoc1 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_1);

            f_Hoc2 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_2);

            f_Hoc3 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_3);

            f_Hoc4 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_4);

            f_Hoc5 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_5);

            f_Hoc6 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_6);

            f_Hoc7 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_7);

            f_DinhChiCongTac = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3,
                Day4, Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);

            f_KoLuongCLD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

            f_KoLuongKLD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);


            f_HocSAGS = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_SAGS);

            f_ConChet = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);

            f_TamHoanHD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);
            f_HoiHop = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOI_HOP);
            f_Le = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_LE_TET);

            nghiTuan = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_TUAN);

            nghiBu = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_BU);

            nghiMat = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_MAT);

            nghiViec = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_VIEC);

            chuaDiLam = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CHUA_DI_LAM);

            var X = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, UserId, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_X);
            #endregion

            var HSK = Convert.ToDouble(radGridView3.Rows[0].Cells["HSK"].Value);
            var Remark = radGridView3.Rows[0].Cells["Remark"].Value.ToString();

            double LamDem = 0;
            if (
                Convert.ToInt32(
                    EmployeesBLL.GetDataRowEmployeeById(Convert.ToInt32(rmbEmployees.SelectedValue))["DirectWorking"]) ==
                1)
                LamDem = double.Parse(radGridView5.Rows[0].Cells["GC_LamDem"].Value.ToString());
            var UpdateUserId = clsGlobal.UserId;
            var UpdatedDate = DateTime.Now;

            var TotalLeave = f_OmDNBHXH + f_Om + f_OmDaiNgay + f_KHHDS + f_Con_Om + f_ThaiSan + f_SayThai + f_KhamThai +
                             f_TNLD + f_Nam + f_DiDuong + f_CongTac + f_db + f_Hoc1 + f_Hoc2 + f_Hoc3 + f_Hoc4 + f_Hoc5 +
                             f_Hoc6 + f_Hoc7 + f_DinhChiCongTac + f_KoLuongCLD + f_KoLuongKLD + nghiBu;
            var DRWD =
                WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDate(Convert.ToInt32(rmbEmployees.SelectedValue),
                    DataDate, Constants.DataType_Run);

            var NCDC = X + TotalLeave;

            if (NCDC < Convert.ToInt32(DRWD["NCQD"]))
            {
                MessageBox.Show(string.Format("NCDC: {0} < NCQD: {1} => Chấm thiếu công", NCDC, DRWD["NCQD"]));
            }
            else
            {
                if (NCDC > Convert.ToInt32(DRWD["NCQD"]))
                    MessageBox.Show(string.Format("NCDC: {0} > NCQD: {1} => Chấm lố công", NCDC, DRWD["NCQD"]));
            }
            _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_By_UserId_Date_For_EdittingWorkday";
            _SPValue =
                $"UserId: {Convert.ToInt32(rmbEmployees.SelectedValue)}, DataDate: '{DataDate}', Day1: '{Day1}', Day2: '{Day2}', Day3: '{Day3}', Day4: '{Day4}', Day5: '{Day5}', Day6: '{Day6}', Day7: '{Day7}', Day8: '{Day8}', Day9: '{Day9}', Day10: '{Day10}', Day11: '{Day11}', Day12: '{Day12}', Day13: '{Day13}', Day14: '{Day14}', Day15: '{Day15}', Day16: '{Day16}', Day17: '{Day17}', Day18: '{Day18}', Day19: '{Day19}', Day20: '{Day20}', Day21: '{Day21}', Day22: '{Day22}', Day23: '{Day23}', Day24: '{Day24}', Day25: '{Day25}', Day26: '{Day26}', Day27: '{Day27}', Day28: '{Day28}', Day29: '{Day29}', Day30: '{Day30}', Day31: '{Day31}', LamDem: {LamDem}, UpdatedDate: '{UpdatedDate}', UpdateUserId: {UpdateUserId}, f_OmDNBHXH: {f_OmDNBHXH}, f_Om: {f_Om}, f_OmDN: {f_OmDaiNgay}, f_KHHDS: {f_KHHDS}, f_ConOm: {f_Con_Om}, f_TS: {f_ThaiSan}, f_ST: {f_SayThai}, f_Khamthai: {f_KhamThai}, f_TNLD: {f_TNLD}, f_Nam: {f_Nam}, f_Diduong: {f_DiDuong}, f_CongTac: {f_CongTac}, f_db: {f_db}, f_Hoc1: {f_Hoc1}, f_Hoc2: {f_Hoc2}, f_Hoc3: {f_Hoc3}, f_Hoc4: {f_Hoc4}, f_Hoc5: {f_Hoc5}, f_Hoc6: {f_Hoc6}, f_Hoc7: {f_Hoc7}, f_DinhChiCT: {f_DinhChiCongTac}, f_KoLuongCLD: {f_KoLuongCLD}, f_KoLuongKLD: {f_KoLuongKLD}, X: {X}, NT: {nghiTuan}, NB: {nghiBu}, NghiViec: {nghiViec}, NghiMat: {nghiMat}, ChuaDiLam: {chuaDiLam}, HSK: {HSK}, Remark: N'{Remark}', RemarkHRMAdmin: N'{radTextBox1.Text}, NCDC: {NCDC}";

            var drOld =
                WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDateToDR(
                    Convert.ToInt32(rmbEmployees.SelectedValue), DataDate, 2);
            _OldContent =
                $"UserId: {drOld["UserId"]}, DataDate: '{drOld["DataDate"]}', Day1: '{drOld["Day1"]}', Day2: '{drOld["Day2"]}', Day3: '{drOld["Day3"]}', Day4: '{drOld["Day4"]}', Day5: '{drOld["Day5"]}', Day6: '{drOld["Day6"]}', Day7: '{drOld["Day7"]}', Day8: '{drOld["Day8"]}', Day9: '{drOld["Day9"]}', Day10: '{drOld["Day10"]}', Day11: '{drOld["Day11"]}', Day12: '{drOld["Day12"]}', Day13: '{drOld["Day13"]}', Day14: '{drOld["Day14"]}', Day15: '{drOld["Day15"]}', Day16: '{drOld["Day16"]}', Day17: '{drOld["Day17"]}', Day18: '{drOld["Day18"]}', Day19: '{drOld["Day19"]}', Day20: '{drOld["Day20"]}', Day21: '{drOld["Day21"]}', Day22: '{drOld["Day22"]}', Day23: '{drOld["Day23"]}', Day24: '{drOld["Day24"]}', Day25: '{drOld["Day25"]}', Day26: '{drOld["Day26"]}', Day27: '{drOld["Day27"]}', Day28: '{drOld["Day28"]}', Day29: '{drOld["Day29"]}', Day30: '{drOld["Day30"]}', Day31: '{drOld["Day31"]}', Lamdem: {drOld["Lamdem"]}, UpdateDate: '{drOld["UpdateDate"]}', UpdateUserId: {drOld["UpdateUserId"]}, OmDNBHXH: {drOld["OmDNBHXH"]}, Om: {drOld["Om"]}, OmDN: {drOld["OmDN"]}, KHH: {drOld["KHH"]}, Co: {drOld["Co"]}, TS: {drOld["TS"]}, ST: {drOld["ST"]}, Khamthai: {drOld["Khamthai"]}, TNLD: {drOld["TNLD"]}, F: {drOld["F"]}, Diduong: {drOld["Diduong"]}, CTac: {drOld["CTac"]}, Fdb: {drOld["Fdb"]}, H1: {drOld["H1"]}, H2: {drOld["H2"]}, H3: {drOld["H3"]}, H4: {drOld["H4"]}, H5: {drOld["H5"]}, H6: {drOld["H6"]}, H7: {drOld["H7"]}, DinhChiCT: {drOld["DinhChiCT"]}, Ro: {drOld["Ro"]}, Ko: {drOld["Ko"]}, X: {drOld["X"]}, NT: {drOld["NghiTuan"]}, NB: {drOld["NghiBu"]}, NghiViec: {drOld["NghiViec"]}, NghiMat: {drOld["NghiMat"]}, ChuaDiLam: {drOld["ChuaDiLam"]}, HSK: {drOld["HSK"]}, Remark: N'{drOld["Remark"]}', RemarkHRMAdmin: N'{drOld["RemarkHRMAdmin"]}', NCDC: {drOld["NCDC"]}";
            Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue, _OldContent);
            WorkdayCoefficientEmployeesFinalBLL.UpdateWorkingDayFinal(Convert.ToInt32(rmbEmployees.SelectedValue),
                DataDate, Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10,
                Day11, Day12, Day13, Day14, Day15, Day16, Day17, Day18, Day19,
                Day20, Day21, Day22, Day23, Day24, Day25, Day26, Day27, Day28, Day29,
                Day30, Day31,
                LamDem, UpdatedDate, UpdateUserId, f_OmDNBHXH, f_Om, f_OmDaiNgay, f_KHHDS, f_Con_Om, f_ThaiSan,
                f_SayThai, f_KhamThai, f_TNLD,
                f_Nam, f_DiDuong, f_CongTac, f_db, f_Hoc1, f_Hoc2, f_Hoc3, f_Hoc4, f_Hoc5, f_Hoc6, f_Hoc7,
                f_DinhChiCongTac, f_KoLuongCLD,
                f_KoLuongKLD, X, nghiTuan, nghiBu, nghiViec, nghiMat, chuaDiLam, HSK, Remark, radTextBox1.Text, NCDC);

            var WorkdayCoefficientEmployeeIdFinal = Convert.ToInt32(DRWD["WorkdayCoefficientEmployeeIdFinal"]);
            _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
            _SPValue =
                $"UserId: {Convert.ToInt32(rmbEmployees.SelectedValue)}, DataDate: '{DataDate}', WDStatus: {2}, CheckRemark: {string.Empty}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
            Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue, string.Empty);
            WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(Convert.ToInt32(rmbEmployees.SelectedValue), DataDate, 2,
                string.Empty, WorkdayCoefficientEmployeeIdFinal);
            InitData(Convert.ToInt32(rmbEmployees.SelectedValue));
            Text = string.Format("Bảng công hưởng lương của {0} (Mã nhân viên: {1}) tháng {2} năm {3}",
                EmployeesBLL.GetDataRowEmployeeById(Convert.ToInt32(rmbEmployees.SelectedValue))["FullName"].ToString()
                    .ToUpper(), rmbEmployees.SelectedValue, DataDate.Month, DataDate.Year);

            MessageBox.Show("Done!");
        }

        private void RadGridView5_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name != "GC_LamDem")
            {
                var Total = 0;
                for (var i = 0; i < radGridView5.Rows.Count; i++)
                    for (var j = 1; j < radGridView5.Columns.Count; j++)
                        Total += int.Parse(radGridView5.Rows[i].Cells[j].Value.ToString());
                e.Row.Cells["GC_LamDem"].Value = Total;
            }
        }

        private void RmbEmployees_SelectedValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            InitData(Convert.ToInt32(rmbEmployees.SelectedValue));
            Text = string.Format("Bảng công hưởng lương của {0} (Mã nhân viên: {1}) tháng {2} năm {3}",
                EmployeesBLL.GetDataRowEmployeeById(Convert.ToInt32(rmbEmployees.SelectedValue))["FullName"].ToString()
                    .ToUpper(), rmbEmployees.SelectedValue, DataDate.Month, DataDate.Year);
            Cursor.Current = Cursors.Default;
        }

        private void InitDataSource()
        {
            BS_Employees.DataSource = EmployeesBLL.DT_GetAll(-1);
            rmbEmployees.SelectedValue = UserId;
            BS_LeaveCode.DataSource = Constants.GetAllLeaveCode();
        }

        private void InitData(int UserId)
        {
            var _Direct = 0;
            try
            {
                _Direct = Convert.ToInt32(EmployeesBLL.GetDataRowEmployeeById(UserId)["DirectWorking"]);
            }
            catch
            {
            }


            var DT_Workingday = WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDateToDT(UserId, DataDate,
                Constants.DataType_Run);
            if (DT_Workingday.Rows.Count > 0)
            {
                string _ERROR = "";
                try
                {
                    _ERROR = DT_Workingday.Rows[0]["CheckRemark"].ToString().Replace(Environment.NewLine, "");
                }
                catch
                {
                }
                if (_ERROR == "")
                    radTextBox2.Text = string.Empty;
                else
                {
                    var _strError = "";
                    var _str = _ERROR.Split(',');
                    foreach (var item in _str)
                    {
                        if (Convert.ToInt32(item) > -1)
                            _strError += new WE().GetErrorNameById(Convert.ToInt32(item)) + Environment.NewLine;// Utilities.Utilities.GetEnumDescription((WORKDAYERROR)Convert.ToInt32(item)) + Environment.NewLine;
                    }
                    radTextBox2.Text = _strError;// Utilities.Utilities.GetEnumDescription((WORKDAYERROR)_ERROR);
                }
                radGridView1.DataSource = DT_Workingday;
                radTextBox1.Text = DT_Workingday.Rows[0]["RemarkHRMAdmin"].ToString();
            }


            if (_Direct == 1)
            {
                radCollapsiblePanel5.Visible = true;
                radGridView5.Visible = true;
                var DT_WorkingdayNight = WorkdayEmployeesBLL.GetDTByUserId_Month_Year(UserId, DataDate.Month,
                    DataDate.Year);
                if (DT_WorkingdayNight.Rows.Count > 0)
                    radGridView5.DataSource = DT_WorkingdayNight;
            }
            else
            {
                radCollapsiblePanel5.Visible = false;
                radGridView5.Visible = false;
            }

            var LeaveDT = EmployeeLeaveBLL.GetDTByUserId_Date(UserId, DataDate.Month, DataDate.Year);
            if (LeaveDT.Rows.Count > 0)
                radGridView4.DataSource = LeaveDT;
            else
                radGridView4.DataSource = null;
            var DT_Total = WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDateToDT(UserId, DataDate,
                Constants.DataType_Run);
            if (DT_Total.Rows.Count > 0)
                radGridView3.DataSource = DT_Total;
            else
                radGridView3.DataSource = null;
            HideGridviewColumn();
            var ListOfSunday = getSundays();
            foreach (var Temp in ListOfSunday)
                PaintCell("Day" + Temp.Day);
        }

        private void HideGridviewColumn()
        {
            var _Direct = 0;
            try
            {
                _Direct =
                    Convert.ToInt32(
                        EmployeesBLL.GetDataRowEmployeeById(Convert.ToInt32(rmbEmployees.SelectedValue))["DirectWorking"
                        ]);
            }
            catch
            {
            }
            if (_Direct == 1)
            {
                radCollapsiblePanel5.Visible = true;
                radGridView5.Visible = true;
            }
            else
            {
                radCollapsiblePanel5.Visible = false;
                radGridView5.Visible = false;
            }
            var DaysInMonth = DateTime.DaysInMonth(DataDate.Year, DataDate.Month);

            if (DaysInMonth == 28)
            {
                radGridView1.Columns["Day29"].IsVisible = false;
                radGridView1.Columns["Day30"].IsVisible = false;
                radGridView1.Columns["Day31"].IsVisible = false;
                if (_Direct == 1)
                {
                    radGridView5.Columns["Night29"].IsVisible = false;
                    radGridView5.Columns["Night30"].IsVisible = false;
                    radGridView5.Columns["Night31"].IsVisible = false;
                }
            }
            else
            {
                if (DaysInMonth == 29)
                {
                    radGridView1.Columns["Day30"].IsVisible = false;
                    radGridView1.Columns["Day31"].IsVisible = false;
                    if (_Direct == 1)
                    {
                        radGridView5.Columns["Night30"].IsVisible = false;
                        radGridView5.Columns["Night31"].IsVisible = false;
                    }
                }
                else
                {
                    if (DaysInMonth == 30)
                    {
                        radGridView1.Columns["Day31"].IsVisible = false;
                        if (_Direct == 1)
                            radGridView5.Columns["Night31"].IsVisible = false;
                    }
                    else
                    {
                        radGridView1.Columns["Day29"].IsVisible = true;
                        radGridView1.Columns["Day30"].IsVisible = true;
                        radGridView1.Columns["Day31"].IsVisible = true;
                        if (_Direct == 1)
                        {
                            radGridView5.Columns["Night29"].IsVisible = true;
                            radGridView5.Columns["Night30"].IsVisible = true;
                            radGridView5.Columns["Night31"].IsVisible = true;
                        }
                    }
                }
            }
        }

        private void PaintCell(string cellName)
        {
            foreach (var row in radGridView1.Rows)
                if ((row.Cells[cellName].Value != null) && (row.Cells[cellName].Value.ToString() != string.Empty))
                {
                    row.Cells[cellName].Style.CustomizeFill = true;
                    row.Cells[cellName].Style.DrawFill = true;
                    row.Cells[cellName].Style.BackColor = Color.FromArgb(0x8B, 0xE5, 0xA2);
                }
        }

        public List<DateTime> getSundays()
        {
            var lstSundays = new List<DateTime>();
            var intMonth = DataDate.Month;
            var intYear = DataDate.Year;
            var ci = new CultureInfo("en-US");
            for (var i = 1; i <= ci.Calendar.GetDaysInMonth(intYear, intMonth); i++)
                if (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Sunday)
                    lstSundays.Add(new DateTime(intYear, intMonth, i));
            return lstSundays;
        }
    }
}