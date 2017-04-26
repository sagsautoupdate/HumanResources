using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;
using StringFormat = HRMUtil.StringFormat;

namespace HumanResources.Forms.Salary
{
    public partial class frm_ListIncomeEmployees : RadForm
    {
        private static frm_ListIncomeEmployees s_Instance;
        private readonly FillPrimitive fill;
        public DateTime _Date;
        public string _departmentIds;
        public int _isVCQLNN;
        private RadContextMenu contextMenu;
        public RadButtonElement rbtnCalculateSalary = null;
        public DataTable Temp;

        public frm_ListIncomeEmployees()
        {
            InitializeComponent();
            fill = (FillPrimitive) rpnlHeaderText.PanelElement.Children[0];
            fill.BackColor2 = Color.FromArgb(255, 250, 214);
            fill.BackColor = Color.FromArgb(255, 228, 127);
            fill.GradientStyle = GradientStyles.Linear;
        }

        public static frm_ListIncomeEmployees Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_ListIncomeEmployees();
                return s_Instance;
            }
        }

        private void BindDataToDDLSalaryTableType()
        {
            rddlSalaryTableType.DisplayMember = "UnitName";
            rddlSalaryTableType.ValueMember = "UnitId";
            rddlSalaryTableType.DataSource = Constants.GetAllVCQLNN(true);
        }

        private void BindDataToDropDownListYear()
        {
            rddlMonths.DisplayMember = "UnitName";
            rddlMonths.ValueMember = "UnitId";
            rddlMonths.DataSource = Constants.GetAllMonths();

            rddlYears.DisplayMember = "UnitName";
            rddlYears.ValueMember = "UnitId";
            rddlYears.DataSource = Constants.GetAllYears();

            rddlYears.SelectedText = DateTime.Now.Year.ToString();
        }

        private void PopulateRootLevel()
        {
            var list = DepartmentsBLL.GetDepartmentRoot();
            PopulateNodes(list, rtrvDepartment.Nodes);
            rtrvDepartment.Nodes[0].Expand();
        }

        private void PopulateNodes(List<DepartmentsBLL> list, RadTreeNodeCollection nodes)
        {
            foreach (var obj in list)
            {
                var tn = new RadTreeNode();
                tn.Text = obj.DepartmentName;
                tn.Value = obj.DepartmentId.ToString();
                tn.ImageIndex = obj.Level;

                nodes.Add(tn);


                if (obj.ChildNodeCount > 0)
                {
                    PopulateSubLevel(obj.DepartmentId, tn);
                    tn.Font = new Font(rtrvDepartment.Font, FontStyle.Bold);
                }
            }
        }

        private void PopulateSubLevel(int parentid, RadTreeNode parentNode)
        {
            var list = DepartmentsBLL.GetAll_SubLevel(parentid);
            PopulateNodes(list, parentNode.Nodes);
        }

        private void ListIncomeEmployees_Load(object sender, EventArgs e)
        {
            FormClosed += Frm_ListIncomeEmployees_FormClosed;
            BindDataToDDLSalaryTableType();
            BindDataToDropDownListYear();
            PopulateRootLevel();
            var dtTemp = DateTime.Now.AddMonths(-1);
            rddlMonths.SelectedValue = dtTemp.Month;
            rddlYears.SelectedValue = dtTemp.Year;

            rtrvDepartment.Nodes[0].Selected = true;


            contextMenu = new RadContextMenu();
            var menuContextFilter = new RadMenuItem("Mở chức năng filter");
            menuContextFilter.ForeColor = Color.Red;
            menuContextFilter.CheckOnClick = true;
            menuContextFilter.Click += menuContextFilter_Click;

            var menuContextFilter1 = new RadMenuItem("In bảng lương");
            menuContextFilter1.ForeColor = Color.Red;
            menuContextFilter1.CheckOnClick = true;
            menuContextFilter1.Click += menuContextFilter1_Click;

            contextMenu.Items.Add(menuContextFilter);
            contextMenu.Items.Add(menuContextFilter1);
        }

        private void Frm_ListIncomeEmployees_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void menuContextFilter_Click(object sender, EventArgs e)
        {
            var menuitem = (RadMenuItem) sender;
            rgwListIncome.EnableFiltering = menuitem.IsChecked;
        }

        private void menuContextFilter1_Click(object sender, EventArgs e)
        {
        }

        private void BindDataToGrid()
        {
            if (rtrvDepartment.SelectedNode != null)
            {
                clsGlobal.SalaryDataDate = new DateTime(int.Parse(rddlYears.SelectedValue.ToString()),
                    int.Parse(rddlMonths.SelectedValue.ToString()), 1);

                var isVCQLNN = int.Parse(rddlSalaryTableType.SelectedValue.ToString());
                clsGlobal.SalaryIsVCQLNN = isVCQLNN;

                var deptSelected = int.Parse(rtrvDepartment.SelectedNode.Value.ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;
                var list = IncomeEmployeesBLL.GetDataTableByDataDate(clsGlobal.SalaryDataDate, isVCQLNN, departmentIds);
                rgwListIncome.DataSource = list;

                Temp = list;
                _Date = new DateTime(int.Parse(rddlYears.SelectedValue.ToString()),
                    int.Parse(rddlMonths.SelectedValue.ToString()), 1);
                _isVCQLNN = int.Parse(rddlSalaryTableType.SelectedValue.ToString());
                _departmentIds = objD.ChildNodeIds;

                GridFormatting();
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
            rgwListIncome.Columns[17].FormatString = StringFormat.FormatCurrency;
            rgwListIncome.Columns[18].FormatString = StringFormat.FormatCurrency;
        }


        private void rtrvDepartment_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            BindDataToGrid();
        }

        private void ListIncomeEmployees_FormClosed(object sender, FormClosedEventArgs e)
        {
            rbtnCalculateSalary.Enabled = false;
        }

        private void rbtnView_Click(object sender, EventArgs e)
        {
            BindDataToGrid();
        }

        private void rgwListIncome_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = contextMenu.DropDown;
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            var _fcl = new frm_Compare_ListIncomeEmployees();
            _fcl.ShowDialog();
        }
    }
}