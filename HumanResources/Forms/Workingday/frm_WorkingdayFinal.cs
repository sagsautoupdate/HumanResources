using System;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using Telerik.WinControls.UI;
using HumanResources.Forms.Workingday.Helper;
using Telerik.WinControls;
using System.Drawing;

namespace HumanResources.Forms.Workingday
{
    public partial class frm_WorkingdayFinal : RadForm
    {
        private static frm_WorkingdayFinal s_Instance;
        private int SelectedRowIndex;

        public frm_WorkingdayFinal()
        {
            InitializeComponent();
        }

        public static frm_WorkingdayFinal Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_WorkingdayFinal();
                return s_Instance;
            }
        }

        private void WorkingdayFinal_Load(object sender, EventArgs e)
        {
            radDateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            toolWindow1.Text = "Danh sách Phòng ban";
            toolWindow1.AutoHide();
            documentWindow1.Text = "DANH SÁCH NGÀY CÔNG HƯỞNG LƯƠNG";

            MaximizeBox = false;
            MinimizeBox = false;

            Utilities.Utilities.PopulateRootLevel(radTreeView1);
            radTreeView1.Nodes[0].Selected = true;
            radTreeView1.Nodes[0].Expand();

            InitData();
            Utilities.Utilities.GridFormatting(radGridView1);
            if (radGridView1.Rows.Count > 0) radGridView1.Rows[0].IsCurrent = true; radGridView1.Rows[0].IsSelected = true;

            FormClosed += WorkingdayFinal_FormClosed;
            radGridView1.FilterChanged += RadGridView1_FilterChanged;
            rmiCheck.Click += RmiCheck_Click;
            rmiOpen.Click += RmiOpen_Click;
            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            radGridView1.CellDoubleClick += RadGridView1_CellDoubleClick;
            radGridView1.GroupSummaryEvaluate += RadGridView1_GroupSummaryEvaluate;
            radTreeView1.SelectedNodeChanged += RadTreeView1_SelectedNodeChanged;
            rmiERROR.Click += RmiERROR_Click;
            radGridView1.CellFormatting += RadGridView1_CellFormatting;
        }

        private void RadGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "CheckRemark")
            {
                string _ERROR = "";
                try
                {
                    _ERROR = e.CellElement.RowInfo.Cells["CheckRemark"].Value.ToString().Replace(Environment.NewLine, "");
                }
                catch { }
                var _strError = "";
                var _str = _ERROR.Split(',');
                foreach (var item in _str)
                {
                    if (item.Length > 0 && Convert.ToInt32(item) > -1)
                        _strError += new WE().GetErrorNameById(Convert.ToInt32(item)) + Environment.NewLine;// Utilities.Utilities.GetEnumDescription((WORKDAYERROR)Convert.ToInt32(item)) + Environment.NewLine;
                }
                e.CellElement.Text = _strError;
            }
            if (e.CellElement.ColumnInfo.Name == "OmDNBHXH"
                || e.CellElement.ColumnInfo.Name == "Om"
                || e.CellElement.ColumnInfo.Name == "OmDN"
                || e.CellElement.ColumnInfo.Name == "KHH"
                || e.CellElement.ColumnInfo.Name == "Co"
                || e.CellElement.ColumnInfo.Name == "TS"
                || e.CellElement.ColumnInfo.Name == "ST"
                || e.CellElement.ColumnInfo.Name == "Khamthai"
                || e.CellElement.ColumnInfo.Name == "TNLD"
                || e.CellElement.ColumnInfo.Name == "F"
                || e.CellElement.ColumnInfo.Name == "Diduong"
                || e.CellElement.ColumnInfo.Name == "CTac"
                || e.CellElement.ColumnInfo.Name == "Fdb"
                || e.CellElement.ColumnInfo.Name == "H1"
                || e.CellElement.ColumnInfo.Name == "H2"
                || e.CellElement.ColumnInfo.Name == "H3"
                || e.CellElement.ColumnInfo.Name == "H4"
                || e.CellElement.ColumnInfo.Name == "H5"
                || e.CellElement.ColumnInfo.Name == "H6"
                || e.CellElement.ColumnInfo.Name == "H7"
                || e.CellElement.ColumnInfo.Name == "DinhChiCT"
                || e.CellElement.ColumnInfo.Name == "Ro"
                || e.CellElement.ColumnInfo.Name == "Ko")
            {
                if (e.CellElement.Value != null)
                    if (Convert.ToInt32(e.CellElement.Value) > 0)
                    {
                        e.CellElement.DrawFill = true;
                        e.CellElement.BackColor = Color.FromArgb(107, 174, 239);
                    }
                    else
                    {
                        e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                    }
            }
            if (e.Row is GridViewDataRowInfo)
            {
                e.CellElement.ToolTipText = e.CellElement.Text;
            }
        }

        private void RmiERROR_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            var _fewd = new frm_ERROR_WorkingDay(this, radDateTimePicker1.Value, string.Empty);
            Utilities.Utilities.SetScreenColor(_fewd);
            _fewd.ShowDialog();
            Cursor.Current = Cursors.Default;
        }


        public void InitData()
        {
            var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
            var objD = new DepartmentsBLL();
            objD.GetAllChildId(deptSelected);
            var departmentIds = objD.ChildNodeIds;

            radGridView1.DataSource =
                WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay(
                    Convert.ToInt32(radDateTimePicker1.Value.Month), Convert.ToInt32(radDateTimePicker1.Value.Year),
                    clsGlobal.SalaryIsVCQLNN, departmentIds, 9999, Constants.DataType_Run);
            Utilities.Utilities.GridFormatting(radGridView1);
            radLabelElement1.Text = "Tổng cộng: " + Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
        }


        private void RadTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            InitData();
        }

        private void RadGridView1_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.SummaryItem.Name == "ParentId")
                e.FormatString = string.Format("{0}", DepartmentsBLL.GetById(Convert.ToInt32(e.Value)).DepartmentName);
        }

        private void RadGridView1_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewRowInfo)
            {
                if (e.Row is GridViewFilteringRowInfo || e.Row is GridViewSearchRowInfo || e.Row is GridViewTableHeaderRowInfo)
                {

                }
                else
                {
                    Cursor.Current = Cursors.AppStarting;

                    SelectedRowIndex = radGridView1.CurrentRow.Index;
                    var fwfd = new frm_WorkingdayFinalDetail(this,
                        Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                        radGridView1.CurrentRow.Cells["FullName"].Value.ToString(), radDateTimePicker1.Value);
                    Utilities.Utilities.SetScreenColor(fwfd);

                    Cursor.Current = Cursors.Default;
                    fwfd.ShowDialog();
                }
            }
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
            if (cell == null)
            {
                return;
            }
            if (cell is GridFilterCellElement )
            {
                //e.ContextMenu = null;
                //e.Cancel = true;
            }
            else
            {
                e.ContextMenu = radContextMenu1.DropDown;
            }
        }

        private void RmiOpen_Click(object sender, EventArgs e)
        {
            if (radGridView1.CurrentRow is GridViewRowInfo)
            {
                Cursor.Current = Cursors.AppStarting;

                SelectedRowIndex = radGridView1.CurrentRow.Index;
                var fwfd = new frm_WorkingdayFinalDetail(this,
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString(), radDateTimePicker1.Value);
                Utilities.Utilities.SetScreenColor(fwfd);

                Cursor.Current = Cursors.Default;
                fwfd.ShowDialog();
            }
        }

        private void RmiCheck_Click(object sender, EventArgs e)
        {
            var cs = new frm_CheckSend(this, 0, radDateTimePicker1.Value);

            cs.ShowDialog();
        }

        private void RadGridView1_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            radLabelElement1.Text = "Tổng cộng: " + Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
        }

        private void WorkingdayFinal_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        public void radButton1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;

            var dt =
                WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay(
                    Convert.ToInt32(radDateTimePicker1.Value.Month), Convert.ToInt32(radDateTimePicker1.Value.Year),
                    clsGlobal.SalaryIsVCQLNN, string.Empty, 9999, Constants.DataType_Run);
            radGridView1.DataSource = dt;
            Utilities.Utilities.GridFormatting(radGridView1);
            radLabelElement1.Text = "Tổng cộng: " + Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);

            Cursor.Current = Cursors.Default;

            var tableElement = radGridView1.CurrentView as GridTableElement;
            var row = radGridView1.Rows[SelectedRowIndex];

            if ((tableElement != null) && (row != null))
            {
                row.IsSelected = true;
                row.IsCurrent = true;
            }
        }

        private void radGridView1_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                if (e.CellElement.ColumnInfo.Name == "Lamdem")
                    e.CellElement.ColumnInfo.HeaderText = "Lam\r\ndem";
                if (e.CellElement.ColumnInfo.Name == "Khamthai")
                    e.CellElement.ColumnInfo.HeaderText = "Kham\r\nthai";
                if (e.CellElement.ColumnInfo.Name == "OmDNBHXH")
                    e.CellElement.ColumnInfo.HeaderText = "OmDN\r\nBHXH";
                if (e.CellElement.ColumnInfo.Name == "Diduong")
                    e.CellElement.ColumnInfo.HeaderText = "Di\r\nduong";
                if (e.CellElement.ColumnInfo.Name == "DinhChiCT")
                    e.CellElement.ColumnInfo.HeaderText = "Dinh\r\nChi\r\nCT";
            }
        }

        private void radGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo.Cells["WDStatus"].Value != null)
            {
                if (e.RowElement.RowInfo.Cells["WDStatus"].Value.ToString().Equals("0"))
                {
                    e.RowElement.DrawFill = true;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                    e.RowElement.BackColor = Color.LightCoral;
                }
                else if (e.RowElement.RowInfo.Cells["WDStatus"].Value.ToString().Equals("3"))
                {
                    e.RowElement.DrawFill = true;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                    e.RowElement.BackColor = Color.FromArgb(192, 255, 192);
                }
                else if (e.RowElement.RowInfo.Cells["WDStatus"].Value.ToString().Equals("2"))
                {
                    e.RowElement.DrawFill = true;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                    e.RowElement.BackColor = Color.NavajoWhite;
                }
                else
                {
                    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                }
            }
            if (e.RowElement.IsCurrent)
            {
                e.RowElement.DrawFill = true;
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = Color.FromArgb(255, 178, 0);
            }
        }

        private void radGridView1_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {

        }
    }
}