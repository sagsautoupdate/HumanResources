using System;
using System.Data;
using System.Windows.Forms;
using HumanResources.Forms.Bonus;
using HumanResources.Forms.Employees;
using HumanResources.Forms.Salary;
using HumanResources.Forms.Workingday;
using Telerik.WinControls.UI;

namespace HumanResources
{
    public partial class ListSheet : RadForm
    {
        private readonly frm_Import_EmployeeSecurityControl _fiesc;

        private readonly frm_ImportBonus _objBN;

        private readonly frm_ImportWorkingday _objIW;
        private readonly bool DataTables;
        private readonly DataTable dtTable = new DataTable();
        private readonly string[] Tables;
        private readonly frm_ImportSalary _objSL;

        public ListSheet()
        {
            InitializeComponent();
        }

        public ListSheet(frm_ImportBonus objBN, DataTable dt)
        {
            InitializeComponent();
            dtTable = dt;
            DataTables = true;
            _objBN = objBN;
        }

        public ListSheet(frm_Import_EmployeeSecurityControl fiesc, string[] StrTable)
        {
            InitializeComponent();
            Tables = StrTable;
            _fiesc = fiesc;
        }

        public ListSheet(frm_ImportSalary objSL, string[] StrTable)
        {
            InitializeComponent();
            Tables = StrTable;
            _objSL = objSL;
        }

        public ListSheet(frm_ImportSalary objSL, DataTable dt)
        {
            InitializeComponent();
            dtTable = dt;
            DataTables = true;
            _objSL = objSL;
        }


        public ListSheet(frm_ImportWorkingday objIW, DataTable dt)
        {
            InitializeComponent();
            dtTable = dt;
            DataTables = true;
            _objIW = objIW;
        }


        private void Select_Tables_Load()
        {
            if (!DataTables)
            {
                if (Tables != null)
                    for (var tables = 0; tables < Tables.Length; tables++)
                        try
                        {
                            var lv = new ListViewItem();
                            lv.Text = Tables[tables];
                            lv.Tag = tables;
                            rlstListSheet.Items.Add(lv.Text);
                        }
                        catch (Exception ex)
                        {
                        }
            }
            else
            {
                if (dtTable.Rows.Count > 0)
                    for (var tables = 0; tables < dtTable.Rows.Count; tables++)
                        try
                        {
                            var lv = new ListViewItem();
                            lv.Text = dtTable.Rows[tables][0].ToString();
                            lv.Tag = dtTable.Rows[tables][0];
                            rlstListSheet.Items.Add(lv.Text);
                        }
                        catch (Exception ex)
                        {
                        }
            }
        }

        private void rbtnOK_Click(object sender, EventArgs e)
        {
            Ok_Click();
        }

        private void Ok_Click()
        {
            if (rlstListSheet.Items.Count > 0)
                if (rlstListSheet.SelectedItem != null)
                {
                    if (_fiesc != null)
                    {
                        frm_Import_EmployeeSecurityControl.SelectedTable = rlstListSheet.SelectedItem.Text;
                    }


                    else
                    {
                        if (_objIW != null)
                        {
                            frm_ImportWorkingday.SelectedTable = rlstListSheet.SelectedItem.Text;
                        }


                        else
                        {
                            if (_objBN != null)
                            {
                                frm_ImportBonus.SelectedTable = rlstListSheet.SelectedItem.Text;
                            }
                            else
                            {
                                if (_objSL != null)
                                    frm_ImportSalary.SelectedTable = rlstListSheet.SelectedItem.Text;
                            }
                        }
                    }


                    Close();
                }
                else
                {
                    MessageBox.Show("Select a sheet");
                }
            else
                Close();
        }


        private void ListSheet_Load(object sender, EventArgs e)
        {
            Select_Tables_Load();
        }

        private void rlstListSheet_DoubleClick(object sender, EventArgs e)
        {
            Ok_Click();
        }
    }
}