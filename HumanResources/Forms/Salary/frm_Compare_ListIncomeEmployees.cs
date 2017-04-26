using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Salary
{
    public partial class frm_Compare_ListIncomeEmployees : RadForm
    {
        public frm_Compare_ListIncomeEmployees()
        {
            InitializeComponent();

            Utilities.Utilities.SetScreenColor(this);
        }

        private void frm_Compare_ListIncomeEmployees_Load(object sender, EventArgs e)
        {
            radGridView1.EnableFiltering = true;
            radGridView1.MasterTemplate.EnableFiltering = true;

            Utilities.Utilities.PopulateRootLevel(radTreeView2);
            radTreeView2.Nodes[0].Selected = true;
            InitData();

            documentWindow1.Text = string.Empty;
        }

        private void InitData()
        {
            radDropDownList1.DataSource = Constants.GetAllMonths();
            radDropDownList1.DisplayMember = "UnitName";
            radDropDownList1.ValueMember = "UnitId";

            radDropDownList3.DataSource = Constants.GetAllMonths();
            radDropDownList3.DisplayMember = "UnitName";
            radDropDownList3.ValueMember = "UnitId";

            radDropDownList2.DataSource = Constants.GetAllYears();
            radDropDownList2.DisplayMember = "UnitName";
            radDropDownList2.ValueMember = "UnitId";

            radDropDownList4.DataSource = Constants.GetAllYears();
            radDropDownList4.DisplayMember = "UnitName";
            radDropDownList4.ValueMember = "UnitId";
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
        }

        private void GetData()
        {
            Cursor.Current = Cursors.AppStarting;
            try
            {
                var deptSelected = int.Parse(radTreeView2.SelectedNode.Value.ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;

                var dt1 = new DateTime(Convert.ToInt32(radDropDownList2.SelectedValue),
                    Convert.ToInt32(radDropDownList1.SelectedValue), 1);
                var dt2 = new DateTime(Convert.ToInt32(radDropDownList4.SelectedValue),
                    Convert.ToInt32(radDropDownList3.SelectedValue), 1);
                var dt = WorkdayCoefficientEmployeesFinalBLL.GetWorkdayIncome(dt1, dt2, 0, departmentIds);
                radGridView1.DataSource = dt;
                Utilities.Utilities.GridFormatting(radGridView1);

                radLabel7.Text =
                    DepartmentsBLL.GetById(int.Parse(radTreeView2.SelectedNode.Value.ToString()))
                        .DepartmentFullName.Trim();
            }
            catch (Exception ex)
            {
            }

            Cursor.Current = Cursors.Default;
        }

        private string GetDataRow(DataTable datatable, string condition, string result)
        {
            var sb = new StringBuilder();
            var sampleResults = from DataRow sampleRow in datatable.AsEnumerable()
                where sampleRow.Field<int>("UserId") == Convert.ToInt32(condition)
                select sampleRow;

            Parallel.ForEach(sampleResults, sampleRow =>
            {
                var sval = sampleRow[result].ToString();
                sb.Append(sval);
            });
            if (sb.ToString() == string.Empty)
                return "0";
            return sb.ToString();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            GetData();
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if ((e.CellElement.ColumnInfo.Name == "T1") || (e.CellElement.ColumnInfo.Name == "T2") ||
                (e.CellElement.ColumnInfo.Name == "T3"))
            {
                e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
            }
            else
            {
                if ((e.CellElement.ColumnInfo.Name == "HSK1") || (e.CellElement.ColumnInfo.Name == "HSK2") ||
                    (e.CellElement.ColumnInfo.Name == "HSK3"))
                {
                    e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
                }
                else
                {
                    if ((e.CellElement.ColumnInfo.Name == "HSLNS1") || (e.CellElement.ColumnInfo.Name == "HSLNS2") ||
                        (e.CellElement.ColumnInfo.Name == "HSLNS3"))
                    {
                        e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
                    }
                    else
                    {
                        e.CellElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
                    }
                }
            }
        }

        private void radTreeView2_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            if (radTreeView2.SelectedNode != null)
            {
                Cursor.Current = Cursors.AppStarting;

                GetData();

                Cursor.Current = Cursors.Default;
            }
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void radGridView1_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ViewTemplate.Parent == null)
            {
                if (e.CellElement.ColumnInfo != null)
                    if ((e.CellElement.ColumnInfo.Name == "T1") || (e.CellElement.ColumnInfo.Name == "T2") ||
                        (e.CellElement.ColumnInfo.Name == "T3"))
                    {
                        e.CellElement.ForeColor = Color.FromArgb(0x00, 0x00, 0xFF);
                        e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                    }
                    else
                    {
                        if ((e.CellElement.ColumnInfo.Name == "HSLNS1") || (e.CellElement.ColumnInfo.Name == "HSLNS2") ||
                            (e.CellElement.ColumnInfo.Name == "HSLNS3"))
                        {
                            e.CellElement.ForeColor = Color.FromArgb(0x00, 0x66, 0x00);
                            e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                        }
                        else
                        {
                            if ((e.CellElement.ColumnInfo.Name == "HSK1") || (e.CellElement.ColumnInfo.Name == "HSK2") ||
                                (e.CellElement.ColumnInfo.Name == "HSK3"))
                            {
                                e.CellElement.ForeColor = Color.Coral;
                                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                            }
                            else
                            {
                                e.CellElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
                                e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
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
    }
}