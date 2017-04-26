using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HRMBLL.H0;
using HumanResources.Forms.Contract.SubContract;
using HumanResources.Properties;
using HumanResources.Reports.Report_Preview;
using Telerik.WinControls.UI;
using StringFormat = HRMUtil.StringFormat;
using ValueChangedEventArgs = Telerik.WinControls.UI.Data.ValueChangedEventArgs;

namespace HumanResources.Forms.Contract.Contract
{
    public partial class frm_List_Contract : RadForm
    {
        private static frm_List_Contract s_Instance;
        private readonly RadContextMenu _rcmm = Utilities.Utilities.DefaultRadContextMenu();
        private readonly DataTable dtAllEmp = DepartmentEmployeeBLL.GetByStatus(1);

        private readonly DataTable dtEffective = EmployeeContractBLL.GetAllToDT();
        private int _departmentMenuId;
        private int _IsContractType;
        private DataTable dtSubContract = EmployeeSubContractBLL.GetAll();

        public frm_List_Contract()
        {
            InitializeComponent();

            AddMenu();
        }

        public static frm_List_Contract Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_List_Contract();
                return s_Instance;
            }
        }

        private void ContractList_Load(object sender, EventArgs e)
        {
            InitData();
            Utilities.Utilities.GridFormatting(rgvEmployeeContract);
            
            rgvEmployeeContract.CellFormatting += RgvEmployeeContract_CellFormatting;
            rgvEmployeeContract.CellDoubleClick += RgvEmployeeContract_CellDoubleClick;
            FormClosed += ContractList_FormClosed;
            rgvEmployeeContract.RowFormatting += RgvEmployeeContract_RowFormatting;
            rgvEmployeeContract.ViewCellFormatting += RgvEmployeeContract_ViewCellFormatting;
            rgvEmployeeContract.ContextMenuOpening += rgvEmployeeContract_ContextMenuOpening;

            _rcmm.Items["rmiRefresh"].Click += RmiRefresh_Click;
        }


        public void AddMenu()
        {
            _rcmm.Items.Remove(_rcmm.Items["rmiAdd"]);
            var _rmiAddContract = new RadMenuItem
            {
                Text = "Thêm mới",
                Image = Resources.Add,
                Name = "rmiAdd"
            };
            var _rmiAddOne = new RadMenuItem
            {
                Text = "Thêm mới",
                Image = Resources.One,
                Name = "rmiAddOne"
            };
            _rmiAddOne.Click += _rmiAddOne_Click;
            _rmiAddContract.Items.Add(_rmiAddOne);
            var _rmiAddGroup = new RadMenuComboItem
            {
                Text = "Thêm mới theo phòng",
                Image = Resources.Nine,
                Name = "rmiAddGroup"
            };
            _rmiAddGroup.ComboBoxElement.DataSource = DepartmentsBLL.GetDTAllDepartments();
            _rmiAddGroup.ComboBoxElement.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
            _rmiAddGroup.ComboBoxElement.DropDownWidth = 250;
            _rmiAddGroup.ComboBoxElement.DisplayMember = "DepartmentFullName";
            _rmiAddGroup.ComboBoxElement.ValueMember = "DepartmentId";
            _rmiAddGroup.ComboBoxElement.SelectedValue = 0;
            _rmiAddGroup.ComboBoxElement.SelectedValueChanged += ComboBoxElement_SelectedValueChanged;
            _rmiAddContract.Items.Add(_rmiAddGroup);

            var _rmbi = new RadMenuButtonItem
            {
                Text = "OK",
                Name = "rmiOK"
            };
            _rmbi.Click += RmiOK_Click;
            _rmiAddContract.Items.Add(_rmbi);
            _rcmm.Items.Insert(0, _rmiAddContract);

            _rcmm.Items.Remove(_rcmm.Items["rmiEdit"]);
            var _rmiEdit = new RadMenuItem
            {
                Text = "Chỉnh sửa",
                Image = Resources.Edit,
                Name = "rmiEdit"
            };
            var _rmiEditContract = new RadMenuItem
            {
                Text = "Hợp đồng lao động",
                Image = Resources.Edit,
                Name = "rmiEditContract"
            };
            _rmiEditContract.Click += _rmiEditContract_Click;
            _rmiEdit.Items.Add(_rmiEditContract);


            _rcmm.Items.Insert(1, _rmiEdit);


            var _rmsi = new RadMenuSeparatorItem();
            _rcmm.Items.Add(_rmsi);


            var _rmiHistory = new RadMenuItem
            {
                Text = "Lịch sử",
                Image = Resources.History,
                Name = "rmiHistory"
            };
            var _rmiHistoryContract = new RadMenuItem
            {
                Text = "Lịch sử HĐLĐ",
                Image = Resources.History,
                Name = "rmiHistoryContract"
            };
            _rmiHistoryContract.Click += _rmiHistoryContract_Click;
            _rmiHistory.Items.Add(_rmiHistoryContract);
            var _rmiHistorySubContract = new RadMenuItem
            {
                Text = "Lịch sử PLHĐ",
                Image = Resources.History,
                Name = "rmiHistorySubContract"
            };
            _rmiHistorySubContract.Click += _rmiHistorySubContract_Click;
            _rmiHistory.Items.Add(_rmiHistorySubContract);
            _rcmm.Items.Add(_rmiHistory);


            _rmsi = new RadMenuSeparatorItem();
            _rcmm.Items.Add(_rmsi);


            var _rmiContract = new RadMenuItem
            {
                Text = "Hợp đồng lao động",
                Image = Resources.File,
                Name = "rmiContract"
            };
            var _rmiPreviewContract = new RadMenuItem
            {
                Text = "Xem hợp đồng",
                Image = Resources.Preview,
                Name = "rmiPreviewContract"
            };
            _rmiPreviewContract.Click += _rmiPreviewContract_Click;
            _rmiContract.Items.Add(_rmiPreviewContract);
            var _rmiPrintContract = new RadMenuItem
            {
                Text = "In hợp đồng",
                Image = Resources.File,
                Name = "rmiPrintContract"
            };
            _rmiPrintContract.Click += _rmiPrintContract_Click;
            _rmiContract.Items.Add(_rmiPrintContract);
            _rcmm.Items.Add(_rmiContract);
        }


        private void _rmiEditContract_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            try
            {
                if (!(rgvEmployeeContract.CurrentRow is GridViewTableHeaderRowInfo))
                {
                    var _FullName = string.Empty;
                    var _UserId = 0;
                    if (rgvEmployeeContract.CurrentRow.ViewTemplate.Parent != null)
                    {
                        _FullName =
                            (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["FullName"].Value.ToString();
                        _UserId =
                            Convert.ToInt32(
                                (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["UserId"].Value
                                    .ToString());
                    }
                    else
                    {
                        _FullName = rgvEmployeeContract.CurrentRow.Cells["FullName"].Value.ToString();
                        _UserId = Convert.ToInt32(rgvEmployeeContract.CurrentRow.Cells["UserId"].Value);
                    }
                    {
                        Cursor.Current = Cursors.AppStarting;

                        var f_NewContract = new frm_Add_EmployeeContract(this,
                            _UserId,
                            _FullName, 0);
                        Cursor.Current = Cursors.Default;
                        f_NewContract.ShowDialog();
                    }
                }
            }
            catch
            {
            }
            Cursor.Current = Cursors.Default;
        }

        public void InitData()
        {
            Utilities.Utilities.GridFormatting(rgvEmployeeContract);
            
            rgvEmployeeContract.DataSource = dtEffective;
            
            radLabelElement2.Text = string.Format("Đang hiệu lực ({0})", dtEffective.Rows.Count);
            radLabelElement3.Text = string.Format("Đến hạn ({0})", CalculateExpired());
            radLabelElement4.Text = string.Format("Chuyển đổi ({0})", CalculateChanged());
            radLabelElement1.Text = "Tổng số nhân viên: " + rgvEmployeeContract.ChildRows.Count;
        }

        public void RefreshData()
        {
            Cursor.Current = Cursors.AppStarting;
            switch (_IsContractType)
            {
                case 0:
                    rgvEmployeeContract.DataSource = DepartmentEmployeeBLL.GetByStatus(1);
                    break;
                case 1:
                    rgvEmployeeContract.DataSource = EmployeeContractBLL.RemindExpiredConstractsToDT("", "",
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), 0);
                    break;
                case 2:
                    rgvEmployeeContract.DataSource = EmployeeContractBLL.ChangedConstractsToDT("", 0, DateTime.Now.Month,
                        DateTime.Now.Year, 0);
                    break;
            }
            foreach (var item in rgvEmployeeContract.Rows)
            {
                var _ImageName = StringFormat.GetUserCode(int.Parse(item.Cells["UserId"].Value.ToString()));
                if (clsGlobal.EmployeeImageList.Exists(x => x.ImageName == _ImageName))
                    if (_ImageName == clsGlobal.EmployeeImageList.Find(x => x.ImageName == _ImageName).ImageName)
                        item.Cells["Picture"].Value =
                            clsGlobal.EmployeeImageList.Find(x => x.ImageName == _ImageName).Image;
            }

            Utilities.Utilities.GridFormatting(rgvEmployeeContract);
            gvtContract.DataSource = EmployeeContractBLL.GetAllToDT();
            gvtSubContract.DataSource = EmployeeSubContractBLL.GetAll();
            Utilities.Utilities.GridFormatting(rgvEmployeeContract);
            Utilities.Utilities.GridTemplateFormatting(gvtContract);
            Utilities.Utilities.GridTemplateFormatting(gvtSubContract);


            radLabelElement2.Text = string.Format("Đang hiệu lực ({0})", dtEffective.Rows.Count);
            radLabelElement3.Text = string.Format("Đến hạn ({0})", CalculateExpired());
            radLabelElement4.Text = string.Format("Chuyển đổi ({0})", CalculateChanged());
            radLabelElement1.Text = "Tổng số nhân viên: " + rgvEmployeeContract.ChildRows.Count;

            Cursor.Current = Cursors.Default;
        }


        private void _rmiPrintContract_Click(object sender, EventArgs e)
        {
            try
            {
                var _FullName = string.Empty;
                var _UserId = 0;
                if (rgvEmployeeContract.CurrentRow.ViewTemplate.Parent != null)
                {
                    _FullName =
                        (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["FullName"].Value.ToString();
                    _UserId =
                        Convert.ToInt32(
                            (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["UserId"].Value.ToString());
                }
                else
                {
                    _FullName = rgvEmployeeContract.CurrentRow.Cells["FullName"].Value.ToString();
                    _UserId = Convert.ToInt32(rgvEmployeeContract.CurrentRow.Cells["UserId"].Value);
                }

                ReportPreview rp = null;

                if (rgvEmployeeContract.CurrentRow.ViewTemplate == rgvEmployeeContract.MasterTemplate.Templates[0])
                {
                    var _EmployeeContractId =
                        Convert.ToInt32(rgvEmployeeContract.MasterTemplate.CurrentRow.Cells["EmployeeContractId"].Value);
                    rp = new ReportPreview(this,
                        _EmployeeContractId,
                        _UserId,
                        _FullName, "Prt");
                }

                else
                {
                    if (rgvEmployeeContract.CurrentRow.ViewTemplate == rgvEmployeeContract.MasterTemplate.Templates[1])
                    {
                        var dr = EmployeeSubContractBLL.GetAllByUserIdByActive(_UserId.ToString());
                        rp = new ReportPreview(this,
                            Convert.ToInt32(dr["EmployeeSubContractId"]),
                            _UserId,
                            _FullName, "Prt", "SubContract");
                    }
                    else
                    {
                        var _EmployeeContractId =
                            Convert.ToInt32(
                                EmployeeContractBLL.DR_GetActiveContractByUserId(_UserId)["EmployeeContractId"]);

                        rp = new ReportPreview(this,
                            _EmployeeContractId,
                            _UserId,
                            _FullName, "Prt");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Crystal report missing");
            }
        }

        private void _rmiPreviewContract_Click(object sender, EventArgs e)
        {
            try
            {
                var _FullName = string.Empty;
                var _UserId = 0;
                if (rgvEmployeeContract.CurrentRow.ViewTemplate.Parent != null)
                {
                    _FullName =
                        (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["FullName"].Value.ToString();
                    _UserId =
                        Convert.ToInt32(
                            (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["UserId"].Value.ToString());
                }
                else
                {
                    _FullName = rgvEmployeeContract.CurrentRow.Cells["FullName"].Value.ToString();
                    _UserId = Convert.ToInt32(rgvEmployeeContract.CurrentRow.Cells["UserId"].Value);
                }

                ReportPreview rp = null;

                if (rgvEmployeeContract.CurrentRow.ViewTemplate == rgvEmployeeContract.MasterTemplate.Templates[0])
                {
                    var _EmployeeContractId =
                        Convert.ToInt32(rgvEmployeeContract.MasterTemplate.CurrentRow.Cells["EmployeeContractId"].Value);
                    rp = new ReportPreview(this,
                        _EmployeeContractId,
                        _UserId,
                        _FullName, "Pre");
                }

                else
                {
                    if (rgvEmployeeContract.CurrentRow.ViewTemplate == rgvEmployeeContract.MasterTemplate.Templates[1])
                    {
                        var dr = EmployeeSubContractBLL.GetAllByUserIdByActive(_UserId.ToString());

                        rp = new ReportPreview(this,
                            Convert.ToInt32(dr["EmployeeSubContractId"]),
                            _UserId,
                            _FullName, "Pre", "SubContract");
                    }
                    else
                    {
                        var _EmployeeContractId =
                            Convert.ToInt32(
                                EmployeeContractBLL.DR_GetActiveContractByUserId(_UserId)["EmployeeContractId"]);

                        rp = new ReportPreview(this,
                            _EmployeeContractId,
                            _UserId,
                            _FullName, "Pre");
                    }
                }
                rp.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Crystal report missing");
            }
        }

        private void _rmiHistorySubContract_Click(object sender, EventArgs e)
        {
            if (!(rgvEmployeeContract.CurrentRow is GridViewTableHeaderRowInfo))
            {
                var _FullName = string.Empty;
                var _UserId = 0;
                if (rgvEmployeeContract.CurrentRow.ViewTemplate.Parent != null)
                {
                    _FullName =
                        (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["FullName"].Value.ToString();
                    _UserId =
                        Convert.ToInt32(
                            (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["UserId"].Value.ToString());
                }
                else
                {
                    _FullName = rgvEmployeeContract.CurrentRow.Cells["FullName"].Value.ToString();
                    _UserId = Convert.ToInt32(rgvEmployeeContract.CurrentRow.Cells["UserId"].Value);
                }
                {
                    Cursor.Current = Cursors.AppStarting;

                    var sch = new frm_SubContractHistory(this,
                        _UserId,
                        _FullName);
                    Cursor.Current = Cursors.Default;
                    sch.ShowDialog();
                }
            }
        }

        private void _rmiHistoryContract_Click(object sender, EventArgs e)
        {
            if (!(rgvEmployeeContract.CurrentRow is GridViewTableHeaderRowInfo))
            {
                var _FullName = string.Empty;
                var _UserId = 0;
                if (rgvEmployeeContract.CurrentRow.ViewTemplate.Parent != null)
                {
                    _FullName =
                        (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["FullName"].Value.ToString();
                    _UserId =
                        Convert.ToInt32(
                            (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["UserId"].Value.ToString());
                }
                else
                {
                    _FullName = rgvEmployeeContract.CurrentRow.Cells["FullName"].Value.ToString();
                    _UserId = Convert.ToInt32(rgvEmployeeContract.CurrentRow.Cells["UserId"].Value);
                }
                {
                    Cursor.Current = Cursors.AppStarting;

                    var f_NewContract = new frm_Add_EmployeeContract(this,
                        _UserId,
                        _FullName, 1);
                    Cursor.Current = Cursors.Default;
                    f_NewContract.ShowDialog();
                }
            }
        }

        private void _rmiAddOne_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;

            var f_NewContract = frm_Add_EmployeeContract.Instance;
            Cursor.Current = Cursors.Default;
            f_NewContract.ShowDialog();
        }

        private void ComboBoxElement_SelectedValueChanged(object sender, ValueChangedEventArgs e)
        {
            _departmentMenuId = Convert.ToInt32(e.NewValue);
        }

        private void RgvEmployeeContract_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ViewTemplate.Parent != null)
            {
                e.CellElement.BackColor = Color.FromArgb(0xE4, 0xF3, 0xEA);
                e.CellElement.BackColor2 = Color.FromArgb(0xE4, 0xF3, 0xEA);
                e.CellElement.BackColor3 = Color.FromArgb(0xE4, 0xF3, 0xEA);
                e.CellElement.BackColor4 = Color.FromArgb(0xE4, 0xF3, 0xEA);
                e.CellElement.DrawFill = true;
            }
        }

        private void RgvEmployeeContract_RowFormatting(object sender, RowFormattingEventArgs e)
        {
        }

        private void RmiOK_Click(object sender, EventArgs e)
        {
            var objBLL = DepartmentsBLL.GetById(_departmentMenuId);
            if (
                MessageBox.Show("Thêm hợp đồng cho phòng " + objBLL.DepartmentFullName.ToUpper(), "?",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (_departmentMenuId > 0)
                {
                    Cursor.Current = Cursors.AppStarting;

                    var f_NewContract = new frm_Add_EmployeeContract(this, _departmentMenuId);
                    Utilities.Utilities.SetScreenColor(f_NewContract);

                    Cursor.Current = Cursors.Default;
                    f_NewContract.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Cancel", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RgvEmployeeContract_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (!(e.Row is GridViewTableHeaderRowInfo))
                if (e.Row.ViewTemplate is GridViewTemplate && (e.Row.ViewTemplate.Parent != null))
                {
                    var _FullName = string.Empty;
                    var _UserId = 0;
                    if (rgvEmployeeContract.CurrentRow.ViewTemplate.Parent != null)
                    {
                        _FullName =
                            (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["FullName"].Value.ToString();
                        _UserId =
                            Convert.ToInt32(
                                (rgvEmployeeContract.CurrentRow.Parent as GridViewRowInfo).Cells["UserId"].Value
                                    .ToString());
                    }
                    else
                    {
                        _FullName = rgvEmployeeContract.CurrentRow.Cells["FullName"].Value.ToString();
                        _UserId = Convert.ToInt32(rgvEmployeeContract.CurrentRow.Cells["UserId"].Value);
                    }


                    if (e.Row.ViewTemplate == rgvEmployeeContract.MasterTemplate.Templates[0])
                    {
                        Cursor.Current = Cursors.AppStarting;

                        var f_NewContract = new frm_Add_EmployeeContract(this,
                            _UserId,
                            _FullName, 0);
                        Cursor.Current = Cursors.Default;
                        f_NewContract.ShowDialog();
                    }

                    else
                    {
                        Cursor.Current = Cursors.AppStarting;

                        frm_Add_SubContract f_NewSubContract = null;
                        try
                        {
                            f_NewSubContract = new frm_Add_SubContract(this,
                                Convert.ToInt32(e.Row.Cells["EmployeeSubContractId"].Value),
                                _UserId,
                                _FullName);
                        }
                        catch
                        {
                        }
                        Cursor.Current = Cursors.Default;
                        if (f_NewSubContract != null)
                            f_NewSubContract.ShowDialog();
                    }
                }
        }

        private void rgvEmployeeContract_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            radLabelElement1.Text = "Tổng số nhân viên: " +
                                    Utilities.Utilities.GetDataRowCount(rgvEmployeeContract.ChildRows);
        }

        private void RmiRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void ContractList_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void RgvEmployeeContract_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            var dataColumn = e.CellElement.ColumnInfo as GridViewDataColumn;
            if (dataColumn.Name == "ToDate")
            {
                var dtValue = (DateTime) e.CellElement.RowInfo.Cells[dataColumn.Name].Value;
                if (dtValue.Year == 1753)
                    e.CellElement.Text = "Không xác định ";
            }
        }

        private void radLabel1_Click()
        {
            Cursor.Current = Cursors.AppStarting;
            _IsContractType = 0;
            rgvEmployeeContract.DataSource = EmployeeContractBLL.GetAllToDT();

            Utilities.Utilities.GridFormatting(rgvEmployeeContract);
            radLabelElement1.Text = "Tổng số nhân viên: " + rgvEmployeeContract.ChildRows.Count;
            Cursor.Current = Cursors.Default;
        }

        private void radLabel2_Click()
        {
            Cursor.Current = Cursors.AppStarting;
            _IsContractType = 1;
            rgvEmployeeContract.DataSource = EmployeeContractBLL.RemindExpiredConstractsToDT(string.Empty, string.Empty,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), 0);
            
            Utilities.Utilities.GridFormatting(rgvEmployeeContract);
            radLabelElement1.Text = "Tổng số nhân viên: " + rgvEmployeeContract.ChildRows.Count;
            Cursor.Current = Cursors.Default;
        }

        private int CalculateExpired()
        {
            var _Expression =
                $"ToDate <= #{new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(2)}# AND ToDate <> #01/01/1753# OR (FromDate is null AND ToDate is null) AND Active = 1";
            return dtEffective.Select(_Expression).Length;
        }

        private int CalculateChanged()
        {
            var _Expression =
                $"FromDate >= #{new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)}# AND FromDate <= #{DateTime.Now}#";
            return dtEffective.Select(_Expression).Length;
        }

        private void radLabel3_Click()
        {
            Cursor.Current = Cursors.AppStarting;
            _IsContractType = 2;
            rgvEmployeeContract.DataSource = EmployeeContractBLL.ChangedConstractsToDT(string.Empty, 0,
                DateTime.Now.Month,
                DateTime.Now.Year, 0);
            //foreach (var item in rgvEmployeeContract.ChildRows)
            //{
            //    var _ImageName = StringFormat.GetUserCode(int.Parse(item.Cells["UserId"].Value.ToString()));
            //    if (clsGlobal.EmployeeImageList.Exists(x => x.ImageName == _ImageName))
            //        if (_ImageName == clsGlobal.EmployeeImageList.Find(x => x.ImageName == _ImageName).ImageName)
            //            item.Cells["Picture"].Value =
            //                clsGlobal.EmployeeImageList.Find(x => x.ImageName == _ImageName).Image;
            //}
            Utilities.Utilities.GridFormatting(rgvEmployeeContract);
            radLabelElement1.Text = "Tổng số nhân viên: " + rgvEmployeeContract.ChildRows.Count;
            Cursor.Current = Cursors.Default;
        }

        private void radLabelElement2_Click(object sender, EventArgs e)
        {
            radLabel1_Click();
        }

        private void radLabelElement3_Click(object sender, EventArgs e)
        {
            radLabel2_Click();
        }

        private void radLabelElement4_Click(object sender, EventArgs e)
        {
            radLabel3_Click();
        }

        private void rgvEmployeeContract_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcmm.DropDown;
        }
    }
}