using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel;
using HRMBLL.H;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using HumanResources.Forms.Export.Workingday;
using HumanResources.Properties;
using HumanResources.Utilities;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Application = Excel.Application;
using Constants = HRMUtil.Constants;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using Range = Excel.Range;
using StringFormat = HRMUtil.StringFormat;

namespace HumanResources.Forms.Export
{
    public partial class CustomExport : RadForm
    {
        private static readonly DateTime MinValue = FormatDate.GetSQLDateMinValue;
        private static CustomExport s_Instance;
        private readonly DataTable dt = AdministrationBLL.GetAllTranslatedColumn();
        private readonly RadWaitingBar rwb = new RadWaitingBar();
        private List<string> _lstSheet;
        private RadHostItem h;
        private RadGridView radGridView2;
        private RadTreeView radTreeView1;
        public CustomExport()
        {
            InitializeComponent();
        }

        public static CustomExport Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new CustomExport();
                return s_Instance;
            }
        }

        private void CustomExport_Load(object sender, EventArgs e)
        {
            radCheckedDropDownList1.DataSource = BindSheetList();
            radCheckedDropDownList1.DisplayMember = "LeaveName";
            radCheckedDropDownList1.ValueMember = "LeaveName";

            radCheckedDropDownList1.AutoCompleteDataSource = BindSheetList();
            radCheckedDropDownList1.AutoCompleteDisplayMember = "LeaveName";
            radCheckedDropDownList1.AutoCompleteValueMember = "LeaveName";

            radDropDownList3.DataSource = Constants.GetAllYears();
            radDropDownList3.ValueMember = "UnitId";
            radDropDownList3.DisplayMember = "UnitName";
            radDropDownList3.SelectedIndex = 0;

            radDropDownList2.SelectedIndex = 0;

            radDropDownList1.DataSource = GetList();
            radDropDownList1.ValueMember = "TableNameValue";
            radDropDownList1.DisplayMember = "TableNameDescription";

            radDropDownList1.SelectedValue = "View_H0_DepartmentEmployee";
            
            InitRadtree();

            InitRadGrid();
            
            radDropDownList1.SelectedValueChanged += RadDropDownList1_SelectedValueChanged;
            rRefresh();
        }

        public RadContextMenu DefaultRadContextMenu()
        {
            var _rcm = new RadContextMenu();
            _rcm.ThemeName = "Windows8";

            switch (radDropDownList1.SelectedValue.ToString())
            {
                case "View_H0_DepartmentEmployee":
                    break;
                case "View_H0_EmployeeContracts":
                {
                    var _rmi = new RadMenuItem
                    {
                        Text = "Hợp đồng đến hạn",
                        Image = Resources.Excel_Export_,
                        Name = "rmiDenHan"
                    };
                    _rmi.Click += RmiDenHanOnClick;
                    _rcm.Items.Add(_rmi);
                    _rmi = new RadMenuItem
                    {
                        Text = "Hợp đồng chuyển đổi",
                        Image = Resources.Excel_Export_,
                        Name = "rmiChuyenDoi"
                    };
                    _rmi.Click += RmiChuyenDoiOnClick;
                    _rcm.Items.Add(_rmi);
                }
                    break;
                case "View_H0_Employee_Leaves_Detail":
                    break;
                case "View_H0_EmployeeSubContract":
                    break;
                case "View_H0_EmployeeSecurityControl":
                    break;
                case "View_H1_WorkdayCoefficientEmployeesFinal":
                    break;
            }


            return _rcm;
        }

        private void RmiChuyenDoiOnClick(object sender, EventArgs eventArgs)
        {
            radGridView1.FilterDescriptors.Clear();

            var compositeFilter = new CompositeFilterDescriptor();
            compositeFilter.FilterDescriptors.Add(new FilterDescriptor("FromDate", FilterOperator.IsGreaterThanOrEqualTo,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
            compositeFilter.FilterDescriptors.Add(new FilterDescriptor("FromDate", FilterOperator.IsLessThanOrEqualTo,
                DateTime.Now));
            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
            radGridView1.FilterDescriptors.Add(compositeFilter);
        }

        private void RmiDenHanOnClick(object sender, EventArgs eventArgs)
        {
            radGridView1.FilterDescriptors.Clear();

            var compositeFilter = new CompositeFilterDescriptor();
            compositeFilter.FilterDescriptors.Add(new FilterDescriptor("ToDate", FilterOperator.IsLessThanOrEqualTo,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(2)));
            compositeFilter.FilterDescriptors.Add(new FilterDescriptor("ToDate", FilterOperator.IsNotEqualTo,
                new DateTime(1753, 01, 01)));
            compositeFilter.LogicalOperator = FilterLogicalOperator.And;

            var filter2 = new FilterDescriptor("Active", FilterOperator.IsEqualTo, 1);
            var compositeFilter1 = new CompositeFilterDescriptor();
            compositeFilter1.FilterDescriptors.Add(new FilterDescriptor("FromDate", FilterOperator.IsNull, null));
            compositeFilter1.FilterDescriptors.Add(new FilterDescriptor("ToDate", FilterOperator.IsNull, null));
            compositeFilter1.LogicalOperator = FilterLogicalOperator.And;

            var filterDescriptor1 = new CompositeFilterDescriptor();
            filterDescriptor1.FilterDescriptors.Add(compositeFilter1);
            filterDescriptor1.FilterDescriptors.Add(filter2);
            filterDescriptor1.LogicalOperator = FilterLogicalOperator.And;

            var filterDescriptor2 = new CompositeFilterDescriptor();
            filterDescriptor2.FilterDescriptors.Add(compositeFilter);
            filterDescriptor2.FilterDescriptors.Add(filterDescriptor1);
            filterDescriptor2.LogicalOperator = FilterLogicalOperator.Or;

            radGridView1.FilterDescriptors.Add(filterDescriptor2);
        }

        public void InitRadGrid()
        {
            radGridView2 = new RadGridView();
            radGridView2.Dock = DockStyle.Fill;
            radGridView2.AutoGenerateColumns = true;
            radGridView2.AllowSearchRow = true;
            radGridView2.AllowEditRow = true;
            radGridView2.AllowAddNewRow = false;
            radGridView2.AllowDeleteRow = false;
            radGridView2.AllowDragToGroup = false;
            radGridView2.ShowGroupPanel = false;
            radGridView2.AutoScroll = true;

            radGridView2.Columns.Add(new GridViewCheckBoxColumn("ColCheck"));
            radGridView2.Columns.Add(new GridViewTextBoxColumn("ColName", "ColName"));
            radGridView2.Columns.Add(new GridViewTextBoxColumn("ColNameInVNese", "ColNameInVNese"));

            radGridView2.DataSource = //AdministrationBLL.GetExportTable(radDropDownList1.SelectedValue.ToString());
                Utilities.Utilities.TableColumnsForGrid(
                    AdministrationBLL.GetExportTable(radDropDownList1.SelectedValue.ToString()));

            radGridView2.Columns["ColName"].ReadOnly = true;
            Utilities.Utilities.GridFormatting(radGridView2);
            radGridView2.ValueChanged += RadGridView2_ValueChanged;
            h = new RadHostItem(radGridView2);

            radDropDownList5.DropDownMinSize = new Size(radDropDownList4.Width, 300);

            if (radDropDownList5.DropDownListElement.ListElement.Parent != null)
                radDropDownList5.DropDownListElement.ListElement.Children.Insert(0, h);
        }

        private void RadGridView2_ValueChanged(object sender, EventArgs e)
        {
            if (radGridView2.ActiveEditor is RadCheckBoxEditor)
            {
                if (radGridView2.ActiveEditor.Value.ToString().Equals("On"))
                    radGridView1.Columns.Add(
                        new GridViewTextBoxColumn(radGridView2.CurrentRow.Cells["ColName"].Value.ToString(),
                            radGridView2.CurrentRow.Cells["ColName"].Value.ToString()));
                else
                    radGridView1.Columns.Remove(radGridView2.CurrentRow.Cells["ColName"].Value.ToString());
                Utilities.Utilities.GridFormatting(radGridView1);
            }
        }

        private void searchBox_Search(object sender, SearchTextBox.SearchBoxEventArgs e)
        {
            RadMessageBox.Show("Search >> " + e.SearchText);
        }

        private List<LeaveNameExport> BindSheetList()
        {
            var _lst = new List<LeaveNameExport>();
            _lst.Add(new LeaveNameExport(9999, "Total"));
            _lst.Add(new LeaveNameExport(5, "F"));
            _lst.Add(new LeaveNameExport(1, "Om"));
            _lst.Add(new LeaveNameExport(3, "TS"));
            _lst.Add(new LeaveNameExport(8, "Ko"));
            _lst.Add(new LeaveNameExport(18, "Co"));
            _lst.Add(new LeaveNameExport(2, "OmDN"));
            _lst.Add(new LeaveNameExport(34, "OmDNBHXH"));
            _lst.Add(new LeaveNameExport(19, "KHH"));
            _lst.Add(new LeaveNameExport(20, "ST"));
            _lst.Add(new LeaveNameExport(21, "Khamthai"));
            _lst.Add(new LeaveNameExport(4, "TNLD"));
            _lst.Add(new LeaveNameExport(6, "Fdb"));
            _lst.Add(new LeaveNameExport(7, "Ro"));
            _lst.Add(new LeaveNameExport(9, "Diduong"));
            _lst.Add(new LeaveNameExport(23, "DinhChiCT"));

            return _lst;
        }

        public void InitRadtree()
        {
            radTreeView1 = new RadTreeView();
            radTreeView1.ShowLines = true;


            PopulateRootLevel(radTreeView1);

            radTreeView1.Dock = DockStyle.Fill;
            h = new RadHostItem(radTreeView1);
            radDropDownList4.DropDownMinSize = new Size(radDropDownList4.Width, 500);
            radTreeView1.SelectedNodeChanged += radTreeView1_SelectedNodeChanged;

            if (radDropDownList4.DropDownListElement.ListElement.Parent != null)
                radDropDownList4.DropDownListElement.ListElement.Children.Insert(0, h);
            radTreeView1.ExpandAll();
        }

        public void PopulateRootLevel(RadTreeView radTreeView)
        {
            var list = DepartmentsBLL.GetDepartmentRoot();
            PopulateNodes(list, radTreeView.Nodes, radTreeView);
            radTreeView.Nodes[0].Expand();
        }

        public void PopulateNodes(List<DepartmentsBLL> list, RadTreeNodeCollection nodes, RadTreeView rtv)
        {
            foreach (var obj in list)
            {
                var tn = new RadTreeNode
                {
                    Text = obj.DepartmentName,
                    Value = obj.DepartmentId.ToString(),
                    ImageIndex = obj.Level
                };

                nodes.Add(tn);


                if (obj.ChildNodeCount > 0)
                {
                    PopulateSubLevel(obj.DepartmentId, tn, rtv);
                    tn.Font = new Font(rtv.Font, FontStyle.Bold);
                }
            }
        }

        public void PopulateSubLevel(int parentid, RadTreeNode parentNode, RadTreeView rtv)
        {
            var list = DepartmentsBLL.GetAll_SubLevel(parentid);
            PopulateNodes(list, parentNode.Nodes, rtv);
        }

        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            radButton1.PerformClick();
        }

        private void rRefresh()
        {
            Cursor.Current = Cursors.AppStarting;
            if (radGridView1.Columns.Count > 0)
                foreach (var VARIABLE in radGridView1.Columns)
                    VARIABLE.IsVisible = (string.Compare(VARIABLE.Name, "Status", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "RootId", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "DepartmentId", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "ParentId", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "PositionId", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "Level", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "UserId", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "FullName", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "PositionName", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "RootName", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "DepartmentName", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "DepartmentFullName", false) == 0) ||
                                         (string.Compare(VARIABLE.Name, "LevelPosition", false) == 0);
            radGridView2.DataSource =
                Utilities.Utilities.TableColumnsForGrid(
                    AdministrationBLL.GetExportTable(radDropDownList1.SelectedValue.ToString()));

            switch (radDropDownList1.SelectedValue.ToString())
            {
                case "View_H0_DepartmentEmployee":
                    IsOpenDepartment(true);
                    IsOpenLeaveList(false);
                    IsOpenMonthYear(false);
                    radGridView1.Name = "radGridView1";
                    break;
                case "View_H0_EmployeeContracts":
                    IsOpenDepartment(true);
                    IsOpenLeaveList(false);
                    IsOpenMonthYear(false);
                    radGridView1.Name = "radGridView1";
                    break;
                case "View_H0_Employee_Leaves_Detail":
                    IsOpenDepartment(true);
                    IsOpenLeaveList(false);
                    IsOpenMonthYear(true);
                    radGridView1.Name = "radGridView1";
                    break;
                case "View_H0_EmployeeSubContract":
                    IsOpenDepartment(true);
                    IsOpenLeaveList(false);
                    IsOpenMonthYear(false);
                    radGridView1.Name = "radGridView1";
                    break;
                case "View_H0_EmployeeSecurityControl":
                    IsOpenDepartment(true);
                    IsOpenLeaveList(false);
                    IsOpenMonthYear(false);
                    radGridView1.Name = "radGridView1";
                    break;
                case "View_H1_WorkdayCoefficientEmployeesFinal":
                    IsOpenDepartment(true);
                    IsOpenLeaveList(true);
                    IsOpenMonthYear(true);
                    radGridView1.Name = "Workingday";
                    break;
            }
            Cursor.Current = Cursors.Default;
        }

        private void IsOpenMonthYear(bool isOpen)
        {
            if (isOpen)
            {
                radDropDownList2.Enabled = true;
                radDropDownList3.Enabled = true;
            }
            else
            {
                radDropDownList2.Enabled = false;
                radDropDownList3.Enabled = false;
            }
        }

        private void IsOpenLeaveList(bool isOpen)
        {
            if (isOpen)
                radCheckedDropDownList1.Enabled = true;
            else
                radCheckedDropDownList1.Enabled = false;
        }

        private void IsOpenDepartment(bool isOpen)
        {
        }

        private void RadDropDownList1_SelectedValueChanged(object sender, EventArgs e)
        {
            rRefresh();
        }

        private List<string> strGetList()
        {
            var _lstTemp = new List<string>();
            _lstTemp.Add(("View_H0_DepartmentEmployee"));
            _lstTemp.Add(("View_H0_EmployeeContracts"));
            _lstTemp.Add(("View_H0_Employee_Leaves_Detail"));
            _lstTemp.Add(("View_H0_EmployeeSubContract"));
            _lstTemp.Add(("View_H0_EmployeeSecurityControl"));
            _lstTemp.Add(("View_H1_WorkdayCoefficientEmployeesFinal"));
            return _lstTemp;
        }

        private List<TableName> GetList()
        {
            var _lstTemp = new List<TableName>();
            _lstTemp.Add(new TableName("View_H0_DepartmentEmployee", "Danh sách nhân viên"));
            _lstTemp.Add(new TableName("View_H0_EmployeeContracts", "Danh sách hợp đồng"));
            _lstTemp.Add(new TableName("View_H0_Employee_Leaves_Detail", "Danh sách ngày phép"));
            _lstTemp.Add(new TableName("View_H0_EmployeeSubContract", "Danh sách phụ lục hợp đồng"));
            _lstTemp.Add(new TableName("View_H0_EmployeeSecurityControl", "Danh sách thẻ KSAN"));
            _lstTemp.Add(new TableName("View_H1_WorkdayCoefficientEmployeesFinal", "Danh sách chấm công hưởng lương"));
            return _lstTemp;
        }

        private void radListView1_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {
            var _origin = Font;
            var _bold = new Font(Font, FontStyle.Bold);

            if (GetVNIColumn(e.VisualItem.Text) != string.Empty)
            {
                e.VisualItem.Font = _bold;
                e.VisualItem.Text = e.VisualItem.Text + " - " + GetVNIColumn(e.VisualItem.Text);
            }
            else
            {
                e.VisualItem.Font = _origin;
                e.VisualItem.Text = e.VisualItem.Text;
            }
        }

        private string GetVNIColumn(string name)
        {
            var strReturn = string.Empty;
            foreach (DataRow dr in dt.Rows)
                if (dr["ColumnName"].ToString() == name)
                    strReturn = dr["ColumnNameInVI"].ToString();
            return strReturn;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;

            var _colName = string.Empty;

            foreach (var item in radGridView1.Columns)
                _colName += item.Name + ",";
            if ((_colName != null) && (_colName.Length > 0))
                _colName = _colName.Substring(0, _colName.Length - 1);
            var _condition = string.Empty;
            switch (radDropDownList1.SelectedValue.ToString())
            {
                case "View_H0_Employee_Leaves_Detail":
                    if (Convert.ToInt32(radDropDownList3.SelectedValue) > 0)
                        if (Convert.ToInt32(radDropDownList2.SelectedIndex) > 0)
                            _condition += " AND MONTH(FromDate)=" + Convert.ToInt32(radDropDownList2.SelectedIndex) +
                                          " AND YEAR(FromDate)=" + Convert.ToInt32(radDropDownList3.SelectedValue);
                        else
                            _condition += " AND YEAR(FromDate)=" + Convert.ToInt32(radDropDownList3.SelectedValue);
                    else
                        _condition += "";
                    break;
                case "View_H1_WorkdayCoefficientEmployeesFinal":
                {
                    if ((Convert.ToInt32(radDropDownList3.SelectedValue) > 0) &&
                        (Convert.ToInt32(radDropDownList2.SelectedIndex) > 0))
                    {
                        _condition += " AND MONTH(DataDate)=" + Convert.ToInt32(radDropDownList2.SelectedIndex) +
                                      " AND YEAR(DataDate)=" + Convert.ToInt32(radDropDownList3.SelectedValue);
                    }
                    else
                    {
                        if ((Convert.ToInt32(radDropDownList3.SelectedValue) > 0) &&
                            (Convert.ToInt32(radDropDownList2.SelectedIndex) <= 0))
                            _condition += " AND YEAR(DataDate)=" + Convert.ToInt32(radDropDownList3.SelectedValue);
                        else
                            _condition += "";
                    }
                    if (radCheckedDropDownList1.CheckedItems.Count > 0)
                    {
                        _lstSheet = new List<string>();
                        foreach (var item in radCheckedDropDownList1.CheckedItems)
                            _lstSheet.Add(item.Text);
                    }
                    else
                    {
                        MessageBox.Show("Chọn ít nhất một loại phép để xuất Excel");
                    }
                }
                    break;
                case "View_H0_EmployeeContracts":
                {
                    if (rbAll.CheckState == CheckState.Checked)
                        _condition += "";
                    if (rbDenHan.CheckState == CheckState.Checked)
                    {
                        _condition +=
                            $" AND Active = 1 AND ToDate <= '{new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(2).ToString("dd/MMM/yyyy")}' AND ToDate <> '{new DateTime(1753, 1, 1).ToString("dd/MMM/yyyy")}' OR (FromDate IS NULL AND ToDate IS NULL) AND Active = 1";
                        if (rbChuyenDoi.CheckState == CheckState.Checked)
                            _condition +=
                                $" AND Active = 1 AND MONTH(FromDate) = {DateTime.Now.Month} AND YEAR(FromDate) = {DateTime.Now.Year}";
                    }
                }
                    break;
            }

            if ((radTreeView1.SelectedNode != null) && (Convert.ToInt32(radTreeView1.SelectedNode.Value) > 0))
            {
                var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;

                _condition += " AND DepartmentId IN (" + departmentIds + ")";
            }
            else
            {
                _condition += string.Empty;
            }

            try
            {
                radGridView1.DataSource = AdministrationBLL.GetExportData(radDropDownList1.SelectedValue.ToString(),
                    _colName, _condition);
            }
            catch
            {
            }

            Utilities.Utilities.GridFormatting(radGridView1);

            radGridView1.Columns["Status"].IsVisible = false;
            radGridView1.Columns["RootId"].IsVisible = false;
            radGridView1.Columns["DepartmentId"].IsVisible = false;
            radGridView1.Columns["ParentId"].IsVisible = false;
            radGridView1.Columns["Level"].IsVisible = false;
            radGridView1.Columns["PositionId"].IsVisible = false;
            radGridView1.Columns["LevelPosition"].IsVisible = false;

            radLabel4.Text = "Tổng cộng: " + radGridView1.ChildRows.Count;
            Cursor.Current = Cursors.Default;
        }

        private void radGridView1_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            radLabel4.Text = "Tổng cộng: " + radGridView1.ChildRows.Count;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var fullName = (string) e.Argument;
            e.Result = runExport(fullName);
        }

        private _Application runExport(string fullName)
        {
            return ExecuteExport(radGridView1, radDropDownList1.SelectedItem.Text,
                radDropDownList1.SelectedItem.Text);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
                Utilities.Utilities.SetWaitingText(rwb,
                    $"Exporting: {e.ProgressPercentage}/{radGridView1.ChildRows.Count}");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Utilities.Utilities.StopWaiting(rwb, radGridView1);
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            if (radGridView1.Rows.Count <= 0)
                MessageBox.Show("Không có dữ liệu!");
            else
                try
                {
                    if (!backgroundWorker1.IsBusy)
                    {
                        var fullName = string.Empty;
                        backgroundWorker1.RunWorkerAsync(fullName);

                        Utilities.Utilities.ShowWaiting(rwb, radGridView1);
                    }
                }
                catch
                {
                    MessageBox.Show("Lỗi trích xuất dữ liệu!");
                }
        }

        private void radCheckBox1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            Filter();
        }

        private void Filter()
        {
            radGridView1.FilterDescriptors.Clear();

            var filterWorking = new FilterDescriptor();
            filterWorking.PropertyName = "Status";
            filterWorking.Operator = FilterOperator.IsEqualTo;
            filterWorking.Value = "1";
            filterWorking.IsFilterEditor = true;

            if (radCheckBox1.Checked)
                radGridView1.FilterDescriptors.Add(filterWorking);
            else
                radGridView1.FilterDescriptors.Remove(filterWorking);
        }

        private void radGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
        }


        public List<string> GetGridViewColumnName(RadGridView rgv)
        {
            var lstCol = new List<string>();
            foreach (var col in rgv.Columns)
                if ((col.Name != string.Empty) && col.IsVisible && (col.Name != "FullName2"))
                    lstCol.Add(col.Name);
            return lstCol;
        }

        public void CreateHeaderByGridViewColumnName_KSANFormat(List<string> lstColName,
            ref _Worksheet oWorkSheet, ref int initTitleIndex, string oWorkSheetName)
        {
            var rangeHeader1 = oWorkSheet.get_Range("A1", "O1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "Phụ lục IX:";
            rangeHeader1.Font.Size = 12;
            rangeHeader1.Font.Bold = true;
            rangeHeader1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader1.Font.Name = "Times New Roman";

            var rangeHeader2 = oWorkSheet.get_Range("A2", "O2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = "MẪU DANH SÁCH ĐỀ NGHỊ CẤP THẺ KIỂM SOÁT AN NINH";
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader2.Font.Name = "Times New Roman";

            var rangeHeader3 = oWorkSheet.get_Range("A3", "O3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "CẢNG HÀNG KHÔNG, SÂN BAY CÓ GIÁ TRỊ SỬ DỤNG DÀI HẠN";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Name = "Times New Roman";

            var rangeHeader4 = oWorkSheet.get_Range("A4", "O4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "(Ban hành kèm theo Thông tư số 01/2016/TT-BGTVT ngày 01/02/2016";
            rangeHeader4.Font.Size = 12;
            rangeHeader4.Font.Italic = true;
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Name = "Times New Roman";

            var rangeHeader5 = oWorkSheet.get_Range("A5", "O5");
            rangeHeader5.Merge(Type.Missing);
            oWorkSheet.Cells[5, 1] = "của Bộ trưởng Bộ Giao thông vận tải)";
            rangeHeader5.Font.Size = 12;
            rangeHeader5.Font.Italic = true;
            rangeHeader5.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader5.Font.Name = "Times New Roman";

            var rangeHeader8 = oWorkSheet.get_Range("A8", "E8");
            rangeHeader8.Merge(Type.Missing);
            oWorkSheet.Cells[8, 1] = "ĐƠN VỊ: CÔNG TY CỔ PHẦN PHỤC VỤ MẶT ĐẤT SÀI GÒN";
            rangeHeader8.Font.Size = 12;
            rangeHeader8.Font.Bold = true;
            rangeHeader8.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            rangeHeader8.Font.Name = "Times New Roman";

            var rangeHeader8b = oWorkSheet.get_Range("F8", "O8");
            rangeHeader8b.Merge(Type.Missing);
            rangeHeader8b.Value = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
            rangeHeader8b.Font.Size = 12;
            rangeHeader8b.Font.Bold = true;
            rangeHeader8b.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader8b.Font.Name = "Times New Roman";

            var rangeHeader9 = oWorkSheet.get_Range("F9", "O9");
            rangeHeader9.Merge(Type.Missing);
            rangeHeader9.Value = "Độc lập - Tự do - Hạnh phúc";
            rangeHeader9.Font.Size = 12;
            rangeHeader9.Font.Bold = true;
            rangeHeader9.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader9.Font.Name = "Times New Roman";

            var rangeHeader10 = oWorkSheet.get_Range("F10", "O10");
            rangeHeader10.Merge(Type.Missing);
            rangeHeader10.Value = "TP, Hồ Chí Minh, ngày            tháng            năm " + DateTime.Now.Year;
            rangeHeader10.Font.Size = 12;
            rangeHeader10.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader10.Font.Name = "Times New Roman";

            var rangeHeader13 = oWorkSheet.get_Range("A13", "O13");
            rangeHeader13.Merge(Type.Missing);
            rangeHeader13.Value = "DANH SÁCH TRÍCH NGANG CÁN BỘ, NHÂN VIÊN CẤP THẺ KIỂM SOÁT AN NINH";
            rangeHeader13.Font.Size = 12;
            rangeHeader13.Font.Bold = true;
            rangeHeader13.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader13.Font.Name = "Times New Roman";

            var rangeHeader14 = oWorkSheet.get_Range("A14", "O14");
            rangeHeader14.Merge(Type.Missing);
            rangeHeader14.Value = "CẢNG HÀNG KHÔNG, SÂN BAY CÓ GIÁ TRỊ SỬ DỤNG DÀI HẠN";
            rangeHeader14.Font.Size = 12;
            rangeHeader14.Font.Bold = true;
            rangeHeader14.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader14.Font.Name = "Times New Roman";

            var rangeHeader17 = oWorkSheet.get_Range("A17", "O17");
            rangeHeader17.Merge(Type.Missing);
            rangeHeader17.Value = "Kính gửi: Ban Giám đốc Cảng vụ Hàng không Miền Nam";
            rangeHeader17.Font.Size = 12;
            rangeHeader17.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader17.Font.Name = "Times New Roman";

            oWorkSheet.Cells[20, 1] = "STT";
            ((Range) oWorkSheet.Cells[20, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[20, 1]).ColumnWidth = 5;
            ((Range) oWorkSheet.Cells[20, 1]).Font.Bold = true;
            var rNo = oWorkSheet.get_Range("A20", "A21");
            rNo.Merge(Type.Missing);
            rNo.WrapText = true;
            rNo.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rNo.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[20, 2] = "Họ và tên";
            ((Range) oWorkSheet.Cells[20, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[20, 2]).ColumnWidth = 20;
            ((Range) oWorkSheet.Cells[20, 2]).Font.Bold = true;
            var rName = oWorkSheet.get_Range("B20", "B21");
            rName.Merge(Type.Missing);
            rName.WrapText = true;
            rName.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rName.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[20, 3] = "Chức danh";
            ((Range) oWorkSheet.Cells[20, 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[20, 3]).ColumnWidth = 20;
            ((Range) oWorkSheet.Cells[20, 3]).Font.Bold = true;
            var rPositionName = oWorkSheet.get_Range("C20", "C21");
            rPositionName.Merge(Type.Missing);
            rPositionName.WrapText = true;
            rPositionName.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rPositionName.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[20, 4] = "Đơn vị";
            ((Range) oWorkSheet.Cells[20, 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[20, 4]).ColumnWidth = 20;
            ((Range) oWorkSheet.Cells[20, 4]).Font.Bold = true;
            var rDepartment = oWorkSheet.get_Range("D20", "D21");
            rDepartment.Merge(Type.Missing);
            rDepartment.WrapText = true;
            rDepartment.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rDepartment.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[20, 5] = "Phòng/ đội";
            ((Range) oWorkSheet.Cells[20, 5]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range) oWorkSheet.Cells[20, 5]).ColumnWidth = 30;
            ((Range) oWorkSheet.Cells[20, 5]).Font.Bold = true;
            var rDepartmentFull = oWorkSheet.get_Range("E20", "E21");
            rDepartmentFull.Merge(Type.Missing);
            rDepartmentFull.WrapText = true;
            rDepartmentFull.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            rDepartmentFull.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[20, 6] = "Số CMND/ Hộ chiếu";
            ((Range) oWorkSheet.Cells[20, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[20, 6]).ColumnWidth = 15;
            ((Range) oWorkSheet.Cells[20, 6]).Font.Bold = true;
            var rId = oWorkSheet.get_Range("F20", "F21");
            rId.Merge(Type.Missing);
            rId.WrapText = true;
            rId.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rId.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[20, 7] = "Số thẻ đã cấp (nếu có)";
            ((Range) oWorkSheet.Cells[20, 7]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[20, 7]).ColumnWidth = 15;
            ((Range) oWorkSheet.Cells[20, 7]).Font.Bold = true;
            var rOldNumber = oWorkSheet.get_Range("G20", "G21");
            rOldNumber.Merge(Type.Missing);
            rOldNumber.WrapText = true;
            rOldNumber.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rOldNumber.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[20, 8] = "Thời hạn cấp";
            ((Range) oWorkSheet.Cells[20, 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[20, 8]).ColumnWidth = 15;
            ((Range) oWorkSheet.Cells[20, 8]).Font.Bold = true;
            var rPeriod = oWorkSheet.get_Range("H20", "H21");
            rPeriod.Merge(Type.Missing);
            rPeriod.WrapText = true;
            rPeriod.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rPeriod.VerticalAlignment = XlVAlign.xlVAlignCenter;


            oWorkSheet.Cells[21, 9] = "KV 1";
            ((Range) oWorkSheet.Cells[21, 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[21, 9]).ColumnWidth = 5;
            ((Range) oWorkSheet.Cells[21, 9]).Font.Bold = true;
            var rA1 = oWorkSheet.get_Range("I20", "I21");
            rA1.Merge(Type.Missing);
            rA1.WrapText = true;
            rA1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rA1.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[21, 10] = "KV 2";
            ((Range) oWorkSheet.Cells[21, 10]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[21, 10]).ColumnWidth = 5;
            ((Range) oWorkSheet.Cells[21, 10]).Font.Bold = true;
            var rA2 = oWorkSheet.get_Range("J20", "J21");
            rA2.Merge(Type.Missing);
            rA2.WrapText = true;
            rA2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rA2.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[21, 11] = "KV 3";
            ((Range) oWorkSheet.Cells[21, 11]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[21, 11]).ColumnWidth = 5;
            ((Range) oWorkSheet.Cells[21, 11]).Font.Bold = true;
            var rA3 = oWorkSheet.get_Range("K20", "K21");
            rA3.Merge(Type.Missing);
            rA3.WrapText = true;
            rA3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rA3.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[21, 12] = "KV 4";
            ((Range) oWorkSheet.Cells[21, 12]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[21, 12]).ColumnWidth = 5;
            ((Range) oWorkSheet.Cells[21, 12]).Font.Bold = true;
            var rA4 = oWorkSheet.get_Range("L20", "L21");
            rA4.Merge(Type.Missing);
            rA4.WrapText = true;
            rA4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rA4.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[21, 13] = "KV 5";
            ((Range) oWorkSheet.Cells[21, 13]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[21, 13]).ColumnWidth = 5;
            ((Range) oWorkSheet.Cells[21, 13]).Font.Bold = true;
            var rA5 = oWorkSheet.get_Range("M20", "M21");
            rA5.Merge(Type.Missing);
            rA5.WrapText = true;
            rA5.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rA5.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[21, 14] = "KV 6";
            ((Range) oWorkSheet.Cells[21, 14]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[21, 14]).ColumnWidth = 5;
            ((Range) oWorkSheet.Cells[21, 14]).Font.Bold = true;
            var rA6 = oWorkSheet.get_Range("N20", "N21");
            rA6.Merge(Type.Missing);
            rA6.WrapText = true;
            rA6.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rA6.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[20, 15] = "Ghi chú";
            ((Range) oWorkSheet.Cells[20, 15]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[20, 15]).ColumnWidth = 30;
            ((Range) oWorkSheet.Cells[20, 15]).Font.Bold = true;
            var rRemark = oWorkSheet.get_Range("O20", "O21");
            rRemark.Merge(Type.Missing);
            rRemark.WrapText = true;
            rRemark.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rRemark.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[20, 16] = "Mã NV";
            ((Range) oWorkSheet.Cells[20, 16]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[20, 16]).ColumnWidth = 5;
            ((Range) oWorkSheet.Cells[20, 16]).Font.Bold = true;
            var rUserId = oWorkSheet.get_Range("P20", "P21");
            rUserId.Merge(Type.Missing);
            rUserId.WrapText = true;
            rUserId.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rUserId.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[13, 17] = "1 đỏ";
            oWorkSheet.Cells[13, 18] = "2";
            oWorkSheet.Cells[13, 19] = "3";
            oWorkSheet.Cells[13, 20] = "4 đỏ";
            oWorkSheet.Cells[13, 21] = "5";
            oWorkSheet.Cells[13, 22] = "6";

            oWorkSheet.Cells[14, 17] = "1";
            oWorkSheet.Cells[14, 18] = "2A";
            oWorkSheet.Cells[14, 19] = "3A";
            oWorkSheet.Cells[14, 20] = "4";
            oWorkSheet.Cells[14, 21] = "5A";
            oWorkSheet.Cells[14, 22] = "6A";

            oWorkSheet.Cells[15, 17] = "1A";
            oWorkSheet.Cells[15, 18] = "2B";
            oWorkSheet.Cells[15, 19] = "3B";
            oWorkSheet.Cells[15, 21] = "5B";
            oWorkSheet.Cells[15, 22] = "6B";

            oWorkSheet.Cells[16, 17] = "1B";
            oWorkSheet.Cells[16, 18] = "2C";
            oWorkSheet.Cells[16, 19] = "3C";
            oWorkSheet.Cells[16, 21] = "5C";
            oWorkSheet.Cells[16, 22] = "6C";

            oWorkSheet.Cells[17, 17] = "1C";
            oWorkSheet.Cells[17, 21] = "5D";

            oWorkSheet.Cells[18, 17] = "1D";
            oWorkSheet.Cells[18, 21] = "5E";

            oWorkSheet.Cells[19, 17] = "1E";

            var rNote = oWorkSheet.get_Range("P13", "V19");
            rNote.Font.Italic = true;
            rNote.Font.Bold = true;
            rNote.Font.Color = Color.Red;
            rNote.NumberFormat = "@";
            rNote.ColumnWidth = 5;
        }

        public void CreateHeaderByGridViewColumnName(List<string> lstColName, ref _Worksheet oWorkSheet,
            ref int initTitleIndex, string oWorkSheetName)
        {
            var ColNumber = lstColName.Count;

            var rangeHeader1 = oWorkSheet.get_Range("A1", GetExcelColumnName(ColNumber) + "1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "CÔNG TY CỔ PHẦN PHỤC VỤ MẶT ĐẤT SÀI GON";
            rangeHeader1.Font.Size = 15;
            rangeHeader1.Font.Bold = true;
            rangeHeader1.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            rangeHeader1.Font.Name = "Times New Roman";

            var rangeHeader2 = oWorkSheet.get_Range("A3", GetExcelColumnName(ColNumber) + "3");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = oWorkSheetName.ToUpper();
            rangeHeader2.Font.Size = 16;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader2.Font.Name = "Times New Roman";

            oWorkSheet.Cells[initTitleIndex, 1] = lstColName[0];
            for (var i = 0; i < ColNumber; i++)
            {
                var colName = string.Empty;
                try
                {
                    colName =
                        AdministrationBLL.GetAllTranslatedColumn_ByName(lstColName[i])["ColumnNameInVI"].ToString();
                }
                catch
                {
                    colName = lstColName[i];
                }

                if (lstColName[i] == "Resident")
                {
                    oWorkSheet.Cells[initTitleIndex, i + 1] = colName;
                    ((Range) oWorkSheet.Cells[initTitleIndex, i + 1]).ColumnWidth = 150;
                }
                else
                {
                    if (lstColName[i] == "Live")
                    {
                        oWorkSheet.Cells[initTitleIndex, i + 1] = colName;
                        ((Range) oWorkSheet.Cells[initTitleIndex, i + 1]).ColumnWidth = 150;
                    }
                    else
                    {
                        if (lstColName[i] == "WorkedCompany")
                        {
                            oWorkSheet.Cells[initTitleIndex, i + 1] = colName;
                            ((Range) oWorkSheet.Cells[initTitleIndex, i + 1]).ColumnWidth = 200;
                        }
                        else
                        {
                            if (lstColName[i] == "JobDescription")
                            {
                                oWorkSheet.Cells[initTitleIndex, i + 1] = colName;
                                ((Range) oWorkSheet.Cells[initTitleIndex, i + 1]).ColumnWidth = 200;
                            }
                            else
                            {
                                oWorkSheet.Cells[initTitleIndex, i + 1] = colName;
                                ((Range) oWorkSheet.Cells[initTitleIndex, i + 1]).ColumnWidth =
                                    lstColName[i].Trim().Length;
                            }
                        }
                    }
                }
            }

            var Header = oWorkSheet.get_Range("A7", GetExcelColumnName(ColNumber) + "7");
            Header.Font.Size = 11;
            Header.Font.Bold = true;
            Header.Font.Name = "Times New Roman";
            Header.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        public string GetSecurityAreaByFull(string str)
        {
            var strReturn = string.Empty;
            var _index = str.LastIndexOf(';');
            var _str = string.Empty;
            if (_index > 0)
                _str = str.Remove(_index);
            if (!_str.Contains(";"))
                strReturn = Constants.GetNameBySecurityControlCode(_str);
            else
                foreach (var item in str.Split(';'))
                    if (item != string.Empty)
                        strReturn += Constants.GetNameBySecurityControlCode(item) + Environment.NewLine;
            return strReturn;
        }

        public void InsertDataToWorkSheet(RadGridView rgv, ref _Worksheet oWorkSheet, string ExportName)
        {
            if ((rgv.Name == "KSAN") || (oWorkSheet.Name == "Nhân viên") || (oWorkSheet.Name == "Học viên"))
            {
                var RootId = 0;
                var RootIdBefore = 0;
                var initTitleIndex = 21;
                var orderNumber = 1;

                var indexRow = initTitleIndex + 1;
                var lstRoot = new List<int>();

                var listColName = GetGridViewColumnName(rgv);
                listColName.Add("UserId1");
                var LastColumnName = GetExcelColumnName(listColName.Count);

                CreateHeaderByGridViewColumnName_KSANFormat(listColName, ref oWorkSheet, ref initTitleIndex, ExportName);

                if (oWorkSheet.Name == "Nhân viên")
                {
                    var dt = SecurityControlBLL.GetAllForExport_Employee();

                    var _lstIndexRoot = new List<int>();
                    var dr0 = dt.Rows[0];

                    oWorkSheet.Cells[indexRow, 1] = (dr0["RootName"] == DBNull.Value) ||
                                                    (dr0["RootName"].ToString().Length <= 0)
                        ? string.Empty
                        : dr0["RootName"].ToString().ToUpper();
                    _lstIndexRoot.Add(indexRow);

                    indexRow++;

                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                    oWorkSheet.Cells[indexRow, 2] = (dr0["FullName"] == DBNull.Value) || (dr0["FullName"] == null)
                        ? string.Empty
                        : dr0["FullName"].ToString();
                    oWorkSheet.Cells[indexRow, 3] = (dr0["PositionName"] == DBNull.Value) ||
                                                    (dr0["PositionName"] == null)
                        ? string.Empty
                        : dr0["PositionName"].ToString();
                    oWorkSheet.Cells[indexRow, 4] = (dr0["RootName"] == DBNull.Value) || (dr0["RootName"] == null)
                        ? string.Empty
                        : dr0["RootName"].ToString();
                    oWorkSheet.Cells[indexRow, 5] = (dr0["DepartmentFullName"] == DBNull.Value) ||
                                                    (dr0["DepartmentFullName"] == null)
                        ? string.Empty
                        : Utilities.Utilities.GetDepartmentFullName(dr0["DepartmentFullName"].ToString(),
                            Convert.ToInt32(dr0["Level"]));

                    var formatIdCard1 = oWorkSheet.get_Range("F" + indexRow, "F" + indexRow);
                    formatIdCard1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    formatIdCard1.NumberFormat = "@";
                    oWorkSheet.Cells[indexRow, 6] = (dr0["IdCard"] == DBNull.Value) || (dr0["IdCard"] == null)
                        ? string.Empty
                        : dr0["IdCard"].ToString();

                    oWorkSheet.Cells[indexRow, 7] = (dr0["CurrentSCI"] == DBNull.Value) || (dr0["CurrentSCI"] == null)
                        ? string.Empty
                        : dr0["CurrentSCI"].ToString();
                    if (dr0["ContractTypeCode"].ToString() == "HDKX")
                        oWorkSheet.Cells[indexRow, 8] = new DateTime(2018, 12, 31).ToString("dd/MM/yyyy");
                    else
                        oWorkSheet.Cells[indexRow, 8] = (dr0["ContractToDate"] == DBNull.Value) ||
                                                        (dr0["ContractToDate"] == null)
                            ? string.Empty
                            : Convert.ToDateTime(dr0["ContractToDate"]).ToString("dd/MM/yyyy");
                    oWorkSheet.Cells[indexRow, 9] = (dr0["Area1"] == DBNull.Value) || (dr0["Area1"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area1"].ToString());
                    oWorkSheet.Cells[indexRow, 10] = (dr0["Area2"] == DBNull.Value) || (dr0["Area2"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area2"].ToString());
                    oWorkSheet.Cells[indexRow, 11] = (dr0["Area3"] == DBNull.Value) || (dr0["Area3"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area3"].ToString());
                    oWorkSheet.Cells[indexRow, 12] = (dr0["Area4"] == DBNull.Value) || (dr0["Area4"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area4"].ToString());
                    oWorkSheet.Cells[indexRow, 13] = (dr0["Area5"] == DBNull.Value) || (dr0["Area5"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area5"].ToString());
                    oWorkSheet.Cells[indexRow, 14] = (dr0["Area6"] == DBNull.Value) || (dr0["Area6"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area6"].ToString());
                    oWorkSheet.Cells[indexRow, 15] = (dr0["Remark"] == DBNull.Value) || (dr0["Remark"] == null)
                        ? string.Empty
                        : dr0["Remark"].ToString();
                    oWorkSheet.Cells[indexRow, 16] = (dr0["UserId"] == DBNull.Value) || (dr0["UserId"] == null)
                        ? string.Empty
                        : dr0["UserId"].ToString();
                    for (var i = 1; i < dt.Rows.Count; i++)
                    {
                        var dr = dt.Rows[i];
                        var dr_1 = dt.Rows[i - 1];
                        var rootId = (dr["RootId"] == DBNull.Value) || (dr["RootId"] == null)
                            ? 0
                            : int.Parse(dr["RootId"].ToString());
                        var rootId_1 = (dr_1["RootId"] == DBNull.Value) || (dr_1["RootId"] == null)
                            ? 0
                            : int.Parse(dr_1["RootId"].ToString());

                        indexRow++;
                        if ((i < dt.Rows.Count - 1) && (i > 1))
                        {
                            RootId = rootId;
                            RootIdBefore = rootId_1;
                        }

                        if (RootId != RootIdBefore)
                        {
                            oWorkSheet.Cells[indexRow, 1] = (dr["RootName"] == DBNull.Value) || (dr["RootName"] == null)
                                ? string.Empty
                                : dr["RootName"].ToString().ToUpper();
                            _lstIndexRoot.Add(indexRow);


                            indexRow++;
                        }

                        oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                        oWorkSheet.Cells[indexRow, 2] = (dr["FullName"] == DBNull.Value) || (dr["FullName"] == null)
                            ? string.Empty
                            : dr["FullName"].ToString();
                        oWorkSheet.Cells[indexRow, 3] = (dr["PositionName"] == DBNull.Value) ||
                                                        (dr["PositionName"] == null)
                            ? string.Empty
                            : dr["PositionName"].ToString();
                        oWorkSheet.Cells[indexRow, 4] = (dr["RootName"] == DBNull.Value) || (dr["RootName"] == null)
                            ? string.Empty
                            : dr["RootName"].ToString();
                        oWorkSheet.Cells[indexRow, 5] = (dr["DepartmentFullName"] == DBNull.Value) ||
                                                        (dr["DepartmentFullName"] == null)
                            ? string.Empty
                            : Utilities.Utilities.GetDepartmentFullName(dr["DepartmentFullName"].ToString(),
                                Convert.ToInt32(dr["Level"]));

                        var formatIdCard = oWorkSheet.get_Range("F" + indexRow, "F" + indexRow);
                        formatIdCard.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        formatIdCard.NumberFormat = "@";
                        oWorkSheet.Cells[indexRow, 6] = (dr["IdCard"] == DBNull.Value) || (dr["IdCard"] == null)
                            ? string.Empty
                            : dr["IdCard"].ToString();

                        oWorkSheet.Cells[indexRow, 7] = (dr["CurrentSCI"] == DBNull.Value) || (dr["CurrentSCI"] == null)
                            ? string.Empty
                            : dr["CurrentSCI"].ToString();

                        if (dr["ContractTypeCode"].ToString() == "HDKX")
                            oWorkSheet.Cells[indexRow, 8] = new DateTime(2018, 12, 31).ToString("dd/MM/yyyy");
                        else
                            oWorkSheet.Cells[indexRow, 8] = (dr["ContractToDate"] == DBNull.Value) ||
                                                            (dr["ContractToDate"] == null)
                                ? string.Empty
                                : Convert.ToDateTime(dr["ContractToDate"]).ToString("dd/MM/yyyy");
                        oWorkSheet.Cells[indexRow, 9] = (dr["Area1"] == DBNull.Value) || (dr["Area1"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area1"].ToString());
                        oWorkSheet.Cells[indexRow, 10] = (dr["Area2"] == DBNull.Value) || (dr["Area2"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area2"].ToString());
                        oWorkSheet.Cells[indexRow, 11] = (dr["Area3"] == DBNull.Value) || (dr["Area3"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area3"].ToString());
                        oWorkSheet.Cells[indexRow, 12] = (dr["Area4"] == DBNull.Value) || (dr["Area4"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area4"].ToString());
                        oWorkSheet.Cells[indexRow, 13] = (dr["Area5"] == DBNull.Value) || (dr["Area5"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area5"].ToString());
                        oWorkSheet.Cells[indexRow, 14] = (dr["Area6"] == DBNull.Value) || (dr["Area6"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area6"].ToString());
                        oWorkSheet.Cells[indexRow, 15] = (dr["Remark"] == DBNull.Value) || (dr["Remark"] == null)
                            ? string.Empty
                            : dr["Remark"].ToString();
                        oWorkSheet.Cells[indexRow, 16] = (dr["UserId"] == DBNull.Value) || (dr["UserId"] == null)
                            ? string.Empty
                            : dr["UserId"].ToString();

                        backgroundWorker1.ReportProgress(i + 1, string.Empty);
                    }

                    var formatSTT = oWorkSheet.get_Range("A22", "A" + indexRow);
                    formatSTT.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    var formatSCI = oWorkSheet.get_Range("G22", "G" + indexRow);
                    formatSCI.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    formatSCI.NumberFormat = "@";

                    var formatExpired = oWorkSheet.get_Range("H22", "H" + indexRow);
                    formatExpired.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    var rangeAll = oWorkSheet.get_Range("A1", "P" + indexRow);
                    rangeAll.Font.Name = "Times New Roman";
                    rangeAll.Font.Size = 12;
                    rangeAll.VerticalAlignment = XlVAlign.xlVAlignCenter;

                    var rA6 = oWorkSheet.get_Range("I22", "M" + indexRow);
                    rA6.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    rA6.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    foreach (var item in _lstIndexRoot)
                    {
                        var rangeDept = oWorkSheet.get_Range("A" + item, "P" + item);
                        rangeDept.Merge(Type.Missing);
                        rangeDept.Font.Bold = true;
                        rangeDept.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    }

                    var rangeDirectorSign = oWorkSheet.get_Range("H" + (indexRow + 4), "P" + (indexRow + 4));
                    rangeDirectorSign.Merge(Type.Missing);
                    rangeDirectorSign.Font.Bold = true;
                    rangeDirectorSign.Value = "TỔNG GIÁM ĐỐC";
                    rangeDirectorSign.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    rangeDirectorSign.VerticalAlignment = XlVAlign.xlVAlignCenter;

                    var rangeDirectorName = oWorkSheet.get_Range("H" + (indexRow + 9), "P" + (indexRow + 9));
                    rangeDirectorName.Merge(Type.Missing);
                    rangeDirectorName.Font.Bold = true;
                    rangeDirectorName.Value = "NGUYỄN ĐÌNH HÙNG";
                    rangeDirectorName.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    rangeDirectorName.VerticalAlignment = XlVAlign.xlVAlignCenter;

                    rangeAll.WrapText = true;
                }
                else
                {
                    var dt = SecurityControlBLL.GetAllForExport_Candidate();

                    var dr0 = dt.Rows[0];
                    var _lstIndexRoot = new List<int>();


                    oWorkSheet.Cells[indexRow, 1] = (dr0["RootName"] == DBNull.Value) ||
                                                    (dr0["RootName"].ToString().Length <= 0)
                        ? string.Empty
                        : dr0["RootName"].ToString().ToUpper();


                    _lstIndexRoot.Add(indexRow);

                    indexRow++;

                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                    oWorkSheet.Cells[indexRow, 2] = (dr0["FullName"] == DBNull.Value) || (dr0["FullName"] == null)
                        ? string.Empty
                        : dr0["FullName"].ToString();
                    oWorkSheet.Cells[indexRow, 3] = (dr0["PositionName"] == DBNull.Value) ||
                                                    (dr0["PositionName"] == null)
                        ? string.Empty
                        : dr0["PositionName"].ToString();
                    oWorkSheet.Cells[indexRow, 4] = (dr0["RootName"] == DBNull.Value) || (dr0["RootName"] == null)
                        ? string.Empty
                        : dr0["RootName"].ToString();
                    oWorkSheet.Cells[indexRow, 5] = (dr0["DepartmentFullName"] == DBNull.Value) ||
                                                    (dr0["DepartmentFullName"] == null)
                        ? string.Empty
                        : dr0["DepartmentFullName"].ToString();
                    oWorkSheet.Cells[indexRow, 6] = (dr0["IdCard"] == DBNull.Value) || (dr0["IdCard"] == null)
                        ? string.Empty
                        : dr0["IdCard"].ToString();
                    oWorkSheet.Cells[indexRow, 7] = (dr0["CurrentSCI"] == DBNull.Value) || (dr0["CurrentSCI"] == null)
                        ? string.Empty
                        : dr0["CurrentSCI"].ToString();

                    if (dr0["ContractTypeCode"].ToString() == "HDKX")
                        oWorkSheet.Cells[indexRow, 8] = new DateTime(2018, 12, 31).ToString("dd/MM/yyyy");
                    else
                        oWorkSheet.Cells[indexRow, 8] = (dr0["ContractToDate"] == DBNull.Value) ||
                                                        (dr0["ContractToDate"] == null)
                            ? string.Empty
                            : Convert.ToDateTime(dr0["ContractToDate"]).ToString("dd/MM/yyyy");
                    oWorkSheet.Cells[indexRow, 9] = (dr0["Area1"] == DBNull.Value) || (dr0["Area1"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area1"].ToString());
                    oWorkSheet.Cells[indexRow, 10] = (dr0["Area2"] == DBNull.Value) || (dr0["Area2"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area2"].ToString());
                    oWorkSheet.Cells[indexRow, 11] = (dr0["Area3"] == DBNull.Value) || (dr0["Area3"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area3"].ToString());
                    oWorkSheet.Cells[indexRow, 12] = (dr0["Area4"] == DBNull.Value) || (dr0["Area4"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area4"].ToString());
                    oWorkSheet.Cells[indexRow, 13] = (dr0["Area5"] == DBNull.Value) || (dr0["Area5"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area5"].ToString());
                    oWorkSheet.Cells[indexRow, 14] = (dr0["Area6"] == DBNull.Value) || (dr0["Area6"] == null)
                        ? string.Empty
                        : GetSecurityAreaByFull(dr0["Area6"].ToString());
                    oWorkSheet.Cells[indexRow, 15] = (dr0["Remark"] == DBNull.Value) || (dr0["Remark"] == null)
                        ? string.Empty
                        : dr0["Remark"].ToString();
                    oWorkSheet.Cells[indexRow, 16] = (dr0["UserId"] == DBNull.Value) || (dr0["UserId"] == null)
                        ? string.Empty
                        : dr0["UserId"].ToString();
                    for (var i = 1; i < dt.Rows.Count; i++)
                    {
                        var dr = dt.Rows[i];
                        var dr_1 = dt.Rows[i - 1];
                        var rootId = (dr["RootId"] == DBNull.Value) || (dr["RootId"] == null)
                            ? 0
                            : int.Parse(dr["RootId"].ToString());
                        var rootId_1 = (dr_1["RootId"] == DBNull.Value) || (dr_1["RootId"] == null)
                            ? 0
                            : int.Parse(dr_1["RootId"].ToString());

                        indexRow++;
                        if ((i < dt.Rows.Count - 1) && (i > 1))
                        {
                            RootId = rootId;
                            RootIdBefore = rootId_1;
                        }

                        if (RootId != RootIdBefore)
                        {
                            oWorkSheet.Cells[indexRow, 1] = (dr["RootName"] == DBNull.Value) || (dr["RootName"] == null)
                                ? string.Empty
                                : dr["RootName"].ToString().ToUpper();
                            _lstIndexRoot.Add(indexRow);


                            indexRow++;
                        }

                        oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                        oWorkSheet.Cells[indexRow, 2] = (dr["FullName"] == DBNull.Value) || (dr["FullName"] == null)
                            ? string.Empty
                            : dr["FullName"].ToString();
                        oWorkSheet.Cells[indexRow, 3] = (dr["PositionName"] == DBNull.Value) ||
                                                        (dr["PositionName"] == null)
                            ? string.Empty
                            : dr["PositionName"].ToString();
                        oWorkSheet.Cells[indexRow, 4] = (dr["RootName"] == DBNull.Value) || (dr["RootName"] == null)
                            ? string.Empty
                            : dr["RootName"].ToString();
                        oWorkSheet.Cells[indexRow, 5] = (dr["DepartmentFullName"] == DBNull.Value) ||
                                                        (dr["DepartmentFullName"] == null)
                            ? string.Empty
                            : dr["DepartmentFullName"].ToString();
                        oWorkSheet.Cells[indexRow, 6] = (dr["IdCard"] == DBNull.Value) || (dr["IdCard"] == null)
                            ? string.Empty
                            : dr["IdCard"].ToString();
                        oWorkSheet.Cells[indexRow, 7] = (dr["CurrentSCI"] == DBNull.Value) || (dr["CurrentSCI"] == null)
                            ? string.Empty
                            : dr["CurrentSCI"].ToString();

                        if (dr["ContractTypeCode"].ToString() == "HDKX")
                            oWorkSheet.Cells[indexRow, 8] = new DateTime(2018, 12, 31).ToString("dd/MM/yyyy");
                        else
                            oWorkSheet.Cells[indexRow, 8] = (dr["ContractToDate"] == DBNull.Value) ||
                                                            (dr["ContractToDate"] == null)
                                ? string.Empty
                                : Convert.ToDateTime(dr["ContractToDate"]).ToString("dd/MM/yyyy");
                        oWorkSheet.Cells[indexRow, 9] = (dr["Area1"] == DBNull.Value) || (dr["Area1"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area1"].ToString());
                        oWorkSheet.Cells[indexRow, 10] = (dr["Area2"] == DBNull.Value) || (dr["Area2"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area2"].ToString());
                        oWorkSheet.Cells[indexRow, 11] = (dr["Area3"] == DBNull.Value) || (dr["Area3"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area3"].ToString());
                        oWorkSheet.Cells[indexRow, 12] = (dr["Area4"] == DBNull.Value) || (dr["Area4"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area4"].ToString());
                        oWorkSheet.Cells[indexRow, 13] = (dr["Area5"] == DBNull.Value) || (dr["Area5"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area5"].ToString());
                        oWorkSheet.Cells[indexRow, 14] = (dr["Area6"] == DBNull.Value) || (dr["Area6"] == null)
                            ? string.Empty
                            : GetSecurityAreaByFull(dr["Area6"].ToString());
                        oWorkSheet.Cells[indexRow, 15] = (dr["Remark"] == DBNull.Value) || (dr["Remark"] == null)
                            ? string.Empty
                            : dr["Remark"].ToString();
                        oWorkSheet.Cells[indexRow, 16] = (dr["UserId"] == DBNull.Value) || (dr["UserId"] == null)
                            ? string.Empty
                            : dr["UserId"].ToString();

                        backgroundWorker1.ReportProgress(i + 1, string.Empty);
                    }

                    var rangeAll = oWorkSheet.get_Range("A1", "P" + indexRow);
                    rangeAll.Font.Name = "Times New Roman";
                    rangeAll.Font.Size = 12;
                    rangeAll.VerticalAlignment = XlVAlign.xlVAlignCenter;

                    var rA6 = oWorkSheet.get_Range("I22", "M" + indexRow);
                    rA6.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    rA6.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    var formatSTT = oWorkSheet.get_Range("A22", "A" + indexRow);
                    formatSTT.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    var formatSCI = oWorkSheet.get_Range("G22", "G" + indexRow);
                    formatSCI.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    formatSCI.NumberFormat = "@";

                    var formatExpired = oWorkSheet.get_Range("H22", "H" + indexRow);
                    formatExpired.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    foreach (var item in _lstIndexRoot)
                    {
                        var rangeDept = oWorkSheet.get_Range("A" + item, "P" + item);
                        rangeDept.Merge(Type.Missing);
                        rangeDept.Font.Bold = true;
                        rangeDept.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    }

                    var rangeDirectorSign = oWorkSheet.get_Range("H" + (indexRow + 4), "P" + (indexRow + 4));
                    rangeDirectorSign.Merge(Type.Missing);
                    rangeDirectorSign.Font.Bold = true;
                    rangeDirectorSign.Value = "TỔNG GIÁM ĐỐC";
                    rangeDirectorSign.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    rangeDirectorSign.VerticalAlignment = XlVAlign.xlVAlignCenter;

                    var rangeDirectorName = oWorkSheet.get_Range("H" + (indexRow + 9), "P" + (indexRow + 9));
                    rangeDirectorName.Merge(Type.Missing);
                    rangeDirectorName.Font.Bold = true;
                    rangeDirectorName.Value = "NGUYỄN ĐÌNH HÙNG";
                    rangeDirectorName.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    rangeDirectorName.VerticalAlignment = XlVAlign.xlVAlignCenter;

                    var formatIdCard = oWorkSheet.get_Range("F22", "F" + indexRow);
                    formatIdCard.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    formatIdCard.NumberFormat = "@";

                    rangeAll.WrapText = true;
                }
            }


            else
            {
                var RootId = 0;
                var RootIdBefore = 0;
                var initTitleIndex = 7;

                var indexRow = initTitleIndex + 1;
                var lstRoot = new List<int>();

                var listColName = GetGridViewColumnName(rgv);
                listColName.Add("UserId1");
                var LastColumnName = GetExcelColumnName(listColName.Count);
                CreateHeaderByGridViewColumnName(listColName, ref oWorkSheet, ref initTitleIndex, ExportName);

                var rgv0 = rgv.ChildRows[0];
                {
                    var rangeDept = oWorkSheet.get_Range("A" + indexRow, LastColumnName + indexRow);
                    rangeDept.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = (rgv0.Cells["RootName"].Value == DBNull.Value) ||
                                                    (rgv0.Cells["RootName"].Value.ToString().Length <= 0)
                        ? string.Empty
                        : rgv0.Cells["RootName"].Value.ToString().ToUpper();
                    rangeDept.Font.Bold = true;
                    lstRoot.Add(indexRow);

                    indexRow++;
                    for (var i = 0; i < listColName.Count; i++)
                        if (listColName[i] == "UserId")
                        {
                            var rUserId = oWorkSheet.get_Range(GetExcelColumnName(i + 1) + indexRow,
                                GetExcelColumnName(i + 1) + indexRow);
                            rUserId.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            rUserId.NumberFormat = "@";

                            oWorkSheet.Cells[indexRow, i + 1] =
                                StringFormat.GetUserCodeX6(int.Parse(rgv0.Cells[listColName[i]].Value.ToString()));
                        }
                        else
                        {
                            if (listColName[i] == "UserId1")
                            {
                                var rUserId = oWorkSheet.get_Range(GetExcelColumnName(i + 1) + indexRow,
                                    GetExcelColumnName(i + 1) + indexRow);
                                rUserId.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                rUserId.NumberFormat = "@";

                                oWorkSheet.Cells[indexRow, i + 1] = int.Parse(rgv0.Cells["UserId"].Value.ToString());
                            }
                            else
                            {
                                if (listColName[i] == "Area1")
                                {
                                    var rArea1 = oWorkSheet.get_Range(GetExcelColumnName(i + 1) + indexRow,
                                        GetExcelColumnName(i + 1) + indexRow);
                                    rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                    rArea1.NumberFormat = "@";

                                    oWorkSheet.Cells[indexRow, i + 1] = rgv0.Cells[listColName[i]].Value == DBNull.Value
                                        ? string.Empty
                                        : GetSecurityAreaByFull(rgv0.Cells[listColName[i]].Value.ToString());
                                }
                                else
                                {
                                    if (listColName[i] == "Area2")
                                    {
                                        var rArea1 = oWorkSheet.get_Range(GetExcelColumnName(i + 1) + indexRow,
                                            GetExcelColumnName(i + 1) + indexRow);
                                        rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                        rArea1.NumberFormat = "@";

                                        oWorkSheet.Cells[indexRow, i + 1] = rgv0.Cells[listColName[i]].Value ==
                                                                            DBNull.Value
                                            ? string.Empty
                                            : GetSecurityAreaByFull(rgv0.Cells[listColName[i]].Value.ToString());
                                    }
                                    else
                                    {
                                        if (listColName[i] == "Area3")
                                        {
                                            var rArea1 = oWorkSheet.get_Range(GetExcelColumnName(i + 1) + indexRow,
                                                GetExcelColumnName(i + 1) + indexRow);
                                            rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                            rArea1.NumberFormat = "@";

                                            oWorkSheet.Cells[indexRow, i + 1] = rgv0.Cells[listColName[i]].Value ==
                                                                                DBNull.Value
                                                ? string.Empty
                                                : GetSecurityAreaByFull(rgv0.Cells[listColName[i]].Value.ToString());
                                        }
                                        else
                                        {
                                            if (listColName[i] == "Area4")
                                            {
                                                var rArea1 = oWorkSheet.get_Range(GetExcelColumnName(i + 1) + indexRow,
                                                    GetExcelColumnName(i + 1) + indexRow);
                                                rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                rArea1.NumberFormat = "@";

                                                oWorkSheet.Cells[indexRow, i + 1] = rgv0.Cells[listColName[i]].Value ==
                                                                                    DBNull.Value
                                                    ? string.Empty
                                                    : GetSecurityAreaByFull(rgv0.Cells[listColName[i]].Value.ToString());
                                            }
                                            else
                                            {
                                                if (listColName[i] == "Area5")
                                                {
                                                    var rArea1 =
                                                        oWorkSheet.get_Range(GetExcelColumnName(i + 1) + indexRow,
                                                            GetExcelColumnName(i + 1) + indexRow);
                                                    rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                    rArea1.NumberFormat = "@";

                                                    oWorkSheet.Cells[indexRow, i + 1] =
                                                        rgv0.Cells[listColName[i]].Value == DBNull.Value
                                                            ? string.Empty
                                                            : GetSecurityAreaByFull(
                                                                rgv0.Cells[listColName[i]].Value.ToString());
                                                }
                                                else
                                                {
                                                    if (listColName[i] == "Area6")
                                                    {
                                                        var rArea1 =
                                                            oWorkSheet.get_Range(GetExcelColumnName(i + 1) + indexRow,
                                                                GetExcelColumnName(i + 1) + indexRow);
                                                        rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                        rArea1.NumberFormat = "@";

                                                        oWorkSheet.Cells[indexRow, i + 1] =
                                                            rgv0.Cells[listColName[i]].Value == DBNull.Value
                                                                ? string.Empty
                                                                : GetSecurityAreaByFull(
                                                                    rgv0.Cells[listColName[i]].Value.ToString());
                                                    }
                                                    else
                                                    {
                                                        if (listColName[i] == "EmployeeCode")
                                                        {
                                                            var rUserId =
                                                                oWorkSheet.get_Range(
                                                                    GetExcelColumnName(i + 1) + indexRow,
                                                                    GetExcelColumnName(i + 1) + indexRow);
                                                            rUserId.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                            rUserId.NumberFormat = "@";

                                                            var _EmployeeCode = rgv0.Cells[listColName[i]].Value ==
                                                                                DBNull.Value
                                                                ? string.Empty
                                                                : rgv0.Cells[listColName[i]].Value.ToString();
                                                            if (_EmployeeCode != string.Empty)
                                                                oWorkSheet.Cells[indexRow, i + 1] =
                                                                    StringFormat.GetUserCodeX6(
                                                                        int.Parse(
                                                                            rgv0.Cells[listColName[i]].Value.ToString()));
                                                        }
                                                        else
                                                        {
                                                            if (listColName[i] == "FromDate")
                                                            {
                                                                var rFromDate =
                                                                    oWorkSheet.get_Range(
                                                                        GetExcelColumnName(i + 1) + indexRow,
                                                                        GetExcelColumnName(i + 1) + indexRow);
                                                                rFromDate.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                                rFromDate.NumberFormat = "@";

                                                                var _FromDate = (rgv0.Cells[listColName[i]].Value ==
                                                                                 DBNull.Value) ||
                                                                                (Convert.ToDateTime(
                                                                                     rgv0.Cells[listColName[i]].Value) ==
                                                                                 FormatDate.GetSQLDateMinValue)
                                                                    ? FormatDate.GetSQLDateMinValue
                                                                    : Convert.ToDateTime(
                                                                        rgv0.Cells[listColName[i]].Value.ToString());
                                                                oWorkSheet.Cells[indexRow, i + 1] = _FromDate ==
                                                                                                    MinValue
                                                                    ? string.Empty
                                                                    : Convert.ToDateTime(
                                                                            rgv0.Cells[listColName[i]].Value.ToString())
                                                                        .ToString("dd/MM/yyyy");
                                                            }
                                                            else
                                                            {
                                                                if (listColName[i] == "Expired")
                                                                {
                                                                    var rFromDate =
                                                                        oWorkSheet.get_Range(
                                                                            GetExcelColumnName(i + 1) + indexRow,
                                                                            GetExcelColumnName(i + 1) + indexRow);
                                                                    rFromDate.HorizontalAlignment =
                                                                        XlHAlign.xlHAlignCenter;
                                                                    rFromDate.NumberFormat = "@";

                                                                    var _Expired = (rgv0.Cells[listColName[i]].Value ==
                                                                                    DBNull.Value) ||
                                                                                   (Convert.ToDateTime(
                                                                                        rgv0.Cells[listColName[i]].Value) ==
                                                                                    FormatDate.GetSQLDateMinValue)
                                                                        ? FormatDate.GetSQLDateMinValue
                                                                        : Convert.ToDateTime(
                                                                            rgv0.Cells[listColName[i]].Value.ToString());
                                                                    oWorkSheet.Cells[indexRow, i + 1] = _Expired ==
                                                                                                        MinValue
                                                                        ? string.Empty
                                                                        : Convert.ToDateTime(
                                                                                rgv0.Cells[listColName[i]].Value
                                                                                    .ToString())
                                                                            .ToString("dd/MM/yyyy");
                                                                }
                                                                else
                                                                {
                                                                    if (listColName[i] == "ContractFromDate")
                                                                    {
                                                                        var rContractFromDate =
                                                                            oWorkSheet.get_Range(
                                                                                GetExcelColumnName(i + 1) + indexRow,
                                                                                GetExcelColumnName(i + 1) + indexRow);
                                                                        rContractFromDate.HorizontalAlignment =
                                                                            XlHAlign.xlHAlignCenter;
                                                                        rContractFromDate.NumberFormat = "@";

                                                                        var _FromDate =
                                                                            (rgv0.Cells[listColName[i]].Value ==
                                                                             DBNull.Value) ||
                                                                            (Convert.ToDateTime(
                                                                                 rgv0.Cells[listColName[i]].Value) ==
                                                                             FormatDate.GetSQLDateMinValue)
                                                                                ? FormatDate.GetSQLDateMinValue
                                                                                : Convert.ToDateTime(
                                                                                    rgv0.Cells[listColName[i]].Value
                                                                                        .ToString());
                                                                        oWorkSheet.Cells[indexRow, i + 1] = _FromDate ==
                                                                                                            MinValue
                                                                            ? string.Empty
                                                                            : Convert.ToDateTime(
                                                                                rgv0.Cells[listColName[i]].Value
                                                                                    .ToString()).ToString("dd/MM/yyyy");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (listColName[i] == "ContractToDate")
                                                                        {
                                                                            var rContractToDate =
                                                                                oWorkSheet.get_Range(
                                                                                    GetExcelColumnName(i + 1) + indexRow,
                                                                                    GetExcelColumnName(i + 1) + indexRow);
                                                                            rContractToDate.HorizontalAlignment =
                                                                                XlHAlign.xlHAlignCenter;
                                                                            rContractToDate.NumberFormat = "@";

                                                                            var _ToDate =
                                                                                (rgv0.Cells[listColName[i]].Value ==
                                                                                 DBNull.Value) ||
                                                                                (Convert.ToDateTime(
                                                                                     rgv0.Cells[listColName[i]].Value) ==
                                                                                 FormatDate.GetSQLDateMinValue)
                                                                                    ? FormatDate.GetSQLDateMinValue
                                                                                    : Convert.ToDateTime(
                                                                                        rgv0.Cells[listColName[i]].Value
                                                                                            .ToString());
                                                                            oWorkSheet.Cells[indexRow, i + 1] =
                                                                                _ToDate == MinValue
                                                                                    ? "Không xác định"
                                                                                    : Convert.ToDateTime(
                                                                                            rgv0.Cells[listColName[i]]
                                                                                                .Value
                                                                                                .ToString())
                                                                                        .ToString("dd/MM/yyyy");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (listColName[i] == "ToDate")
                                                                            {
                                                                                var rToDate =
                                                                                    oWorkSheet.get_Range(
                                                                                        GetExcelColumnName(i + 1) +
                                                                                        indexRow,
                                                                                        GetExcelColumnName(i + 1) +
                                                                                        indexRow);
                                                                                rToDate.HorizontalAlignment =
                                                                                    XlHAlign.xlHAlignCenter;
                                                                                rToDate.NumberFormat = "@";

                                                                                var _ToDate =
                                                                                    (rgv0.Cells[listColName[i]].Value ==
                                                                                     DBNull.Value) ||
                                                                                    (Convert.ToDateTime(
                                                                                         rgv0.Cells[listColName[i]]
                                                                                             .Value) ==
                                                                                     FormatDate.GetSQLDateMinValue)
                                                                                        ? FormatDate.GetSQLDateMinValue
                                                                                        : Convert.ToDateTime(
                                                                                            rgv0.Cells[listColName[i]]
                                                                                                .Value.ToString());
                                                                                oWorkSheet.Cells[indexRow, i + 1] =
                                                                                    _ToDate == MinValue
                                                                                        ? "Không xác định"
                                                                                        : Convert.ToDateTime(
                                                                                                rgv0.Cells[
                                                                                                        listColName[i]]
                                                                                                    .Value.ToString())
                                                                                            .ToString("dd/MM/yyyy");
                                                                            }
                                                                            else
                                                                            {
                                                                                if (listColName[i] == "ContractNo")
                                                                                {
                                                                                    var rContractNo =
                                                                                        oWorkSheet.get_Range(
                                                                                            GetExcelColumnName(i + 1) +
                                                                                            indexRow,
                                                                                            GetExcelColumnName(i + 1) +
                                                                                            indexRow);
                                                                                    rContractNo.HorizontalAlignment =
                                                                                        XlHAlign.xlHAlignCenter;
                                                                                    rContractNo.NumberFormat = "@";

                                                                                    oWorkSheet.Cells[indexRow, i + 1] =
                                                                                        rgv0.Cells[listColName[i]].Value ==
                                                                                        DBNull.Value
                                                                                            ? string.Empty
                                                                                            : StringFormat.GetUserCodeX6
                                                                                            (Convert.ToInt32(
                                                                                                rgv0.Cells[
                                                                                                        listColName[i]]
                                                                                                    .Value));
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (listColName[i] == "Status")
                                                                                    {
                                                                                        var rContractNo =
                                                                                            oWorkSheet.get_Range(
                                                                                                GetExcelColumnName(i + 1) +
                                                                                                indexRow,
                                                                                                GetExcelColumnName(i + 1) +
                                                                                                indexRow);
                                                                                        rContractNo.HorizontalAlignment
                                                                                            = XlHAlign.xlHAlignCenter;
                                                                                        rContractNo.NumberFormat = "@";

                                                                                        var _Status =
                                                                                            rgv0.Cells[listColName[i]]
                                                                                                .Value == DBNull.Value
                                                                                                ? 9999
                                                                                                : Convert.ToInt32(
                                                                                                    rgv0.Cells[
                                                                                                            listColName[
                                                                                                                i]]
                                                                                                        .Value);


                                                                                        if (_Status == 9999)
                                                                                            oWorkSheet.Cells[
                                                                                                    indexRow, i + 1] =
                                                                                                "Không xác định";
                                                                                        else
                                                                                            oWorkSheet.Cells[
                                                                                                    indexRow, i + 1] =
                                                                                                _Status == 1
                                                                                                    ? "Đang làm việc"
                                                                                                    : "Nghỉ việc";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (listColName[i] ==
                                                                                            "WorkingName")
                                                                                        {
                                                                                            var rWorkingName =
                                                                                                oWorkSheet.get_Range(
                                                                                                    GetExcelColumnName(
                                                                                                        i + 1) +
                                                                                                    indexRow,
                                                                                                    GetExcelColumnName(
                                                                                                        i + 1) +
                                                                                                    indexRow);
                                                                                            rWorkingName
                                                                                                    .HorizontalAlignment
                                                                                                =
                                                                                                XlHAlign.xlHAlignLeft;

                                                                                            oWorkSheet.Cells[
                                                                                                    indexRow, i + 1] =
                                                                                                rgv0.Cells[
                                                                                                        listColName[i]]
                                                                                                    .Value ==
                                                                                                DBNull.Value
                                                                                                    ? string.Empty
                                                                                                    : rgv0.Cells[
                                                                                                            listColName[
                                                                                                                i]]
                                                                                                        .Value.ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (listColName[i] ==
                                                                                                "Overtime")
                                                                                            {
                                                                                                var rOvertime =
                                                                                                    oWorkSheet.get_Range
                                                                                                    (GetExcelColumnName(
                                                                                                         i + 1) +
                                                                                                     indexRow,
                                                                                                        GetExcelColumnName
                                                                                                            (i + 1) +
                                                                                                        indexRow);
                                                                                                rOvertime
                                                                                                        .HorizontalAlignment
                                                                                                    =
                                                                                                    XlHAlign
                                                                                                        .xlHAlignLeft;

                                                                                                oWorkSheet.Cells[
                                                                                                        indexRow, i + 1]
                                                                                                    =
                                                                                                    rgv0.Cells[
                                                                                                            listColName[
                                                                                                                i]]
                                                                                                        .Value ==
                                                                                                    DBNull.Value
                                                                                                        ? string.Empty
                                                                                                        : rgv0.Cells[
                                                                                                                listColName
                                                                                                                [
                                                                                                                    i]]
                                                                                                            .Value
                                                                                                            .ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (listColName[i] ==
                                                                                                    "CreateDate")
                                                                                                {
                                                                                                    var rCreateDate =
                                                                                                        oWorkSheet
                                                                                                            .get_Range(
                                                                                                                GetExcelColumnName
                                                                                                                (i +
                                                                                                                 1) +
                                                                                                                indexRow,
                                                                                                                GetExcelColumnName
                                                                                                                (i +
                                                                                                                 1) +
                                                                                                                indexRow);
                                                                                                    rCreateDate
                                                                                                            .HorizontalAlignment
                                                                                                        =
                                                                                                        XlHAlign
                                                                                                            .xlHAlignCenter;
                                                                                                    rCreateDate
                                                                                                            .NumberFormat
                                                                                                        =
                                                                                                        "@";

                                                                                                    var _CreateDate =
                                                                                                        (rgv0.Cells[
                                                                                                                 listColName
                                                                                                                     [i]
                                                                                                             ]
                                                                                                             .Value ==
                                                                                                         DBNull.Value) ||
                                                                                                        (Convert
                                                                                                             .ToDateTime
                                                                                                             (rgv0.Cells
                                                                                                             [
                                                                                                                 listColName
                                                                                                                     [i]
                                                                                                             ].Value) ==
                                                                                                         FormatDate
                                                                                                             .GetSQLDateMinValue)
                                                                                                            ? FormatDate
                                                                                                                .GetSQLDateMinValue
                                                                                                            : Convert
                                                                                                                .ToDateTime
                                                                                                                (rgv0
                                                                                                                    .Cells
                                                                                                                    [
                                                                                                                        listColName
                                                                                                                        [
                                                                                                                            i
                                                                                                                        ]
                                                                                                                    ]
                                                                                                                    .Value
                                                                                                                    .ToString
                                                                                                                    ());
                                                                                                    oWorkSheet.Cells[
                                                                                                            indexRow,
                                                                                                            i + 1] =
                                                                                                        _CreateDate ==
                                                                                                        MinValue
                                                                                                            ? string
                                                                                                                .Empty
                                                                                                            : Convert
                                                                                                                .ToDateTime
                                                                                                                (rgv0
                                                                                                                    .Cells
                                                                                                                    [
                                                                                                                        listColName
                                                                                                                        [
                                                                                                                            i
                                                                                                                        ]
                                                                                                                    ]
                                                                                                                    .Value
                                                                                                                    .ToString
                                                                                                                    ())
                                                                                                                .ToString
                                                                                                                ("dd/MM/yyyy");
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (
                                                                                                        listColName[i] ==
                                                                                                        "WorkingName")
                                                                                                    {
                                                                                                        var rWorkingName
                                                                                                            =
                                                                                                            oWorkSheet
                                                                                                                .get_Range
                                                                                                                (GetExcelColumnName
                                                                                                                 (i +
                                                                                                                  1) +
                                                                                                                 indexRow,
                                                                                                                    GetExcelColumnName
                                                                                                                    (i +
                                                                                                                     1) +
                                                                                                                    indexRow);
                                                                                                        rWorkingName
                                                                                                                .HorizontalAlignment
                                                                                                            =
                                                                                                            XlHAlign
                                                                                                                .xlHAlignLeft;

                                                                                                        oWorkSheet.Cells
                                                                                                            [
                                                                                                                indexRow,
                                                                                                                i + 1] =
                                                                                                            (rgv0.Cells[
                                                                                                                 listColName
                                                                                                                     [i]
                                                                                                             ].Value ==
                                                                                                             DBNull
                                                                                                                 .Value) ||
                                                                                                            (rgv0.Cells[
                                                                                                                     listColName
                                                                                                                     [
                                                                                                                         i
                                                                                                                     ]
                                                                                                                 ].Value
                                                                                                                 .ToString
                                                                                                                 () ==
                                                                                                             string
                                                                                                                 .Empty)
                                                                                                                ? string
                                                                                                                    .Empty
                                                                                                                : rgv0
                                                                                                                    .Cells
                                                                                                                    [
                                                                                                                        listColName
                                                                                                                        [
                                                                                                                            i
                                                                                                                        ]
                                                                                                                    ]
                                                                                                                    .Value
                                                                                                                    .ToString
                                                                                                                    ();
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (
                                                                                                            listColName[
                                                                                                                i] ==
                                                                                                            "Birthday")
                                                                                                        {
                                                                                                            var
                                                                                                                rBirthday
                                                                                                                    =
                                                                                                                    oWorkSheet
                                                                                                                        .get_Range
                                                                                                                        (GetExcelColumnName
                                                                                                                         (i +
                                                                                                                          1) +
                                                                                                                         indexRow,
                                                                                                                            GetExcelColumnName
                                                                                                                            (i +
                                                                                                                             1) +
                                                                                                                            indexRow);
                                                                                                            rBirthday
                                                                                                                    .HorizontalAlignment
                                                                                                                =
                                                                                                                XlHAlign
                                                                                                                    .xlHAlignCenter;
                                                                                                            rBirthday
                                                                                                                    .NumberFormat
                                                                                                                = "@";

                                                                                                            var
                                                                                                                _Birthday
                                                                                                                    =
                                                                                                                    (rgv0
                                                                                                                         .Cells
                                                                                                                         [
                                                                                                                             listColName
                                                                                                                             [
                                                                                                                                 i
                                                                                                                             ]
                                                                                                                         ]
                                                                                                                         .Value ==
                                                                                                                     DBNull
                                                                                                                         .Value) ||
                                                                                                                    (Convert
                                                                                                                         .ToDateTime
                                                                                                                         (rgv0
                                                                                                                             .Cells
                                                                                                                             [
                                                                                                                                 listColName
                                                                                                                                 [
                                                                                                                                     i
                                                                                                                                 ]
                                                                                                                             ]
                                                                                                                             .Value) ==
                                                                                                                     FormatDate
                                                                                                                         .GetSQLDateMinValue)
                                                                                                                        ? FormatDate
                                                                                                                            .GetSQLDateMinValue
                                                                                                                        : Convert
                                                                                                                            .ToDateTime
                                                                                                                            (rgv0
                                                                                                                                .Cells
                                                                                                                                [
                                                                                                                                    listColName
                                                                                                                                    [
                                                                                                                                        i
                                                                                                                                    ]
                                                                                                                                ]
                                                                                                                                .Value
                                                                                                                                .ToString
                                                                                                                                ());
                                                                                                            oWorkSheet
                                                                                                                    .Cells
                                                                                                                    [
                                                                                                                        indexRow,
                                                                                                                        i +
                                                                                                                        1
                                                                                                                    ] =
                                                                                                                _Birthday ==
                                                                                                                MinValue
                                                                                                                    ? string
                                                                                                                        .Empty
                                                                                                                    : Convert
                                                                                                                        .ToDateTime
                                                                                                                        (rgv0
                                                                                                                            .Cells
                                                                                                                            [
                                                                                                                                listColName
                                                                                                                                [
                                                                                                                                    i
                                                                                                                                ]
                                                                                                                            ]
                                                                                                                            .Value
                                                                                                                            .ToString
                                                                                                                            ())
                                                                                                                        .ToString
                                                                                                                        ("dd/MM/yyyy");
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (
                                                                                                                listColName
                                                                                                                    [i] ==
                                                                                                                "Sex")
                                                                                                            {
                                                                                                                var
                                                                                                                    rWorkingName
                                                                                                                        =
                                                                                                                        oWorkSheet
                                                                                                                            .get_Range
                                                                                                                            (GetExcelColumnName
                                                                                                                             (i +
                                                                                                                              1) +
                                                                                                                             indexRow,
                                                                                                                                GetExcelColumnName
                                                                                                                                (i +
                                                                                                                                 1) +
                                                                                                                                indexRow);
                                                                                                                rWorkingName
                                                                                                                        .HorizontalAlignment
                                                                                                                    =
                                                                                                                    XlHAlign
                                                                                                                        .xlHAlignLeft;

                                                                                                                var _Sex
                                                                                                                    =
                                                                                                                    rgv0
                                                                                                                        .Cells
                                                                                                                        [
                                                                                                                            listColName
                                                                                                                            [
                                                                                                                                i
                                                                                                                            ]
                                                                                                                        ]
                                                                                                                        .Value ==
                                                                                                                    DBNull
                                                                                                                        .Value
                                                                                                                        ? 9999
                                                                                                                        : Convert
                                                                                                                            .ToInt32
                                                                                                                            (rgv0
                                                                                                                                .Cells
                                                                                                                                [
                                                                                                                                    listColName
                                                                                                                                    [
                                                                                                                                        i
                                                                                                                                    ]
                                                                                                                                ]
                                                                                                                                .Value);

                                                                                                                if (
                                                                                                                    _Sex ==
                                                                                                                    9999)
                                                                                                                    oWorkSheet
                                                                                                                            .Cells
                                                                                                                            [
                                                                                                                                indexRow,
                                                                                                                                i +
                                                                                                                                1
                                                                                                                            ]
                                                                                                                        =
                                                                                                                        "Không xác định";
                                                                                                                else
                                                                                                                    oWorkSheet
                                                                                                                            .Cells
                                                                                                                            [
                                                                                                                                indexRow,
                                                                                                                                i +
                                                                                                                                1
                                                                                                                            ]
                                                                                                                        =
                                                                                                                        Convert
                                                                                                                            .ToInt32
                                                                                                                            (rgv0
                                                                                                                                .Cells
                                                                                                                                [
                                                                                                                                    listColName
                                                                                                                                    [
                                                                                                                                        i
                                                                                                                                    ]
                                                                                                                                ]
                                                                                                                                .Value) ==
                                                                                                                        1
                                                                                                                            ? "Nam"
                                                                                                                            : "Nữ";
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (
                                                                                                                    listColName
                                                                                                                    [
                                                                                                                        i
                                                                                                                    ] ==
                                                                                                                    "JoinDate")
                                                                                                                {
                                                                                                                    var
                                                                                                                        rJoinDate
                                                                                                                            =
                                                                                                                            oWorkSheet
                                                                                                                                .get_Range
                                                                                                                                (GetExcelColumnName
                                                                                                                                 (i +
                                                                                                                                  1) +
                                                                                                                                 indexRow,
                                                                                                                                    GetExcelColumnName
                                                                                                                                    (i +
                                                                                                                                     1) +
                                                                                                                                    indexRow);
                                                                                                                    rJoinDate
                                                                                                                            .HorizontalAlignment
                                                                                                                        =
                                                                                                                        XlHAlign
                                                                                                                            .xlHAlignCenter;
                                                                                                                    rJoinDate
                                                                                                                            .NumberFormat
                                                                                                                        =
                                                                                                                        "@";

                                                                                                                    var
                                                                                                                        _JoinDate
                                                                                                                            =
                                                                                                                            (rgv0
                                                                                                                                 .Cells
                                                                                                                                 [
                                                                                                                                     listColName
                                                                                                                                     [
                                                                                                                                         i
                                                                                                                                     ]
                                                                                                                                 ]
                                                                                                                                 .Value ==
                                                                                                                             DBNull
                                                                                                                                 .Value) ||
                                                                                                                            (Convert
                                                                                                                                 .ToDateTime
                                                                                                                                 (rgv0
                                                                                                                                     .Cells
                                                                                                                                     [
                                                                                                                                         listColName
                                                                                                                                         [
                                                                                                                                             i
                                                                                                                                         ]
                                                                                                                                     ]
                                                                                                                                     .Value) ==
                                                                                                                             FormatDate
                                                                                                                                 .GetSQLDateMinValue)
                                                                                                                                ? FormatDate
                                                                                                                                    .GetSQLDateMinValue
                                                                                                                                : Convert
                                                                                                                                    .ToDateTime
                                                                                                                                    (rgv0
                                                                                                                                        .Cells
                                                                                                                                        [
                                                                                                                                            listColName
                                                                                                                                            [
                                                                                                                                                i
                                                                                                                                            ]
                                                                                                                                        ]
                                                                                                                                        .Value
                                                                                                                                        .ToString
                                                                                                                                        ());
                                                                                                                    oWorkSheet
                                                                                                                            .Cells
                                                                                                                            [
                                                                                                                                indexRow,
                                                                                                                                i +
                                                                                                                                1
                                                                                                                            ]
                                                                                                                        =
                                                                                                                        _JoinDate ==
                                                                                                                        MinValue
                                                                                                                            ? string
                                                                                                                                .Empty
                                                                                                                            : Convert
                                                                                                                                .ToDateTime
                                                                                                                                (rgv0
                                                                                                                                    .Cells
                                                                                                                                    [
                                                                                                                                        listColName
                                                                                                                                        [
                                                                                                                                            i
                                                                                                                                        ]
                                                                                                                                    ]
                                                                                                                                    .Value
                                                                                                                                    .ToString
                                                                                                                                    ())
                                                                                                                                .ToString
                                                                                                                                ("dd/MM/yyyy");
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (
                                                                                                                        listColName
                                                                                                                        [
                                                                                                                            i
                                                                                                                        ] ==
                                                                                                                        "DateOfIssue")
                                                                                                                    {
                                                                                                                        var
                                                                                                                            rDateOfIssue
                                                                                                                                =
                                                                                                                                oWorkSheet
                                                                                                                                    .get_Range
                                                                                                                                    (GetExcelColumnName
                                                                                                                                     (i +
                                                                                                                                      1) +
                                                                                                                                     indexRow,
                                                                                                                                        GetExcelColumnName
                                                                                                                                        (i +
                                                                                                                                         1) +
                                                                                                                                        indexRow);
                                                                                                                        rDateOfIssue
                                                                                                                                .HorizontalAlignment
                                                                                                                            =
                                                                                                                            XlHAlign
                                                                                                                                .xlHAlignCenter;
                                                                                                                        rDateOfIssue
                                                                                                                                .NumberFormat
                                                                                                                            =
                                                                                                                            "@";

                                                                                                                        var
                                                                                                                            _DateOfIssue
                                                                                                                                =
                                                                                                                                (rgv0
                                                                                                                                     .Cells
                                                                                                                                     [
                                                                                                                                         listColName
                                                                                                                                         [
                                                                                                                                             i
                                                                                                                                         ]
                                                                                                                                     ]
                                                                                                                                     .Value ==
                                                                                                                                 DBNull
                                                                                                                                     .Value) ||
                                                                                                                                (Convert
                                                                                                                                     .ToDateTime
                                                                                                                                     (rgv0
                                                                                                                                         .Cells
                                                                                                                                         [
                                                                                                                                             listColName
                                                                                                                                             [
                                                                                                                                                 i
                                                                                                                                             ]
                                                                                                                                         ]
                                                                                                                                         .Value) ==
                                                                                                                                 FormatDate
                                                                                                                                     .GetSQLDateMinValue)
                                                                                                                                    ? FormatDate
                                                                                                                                        .GetSQLDateMinValue
                                                                                                                                    : Convert
                                                                                                                                        .ToDateTime
                                                                                                                                        (rgv0
                                                                                                                                            .Cells
                                                                                                                                            [
                                                                                                                                                listColName
                                                                                                                                                [
                                                                                                                                                    i
                                                                                                                                                ]
                                                                                                                                            ]
                                                                                                                                            .Value
                                                                                                                                            .ToString
                                                                                                                                            ());
                                                                                                                        oWorkSheet
                                                                                                                                .Cells
                                                                                                                                [
                                                                                                                                    indexRow,
                                                                                                                                    i +
                                                                                                                                    1
                                                                                                                                ]
                                                                                                                            =
                                                                                                                            _DateOfIssue ==
                                                                                                                            MinValue
                                                                                                                                ? string
                                                                                                                                    .Empty
                                                                                                                                : Convert
                                                                                                                                    .ToDateTime
                                                                                                                                    (rgv0
                                                                                                                                        .Cells
                                                                                                                                        [
                                                                                                                                            listColName
                                                                                                                                            [
                                                                                                                                                i
                                                                                                                                            ]
                                                                                                                                        ]
                                                                                                                                        .Value
                                                                                                                                        .ToString
                                                                                                                                        ())
                                                                                                                                    .ToString
                                                                                                                                    ("dd/MM/yyyy");
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if
                                                                                                                        (
                                                                                                                            listColName
                                                                                                                            [
                                                                                                                                i
                                                                                                                            ] ==
                                                                                                                            "JoinCompanyDate")
                                                                                                                        {
                                                                                                                            var
                                                                                                                                rJoinCompanyDate
                                                                                                                                    =
                                                                                                                                    oWorkSheet
                                                                                                                                        .get_Range
                                                                                                                                        (GetExcelColumnName
                                                                                                                                         (i +
                                                                                                                                          1) +
                                                                                                                                         indexRow,
                                                                                                                                            GetExcelColumnName
                                                                                                                                            (i +
                                                                                                                                             1) +
                                                                                                                                            indexRow);
                                                                                                                            rJoinCompanyDate
                                                                                                                                    .HorizontalAlignment
                                                                                                                                =
                                                                                                                                XlHAlign
                                                                                                                                    .xlHAlignCenter;
                                                                                                                            rJoinCompanyDate
                                                                                                                                    .NumberFormat
                                                                                                                                =
                                                                                                                                "@";

                                                                                                                            var
                                                                                                                                _JoinCompanyDate
                                                                                                                                    =
                                                                                                                                    (rgv0
                                                                                                                                         .Cells
                                                                                                                                         [
                                                                                                                                             listColName
                                                                                                                                             [
                                                                                                                                                 i
                                                                                                                                             ]
                                                                                                                                         ]
                                                                                                                                         .Value ==
                                                                                                                                     DBNull
                                                                                                                                         .Value) ||
                                                                                                                                    (Convert
                                                                                                                                         .ToDateTime
                                                                                                                                         (rgv0
                                                                                                                                             .Cells
                                                                                                                                             [
                                                                                                                                                 listColName
                                                                                                                                                 [
                                                                                                                                                     i
                                                                                                                                                 ]
                                                                                                                                             ]
                                                                                                                                             .Value) ==
                                                                                                                                     FormatDate
                                                                                                                                         .GetSQLDateMinValue)
                                                                                                                                        ? FormatDate
                                                                                                                                            .GetSQLDateMinValue
                                                                                                                                        : Convert
                                                                                                                                            .ToDateTime
                                                                                                                                            (rgv0
                                                                                                                                                .Cells
                                                                                                                                                [
                                                                                                                                                    listColName
                                                                                                                                                    [
                                                                                                                                                        i
                                                                                                                                                    ]
                                                                                                                                                ]
                                                                                                                                                .Value
                                                                                                                                                .ToString
                                                                                                                                                ());
                                                                                                                            oWorkSheet
                                                                                                                                    .Cells
                                                                                                                                    [
                                                                                                                                        indexRow,
                                                                                                                                        i +
                                                                                                                                        1
                                                                                                                                    ]
                                                                                                                                =
                                                                                                                                _JoinCompanyDate ==
                                                                                                                                MinValue
                                                                                                                                    ? string
                                                                                                                                        .Empty
                                                                                                                                    : Convert
                                                                                                                                        .ToDateTime
                                                                                                                                        (rgv0
                                                                                                                                            .Cells
                                                                                                                                            [
                                                                                                                                                listColName
                                                                                                                                                [
                                                                                                                                                    i
                                                                                                                                                ]
                                                                                                                                            ]
                                                                                                                                            .Value
                                                                                                                                            .ToString
                                                                                                                                            ())
                                                                                                                                        .ToString
                                                                                                                                        ("dd/MM/yyyy");
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if
                                                                                                                            (
                                                                                                                                listColName
                                                                                                                                [
                                                                                                                                    i
                                                                                                                                ] ==
                                                                                                                                "HighestLevelNameValue")
                                                                                                                            {
                                                                                                                                var
                                                                                                                                    rJoinCompanyDate
                                                                                                                                        =
                                                                                                                                        oWorkSheet
                                                                                                                                            .get_Range
                                                                                                                                            (GetExcelColumnName
                                                                                                                                             (i +
                                                                                                                                              1) +
                                                                                                                                             indexRow,
                                                                                                                                                GetExcelColumnName
                                                                                                                                                (i +
                                                                                                                                                 1) +
                                                                                                                                                indexRow);
                                                                                                                                rJoinCompanyDate
                                                                                                                                        .HorizontalAlignment
                                                                                                                                    =
                                                                                                                                    XlHAlign
                                                                                                                                        .xlHAlignLeft;
                                                                                                                                rJoinCompanyDate
                                                                                                                                        .NumberFormat
                                                                                                                                    =
                                                                                                                                    "@";

                                                                                                                                oWorkSheet
                                                                                                                                        .Cells
                                                                                                                                        [
                                                                                                                                            indexRow,
                                                                                                                                            i +
                                                                                                                                            1
                                                                                                                                        ]
                                                                                                                                    =
                                                                                                                                    rgv0
                                                                                                                                        .Cells
                                                                                                                                        [
                                                                                                                                            listColName
                                                                                                                                            [
                                                                                                                                                i
                                                                                                                                            ]
                                                                                                                                        ]
                                                                                                                                        .Value ==
                                                                                                                                    DBNull
                                                                                                                                        .Value
                                                                                                                                        ? string
                                                                                                                                            .Empty
                                                                                                                                        : rgv0
                                                                                                                                            .Cells
                                                                                                                                            [
                                                                                                                                                listColName
                                                                                                                                                [
                                                                                                                                                    i
                                                                                                                                                ]
                                                                                                                                            ]
                                                                                                                                            .Value
                                                                                                                                            .ToString
                                                                                                                                            ();
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if
                                                                                                                                (
                                                                                                                                    listColName
                                                                                                                                    [
                                                                                                                                        i
                                                                                                                                    ] ==
                                                                                                                                    "SocialInsuranceNo")
                                                                                                                                {
                                                                                                                                    var
                                                                                                                                        rWorkingName
                                                                                                                                            =
                                                                                                                                            oWorkSheet
                                                                                                                                                .get_Range
                                                                                                                                                (GetExcelColumnName
                                                                                                                                                 (i +
                                                                                                                                                  1) +
                                                                                                                                                 indexRow,
                                                                                                                                                    GetExcelColumnName
                                                                                                                                                    (i +
                                                                                                                                                     1) +
                                                                                                                                                    indexRow);
                                                                                                                                    rWorkingName
                                                                                                                                            .HorizontalAlignment
                                                                                                                                        =
                                                                                                                                        XlHAlign
                                                                                                                                            .xlHAlignCenter;
                                                                                                                                    rWorkingName
                                                                                                                                            .NumberFormat
                                                                                                                                        =
                                                                                                                                        "@";

                                                                                                                                    oWorkSheet
                                                                                                                                            .Cells
                                                                                                                                            [
                                                                                                                                                indexRow,
                                                                                                                                                i +
                                                                                                                                                1
                                                                                                                                            ]
                                                                                                                                        =
                                                                                                                                        rgv0
                                                                                                                                            .Cells
                                                                                                                                            [
                                                                                                                                                listColName
                                                                                                                                                [
                                                                                                                                                    i
                                                                                                                                                ]
                                                                                                                                            ]
                                                                                                                                            .Value ==
                                                                                                                                        DBNull
                                                                                                                                            .Value
                                                                                                                                            ? string
                                                                                                                                                .Empty
                                                                                                                                            : rgv0
                                                                                                                                                .Cells
                                                                                                                                                [
                                                                                                                                                    listColName
                                                                                                                                                    [
                                                                                                                                                        i
                                                                                                                                                    ]
                                                                                                                                                ]
                                                                                                                                                .Value
                                                                                                                                                .ToString
                                                                                                                                                ();
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if
                                                                                                                                    (
                                                                                                                                        listColName
                                                                                                                                        [
                                                                                                                                            i
                                                                                                                                        ] ==
                                                                                                                                        "HealthInsuranceNo")
                                                                                                                                    {
                                                                                                                                        var
                                                                                                                                            rWorkingName
                                                                                                                                                =
                                                                                                                                                oWorkSheet
                                                                                                                                                    .get_Range
                                                                                                                                                    (GetExcelColumnName
                                                                                                                                                     (i +
                                                                                                                                                      1) +
                                                                                                                                                     indexRow,
                                                                                                                                                        GetExcelColumnName
                                                                                                                                                        (i +
                                                                                                                                                         1) +
                                                                                                                                                        indexRow);
                                                                                                                                        rWorkingName
                                                                                                                                                .HorizontalAlignment
                                                                                                                                            =
                                                                                                                                            XlHAlign
                                                                                                                                                .xlHAlignCenter;
                                                                                                                                        rWorkingName
                                                                                                                                                .NumberFormat
                                                                                                                                            =
                                                                                                                                            "@";

                                                                                                                                        oWorkSheet
                                                                                                                                                .Cells
                                                                                                                                                [
                                                                                                                                                    indexRow,
                                                                                                                                                    i +
                                                                                                                                                    1
                                                                                                                                                ]
                                                                                                                                            =
                                                                                                                                            rgv0
                                                                                                                                                .Cells
                                                                                                                                                [
                                                                                                                                                    listColName
                                                                                                                                                    [
                                                                                                                                                        i
                                                                                                                                                    ]
                                                                                                                                                ]
                                                                                                                                                .Value ==
                                                                                                                                            DBNull
                                                                                                                                                .Value
                                                                                                                                                ? string
                                                                                                                                                    .Empty
                                                                                                                                                : rgv0
                                                                                                                                                    .Cells
                                                                                                                                                    [
                                                                                                                                                        listColName
                                                                                                                                                        [
                                                                                                                                                            i
                                                                                                                                                        ]
                                                                                                                                                    ]
                                                                                                                                                    .Value
                                                                                                                                                    .ToString
                                                                                                                                                    ();
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if
                                                                                                                                        (
                                                                                                                                            listColName
                                                                                                                                            [
                                                                                                                                                i
                                                                                                                                            ] ==
                                                                                                                                            "TaxCode")
                                                                                                                                        {
                                                                                                                                            var
                                                                                                                                                rWorkingName
                                                                                                                                                    =
                                                                                                                                                    oWorkSheet
                                                                                                                                                        .get_Range
                                                                                                                                                        (GetExcelColumnName
                                                                                                                                                         (i +
                                                                                                                                                          1) +
                                                                                                                                                         indexRow,
                                                                                                                                                            GetExcelColumnName
                                                                                                                                                            (i +
                                                                                                                                                             1) +
                                                                                                                                                            indexRow);
                                                                                                                                            rWorkingName
                                                                                                                                                    .HorizontalAlignment
                                                                                                                                                =
                                                                                                                                                XlHAlign
                                                                                                                                                    .xlHAlignCenter;
                                                                                                                                            rWorkingName
                                                                                                                                                    .NumberFormat
                                                                                                                                                =
                                                                                                                                                "@";

                                                                                                                                            oWorkSheet
                                                                                                                                                    .Cells
                                                                                                                                                    [
                                                                                                                                                        indexRow,
                                                                                                                                                        i +
                                                                                                                                                        1
                                                                                                                                                    ]
                                                                                                                                                =
                                                                                                                                                rgv0
                                                                                                                                                    .Cells
                                                                                                                                                    [
                                                                                                                                                        listColName
                                                                                                                                                        [
                                                                                                                                                            i
                                                                                                                                                        ]
                                                                                                                                                    ]
                                                                                                                                                    .Value ==
                                                                                                                                                DBNull
                                                                                                                                                    .Value
                                                                                                                                                    ? string
                                                                                                                                                        .Empty
                                                                                                                                                    : rgv0
                                                                                                                                                        .Cells
                                                                                                                                                        [
                                                                                                                                                            listColName
                                                                                                                                                            [
                                                                                                                                                                i
                                                                                                                                                            ]
                                                                                                                                                        ]
                                                                                                                                                        .Value
                                                                                                                                                        .ToString
                                                                                                                                                        ();
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if
                                                                                                                                            (
                                                                                                                                                listColName
                                                                                                                                                [
                                                                                                                                                    i
                                                                                                                                                ] ==
                                                                                                                                                "LeaveDate")
                                                                                                                                            {
                                                                                                                                                var
                                                                                                                                                    rLeaveDate
                                                                                                                                                        =
                                                                                                                                                        oWorkSheet
                                                                                                                                                            .get_Range
                                                                                                                                                            (GetExcelColumnName
                                                                                                                                                             (i +
                                                                                                                                                              1) +
                                                                                                                                                             indexRow,
                                                                                                                                                                GetExcelColumnName
                                                                                                                                                                (i +
                                                                                                                                                                 1) +
                                                                                                                                                                indexRow);
                                                                                                                                                rLeaveDate
                                                                                                                                                        .HorizontalAlignment
                                                                                                                                                    =
                                                                                                                                                    XlHAlign
                                                                                                                                                        .xlHAlignCenter;
                                                                                                                                                rLeaveDate
                                                                                                                                                        .NumberFormat
                                                                                                                                                    =
                                                                                                                                                    "@";

                                                                                                                                                var
                                                                                                                                                    _LeaveDate
                                                                                                                                                        =
                                                                                                                                                        (rgv0
                                                                                                                                                             .Cells
                                                                                                                                                             [
                                                                                                                                                                 listColName
                                                                                                                                                                 [
                                                                                                                                                                     i
                                                                                                                                                                 ]
                                                                                                                                                             ]
                                                                                                                                                             .Value ==
                                                                                                                                                         DBNull
                                                                                                                                                             .Value) ||
                                                                                                                                                        (Convert
                                                                                                                                                             .ToDateTime
                                                                                                                                                             (rgv0
                                                                                                                                                                 .Cells
                                                                                                                                                                 [
                                                                                                                                                                     listColName
                                                                                                                                                                     [
                                                                                                                                                                         i
                                                                                                                                                                     ]
                                                                                                                                                                 ]
                                                                                                                                                                 .Value) ==
                                                                                                                                                         FormatDate
                                                                                                                                                             .GetSQLDateMinValue)
                                                                                                                                                            ? FormatDate
                                                                                                                                                                .GetSQLDateMinValue
                                                                                                                                                            : Convert
                                                                                                                                                                .ToDateTime
                                                                                                                                                                (rgv0
                                                                                                                                                                    .Cells
                                                                                                                                                                    [
                                                                                                                                                                        listColName
                                                                                                                                                                        [
                                                                                                                                                                            i
                                                                                                                                                                        ]
                                                                                                                                                                    ]
                                                                                                                                                                    .Value
                                                                                                                                                                    .ToString
                                                                                                                                                                    ());
                                                                                                                                                oWorkSheet
                                                                                                                                                        .Cells
                                                                                                                                                        [
                                                                                                                                                            indexRow,
                                                                                                                                                            i +
                                                                                                                                                            1
                                                                                                                                                        ]
                                                                                                                                                    =
                                                                                                                                                    _LeaveDate ==
                                                                                                                                                    MinValue
                                                                                                                                                        ? string
                                                                                                                                                            .Empty
                                                                                                                                                        : Convert
                                                                                                                                                            .ToDateTime
                                                                                                                                                            (rgv0
                                                                                                                                                                .Cells
                                                                                                                                                                [
                                                                                                                                                                    listColName
                                                                                                                                                                    [
                                                                                                                                                                        i
                                                                                                                                                                    ]
                                                                                                                                                                ]
                                                                                                                                                                .Value
                                                                                                                                                                .ToString
                                                                                                                                                                ())
                                                                                                                                                            .ToString
                                                                                                                                                            ("dd/MM/yyyy");
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                if
                                                                                                                                                (
                                                                                                                                                    listColName
                                                                                                                                                    [
                                                                                                                                                        i
                                                                                                                                                    ] ==
                                                                                                                                                    "IdCard")
                                                                                                                                                {
                                                                                                                                                    var
                                                                                                                                                        rIdCard
                                                                                                                                                            =
                                                                                                                                                            oWorkSheet
                                                                                                                                                                .get_Range
                                                                                                                                                                (GetExcelColumnName
                                                                                                                                                                 (i +
                                                                                                                                                                  1) +
                                                                                                                                                                 indexRow,
                                                                                                                                                                    GetExcelColumnName
                                                                                                                                                                    (i +
                                                                                                                                                                     1) +
                                                                                                                                                                    indexRow);
                                                                                                                                                    rIdCard
                                                                                                                                                            .NumberFormat
                                                                                                                                                        =
                                                                                                                                                        "@";
                                                                                                                                                    rIdCard
                                                                                                                                                            .HorizontalAlignment
                                                                                                                                                        =
                                                                                                                                                        XlHAlign
                                                                                                                                                            .xlHAlignCenter;

                                                                                                                                                    oWorkSheet
                                                                                                                                                            .Cells
                                                                                                                                                            [
                                                                                                                                                                indexRow,
                                                                                                                                                                i +
                                                                                                                                                                1
                                                                                                                                                            ]
                                                                                                                                                        =
                                                                                                                                                        rgv0
                                                                                                                                                            .Cells
                                                                                                                                                            [
                                                                                                                                                                listColName
                                                                                                                                                                [
                                                                                                                                                                    i
                                                                                                                                                                ]
                                                                                                                                                            ]
                                                                                                                                                            .Value ==
                                                                                                                                                        DBNull
                                                                                                                                                            .Value
                                                                                                                                                            ? string
                                                                                                                                                                .Empty
                                                                                                                                                            : rgv0
                                                                                                                                                                .Cells
                                                                                                                                                                [
                                                                                                                                                                    listColName
                                                                                                                                                                    [
                                                                                                                                                                        i
                                                                                                                                                                    ]
                                                                                                                                                                ]
                                                                                                                                                                .Value
                                                                                                                                                                .ToString
                                                                                                                                                                ();
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    if
                                                                                                                                                    (
                                                                                                                                                        listColName
                                                                                                                                                        [
                                                                                                                                                            i
                                                                                                                                                        ] ==
                                                                                                                                                        "DepartmentFullName")
                                                                                                                                                    {
                                                                                                                                                        var
                                                                                                                                                            dr
                                                                                                                                                                =
                                                                                                                                                                DepartmentEmployeeBLL
                                                                                                                                                                    .GetDRByDeptId
                                                                                                                                                                    (Convert
                                                                                                                                                                        .ToInt32
                                                                                                                                                                        (rgv0
                                                                                                                                                                            .Cells
                                                                                                                                                                            [
                                                                                                                                                                                "DepartmentId"
                                                                                                                                                                            ]
                                                                                                                                                                            .Value));
                                                                                                                                                        if
                                                                                                                                                        (
                                                                                                                                                            dr !=
                                                                                                                                                            null)
                                                                                                                                                        {
                                                                                                                                                            var
                                                                                                                                                                _Level
                                                                                                                                                                    =
                                                                                                                                                                    dr
                                                                                                                                                                    [
                                                                                                                                                                        "Level"
                                                                                                                                                                    ] ==
                                                                                                                                                                    DBNull
                                                                                                                                                                        .Value
                                                                                                                                                                        ? 9999
                                                                                                                                                                        : Convert
                                                                                                                                                                            .ToInt32
                                                                                                                                                                            (dr
                                                                                                                                                                            [
                                                                                                                                                                                "Level"
                                                                                                                                                                            ]);

                                                                                                                                                            if
                                                                                                                                                            (
                                                                                                                                                                _Level !=
                                                                                                                                                                9999)
                                                                                                                                                                oWorkSheet
                                                                                                                                                                        .Cells
                                                                                                                                                                        [
                                                                                                                                                                            indexRow,
                                                                                                                                                                            i +
                                                                                                                                                                            1
                                                                                                                                                                        ]
                                                                                                                                                                    =
                                                                                                                                                                    rgv0
                                                                                                                                                                        .Cells
                                                                                                                                                                        [
                                                                                                                                                                            listColName
                                                                                                                                                                            [
                                                                                                                                                                                i
                                                                                                                                                                            ]
                                                                                                                                                                        ]
                                                                                                                                                                        .Value ==
                                                                                                                                                                    DBNull
                                                                                                                                                                        .Value
                                                                                                                                                                        ? string
                                                                                                                                                                            .Empty
                                                                                                                                                                        : Utilities
                                                                                                                                                                            .Utilities
                                                                                                                                                                            .GetDepartmentFullName
                                                                                                                                                                            (
                                                                                                                                                                                rgv0
                                                                                                                                                                                    .Cells
                                                                                                                                                                                    [
                                                                                                                                                                                        listColName
                                                                                                                                                                                        [
                                                                                                                                                                                            i
                                                                                                                                                                                        ]
                                                                                                                                                                                    ]
                                                                                                                                                                                    .Value
                                                                                                                                                                                    .ToString
                                                                                                                                                                                    (),
                                                                                                                                                                                _Level);
                                                                                                                                                            else
                                                                                                                                                                oWorkSheet
                                                                                                                                                                        .Cells
                                                                                                                                                                        [
                                                                                                                                                                            indexRow,
                                                                                                                                                                            i +
                                                                                                                                                                            1
                                                                                                                                                                        ]
                                                                                                                                                                    =
                                                                                                                                                                    string
                                                                                                                                                                        .Empty;
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if
                                                                                                                                                        (
                                                                                                                                                            listColName
                                                                                                                                                            [
                                                                                                                                                                i
                                                                                                                                                            ] ==
                                                                                                                                                            "Office")
                                                                                                                                                        {
                                                                                                                                                            var
                                                                                                                                                                _Office
                                                                                                                                                                    =
                                                                                                                                                                    rgv0
                                                                                                                                                                        .Cells
                                                                                                                                                                        [
                                                                                                                                                                            listColName
                                                                                                                                                                            [
                                                                                                                                                                                i
                                                                                                                                                                            ]
                                                                                                                                                                        ]
                                                                                                                                                                        .Value ==
                                                                                                                                                                    DBNull
                                                                                                                                                                        .Value
                                                                                                                                                                        ? "theo Quyết định số /QĐ-SAGS ngày "
                                                                                                                                                                        : rgv0
                                                                                                                                                                            .Cells
                                                                                                                                                                            [
                                                                                                                                                                                listColName
                                                                                                                                                                                [
                                                                                                                                                                                    i
                                                                                                                                                                                ]
                                                                                                                                                                            ]
                                                                                                                                                                            .Value
                                                                                                                                                                            .ToString
                                                                                                                                                                            ()
                                                                                                                                                                            .Trim
                                                                                                                                                                            ();
                                                                                                                                                            var
                                                                                                                                                                _RawOffice
                                                                                                                                                                    =
                                                                                                                                                                    "theo Quyết định số /QĐ-SAGS ngày "
                                                                                                                                                                        .Trim
                                                                                                                                                                        ();
                                                                                                                                                            oWorkSheet
                                                                                                                                                                    .Cells
                                                                                                                                                                    [
                                                                                                                                                                        indexRow,
                                                                                                                                                                        i +
                                                                                                                                                                        1
                                                                                                                                                                    ]
                                                                                                                                                                =
                                                                                                                                                                _Office
                                                                                                                                                                    .Length ==
                                                                                                                                                                _RawOffice
                                                                                                                                                                    .Length
                                                                                                                                                                    ? string
                                                                                                                                                                        .Empty
                                                                                                                                                                    : rgv0
                                                                                                                                                                        .Cells
                                                                                                                                                                        [
                                                                                                                                                                            listColName
                                                                                                                                                                            [
                                                                                                                                                                                i
                                                                                                                                                                            ]
                                                                                                                                                                        ]
                                                                                                                                                                        .Value
                                                                                                                                                                        .ToString
                                                                                                                                                                        ();
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            if
                                                                                                                                                            (
                                                                                                                                                                listColName
                                                                                                                                                                [
                                                                                                                                                                    i
                                                                                                                                                                ] ==
                                                                                                                                                                "DirectWorking")
                                                                                                                                                            {
                                                                                                                                                                var
                                                                                                                                                                    rDirectWorking
                                                                                                                                                                        =
                                                                                                                                                                        oWorkSheet
                                                                                                                                                                            .get_Range
                                                                                                                                                                            (GetExcelColumnName
                                                                                                                                                                             (i +
                                                                                                                                                                              1) +
                                                                                                                                                                             indexRow,
                                                                                                                                                                                GetExcelColumnName
                                                                                                                                                                                (i +
                                                                                                                                                                                 1) +
                                                                                                                                                                                indexRow);
                                                                                                                                                                rDirectWorking
                                                                                                                                                                        .HorizontalAlignment
                                                                                                                                                                    =
                                                                                                                                                                    XlHAlign
                                                                                                                                                                        .xlHAlignCenter;

                                                                                                                                                                var
                                                                                                                                                                    _Direct
                                                                                                                                                                        =
                                                                                                                                                                        Convert
                                                                                                                                                                            .ToInt32
                                                                                                                                                                            (rgv0
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    listColName
                                                                                                                                                                                    [
                                                                                                                                                                                        i
                                                                                                                                                                                    ]
                                                                                                                                                                                ]
                                                                                                                                                                                .Value
                                                                                                                                                                                .ToString
                                                                                                                                                                                ());
                                                                                                                                                                oWorkSheet
                                                                                                                                                                        .Cells
                                                                                                                                                                        [
                                                                                                                                                                            indexRow,
                                                                                                                                                                            i +
                                                                                                                                                                            1
                                                                                                                                                                        ]
                                                                                                                                                                    =
                                                                                                                                                                    _Direct ==
                                                                                                                                                                    1
                                                                                                                                                                        ? "Trực tiếp"
                                                                                                                                                                        : "Gián tiếp";
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                if
                                                                                                                                                                (
                                                                                                                                                                    listColName
                                                                                                                                                                    [
                                                                                                                                                                        i
                                                                                                                                                                    ] ==
                                                                                                                                                                    "AirlinesWorking")
                                                                                                                                                                {
                                                                                                                                                                    var
                                                                                                                                                                        rAirlinesWorking
                                                                                                                                                                            =
                                                                                                                                                                            oWorkSheet
                                                                                                                                                                                .get_Range
                                                                                                                                                                                (GetExcelColumnName
                                                                                                                                                                                 (i +
                                                                                                                                                                                  1) +
                                                                                                                                                                                 indexRow,
                                                                                                                                                                                    GetExcelColumnName
                                                                                                                                                                                    (i +
                                                                                                                                                                                     1) +
                                                                                                                                                                                    indexRow);
                                                                                                                                                                    rAirlinesWorking
                                                                                                                                                                            .HorizontalAlignment
                                                                                                                                                                        =
                                                                                                                                                                        XlHAlign
                                                                                                                                                                            .xlHAlignCenter;

                                                                                                                                                                    var
                                                                                                                                                                        _AirlinesWorking
                                                                                                                                                                            =
                                                                                                                                                                            rgv0
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    listColName
                                                                                                                                                                                    [
                                                                                                                                                                                        i
                                                                                                                                                                                    ]
                                                                                                                                                                                ]
                                                                                                                                                                                .Value
                                                                                                                                                                                .ToString
                                                                                                                                                                                ()
                                                                                                                                                                                .Trim
                                                                                                                                                                                ();
                                                                                                                                                                    oWorkSheet
                                                                                                                                                                            .Cells
                                                                                                                                                                            [
                                                                                                                                                                                indexRow,
                                                                                                                                                                                i +
                                                                                                                                                                                1
                                                                                                                                                                            ]
                                                                                                                                                                        =
                                                                                                                                                                        _AirlinesWorking
                                                                                                                                                                            .Length ==
                                                                                                                                                                        "-"
                                                                                                                                                                            .Trim
                                                                                                                                                                            ()
                                                                                                                                                                            .Length
                                                                                                                                                                            ? string
                                                                                                                                                                                .Empty
                                                                                                                                                                            : rgv0
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    listColName
                                                                                                                                                                                    [
                                                                                                                                                                                        i
                                                                                                                                                                                    ]
                                                                                                                                                                                ]
                                                                                                                                                                                .Value
                                                                                                                                                                                .ToString
                                                                                                                                                                                ();
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    var
                                                                                                                                                                        rGeneral
                                                                                                                                                                            =
                                                                                                                                                                            oWorkSheet
                                                                                                                                                                                .get_Range
                                                                                                                                                                                (GetExcelColumnName
                                                                                                                                                                                 (i +
                                                                                                                                                                                  1) +
                                                                                                                                                                                 indexRow,
                                                                                                                                                                                    GetExcelColumnName
                                                                                                                                                                                    (i +
                                                                                                                                                                                     1) +
                                                                                                                                                                                    indexRow);
                                                                                                                                                                    rGeneral
                                                                                                                                                                            .NumberFormat
                                                                                                                                                                        =
                                                                                                                                                                        "@";
                                                                                                                                                                    oWorkSheet
                                                                                                                                                                            .Cells
                                                                                                                                                                            [
                                                                                                                                                                                indexRow,
                                                                                                                                                                                i +
                                                                                                                                                                                1
                                                                                                                                                                            ]
                                                                                                                                                                        =
                                                                                                                                                                        rgv0
                                                                                                                                                                            .Cells
                                                                                                                                                                            [
                                                                                                                                                                                listColName
                                                                                                                                                                                [
                                                                                                                                                                                    i
                                                                                                                                                                                ]
                                                                                                                                                                            ]
                                                                                                                                                                            .Value ==
                                                                                                                                                                        DBNull
                                                                                                                                                                            .Value
                                                                                                                                                                            ? string
                                                                                                                                                                                .Empty
                                                                                                                                                                            : rgv0
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    listColName
                                                                                                                                                                                    [
                                                                                                                                                                                        i
                                                                                                                                                                                    ]
                                                                                                                                                                                ]
                                                                                                                                                                                .Value
                                                                                                                                                                                .ToString
                                                                                                                                                                                ();
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    indexRow++;
                    for (var ii = 1; ii < rgv.ChildRows.Count; ii++)
                    {
                        var rgv1 = rgv.ChildRows[ii];
                        var rgvBefore = rgv.ChildRows[ii - 1];
                        var _RootId = rgv1.Cells["RootId"].Value == DBNull.Value
                            ? 0
                            : Convert.ToInt32(rgv1.Cells["RootId"].Value);
                        var _RootIdBefore = rgvBefore.Cells["RootId"].Value == DBNull.Value
                            ? 0
                            : Convert.ToInt32(rgvBefore.Cells["RootId"].Value);

                        if ((ii < rgv.Rows.Count - 1) && (ii > 1))
                        {
                            RootId = _RootId;
                            RootIdBefore = _RootIdBefore;
                        }
                        if (_RootId != _RootIdBefore)
                        {
                            var rangeDept0 = oWorkSheet.get_Range("A" + indexRow, LastColumnName + indexRow);
                            rangeDept0.Merge(Type.Missing);
                            oWorkSheet.Cells[indexRow, 1] = (rgv1.Cells["RootName"].Value == DBNull.Value) ||
                                                            (rgv1.Cells["RootName"].Value.ToString().Length <= 0)
                                ? string.Empty
                                : rgv1.Cells["RootName"].Value.ToString().ToUpper();
                            rangeDept0.Font.Bold = true;

                            lstRoot.Add(indexRow);
                            indexRow++;
                        }
                        for (var i = 1; i <= listColName.Count; i++)
                            if (listColName[i - 1] == "UserId")
                            {
                                var rUserId = oWorkSheet.get_Range(GetExcelColumnName(i) + indexRow,
                                    GetExcelColumnName(i) + indexRow);
                                rUserId.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                rUserId.NumberFormat = "@";

                                oWorkSheet.Cells[indexRow, i] =
                                    StringFormat.GetUserCodeX6(int.Parse(rgv1.Cells[listColName[i - 1]].Value.ToString()));
                            }
                            else
                            {
                                if (listColName[i - 1] == "UserId1")
                                {
                                    var rUserId1 = oWorkSheet.get_Range(GetExcelColumnName(i) + indexRow,
                                        GetExcelColumnName(i) + indexRow);
                                    rUserId1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                    rUserId1.NumberFormat = "@";

                                    oWorkSheet.Cells[indexRow, i] = int.Parse(rgv1.Cells["UserId"].Value.ToString());
                                }
                                else
                                {
                                    if (listColName[i - 1] == "EmployeeCode")
                                    {
                                        var rEmployeeCode = oWorkSheet.get_Range(GetExcelColumnName(i) + indexRow,
                                            GetExcelColumnName(i) + indexRow);
                                        rEmployeeCode.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                        rEmployeeCode.NumberFormat = "@";

                                        var _EmployeeCode = rgv0.Cells[listColName[i - 1]].Value == DBNull.Value
                                            ? string.Empty
                                            : rgv0.Cells[listColName[i - 1]].Value.ToString();
                                        if (_EmployeeCode != string.Empty)
                                            oWorkSheet.Cells[indexRow, i] =
                                                StringFormat.GetUserCodeX6(
                                                    int.Parse(rgv0.Cells[listColName[i - 1]].Value.ToString()));
                                    }
                                    else
                                    {
                                        if (listColName[i - 1] == "FromDate")
                                        {
                                            var rFromDate = oWorkSheet.get_Range(GetExcelColumnName(i) + indexRow,
                                                GetExcelColumnName(i) + indexRow);
                                            rFromDate.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                            rFromDate.NumberFormat = "@";

                                            var _FromDate = (rgv1.Cells[listColName[i - 1]].Value == DBNull.Value) ||
                                                            (Convert.ToDateTime(rgv1.Cells[listColName[i - 1]].Value) ==
                                                             FormatDate.GetSQLDateMinValue)
                                                ? FormatDate.GetSQLDateMinValue
                                                : Convert.ToDateTime(rgv1.Cells[listColName[i - 1]].Value.ToString());
                                            oWorkSheet.Cells[indexRow, i] = _FromDate == MinValue
                                                ? string.Empty
                                                : Convert.ToDateTime(rgv1.Cells[listColName[i - 1]].Value.ToString())
                                                    .ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            if (listColName[i - 1] == "Expired")
                                            {
                                                var rFromDate = oWorkSheet.get_Range(GetExcelColumnName(i) + indexRow,
                                                    GetExcelColumnName(i) + indexRow);
                                                rFromDate.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                rFromDate.NumberFormat = "@";

                                                var _Expired = (rgv1.Cells[listColName[i - 1]].Value == DBNull.Value) ||
                                                               (Convert.ToDateTime(rgv1.Cells[listColName[i - 1]].Value) ==
                                                                FormatDate.GetSQLDateMinValue)
                                                    ? FormatDate.GetSQLDateMinValue
                                                    : Convert.ToDateTime(rgv1.Cells[listColName[i - 1]].Value.ToString());
                                                oWorkSheet.Cells[indexRow, i] = _Expired == MinValue
                                                    ? string.Empty
                                                    : Convert.ToDateTime(rgv1.Cells[listColName[i - 1]].Value.ToString())
                                                        .ToString("dd/MM/yyyy");
                                            }
                                            else
                                            {
                                                if (listColName[i - 1] == "Area1")
                                                {
                                                    var rArea1 = oWorkSheet.get_Range(GetExcelColumnName(i) + indexRow,
                                                        GetExcelColumnName(i) + indexRow);
                                                    rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                    rArea1.NumberFormat = "@";

                                                    oWorkSheet.Cells[indexRow, i] =
                                                        rgv1.Cells[listColName[i - 1]].Value == DBNull.Value
                                                            ? string.Empty
                                                            : GetSecurityAreaByFull(
                                                                rgv1.Cells[listColName[i - 1]].Value.ToString());
                                                }
                                                else
                                                {
                                                    if (listColName[i - 1] == "Area2")
                                                    {
                                                        var rArea1 =
                                                            oWorkSheet.get_Range(GetExcelColumnName(i) + indexRow,
                                                                GetExcelColumnName(i) + indexRow);
                                                        rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                        rArea1.NumberFormat = "@";

                                                        oWorkSheet.Cells[indexRow, i] =
                                                            rgv1.Cells[listColName[i - 1]].Value == DBNull.Value
                                                                ? string.Empty
                                                                : GetSecurityAreaByFull(
                                                                    rgv1.Cells[listColName[i - 1]].Value.ToString());
                                                    }
                                                    else
                                                    {
                                                        if (listColName[i - 1] == "Area3")
                                                        {
                                                            var rArea1 =
                                                                oWorkSheet.get_Range(GetExcelColumnName(i) + indexRow,
                                                                    GetExcelColumnName(i) + indexRow);
                                                            rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                            rArea1.NumberFormat = "@";

                                                            oWorkSheet.Cells[indexRow, i] =
                                                                rgv1.Cells[listColName[i - 1]].Value == DBNull.Value
                                                                    ? string.Empty
                                                                    : GetSecurityAreaByFull(
                                                                        rgv1.Cells[listColName[i - 1]].Value.ToString());
                                                        }
                                                        else
                                                        {
                                                            if (listColName[i - 1] == "Area4")
                                                            {
                                                                var rArea1 =
                                                                    oWorkSheet.get_Range(
                                                                        GetExcelColumnName(i) + indexRow,
                                                                        GetExcelColumnName(i) + indexRow);
                                                                rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                                rArea1.NumberFormat = "@";

                                                                oWorkSheet.Cells[indexRow, i] =
                                                                    rgv1.Cells[listColName[i - 1]].Value == DBNull.Value
                                                                        ? string.Empty
                                                                        : GetSecurityAreaByFull(
                                                                            rgv1.Cells[listColName[i - 1]].Value
                                                                                .ToString());
                                                            }
                                                            else
                                                            {
                                                                if (listColName[i - 1] == "Area5")
                                                                {
                                                                    var rArea1 =
                                                                        oWorkSheet.get_Range(
                                                                            GetExcelColumnName(i) + indexRow,
                                                                            GetExcelColumnName(i) + indexRow);
                                                                    rArea1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                                    rArea1.NumberFormat = "@";

                                                                    oWorkSheet.Cells[indexRow, i] =
                                                                        rgv1.Cells[listColName[i - 1]].Value ==
                                                                        DBNull.Value
                                                                            ? string.Empty
                                                                            : GetSecurityAreaByFull(
                                                                                rgv1.Cells[listColName[i - 1]].Value
                                                                                    .ToString());
                                                                }
                                                                else
                                                                {
                                                                    if (listColName[i - 1] == "Area6")
                                                                    {
                                                                        var rArea1 =
                                                                            oWorkSheet.get_Range(
                                                                                GetExcelColumnName(i) + indexRow,
                                                                                GetExcelColumnName(i) + indexRow);
                                                                        rArea1.HorizontalAlignment =
                                                                            XlHAlign.xlHAlignCenter;
                                                                        rArea1.NumberFormat = "@";

                                                                        oWorkSheet.Cells[indexRow, i] =
                                                                            rgv1.Cells[listColName[i - 1]].Value ==
                                                                            DBNull.Value
                                                                                ? string.Empty
                                                                                : GetSecurityAreaByFull(
                                                                                    rgv1.Cells[listColName[i - 1]].Value
                                                                                        .ToString());
                                                                    }
                                                                    else
                                                                    {
                                                                        if (listColName[i - 1] == "ContractFromDate")
                                                                        {
                                                                            var rFromDate =
                                                                                oWorkSheet.get_Range(
                                                                                    GetExcelColumnName(i) + indexRow,
                                                                                    GetExcelColumnName(i) + indexRow);
                                                                            rFromDate.HorizontalAlignment =
                                                                                XlHAlign.xlHAlignCenter;
                                                                            rFromDate.NumberFormat = "@";

                                                                            var _FromDate =
                                                                                (rgv1.Cells[listColName[i - 1]].Value ==
                                                                                 DBNull.Value) ||
                                                                                (Convert.ToDateTime(
                                                                                     rgv1.Cells[listColName[i - 1]]
                                                                                         .Value) ==
                                                                                 FormatDate.GetSQLDateMinValue)
                                                                                    ? FormatDate.GetSQLDateMinValue
                                                                                    : Convert.ToDateTime(
                                                                                        rgv1.Cells[listColName[i - 1]]
                                                                                            .Value.ToString());
                                                                            oWorkSheet.Cells[indexRow, i] = _FromDate ==
                                                                                                            MinValue
                                                                                ? string.Empty
                                                                                : Convert.ToDateTime(
                                                                                        rgv1.Cells[listColName[i - 1]]
                                                                                            .Value.ToString())
                                                                                    .ToString("dd/MM/yyyy");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (listColName[i - 1] == "ToDate")
                                                                            {
                                                                                var rToDate =
                                                                                    oWorkSheet.get_Range(
                                                                                        GetExcelColumnName(i) + indexRow,
                                                                                        GetExcelColumnName(i) + indexRow);
                                                                                rToDate.HorizontalAlignment =
                                                                                    XlHAlign.xlHAlignCenter;
                                                                                rToDate.NumberFormat = "@";

                                                                                var _ToDate =
                                                                                    (rgv1.Cells[listColName[i - 1]]
                                                                                         .Value == DBNull.Value) ||
                                                                                    (Convert.ToDateTime(
                                                                                         rgv1.Cells[listColName[i - 1]]
                                                                                             .Value) ==
                                                                                     FormatDate.GetSQLDateMinValue)
                                                                                        ? FormatDate.GetSQLDateMinValue
                                                                                        : Convert.ToDateTime(
                                                                                            rgv1.Cells[
                                                                                                    listColName[i - 1]]
                                                                                                .Value.ToString());
                                                                                oWorkSheet.Cells[indexRow, i] =
                                                                                    _ToDate == MinValue
                                                                                        ? "Không xác định"
                                                                                        : Convert.ToDateTime(
                                                                                                rgv1.Cells[
                                                                                                        listColName[
                                                                                                            i - 1]]
                                                                                                    .Value.ToString())
                                                                                            .ToString("dd/MM/yyyy");
                                                                            }
                                                                            else
                                                                            {
                                                                                if (listColName[i - 1] ==
                                                                                    "ContractToDate")
                                                                                {
                                                                                    var rContractToDate =
                                                                                        oWorkSheet.get_Range(
                                                                                            GetExcelColumnName(i) +
                                                                                            indexRow,
                                                                                            GetExcelColumnName(i) +
                                                                                            indexRow);
                                                                                    rContractToDate.HorizontalAlignment
                                                                                        = XlHAlign.xlHAlignCenter;
                                                                                    rContractToDate.NumberFormat = "@";

                                                                                    var _ToDate =
                                                                                        (rgv1.Cells[listColName[i - 1]]
                                                                                             .Value == DBNull.Value) ||
                                                                                        (Convert.ToDateTime(
                                                                                             rgv1.Cells[
                                                                                                     listColName[i - 1]]
                                                                                                 .Value) ==
                                                                                         FormatDate.GetSQLDateMinValue)
                                                                                            ? FormatDate
                                                                                                .GetSQLDateMinValue
                                                                                            : Convert.ToDateTime(
                                                                                                rgv1.Cells[
                                                                                                        listColName[
                                                                                                            i - 1]]
                                                                                                    .Value.ToString());
                                                                                    oWorkSheet.Cells[indexRow, i] =
                                                                                        _ToDate == MinValue
                                                                                            ? "Không xác định"
                                                                                            : Convert.ToDateTime(
                                                                                                    rgv1.Cells[
                                                                                                            listColName[
                                                                                                                i - 1]]
                                                                                                        .Value.ToString())
                                                                                                .ToString("dd/MM/yyyy");
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (listColName[i - 1] ==
                                                                                        "ContractNo")
                                                                                    {
                                                                                        var rContractNo =
                                                                                            oWorkSheet.get_Range(
                                                                                                GetExcelColumnName(i) +
                                                                                                indexRow,
                                                                                                GetExcelColumnName(i) +
                                                                                                indexRow);
                                                                                        rContractNo.HorizontalAlignment
                                                                                            = XlHAlign.xlHAlignCenter;
                                                                                        rContractNo.NumberFormat = "@";

                                                                                        oWorkSheet.Cells[indexRow, i] =
                                                                                            rgv1.Cells[
                                                                                                    listColName[i - 1]]
                                                                                                .Value == DBNull.Value
                                                                                                ? string.Empty
                                                                                                : StringFormat
                                                                                                    .GetUserCodeX6(
                                                                                                        Convert.ToInt32(
                                                                                                            rgv1.Cells[
                                                                                                                    listColName
                                                                                                                    [
                                                                                                                        i -
                                                                                                                        1
                                                                                                                    ]]
                                                                                                                .Value));
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (listColName[i - 1] ==
                                                                                            "Status")
                                                                                        {
                                                                                            var rStatus =
                                                                                                oWorkSheet.get_Range(
                                                                                                    GetExcelColumnName(i) +
                                                                                                    indexRow,
                                                                                                    GetExcelColumnName(i) +
                                                                                                    indexRow);
                                                                                            rStatus.HorizontalAlignment
                                                                                                =
                                                                                                XlHAlign.xlHAlignCenter;
                                                                                            rStatus.NumberFormat = "@";

                                                                                            var _Status = string.Empty;
                                                                                            if (
                                                                                                Convert.ToInt32(
                                                                                                    rgv1.Cells[
                                                                                                            listColName[
                                                                                                                i - 1]]
                                                                                                        .Value) == 1)
                                                                                            {
                                                                                                _Status =
                                                                                                    "Đang làm việc";
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (
                                                                                                    Convert.ToInt32(
                                                                                                        rgv1.Cells[
                                                                                                                listColName
                                                                                                                [
                                                                                                                    i -
                                                                                                                    1]]
                                                                                                            .Value) ==
                                                                                                    11)
                                                                                                {
                                                                                                    _Status =
                                                                                                        "Chuyển công tác";
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (
                                                                                                        Convert.ToInt32(
                                                                                                            rgv1.Cells[
                                                                                                                    listColName
                                                                                                                    [
                                                                                                                        i -
                                                                                                                        1
                                                                                                                    ]]
                                                                                                                .Value) ==
                                                                                                        0)
                                                                                                        _Status =
                                                                                                            "Nghỉ việc";
                                                                                                    else
                                                                                                        _Status =
                                                                                                            rgv1.Cells[
                                                                                                                    listColName
                                                                                                                    [
                                                                                                                        i -
                                                                                                                        1
                                                                                                                    ]]
                                                                                                                .Value
                                                                                                                .ToString
                                                                                                                ();
                                                                                                }
                                                                                            }
                                                                                            oWorkSheet.Cells[indexRow, i
                                                                                            ] = _Status;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (listColName[i - 1] ==
                                                                                                "WorkingHour")
                                                                                            {
                                                                                                var rWorkingName =
                                                                                                    oWorkSheet.get_Range
                                                                                                    (GetExcelColumnName(
                                                                                                         i) + indexRow,
                                                                                                        GetExcelColumnName
                                                                                                            (i) +
                                                                                                        indexRow);
                                                                                                rWorkingName
                                                                                                        .HorizontalAlignment
                                                                                                    =
                                                                                                    XlHAlign
                                                                                                        .xlHAlignRight;

                                                                                                oWorkSheet.Cells[
                                                                                                        indexRow, i] =
                                                                                                    rgv1.Cells[
                                                                                                            listColName[
                                                                                                                i - 1]]
                                                                                                        .Value ==
                                                                                                    DBNull.Value
                                                                                                        ? string.Empty
                                                                                                        : rgv1.Cells[
                                                                                                                listColName
                                                                                                                [
                                                                                                                    i -
                                                                                                                    1]]
                                                                                                            .Value
                                                                                                            .ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (
                                                                                                    listColName[i - 1] ==
                                                                                                    "Overtime")
                                                                                                {
                                                                                                    var rOvertime =
                                                                                                        oWorkSheet
                                                                                                            .get_Range(
                                                                                                                GetExcelColumnName
                                                                                                                    (i) +
                                                                                                                indexRow,
                                                                                                                GetExcelColumnName
                                                                                                                    (i) +
                                                                                                                indexRow);
                                                                                                    rOvertime
                                                                                                            .HorizontalAlignment
                                                                                                        =
                                                                                                        XlHAlign
                                                                                                            .xlHAlignRight;

                                                                                                    oWorkSheet.Cells[
                                                                                                            indexRow, i]
                                                                                                        =
                                                                                                        rgv1.Cells[
                                                                                                                listColName
                                                                                                                [
                                                                                                                    i -
                                                                                                                    1]]
                                                                                                            .Value ==
                                                                                                        DBNull.Value
                                                                                                            ? string
                                                                                                                .Empty
                                                                                                            : rgv1.Cells
                                                                                                                [
                                                                                                                    listColName
                                                                                                                    [
                                                                                                                        i -
                                                                                                                        1
                                                                                                                    ]]
                                                                                                                .Value
                                                                                                                .ToString
                                                                                                                ();
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (
                                                                                                        listColName[
                                                                                                            i - 1] ==
                                                                                                        "CreateDate")
                                                                                                    {
                                                                                                        var rCreateDate
                                                                                                            =
                                                                                                            oWorkSheet
                                                                                                                .get_Range
                                                                                                                (GetExcelColumnName
                                                                                                                     (i) +
                                                                                                                 indexRow,
                                                                                                                    GetExcelColumnName
                                                                                                                        (i) +
                                                                                                                    indexRow);
                                                                                                        rCreateDate
                                                                                                                .HorizontalAlignment
                                                                                                            =
                                                                                                            XlHAlign
                                                                                                                .xlHAlignCenter;
                                                                                                        rCreateDate
                                                                                                                .NumberFormat
                                                                                                            = "@";

                                                                                                        var _CreateDate
                                                                                                            =
                                                                                                            (rgv1.Cells[
                                                                                                                     listColName
                                                                                                                     [
                                                                                                                         i -
                                                                                                                         1
                                                                                                                     ]]
                                                                                                                 .Value ==
                                                                                                             DBNull
                                                                                                                 .Value) ||
                                                                                                            (Convert
                                                                                                                 .ToDateTime
                                                                                                                 (rgv1
                                                                                                                     .Cells
                                                                                                                     [
                                                                                                                         listColName
                                                                                                                         [
                                                                                                                             i -
                                                                                                                             1
                                                                                                                         ]
                                                                                                                     ]
                                                                                                                     .Value) ==
                                                                                                             FormatDate
                                                                                                                 .GetSQLDateMinValue)
                                                                                                                ? FormatDate
                                                                                                                    .GetSQLDateMinValue
                                                                                                                : Convert
                                                                                                                    .ToDateTime
                                                                                                                    (rgv1
                                                                                                                        .Cells
                                                                                                                        [
                                                                                                                            listColName
                                                                                                                            [
                                                                                                                                i -
                                                                                                                                1
                                                                                                                            ]
                                                                                                                        ]
                                                                                                                        .Value
                                                                                                                        .ToString
                                                                                                                        ());
                                                                                                        oWorkSheet.Cells
                                                                                                            [
                                                                                                                indexRow,
                                                                                                                i] =
                                                                                                            _CreateDate ==
                                                                                                            MinValue
                                                                                                                ? string
                                                                                                                    .Empty
                                                                                                                : Convert
                                                                                                                    .ToDateTime
                                                                                                                    (rgv1
                                                                                                                        .Cells
                                                                                                                        [
                                                                                                                            listColName
                                                                                                                            [
                                                                                                                                i -
                                                                                                                                1
                                                                                                                            ]
                                                                                                                        ]
                                                                                                                        .Value
                                                                                                                        .ToString
                                                                                                                        ())
                                                                                                                    .ToString
                                                                                                                    ("dd/MM/yyyy");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (
                                                                                                            listColName[
                                                                                                                i - 1] ==
                                                                                                            "WorkingName")
                                                                                                        {
                                                                                                            var
                                                                                                                rWorkingName
                                                                                                                    =
                                                                                                                    oWorkSheet
                                                                                                                        .get_Range
                                                                                                                        (GetExcelColumnName
                                                                                                                             (i) +
                                                                                                                         indexRow,
                                                                                                                            GetExcelColumnName
                                                                                                                                (i) +
                                                                                                                            indexRow);
                                                                                                            rWorkingName
                                                                                                                    .HorizontalAlignment
                                                                                                                =
                                                                                                                XlHAlign
                                                                                                                    .xlHAlignLeft;

                                                                                                            oWorkSheet
                                                                                                                    .Cells
                                                                                                                    [
                                                                                                                        indexRow,
                                                                                                                        i
                                                                                                                    ] =
                                                                                                                rgv1
                                                                                                                    .Cells
                                                                                                                    [
                                                                                                                        listColName
                                                                                                                        [
                                                                                                                            i -
                                                                                                                            1
                                                                                                                        ]
                                                                                                                    ]
                                                                                                                    .Value ==
                                                                                                                DBNull
                                                                                                                    .Value
                                                                                                                    ? string
                                                                                                                        .Empty
                                                                                                                    : rgv1
                                                                                                                        .Cells
                                                                                                                        [
                                                                                                                            listColName
                                                                                                                            [
                                                                                                                                i -
                                                                                                                                1
                                                                                                                            ]
                                                                                                                        ]
                                                                                                                        .Value
                                                                                                                        .ToString
                                                                                                                        ();
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (
                                                                                                                listColName
                                                                                                                [
                                                                                                                    i -
                                                                                                                    1] ==
                                                                                                                "Birthday")
                                                                                                            {
                                                                                                                var
                                                                                                                    rBirthday
                                                                                                                        =
                                                                                                                        oWorkSheet
                                                                                                                            .get_Range
                                                                                                                            (GetExcelColumnName
                                                                                                                                 (i) +
                                                                                                                             indexRow,
                                                                                                                                GetExcelColumnName
                                                                                                                                    (i) +
                                                                                                                                indexRow);
                                                                                                                rBirthday
                                                                                                                        .HorizontalAlignment
                                                                                                                    =
                                                                                                                    XlHAlign
                                                                                                                        .xlHAlignCenter;
                                                                                                                rBirthday
                                                                                                                        .NumberFormat
                                                                                                                    =
                                                                                                                    "@";

                                                                                                                var
                                                                                                                    _Birthday
                                                                                                                        =
                                                                                                                        (rgv1
                                                                                                                             .Cells
                                                                                                                             [
                                                                                                                                 listColName
                                                                                                                                 [
                                                                                                                                     i -
                                                                                                                                     1
                                                                                                                                 ]
                                                                                                                             ]
                                                                                                                             .Value ==
                                                                                                                         DBNull
                                                                                                                             .Value) ||
                                                                                                                        (Convert
                                                                                                                             .ToDateTime
                                                                                                                             (rgv1
                                                                                                                                 .Cells
                                                                                                                                 [
                                                                                                                                     listColName
                                                                                                                                     [
                                                                                                                                         i -
                                                                                                                                         1
                                                                                                                                     ]
                                                                                                                                 ]
                                                                                                                                 .Value) ==
                                                                                                                         FormatDate
                                                                                                                             .GetSQLDateMinValue)
                                                                                                                            ? FormatDate
                                                                                                                                .GetSQLDateMinValue
                                                                                                                            : Convert
                                                                                                                                .ToDateTime
                                                                                                                                (rgv1
                                                                                                                                    .Cells
                                                                                                                                    [
                                                                                                                                        listColName
                                                                                                                                        [
                                                                                                                                            i -
                                                                                                                                            1
                                                                                                                                        ]
                                                                                                                                    ]
                                                                                                                                    .Value
                                                                                                                                    .ToString
                                                                                                                                    ());
                                                                                                                oWorkSheet
                                                                                                                        .Cells
                                                                                                                        [
                                                                                                                            indexRow,
                                                                                                                            i
                                                                                                                        ]
                                                                                                                    =
                                                                                                                    _Birthday ==
                                                                                                                    MinValue
                                                                                                                        ? string
                                                                                                                            .Empty
                                                                                                                        : Convert
                                                                                                                            .ToDateTime
                                                                                                                            (rgv1
                                                                                                                                .Cells
                                                                                                                                [
                                                                                                                                    listColName
                                                                                                                                    [
                                                                                                                                        i -
                                                                                                                                        1
                                                                                                                                    ]
                                                                                                                                ]
                                                                                                                                .Value
                                                                                                                                .ToString
                                                                                                                                ())
                                                                                                                            .ToString
                                                                                                                            ("dd/MM/yyyy");
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (
                                                                                                                    listColName
                                                                                                                    [
                                                                                                                        i -
                                                                                                                        1
                                                                                                                    ] ==
                                                                                                                    "Sex")
                                                                                                                {
                                                                                                                    var
                                                                                                                        rWorkingName
                                                                                                                            =
                                                                                                                            oWorkSheet
                                                                                                                                .get_Range
                                                                                                                                (GetExcelColumnName
                                                                                                                                     (i) +
                                                                                                                                 indexRow,
                                                                                                                                    GetExcelColumnName
                                                                                                                                        (i) +
                                                                                                                                    indexRow);
                                                                                                                    rWorkingName
                                                                                                                            .HorizontalAlignment
                                                                                                                        =
                                                                                                                        XlHAlign
                                                                                                                            .xlHAlignLeft;

                                                                                                                    var
                                                                                                                        _Sex
                                                                                                                            =
                                                                                                                            rgv1
                                                                                                                                .Cells
                                                                                                                                [
                                                                                                                                    listColName
                                                                                                                                    [
                                                                                                                                        i -
                                                                                                                                        1
                                                                                                                                    ]
                                                                                                                                ]
                                                                                                                                .Value ==
                                                                                                                            DBNull
                                                                                                                                .Value
                                                                                                                                ? 9999
                                                                                                                                : Convert
                                                                                                                                    .ToInt32
                                                                                                                                    (rgv1
                                                                                                                                        .Cells
                                                                                                                                        [
                                                                                                                                            listColName
                                                                                                                                            [
                                                                                                                                                i -
                                                                                                                                                1
                                                                                                                                            ]
                                                                                                                                        ]
                                                                                                                                        .Value);
                                                                                                                    if (
                                                                                                                        _Sex ==
                                                                                                                        9999)
                                                                                                                        oWorkSheet
                                                                                                                                .Cells
                                                                                                                                [
                                                                                                                                    indexRow,
                                                                                                                                    i
                                                                                                                                ]
                                                                                                                            =
                                                                                                                            "Không xác định";
                                                                                                                    else
                                                                                                                        oWorkSheet
                                                                                                                                .Cells
                                                                                                                                [
                                                                                                                                    indexRow,
                                                                                                                                    i
                                                                                                                                ]
                                                                                                                            =
                                                                                                                            _Sex ==
                                                                                                                            1
                                                                                                                                ? "Nam"
                                                                                                                                : "Nữ";
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (
                                                                                                                        listColName
                                                                                                                        [
                                                                                                                            i -
                                                                                                                            1
                                                                                                                        ] ==
                                                                                                                        "TaxCode")
                                                                                                                    {
                                                                                                                        var
                                                                                                                            rWorkingName
                                                                                                                                =
                                                                                                                                oWorkSheet
                                                                                                                                    .get_Range
                                                                                                                                    (GetExcelColumnName
                                                                                                                                         (i) +
                                                                                                                                     indexRow,
                                                                                                                                        GetExcelColumnName
                                                                                                                                            (i) +
                                                                                                                                        indexRow);
                                                                                                                        rWorkingName
                                                                                                                                .HorizontalAlignment
                                                                                                                            =
                                                                                                                            XlHAlign
                                                                                                                                .xlHAlignCenter;
                                                                                                                        rWorkingName
                                                                                                                                .NumberFormat
                                                                                                                            =
                                                                                                                            "@";

                                                                                                                        oWorkSheet
                                                                                                                                .Cells
                                                                                                                                [
                                                                                                                                    indexRow,
                                                                                                                                    i
                                                                                                                                ]
                                                                                                                            =
                                                                                                                            rgv1
                                                                                                                                .Cells
                                                                                                                                [
                                                                                                                                    listColName
                                                                                                                                    [
                                                                                                                                        i -
                                                                                                                                        1
                                                                                                                                    ]
                                                                                                                                ]
                                                                                                                                .Value ==
                                                                                                                            DBNull
                                                                                                                                .Value
                                                                                                                                ? string
                                                                                                                                    .Empty
                                                                                                                                : rgv1
                                                                                                                                    .Cells
                                                                                                                                    [
                                                                                                                                        listColName
                                                                                                                                        [
                                                                                                                                            i -
                                                                                                                                            1
                                                                                                                                        ]
                                                                                                                                    ]
                                                                                                                                    .Value
                                                                                                                                    .ToString
                                                                                                                                    ();
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if
                                                                                                                        (
                                                                                                                            listColName
                                                                                                                            [
                                                                                                                                i -
                                                                                                                                1
                                                                                                                            ] ==
                                                                                                                            "JoinDate")
                                                                                                                        {
                                                                                                                            var
                                                                                                                                rJoinDate
                                                                                                                                    =
                                                                                                                                    oWorkSheet
                                                                                                                                        .get_Range
                                                                                                                                        (GetExcelColumnName
                                                                                                                                             (i) +
                                                                                                                                         indexRow,
                                                                                                                                            GetExcelColumnName
                                                                                                                                                (i) +
                                                                                                                                            indexRow);
                                                                                                                            rJoinDate
                                                                                                                                    .HorizontalAlignment
                                                                                                                                =
                                                                                                                                XlHAlign
                                                                                                                                    .xlHAlignCenter;
                                                                                                                            rJoinDate
                                                                                                                                    .NumberFormat
                                                                                                                                =
                                                                                                                                "@";

                                                                                                                            var
                                                                                                                                _JoinDate
                                                                                                                                    =
                                                                                                                                    (rgv1
                                                                                                                                         .Cells
                                                                                                                                         [
                                                                                                                                             listColName
                                                                                                                                             [
                                                                                                                                                 i -
                                                                                                                                                 1
                                                                                                                                             ]
                                                                                                                                         ]
                                                                                                                                         .Value ==
                                                                                                                                     DBNull
                                                                                                                                         .Value) ||
                                                                                                                                    (Convert
                                                                                                                                         .ToDateTime
                                                                                                                                         (rgv1
                                                                                                                                             .Cells
                                                                                                                                             [
                                                                                                                                                 listColName
                                                                                                                                                 [
                                                                                                                                                     i -
                                                                                                                                                     1
                                                                                                                                                 ]
                                                                                                                                             ]
                                                                                                                                             .Value) ==
                                                                                                                                     FormatDate
                                                                                                                                         .GetSQLDateMinValue)
                                                                                                                                        ? FormatDate
                                                                                                                                            .GetSQLDateMinValue
                                                                                                                                        : Convert
                                                                                                                                            .ToDateTime
                                                                                                                                            (rgv1
                                                                                                                                                .Cells
                                                                                                                                                [
                                                                                                                                                    listColName
                                                                                                                                                    [
                                                                                                                                                        i -
                                                                                                                                                        1
                                                                                                                                                    ]
                                                                                                                                                ]
                                                                                                                                                .Value
                                                                                                                                                .ToString
                                                                                                                                                ());
                                                                                                                            oWorkSheet
                                                                                                                                    .Cells
                                                                                                                                    [
                                                                                                                                        indexRow,
                                                                                                                                        i
                                                                                                                                    ]
                                                                                                                                =
                                                                                                                                _JoinDate ==
                                                                                                                                MinValue
                                                                                                                                    ? string
                                                                                                                                        .Empty
                                                                                                                                    : Convert
                                                                                                                                        .ToDateTime
                                                                                                                                        (rgv1
                                                                                                                                            .Cells
                                                                                                                                            [
                                                                                                                                                listColName
                                                                                                                                                [
                                                                                                                                                    i -
                                                                                                                                                    1
                                                                                                                                                ]
                                                                                                                                            ]
                                                                                                                                            .Value
                                                                                                                                            .ToString
                                                                                                                                            ())
                                                                                                                                        .ToString
                                                                                                                                        ("dd/MM/yyyy");
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if
                                                                                                                            (
                                                                                                                                listColName
                                                                                                                                [
                                                                                                                                    i -
                                                                                                                                    1
                                                                                                                                ] ==
                                                                                                                                "DateOfIssue")
                                                                                                                            {
                                                                                                                                var
                                                                                                                                    rDateOfIssue
                                                                                                                                        =
                                                                                                                                        oWorkSheet
                                                                                                                                            .get_Range
                                                                                                                                            (GetExcelColumnName
                                                                                                                                                 (i) +
                                                                                                                                             indexRow,
                                                                                                                                                GetExcelColumnName
                                                                                                                                                    (i) +
                                                                                                                                                indexRow);
                                                                                                                                rDateOfIssue
                                                                                                                                        .HorizontalAlignment
                                                                                                                                    =
                                                                                                                                    XlHAlign
                                                                                                                                        .xlHAlignCenter;
                                                                                                                                rDateOfIssue
                                                                                                                                        .NumberFormat
                                                                                                                                    =
                                                                                                                                    "@";

                                                                                                                                var
                                                                                                                                    _DateOfIssue
                                                                                                                                        =
                                                                                                                                        (rgv1
                                                                                                                                             .Cells
                                                                                                                                             [
                                                                                                                                                 listColName
                                                                                                                                                 [
                                                                                                                                                     i -
                                                                                                                                                     1
                                                                                                                                                 ]
                                                                                                                                             ]
                                                                                                                                             .Value ==
                                                                                                                                         DBNull
                                                                                                                                             .Value) ||
                                                                                                                                        (Convert
                                                                                                                                             .ToDateTime
                                                                                                                                             (rgv1
                                                                                                                                                 .Cells
                                                                                                                                                 [
                                                                                                                                                     listColName
                                                                                                                                                     [
                                                                                                                                                         i -
                                                                                                                                                         1
                                                                                                                                                     ]
                                                                                                                                                 ]
                                                                                                                                                 .Value) ==
                                                                                                                                         FormatDate
                                                                                                                                             .GetSQLDateMinValue)
                                                                                                                                            ? FormatDate
                                                                                                                                                .GetSQLDateMinValue
                                                                                                                                            : Convert
                                                                                                                                                .ToDateTime
                                                                                                                                                (rgv1
                                                                                                                                                    .Cells
                                                                                                                                                    [
                                                                                                                                                        listColName
                                                                                                                                                        [
                                                                                                                                                            i -
                                                                                                                                                            1
                                                                                                                                                        ]
                                                                                                                                                    ]
                                                                                                                                                    .Value
                                                                                                                                                    .ToString
                                                                                                                                                    ());
                                                                                                                                oWorkSheet
                                                                                                                                        .Cells
                                                                                                                                        [
                                                                                                                                            indexRow,
                                                                                                                                            i
                                                                                                                                        ]
                                                                                                                                    =
                                                                                                                                    _DateOfIssue ==
                                                                                                                                    MinValue
                                                                                                                                        ? string
                                                                                                                                            .Empty
                                                                                                                                        : Convert
                                                                                                                                            .ToDateTime
                                                                                                                                            (rgv1
                                                                                                                                                .Cells
                                                                                                                                                [
                                                                                                                                                    listColName
                                                                                                                                                    [
                                                                                                                                                        i -
                                                                                                                                                        1
                                                                                                                                                    ]
                                                                                                                                                ]
                                                                                                                                                .Value
                                                                                                                                                .ToString
                                                                                                                                                ())
                                                                                                                                            .ToString
                                                                                                                                            ("dd/MM/yyyy");
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if
                                                                                                                                (
                                                                                                                                    listColName
                                                                                                                                    [
                                                                                                                                        i -
                                                                                                                                        1
                                                                                                                                    ] ==
                                                                                                                                    "JoinCompanyDate")
                                                                                                                                {
                                                                                                                                    var
                                                                                                                                        rJoinCompanyDate
                                                                                                                                            =
                                                                                                                                            oWorkSheet
                                                                                                                                                .get_Range
                                                                                                                                                (GetExcelColumnName
                                                                                                                                                     (i) +
                                                                                                                                                 indexRow,
                                                                                                                                                    GetExcelColumnName
                                                                                                                                                        (i) +
                                                                                                                                                    indexRow);
                                                                                                                                    rJoinCompanyDate
                                                                                                                                            .HorizontalAlignment
                                                                                                                                        =
                                                                                                                                        XlHAlign
                                                                                                                                            .xlHAlignCenter;
                                                                                                                                    rJoinCompanyDate
                                                                                                                                            .NumberFormat
                                                                                                                                        =
                                                                                                                                        "@";

                                                                                                                                    var
                                                                                                                                        _JoinCompanyDate
                                                                                                                                            =
                                                                                                                                            (rgv1
                                                                                                                                                 .Cells
                                                                                                                                                 [
                                                                                                                                                     listColName
                                                                                                                                                     [
                                                                                                                                                         i -
                                                                                                                                                         1
                                                                                                                                                     ]
                                                                                                                                                 ]
                                                                                                                                                 .Value ==
                                                                                                                                             DBNull
                                                                                                                                                 .Value) ||
                                                                                                                                            (Convert
                                                                                                                                                 .ToDateTime
                                                                                                                                                 (rgv1
                                                                                                                                                     .Cells
                                                                                                                                                     [
                                                                                                                                                         listColName
                                                                                                                                                         [
                                                                                                                                                             i -
                                                                                                                                                             1
                                                                                                                                                         ]
                                                                                                                                                     ]
                                                                                                                                                     .Value) ==
                                                                                                                                             FormatDate
                                                                                                                                                 .GetSQLDateMinValue)
                                                                                                                                                ? FormatDate
                                                                                                                                                    .GetSQLDateMinValue
                                                                                                                                                : Convert
                                                                                                                                                    .ToDateTime
                                                                                                                                                    (rgv1
                                                                                                                                                        .Cells
                                                                                                                                                        [
                                                                                                                                                            listColName
                                                                                                                                                            [
                                                                                                                                                                i -
                                                                                                                                                                1
                                                                                                                                                            ]
                                                                                                                                                        ]
                                                                                                                                                        .Value
                                                                                                                                                        .ToString
                                                                                                                                                        ());
                                                                                                                                    oWorkSheet
                                                                                                                                            .Cells
                                                                                                                                            [
                                                                                                                                                indexRow,
                                                                                                                                                i
                                                                                                                                            ]
                                                                                                                                        =
                                                                                                                                        _JoinCompanyDate ==
                                                                                                                                        MinValue
                                                                                                                                            ? string
                                                                                                                                                .Empty
                                                                                                                                            : Convert
                                                                                                                                                .ToDateTime
                                                                                                                                                (rgv1
                                                                                                                                                    .Cells
                                                                                                                                                    [
                                                                                                                                                        listColName
                                                                                                                                                        [
                                                                                                                                                            i -
                                                                                                                                                            1
                                                                                                                                                        ]
                                                                                                                                                    ]
                                                                                                                                                    .Value
                                                                                                                                                    .ToString
                                                                                                                                                    ())
                                                                                                                                                .ToString
                                                                                                                                                ("dd/MM/yyyy");
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if
                                                                                                                                    (
                                                                                                                                        listColName
                                                                                                                                        [
                                                                                                                                            i -
                                                                                                                                            1
                                                                                                                                        ] ==
                                                                                                                                        "SocialInsuranceNo")
                                                                                                                                    {
                                                                                                                                        var
                                                                                                                                            rWorkingName
                                                                                                                                                =
                                                                                                                                                oWorkSheet
                                                                                                                                                    .get_Range
                                                                                                                                                    (GetExcelColumnName
                                                                                                                                                         (i) +
                                                                                                                                                     indexRow,
                                                                                                                                                        GetExcelColumnName
                                                                                                                                                            (i) +
                                                                                                                                                        indexRow);
                                                                                                                                        rWorkingName
                                                                                                                                                .HorizontalAlignment
                                                                                                                                            =
                                                                                                                                            XlHAlign
                                                                                                                                                .xlHAlignCenter;
                                                                                                                                        rWorkingName
                                                                                                                                                .NumberFormat
                                                                                                                                            =
                                                                                                                                            "@";

                                                                                                                                        oWorkSheet
                                                                                                                                                .Cells
                                                                                                                                                [
                                                                                                                                                    indexRow,
                                                                                                                                                    i
                                                                                                                                                ]
                                                                                                                                            =
                                                                                                                                            rgv1
                                                                                                                                                .Cells
                                                                                                                                                [
                                                                                                                                                    listColName
                                                                                                                                                    [
                                                                                                                                                        i -
                                                                                                                                                        1
                                                                                                                                                    ]
                                                                                                                                                ]
                                                                                                                                                .Value ==
                                                                                                                                            DBNull
                                                                                                                                                .Value
                                                                                                                                                ? string
                                                                                                                                                    .Empty
                                                                                                                                                : rgv1
                                                                                                                                                    .Cells
                                                                                                                                                    [
                                                                                                                                                        listColName
                                                                                                                                                        [
                                                                                                                                                            i -
                                                                                                                                                            1
                                                                                                                                                        ]
                                                                                                                                                    ]
                                                                                                                                                    .Value
                                                                                                                                                    .ToString
                                                                                                                                                    ();
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if
                                                                                                                                        (
                                                                                                                                            listColName
                                                                                                                                            [
                                                                                                                                                i -
                                                                                                                                                1
                                                                                                                                            ] ==
                                                                                                                                            "HealthInsuranceNo")
                                                                                                                                        {
                                                                                                                                            var
                                                                                                                                                rWorkingName
                                                                                                                                                    =
                                                                                                                                                    oWorkSheet
                                                                                                                                                        .get_Range
                                                                                                                                                        (GetExcelColumnName
                                                                                                                                                             (i) +
                                                                                                                                                         indexRow,
                                                                                                                                                            GetExcelColumnName
                                                                                                                                                                (i) +
                                                                                                                                                            indexRow);
                                                                                                                                            rWorkingName
                                                                                                                                                    .HorizontalAlignment
                                                                                                                                                =
                                                                                                                                                XlHAlign
                                                                                                                                                    .xlHAlignCenter;
                                                                                                                                            rWorkingName
                                                                                                                                                    .NumberFormat
                                                                                                                                                =
                                                                                                                                                "@";

                                                                                                                                            oWorkSheet
                                                                                                                                                    .Cells
                                                                                                                                                    [
                                                                                                                                                        indexRow,
                                                                                                                                                        i
                                                                                                                                                    ]
                                                                                                                                                =
                                                                                                                                                rgv1
                                                                                                                                                    .Cells
                                                                                                                                                    [
                                                                                                                                                        listColName
                                                                                                                                                        [
                                                                                                                                                            i -
                                                                                                                                                            1
                                                                                                                                                        ]
                                                                                                                                                    ]
                                                                                                                                                    .Value ==
                                                                                                                                                DBNull
                                                                                                                                                    .Value
                                                                                                                                                    ? string
                                                                                                                                                        .Empty
                                                                                                                                                    : rgv1
                                                                                                                                                        .Cells
                                                                                                                                                        [
                                                                                                                                                            listColName
                                                                                                                                                            [
                                                                                                                                                                i -
                                                                                                                                                                1
                                                                                                                                                            ]
                                                                                                                                                        ]
                                                                                                                                                        .Value
                                                                                                                                                        .ToString
                                                                                                                                                        ();
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if
                                                                                                                                            (
                                                                                                                                                listColName
                                                                                                                                                [
                                                                                                                                                    i -
                                                                                                                                                    1
                                                                                                                                                ] ==
                                                                                                                                                "LeaveDate")
                                                                                                                                            {
                                                                                                                                                var
                                                                                                                                                    rLeaveDate
                                                                                                                                                        =
                                                                                                                                                        oWorkSheet
                                                                                                                                                            .get_Range
                                                                                                                                                            (GetExcelColumnName
                                                                                                                                                                 (i) +
                                                                                                                                                             indexRow,
                                                                                                                                                                GetExcelColumnName
                                                                                                                                                                    (i) +
                                                                                                                                                                indexRow);
                                                                                                                                                rLeaveDate
                                                                                                                                                        .HorizontalAlignment
                                                                                                                                                    =
                                                                                                                                                    XlHAlign
                                                                                                                                                        .xlHAlignCenter;
                                                                                                                                                rLeaveDate
                                                                                                                                                        .NumberFormat
                                                                                                                                                    =
                                                                                                                                                    "@";

                                                                                                                                                var
                                                                                                                                                    _LeaveDate
                                                                                                                                                        =
                                                                                                                                                        (rgv1
                                                                                                                                                             .Cells
                                                                                                                                                             [
                                                                                                                                                                 listColName
                                                                                                                                                                 [
                                                                                                                                                                     i -
                                                                                                                                                                     1
                                                                                                                                                                 ]
                                                                                                                                                             ]
                                                                                                                                                             .Value ==
                                                                                                                                                         DBNull
                                                                                                                                                             .Value) ||
                                                                                                                                                        (Convert
                                                                                                                                                             .ToDateTime
                                                                                                                                                             (rgv1
                                                                                                                                                                 .Cells
                                                                                                                                                                 [
                                                                                                                                                                     listColName
                                                                                                                                                                     [
                                                                                                                                                                         i -
                                                                                                                                                                         1
                                                                                                                                                                     ]
                                                                                                                                                                 ]
                                                                                                                                                                 .Value) ==
                                                                                                                                                         FormatDate
                                                                                                                                                             .GetSQLDateMinValue)
                                                                                                                                                            ? FormatDate
                                                                                                                                                                .GetSQLDateMinValue
                                                                                                                                                            : Convert
                                                                                                                                                                .ToDateTime
                                                                                                                                                                (rgv1
                                                                                                                                                                    .Cells
                                                                                                                                                                    [
                                                                                                                                                                        listColName
                                                                                                                                                                        [
                                                                                                                                                                            i -
                                                                                                                                                                            1
                                                                                                                                                                        ]
                                                                                                                                                                    ]
                                                                                                                                                                    .Value
                                                                                                                                                                    .ToString
                                                                                                                                                                    ());
                                                                                                                                                oWorkSheet
                                                                                                                                                        .Cells
                                                                                                                                                        [
                                                                                                                                                            indexRow,
                                                                                                                                                            i
                                                                                                                                                        ]
                                                                                                                                                    =
                                                                                                                                                    _LeaveDate ==
                                                                                                                                                    MinValue
                                                                                                                                                        ? string
                                                                                                                                                            .Empty
                                                                                                                                                        : Convert
                                                                                                                                                            .ToDateTime
                                                                                                                                                            (rgv1
                                                                                                                                                                .Cells
                                                                                                                                                                [
                                                                                                                                                                    listColName
                                                                                                                                                                    [
                                                                                                                                                                        i -
                                                                                                                                                                        1
                                                                                                                                                                    ]
                                                                                                                                                                ]
                                                                                                                                                                .Value
                                                                                                                                                                .ToString
                                                                                                                                                                ())
                                                                                                                                                            .ToString
                                                                                                                                                            ("dd/MM/yyyy");
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                if
                                                                                                                                                (
                                                                                                                                                    listColName
                                                                                                                                                    [
                                                                                                                                                        i -
                                                                                                                                                        1
                                                                                                                                                    ] ==
                                                                                                                                                    "IdCard")
                                                                                                                                                {
                                                                                                                                                    var
                                                                                                                                                        rWorkingName
                                                                                                                                                            =
                                                                                                                                                            oWorkSheet
                                                                                                                                                                .get_Range
                                                                                                                                                                (GetExcelColumnName
                                                                                                                                                                     (i) +
                                                                                                                                                                 indexRow,
                                                                                                                                                                    GetExcelColumnName
                                                                                                                                                                        (i) +
                                                                                                                                                                    indexRow);
                                                                                                                                                    rWorkingName
                                                                                                                                                            .NumberFormat
                                                                                                                                                        =
                                                                                                                                                        "@";
                                                                                                                                                    rWorkingName
                                                                                                                                                            .HorizontalAlignment
                                                                                                                                                        =
                                                                                                                                                        XlHAlign
                                                                                                                                                            .xlHAlignCenter;

                                                                                                                                                    oWorkSheet
                                                                                                                                                            .Cells
                                                                                                                                                            [
                                                                                                                                                                indexRow,
                                                                                                                                                                i
                                                                                                                                                            ]
                                                                                                                                                        =
                                                                                                                                                        rgv1
                                                                                                                                                            .Cells
                                                                                                                                                            [
                                                                                                                                                                listColName
                                                                                                                                                                [
                                                                                                                                                                    i -
                                                                                                                                                                    1
                                                                                                                                                                ]
                                                                                                                                                            ]
                                                                                                                                                            .Value ==
                                                                                                                                                        DBNull
                                                                                                                                                            .Value
                                                                                                                                                            ? string
                                                                                                                                                                .Empty
                                                                                                                                                            : rgv1
                                                                                                                                                                .Cells
                                                                                                                                                                [
                                                                                                                                                                    listColName
                                                                                                                                                                    [
                                                                                                                                                                        i -
                                                                                                                                                                        1
                                                                                                                                                                    ]
                                                                                                                                                                ]
                                                                                                                                                                .Value
                                                                                                                                                                .ToString
                                                                                                                                                                ();
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    if
                                                                                                                                                    (
                                                                                                                                                        listColName
                                                                                                                                                        [
                                                                                                                                                            i -
                                                                                                                                                            1
                                                                                                                                                        ] ==
                                                                                                                                                        "DepartmentFullName")
                                                                                                                                                    {
                                                                                                                                                        var
                                                                                                                                                            dr
                                                                                                                                                                =
                                                                                                                                                                DepartmentEmployeeBLL
                                                                                                                                                                    .GetDRByDeptId
                                                                                                                                                                    (Convert
                                                                                                                                                                        .ToInt32
                                                                                                                                                                        (rgv1
                                                                                                                                                                            .Cells
                                                                                                                                                                            [
                                                                                                                                                                                "DepartmentId"
                                                                                                                                                                            ]
                                                                                                                                                                            .Value));
                                                                                                                                                        if
                                                                                                                                                        (
                                                                                                                                                            dr !=
                                                                                                                                                            null)
                                                                                                                                                        {
                                                                                                                                                            var
                                                                                                                                                                Level
                                                                                                                                                                    =
                                                                                                                                                                    Convert
                                                                                                                                                                        .ToInt32
                                                                                                                                                                        (dr
                                                                                                                                                                        [
                                                                                                                                                                            "Level"
                                                                                                                                                                        ]);

                                                                                                                                                            var
                                                                                                                                                                rWorkingName
                                                                                                                                                                    =
                                                                                                                                                                    oWorkSheet
                                                                                                                                                                        .get_Range
                                                                                                                                                                        (GetExcelColumnName
                                                                                                                                                                             (i) +
                                                                                                                                                                         indexRow,
                                                                                                                                                                            GetExcelColumnName
                                                                                                                                                                                (i) +
                                                                                                                                                                            indexRow);

                                                                                                                                                            oWorkSheet
                                                                                                                                                                    .Cells
                                                                                                                                                                    [
                                                                                                                                                                        indexRow,
                                                                                                                                                                        i
                                                                                                                                                                    ]
                                                                                                                                                                =
                                                                                                                                                                rgv1
                                                                                                                                                                    .Cells
                                                                                                                                                                    [
                                                                                                                                                                        listColName
                                                                                                                                                                        [
                                                                                                                                                                            i -
                                                                                                                                                                            1
                                                                                                                                                                        ]
                                                                                                                                                                    ]
                                                                                                                                                                    .Value ==
                                                                                                                                                                DBNull
                                                                                                                                                                    .Value
                                                                                                                                                                    ? string
                                                                                                                                                                        .Empty
                                                                                                                                                                    : Utilities
                                                                                                                                                                        .Utilities
                                                                                                                                                                        .GetDepartmentFullName
                                                                                                                                                                        (
                                                                                                                                                                            rgv1
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    listColName
                                                                                                                                                                                    [
                                                                                                                                                                                        i -
                                                                                                                                                                                        1
                                                                                                                                                                                    ]
                                                                                                                                                                                ]
                                                                                                                                                                                .Value
                                                                                                                                                                                .ToString
                                                                                                                                                                                (),
                                                                                                                                                                            Level);
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if
                                                                                                                                                        (
                                                                                                                                                            listColName
                                                                                                                                                            [
                                                                                                                                                                i -
                                                                                                                                                                1
                                                                                                                                                            ] ==
                                                                                                                                                            "Office")
                                                                                                                                                        {
                                                                                                                                                            var
                                                                                                                                                                _Office
                                                                                                                                                                    =
                                                                                                                                                                    rgv1
                                                                                                                                                                        .Cells
                                                                                                                                                                        [
                                                                                                                                                                            listColName
                                                                                                                                                                            [
                                                                                                                                                                                i -
                                                                                                                                                                                1
                                                                                                                                                                            ]
                                                                                                                                                                        ]
                                                                                                                                                                        .Value ==
                                                                                                                                                                    DBNull
                                                                                                                                                                        .Value
                                                                                                                                                                        ? "theo Quyết định số /QĐ-SAGS ngày "
                                                                                                                                                                        : rgv1
                                                                                                                                                                            .Cells
                                                                                                                                                                            [
                                                                                                                                                                                listColName
                                                                                                                                                                                [
                                                                                                                                                                                    i -
                                                                                                                                                                                    1
                                                                                                                                                                                ]
                                                                                                                                                                            ]
                                                                                                                                                                            .Value
                                                                                                                                                                            .ToString
                                                                                                                                                                            ()
                                                                                                                                                                            .Trim
                                                                                                                                                                            ();
                                                                                                                                                            var
                                                                                                                                                                _RawOffice
                                                                                                                                                                    =
                                                                                                                                                                    "theo Quyết định số /QĐ-SAGS ngày "
                                                                                                                                                                        .Trim
                                                                                                                                                                        ();

                                                                                                                                                            oWorkSheet
                                                                                                                                                                    .Cells
                                                                                                                                                                    [
                                                                                                                                                                        indexRow,
                                                                                                                                                                        i
                                                                                                                                                                    ]
                                                                                                                                                                =
                                                                                                                                                                _Office
                                                                                                                                                                    .Length ==
                                                                                                                                                                _RawOffice
                                                                                                                                                                    .Length
                                                                                                                                                                    ? string
                                                                                                                                                                        .Empty
                                                                                                                                                                    : rgv1
                                                                                                                                                                        .Cells
                                                                                                                                                                        [
                                                                                                                                                                            listColName
                                                                                                                                                                            [
                                                                                                                                                                                i -
                                                                                                                                                                                1
                                                                                                                                                                            ]
                                                                                                                                                                        ]
                                                                                                                                                                        .Value
                                                                                                                                                                        .ToString
                                                                                                                                                                        ();
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            if
                                                                                                                                                            (
                                                                                                                                                                listColName
                                                                                                                                                                [
                                                                                                                                                                    i -
                                                                                                                                                                    1
                                                                                                                                                                ] ==
                                                                                                                                                                "DirectWorking")
                                                                                                                                                            {
                                                                                                                                                                var
                                                                                                                                                                    rDirectWorking
                                                                                                                                                                        =
                                                                                                                                                                        oWorkSheet
                                                                                                                                                                            .get_Range
                                                                                                                                                                            (GetExcelColumnName
                                                                                                                                                                                 (i) +
                                                                                                                                                                             indexRow,
                                                                                                                                                                                GetExcelColumnName
                                                                                                                                                                                    (i) +
                                                                                                                                                                                indexRow);
                                                                                                                                                                rDirectWorking
                                                                                                                                                                        .HorizontalAlignment
                                                                                                                                                                    =
                                                                                                                                                                    XlHAlign
                                                                                                                                                                        .xlHAlignCenter;

                                                                                                                                                                var
                                                                                                                                                                    _Direct
                                                                                                                                                                        =
                                                                                                                                                                        rgv1
                                                                                                                                                                            .Cells
                                                                                                                                                                            [
                                                                                                                                                                                listColName
                                                                                                                                                                                [
                                                                                                                                                                                    i -
                                                                                                                                                                                    1
                                                                                                                                                                                ]
                                                                                                                                                                            ]
                                                                                                                                                                            .Value ==
                                                                                                                                                                        DBNull
                                                                                                                                                                            .Value
                                                                                                                                                                            ? 1
                                                                                                                                                                            : Convert
                                                                                                                                                                                .ToInt32
                                                                                                                                                                                (rgv1
                                                                                                                                                                                    .Cells
                                                                                                                                                                                    [
                                                                                                                                                                                        listColName
                                                                                                                                                                                        [
                                                                                                                                                                                            i -
                                                                                                                                                                                            1
                                                                                                                                                                                        ]
                                                                                                                                                                                    ]
                                                                                                                                                                                    .Value
                                                                                                                                                                                    .ToString
                                                                                                                                                                                    ());
                                                                                                                                                                oWorkSheet
                                                                                                                                                                        .Cells
                                                                                                                                                                        [
                                                                                                                                                                            indexRow,
                                                                                                                                                                            i
                                                                                                                                                                        ]
                                                                                                                                                                    =
                                                                                                                                                                    _Direct ==
                                                                                                                                                                    1
                                                                                                                                                                        ? "Trực tiếp"
                                                                                                                                                                        : "Gián tiếp";
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                if
                                                                                                                                                                (
                                                                                                                                                                    listColName
                                                                                                                                                                    [
                                                                                                                                                                        i -
                                                                                                                                                                        1
                                                                                                                                                                    ] ==
                                                                                                                                                                    "AirlinesWorking")
                                                                                                                                                                {
                                                                                                                                                                    var
                                                                                                                                                                        rAirlinesWorking
                                                                                                                                                                            =
                                                                                                                                                                            oWorkSheet
                                                                                                                                                                                .get_Range
                                                                                                                                                                                (GetExcelColumnName
                                                                                                                                                                                     (i) +
                                                                                                                                                                                 indexRow,
                                                                                                                                                                                    GetExcelColumnName
                                                                                                                                                                                        (i) +
                                                                                                                                                                                    indexRow);
                                                                                                                                                                    rAirlinesWorking
                                                                                                                                                                            .HorizontalAlignment
                                                                                                                                                                        =
                                                                                                                                                                        XlHAlign
                                                                                                                                                                            .xlHAlignCenter;

                                                                                                                                                                    var
                                                                                                                                                                        _AirlinesWorking
                                                                                                                                                                            =
                                                                                                                                                                            rgv1
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    listColName
                                                                                                                                                                                    [
                                                                                                                                                                                        i -
                                                                                                                                                                                        1
                                                                                                                                                                                    ]
                                                                                                                                                                                ]
                                                                                                                                                                                .Value ==
                                                                                                                                                                            DBNull
                                                                                                                                                                                .Value
                                                                                                                                                                                ? string
                                                                                                                                                                                    .Empty
                                                                                                                                                                                : rgv1
                                                                                                                                                                                    .Cells
                                                                                                                                                                                    [
                                                                                                                                                                                        listColName
                                                                                                                                                                                        [
                                                                                                                                                                                            i -
                                                                                                                                                                                            1
                                                                                                                                                                                        ]
                                                                                                                                                                                    ]
                                                                                                                                                                                    .Value
                                                                                                                                                                                    .ToString
                                                                                                                                                                                    ()
                                                                                                                                                                                    .Trim
                                                                                                                                                                                    ();
                                                                                                                                                                    oWorkSheet
                                                                                                                                                                            .Cells
                                                                                                                                                                            [
                                                                                                                                                                                indexRow,
                                                                                                                                                                                i
                                                                                                                                                                            ]
                                                                                                                                                                        =
                                                                                                                                                                        _AirlinesWorking
                                                                                                                                                                            .Length ==
                                                                                                                                                                        "-"
                                                                                                                                                                            .Trim
                                                                                                                                                                            ()
                                                                                                                                                                            .Length
                                                                                                                                                                            ? string
                                                                                                                                                                                .Empty
                                                                                                                                                                            : rgv1
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    listColName
                                                                                                                                                                                    [
                                                                                                                                                                                        i -
                                                                                                                                                                                        1
                                                                                                                                                                                    ]
                                                                                                                                                                                ]
                                                                                                                                                                                .Value
                                                                                                                                                                                .ToString
                                                                                                                                                                                ();
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    if
                                                                                                                                                                    (
                                                                                                                                                                        listColName
                                                                                                                                                                        [
                                                                                                                                                                            i -
                                                                                                                                                                            1
                                                                                                                                                                        ] ==
                                                                                                                                                                        "HighestLevelNameValue")
                                                                                                                                                                    {
                                                                                                                                                                        var
                                                                                                                                                                            rWorkingName
                                                                                                                                                                                =
                                                                                                                                                                                oWorkSheet
                                                                                                                                                                                    .get_Range
                                                                                                                                                                                    (GetExcelColumnName
                                                                                                                                                                                         (i) +
                                                                                                                                                                                     indexRow,
                                                                                                                                                                                        GetExcelColumnName
                                                                                                                                                                                            (i) +
                                                                                                                                                                                        indexRow);
                                                                                                                                                                        rWorkingName
                                                                                                                                                                                .NumberFormat
                                                                                                                                                                            =
                                                                                                                                                                            "@";
                                                                                                                                                                        rWorkingName
                                                                                                                                                                                .HorizontalAlignment
                                                                                                                                                                            =
                                                                                                                                                                            XlHAlign
                                                                                                                                                                                .xlHAlignLeft;

                                                                                                                                                                        oWorkSheet
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    indexRow,
                                                                                                                                                                                    i
                                                                                                                                                                                ]
                                                                                                                                                                            =
                                                                                                                                                                            rgv1
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    listColName
                                                                                                                                                                                    [
                                                                                                                                                                                        i -
                                                                                                                                                                                        1
                                                                                                                                                                                    ]
                                                                                                                                                                                ]
                                                                                                                                                                                .Value ==
                                                                                                                                                                            DBNull
                                                                                                                                                                                .Value
                                                                                                                                                                                ? string
                                                                                                                                                                                    .Empty
                                                                                                                                                                                : rgv1
                                                                                                                                                                                    .Cells
                                                                                                                                                                                    [
                                                                                                                                                                                        listColName
                                                                                                                                                                                        [
                                                                                                                                                                                            i -
                                                                                                                                                                                            1
                                                                                                                                                                                        ]
                                                                                                                                                                                    ]
                                                                                                                                                                                    .Value
                                                                                                                                                                                    .ToString
                                                                                                                                                                                    ();
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        var
                                                                                                                                                                            rWorkingName
                                                                                                                                                                                =
                                                                                                                                                                                oWorkSheet
                                                                                                                                                                                    .get_Range
                                                                                                                                                                                    (GetExcelColumnName
                                                                                                                                                                                         (i) +
                                                                                                                                                                                     indexRow,
                                                                                                                                                                                        GetExcelColumnName
                                                                                                                                                                                            (i) +
                                                                                                                                                                                        indexRow);
                                                                                                                                                                        rWorkingName
                                                                                                                                                                                .NumberFormat
                                                                                                                                                                            =
                                                                                                                                                                            "@";

                                                                                                                                                                        oWorkSheet
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    indexRow,
                                                                                                                                                                                    i
                                                                                                                                                                                ]
                                                                                                                                                                            =
                                                                                                                                                                            rgv1
                                                                                                                                                                                .Cells
                                                                                                                                                                                [
                                                                                                                                                                                    listColName
                                                                                                                                                                                    [
                                                                                                                                                                                        i -
                                                                                                                                                                                        1
                                                                                                                                                                                    ]
                                                                                                                                                                                ]
                                                                                                                                                                                .Value ==
                                                                                                                                                                            DBNull
                                                                                                                                                                                .Value
                                                                                                                                                                                ? string
                                                                                                                                                                                    .Empty
                                                                                                                                                                                : rgv1
                                                                                                                                                                                    .Cells
                                                                                                                                                                                    [
                                                                                                                                                                                        listColName
                                                                                                                                                                                        [
                                                                                                                                                                                            i -
                                                                                                                                                                                            1
                                                                                                                                                                                        ]
                                                                                                                                                                                    ]
                                                                                                                                                                                    .Value
                                                                                                                                                                                    .ToString
                                                                                                                                                                                    ();
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        indexRow++;
                        backgroundWorker1.ReportProgress(ii + 1, string.Empty);
                    }

                    var rangeAll = oWorkSheet.get_Range("A8", LastColumnName + indexRow);
                    rangeAll.Font.Size = 10;
                    rangeAll.Font.Name = "Times New Roman";
                }
            }
        }

        public bool IsRootNameColumn(List<int> _lst, int _indexRow)
        {
            var ReturnValue = false;
            foreach (var item in _lst)
                if (item.Equals(_indexRow))
                    ReturnValue = true;
                else
                    ReturnValue = false;
            return ReturnValue;
        }

        public _Application ExecuteExport(RadGridView exportDt, string fullName, string pathName)
        {
            if (exportDt.TableElement.VisualRows.Count > 0)
            {
                _Application oExcel = null;
                _Workbook oWorkBook = null;
                _Worksheet oWorkSheet = null;

                var fileName = string.Empty;
                try
                {
                    if ((exportDt.Name == "KSAN") || (exportDt.Name == "Nhân viên") || (exportDt.Name == "Học viên"))
                    {
                        _Worksheet oWorkSheetHocVien;

                        oExcel = new Application();
                        oExcel.Visible = false;


                        oWorkBook = oExcel.Workbooks.Add(Missing.Value);

                        oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];
                        oWorkSheet.Name = "Nhân viên";

                        oWorkSheetHocVien = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheet, Type.Missing,
                            Type.Missing);
                        oWorkSheetHocVien.Name = "Học viên";

                        foreach (_Worksheet ws in oWorkBook.Worksheets)
                            if (ws.Name == "Nhân viên")
                                InsertDataToWorkSheet(exportDt, ref oWorkSheet, fullName);
                            else
                                InsertDataToWorkSheet(exportDt, ref oWorkSheetHocVien, fullName);
                        oWorkSheet.Activate();

                        oExcel.UserControl = false;

                        fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" +
                                   pathName.ToUpper() + " " + DateTime.Now.ToString("dd-MM-yyyy");
                        if (File.Exists(fileName))
                            File.Delete(fileName);
                        oWorkBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, null, null, false, false,
                            XlSaveAsAccessMode.xlShared, false, false, null, null, null);

                        return oExcel;
                    }
                    else
                    {
                        if (exportDt.Name == "Workingday")
                        {
                            var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
                            var objD = new DepartmentsBLL();
                            objD.GetAllChildId(deptSelected);
                            var departmentIds = objD.ChildNodeIds;

                            var _lstSheet = new List<string>();
                            foreach (var item in radCheckedDropDownList1.CheckedItems)
                                _lstSheet.Add(item.Text);

                            return ExecuteExport_Workingday(departmentIds, _lstSheet, radGridView1, Text, Text,
                                Convert.ToInt32(radDropDownList3.SelectedValue));
                        }
                        else
                        {
                            oExcel = new Application();
                            oExcel.Visible = false;


                            oWorkBook = oExcel.Workbooks.Add(Missing.Value);
                            oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];

                            oWorkSheet.Name = pathName;

                            InsertDataToWorkSheet(exportDt, ref oWorkSheet, fullName);

                            oExcel.UserControl = false;

                            fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" +
                                       pathName.ToUpper() + " " + DateTime.Now.ToString("dd-MM-yyyy");
                            if (File.Exists(fileName))
                                File.Delete(fileName);
                            oWorkBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, null, null, false, false,
                                XlSaveAsAccessMode.xlShared, false, false, null, null, null);

                            return oExcel;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = string.Concat(errorMessage, ex.Message);
                    errorMessage = string.Concat(errorMessage, " Line: ");
                    errorMessage = string.Concat(errorMessage, ex.Source);
                    throw new Exception(errorMessage);
                }
                finally
                {
                    oWorkBook.Close(true);
                    oExcel.Quit();
                    Marshal.ReleaseComObject(oExcel);
                }
            }
            return null;
        }

        public string GetExcelColumnName(int columnNumber)
        {
            var dividend = columnNumber;
            var columnName = string.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1)%26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo)/26;
            }

            return columnName;
        }


        public _Application ExecuteExport_Workingday(string depIds, List<string> lstSheet, RadGridView exportDt,
            string fullName, string pathName, int sYear)
        {
            if (exportDt.TableElement.VisualRows.Count > 0)
            {
                _Application oExcel;
                _Workbook oWorkBook;

                _Worksheet oWorkSheet;
                _Worksheet oWorkSheetF;
                _Worksheet oWorkSheetOm;
                _Worksheet oWorkSheetTS;
                _Worksheet oWorkSheetRo;
                _Worksheet oWorkSheetCo;
                _Worksheet oWorkSheetOmDN;
                _Worksheet oWorkSheetOmDNBHXH;
                _Worksheet oWorkSheetKHH;
                _Worksheet oWorkSheetST;
                _Worksheet oWorkSheetKhamthai;
                _Worksheet oWorkSheetTNLD;
                _Worksheet oWorkSheetFdb;
                _Worksheet oWorkSheetKo;
                _Worksheet oWorkSheetDiduong;
                _Worksheet oWorkSheetDinhChiCT;

                var fileName = string.Empty;
                try
                {
                    oExcel = new Application();
                    oExcel.Visible = false;


                    oWorkBook = oExcel.Workbooks.Add(Missing.Value);

                    oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];
                    oWorkSheet.Name = "Total";

                    oWorkSheetF = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheet, Type.Missing, Type.Missing);
                    oWorkSheetF.Name = "F";
                    oWorkSheetOm = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetF, Type.Missing, Type.Missing);
                    oWorkSheetOm.Name = "Om";
                    oWorkSheetTS = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetOm, Type.Missing, Type.Missing);
                    oWorkSheetTS.Name = "TS";
                    oWorkSheetKo = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetTS, Type.Missing, Type.Missing);
                    oWorkSheetKo.Name = "Ko";
                    oWorkSheetCo = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetKo, Type.Missing, Type.Missing);
                    oWorkSheetCo.Name = "Co";
                    oWorkSheetOmDN = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetCo, Type.Missing, Type.Missing);
                    oWorkSheetOmDN.Name = "OmDN";
                    oWorkSheetOmDNBHXH = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetOmDN, Type.Missing,
                        Type.Missing);
                    oWorkSheetOmDNBHXH.Name = "OmDNBHXH";
                    oWorkSheetKHH = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetOmDNBHXH, Type.Missing,
                        Type.Missing);
                    oWorkSheetKHH.Name = "KHH";
                    oWorkSheetST = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetKHH, Type.Missing, Type.Missing);
                    oWorkSheetST.Name = "ST";
                    oWorkSheetKhamthai = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetST, Type.Missing, Type.Missing);
                    oWorkSheetKhamthai.Name = "Khamthai";
                    oWorkSheetTNLD = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetKhamthai, Type.Missing,
                        Type.Missing);
                    oWorkSheetTNLD.Name = "TNLD";
                    oWorkSheetFdb = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetTNLD, Type.Missing, Type.Missing);
                    oWorkSheetFdb.Name = "Fdb";
                    oWorkSheetRo = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetFdb, Type.Missing, Type.Missing);
                    oWorkSheetRo.Name = "Ro";
                    oWorkSheetDiduong = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetKo, Type.Missing, Type.Missing);
                    oWorkSheetDiduong.Name = "Diduong";
                    oWorkSheetDinhChiCT = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetDiduong, Type.Missing,
                        Type.Missing);
                    oWorkSheetDinhChiCT.Name = "DinhChiCT";

                    foreach (_Worksheet ws in oWorkBook.Worksheets)
                        if (lstSheet.Exists(x => x.Contains(ws.Name)))
                            InsertDataToWorkSheet_Workingday(depIds, ws, fullName, sYear);
                    oWorkSheet.Activate();

                    oExcel.Visible = false;
                    oExcel.UserControl = false;

                    fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + pathName.ToUpper() +
                               " " + DateTime.Now.ToString("dd-MM-yyyy");
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                    oWorkBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, null, null, false, false,
                        XlSaveAsAccessMode.xlShared, false, false, null, null, null);


                    oWorkBook.Close(null, null, null);
                    oExcel.Workbooks.Close();
                    oExcel.Quit();

                    Marshal.ReleaseComObject(oExcel);

                    Marshal.ReleaseComObject(oWorkSheet);
                    Marshal.ReleaseComObject(oWorkSheetF);
                    Marshal.ReleaseComObject(oWorkSheetOm);
                    Marshal.ReleaseComObject(oWorkSheetTS);
                    Marshal.ReleaseComObject(oWorkSheetRo);
                    Marshal.ReleaseComObject(oWorkSheetCo);
                    Marshal.ReleaseComObject(oWorkSheetOmDN);
                    Marshal.ReleaseComObject(oWorkSheetOmDNBHXH);
                    Marshal.ReleaseComObject(oWorkSheetKHH);
                    Marshal.ReleaseComObject(oWorkSheetST);
                    Marshal.ReleaseComObject(oWorkSheetKhamthai);
                    Marshal.ReleaseComObject(oWorkSheetTNLD);
                    Marshal.ReleaseComObject(oWorkSheetFdb);
                    Marshal.ReleaseComObject(oWorkSheetKo);
                    Marshal.ReleaseComObject(oWorkSheetDiduong);
                    Marshal.ReleaseComObject(oWorkSheetDinhChiCT);

                    Marshal.ReleaseComObject(oWorkBook);

                    oWorkSheet = null;
                    oWorkSheetF = null;
                    oWorkSheetOm = null;
                    oWorkSheetTS = null;
                    oWorkSheetRo = null;
                    oWorkSheetCo = null;
                    oWorkSheetOmDN = null;
                    oWorkSheetOmDNBHXH = null;
                    oWorkSheetKHH = null;
                    oWorkSheetST = null;
                    oWorkSheetKhamthai = null;
                    oWorkSheetTNLD = null;
                    oWorkSheetFdb = null;
                    oWorkSheetKo = null;
                    oWorkSheetDiduong = null;
                    oWorkSheetDinhChiCT = null;

                    oExcel = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    return oExcel;
                }
                catch (Exception ex)
                {
                    string errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = string.Concat(errorMessage, ex.Message);
                    errorMessage = string.Concat(errorMessage, " Line: ");
                    errorMessage = string.Concat(errorMessage, ex.Source);
                    throw new Exception(errorMessage);
                }
            }
            return null;
        }

        public void InsertDataToWorkSheet_Workingday(string depIds, _Worksheet oWorkSheet, string ExportName,
            int sYear)
        {
            var RootId = 0;
            var RootIdBefore = 0;
            var initTitleIndex = 7;
            var orderNumber = 1;

            var indexRow = initTitleIndex + 2;
            var lstRoot = new List<int>();


            CreateHeader_Workingday(ref oWorkSheet, ref initTitleIndex, ExportName);


            if (oWorkSheet.Name == "Total")
            {
                var dt = WorkdayCoefficientEmployeesFinalBLL.GetForExport(depIds, 0, sYear);
                var dr0 = dt.Rows[0];
                var rangeDept = oWorkSheet.get_Range("A" + indexRow, "R" + indexRow);
                rangeDept.Merge(Type.Missing);
                oWorkSheet.Cells[indexRow, 1] = dr0["RootName"] == DBNull.Value
                    ? string.Empty
                    : dr0["RootName"].ToString().ToUpper();
                rangeDept.Font.Bold = true;
                lstRoot.Add(indexRow);

                indexRow++;

                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr0["UserId"] == DBNull.Value ? string.Empty : dr0["UserId"].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr0["FullName"] == DBNull.Value
                    ? string.Empty
                    : dr0["FullName"].ToString();
                oWorkSheet.Cells[indexRow, 4] = dr0["PositionName"] == DBNull.Value
                    ? string.Empty
                    : dr0["PositionName"].ToString();
                var drDep = DepartmentEmployeeBLL.GetDRByDeptId(Convert.ToInt32(dr0["DepartmentId"]));
                if (drDep != null)
                {
                    var Level = Convert.ToInt32(drDep["Level"]);

                    oWorkSheet.Cells[indexRow, 5] = dr0["DepartmentFullName"] == DBNull.Value
                        ? string.Empty
                        : Utilities.Utilities.GetDepartmentFullName(dr0["DepartmentFullName"].ToString(), Level);
                }
                oWorkSheet.Cells[indexRow, 6] = dr0["FLeft"] == DBNull.Value ? string.Empty : dr0["FLeft"].ToString();
                oWorkSheet.Cells[indexRow, 7] = dr0["OmLeft"] == DBNull.Value ? string.Empty : dr0["OmLeft"].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr0["TSLeft"] == DBNull.Value ? string.Empty : dr0["TSLeft"].ToString();
                oWorkSheet.Cells[indexRow, 9] = dr0["RoLeft"] == DBNull.Value ? string.Empty : dr0["RoLeft"].ToString();
                oWorkSheet.Cells[indexRow, 10] = dr0["CoLeft"] == DBNull.Value ? string.Empty : dr0["CoLeft"].ToString();
                oWorkSheet.Cells[indexRow, 11] = dr0["OmDN"] == DBNull.Value ? string.Empty : dr0["OmDN"].ToString();
                oWorkSheet.Cells[indexRow, 12] = dr0["OmDNBHXH"] == DBNull.Value
                    ? string.Empty
                    : dr0["OmDNBHXH"].ToString();
                oWorkSheet.Cells[indexRow, 13] = dr0["KHH"] == DBNull.Value ? string.Empty : dr0["KHH"].ToString();
                oWorkSheet.Cells[indexRow, 14] = dr0["ST"] == DBNull.Value ? string.Empty : dr0["ST"].ToString();
                oWorkSheet.Cells[indexRow, 15] = dr0["Khamthai"] == DBNull.Value
                    ? string.Empty
                    : dr0["Khamthai"].ToString();
                oWorkSheet.Cells[indexRow, 16] = dr0["TNLD"] == DBNull.Value ? string.Empty : dr0["TNLD"].ToString();
                oWorkSheet.Cells[indexRow, 17] = dr0["Fdb"] == DBNull.Value ? string.Empty : dr0["Fdb"].ToString();
                oWorkSheet.Cells[indexRow, 18] = dr0["Ro"] == DBNull.Value ? string.Empty : dr0["Ro"].ToString();
                oWorkSheet.Cells[indexRow, 19] = dr0["Diduong"] == DBNull.Value
                    ? string.Empty
                    : dr0["Diduong"].ToString();
                oWorkSheet.Cells[indexRow, 20] = dr0["DinhChiCT"] == DBNull.Value
                    ? string.Empty
                    : dr0["DinhChiCT"].ToString();
                for (var i = 1; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var dr_1 = dt.Rows[i - 1];
                    var rootId = dr["RootId"] == DBNull.Value ? 0 : int.Parse(dr["RootId"].ToString());
                    var rootId_1 = dr_1["RootId"] == DBNull.Value ? 0 : int.Parse(dr_1["RootId"].ToString());

                    indexRow++;
                    if ((i < dt.Rows.Count - 1) && (i > 1))
                    {
                        RootId = rootId;
                        RootIdBefore = rootId_1;
                    }

                    if (RootId != RootIdBefore)
                    {
                        rangeDept = oWorkSheet.get_Range("A" + indexRow, "R" + indexRow);
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] = dr["RootName"] == DBNull.Value
                            ? string.Empty
                            : dr["RootName"].ToString().ToUpper();
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }


                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                    oWorkSheet.Cells[indexRow, 2] = dr["UserId"] == DBNull.Value
                        ? string.Empty
                        : dr["UserId"].ToString();
                    oWorkSheet.Cells[indexRow, 3] = dr["FullName"] == DBNull.Value
                        ? string.Empty
                        : dr["FullName"].ToString();
                    oWorkSheet.Cells[indexRow, 4] = dr["PositionName"] == DBNull.Value
                        ? string.Empty
                        : dr["PositionName"].ToString();
                    var drDep1 = DepartmentEmployeeBLL.GetDRByDeptId(Convert.ToInt32(dr["DepartmentId"]));
                    if (drDep1 != null)
                    {
                        var Level = Convert.ToInt32(drDep1["Level"]);

                        oWorkSheet.Cells[indexRow, 5] = dr["DepartmentFullName"] == DBNull.Value
                            ? string.Empty
                            : Utilities.Utilities.GetDepartmentFullName(dr["DepartmentFullName"].ToString(), Level);
                    }
                    oWorkSheet.Cells[indexRow, 6] = dr["FLeft"] == DBNull.Value ? string.Empty : dr["FLeft"].ToString();
                    oWorkSheet.Cells[indexRow, 7] = dr["OmLeft"] == DBNull.Value
                        ? string.Empty
                        : dr["OmLeft"].ToString();
                    oWorkSheet.Cells[indexRow, 8] = dr["TSLeft"] == DBNull.Value
                        ? string.Empty
                        : dr["TSLeft"].ToString();
                    oWorkSheet.Cells[indexRow, 9] = dr["RoLeft"] == DBNull.Value
                        ? string.Empty
                        : dr["RoLeft"].ToString();
                    oWorkSheet.Cells[indexRow, 10] = dr["CoLeft"] == DBNull.Value
                        ? string.Empty
                        : dr["CoLeft"].ToString();
                    oWorkSheet.Cells[indexRow, 11] = dr["OmDN"] == DBNull.Value ? string.Empty : dr["OmDN"].ToString();
                    oWorkSheet.Cells[indexRow, 12] = dr["OmDNBHXH"] == DBNull.Value
                        ? string.Empty
                        : dr["OmDNBHXH"].ToString();
                    oWorkSheet.Cells[indexRow, 13] = dr["KHH"] == DBNull.Value ? string.Empty : dr["KHH"].ToString();
                    oWorkSheet.Cells[indexRow, 14] = dr["ST"] == DBNull.Value ? string.Empty : dr["ST"].ToString();
                    oWorkSheet.Cells[indexRow, 15] = dr["Khamthai"] == DBNull.Value
                        ? string.Empty
                        : dr["Khamthai"].ToString();
                    oWorkSheet.Cells[indexRow, 16] = dr["TNLD"] == DBNull.Value ? string.Empty : dr["TNLD"].ToString();
                    oWorkSheet.Cells[indexRow, 17] = dr["Fdb"] == DBNull.Value ? string.Empty : dr["Fdb"].ToString();
                    oWorkSheet.Cells[indexRow, 18] = dr["Ro"] == DBNull.Value ? string.Empty : dr["Ro"].ToString();
                    oWorkSheet.Cells[indexRow, 19] = dr["Diduong"] == DBNull.Value
                        ? string.Empty
                        : dr["Diduong"].ToString();
                    oWorkSheet.Cells[indexRow, 20] = dr["DinhChiCT"] == DBNull.Value
                        ? string.Empty
                        : dr["DinhChiCT"].ToString();
                }
            }


            else
            {
                var dt0 = WorkdayCoefficientEmployeesFinalBLL.GetForExport(depIds, 0, sYear);
                var dtEmp =
                    WorkdayCoefficientEmployeesFinalBLL.GetDataByMonthYear(Convert.ToInt32(dt0.Rows[0]["UserId"]), 0,
                        sYear, 2);
                var dr0Emp = dtEmp.Rows[0];
                var rangeDept = oWorkSheet.get_Range("A" + indexRow, "R" + indexRow);
                rangeDept.Merge(Type.Missing);
                oWorkSheet.Cells[indexRow, 1] = dr0Emp["RootName"] == DBNull.Value
                    ? string.Empty
                    : dr0Emp["RootName"].ToString().ToUpper();
                rangeDept.Font.Bold = true;


                indexRow++;

                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr0Emp["UserId"] == DBNull.Value
                    ? string.Empty
                    : dr0Emp["UserId"].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr0Emp["FullName"] == DBNull.Value
                    ? string.Empty
                    : dr0Emp["FullName"].ToString();
                oWorkSheet.Cells[indexRow, 4] = dr0Emp["PositionName"] == DBNull.Value
                    ? string.Empty
                    : dr0Emp["PositionName"].ToString();
                var Level = Convert.ToInt32(dr0Emp["Level"]);
                oWorkSheet.Cells[indexRow, 5] = dr0Emp["DepartmentFullName"] == DBNull.Value
                    ? string.Empty
                    : Utilities.Utilities.GetDepartmentFullName(dr0Emp["DepartmentFullName"].ToString(), Level);

                CalculateTotalLeave(dtEmp, oWorkSheet, indexRow);
                for (var i = 1; i < dt0.Rows.Count; i++)
                {
                    var dr1Emp = dt0.Rows[i];
                    var drNext = dt0.Rows[i - 1];
                    var rootId = dr1Emp["RootId"] == DBNull.Value ? 0 : int.Parse(dr1Emp["RootId"].ToString());
                    var rootId_1 = drNext["RootId"] == DBNull.Value ? 0 : int.Parse(drNext["RootId"].ToString());
                    var dt1Emp =
                        WorkdayCoefficientEmployeesFinalBLL.GetDataByMonthYear(Convert.ToInt32(dr1Emp["UserId"]), 0,
                            sYear, 2);
                    indexRow++;
                    if ((i < dt0.Rows.Count - 1) && (i > 1))
                    {
                        RootId = rootId;
                        RootIdBefore = rootId_1;
                    }

                    if (RootId != RootIdBefore)
                    {
                        rangeDept = oWorkSheet.get_Range("A" + indexRow, "R" + indexRow);
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] = dr1Emp["RootName"] == DBNull.Value
                            ? string.Empty
                            : dr1Emp["RootName"].ToString().ToUpper();
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }

                    /// inserting order number
                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                    oWorkSheet.Cells[indexRow, 2] = dr1Emp["UserId"] == DBNull.Value
                        ? string.Empty
                        : dr1Emp["UserId"].ToString();
                    oWorkSheet.Cells[indexRow, 3] = dr1Emp["FullName"] == DBNull.Value
                        ? string.Empty
                        : dr1Emp["FullName"].ToString();
                    oWorkSheet.Cells[indexRow, 4] = dr1Emp["PositionName"] == DBNull.Value
                        ? string.Empty
                        : dr1Emp["PositionName"].ToString();
                    var Level0 = Convert.ToInt32(dt1Emp.Rows[0]["Level"]);
                    oWorkSheet.Cells[indexRow, 5] = dt1Emp.Rows[0]["DepartmentFullName"] == DBNull.Value
                        ? string.Empty
                        : Utilities.Utilities.GetDepartmentFullName(dt1Emp.Rows[0]["DepartmentFullName"].ToString(),
                            Level0);

                    CalculateTotalLeave(dt1Emp, oWorkSheet, indexRow);
                }
            }
        }

        public void CreateHeader_Workingday(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            string oWorkSheetName)
        {
            var childTitleIndex = initTitleIndex + 1;

            var rangeHeader1 = oWorkSheet.get_Range("A1", "R1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "CÔNG TY CỔ PHẦN PHỤC VỤ MẶT ĐẤT SÀI GON";
            rangeHeader1.Font.Size = 15;
            rangeHeader1.Font.Bold = true;
            rangeHeader1.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            rangeHeader1.Font.Name = "Times New Roman";

            var rangeHeader2 = oWorkSheet.get_Range("A3", "R3");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = oWorkSheetName.ToUpper();
            rangeHeader2.Font.Size = 16;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader2.Font.Name = "Times New Roman";

            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            ((Range) oWorkSheet.Cells[initTitleIndex, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, 1]).ColumnWidth = 5;
            ((Range) oWorkSheet.Cells[initTitleIndex, 1]).Font.Bold = true;
            var rNo = oWorkSheet.get_Range("A" + initTitleIndex, "A" + childTitleIndex);
            rNo.Merge(Type.Missing);
            rNo.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rNo.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[initTitleIndex, 2] = "Mã nhân viên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 7;
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).Font.Bold = true;
            var rUserId = oWorkSheet.get_Range("B" + initTitleIndex, "B" + childTitleIndex);
            rUserId.Merge(Type.Missing);
            rUserId.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rUserId.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[initTitleIndex, 3] = "Họ và tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 20;
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).Font.Bold = true;
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            var rFullName = oWorkSheet.get_Range("C" + initTitleIndex, "C" + childTitleIndex);
            rFullName.Merge(Type.Missing);
            rFullName.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rFullName.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[initTitleIndex, 4] = "Chức danh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 25;
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).Font.Bold = true;
            var rPositionName = oWorkSheet.get_Range("D" + initTitleIndex, "D" + childTitleIndex);
            rPositionName.Merge(Type.Missing);
            rPositionName.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rPositionName.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[initTitleIndex, 5] = "Phòng ban";
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).ColumnWidth = 25;
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).Font.Bold = true;
            var rDepartment = oWorkSheet.get_Range("E" + initTitleIndex, "E" + childTitleIndex);
            rDepartment.Merge(Type.Missing);
            rDepartment.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rDepartment.VerticalAlignment = XlVAlign.xlVAlignCenter;

            if (oWorkSheet.Name == "Total")
            {
                oWorkSheet.Cells[initTitleIndex, 6] = "F";
                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 7] = "Om";
                ((Range) oWorkSheet.Cells[initTitleIndex, 7]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 7]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 8] = "TS";
                ((Range) oWorkSheet.Cells[initTitleIndex, 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 8]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 18] = "Ko";
                ((Range) oWorkSheet.Cells[initTitleIndex, 18]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 18]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 10] = "Co";
                ((Range) oWorkSheet.Cells[initTitleIndex, 10]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 10]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 11] = "OmDN";
                ((Range) oWorkSheet.Cells[initTitleIndex, 11]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 11]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 12] = "OmDNBHXH";
                ((Range) oWorkSheet.Cells[initTitleIndex, 12]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 12]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 13] = "KHH";
                ((Range) oWorkSheet.Cells[initTitleIndex, 13]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 13]).Font.Bold = true;
                ;

                oWorkSheet.Cells[initTitleIndex, 14] = "ST";
                ((Range) oWorkSheet.Cells[initTitleIndex, 14]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 14]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 15] = "Khamthai";
                ((Range) oWorkSheet.Cells[initTitleIndex, 15]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 15]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 16] = "TNLD";
                ((Range) oWorkSheet.Cells[initTitleIndex, 16]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 16]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 17] = "Fdb";
                ((Range) oWorkSheet.Cells[initTitleIndex, 17]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 17]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 9] = "Ro";
                ((Range) oWorkSheet.Cells[initTitleIndex, 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 9]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 19] = "Diduong";
                ((Range) oWorkSheet.Cells[initTitleIndex, 19]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 19]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 20] = "DinhChiCT";
                ((Range) oWorkSheet.Cells[initTitleIndex, 20]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 20]).Font.Bold = true;
            }
            else
            {
                if (oWorkSheet.Name == "F")
                {
                    oWorkSheet.Cells[initTitleIndex, 6] = "F";
                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                    var rF = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                        GetExcelColumnName(18) + initTitleIndex);
                    rF.Merge(Type.Missing);
                    AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                }
                else
                {
                    if (oWorkSheet.Name == "Om")
                    {
                        oWorkSheet.Cells[initTitleIndex, 6] = "Om";
                        ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                        var rOm = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                            GetExcelColumnName(18) + initTitleIndex);
                        rOm.Merge(Type.Missing);
                        AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                    }
                    else
                    {
                         if (oWorkSheet.Name == "TS")
                        {
                            oWorkSheet.Cells[initTitleIndex, 6] = "TS";
                            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                            var rTS = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                GetExcelColumnName(18) + initTitleIndex);
                            rTS.Merge(Type.Missing);
                            AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                        }
                        else
                        {
                            if (oWorkSheet.Name == "Ro")
                            {
                                oWorkSheet.Cells[initTitleIndex, 6] = "Ro";
                                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                    XlHAlign.xlHAlignCenter;
                                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                var rRo = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                    GetExcelColumnName(18) + initTitleIndex);
                                rRo.Merge(Type.Missing);
                                AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                            }
                            else
                            {
                                if (oWorkSheet.Name == "Co")
                                {
                                    oWorkSheet.Cells[initTitleIndex, 6] = "Co";
                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                        XlHAlign.xlHAlignCenter;
                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                    var rCo = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                        GetExcelColumnName(18) + initTitleIndex);
                                    rCo.Merge(Type.Missing);
                                    AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                }
                                else
                                {
                                    if (oWorkSheet.Name == "OmDN")
                                    {
                                        oWorkSheet.Cells[initTitleIndex, 6] = "OmDN";
                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                            XlHAlign.xlHAlignCenter;
                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                        var rOmDN = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                            GetExcelColumnName(18) + initTitleIndex);
                                        rOmDN.Merge(Type.Missing);
                                        AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                    }
                                    else
                                    {
                                        if (oWorkSheet.Name == "OmDNBHXH")
                                        {
                                            oWorkSheet.Cells[initTitleIndex, 6] = "OmDNBHXH";
                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                                XlHAlign.xlHAlignCenter;
                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                            var rOmDNBHXH = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                                GetExcelColumnName(18) + initTitleIndex);
                                            rOmDNBHXH.Merge(Type.Missing);
                                            AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                        }
                                        else
                                        {
                                            if (oWorkSheet.Name == "KHH")
                                            {
                                                oWorkSheet.Cells[initTitleIndex, 6] = "KHH";
                                                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                                    XlHAlign.xlHAlignCenter;
                                                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                                var rKHH = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                                    GetExcelColumnName(18) + initTitleIndex);
                                                rKHH.Merge(Type.Missing);
                                                AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                            }
                                            else
                                            {
                                                if (oWorkSheet.Name == "ST")
                                                {
                                                    oWorkSheet.Cells[initTitleIndex, 6] = "ST";
                                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                                        XlHAlign.xlHAlignCenter;
                                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                                    var rST =
                                                        oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                                            GetExcelColumnName(18) + initTitleIndex);
                                                    rST.Merge(Type.Missing);
                                                    AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                                }
                                                else
                                                {
                                                    if (oWorkSheet.Name == "Khamthai")
                                                    {
                                                        oWorkSheet.Cells[initTitleIndex, 6] = "KHH";
                                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                            .HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                                        var rKhamthai =
                                                            oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                                                GetExcelColumnName(18) + initTitleIndex);
                                                        rKhamthai.Merge(Type.Missing);
                                                        AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                                    }
                                                    else
                                                    {
                                                        if (oWorkSheet.Name == "TNLD")
                                                        {
                                                            oWorkSheet.Cells[initTitleIndex, 6] = "TNLD";
                                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                .HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold =
                                                                true;
                                                            var rTNLD =
                                                                oWorkSheet.get_Range(
                                                                    GetExcelColumnName(6) + initTitleIndex,
                                                                    GetExcelColumnName(18) + initTitleIndex);
                                                            rTNLD.Merge(Type.Missing);
                                                            AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                                        }
                                                        else
                                                        {
                                                            if (oWorkSheet.Name == "Fdb")
                                                            {
                                                                oWorkSheet.Cells[initTitleIndex, 6] = "Fdb";
                                                                ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                    .HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold
                                                                    = true;
                                                                var rFdb =
                                                                    oWorkSheet.get_Range(
                                                                        GetExcelColumnName(6) + initTitleIndex,
                                                                        GetExcelColumnName(18) + initTitleIndex);
                                                                rFdb.Merge(Type.Missing);
                                                                AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                                            }
                                                            else
                                                            {
                                                                if (oWorkSheet.Name == "Ko")
                                                                {
                                                                    oWorkSheet.Cells[initTitleIndex, 6] = "Ko";
                                                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                        .HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font
                                                                        .Bold = true;
                                                                    var rKo =
                                                                        oWorkSheet.get_Range(
                                                                            GetExcelColumnName(6) + initTitleIndex,
                                                                            GetExcelColumnName(18) + initTitleIndex);
                                                                    rKo.Merge(Type.Missing);
                                                                    AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                                                }
                                                                else
                                                                {
                                                                    if (oWorkSheet.Name == "Diduong")
                                                                    {
                                                                        oWorkSheet.Cells[initTitleIndex, 6] = "Diduong";
                                                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                                .HorizontalAlignment =
                                                                            XlHAlign.xlHAlignCenter;
                                                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                            .Font.Bold = true;
                                                                        var rDiduong =
                                                                            oWorkSheet.get_Range(
                                                                                GetExcelColumnName(6) + initTitleIndex,
                                                                                GetExcelColumnName(18) + initTitleIndex);
                                                                        rDiduong.Merge(Type.Missing);
                                                                        AddColumn_Workingday(oWorkSheet, childTitleIndex,
                                                                            6);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (oWorkSheet.Name == "DinhChiCT")
                                                                        {
                                                                            oWorkSheet.Cells[initTitleIndex, 6] =
                                                                                "DinhChiCT";
                                                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                                    .HorizontalAlignment =
                                                                                XlHAlign.xlHAlignCenter;
                                                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                                .Font.Bold = true;
                                                                            var rDC =
                                                                                oWorkSheet.get_Range(
                                                                                    GetExcelColumnName(6) +
                                                                                    initTitleIndex,
                                                                                    GetExcelColumnName(18) +
                                                                                    initTitleIndex);
                                                                            rDC.Merge(Type.Missing);
                                                                            AddColumn_Workingday(oWorkSheet,
                                                                                childTitleIndex, 6);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AddColumn_Workingday(_Worksheet oWorkSheet, int initTitleIndex, int startNumber)
        {
            for (var i = 0; i < 13; i++)
                if (i == 12)
                {
                    oWorkSheet.Cells[initTitleIndex, startNumber + i] = "T";
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).ColumnWidth = 4;
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).Font.Bold = true;
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).HorizontalAlignment =
                        XlHAlign.xlHAlignCenter;
                }
                else
                {
                    oWorkSheet.Cells[initTitleIndex, startNumber + i] = i + 1;
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).ColumnWidth = 3;
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).Font.Bold = true;
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).HorizontalAlignment =
                        XlHAlign.xlHAlignCenter;
                }
        }

        public List<string> GetAllLeaveCode()
        {
            var list = new List<string>();

            list.Add("Total");
            list.Add("F");
            list.Add("Om");
            list.Add("TS");
            list.Add("Ro");
            list.Add("Co");
            list.Add("OmDN");
            list.Add("OmDNBHXH");
            list.Add("KHH");
            list.Add("ST");
            list.Add("Khamthai");
            list.Add("TNLD");
            list.Add("Fdb");
            list.Add("Ko");
            list.Add("Diduong");
            list.Add("DinhChiCT");

            return list;
        }

        public int CalculateTotal(string LeaveCode, DataTable dt)
        {
            return CalculateTotalByCode(LeaveCode, dt);
        }

        public int CalculateTotalByCode(string LeaveCode, DataTable dt)
        {
            var Return = 0;
            foreach (DataRow dr in dt.Rows)
                if (dr[LeaveCode] != DBNull.Value)
                    Return += Convert.ToInt32(dr[LeaveCode]);
            return Return;
        }

        public void CalculateTotalLeave(DataTable dt, _Worksheet oWorkSheet, int indexRow)
        {
            var _lstTemp = new List<LeaveCal>();
            if (dt.Rows.Count > 0)
                switch (oWorkSheet.Name)
                {
                    case "F":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["F"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["F"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["F"]), CalculateTotal("F", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("F", dt));
                    }
                        break;
                    case "Om":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["Om"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["Om"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Om"]), CalculateTotal("Om", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Om", dt));
                    }
                        break;
                    case "TS":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["TS"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["TS"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["TS"]), CalculateTotal("TS", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("TS", dt));
                    }
                        break;
                    case "Ko":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["Ko"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["Ko"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Ko"]), CalculateTotal("Ko", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Ko", dt));
                    }
                        break;
                    case "Co":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["Co"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["Co"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Co"]), CalculateTotal("Co", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Co", dt));
                    }
                        break;
                    case "OmDN":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["OmDN"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["OmDN"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["OmDN"]), CalculateTotal("OmDN", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("OmDN", dt));
                    }
                        break;
                    case "OmDNBHXH":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) &&
                                (dr["OmDNBHXH"] != DBNull.Value) && (Convert.ToInt32(dr["OmDNBHXH"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["OmDNBHXH"]), CalculateTotal("OmDNBHXH", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("OmDNBHXH", dt));
                    }
                        break;
                    case "KHH":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["KHH"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["KHH"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["KHH"]), CalculateTotal("KHH", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("KHH", dt));
                    }
                        break;
                    case "ST":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["ST"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["ST"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["ST"]), CalculateTotal("ST", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("ST", dt));
                    }
                        break;
                    case "Khamthai":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) &&
                                (dr["Khamthai"] != DBNull.Value) && (Convert.ToInt32(dr["Khamthai"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Khamthai"]), CalculateTotal("Khamthai", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Khamthai", dt));
                    }
                        break;
                    case "TNLD":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["TNLD"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["TNLD"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["TNLD"]), CalculateTotal("TNLD", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("TNLD", dt));
                    }
                        break;
                    case "Fdb":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["Fdb"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["Fdb"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Fdb"]), CalculateTotal("Fdb", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Fdb", dt));
                    }
                        break;
                    case "Ro":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["Ro"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["Ro"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Ro"]), CalculateTotal("Ro", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Ro", dt));
                    }
                        break;
                    case "Diduong":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) &&
                                (dr["Diduong"] != DBNull.Value) && (Convert.ToInt32(dr["Diduong"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Diduong"]), CalculateTotal("Diduong", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Diduong", dt));
                    }
                        break;
                    case "DinhChiCT":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) &&
                                (dr["DinhChiCT"] != DBNull.Value) && (Convert.ToInt32(dr["DinhChiCT"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["DinhChiCT"]), CalculateTotal("DinhChiCT", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("DinhChiCT", dt));
                    }
                        break;
                }
        }

        public void BindCalculateToRow(List<LeaveCal> _lstTemp, _Worksheet oWorkSheet, int indexRow, int Sum)
        {
            foreach (var item in _lstTemp)
                switch (item.Month)
                {
                    case 1:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 6] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 6]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 6] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 6]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 2:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 7] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 7]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 7] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 7]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 3:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 8] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 8]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 8] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 8]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 4:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 9] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 9]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 9] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 9]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 5:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 10] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 10]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 10] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 10]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 6:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 11] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 11]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 11] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 11]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 7:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 12] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 12]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 12] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 12]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 8:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 13] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 13]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 13] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 13]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 9:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 14] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 14]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 14] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 14]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 10:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 15] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 15]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 15] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 15]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 11:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 16] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 16]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 16] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 16]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 12:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 17] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 17]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 17] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 17]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                }
            oWorkSheet.Cells[indexRow, 18] = Sum == 0 ? "-" : Sum.ToString();
            ((Range) oWorkSheet.Cells[indexRow, 18]).HorizontalAlignment = XlHAlign.xlHAlignRight;
        }

        public int CalculateLeave(DataRow dr, string LeaveCode)
        {
            var Return = 0;
            if ((dr != null) && (dr[LeaveCode] != null) && (Convert.ToInt32(dr[LeaveCode]) > 0))
            {
                if (LeaveCode == "Om")
                    LeaveCode = Constants.LEAVE_TYPE_O_BAN_THAN_CODE;
                if (LeaveCode == "OmDN")
                    LeaveCode = Constants.LEAVE_TYPE_O_DAI_NGAY_CODE;
                if (LeaveCode == "TS")
                    LeaveCode = Constants.LEAVE_TYPE_THAI_SAN_CODE;
                if (LeaveCode == "TNLD")
                    LeaveCode = Constants.LEAVE_TYPE_TNLD_CODE;
                if (LeaveCode == "F")
                    LeaveCode = Constants.LEAVE_TYPE_F_NAM_CODE;
                if (LeaveCode == "Fdb")
                    LeaveCode = Constants.LEAVE_TYPE_FDB_CODE;
                if (LeaveCode == "Ro")
                    LeaveCode = Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE;
                if (LeaveCode == "Ko")
                    LeaveCode = Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE;
                if (LeaveCode == "Diduong")
                    LeaveCode = Constants.LEAVE_TYPE_F_DI_DUONG_CODE;
                if (LeaveCode == "Co")
                    LeaveCode = Constants.LEAVE_TYPE_CON_OM_CODE;
                if (LeaveCode == "KHH")
                    LeaveCode = Constants.LEAVE_TYPE_KHHDS_CODE;
                if (LeaveCode == "ST")
                    LeaveCode = Constants.LEAVE_TYPE_SAY_THAI_CODE;
                if (LeaveCode == "Khamthai")
                    LeaveCode = Constants.LEAVE_TYPE_KHAM_THAI_CODE;
                if (LeaveCode == "DinhChiCT")
                    LeaveCode = Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE;
                if (LeaveCode == "OmDNBHXH")
                    LeaveCode = Constants.LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_CODE;
                for (var i = 1; i <= 31; i++)
                    if (dr["Day" + i].ToString().Equals(LeaveCode))
                        Return++;
            }
            return Return;
        }


        private void radButton3_Click_1(object sender, EventArgs e)
        {
            //new frm_Export_Workingday().ShowDialog();
        }

        private void CustomExport_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }
    }

    public class TableName
    {
        public TableName()
        {
        }

        public TableName(string TableNameValue, string TableNameDescription)
        {
            this.TableNameValue = TableNameValue;
            this.TableNameDescription = TableNameDescription;
        }

        public string TableNameValue { get; set; }

        public string TableNameDescription { get; set; }
    }
}