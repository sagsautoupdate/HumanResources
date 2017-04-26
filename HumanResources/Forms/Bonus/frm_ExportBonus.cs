using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel;
using HRMBLL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;
using HumanResources.Forms.Bonus.Export;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Bonus
{
    //Test
    public partial class frm_ExportBonus : RadForm
    {
        public frm_ExportBonus()
        {
            InitializeComponent();
        }

        private void frm_ExportBonus_Load(object sender, EventArgs e)
        {
            rDTPPayDate.Value = DateTime.Now;
            BindDataToDDLBonusTitle();
            rbtnView_Click(this, null);
        }

        private void BindDataToGrid()
        {
            var list = BonusEmployeeConditionBLL.GetByFilter(rDTPBonusYear.Value.Year, rDTPPayDate.Value,
                int.Parse(rddlBonusTitle.SelectedValue.ToString()), string.Empty);
            rgwListBonus.DataSource = list;

            GridFormatting();
        }

        private void GridFormatting()
        {
            rgwListBonus.MasterTemplate.Columns[0].IsPinned = true;
            rgwListBonus.MasterTemplate.Columns[1].IsPinned = true;
            rgwListBonus.MasterTemplate.Columns[2].IsPinned = true;
            rgwListBonus.Columns[0].FormatString = StringFormat.FormatGridUserId;
            rgwListBonus.Columns[3].FormatString = StringFormat.FormatCurrency;
            rgwListBonus.Columns[11].FormatString = StringFormat.FormatCurrency;
            rgwListBonus.Columns[12].FormatString = StringFormat.FormatCurrency;
            rgwListBonus.Columns[13].FormatString = StringFormat.FormatCurrency;
        }

        private void BindDataToDDLBonusTitle()
        {
            rddlBonusTitle.DisplayMember = BonusTitleKeys.Field_BonusTitle_BonusTitle;
            rddlBonusTitle.ValueMember = BonusTitleKeys.Field_BonusTitle_BonusTitleId;
            rddlBonusTitle.DataSource = BonusTitle.GetByType(1);
        }

        private void rbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!backgroundWorkerExport.IsBusy)
                {
                    var fullName = string.Empty;
                    progressBarImport.Maximum = rgwListBonus.RowCount;
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
            var bonusTitleId = int.Parse(rddlBonusTitle.SelectedItem.Value.ToString());
            if (rrSalaryApproval.IsChecked)
            {
                if ((bonusTitleId == 7) || (bonusTitleId == 9))
                    return BonusEmployeeExport.ExportQuyThuongForApproval(rgwListBonus, rDTPPayDate.Value, fullName,
                        backgroundWorkerExport);
                if (bonusTitleId == 5)
                    return BonusEmployeeExport.ExportBSDTForApproval(rgwListBonus, rDTPPayDate.Value,
                        rDTPBonusYear.Value.Year, fullName, backgroundWorkerExport);
                if ((bonusTitleId == 1) || (bonusTitleId == 2) || (bonusTitleId == 3) || (bonusTitleId == 4))
                    return BonusEmployeeExport.ExportLeTetForApproval(rgwListBonus, rDTPPayDate.Value,
                        rDTPBonusYear.Value.Year, fullName, backgroundWorkerExport);
                return null;
            }
            if (rrbSalaryBank.IsChecked)
                return BonusEmployeeExport.ExportBonusForBank(saveFileDialogExportExcel.FileName, rgwListBonus,
                    rDTPPayDate.Value, fullName, backgroundWorkerExport);
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
                                         rgwListBonus.RowCount;
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

        private void rbtnView_Click(object sender, EventArgs e)
        {
            BindDataToGrid();
            if (rgwListBonus.RowCount > 1)
                rbtnExport.Enabled = true;
            else
                rbtnExport.Enabled = false;
        }
    }
}