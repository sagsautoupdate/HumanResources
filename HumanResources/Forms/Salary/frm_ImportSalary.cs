using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using HRMBLL.H1;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Salary
{
    public partial class frm_ImportSalary : RadForm
    {
        public static string SelectedTable = string.Empty;

        public frm_ImportSalary()
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
            var dataDate = rDTPDate.Value;


            decimal LNS = 0;
            decimal LCB = 0;
            decimal PCCV = 0;
            decimal PCTN = 0;
            decimal TienAnGiuaCa = 0;
            decimal BoSungLuong = 0;
            decimal TienThemGio = 0;
            decimal TienLamDem = 0;
            decimal TotalIncome = 0;
            decimal TotalIncomeForTax = 0;
            decimal BHXH = 0;
            decimal BHYT = 0;
            decimal BHTN = 0;
            decimal DoanPhiCD = 0;
            decimal ThueThuNhap = 0;
            decimal TruocThucLinh = 0;
            decimal DongGop = 0;
            decimal Luong = 0;
            decimal TNTHieuQuaCongViec = 0;
            decimal ThucLinh = 0;

            var createUserId = clsGlobal.UserId;
            var updateUserId = clsGlobal.UserId;
            var updateDate = DateTime.Now;
            var createDate = DateTime.Now;
            var Lock = false;
            var remark = "Import Excel";


            for (var i = 0; i < rGVData.Rows.Count; i++)
            {
                var row = rGVData.Rows[i];

                ACVId = row.Cells["Mã nhân viên"] == null ? "0" : row.Cells["Mã nhân viên"].Value.ToString().Trim();

                if (row.Cells["Mã SAGS"].Value.ToString().Trim().Length > 0)
                {
                    UserId = int.Parse(row.Cells["Mã SAGS"].Value.ToString().Trim());

                    LNS =
                        Convert.ToDecimal(row.Cells["Lương năng suất"] == null ? 0 : row.Cells["Lương năng suất"].Value);
                    LCB = Convert.ToDecimal(row.Cells["Lương cơ bản"] == null ? 0 : row.Cells["Lương cơ bản"].Value);
                    PCCV = Convert.ToDecimal(row.Cells["PCCV"] == null ? 0 : row.Cells["PCCV"].Value);
                    PCTN = Convert.ToDecimal(row.Cells["PCTN"] == null ? 0 : row.Cells["PCTN"].Value);
                    TienAnGiuaCa =
                        Convert.ToDecimal(row.Cells["Tiền ăn giữa ca"] == null ? 0 : row.Cells["Tiền ăn giữa ca"].Value);
                    try
                    {
                        BoSungLuong =
                            Convert.ToDecimal(row.Cells["Bổ sung lương"] == null ? 0 : row.Cells["Bổ sung lương"].Value);
                    }
                    catch
                    {
                    }
                    TienThemGio =
                        Convert.ToDecimal(row.Cells["Tiền thêm giờ"] == null ? 0 : row.Cells["Tiền thêm giờ"].Value);
                    TienLamDem =
                        Convert.ToDecimal(row.Cells["Tiền làm đêm"] == null ? 0 : row.Cells["Tiền làm đêm"].Value);

                    TotalIncome = Convert.ToDecimal(row.Cells["Tổng cộng"] == null ? 0 : row.Cells["Tổng cộng"].Value);
                    TotalIncomeForTax = 0;


                    BHXH = Convert.ToDecimal(row.Cells["BHXH"] == null ? 0 : row.Cells["BHXH"].Value);
                    BHYT = Convert.ToDecimal(row.Cells["BHYT"] == null ? 0 : row.Cells["BHYT"].Value);
                    BHTN = Convert.ToDecimal(row.Cells["BH thất nghiệp"] == null ? 0 : row.Cells["BH thất nghiệp"].Value);
                    DoanPhiCD = Convert.ToDecimal(row.Cells["Đoàn phí CĐ"] == null ? 0 : row.Cells["Đoàn phí CĐ"].Value);
                    try
                    {
                        ThueThuNhap =
                            Convert.ToDecimal(row.Cells["Thuế thu nhập"] == null ? 0 : row.Cells["Thuế thu nhập"].Value);
                    }
                    catch
                    {
                    }

                    TruocThucLinh =
                        Convert.ToDecimal(row.Cells["Trước Thực lĩnh"] == null ? 0 : row.Cells["Trước Thực lĩnh"].Value);
                    try
                    {
                        DongGop = Convert.ToDecimal(row.Cells["DongGop"] == null ? 0 : row.Cells["DongGop"].Value);
                    }
                    catch
                    {
                        DongGop = 0;
                    }

                    Luong = Convert.ToDecimal(row.Cells["LƯƠNG"] == null ? 0 : row.Cells["LƯƠNG"].Value);
                    TNTHieuQuaCongViec =
                        Convert.ToDecimal(row.Cells["THU NHẬP THEO HIỆU QUẢ CÔNG VIỆC"] == null
                            ? 0
                            : row.Cells["THU NHẬP THEO HIỆU QUẢ CÔNG VIỆC"].Value);
                    ThucLinh = Convert.ToDecimal(row.Cells["Thực lĩnh"] == null ? 0 : row.Cells["Thực lĩnh"].Value);

                    IncomeEmployeesBLL.ImportFromExcelACV(dataDate, ACVId,
                        LNS, LCB, PCCV, PCTN, TienAnGiuaCa, BoSungLuong, TienThemGio, TienLamDem,
                        TotalIncome, TotalIncomeForTax,
                        BHXH, BHYT, BHTN, DoanPhiCD, ThueThuNhap,
                        TruocThucLinh, DongGop, Luong, TNTHieuQuaCongViec, ThucLinh,
                        createDate, createUserId, updateDate, updateUserId,
                        Lock, Constants.DataType_Import, remark, UserId);
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

        private void ImportSalary_Load(object sender, EventArgs e)
        {
            rDTPDate.Value = DateTime.Now.AddMonths(-1);
        }
    }
}