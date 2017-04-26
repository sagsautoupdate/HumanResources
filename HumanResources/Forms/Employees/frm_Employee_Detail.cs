using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H2;
using HRMUtil;
using HumanResources.Properties;
using Telerik.WinControls.UI;
using StringFormat = HRMUtil.StringFormat;

namespace HumanResources.Forms.Employees
{
    public partial class frm_Employee_Detail : RadForm
    {
        private static frm_Employee_Detail s_Instance;
        private readonly int _UserId;
        private frm_Employee_Working _few;
        private string _FullName;
        private string _OldContent = string.Empty;

        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        public frm_Employee_Detail()
        {
            InitializeComponent();
            Utilities.Utilities.SetFormSize(this);
            Utilities.Utilities.SetScreenColor(this);
        }

        public frm_Employee_Detail(frm_Employee_Working few, int UserId, string FullName)
        {
            InitializeComponent();
            Utilities.Utilities.SetFormSize(this);
            Utilities.Utilities.SetScreenColor(this);

            _few = few;
            _UserId = UserId;
            _FullName = FullName;

            Text = string.Format("HỒ SƠ CỦA {0} (MÃ NV: {1})", FullName.ToUpper(), UserId);
            label1.Text = FullName.ToUpper();
        }

        public static frm_Employee_Detail Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Employee_Detail();
                return s_Instance;
            }
        }

        private void frm_Employee_Detail_Load(object sender, EventArgs e)
        {
            radMultiColumnComboBox1.DataSource = EmployeesBLL.GetAllDT(1);
            radMultiColumnComboBox1.DisplayMember = "FullName";
            radMultiColumnComboBox1.ValueMember = "UserId";
            radMultiColumnComboBox1.SelectedValue = _UserId;

            BS_Education.DataSource = EducationLevelsBLL.GetDtAll();
            InitData(Convert.ToInt32(radMultiColumnComboBox1.SelectedValue));

            BS_RelationTypes.DataSource = RelationTypesBLL.GetAllDT();

            Utilities.Utilities.GridFormatting(rgvCongTac);
            Utilities.Utilities.GridFormatting(rgvHocVan);
            Utilities.Utilities.GridFormatting(rgvKhenThuong);
            Utilities.Utilities.GridFormatting(rgvKyLuat);
            Utilities.Utilities.GridFormatting(rgvQuanHe);
            FormClosed += Frm_Employee_Detail_FormClosed;
            rgvHocVan.CellEditorInitialized += RgvHocVan_CellEditorInitialized;
            rgvQuanHe.CellEditorInitialized += RgvQuanHe_CellEditorInitialized;

            radMultiColumnComboBox1.SelectedValueChanged += RadMultiColumnComboBox1_SelectedValueChanged;
        }

        private void RadMultiColumnComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            InitData(Convert.ToInt32(radMultiColumnComboBox1.SelectedValue));
            Cursor.Current = Cursors.Default;
        }


        private void InitData(int userId)
        {
            var dr = EmployeesBLL.DR_GetEmployeeById(userId);
            Text = string.Format("HỒ SƠ CỦA {0} (MÃ NV: {1})", dr["FullName"].ToString().ToUpper(), userId);
            label1.Text = dr["FullName"].ToString().ToUpper();

            var DataSource = Utilities.Utilities.GetServerByKeyWithoutDecrypt(clsGlobal.ConnectionString);
            if (DataSource.Equals("10.10.0.5"))
                DataSource = "10.10.0.8";
            var ImageLink = string.Format("http://{0}/HRM/Employee/Images/", DataSource);
            try
            {
                pictureBox1.Image =
                    Utilities.Utilities.LoadPicture(string.Format("{0}{1}.jpg", ImageLink,
                        StringFormat.GetUserCode(userId)));
            }
            catch (WebException ex)
            {
                pictureBox1.Image = Resources.no_image;
            }

            txtUserId.Text = StringFormat.GetUserCodeX6(Convert.ToInt32(dr["UserId"]));
            var _MinDate = FormatDate.GetSQLDateMinValue;
            var _Sex = dr["Sex"] == DBNull.Value ? 9999 : Convert.ToInt32(dr["Sex"]);
            if (_Sex != 9999)
                if (Convert.ToInt32(dr["Sex"]) == 1)
                    rbMale.IsChecked = true;
                else
                    rbFemale.IsChecked = true;
            txtFullName.Text = dr["FullName"] == DBNull.Value ? string.Empty : dr["FullName"].ToString();
            dtpBirthday.Text = dr["Birthday"] == DBNull.Value
                ? _MinDate.ToString("dd/MM/yyyy")
                : Convert.ToDateTime(dr["Birthday"]).ToString("dd/MM/yyyy");
            var _Marriage = dr["Marriage"] == DBNull.Value ? false : Convert.ToBoolean(dr["Marriage"]);
            cbMarriage.Checked = _Marriage;
            var _DirectWorking = dr["DirectWorking"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DirectWorking"]);
            radCheckBox1.Checked = _DirectWorking == 1;
            txtBirthPlace.Text = dr["BirthPlace"] == DBNull.Value ? string.Empty : dr["BirthPlace"].ToString();
            txtWorkPhone.Text = dr["WorkingPhone"] == DBNull.Value ? string.Empty : dr["WorkingPhone"].ToString();
            txtNativePlace.Text = dr["NativePlace"] == DBNull.Value ? string.Empty : dr["NativePlace"].ToString();
            txtTaxCode.Text = dr["TaxCode"] == DBNull.Value ? string.Empty : dr["TaxCode"].ToString();
            txtSocialInsuranceNo.Text = dr["SocialInsuranceNo"] == DBNull.Value
                ? string.Empty
                : dr["SocialInsuranceNo"].ToString();
            dtpJoinDate.Text = dr["JoinDate"] == DBNull.Value
                ? _MinDate.ToString("dd/MM/yyyy")
                : Convert.ToDateTime(dr["JoinDate"]).ToString("dd/MM/yyyy");
            dtpJoinCompanyDate.Text = dr["JoinCompanyDate"] == DBNull.Value
                ? _MinDate.ToString("dd/MM/yyyy")
                : Convert.ToDateTime(dr["JoinCompanyDate"]).ToString("dd/MM/yyyy");
            txtHomePhone.Text = dr["HomePhone"] == DBNull.Value ? string.Empty : dr["HomePhone"].ToString();
            txtCellPhone.Text = dr["HandPhone"] == DBNull.Value ? string.Empty : dr["HandPhone"].ToString();
            txtNormalNames.Text = dr["NormalNames"] == DBNull.Value ? string.Empty : dr["NormalNames"].ToString();
            txtOtherNames.Text = dr["OtherNames"] == DBNull.Value ? string.Empty : dr["OtherNames"].ToString();
            txtOrigin.Text = dr["Origin"] == DBNull.Value ? string.Empty : dr["Origin"].ToString();
            txtNation.Text = dr["Nation"] == DBNull.Value ? string.Empty : dr["Nation"].ToString();
            txtNationality.Text = dr["Nationality"] == DBNull.Value ? string.Empty : dr["Nationality"].ToString();
            txtReligion.Text = dr["Religion"] == DBNull.Value ? string.Empty : dr["Religion"].ToString();
            txtResident.Text = dr["Resident"] == DBNull.Value ? string.Empty : dr["Resident"].ToString();
            txtLive.Text = dr["Live"] == DBNull.Value ? string.Empty : dr["Live"].ToString();
            txtIdCard.Text = dr["IdCard"] == DBNull.Value ? string.Empty : dr["IdCard"].ToString();
            dtpDateOfIssue.Text = dr["DateOfIssue"] == DBNull.Value
                ? _MinDate.ToString("dd/MM/yyyy")
                : Convert.ToDateTime(dr["DateOfIssue"]).ToString("dd/MM/yyyy");
            txtPlaceOfIssue.Text = dr["PlaceOfIssue"] == DBNull.Value ? string.Empty : dr["PlaceOfIssue"].ToString();
            dtpDateJoinCYU.Text = dr["DateJoinCYU"] == DBNull.Value
                ? _MinDate.ToString("dd/MM/yyyy")
                : Convert.ToDateTime(dr["DateJoinCYU"]).ToString("dd/MM/yyyy");
            txtPlaceJoinCYU.Text = dr["PlaceJoinCYU"] == DBNull.Value ? string.Empty : dr["PlaceJoinCYU"].ToString();
            dtpDateJoinParty.Text = dr["DateJoinParty"] == DBNull.Value
                ? _MinDate.ToString("dd/MM/yyyy")
                : Convert.ToDateTime(dr["DateJoinParty"]).ToString("dd/MM/yyyy");
            txtPlaceJoinParty.Text = dr["PlaceJoinParty"] == DBNull.Value
                ? string.Empty
                : dr["PlaceJoinParty"].ToString();
            dtpDateOfEnlisted.Text = dr["DateOfEnlisted"] == DBNull.Value
                ? _MinDate.ToString("dd/MM/yyyy")
                : Convert.ToDateTime(dr["DateOfEnlisted"]).ToString("dd/MM/yyyy");
            dtpDateOfDemobilized.Text = dr["DateOfDemobilized"] == DBNull.Value
                ? _MinDate.ToString("dd/MM/yyyy")
                : Convert.ToDateTime(dr["DateOfDemobilized"]).ToString("dd/MM/yyyy");
            txtArmyRank.Text = dr["ArmyRank"] == DBNull.Value ? string.Empty : dr["ArmyRank"].ToString();
            txtAccountNo.Text = dr["AccountNo"] == DBNull.Value ? string.Empty : dr["AccountNo"].ToString();
            txtCardNo.Text = dr["CardNo"] == DBNull.Value ? string.Empty : dr["CardNo"].ToString();
            txtBankName.Text = dr["BankName"] == DBNull.Value ? string.Empty : dr["BankName"].ToString();
            cbStatus.Checked = Convert.ToInt32(dr["Status"]) == 1;

            var dtHocVan = EmployeeEducationLevelsBLL.GetDtById(userId);
            rgvHocVan.DataSource = dtHocVan;
            foreach (var row in rgvHocVan.Rows)
                try
                {
                    if (Convert.ToBoolean(row.Cells["IsHighest"].Value) &&
                        (row.Cells["IsHighest"].Value != DBNull.Value))
                        row.Cells["column1"].Value = true;
                }
                catch
                {
                    row.Cells["column1"].Value = false;
                }
            Utilities.Utilities.GridFormatting(rgvHocVan);

            var dtQuanHe = EmployeeRelationBLL.GetByFilterDT(0, userId, -1);
            rgvQuanHe.DataSource = dtQuanHe;
            Utilities.Utilities.GridFormatting(rgvQuanHe);

            var dtCongTac = EmployeeJobHistoryBLL.GetByFilterToDT(1, userId);
            rgvCongTac.DataSource = dtCongTac;
            Utilities.Utilities.GridFormatting(rgvCongTac);

            rgvKhenThuong.DataSource = EmployeeJobHistoryBLL.GetByFilterToDT(2, userId);
            rgvKyLuat.DataSource = EmployeeJobHistoryBLL.GetByFilterToDT(3, userId);
            Utilities.Utilities.GridFormatting(rgvKhenThuong);
            Utilities.Utilities.GridFormatting(rgvKyLuat);
        }

        private void Expression(int Id)
        {
            rgvHocVan.Columns["Id"].ConditionalFormattingObjectList.Clear();

            var obj = new ConditionalFormattingObject("Id", ConditionTypes.Equal, Id.ToString(), string.Empty, true)
            {
                RowBackColor = Color.FromArgb(0x00, 0xFF, 0x8A)
            };
            rgvHocVan.Columns["Id"].ConditionalFormattingObjectList.Add(obj);
        }

        private int ChangeID(string name)
        {
            return int.Parse(EducationLevelsBLL.GetDRAll(name)["EducationLevelId"].ToString());
        }

        private void SaveEducation()
        {
            var UserId = 0;
            var EduId = 0;
            var IdEdu = 0;
            var _old = string.Empty;
            var _oldEmp = string.Empty;

            foreach (var row in rgvHocVan.Rows)
            {
                var obj = new EmployeeEducationLevelsBLL();
                var id = 0;
                try
                {
                    id = int.Parse(row.Cells["Id"].Value.ToString());
                }
                catch
                {
                    id = 0;
                }
                var educationid = ChangeID(row.Cells["Name"].Value.ToString());
                var educationvalue = row.Cells["EducationLevelValue"].Value.ToString();
                var remark = row.Cells["Remark"].Value.ToString();

                obj.EducationLevelId = educationid;
                obj.EducationLevelValue = educationvalue;
                obj.Remark = remark;
                obj.UserId = _UserId;
                obj.Id = id;

                if (id > 0)
                {
                    var dr1 = EmployeeEducationLevelsBLL.GetByFilter(id, educationid, educationvalue);
                    _old =
                        $"UserId: {_UserId}, EducationLevelId: {dr1["EducationLevelId"]}, EducationLevelValue: N'{dr1["EducationLevelValue"]}', Remark: N'{dr1["Remark"]}', Id: {dr1["Id"]}";
                }
                obj.Save();
                _SP = obj.ReturnSP();
                _SPValue = obj.ReturnSPValue();
                Utilities.Utilities.SaveHRMLog("H0_EmployeeEducationLevel", _SP, _SPValue, _old);
            }

            foreach (var item in rgvHocVan.Rows)
                try
                {
                    var isChecked = Convert.ToBoolean(item.Cells["column1"].Value);
                    var _Id = Convert.ToInt32(item.Cells["Id"].Value);
                    if (isChecked)
                    {
                        var dr2 = EmployeeEducationLevelsBLL.GetByFilter(_Id, 1, string.Empty);
                        _oldEmp = $"UserId: {UserId}, EducationLevelId: {dr2["EducationLevelId"]}, Id: {dr2["Id"]}";

                        UserId = _UserId;
                        EduId = ChangeID(item.Cells["Name"].Value.ToString());
                        IdEdu = int.Parse(item.Cells["Id"].Value.ToString());

                        EmployeeEducationLevelsBLL.UpdateHighest(UserId, EduId, IdEdu);
                        _SP = "Upd_H0_EmployeeEducationLevel_Highest";
                        _SPValue = $"UserId: {UserId}, EducationLevelId: {EduId}, Id: {IdEdu}";
                        Utilities.Utilities.SaveHRMLog("Employees", _SP, _SPValue, _oldEmp);
                    }
                }
                catch
                {
                    MessageBox.Show("Thử lưu trước khi cập nhật trình độ cao nhất", "Alert", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
        }

        public void RefreshGridHocVan()
        {
            var dtHocVan = EmployeeEducationLevelsBLL.GetDtById(_UserId);
            rgvHocVan.DataSource = dtHocVan;

            Utilities.Utilities.GridFormatting(rgvHocVan);

            Expression(
                Convert.ToInt32(
                    EmployeeEducationLevelsBLL.GetByFilter(_UserId,
                        Convert.ToInt32(EmployeesBLL.DR_GetEmployeeById(_UserId)["HighestLevel"]),
                        EmployeesBLL.DR_GetEmployeeById(_UserId)["HighestLevelNameValue"].ToString().Trim())["Id"]));
        }

        public void RefreshGridQuanHe()
        {
            var dtQuanHe = EmployeeRelationBLL.GetByFilterDT(0, _UserId, -1);
            rgvQuanHe.DataSource = dtQuanHe;

            Utilities.Utilities.GridFormatting(rgvQuanHe);
        }

        public void RefreshGridCongTac()
        {
            radPageView1.SelectedPage = pvpWorked;

            var dtCongTac = EmployeeJobHistoryBLL.GetByFilterToDT(1, _UserId);
            rgvCongTac.DataSource = dtCongTac;

            Utilities.Utilities.GridFormatting(rgvCongTac);
        }

        public void RefreshGridKhenThuong()
        {
            radPageView1.SelectedPage = pvpReward;

            var dtKhenThuong = EmployeeJobHistoryBLL.GetByFilterToDT(2, _UserId);
            rgvKhenThuong.DataSource = dtKhenThuong;

            Utilities.Utilities.GridFormatting(rgvKhenThuong);
        }

        public void RefreshGridKyLuat()
        {
            radPageView1.SelectedPage = pvpDisciplined;

            var dtKyLuat = EmployeeJobHistoryBLL.GetByFilterToDT(3, _UserId);
            rgvKyLuat.DataSource = dtKyLuat;

            Utilities.Utilities.GridFormatting(rgvKyLuat);
        }

        private void RgvQuanHe_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            var editor = e.ActiveEditor as RadDropDownListEditor;
            if (editor != null)
            {
                var el = editor.EditorElement as RadDropDownListEditorElement;
                el.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                el.AutoCompleteMode = AutoCompleteMode.Suggest;
                el.AutoCompleteSuggest.SuggestMode = SuggestMode.Contains;
            }
        }

        private void RgvHocVan_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            var editor = e.ActiveEditor as RadDropDownListEditor;
            if (editor != null)
            {
                var el = editor.EditorElement as RadDropDownListEditorElement;
                el.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                el.AutoCompleteMode = AutoCompleteMode.Suggest;
                el.AutoCompleteSuggest.SuggestMode = SuggestMode.Contains;
            }
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            {
                var oldDR = EmployeesBLL.DR_GetEmployeeById(_UserId);
                if (radPageView1.SelectedPage == pvpGeneral)
                {
                    _OldContent =
                        $"FullName: N'{oldDR["FullName"]}', OtherNames: N'{oldDR["OtherNames"]}', NormalNames: N'{oldDR["NormalNames"]}', Sex: '{oldDR["Sex"]}', Birthday: '{oldDR["Birthday"]}', BirthPlace: N'{oldDR["BirthPlace"]}', NativePlace: N'{oldDR["NativePlace"]}', Origin: N'{oldDR["Origin"]}', IdCard: '{oldDR["IdCard"]}', DateOfIssue: '{oldDR["DateOfIssue"]}', PlaceOfIssue: N'{oldDR["PlaceOfIssue"]}', Nation: N'{oldDR["Nation"]}', Nationality: N'{oldDR["Nationality"]}', Marriage: '{oldDR["Marriage"]}', Religion: N'{oldDR["Religion"]}', JoinDate: '{oldDR["JoinDate"]}', JoinCompanyDate: '{oldDR["JoinCompanyDate"]}', UserId: {_UserId}";
                    bool _Sex, _Marriage;
                    DateTime _JoinDate,
                        _JoinCompanyDate,
                        _Birthday,
                        _DateOfIssue,
                        _DateJoinParty,
                        _DateJoinCYU,
                        _DateOfEnlisted,
                        _DateOfDemobilized;
                    try
                    {
                        _JoinDate = DateTime.ParseExact(dtpJoinDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        _JoinDate = FormatDate.GetSQLDateMinValue;
                    }
                    try
                    {
                        _JoinCompanyDate = DateTime.ParseExact(dtpJoinCompanyDate.Text, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        _JoinCompanyDate = FormatDate.GetSQLDateMinValue;
                    }
                    try
                    {
                        _Birthday = DateTime.ParseExact(dtpBirthday.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        _Birthday = FormatDate.GetSQLDateMinValue;
                    }
                    try
                    {
                        _DateOfIssue = DateTime.ParseExact(dtpDateOfIssue.Text, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        _DateOfIssue = FormatDate.GetSQLDateMinValue;
                    }
                    try
                    {
                        _DateJoinParty = DateTime.ParseExact(dtpDateJoinParty.Text, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        _DateJoinParty = FormatDate.GetSQLDateMinValue;
                    }
                    try
                    {
                        _DateJoinCYU = DateTime.ParseExact(dtpDateJoinCYU.Text, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        _DateJoinCYU = FormatDate.GetSQLDateMinValue;
                    }
                    try
                    {
                        _DateOfEnlisted = DateTime.ParseExact(dtpDateOfEnlisted.Text, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        _DateOfEnlisted = FormatDate.GetSQLDateMinValue;
                    }
                    try
                    {
                        _DateOfDemobilized = DateTime.ParseExact(dtpDateOfDemobilized.Text, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        _DateOfDemobilized = FormatDate.GetSQLDateMinValue;
                    }
                    string _FullName,
                        _OtherNames,
                        _NormalNames,
                        _BirthPlace,
                        _NativePlace,
                        _Origin,
                        _IdCard,
                        _PlaceOfIssue,
                        _Nation,
                        _Nationality,
                        _Religion;
                    try
                    {
                        _FullName = txtFullName.Text.Trim();
                    }
                    catch
                    {
                        _FullName = "";
                    }
                    try
                    {
                        _OtherNames = txtOtherNames.Text.Trim();
                    }
                    catch
                    {
                        _OtherNames = "";
                    }
                    try
                    {
                        _NormalNames = txtNormalNames.Text.Trim();
                    }
                    catch
                    {
                        _NormalNames = "";
                    }
                    try
                    {
                        _BirthPlace = txtBirthPlace.Text.Trim();
                    }
                    catch
                    {
                        _BirthPlace = "";
                    }
                    try
                    {
                        _NativePlace = txtNativePlace.Text.Trim();
                    }
                    catch
                    {
                        _NativePlace = "";
                    }
                    try
                    {
                        _Origin = txtOrigin.Text.Trim();
                    }
                    catch
                    {
                        _Origin = "";
                    }
                    try
                    {
                        _IdCard = txtIdCard.Text.Trim();
                    }
                    catch
                    {
                        _IdCard = "";
                    }
                    try
                    {
                        _PlaceOfIssue = txtPlaceOfIssue.Text.Trim();
                    }
                    catch
                    {
                        _PlaceOfIssue = "";
                    }
                    try
                    {
                        _Nation = txtNation.Text.Trim();
                    }
                    catch
                    {
                        _Nation = "";
                    }
                    try
                    {
                        _Nationality = txtNationality.Text.Trim();
                    }
                    catch
                    {
                        _Nationality = "";
                    }
                    try
                    {
                        _Religion = txtReligion.Text.Trim();
                    }
                    catch
                    {
                        _Religion = "";
                    }
                    try
                    {
                        _Sex = rbMale.IsChecked ? true : false;
                    }
                    catch
                    {
                        _Sex = false;
                    }
                    try
                    {
                        _Marriage = cbMarriage.IsChecked ? true : false;
                    }
                    catch
                    {
                        _Marriage = false;
                    }
                    EmployeesBLL.UpdateRadThongTin(
                        _FullName, _OtherNames, _NormalNames,
                        rbMale.IsChecked ? 1 : 0,
                        _Birthday,
                        _BirthPlace, _NativePlace, _Origin,
                        _IdCard, _DateOfIssue, _PlaceOfIssue,
                        _Nation, _Nationality,
                        cbMarriage.Checked ? 1 : 0,
                        _Religion, _JoinDate, _JoinCompanyDate, _UserId);
                    _SP = "Upd_H0_Employee_RadThongTin";
                    _SPValue =
                        $"FullName: N'{_FullName}', OtherNames: N'{_OtherNames}', NormalNames: N'{_NormalNames}', Sex: '{_Sex}', Birthday: '{_Birthday}', BirthPlace: N'{_BirthPlace}', NativePlace: N'{_NativePlace}', Origin: N'{_Origin}', IdCard: '{_IdCard}', DateOfIssue: '{_DateOfIssue}', PlaceOfIssue: N'{_PlaceOfIssue}', Nation: N'{_Nation}', Nationality: N'{_Nationality}', Marriage: '{_Marriage}', Religion: N'{_Religion}', JoinDate: '{dtpJoinDate}', JoinCompanyDate: '{_JoinCompanyDate}', UserId: {_UserId}";
                    Utilities.Utilities.SaveHRMLog("Employees", _SP, _SPValue, _OldContent);

                    _OldContent = $"DirectWorking: {oldDR["DirectWorking"]}, UserId: {_UserId}";
                    var _DirectWorking = 0;
                    try
                    {
                        _DirectWorking = radCheckBox1.Checked ? 1 : 0;
                    }
                    catch
                    {
                        _DirectWorking = 0;
                    }
                    EmployeesBLL.UpdateDirectWorking(_DirectWorking, _UserId);
                    _SP = "Upd_H0_Employee_DirectWorking";
                    _SPValue = $"DirectWorking: {_DirectWorking}, UserId: {_UserId}";
                    Utilities.Utilities.SaveHRMLog("Employees", _SP, _SPValue, _OldContent);

                    _OldContent =
                        $"WorkPhone: '{oldDR["WorkingPhone"]}', HandPhone: '{oldDR["HandPhone"]}', HomePhone: '{oldDR["HomePhone"]}', Resident: N'{oldDR["Resident"]}', Live: N'{oldDR["Live"]}', UserId: {_UserId}";
                    string _WorkPhone, _CellPhone, _HomePhone, _Resident, _Live;
                    try
                    {
                        _WorkPhone = txtWorkPhone.Text.Trim();
                    }
                    catch
                    {
                        _WorkPhone = "";
                    }
                    try
                    {
                        _CellPhone = txtCellPhone.Text.Trim();
                    }
                    catch
                    {
                        _CellPhone = "";
                    }
                    try
                    {
                        _HomePhone = txtHomePhone.Text.Trim();
                    }
                    catch
                    {
                        _HomePhone = "";
                    }
                    try
                    {
                        _Resident = txtResident.Text.Trim();
                    }
                    catch
                    {
                        _Resident = "";
                    }
                    try
                    {
                        _Live = txtLive.Text.Trim();
                    }
                    catch
                    {
                        _Live = "";
                    }
                    EmployeesBLL.UpdateRadLienHe(_WorkPhone, _CellPhone, _HomePhone,
                        _Resident, _Live, _UserId);
                    _SP = "Upd_H0_Employee_RadLienHe";
                    _SPValue =
                        $"WorkPhone: '{_WorkPhone}', CellPhone: '{_CellPhone}', HomePhone: '{_HomePhone}', Resident: N'{_Resident}', Live: N'{_Live}', UserId: {_UserId}";
                    Utilities.Utilities.SaveHRMLog("Employees", _SP, _SPValue, _OldContent);

                    _OldContent =
                        $"Status: '{oldDR["Status"]}', TaxCode: '{oldDR["TaxCode"]}', AccountNo: '{oldDR["AccountNo"]}', CardNo: '{oldDR["CardNo"]}', BankName: N'{oldDR["BankName"]}','', BHXH: '{oldDR["SocialInsuranceNo"]}',{_UserId}";
                    string _TaxCode, _AccountNo, _CardNo, _BankName, _SocialInsuranceNo;
                    var _Status = cbStatus.Checked ? true : false;
                    try
                    {
                        _TaxCode = txtTaxCode.Text.Trim();
                    }
                    catch
                    {
                        _TaxCode = "";
                    }
                    try
                    {
                        _AccountNo = txtAccountNo.Text.Trim();
                    }
                    catch
                    {
                        _AccountNo = "";
                    }
                    try
                    {
                        _CardNo = txtCardNo.Text.Trim();
                    }
                    catch
                    {
                        _CardNo = "";
                    }
                    try
                    {
                        _BankName = txtBankName.Text.Trim();
                    }
                    catch
                    {
                        _BankName = "";
                    }
                    try
                    {
                        _SocialInsuranceNo = txtSocialInsuranceNo.Text.Trim();
                    }
                    catch
                    {
                        _SocialInsuranceNo = "";
                    }
                    EmployeesBLL.UpdateRadCongViec(cbStatus.Checked ? 1 : 0,
                        _TaxCode, _AccountNo, _CardNo, _BankName, "", _SocialInsuranceNo, _UserId);
                    _SP = "Upd_H0_Employee_RadCongViec";
                    _SPValue =
                        $"Status: '{_Status}', TaxCode: '{_TaxCode}', AccountNo: '{_AccountNo}', CardNo: '{_CardNo}', BankName: N'{_BankName}','', SocialInsuranceNo: '{oldDR["SocialInsuranceNo"]}',{_UserId}";
                    Utilities.Utilities.SaveHRMLog("Employees", _SP, _SPValue, _OldContent);

                    _OldContent =
                        $"DateJoinParty: '{oldDR["DateJoinParty"]}', PlaceJoinParty: N'{oldDR["PlaceJoinParty"]}', DateJoinCYU: '{oldDR["DateJoinCYU"]}', PlaceJoinCYU: N'{oldDR["PlaceJoinCYU"]}', DateOfEnlisted: '{oldDR["DateOfEnlisted"]}', DateOfDemobilized: '{oldDR["DateOfDemobilized"]}', ArmyRank: N'{oldDR["ArmyRank"]}', {_UserId}";
                    string _PlaceJoinParty, _PlaceJoinCYU, _ArmyRank;
                    try
                    {
                        _PlaceJoinParty = txtPlaceJoinParty.Text.Trim();
                    }
                    catch
                    {
                        _PlaceJoinParty = "";
                    }
                    try
                    {
                        _PlaceJoinCYU = txtPlaceJoinCYU.Text.Trim();
                    }
                    catch
                    {
                        _PlaceJoinCYU = "";
                    }
                    try
                    {
                        _ArmyRank = txtArmyRank.Text.Trim();
                    }
                    catch
                    {
                        _ArmyRank = "";
                    }
                    EmployeesBLL.UpdateRadChinhTri(_DateJoinParty, _PlaceJoinParty,
                        _DateJoinCYU, _PlaceJoinCYU,
                        _DateOfEnlisted, _DateOfDemobilized, _ArmyRank, _UserId);
                    _SP = "Upd_H0_Employee_RadChinhTri";
                    _SPValue =
                        $"DateJoinParty: '{_DateJoinParty}', PlaceJoinParty: N'{_PlaceJoinParty}', DateJoinCYU: '{_DateJoinCYU}', PlaceJoinCYU: N'{_PlaceJoinCYU}', DateOfEnlisted: '{_DateOfEnlisted}', DateOfDemobilized: '{_DateOfDemobilized}', ArmyRank: N'{_ArmyRank}', {_UserId}";
                    Utilities.Utilities.SaveHRMLog("Employees", _SP, _SPValue, _OldContent);
                }
                else if (radPageView1.SelectedPage == pvpEducation)
                {
                    SaveEducation();

                    RefreshGridHocVan();
                }
                else if (radPageView1.SelectedPage == pvpRelationship)
                {
                    foreach (var row in rgvQuanHe.Rows)
                    {
                        DataRow dr = null;
                        try
                        {
                            dr =
                                EmployeeRelationBLL.GetByUserRelationId(
                                    Convert.ToInt32(row.Cells["UserRelationId"].Value));
                        }
                        catch
                        {
                        }
                        if (dr != null)
                            _OldContent =
                                $"UserId: {_UserId}, RelationTypeId: {dr["RelationTypeId"]}, RFullName: N'{dr["RFullName"]}', RDayOfBirth: {dr["RDayOfBirth"]}, RMonthOfBirth: {dr["RMonthOfBirth"]}, RYearOfBirth: {dr["RYearOfBirth"]}, RNativePlace: N'{dr["RNativePlace"]}', RResident: N'{dr["RResident"]}', RLive: N'{dr["RLive"]}', Before1975: N'{dr["Before1975"]}', After1975: N'{dr["After1975"]}', Participate: N'{dr["Participate"]}', Died: '{dr["Died"]}', DiedCause: N'{dr["DiedCause"]}', Others: N'{dr["Others"]}', UserRelationId: {dr["UserRelationId"]}";
                        else
                            _OldContent = "";
                        var _FullName = row.Cells["RFullName"].Value == null
                            ? ""
                            : row.Cells["RFullName"].Value.ToString();
                        var _Dob = row.Cells["RDayOfBirth"].Value == null
                            ? ""
                            : row.Cells["RDayOfBirth"].Value.ToString();
                        var _Mob = row.Cells["RMonthOfBirth"].Value == null
                            ? ""
                            : row.Cells["RMonthOfBirth"].Value.ToString();
                        var _Yob = row.Cells["RYearOfBirth"].Value == null
                            ? ""
                            : row.Cells["RYearOfBirth"].Value.ToString();
                        var _NativePlace = row.Cells["RNativePlace"].Value == null
                            ? ""
                            : row.Cells["RNativePlace"].Value.ToString();
                        var _Resident = row.Cells["RResident"].Value == null
                            ? ""
                            : row.Cells["RResident"].Value.ToString();
                        var _Live = row.Cells["RLive"].Value == null ? "" : row.Cells["RLive"].Value.ToString();
                        var _Before1975 = row.Cells["Before1975"].Value == null
                            ? ""
                            : row.Cells["Before1975"].Value.ToString();
                        var _After1975 = row.Cells["After1975"].Value == null
                            ? ""
                            : row.Cells["After1975"].Value.ToString();
                        var _Participate = row.Cells["Participate"].Value == null
                            ? ""
                            : row.Cells["Participate"].Value.ToString();
                        var _RelationTypeId = Convert.ToInt32(row.Cells["RelationTypeId"].Value);

                        var _Died = false;
                        var isChecked = Convert.ToBoolean(row.Cells["Died"].Value);
                        if (isChecked)
                            _Died = true;

                        var _Diedcause = row.Cells["DiedCause"].Value == null
                            ? ""
                            : row.Cells["DiedCause"].Value.ToString();
                        var _Others = row.Cells["Others"].Value == null ? "" : row.Cells["Others"].Value.ToString();

                        var _Relationid = 0;
                        try
                        {
                            _Relationid = Convert.ToInt32(row.Cells["UserRelationId"].Value);
                        }
                        catch
                        {
                            _Relationid = 0;
                        }

                        try
                        {
                            var obj = new EmployeeRelationBLL
                            {
                                UserId = _UserId,
                                RFullName = _FullName,
                                RDayOfBirth = int.Parse(_Dob),
                                RMonthOfBirth = int.Parse(_Mob),
                                RYearOfBirth = int.Parse(_Yob),
                                RNativePlace = _NativePlace,
                                RResident = _Resident,
                                RLive = _Live,
                                Before1975 = _Before1975,
                                After1975 = _After1975,
                                Participate = _Participate,
                                Died = _Died,
                                DiedCause = _Diedcause,
                                Others = _Others,
                                UserRelationId = _Relationid,
                                RelationTypeId = _RelationTypeId
                            };

                            obj.Save();
                            _SP = obj.ReturnSP();
                            _SPValue = obj.ReturnSPValue();
                        }
                        catch
                        {
                        }
                        finally
                        {
                            Utilities.Utilities.SaveHRMLog("H0_EmployeeRelation", _SP, _SPValue, _OldContent);
                        }
                    }

                    RefreshGridQuanHe();
                }
                else if (radPageView1.SelectedPage == pvpWorked)
                {
                    foreach (var row in rgvCongTac.Rows)
                    {
                        DataRow dr = null;
                        try
                        {
                            dr = EmployeeJobHistoryBLL.GetOne(Convert.ToInt32(row.Cells["JobHistoryId"].Value));
                        }
                        catch
                        {
                        }
                        if (dr != null)
                            _OldContent =
                                $"UserId: {_UserId}, FromYear: {dr["FromYear"]}, ToYear: {dr["ToYear"]}, Infor: N'{dr["Infor"]}', Type: {dr["Type"]}, JobHistoryId: {dr["JobHistoryId"]}";
                        else
                            _OldContent = "";
                        try
                        {
                            var objBLL = new EmployeeJobHistoryBLL
                            {
                                FromYear =
                                    (row.Cells["FromYear"].Value == null) ||
                                    (row.Cells["FromYear"].Value.ToString() == "")
                                        ? 0
                                        : Convert.ToInt32(row.Cells["FromYear"].Value),
                                ToYear =
                                    (row.Cells["ToYear"].Value == null) || (row.Cells["ToYear"].Value.ToString() == "")
                                        ? 0
                                        : Convert.ToInt32(row.Cells["ToYear"].Value),
                                Infor =
                                    (row.Cells["Infor"].Value == null) || (row.Cells["Infor"].Value.ToString() == "")
                                        ? ""
                                        : row.Cells["Infor"].Value.ToString(),
                                Type = 1,
                                UserId = _UserId,
                                JobHistoryId =
                                    (row.Cells["JobHistoryId"].Value == null) ||
                                    (row.Cells["JobHistoryId"].Value.ToString() == "")
                                        ? 0
                                        : Convert.ToInt32(row.Cells["JobHistoryId"].Value)
                            };

                            objBLL.Save();
                            _SP = objBLL.ReturnSP();
                            _SPValue = objBLL.ReturnSPValue();
                        }
                        catch
                        {
                        }
                        finally
                        {
                            Utilities.Utilities.SaveHRMLog("H0_EmployeeJobHistory", _SP, _SPValue, _OldContent);
                        }
                    }

                    RefreshGridCongTac();
                }
                else if (radPageView1.SelectedPage == pvpReward)
                {
                    foreach (var row in rgvKhenThuong.Rows)
                    {
                        DataRow dr = null;
                        try
                        {
                            dr = EmployeeJobHistoryBLL.GetOne(Convert.ToInt32(row.Cells["JobHistoryId"].Value));
                        }
                        catch
                        {
                        }
                        if (dr != null)
                            _OldContent =
                                $"UserId: {_UserId}, FromYear: {dr["FromYear"]}, ToYear: {dr["ToYear"]}, Infor: N'{dr["Infor"]}', Type: {dr["Type"]}, JobHistoryId: {dr["JobHistoryId"]}";
                        else
                            _OldContent = "";
                        try
                        {
                            var objBLL = new EmployeeJobHistoryBLL
                            {
                                FromYear =
                                    (row.Cells["FromYear"].Value == null) ||
                                    (row.Cells["FromYear"].Value.ToString() == "")
                                        ? 0
                                        : Convert.ToInt32(row.Cells["FromYear"].Value),
                                ToYear =
                                    (row.Cells["ToYear"].Value == null) || (row.Cells["ToYear"].Value.ToString() == "")
                                        ? 0
                                        : Convert.ToInt32(row.Cells["ToYear"].Value),
                                Infor =
                                    (row.Cells["Infor"].Value == null) || (row.Cells["Infor"].Value.ToString() == "")
                                        ? ""
                                        : row.Cells["Infor"].Value.ToString(),
                                Type = 2,
                                UserId = _UserId,
                                JobHistoryId =
                                    (row.Cells["JobHistoryId"].Value == null) ||
                                    (row.Cells["JobHistoryId"].Value.ToString() == "")
                                        ? 0
                                        : Convert.ToInt32(row.Cells["JobHistoryId"].Value)
                            };

                            objBLL.Save();
                            _SP = objBLL.ReturnSP();
                            _SPValue = objBLL.ReturnSPValue();
                        }
                        catch
                        {
                        }
                        finally
                        {
                            Utilities.Utilities.SaveHRMLog("H0_EmployeeJobHistory", _SP, _SPValue, _OldContent);
                        }
                    }

                    RefreshGridKhenThuong();
                }
                else if (radPageView1.SelectedPage == pvpDisciplined)
                {
                    foreach (var row in rgvKyLuat.Rows)
                    {
                        DataRow dr = null;
                        try
                        {
                            dr = EmployeeJobHistoryBLL.GetOne(Convert.ToInt32(row.Cells["JobHistoryId"].Value));
                        }
                        catch
                        {
                        }
                        if (dr != null)
                            _OldContent =
                                $"UserId: {_UserId}, FromYear: {dr["FromYear"]}, ToYear: {dr["ToYear"]}, Infor: N'{dr["Infor"]}', Type: {dr["Type"]}, JobHistoryId: {dr["JobHistoryId"]}";
                        else
                            _OldContent = "";
                        try
                        {
                            var objBLL = new EmployeeJobHistoryBLL
                            {
                                FromYear =
                                    (row.Cells["FromYear"].Value == null) ||
                                    (row.Cells["FromYear"].Value.ToString() == "")
                                        ? 0
                                        : Convert.ToInt32(row.Cells["FromYear"].Value),
                                ToYear =
                                    (row.Cells["ToYear"].Value == null) || (row.Cells["ToYear"].Value.ToString() == "")
                                        ? 0
                                        : Convert.ToInt32(row.Cells["ToYear"].Value),
                                Infor =
                                    (row.Cells["Infor"].Value == null) || (row.Cells["Infor"].Value.ToString() == "")
                                        ? ""
                                        : row.Cells["Infor"].Value.ToString(),
                                Type = 3,
                                UserId = _UserId,
                                JobHistoryId =
                                    (row.Cells["JobHistoryId"].Value == null) ||
                                    (row.Cells["JobHistoryId"].Value.ToString() == "")
                                        ? 0
                                        : Convert.ToInt32(row.Cells["JobHistoryId"].Value)
                            };

                            objBLL.Save();
                            _SP = objBLL.ReturnSP();
                            _SPValue = objBLL.ReturnSPValue();
                        }
                        catch
                        {
                        }
                        finally
                        {
                            Utilities.Utilities.SaveHRMLog("H0_EmployeeJobHistory", _SP, _SPValue, _OldContent);
                        }
                    }

                    RefreshGridKyLuat();
                }
            }

            Cursor.Current = Cursors.Default;
            MessageBox.Show("Cập nhật thành công!");
        }

        private void Frm_Employee_Detail_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }
    }
}