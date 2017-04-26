using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel;
using HHumanResources.Forms.Workingday.Export;
using HRMBLL.H1;
using HRMUtil;
using Telerik.WinControls.UI;
using Constants = HRMUtil.Constants;
using PositionChangedEventArgs = Telerik.WinControls.UI.Data.PositionChangedEventArgs;

namespace HumanResources.Forms.Workingday
{
    public partial class frm_ExportWorkingday : RadForm
    {
        public frm_ExportWorkingday()
        {
            InitializeComponent();
        }

        private void BindDataToGrid()
        {
            var isVCQLNN = int.Parse(rddlSalaryTableType.SelectedValue.ToString());
            var list = WorkdayCoefficientEmployeesFinalBLL.GetByDataDate(rDTPDate.Value, isVCQLNN, 2);
            rgwListCoefficient.DataSource = list;

            GridFormatting();
        }

        private void GridFormatting()
        {
            rgwListCoefficient.MasterTemplate.Columns[0].IsPinned = true;
            rgwListCoefficient.MasterTemplate.Columns[1].IsPinned = true;
            rgwListCoefficient.MasterTemplate.Columns[2].IsPinned = true;
            rgwListCoefficient.Columns[0].FormatString = StringFormat.FormatGridUserId;
            rgwListCoefficient.Columns[2].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[3].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[4].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[5].FormatString = StringFormat.FormatCurrency;
            rgwListCoefficient.Columns[6].FormatString = StringFormat.FormatCurrency;
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
        }

        private void frm_ExportWorkingday_Load(object sender, EventArgs e)
        {
            BindDataToDDLSalaryTableType();
            rDTPDate.Value = DateTime.Now.AddMonths(-1);
        }

        private void BindDataToDDLSalaryTableType()
        {
            rddlSalaryTableType.DisplayMember = "UnitName";
            rddlSalaryTableType.ValueMember = "UnitId";
            rddlSalaryTableType.DataSource = Constants.GetAllVCQLNN(true);
        }

        private void rbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!backgroundWorkerExport.IsBusy)
                {
                    var fullName = string.Empty;
                    progressBarImport.Maximum = rgwListCoefficient.RowCount;
                    backgroundWorkerExport.RunWorkerAsync(fullName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void rDTPDate_ValueChanged(object sender, EventArgs e)
        {
            BindDataToGrid();
        }

        private _Application runExport(string fullName)
        {
            return CoefficientEmployeeFinalExport.ExportCoefficientForApproval(saveFileDialogExportExcel.FileName,
                rgwListCoefficient, rDTPDate.Value, fullName, backgroundWorkerExport);
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
                                         rgwListCoefficient.RowCount;
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