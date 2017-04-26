using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using ADODB;
using ADOX;
using HRMBLL.H0;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Employees
{
    public partial class frm_Import_EmployeeSecurityControl : RadForm
    {
        public static string SelectedTable = string.Empty;
        private readonly string _OldContent = string.Empty;
        private readonly RadWaitingBar _rwb = new RadWaitingBar();
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_Import_EmployeeSecurityControl()
        {
            InitializeComponent();
        }

        private void frm_Import_EmployeeSecurityControl_Load(object sender, EventArgs e)
        {
            radBrowseEditor1.ValueChanged += RadBrowseEditor1_ValueChanged;
        }

        private void RadBrowseEditor1_ValueChanged(object sender, EventArgs e)
        {
            if (radBrowseEditor1.Value.Trim() != string.Empty)
            {
                UseWaitCursor = true;
                Application.DoEvents();

                try
                {
                    var strTables = GetTableExcel(radBrowseEditor1.Value.Trim());

                    var objSelectTable = new ListSheet(this, strTables);
                    objSelectTable.ShowDialog(this);

                    UseWaitCursor = false;

                    objSelectTable.Dispose();

                    if ((SelectedTable != string.Empty) && (SelectedTable != null))
                    {
                        var dt = GetDataTableExcel(radBrowseEditor1.Value.Trim(), SelectedTable);
                        radGridView1.DataSource = dt.DefaultView;
                        Utilities.Utilities.GridFormatting(radGridView1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnImporttvtg_Click(object sender, EventArgs e)
        {
            try
            {
                if (!backgroundWorker1.IsBusy)
                {
                    var fullName = string.Empty;
                    backgroundWorker1.RunWorkerAsync(fullName);

                    Utilities.Utilities.ShowWaiting(_rwb, radGridView1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void Import(string fullName)
        {
            var i = 0;
            foreach (var row in radGridView1.Rows)
                if (Convert.ToInt32(row.Cells["UserId"].Value) > 0)
                {
                    var _UserId = (row.Cells["UserId"].Value == null) || (row.Cells["UserId"].Value == DBNull.Value)
                        ? 0
                        : Convert.ToInt32(row.Cells["UserId"].Value);
                    var _CurrentSCI = (row.Cells["CurrentSCI"].Value == null) ||
                                      (row.Cells["CurrentSCI"].Value == DBNull.Value)
                        ? string.Empty
                        : row.Cells["CurrentSCI"].Value.ToString().Trim().ToUpper();
                    var _StartDate = FormatDate.GetSQLDateMinValue;
                    var _Expired = (row.Cells["Expired"].Value == null) || (row.Cells["Expired"].Value == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(row.Cells["Expired"].Value);
                    var _Area1 = row.Cells["Area1"].Value.ToString().Trim();
                    var _Area2 = row.Cells["Area2"].Value.ToString().Trim();
                    var _Area3 = row.Cells["Area3"].Value.ToString().Trim();
                    var _Area4 = row.Cells["Area4"].Value.ToString().Trim();
                    var _Area5 = row.Cells["Area5"].Value.ToString().Trim();
                    var _Area6 = row.Cells["Area6"].Value.ToString().Trim();
                    var _Remark = row.Cells["Remark"].Value.ToString().Trim();

                    if ((_Expired.Year > 1753) && (_CurrentSCI != string.Empty) && (_UserId > 0))
                    {
                        SecurityControlBLL.Insert(_UserId, _CurrentSCI, _Expired,
                            _Area1, _Area2, _Area3, _Area4, _Area5, _Area6, _Remark, clsGlobal.UserId, _StartDate);
                        _SP = "Ins_H0_SecurityControl";
                        _SPValue =
                            $"UserId: {_UserId}, CurrentSCI: '{_CurrentSCI}', Expired: '{_Expired}', Area1: '{_Area1}', Area2: '{_Area2}', Area3: '{_Area3}', Area4: '{_Area4}', Area5: '{_Area5}', Area6: '{_Area6}', Remark: N'{_Remark}', UpdateBy: {clsGlobal.UserId}, StartDate: '{_StartDate}'";
                        Utilities.Utilities.SaveHRMLog("H0_EmployeeSecurityControl", _SP, _SPValue, _OldContent);
                    }
                    backgroundWorker1.ReportProgress(i + 1, _UserId.ToString());
                    i++;
                }
        }

        public static DataTable GetDataTableExcel(string strFileName, string Table)
        {
            var conn =
                new OleDbConnection(
                    string.Format(
                        "Provider = Microsoft.ACE.OLEDB.12.0; Data Source={0}; Extended Properties = \"Excel 8.0;HDR=Yes;IMEX=1\";",
                        strFileName));
            conn.Open();
            var strQuery = string.Format("SELECT * FROM [{0}]", Table);
            var adapter = new OleDbDataAdapter(strQuery, conn);
            var ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public static string[] GetTableExcel(string strFileName)
        {
            var strTables = new string[100];
            var oCatlog = new Catalog();
            var oTable = new Table();
            var oConn = new Connection();
            oConn.Open(
                string.Format(
                    "Provider = Microsoft.ACE.OLEDB.12.0; Data Source={0}; Extended Properties = \"Excel 8.0;HDR=Yes;IMEX=1\";",
                    strFileName), string.Empty, string.Empty, 0);
            oCatlog.ActiveConnection = oConn;
            if (oCatlog.Tables.Count > 0)
            {
                var item = 0;
                foreach (Table tab in oCatlog.Tables)
                    if (tab.Type == "TABLE")
                    {
                        strTables[item] = tab.Name;
                        item++;
                    }
            }
            return strTables;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var fullName = (string) e.Argument;
            Import(fullName);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
                Utilities.Utilities.SetWaitingText(_rwb, e.ProgressPercentage.ToString());
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Utilities.Utilities.StopWaiting(_rwb, radGridView1);
        }
    }
}