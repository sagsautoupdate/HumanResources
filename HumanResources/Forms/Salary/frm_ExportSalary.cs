using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel;
using HRMBLL.H1;
using HRMUtil;
using HumanResources.Forms.Salary.Export;
using Telerik.WinControls.UI;
using Constants = HRMUtil.Constants;
using PositionChangedEventArgs = Telerik.WinControls.UI.Data.PositionChangedEventArgs;

namespace HumanResources.Forms.Salary
{
    public partial class frm_ExportSalary : RadForm
    {
        public frm_ExportSalary()
        {
            InitializeComponent();
        }

        private void BindDataToGrid()
        {
            try
            {
                var isVCQLNN = int.Parse(rddlSalaryTableType.SelectedValue.ToString());
                var list = IncomeEmployeesBLL.GetDataTableByDataDate(rDTPDate.Value, isVCQLNN, string.Empty);
                rgwListIncome.DataSource = list;

                GridFormatting();
            }
            catch
            {
            }
        }

        private void GridFormatting()
        {
            rgwListIncome.MasterTemplate.Columns[0].IsPinned = true;
            rgwListIncome.MasterTemplate.Columns[1].IsPinned = true;
            rgwListIncome.MasterTemplate.Columns[2].IsPinned = true;
            rgwListIncome.Columns[0].FormatString = StringFormat.FormatGridUserId;
            rgwListIncome.Columns[2].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[3].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[4].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[5].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[6].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[7].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[8].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[9].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[10].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[11].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[12].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[13].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[14].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[15].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[16].FormatString = StringFormat.FormatCurrency;
        }

        private void BindDataToDDLSalaryTableType()
        {
            rddlSalaryTableType.DisplayMember = "UnitName";
            rddlSalaryTableType.ValueMember = "UnitId";
            rddlSalaryTableType.DataSource = Constants.GetAllVCQLNN(true);
        }

        private void rDTPDate_ValueChanged(object sender, EventArgs e)
        {
            BindDataToDDLSalaryTableType();
            BindDataToGrid();
        }

        private void ExportSalary_Load(object sender, EventArgs e)
        {
            rDTPDate.Value = DateTime.Now.AddMonths(-1);
            BindDataToGrid();
        }

        private void rbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!backgroundWorkerExport.IsBusy)
                {
                    var fullName = string.Empty;
                    progressBarImport.Maximum = rgwListIncome.RowCount;
                    backgroundWorkerExport.RunWorkerAsync(fullName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private _Application runExport(string fullName)
        {
            if (rrSalaryApproval.IsChecked)
                return IncomeEmployeeExport.ExportSalaryForApproval(rgwListIncome, rDTPDate.Value, fullName,
                    backgroundWorkerExport);
            if (rrbSalaryBank.IsChecked)
                return IncomeEmployeeExport.ExportSalaryForBank(saveFileDialogExportExcel.FileName, rgwListIncome,
                    rDTPDate.Value, fullName, backgroundWorkerExport);
            return null;
        }

        private void backgroundWorkerExport_DoWork(object sender, DoWorkEventArgs e)
        {
            var fullName = (string) e.Argument;
            e.Result = runExport(fullName);
        }

        private void backgroundWorkerExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorkerExport.CancellationPending)
            {
                progressBarImport.Value1 = e.ProgressPercentage;
                progressBarImport.Text = "Đang trích xuất dữ liệu: " + e.ProgressPercentage + "/" +
                                         rgwListIncome.RowCount;
            }
        }

        private void backgroundWorkerExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var oExcel = (_Application) e.Result;
            _Workbook oWorkBook = oExcel.Workbooks[1];


            progressBarImport.Text = "Trích xuất dữ liệu thành công";

            saveFileDialogExportExcel.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            saveFileDialogExportExcel.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            saveFileDialogExportExcel.FilterIndex = 1;

            if (saveFileDialogExportExcel.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialogExportExcel.FileName))
                    File.Delete(saveFileDialogExportExcel.FileName);
                oWorkBook.SaveAs(saveFileDialogExportExcel.FileName, XlFileFormat.xlWorkbookNormal, null, null, false,
                    false, XlSaveAsAccessMode.xlShared, false, false, null, null, null);


                oWorkBook.Close(null, null, null);
                oExcel.Workbooks.Close();
                oExcel.Quit();

                Marshal.ReleaseComObject(oExcel);

                Marshal.ReleaseComObject(oWorkBook);
                oExcel = null;
                GC.Collect();
            }
        }

        private void rddlSalaryTableType_SelectedIndexChanged(object sender, PositionChangedEventArgs e)
        {
            BindDataToGrid();
        }
    }
}