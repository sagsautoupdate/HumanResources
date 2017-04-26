using System;
using System.Windows.Forms;
using HRMBLL.H1;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Salary
{
    public partial class frm_ListWageFund : RadForm
    {
        private static frm_ListWageFund s_Instance;

        public frm_ListWageFund()
        {
            InitializeComponent();
        }

        public static frm_ListWageFund Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_ListWageFund();
                return s_Instance;
            }
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

        private void BindDataToDDLSalaryTableType()
        {
            rddlSalaryTableType.DisplayMember = "UnitName";
            rddlSalaryTableType.ValueMember = "UnitId";
            rddlSalaryTableType.DataSource = Constants.GetAllVCQLNN(true);
        }


        private void BindDataGrid(int yearLoad)
        {
            if ((rddlYears.SelectedValue != null) && (rddlMonths.SelectedValue != null))
            {
                var month = int.Parse(rddlMonths.SelectedValue.ToString());
                var year = yearLoad;
                if (yearLoad == 0)
                    year = int.Parse(rddlYears.SelectedValue.ToString());
                var dt = WageFundsBLL.GetByDate(month, year);
                rgvWageFundList.DataSource = dt;
                GridFormatting();
            }
        }

        private void GridFormatting()
        {
            rgvWageFundList.Columns[0].FormatString = StringFormat.FormatMonthYear;
            rgvWageFundList.Columns[1].FormatString = StringFormat.FormatCoefficient3DigitGrid;
            rgvWageFundList.Columns[2].FormatString = StringFormat.FormatCurrencyFinal;
            rgvWageFundList.Columns[3].FormatString = StringFormat.FormatCurrencyFinal;
            rgvWageFundList.Columns[4].FormatString = StringFormat.FormatCurrencyFinal;
            rgvWageFundList.Columns[5].FormatString = StringFormat.FormatCurrencyFinal;
            rgvWageFundList.Columns[6].FormatString = StringFormat.FormatCurrencyFinal;
            rgvWageFundList.Columns[7].FormatString = StringFormat.FormatCurrencyFinal;
            rgvWageFundList.Columns[8].FormatString = StringFormat.FormatCurrencyFinal;
        }

        private void ListWageFund_Load(object sender, EventArgs e)
        {
            BindDataToDropDownListYear();
            BindDataToDDLSalaryTableType();
            BindDataGrid(DateTime.Now.Year);

            FormClosed += Frm_ListWageFund_FormClosed;
        }

        private void Frm_ListWageFund_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void rbtnCalculateWagefund_Click(object sender, EventArgs e)
        {
            var month = int.Parse(rddlMonths.SelectedValue.ToString());
            var year = int.Parse(rddlYears.SelectedValue.ToString());
            var isvcqlnn = int.Parse(rddlSalaryTableType.SelectedValue.ToString());
            var dlg = new frm_CalculateWageFund(month, year, isvcqlnn);
            dlg.ShowDialog(this);
            BindDataGrid(0);
        }


        private void rbtnGetData_Click(object sender, EventArgs e)
        {
            BindDataGrid(0);
        }
    }
}