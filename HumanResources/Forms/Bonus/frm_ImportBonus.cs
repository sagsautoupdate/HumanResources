using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using HRMBLL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Bonus
{
    public partial class frm_ImportBonus : RadForm
    {
        public static string SelectedTable = string.Empty;

        public frm_ImportBonus()
        {
            InitializeComponent();
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

        private void runImportToDataBase(string fullName)
        {
            var ACVId = string.Empty;
            var UserId = 0;
            var paydate = rDTPPayDate.Value;
            var bonusyear = rDTPBonusYear.Value.Year;
            var bonusTitleId = int.Parse(rddlBonusTitle.SelectedItem.Value.ToString());
            var BonusTitleDetail = rddlBonusTitle.SelectedItem.Text;
            decimal LCD = 0;
            decimal K = 0;
            decimal X = 0;
            decimal OmCoKHH = 0;
            decimal TS = 0;
            decimal TNLD = 0;
            decimal FDiDuong = 0;
            decimal Khac = 0;
            decimal NgayCongThuong = 0;
            decimal HSLNSThuong = 0;
            decimal BonusValue = 0;
            decimal ThueThuNhap = 0;
            decimal ThueQuyetToan = 0;
            decimal ThucLinh = 0;
            var MonthNumber = 0;
            var MonthNumber_tv = 0;


            for (var i = 0; i < rGVData.Rows.Count; i++)
            {
                var row = rGVData.Rows[i];


                if (row.Cells["Mã SAGS"].Value.ToString().Trim().Length > 0)
                {
                    UserId = int.Parse(row.Cells["Mã SAGS"].Value.ToString().Trim());
                    MonthNumber =
                        Convert.ToInt32(row.Cells["Số tháng làm việc"] == null
                            ? "0"
                            : row.Cells["Số tháng làm việc"].Value);
                    MonthNumber_tv =
                        Convert.ToInt32(row.Cells["Số tháng làm việc tv"] == null
                            ? "0"
                            : row.Cells["Số tháng làm việc tv"].Value);


                    HSLNSThuong =
                        Convert.ToDecimal(row.Cells["HSL tính thưởng"] == null
                            ? "0"
                            : row.Cells["HSL tính thưởng"].Value);
                    BonusValue =
                        Convert.ToDecimal(row.Cells["Tiền thưởng"] == null ? "0" : row.Cells["Tiền thưởng"].Value);
                    ThueThuNhap = Convert.ToDecimal(row.Cells["Thuế TNCN"] == null ? "0" : row.Cells["Thuế TNCN"].Value);

                    ThucLinh = Convert.ToDecimal(row.Cells["Thực lĩnh"] == null ? "0" : row.Cells["Thực lĩnh"].Value);

                    BonusEmployeeConditionBLL.Update(MonthNumber, MonthNumber_tv, LCD, K, X, OmCoKHH, TS, TNLD, FDiDuong,
                        Khac, NgayCongThuong, HSLNSThuong, BonusValue, ThueThuNhap, ThueQuyetToan, ThucLinh,
                        BonusTitleDetail, UserId, bonusTitleId, bonusyear, paydate);
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

        private void BindDataToDDLBonusTitle()
        {
            rddlBonusTitle.DisplayMember = BonusTitleKeys.Field_BonusTitle_BonusTitle;
            rddlBonusTitle.ValueMember = BonusTitleKeys.Field_BonusTitle_BonusTitleId;
            rddlBonusTitle.DataSource = BonusTitle.GetByType(1);
        }

        private void frm_ImportBonus_Load(object sender, EventArgs e)
        {
            rDTPPayDate.Value = DateTime.Now;

            BindDataToDDLBonusTitle();
        }
    }
}