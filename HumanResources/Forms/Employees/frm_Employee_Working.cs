using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMUtil;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using StringFormat = HRMUtil.StringFormat;

namespace HumanResources.Forms.Employees
{
    public partial class frm_Employee_Working : RadForm
    {
        private static frm_Employee_Working s_Instance;

        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();


        private int currRow;
        private string DataSource;
        private string ImageLink = string.Empty;

        public frm_Employee_Working()
        {
            InitializeComponent();
            Utilities.Utilities.PopulateRootLevel(radTreeView2);
        }

        public static frm_Employee_Working Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Employee_Working();
                return s_Instance;
            }
        }

        private void frm_Employee_Working_Load(object sender, EventArgs e)
        {
            _rcm.Items["rmiAdd"].Enabled = false;
            _rcm.Items["rmiDelete"].Enabled = false;
            _rcm.Items["rmiEdit"].Click += Frm_Employee_Working_Click;
            _rcm.Items["rmiRefresh"].Click += Frm_Employee_Working_Click1;

            DataSource = Utilities.Utilities.GetServerByKeyWithoutDecrypt(clsGlobal.ConnectionString);
            switch (clsGlobal.Server)
            {
                case "Server_SAGS":
                    if (DataSource.Equals("10.10.55.5"))
                        DataSource = "10.10.55.5";
                    ImageLink = string.Format("http://{0}/HRM/Employee/Images/", DataSource);
                    break;
                case "Server_DAD":
                    if (DataSource.Equals("172.16.234.5"))
                        DataSource = "172.16.234.5";
                    ImageLink = string.Format("http://{0}/HRM/Employee/Images/", DataSource);
                    break;
                case "Server_CXR":
                    if (DataSource.Equals("172.16.112.5"))
                        DataSource = "172.16.112.8";
                    ImageLink = string.Format("http://{0}/HRM/Employee/Images/", DataSource);
                    break;
            }

            var list = EmployeesBLL.GetByDeptIdsToDT(string.Empty, 0, string.Empty, "0");
            radGridView1.DataSource = list;

            var Count = clsGlobal.EmployeeImageList.Count;
            if (list.Rows.Count == Count)
                foreach (var item in radGridView1.Rows)
                {
                    var _ImageName = StringFormat.GetUserCode(int.Parse(item.Cells["UserId"].Value.ToString()));
                    try
                    {
                        item.Cells["Picture"].Value =
                            clsGlobal.EmployeeImageList.Find(x => x.ImageName == _ImageName).Image;
                    }
                    catch
                    {
                    }
                }
            Utilities.Utilities.GridFormatting(radGridView1);

            documentWindow2.Text = "SAGS";//DepartmentsBLL.GetById(int.Parse(radTreeView2.SelectedNode.Value.ToString()))
                //.DepartmentFullName.Trim();
            toolWindow2.AutoHide();
            radLabelElement1.Text = "Tổng số nhân viên: " + radGridView1.ChildRows.Count;

            radGridView1.CellFormatting += RadGridView1_CellFormatting;
            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            FormClosed += Frm_Employee_Working_FormClosed;
            radGridView1.RowFormatting += RadGridView1_RowFormatting;
            radTreeView2.SelectedNodeChanged += RadTreeView2_SelectedNodeChanged;
            radGridView1.FilterChanged += RadGridView1_FilterChanged;
        }


        public void ViewData()
        {
            Cursor.Current = Cursors.AppStarting;

            Utilities.Utilities.GridFormatting(radGridView1);
            var list = EmployeesBLL.GetByDeptIdsToDT(string.Empty, 0, string.Empty, "0");
            radGridView1.DataSource = list;

            foreach (var item in radGridView1.ChildRows)
            {
                var _ImageName = StringFormat.GetUserCode(int.Parse(item.Cells["UserId"].Value.ToString()));
                try
                {
                    item.Cells["Picture"].Value = clsGlobal.EmployeeImageList.Find(x => x.ImageName == _ImageName).Image;
                }
                catch
                {
                }
            }

            var tableElement = radGridView1.CurrentView as GridTableElement;
            var row = radGridView1.Rows[currRow];

            if ((tableElement != null) && (row != null))
            {
                row.IsSelected = true;
                row.IsCurrent = true;
            }
            Cursor.Current = Cursors.Default;
        }


        private void Frm_Employee_Working_Click1(object sender, EventArgs e)
        {
            ViewData();
        }

        private void Frm_Employee_Working_Click(object sender, EventArgs e)
        {
            if (!(radGridView1.CurrentRow is GridViewTableHeaderRowInfo))
                currRow = radGridView1.CurrentRow.Index;
            Cursor.Current = Cursors.AppStarting;
            var _fed = new frm_Employee_Detail_B1024(this,
                Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                radGridView1.CurrentRow.Cells["FullName"].Value.ToString());
            Cursor.Current = Cursors.Default;
            _fed.ShowDialog();
        }

        private void RadGridView1_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            radLabelElement1.Text = "Tổng số nhân viên: " + Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
        }

        private void RadTreeView2_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            if (radTreeView2.SelectedNode != null)
            {
                Cursor.Current = Cursors.AppStarting;

                var deptSelected = int.Parse(radTreeView2.SelectedNode.Value.ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;

                var list = EmployeesBLL.GetByDeptIdsToDT(departmentIds, 0, string.Empty, "0");
                radGridView1.DataSource = list;

                foreach (var item in radGridView1.ChildRows)
                {
                    var _ImageName = StringFormat.GetUserCode(int.Parse(item.Cells["UserId"].Value.ToString()));
                    try
                    {
                        item.Cells["Picture"].Value =
                            clsGlobal.EmployeeImageList.Find(x => x.ImageName == _ImageName).Image;
                    }
                    catch
                    {
                    }
                }

                Utilities.Utilities.GridFormatting(radGridView1);
                radLabelElement1.Text = "Tổng số nhân viên: " +
                                        Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
                documentWindow2.Text = DepartmentsBLL.GetById(int.Parse(radTreeView2.SelectedNode.Value.ToString()))
                    .DepartmentFullName.Trim();
                Cursor.Current = Cursors.Default;
            }
        }

        private void RadGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            e.RowElement.DrawFill = true;
            if (e.RowElement.IsCurrent)
            {
                e.RowElement.BackColor = Color.FromArgb(255, 178, 0);
            }
            else
            {
                e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
            }

            if (e.RowElement.RowInfo is GridViewTableHeaderRowInfo)
            {
                e.RowElement.RowInfo.MinHeight = 35;
                e.RowElement.RowInfo.MaxHeight = 35;
            }
            else
            {
                e.RowElement.RowInfo.MinHeight = 70;
                e.RowElement.RowInfo.MaxHeight = 70;
            }
        }

        private void Frm_Employee_Working_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }

        private void RadGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            var columnName = e.Column.Name;
            var value = e.Row.Cells[columnName].Value;

            if (columnName == "Sex")
                e.CellElement.Text = Equals(value, 1) ? "Nam" : "Nữ";
            if (e.CellElement is GridDateTimeCellElement)
                e.CellElement.Text = (e.CellElement.Value == null) || (e.CellElement.Value.ToString() == string.Empty)
                    ? FormatDate.GetSQLDateMinValue.ToString("dd/MM/yyyy")
                    : Convert.ToDateTime(e.CellElement.Value).ToString("dd/MM/yyyy");
        }

        private Image DownloadImage(string _URL)
        {
            Image _tmpImage = null;
            try
            {
                var _HttpWebRequest = (HttpWebRequest) WebRequest.Create(_URL);
                _HttpWebRequest.AllowWriteStreamBuffering = true;

                _HttpWebRequest.Timeout = 20000;

                var _WebResponse = _HttpWebRequest.GetResponse();

                var _WebStream = _WebResponse.GetResponseStream();

                _tmpImage = Image.FromStream(_WebStream);

                _WebResponse.Close();
                _WebResponse.Close();
            }
            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}", _Exception);
                return null;
            }
            return _tmpImage;
        }

        private void radGridView1_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            _rcm.Items["rmiEdit"].PerformClick();
        }
    }
}