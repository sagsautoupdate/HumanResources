using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using HumanResources.Properties;
using HumanResources.Utilities;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Leave
{
    public partial class frm_Leave_Management : RadForm
    {
        private static frm_Leave_Management s_Instance;

        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();

        public frm_Leave_Management()
        {
            InitializeComponent();

            toolWindow1.Text = "Danh sách Phòng ban";

            documentWindow1.Text = "DANH SÁCH NGÀY PHÉP";

            if (clsGlobal.IsAdmin || clsGlobal.IsHRAdmin)
                Utilities.Utilities.PopulateRootLevel(radTreeView1);
            else
                PopulateRootLevel(radTreeView1);
        }

        public static frm_Leave_Management Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Leave_Management();
                return s_Instance;
            }
        }

        private void Form_Leave_Management_Load(object sender, EventArgs e)
        {
            AddMenu();
            InitUI();
            ddlMonth.SelectedValue = DateTime.Now.Month;
            ddlYear.SelectedValue = DateTime.Now.Year;

            InitData();
            Utilities.Utilities.GridFormatting(rGVEmployeeLeave);
            Utilities.Utilities.GridTemplateFormatting(gridViewTemplate2);


            radTreeView1.SelectedNodeChanged += RadTreeView1_SelectedNodeChanged;

            FormClosed += Form_Leave_Management_FormClosed;

            rGVEmployeeLeave.ViewCellFormatting += RGVEmployeeLeave_ViewCellFormatting;
            rGVEmployeeLeave.FilterPopupRequired += RGVEmployeeLeave_FilterPopupRequired;
            rGVEmployeeLeave.RowFormatting += RGVEmployeeLeave_RowFormatting;
            rGVEmployeeLeave.CellFormatting += RGVEmployeeLeave_CellFormatting;
            rGVEmployeeLeave.ContextMenuOpening += RGVEmployeeLeave_ContextMenuOpening;
            rGVEmployeeLeave.ToolTipTextNeeded += RGVEmployeeLeave_ToolTipTextNeeded;
            rGVEmployeeLeave.FilterChanged += RGVEmployeeLeave_FilterChanged;

            _rcm.Items["rmiAdd"].Click += RmiAdd_Click;
            _rcm.Items["rmiEdit"].Click += RmiEdit_Click;
            _rcm.Items["rmiDelete"].Enabled = false;
            _rcm.Items["rmiRefresh"].Click += RmiRefresh_Click;


            radLabelElement1.Text = $"Tổng cộng: {rGVEmployeeLeave.ChildRows.Count}";
            rGVEmployeeLeave.MasterView.SummaryRows[0].MinHeight = 30;
        }


        public void AddMenu()
        {
            var _rmsi = new RadMenuSeparatorItem();
            _rcm.Items.Add(_rmsi);


            var _rmiHistory = new RadMenuItem
            {
                Text = "Lịch sử",
                Image = Resources.History,
                Name = "rmiHistory"
            };
            _rmiHistory.Click += _rmiHistory_Click;
            _rcm.Items.Add(_rmiHistory);
        }

        private void _rmiHistory_Click(object sender, EventArgs e)
        {
            if (!(rGVEmployeeLeave.CurrentRow is GridViewSearchRowInfo) ||
                !(rGVEmployeeLeave.CurrentRow is GridViewFilteringRowInfo))
                try
                {
                    Cursor.Current = Cursors.AppStarting;
                    var UserId = Convert.ToInt32(rGVEmployeeLeave.CurrentRow.Cells["UserId"].Value);
                    var FullName = rGVEmployeeLeave.CurrentRow.Cells["FullName"].Value.ToString();

                    var frmLeave = new frm_Leave(UserId, FullName, Convert.ToInt32(ddlYear.SelectedValue), "His");
                    frmLeave.ShowDialog();
                    Cursor.Current = Cursors.Default;
                }
                catch
                {
                }
                finally
                {
                }
        }

        public void PopulateRootLevel(RadTreeView radTreeView)
        {
            var list =
                DepartmentsBLL.GetByRoot(
                    int.Parse(EmployeesBLL.DR_GetEmployeeById(clsGlobal.UserId)["RootId"].ToString()));

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

        private void InitUI()
        {
            BS_LeaveType.DataSource = Constants.GetAllLeaveCode();
            ddlLeaveType.DisplayMember = "RTypeName";
            ddlLeaveType.ValueMember = "RTypeId";

            BS_Month.DataSource = Constants.GetAllMonths();
            ddlMonth.DisplayMember = "UnitName";
            ddlMonth.ValueMember = "UnitId";

            BS_Year.DataSource = Constants.GetAllYears();
            ddlYear.DisplayMember = "UnitName";
            ddlYear.ValueMember = "UnitId";
        }

        private void InitData()
        {
            if (clsGlobal.IsAdmin || clsGlobal.IsHRAdmin)
            {
                BS_EmployeeLeave.DataSource = EmployeeLeaveBLL.DT_GetByDeptId_Total(string.Empty,
                    Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                gridViewTemplate2.DataSource = EmployeeLeaveBLL.DT_GetByUserId_Date(0, 0,
                    Convert.ToInt32(ddlYear.SelectedValue));
            }
            else
            {
                var deptSelected = int.Parse(EmployeesBLL.DR_GetEmployeeById(clsGlobal.UserId)["RootId"].ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;
                BS_EmployeeLeave.DataSource = EmployeeLeaveBLL.DT_GetByDeptId_Total(departmentIds,
                    Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                gridViewTemplate2.DataSource = EmployeeLeaveBLL.DT_GetByUserId_Date(0, 0,
                    Convert.ToInt32(ddlYear.SelectedValue));
            }
        }

        public void ViewData()
        {
            try
            {
                Cursor.Current = Cursors.AppStarting;

                if (clsGlobal.IsAdmin || clsGlobal.IsHRAdmin)
                {
                    BS_EmployeeLeave.DataSource = EmployeeLeaveBLL.DT_GetByDeptId_Total(string.Empty,
                        Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                    gridViewTemplate2.DataSource = EmployeeLeaveBLL.DT_GetByUserId_Date(0, 0,
                        Convert.ToInt32(ddlYear.SelectedValue));
                }
                else
                {
                    var deptSelected = int.Parse(EmployeesBLL.DR_GetEmployeeById(clsGlobal.UserId)["RootId"].ToString());
                    var objD = new DepartmentsBLL();
                    objD.GetAllChildId(deptSelected);
                    var departmentIds = objD.ChildNodeIds;
                    BS_EmployeeLeave.DataSource = EmployeeLeaveBLL.DT_GetByDeptId_Total(departmentIds,
                        Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                    gridViewTemplate2.DataSource = EmployeeLeaveBLL.DT_GetByUserId_Date(0, 0,
                        Convert.ToInt32(ddlYear.SelectedValue));
                }
                Cursor.Current = Cursors.Default;
            }
            catch
            {
            }
            finally
            {
            }
        }

        private string CalculateData(int userId, int leaveType)
        {
            var ReturnStr = string.Empty;

            var _Month = Convert.ToInt32(ddlMonth.SelectedValue);
            var _Year = Convert.ToInt32(ddlYear.SelectedValue);

            var dt = WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDayAll(1, _Year, string.Empty,
                userId, 12,
                _Year);
            if (dt != null)
            {
                var drF = dt.Compute("SUM(F)", null);
                var drOm = dt.Compute("SUM(Om)", null);
                var drKo = dt.Compute("SUM(Ko)", null);
                var drTS = dt.Compute("SUM(TS)", null);
                var drCo = dt.Compute("SUM(Co)", null);

                var SumF = 0;
                try
                {
                    SumF = Convert.ToInt32(drF);
                }
                catch
                {
                    SumF = 0;
                }
                var MaxF = 0;
                try
                {
                    MaxF = Convert.ToInt32(dt.Rows[0]["MaxFCurrent"]);
                }
                catch
                {
                    MaxF = 0;
                }

                var SumOm = 0;
                try
                {
                    SumOm = Convert.ToInt32(drOm);
                }
                catch
                {
                    SumOm = 0;
                }
                var MaxOm = 0;
                try
                {
                    MaxOm = Convert.ToInt32(dt.Rows[0]["Om"]);
                }
                catch
                {
                    MaxOm = 0;
                }

                var SumKo = 0;
                try
                {
                    SumKo = Convert.ToInt32(drKo);
                }
                catch
                {
                    SumKo = 0;
                }

                var SumTS = 0;
                try
                {
                    SumTS = Convert.ToInt32(drTS);
                }
                catch
                {
                    SumTS = 0;
                }
                var SumCo = 0;
                try
                {
                    SumCo = Convert.ToInt32(drCo);
                }
                catch
                {
                    SumCo = 0;
                }

                foreach (DataRow dr in dt.Rows)
                    switch (leaveType)
                    {
                        case 1:
                        {
                            foreach (var item in FindLeave("Om", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                        }
                            break;
                        case 5:
                        {
                            foreach (var item in FindLeave("F", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                        }
                            break;
                        case 8:
                        {
                            foreach (var item in FindLeave("Ko", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                        }
                            break;
                        case 3:
                        {
                            foreach (var item in FindLeave("TS", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                        }
                            break;
                        case 18:
                        {
                            foreach (var item in FindLeave("Co", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                        }
                            break;
                        case 9999:
                        {
                            foreach (var item in FindLeave("OmDNBHXH", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("OmDN", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("KHH", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("ST", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("Khamthai", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("TNLD", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("Diduong", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("CTac", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("Fdb", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("H1", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("H2", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("H3", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("H4", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("H5", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("H6", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("H7", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("DinhChiCT", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                            foreach (var item in FindLeave("Ro", dr))
                                if (item.ToString() != "")
                                    ReturnStr += string.Format("{0}: {1} tháng {2} năm {3}\r\n", item.LeaveCode,
                                        item.LeaveReturn1, Convert.ToDateTime(dr["DataDate"]).Month,
                                        Convert.ToDateTime(dr["DataDate"]).Year);
                        }
                            break;
                        default:
                            ReturnStr += "";
                            break;
                    }
            }
            else
            {
                ReturnStr = string.Empty;
            }

            return ReturnStr;
        }

        private List<LeaveReturn> FindLeave(string LeaveCode, DataRow dr)
        {
            var _lstTemp = new List<LeaveReturn>();
            var iFirst = 0;
            var iLast = 0;
            if ((dr != null) && (dr[LeaveCode] != null) && (Convert.ToInt32(dr[LeaveCode]) > 0))
                for (var i = 1; i <= 31; i++)
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
                    if (LeaveCode == "CTac")
                        LeaveCode = Constants.LEAVE_TYPE_F_CONG_TAC_CODE;
                    if (LeaveCode == "H1")
                        LeaveCode = Constants.LEAVE_TYPE_HOC_1_CODE;
                    if (LeaveCode == "H2")
                        LeaveCode = Constants.LEAVE_TYPE_HOC_2_CODE;
                    if (LeaveCode == "H3")
                        LeaveCode = Constants.LEAVE_TYPE_HOC_3_CODE;
                    if (LeaveCode == "H4")
                        LeaveCode = Constants.LEAVE_TYPE_HOC_4_CODE;
                    if (LeaveCode == "H5")
                        LeaveCode = Constants.LEAVE_TYPE_HOC_5_CODE;
                    if (LeaveCode == "H6")
                        LeaveCode = Constants.LEAVE_TYPE_HOC_6_CODE;
                    if (LeaveCode == "H7")
                        LeaveCode = Constants.LEAVE_TYPE_HOC_7_CODE;
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
                    if (dr["Day" + i].ToString().Equals(LeaveCode))
                    {
                        var iNext = i + 1;
                        iFirst = i;
                        iLast = i;

                        while ((iNext <= 31) && dr["Day" + i].ToString().Equals(dr["Day" + iNext].ToString()))
                        {
                            iLast = iNext;
                            iNext++;
                        }

                        if ((iFirst > 0) && (iFirst == iLast))
                        {
                            _lstTemp.Add(new LeaveReturn(LeaveCode, iFirst.ToString()));
                        }
                        else
                        {
                            if ((iFirst > 0) && (iLast > 0))
                            {
                                _lstTemp.Add(new LeaveReturn(LeaveCode, string.Format("{0} - {1}", iFirst, iLast)));
                                i = iLast;
                            }
                        }
                    }
                }
            return _lstTemp;
        }


        private void RGVEmployeeLeave_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            radLabelElement1.Text = $"Tổng cộng: {rGVEmployeeLeave.ChildRows.Count}";
        }

        private void RGVEmployeeLeave_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            var toolTip = rGVEmployeeLeave.ElementTree.ComponentTreeHandler.Behavior.ToolTip;
            toolTip.AutoPopDelay = 999999999;

            var cell = sender as GridDataCellElement;
            if ((cell != null) &&
                ((cell.ColumnInfo.Name == "MaxFCurrent") || (cell.ColumnInfo.Name == "FLeft") ||
                 (cell.ColumnInfo.Name == "FTimes") || (cell.ColumnInfo.Name == "FRemain")))
                if (CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 5) == string.Empty)
                    e.ToolTipText = "Chưa có thông tin chấm công";
                else
                    e.ToolTipText = CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 5);
            if ((cell != null) &&
                ((cell.ColumnInfo.Name == "MaxOm") || (cell.ColumnInfo.Name == "OmLeft") ||
                 (cell.ColumnInfo.Name == "OmTimes") || (cell.ColumnInfo.Name == "OmRemain")))
                if (CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 1) == string.Empty)
                    e.ToolTipText = "Chưa có thông tin chấm công";
                else
                    e.ToolTipText = CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 1);
            if ((cell != null) && ((cell.ColumnInfo.Name == "KoLeft") || (cell.ColumnInfo.Name == "KoTimes")))
                if (CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 8) == string.Empty)
                    e.ToolTipText = "Chưa có thông tin chấm công";
                else
                    e.ToolTipText = CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 8);
            if ((cell != null) && ((cell.ColumnInfo.Name == "TSLeft") || (cell.ColumnInfo.Name == "TSTimes")))
                if (CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 3) == string.Empty)
                    e.ToolTipText = "Chưa có thông tin chấm công";
                else
                    e.ToolTipText = CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 3);
            if ((cell != null) && ((cell.ColumnInfo.Name == "CoLeft") || (cell.ColumnInfo.Name == "CoTimes")))
                if (CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 18) == string.Empty)
                    e.ToolTipText = "Chưa có thông tin chấm công";
                else
                    e.ToolTipText = CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 18);
            if ((cell != null) && (cell.ColumnInfo.Name == "Other"))
                if (CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 9999) == string.Empty)
                    e.ToolTipText = "Chưa có thông tin chấm công";
                else
                    e.ToolTipText = CalculateData(Convert.ToInt32(cell.RowInfo.Cells["UserId"].Value), 9999);
        }

        private void RadTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            if (radTreeView1.SelectedNode != null)
            {
                Cursor.Current = Cursors.AppStarting;

                var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;

                if (clsGlobal.IsAdmin || clsGlobal.IsHRAdmin)
                    try
                    {
                        BS_EmployeeLeave.DataSource = EmployeeLeaveBLL.DT_GetByDeptId_Total(departmentIds,
                            Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                        gridViewTemplate2.DataSource = EmployeeLeaveBLL.DT_GetByUserId_Date(0, 0,
                            Convert.ToInt32(ddlYear.SelectedValue));
                    }
                    catch
                    {
                    }
                else
                    try
                    {
                        BS_EmployeeLeave.DataSource = EmployeeLeaveBLL.DT_GetByDeptId_Total(departmentIds,
                            Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                        gridViewTemplate2.DataSource = EmployeeLeaveBLL.DT_GetByUserId_Date(0, 0,
                            Convert.ToInt32(ddlYear.SelectedValue));
                    }
                    catch
                    {
                    }
                Cursor.Current = Cursors.Default;
                documentWindow1.Text = DepartmentsBLL.GetById(int.Parse(radTreeView1.SelectedNode.Value.ToString()))
                    .DepartmentFullName.Trim();
                radLabelElement1.Text = $"Tổng cộng: {rGVEmployeeLeave.ChildRows.Count}";
            }
        }

        private void RmiRefresh_Click(object sender, EventArgs e)
        {
            ViewData();
        }

        private void RmiDelete_Click(object sender, EventArgs e)
        {
        }

        private void RmiEdit_Click(object sender, EventArgs e)
        {
            if (!(rGVEmployeeLeave.CurrentRow is GridViewSearchRowInfo) ||
                !(rGVEmployeeLeave.CurrentRow is GridViewFilteringRowInfo))
                try
                {
                    Cursor.Current = Cursors.AppStarting;
                    var UserId = Convert.ToInt32(rGVEmployeeLeave.CurrentRow.Cells["UserId"].Value);
                    var FullName = rGVEmployeeLeave.CurrentRow.Cells["FullName"].Value.ToString();
                    var EmpLeaveId = Convert.ToInt32(rGVEmployeeLeave.CurrentRow.Cells["EmployeeLeaveId"].Value);

                    var frmLeave = new frm_Leave(UserId, FullName, Convert.ToInt32(ddlYear.SelectedValue), "Edt",
                        EmpLeaveId);
                    frmLeave.ShowDialog();
                    Cursor.Current = Cursors.Default;
                }
                catch
                {
                }
                finally
                {
                }
        }

        private void RmiAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.AppStarting;
                var f_NewLeaveDay = frm_Leave.Instance;
                f_NewLeaveDay.ShowDialog();
                Cursor.Current = Cursors.Default;
            }
            catch
            {
            }
            finally
            {
            }
        }

        private void RGVEmployeeLeave_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }

        private void RGVEmployeeLeave_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ViewTemplate.Parent == null)
                if (e.CellElement.Value != null)
                    if (e.CellElement.Value.ToString().Equals("0"))
                        e.CellElement.Text = string.Empty;
        }

        private void RGVEmployeeLeave_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo is GridViewRowInfo)
                e.RowElement.RowInfo.MinHeight = 35;
        }

        private void RGVEmployeeLeave_FilterPopupRequired(object sender, FilterPopupRequiredEventArgs e)
        {
            if (e.FilterPopup is RadListFilterPopup)
                e.FilterPopup = new MyListFilterPopup(e.Column);
        }

        private void RGVEmployeeLeave_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ViewTemplate.Parent == null)
            {
                if (e.CellElement.ColumnInfo != null)
                    if ((e.CellElement.ColumnInfo.Name == "MaxFCurrent") || (e.CellElement.ColumnInfo.Name == "FLeft") ||
                        (e.CellElement.ColumnInfo.Name == "FRemain") || (e.CellElement.ColumnInfo.Name == "FTimes"))
                    {
                        e.CellElement.ForeColor = Color.FromArgb(0x00, 0x00, 0xFF);
                        e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
                        e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                    }
                    else
                    {
                        if ((e.CellElement.ColumnInfo.Name == "MaxOm") || (e.CellElement.ColumnInfo.Name == "OmLeft") ||
                            (e.CellElement.ColumnInfo.Name == "OmRemain") ||
                            (e.CellElement.ColumnInfo.Name == "OmTimes"))
                        {
                            e.CellElement.ForeColor = Color.FromArgb(0x00, 0x66, 0x00);
                            e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
                            e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                        }
                        else
                        {
                            if ((e.CellElement.ColumnInfo.Name == "KoLeft") ||
                                (e.CellElement.ColumnInfo.Name == "KoTimes"))
                            {
                                e.CellElement.ForeColor = Color.FromArgb(255, 0, 251);
                                e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
                                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                            }
                            else
                            {
                                if ((e.CellElement.ColumnInfo.Name == "TSLeft") ||
                                    (e.CellElement.ColumnInfo.Name == "TSTimes"))
                                {
                                    e.CellElement.ForeColor = Color.FromArgb(255, 119, 0);
                                    e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
                                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                                }
                                else
                                {
                                    if ((e.CellElement.ColumnInfo.Name == "CoLeft") ||
                                        (e.CellElement.ColumnInfo.Name == "CoTimes"))
                                    {
                                        e.CellElement.ForeColor = Color.FromArgb(140, 0, 255);
                                        e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
                                        e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                                    }
                                    else
                                    {
                                        if ((e.CellElement.ColumnInfo.Name == "Other") ||
                                            (e.CellElement.ColumnInfo.Name == "OtherTimes"))
                                        {
                                            e.CellElement.ForeColor = Color.Black;
                                            e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
                                            e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                                        }
                                        else
                                        {
                                            e.CellElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
                                            e.CellElement.ResetValue(VisualElement.ForeColorProperty,
                                                ValueResetFlags.Local);
                                        }
                                    }
                                }
                            }
                        }
                    }
            }
            else
            {
                e.CellElement.BackColor = Color.FromArgb(0xE4, 0xF3, 0xEA);
                e.CellElement.BackColor2 = Color.FromArgb(0xE4, 0xF3, 0xEA);
                e.CellElement.BackColor3 = Color.FromArgb(0xE4, 0xF3, 0xEA);
                e.CellElement.BackColor4 = Color.FromArgb(0xE4, 0xF3, 0xEA);
                e.CellElement.DrawFill = true;
            }
        }

        private void Form_Leave_Management_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void rBView_Click(object sender, EventArgs e)
        {
            ViewData();
        }
    }

    public class LeaveReturn
    {
        public LeaveReturn(string LeaveCode, string LeaveReturn)
        {
            this.LeaveCode = LeaveCode;
            LeaveReturn1 = LeaveReturn;
        }

        public LeaveReturn()
        {
        }

        public string LeaveCode { get; set; }

        public string LeaveReturn1 { get; set; }
    }
}