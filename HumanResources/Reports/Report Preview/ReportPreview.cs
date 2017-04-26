using System;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMBLL.H2;
using HRMUtil;
using HumanResources.Forms.Contract.Contract;
using HumanResources.Forms.Contract.SubContract;
using HumanResources.Forms.Recruitment.Candidate;
using HumanResources.Forms.Recruitment.Candidate.Contract;
using Telerik.WinControls.UI;

namespace HumanResources.Reports.Report_Preview
{
    public partial class ReportPreview : RadForm
    {
        private readonly int _CandidateId;
        private readonly int _ContractId;
        private readonly int _SubContractId;

        private readonly int _UserId;
        private frm_Add_EmployeeContract _addEmployeeContract;
        private frm_CandidateContract _candidateContract;

        private frm_CandidateListFinalRound _candidateLFR;
        private string _CandidateName;
        private string _FullName;

        private frm_List_Contract _listContract;
        private frm_List_SubContract _listSubContract;

        private frm_Add_SubContract _subContract;
        private frm_SubContractHistory _subContractHistory;
        private string _Type;

        public ReportPreview()
        {
            InitializeComponent();
        }


        public ReportPreview(frm_CandidateListFinalRound candidateLFR, int candidateId, string candidateName,
            string type)
        {
            InitializeComponent();

            _candidateLFR = candidateLFR;
            _CandidateId = candidateId;
            _CandidateName = candidateName;
            _Type = type;

            Candidate_Education_Contract_Parameters();
            switch (clsGlobal.Server)
            {
                case "Server_SAGS":
                    crystalReportViewer1.ReportSource = education_Contract1;
                    if (type == "Prt")
                        education_Contract1.PrintToPrinter(1, false, 0, 0);
                    break;
                case "Server_DAD":
                    crystalReportViewer1.ReportSource = daD_Education_Contract1;
                    if (type == "Prt")
                        daD_Education_Contract1.PrintToPrinter(1, false, 0, 0);
                    break;
                case "Server_CXR":
                    crystalReportViewer1.ReportSource = cxR_Education_Contract1;
                    if (type == "Prt")
                        cxR_Education_Contract1.PrintToPrinter(1, false, 0, 0);
                    break;
            }
        }

        public ReportPreview(frm_CandidateContract candidateContract, int candidateId, string candidateName, string type)
        {
            InitializeComponent();

            _candidateContract = candidateContract;
            _CandidateId = candidateId;
            _CandidateName = candidateName;
            _Type = type;

            Candidate_Education_Contract_Parameters();
            switch (clsGlobal.Server)
            {
                case "Server_SAGS":
                    crystalReportViewer1.ReportSource = education_Contract1;
                    if (type == "Prt")
                        education_Contract1.PrintToPrinter(1, false, 0, 0);
                    break;
                case "Server_DAD":
                    crystalReportViewer1.ReportSource = daD_Education_Contract1;
                    if (type == "Prt")
                        daD_Education_Contract1.PrintToPrinter(1, false, 0, 0);
                    break;
                case "Server_CXR":
                    crystalReportViewer1.ReportSource = cxR_Education_Contract1;
                    if (type == "Prt")
                        cxR_Education_Contract1.PrintToPrinter(1, false, 0, 0);
                    break;
            }
        }


        public ReportPreview(frm_Add_SubContract subContract, int subContractId, int UserId, string FullName,
            string type)
        {
            InitializeComponent();

            _subContract = subContract;
            _SubContractId = subContractId;
            _UserId = UserId;
            _FullName = FullName;
            _Type = type;

            SubContract_Parameter();
            if (clsGlobal.Server == "Server_SAGS")
            {
                crystalReportViewer1.ReportSource = subcontract_Contract1;
                if (type == "Prt")
                    subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
            if (clsGlobal.Server == "Server_DAD")
            {
                crystalReportViewer1.ReportSource = daD_Subcontract_Contract1;
                if (type == "Prt")
                    daD_Subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
            if (clsGlobal.Server == "Server_CXR")
            {
                crystalReportViewer1.ReportSource = cxR_Subcontract_Contract1;
                if (type == "Prt")
                    cxR_Subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
        }

        public ReportPreview(frm_SubContractHistory subContractHistory, int subContractId, int UserId, string FullName,
            string type)
        {
            InitializeComponent();

            _subContractHistory = subContractHistory;
            _SubContractId = subContractId;
            _UserId = UserId;
            _FullName = FullName;
            _Type = type;

            SubContract_Parameter();
            if (clsGlobal.Server == "Server_SAGS")
            {
                crystalReportViewer1.ReportSource = subcontract_Contract1;
                if (type == "Prt")
                    subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
            if (clsGlobal.Server == "Server_DAD")
            {
                crystalReportViewer1.ReportSource = daD_Subcontract_Contract1;
                if (type == "Prt")
                    daD_Subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
            if (clsGlobal.Server == "Server_CXR")
            {
                crystalReportViewer1.ReportSource = cxR_Subcontract_Contract1;
                if (type == "Prt")
                    cxR_Subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
        }

        public ReportPreview(frm_List_SubContract subContract, int subContractId, int UserId, string FullName,
            string type)
        {
            InitializeComponent();

            _listSubContract = subContract;
            _SubContractId = subContractId;
            _UserId = UserId;
            _FullName = FullName;
            _Type = type;

            SubContract_Parameter();
            if (clsGlobal.Server == "Server_SAGS")
            {
                crystalReportViewer1.ReportSource = subcontract_Contract1;
                if (type == "Prt")
                    subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
            if (clsGlobal.Server == "Server_DAD")
            {
                crystalReportViewer1.ReportSource = daD_Subcontract_Contract1;
                if (type == "Prt")
                    daD_Subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
            if (clsGlobal.Server == "Server_CXR")
            {
                crystalReportViewer1.ReportSource = cxR_Subcontract_Contract1;
                if (type == "Prt")
                    cxR_Subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
        }

        public ReportPreview(frm_List_Contract Contract, int subContractId, int UserId, string FullName, string type,
            string contractType)
        {
            InitializeComponent();

            _listContract = Contract;
            _SubContractId = subContractId;
            _UserId = UserId;
            _FullName = FullName;
            _Type = type;

            SubContract_Parameter();
            if (clsGlobal.Server == "Server_SAGS")
            {
                crystalReportViewer1.ReportSource = subcontract_Contract1;
                if (type == "Prt")
                    subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
            if (clsGlobal.Server == "Server_DAD")
            {
                crystalReportViewer1.ReportSource = daD_Subcontract_Contract1;
                if (type == "Prt")
                    daD_Subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
            if (clsGlobal.Server == "Server_CXR")
            {
                crystalReportViewer1.ReportSource = cxR_Subcontract_Contract1;
                if (type == "Prt")
                    cxR_Subcontract_Contract1.PrintToPrinter(1, false, 0, 0);
            }
        }


        public ReportPreview(frm_List_Contract listContract, int ContractId, int UserId, string FullName, string type)
        {
            InitializeComponent();

            _listContract = listContract;
            _ContractId = ContractId;
            _UserId = UserId;
            _FullName = FullName;
            _Type = type;
            switch (clsGlobal.Server)
            {
                case "Server_SAGS":
                    if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                        "HDVK3T")
                    {
                        ContractUnder3M_Parameter();
                        crystalReportViewer1.ReportSource = contract_Under3M1;
                        if (type == "Prt")
                            contract_Under3M1.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                            "HDTV")
                        {
                            ContractTrial_PassParameters();
                            crystalReportViewer1.ReportSource = contract_Trial1;
                            if (type == "Prt")
                                contract_Trial1.PrintToPrinter(1, false, 0, 0);
                        }
                        else
                        {
                            Contract_Parameter();
                            crystalReportViewer1.ReportSource = contract1;
                            if (type == "Prt")
                                contract1.PrintToPrinter(1, false, 0, 0);
                        }
                    }
                    break;
                case "Server_DAD":
                    if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                        "HDVK3T")
                    {
                        ContractUnder3M_Parameter();
                        crystalReportViewer1.ReportSource = daD_Contract_Under3M1;
                        if (type == "Prt")
                            daD_Contract_Under3M1.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                            "HDTV")
                        {
                            ContractTrial_PassParameters();
                            crystalReportViewer1.ReportSource = daD_Contract_Trial1;
                            if (type == "Prt")
                                daD_Contract_Trial1.PrintToPrinter(1, false, 0, 0);
                        }
                        else
                        {
                            Contract_Parameter();
                            crystalReportViewer1.ReportSource = daD_Contract1;
                            if (type == "Prt")
                                daD_Contract1.PrintToPrinter(1, false, 0, 0);
                        }
                    }
                    break;
                case "Server_CXR":
                    if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                        "HDVK3T")
                    {
                        ContractUnder3M_Parameter();
                        crystalReportViewer1.ReportSource = cxR_Contract_Under3M1;
                        if (type == "Prt")
                            cxR_Contract_Under3M1.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                            "HDTV")
                        {
                            ContractTrial_PassParameters();
                            crystalReportViewer1.ReportSource = cxR_Contract_Trial1;
                            if (type == "Prt")
                                cxR_Contract_Trial1.PrintToPrinter(1, false, 0, 0);
                        }
                        else
                        {
                            Contract_Parameter();
                            crystalReportViewer1.ReportSource = cxR_Contract1;
                            if (type == "Prt")
                                cxR_Contract1.PrintToPrinter(1, false, 0, 0);
                        }
                    }
                    break;
            }
        }

        public ReportPreview(frm_Add_EmployeeContract addContract, int ContractId, int UserId, string FullName,
            string type)
        {
            InitializeComponent();

            _addEmployeeContract = addContract;
            _ContractId = ContractId;
            _UserId = UserId;
            _FullName = FullName;
            _Type = type;
            switch (clsGlobal.Server)
            {
                case "Server_SAGS":
                    if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                        "HDVK3T")
                    {
                        ContractUnder3M_Parameter();
                        crystalReportViewer1.ReportSource = contract_Under3M1;
                        if (type == "Prt")
                            contract_Under3M1.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                            "HDTV")
                        {
                            ContractTrial_PassParameters();
                            crystalReportViewer1.ReportSource = contract_Trial1;
                            if (type == "Prt")
                                contract_Trial1.PrintToPrinter(1, false, 0, 0);
                        }
                        else
                        {
                            Contract_Parameter();
                            crystalReportViewer1.ReportSource = contract1;
                            if (type == "Prt")
                                contract1.PrintToPrinter(1, false, 0, 0);
                        }
                    }
                    break;
                case "Server_DAD":
                    if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                        "HDVK3T")
                    {
                        ContractUnder3M_Parameter();
                        crystalReportViewer1.ReportSource = daD_Contract_Under3M1;
                        if (type == "Prt")
                            daD_Contract_Under3M1.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                            "HDTV")
                        {
                            ContractTrial_PassParameters();
                            crystalReportViewer1.ReportSource = daD_Contract_Trial1;
                            if (type == "Prt")
                                daD_Contract_Trial1.PrintToPrinter(1, false, 0, 0);
                        }
                        else
                        {
                            Contract_Parameter();
                            crystalReportViewer1.ReportSource = daD_Contract1;
                            if (type == "Prt")
                                daD_Contract1.PrintToPrinter(1, false, 0, 0);
                        }
                    }
                    break;
                case "Server_CXR":
                    if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                        "HDVK3T")
                    {
                        ContractUnder3M_Parameter();
                        crystalReportViewer1.ReportSource = cxR_Contract_Under3M1;
                        if (type == "Prt")
                            cxR_Contract_Under3M1.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        if (EmployeeContractBLL.GetActiveContractByUserIdToDT(UserId)["ContractTypeCode"].ToString() ==
                            "HDTV")
                        {
                            ContractTrial_PassParameters();
                            crystalReportViewer1.ReportSource = cxR_Contract_Trial1;
                            if (type == "Prt")
                                cxR_Contract_Trial1.PrintToPrinter(1, false, 0, 0);
                        }
                        else
                        {
                            Contract_Parameter();
                            crystalReportViewer1.ReportSource = cxR_Contract1;
                            if (type == "Prt")
                                cxR_Contract1.PrintToPrinter(1, false, 0, 0);
                        }
                    }
                    break;
            }
        }

        private void Candidate_Education_Contract_Parameters()
        {
            if (clsGlobal.Server == "Server_SAGS")
            {
                var dr = CandidateContractionsBLL.DR_GetOne(_CandidateId);
                if (dr != null)
                {
                    try
                    {
                        var _EducatedPositionName = dr["EducatedPositionName"].ToString().Trim();
                        education_Contract1.SetParameterValue("EducatedPositionName", _EducatedPositionName);
                    }
                    catch
                    {
                    }
                    try
                    {
                        var _FromDate = Convert.ToDateTime(dr["FromDate"]);
                        var _ContractNo = StringFormat.SetContractNo(Convert.ToInt32(dr["ContractNo"]));
                        var ContractNumber = string.Empty;
                        if (clsGlobal.Server == "Server_SAGS")
                            ContractNumber = string.Format("SAGS{0}-{1}/HĐĐT", _FromDate.Year, _ContractNo);
                        if (clsGlobal.Server == "Server_DAD")
                            ContractNumber = string.Format("SAGS{0}-{1}/HĐĐT", _FromDate.Year, _ContractNo);
                        if (clsGlobal.Server == "Server_CXR")
                            ContractNumber = string.Format("SAGS-CXR{0}-{1}/HĐĐT", _FromDate.Year, _ContractNo);
                        education_Contract1.SetParameterValue("ContractNumber", ContractNumber);
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var _FromDate = Convert.ToDateTime(dr["FromDate"]);
                        var FromDate = string.Format("{0} tháng {1} năm {2}", _FromDate.Day, _FromDate.Month,
                            _FromDate.Year);
                        education_Contract1.SetParameterValue("FromDate", FromDate);
                    }
                    catch
                    {
                        MessageBox.Show("Thời gian hợp đồng không hợp lệ");
                    }

                    try
                    {
                        if (dr["Sex"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu giới tính");
                        }
                        else
                        {
                            var _Sex = Convert.ToBoolean(dr["Sex"]);
                            var Sex = _Sex ? "Ông" : "Bà";
                            education_Contract1.SetParameterValue("Sex", Sex);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu giới tính");
                    }

                    try
                    {
                        if (dr["LastName"].ToString().Equals(string.Empty) ||
                            dr["FirstName"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu họ tên");
                        }
                        else
                        {
                            var _LastName = dr["LastName"].ToString().Trim().ToUpper();
                            var _FirstName = dr["FirstName"].ToString().Trim().ToUpper();
                            var FullName = string.Format("{0} {1}", _LastName, _FirstName);

                            education_Contract1.SetParameterValue("FullName", FullName);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu họ tên");
                    }

                    try
                    {
                        if (dr["DayOfBirth"].ToString().Equals(string.Empty) ||
                            dr["MonthOfBirth"].ToString().Equals(string.Empty) ||
                            dr["YearOfBirth"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu ngày/ tháng/ năm sinh");
                        }
                        else
                        {
                            var _DayOfBirth = dr["DayOfBirth"].ToString();
                            var _MonthOfBirth = dr["MonthOfBirth"].ToString();
                            var _YearOfBirth = dr["YearOfBirth"].ToString();
                            var Birthday = string.Format("{0} tháng {1} năm {2}", _DayOfBirth, _MonthOfBirth,
                                _YearOfBirth);
                            education_Contract1.SetParameterValue("Birthday", Birthday);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu ngày/ tháng/ năm sinh");
                    }

                    try
                    {
                        if (dr["BirthPlace"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu nơi sinh");
                        }
                        else
                        {
                            var BirthPlace = dr["BirthPlace"].ToString().Trim();
                            education_Contract1.SetParameterValue("BirthPlace", BirthPlace);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu nơi sinh");
                    }

                    try
                    {
                        if (dr["Nationality"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu quốc tịch");
                        }
                        else
                        {
                            var Nationality = dr["Nationality"].ToString().Trim();
                            education_Contract1.SetParameterValue("Nationality", Nationality);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu quốc tịch");
                    }

                    try
                    {
                        if (dr["PositionName"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu môn học");
                        }
                        else
                        {
                            var Subject = dr["PositionName"].ToString().Trim();
                            education_Contract1.SetParameterValue("Subject", Subject);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu môn học");
                    }

                    try
                    {
                        if (dr["IdCard"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu CMND");
                        }
                        else
                        {
                            var IdCard = dr["IdCard"].ToString().Trim();
                            education_Contract1.SetParameterValue("IDCard", IdCard);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu CMND");
                    }

                    try
                    {
                        if (dr["IdDateOfIssue"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu ngày cấp CMND");
                        }
                        else
                        {
                            var DateOfIssue = Convert.ToDateTime(dr["IdDateOfIssue"]);
                            education_Contract1.SetParameterValue("DateOfIssue", DateOfIssue.ToString("dd/MM/yyyy"));
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu ngày cấp CMND");
                    }

                    try
                    {
                        if (dr["IdPlaceOfIssue"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu nơi cấp");
                        }
                        else
                        {
                            var PlaceOfIssue = dr["IdPlaceOfIssue"].ToString().Trim();
                            education_Contract1.SetParameterValue("PlaceOfIssue", PlaceOfIssue);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu nơi cấp");
                    }

                    try
                    {
                        if (dr["ToDate"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thời gian hợp đồng không hợp lệ");
                        }
                        else
                        {
                            var _ToDate = Convert.ToDateTime(dr["ToDate"]);
                            var ToDate = string.Format("{0} tháng {1} năm {2}", _ToDate.Day, _ToDate.Month, _ToDate.Year);
                            education_Contract1.SetParameterValue("ToDate", ToDate);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thời gian hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var EducationId = Convert.ToInt32(dr["EducationHighestLevelId"]);
                        var _EducationName =
                            CandidateTrainingJobHistoryBLL.GetById(Convert.ToInt32(dr["EducationHighestLevelId"]))[0]
                                .Training_Job;
                        education_Contract1.SetParameterValue("Education", _EducationName);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có trình độ học vấn");
                    }

                    try
                    {
                        if (dr["Resident"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu địa chỉ");
                        }
                        else
                        {
                            var Resident = dr["Resident"].ToString().Trim();
                            education_Contract1.SetParameterValue("Resident", Resident);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu địa chỉ");
                    }
                    var drFee = EducationFeeBLL.GetBySessionIdPositionId(Convert.ToInt32(dr["SessionId"]),
                        Convert.ToInt32(dr["PositionIdFee"]));
                    if (drFee != null)
                    {
                        try
                        {
                            if (drFee["Fee"].ToString().Length <= 0)
                            {
                                MessageBox.Show("Thiếu chi phí đào tạo");
                            }
                            else
                            {
                                var _Fee = string.Format("{0:#,###0}", drFee["Fee"].ToString().Trim());
                                education_Contract1.SetParameterValue("Fee",
                                    StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(_Fee)));
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Thiếu chi phí đào tạo");
                        }

                        try
                        {
                            if (drFee["FeeInVietNamese"].ToString().Length <= 0)
                            {
                                MessageBox.Show("Thiếu chi phí đào tạo");
                            }
                            else
                            {
                                var _FeeVN = drFee["FeeInVietNamese"].ToString().Trim();
                                education_Contract1.SetParameterValue("FeeVN", _FeeVN);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Thiếu chi phí đào tạo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thiếu chi phí đào tạo");
                    }

                    education_Contract1.SetParameterValue("CandidateId", _CandidateId);
                }
                else
                {
                    MessageBox.Show("Chưa tạo hợp đồng!");
                }
            }


            if (clsGlobal.Server == "Server_DAD")
            {
                var dr = CandidateContractionsBLL.DR_GetOne(_CandidateId);
                if (dr != null)
                {
                    try
                    {
                        var _EducatedPositionName = dr["EducatedPositionName"].ToString().Trim();
                        daD_Education_Contract1.SetParameterValue("EducatedPositionName", _EducatedPositionName);
                    }
                    catch
                    {
                    }
                    try
                    {
                        var _FromDate = Convert.ToDateTime(dr["FromDate"]);
                        var _ContractNo = StringFormat.SetContractNo(Convert.ToInt32(dr["ContractNo"]));
                        var ContractNumber = string.Empty;
                        if (clsGlobal.Server == "Server_SAGS")
                            ContractNumber = string.Format("SAGS{0}-{1}/HĐĐT", _FromDate.Year, _ContractNo);
                        if (clsGlobal.Server == "Server_DAD")
                            ContractNumber = string.Format("SAGS{0}-{1}/HĐĐT", _FromDate.Year, _ContractNo);
                        if (clsGlobal.Server == "Server_CXR")
                            ContractNumber = string.Format("SAGS-CXR{0}-{1}/HĐĐT", _FromDate.Year, _ContractNo);
                        daD_Education_Contract1.SetParameterValue("ContractNumber", ContractNumber);
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var _FromDate = Convert.ToDateTime(dr["FromDate"]);
                        var FromDate = string.Format("{0} tháng {1} năm {2}", _FromDate.Day, _FromDate.Month,
                            _FromDate.Year);
                        daD_Education_Contract1.SetParameterValue("FromDate", FromDate);
                    }
                    catch
                    {
                        MessageBox.Show("Thời gian hợp đồng không hợp lệ");
                    }

                    try
                    {
                        if (dr["Sex"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu giới tính");
                        }
                        else
                        {
                            var _Sex = Convert.ToBoolean(dr["Sex"]);
                            var Sex = _Sex ? "Ông" : "Bà";
                            daD_Education_Contract1.SetParameterValue("Sex", Sex);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu giới tính");
                    }

                    try
                    {
                        if (dr["LastName"].ToString().Equals(string.Empty) ||
                            dr["FirstName"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu họ tên");
                        }
                        else
                        {
                            var _LastName = dr["LastName"].ToString().Trim().ToUpper();
                            var _FirstName = dr["FirstName"].ToString().Trim().ToUpper();
                            var FullName = string.Format("{0} {1}", _LastName, _FirstName);

                            daD_Education_Contract1.SetParameterValue("FullName", FullName);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu họ tên");
                    }

                    try
                    {
                        if (dr["DayOfBirth"].ToString().Equals(string.Empty) ||
                            dr["MonthOfBirth"].ToString().Equals(string.Empty) ||
                            dr["YearOfBirth"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu ngày/ tháng/ năm sinh");
                        }
                        else
                        {
                            var _DayOfBirth = dr["DayOfBirth"].ToString();
                            var _MonthOfBirth = dr["MonthOfBirth"].ToString();
                            var _YearOfBirth = dr["YearOfBirth"].ToString();
                            var Birthday = string.Format("{0} tháng {1} năm {2}", _DayOfBirth, _MonthOfBirth,
                                _YearOfBirth);
                            daD_Education_Contract1.SetParameterValue("Birthday", Birthday);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu ngày/ tháng/ năm sinh");
                    }

                    try
                    {
                        if (dr["BirthPlace"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu nơi sinh");
                        }
                        else
                        {
                            var BirthPlace = dr["BirthPlace"].ToString().Trim();
                            daD_Education_Contract1.SetParameterValue("BirthPlace", BirthPlace);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu nơi sinh");
                    }

                    try
                    {
                        if (dr["Nationality"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu quốc tịch");
                        }
                        else
                        {
                            var Nationality = dr["Nationality"].ToString().Trim();
                            daD_Education_Contract1.SetParameterValue("Nationality", Nationality);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu quốc tịch");
                    }

                    try
                    {
                        if (dr["PositionName"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu môn học");
                        }
                        else
                        {
                            var Subject = dr["PositionName"].ToString().Trim();
                            daD_Education_Contract1.SetParameterValue("Subject", Subject);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu môn học");
                    }

                    try
                    {
                        if (dr["IdCard"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu CMND");
                        }
                        else
                        {
                            var IdCard = dr["IdCard"].ToString().Trim();
                            daD_Education_Contract1.SetParameterValue("IDCard", IdCard);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu CMND");
                    }

                    try
                    {
                        if (dr["IdDateOfIssue"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu ngày cấp CMND");
                        }
                        else
                        {
                            var DateOfIssue = Convert.ToDateTime(dr["IdDateOfIssue"]);
                            daD_Education_Contract1.SetParameterValue("DateOfIssue", DateOfIssue.ToString("dd/MM/yyyy"));
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu ngày cấp CMND");
                    }

                    try
                    {
                        if (dr["IdPlaceOfIssue"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu nơi cấp");
                        }
                        else
                        {
                            var PlaceOfIssue = dr["IdPlaceOfIssue"].ToString().Trim();
                            daD_Education_Contract1.SetParameterValue("PlaceOfIssue", PlaceOfIssue);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu nơi cấp");
                    }

                    try
                    {
                        if (dr["ToDate"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thời gian hợp đồng không hợp lệ");
                        }
                        else
                        {
                            var _ToDate = Convert.ToDateTime(dr["ToDate"]);
                            var ToDate = string.Format("{0} tháng {1} năm {2}", _ToDate.Day, _ToDate.Month, _ToDate.Year);
                            daD_Education_Contract1.SetParameterValue("ToDate", ToDate);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thời gian hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var EducationId = Convert.ToInt32(dr["EducationHighestLevelId"]);
                        var _EducationName =
                            CandidateTrainingJobHistoryBLL.GetById(Convert.ToInt32(dr["EducationHighestLevelId"]))[0]
                                .Training_Job;
                        daD_Education_Contract1.SetParameterValue("Education", _EducationName);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có trình độ học vấn");
                    }

                    try
                    {
                        if (dr["Resident"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu địa chỉ");
                        }
                        else
                        {
                            var Resident = dr["Resident"].ToString().Trim();
                            daD_Education_Contract1.SetParameterValue("Resident", Resident);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu địa chỉ");
                    }
                    var drFee = EducationFeeBLL.GetBySessionIdPositionId(Convert.ToInt32(dr["SessionId"]),
                        Convert.ToInt32(dr["PositionIdFee"]));
                    if (drFee != null)
                    {
                        try
                        {
                            if (drFee["Fee"].ToString().Length <= 0)
                            {
                                MessageBox.Show("Thiếu chi phí đào tạo");
                            }
                            else
                            {
                                var _Fee = string.Format("{0:#,###0}", drFee["Fee"].ToString().Trim());
                                daD_Education_Contract1.SetParameterValue("Fee",
                                    StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(_Fee)));
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Thiếu chi phí đào tạo");
                        }

                        try
                        {
                            if (drFee["FeeInVietNamese"].ToString().Length <= 0)
                            {
                                MessageBox.Show("Thiếu chi phí đào tạo");
                            }
                            else
                            {
                                var _FeeVN = drFee["FeeInVietNamese"].ToString().Trim();
                                daD_Education_Contract1.SetParameterValue("FeeVN", _FeeVN);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Thiếu chi phí đào tạo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thiếu chi phí đào tạo");
                    }

                    daD_Education_Contract1.SetParameterValue("CandidateId", _CandidateId);
                }
                else
                {
                    MessageBox.Show("Chưa tạo hợp đồng!");
                }
            }


            if (clsGlobal.Server == "Server_CXR")
            {
                var dr = CandidateContractionsBLL.DR_GetOne(_CandidateId);
                if (dr != null)
                {
                    try
                    {
                        var _EducatedPositionName = dr["EducatedPositionName"].ToString().Trim();
                        cxR_Education_Contract1.SetParameterValue("EducatedPositionName", _EducatedPositionName);
                    }
                    catch
                    {
                    }
                    try
                    {
                        var _FromDate = Convert.ToDateTime(dr["FromDate"]);
                        var _ContractNo = StringFormat.SetContractNo(Convert.ToInt32(dr["ContractNo"]));
                        var ContractNumber = string.Empty;
                        if (clsGlobal.Server == "Server_SAGS")
                            ContractNumber = string.Format("SAGS{0}-{1}/HĐĐT", _FromDate.Year, _ContractNo);
                        if (clsGlobal.Server == "Server_DAD")
                            ContractNumber = string.Format("SAGS{0}-{1}/HĐĐT", _FromDate.Year, _ContractNo);
                        if (clsGlobal.Server == "Server_CXR")
                            ContractNumber = string.Format("SAGS-CXR{0}-{1}/HĐĐT", _FromDate.Year, _ContractNo);
                        cxR_Education_Contract1.SetParameterValue("ContractNumber", ContractNumber);
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var _FromDate = Convert.ToDateTime(dr["FromDate"]);
                        var FromDate = string.Format("{0} tháng {1} năm {2}", _FromDate.Day, _FromDate.Month,
                            _FromDate.Year);
                        cxR_Education_Contract1.SetParameterValue("FromDate", FromDate);
                    }
                    catch
                    {
                        MessageBox.Show("Thời gian hợp đồng không hợp lệ");
                    }

                    try
                    {
                        if (dr["Sex"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu giới tính");
                        }
                        else
                        {
                            var _Sex = Convert.ToBoolean(dr["Sex"]);
                            var Sex = _Sex ? "Ông" : "Bà";
                            cxR_Education_Contract1.SetParameterValue("Sex", Sex);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu giới tính");
                    }

                    try
                    {
                        if (dr["LastName"].ToString().Equals(string.Empty) ||
                            dr["FirstName"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu họ tên");
                        }
                        else
                        {
                            var _LastName = dr["LastName"].ToString().Trim().ToUpper();
                            var _FirstName = dr["FirstName"].ToString().Trim().ToUpper();
                            var FullName = string.Format("{0} {1}", _LastName, _FirstName);

                            cxR_Education_Contract1.SetParameterValue("FullName", FullName);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu họ tên");
                    }

                    try
                    {
                        if (dr["DayOfBirth"].ToString().Equals(string.Empty) ||
                            dr["MonthOfBirth"].ToString().Equals(string.Empty) ||
                            dr["YearOfBirth"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu ngày/ tháng/ năm sinh");
                        }
                        else
                        {
                            var _DayOfBirth = dr["DayOfBirth"].ToString();
                            var _MonthOfBirth = dr["MonthOfBirth"].ToString();
                            var _YearOfBirth = dr["YearOfBirth"].ToString();
                            var Birthday = string.Format("{0} tháng {1} năm {2}", _DayOfBirth, _MonthOfBirth,
                                _YearOfBirth);
                            cxR_Education_Contract1.SetParameterValue("Birthday", Birthday);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu ngày/ tháng/ năm sinh");
                    }

                    try
                    {
                        if (dr["BirthPlace"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu nơi sinh");
                        }
                        else
                        {
                            var BirthPlace = dr["BirthPlace"].ToString().Trim();
                            cxR_Education_Contract1.SetParameterValue("BirthPlace", BirthPlace);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu nơi sinh");
                    }

                    try
                    {
                        if (dr["Nationality"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu quốc tịch");
                        }
                        else
                        {
                            var Nationality = dr["Nationality"].ToString().Trim();
                            cxR_Education_Contract1.SetParameterValue("Nationality", Nationality);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu quốc tịch");
                    }

                    try
                    {
                        if (dr["PositionName"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu môn học");
                        }
                        else
                        {
                            var Subject = dr["PositionName"].ToString().Trim();
                            cxR_Education_Contract1.SetParameterValue("Subject", Subject);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu môn học");
                    }

                    try
                    {
                        if (dr["IdCard"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu CMND");
                        }
                        else
                        {
                            var IdCard = dr["IdCard"].ToString().Trim();
                            cxR_Education_Contract1.SetParameterValue("IDCard", IdCard);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu CMND");
                    }

                    try
                    {
                        if (dr["IdDateOfIssue"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu ngày cấp CMND");
                        }
                        else
                        {
                            var DateOfIssue = Convert.ToDateTime(dr["IdDateOfIssue"]);
                            cxR_Education_Contract1.SetParameterValue("DateOfIssue", DateOfIssue.ToString("dd/MM/yyyy"));
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu ngày cấp CMND");
                    }

                    try
                    {
                        if (dr["IdPlaceOfIssue"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu nơi cấp");
                        }
                        else
                        {
                            var PlaceOfIssue = dr["IdPlaceOfIssue"].ToString().Trim();
                            cxR_Education_Contract1.SetParameterValue("PlaceOfIssue", PlaceOfIssue);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu nơi cấp");
                    }

                    try
                    {
                        if (dr["ToDate"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thời gian hợp đồng không hợp lệ");
                        }
                        else
                        {
                            var _ToDate = Convert.ToDateTime(dr["ToDate"]);
                            var ToDate = string.Format("{0} tháng {1} năm {2}", _ToDate.Day, _ToDate.Month, _ToDate.Year);
                            cxR_Education_Contract1.SetParameterValue("ToDate", ToDate);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thời gian hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var EducationId = Convert.ToInt32(dr["EducationHighestLevelId"]);
                        var _EducationName =
                            CandidateTrainingJobHistoryBLL.GetById(Convert.ToInt32(dr["EducationHighestLevelId"]))[0]
                                .Training_Job;
                        cxR_Education_Contract1.SetParameterValue("Education", _EducationName);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có trình độ học vấn");
                    }

                    try
                    {
                        if (dr["Resident"].ToString().Equals(string.Empty))
                        {
                            MessageBox.Show("Thiếu địa chỉ");
                        }
                        else
                        {
                            var Resident = dr["Resident"].ToString().Trim();
                            cxR_Education_Contract1.SetParameterValue("Resident", Resident);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thiếu địa chỉ");
                    }
                    var drFee = EducationFeeBLL.GetBySessionIdPositionId(Convert.ToInt32(dr["SessionId"]),
                        Convert.ToInt32(dr["PositionIdFee"]));
                    if (drFee != null)
                    {
                        try
                        {
                            if (drFee["Fee"].ToString().Length <= 0)
                            {
                                MessageBox.Show("Thiếu chi phí đào tạo");
                            }
                            else
                            {
                                var _Fee = string.Format("{0:#,###0}", drFee["Fee"].ToString().Trim());
                                cxR_Education_Contract1.SetParameterValue("Fee",
                                    StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(_Fee)));
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Thiếu chi phí đào tạo");
                        }

                        try
                        {
                            if (drFee["FeeInVietNamese"].ToString().Length <= 0)
                            {
                                MessageBox.Show("Thiếu chi phí đào tạo");
                            }
                            else
                            {
                                var _FeeVN = drFee["FeeInVietNamese"].ToString().Trim();
                                cxR_Education_Contract1.SetParameterValue("FeeVN", _FeeVN);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Thiếu chi phí đào tạo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thiếu chi phí đào tạo");
                    }

                    cxR_Education_Contract1.SetParameterValue("CandidateId", _CandidateId);
                }
                else
                {
                    MessageBox.Show("Chưa tạo hợp đồng!");
                }
            }
        }

        private void SubContract_Parameter()
        {
            switch (clsGlobal.Server)
            {
                case "Server_SAGS":
                {
                    var dr = EmployeeSubContractBLL.DR_GetAllBySubContractId(_SubContractId);

                    var _FromDate = (Convert.ToDateTime(dr["FromDate"]) == FormatDate.GetSQLDateMinValue) ||
                                    (dr["FromDate"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["FromDate"]);
                    var _SubContractNo = dr["SubContractNo"] == DBNull.Value
                        ? ""
                        : StringFormat.SetContractNo(Convert.ToInt32(dr["SubContractNo"]));
                    var SubContractNumber = string.Format("SAGS{0}-{1}/PL-HĐLĐ", _FromDate.Year, _SubContractNo);
                    subcontract_Contract1.SetParameterValue("SubContractNumber", SubContractNumber);

                    var _Sex = dr["Sex"] == DBNull.Value ? true : Convert.ToBoolean(dr["Sex"]);
                    var Sex = _Sex ? "Ông" : "Bà";
                    subcontract_Contract1.SetParameterValue("Sex", Sex);

                    var FullName = dr["FullName"] == DBNull.Value ? "" : dr["FullName"].ToString().ToUpper().Trim();
                    subcontract_Contract1.SetParameterValue("FullName", FullName);

                    var _Birthday = (Convert.ToDateTime(dr["Birthday"]) == FormatDate.GetSQLDateMinValue) ||
                                    (dr["Birthday"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["Birthday"]);
                    var Birthday = string.Format("{0} tháng {1} năm {2}", _Birthday.Day, _Birthday.Month, _Birthday.Year);
                    subcontract_Contract1.SetParameterValue("Birthday", Birthday);

                    var BirthPlace = dr["BirthPlace"] == DBNull.Value ? "" : dr["BirthPlace"].ToString().Trim();
                    subcontract_Contract1.SetParameterValue("BirthPlace", BirthPlace);

                    var Nationality = dr["Nationality"] == DBNull.Value ? "" : dr["Nationality"].ToString().Trim();
                    subcontract_Contract1.SetParameterValue("Nationality", Nationality);

                    var _SubContractFromDate = (Convert.ToDateTime(dr["FromDate"]) == FormatDate.GetSQLDateMinValue) ||
                                               (dr["FromDate"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["FromDate"]);
                    subcontract_Contract1.SetParameterValue("SubContractFromDate",
                        _SubContractFromDate.ToString("dd/MM/yyyy"));

                    var _EducationName = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString().Trim();
                    var _HighestEducationName = dr["HighestLevelNameValue"] == DBNull.Value
                        ? ""
                        : dr["HighestLevelNameValue"].ToString().Trim();
                    var Education = "";
                    if (_EducationName.Contains(_HighestEducationName) || _HighestEducationName.Contains(_EducationName))
                        Education = _EducationName;
                    else
                        Education = string.Format("{0} {1}", _EducationName, _HighestEducationName);
                    subcontract_Contract1.SetParameterValue("Education", Education);

                    var _Resident = dr["Resident"] == DBNull.Value ? "" : dr["Resident"].ToString().Trim();
                    subcontract_Contract1.SetParameterValue("Resident", _Resident);

                    var _ID = dr["IdCard"] == DBNull.Value ? "" : dr["IdCard"].ToString().Trim();
                    subcontract_Contract1.SetParameterValue("ID", _ID);

                    var _DateOfIssue = (Convert.ToDateTime(dr["DateOfIssue"]) == FormatDate.GetSQLDateMinValue) ||
                                       (dr["DateOfIssue"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["DateOfIssue"]);
                    subcontract_Contract1.SetParameterValue("DateOfIssue", _DateOfIssue.ToString("dd/MM/yyyy"));

                    var _PlaceOfIssue = dr["PlaceOfIssue"] == DBNull.Value ? "" : dr["PlaceOfIssue"].ToString().Trim();
                    subcontract_Contract1.SetParameterValue("PlaceOfIssue", _PlaceOfIssue);

                    var drCNT = EmployeeContractBLL.GetActiveContractByUserIdToDT(_UserId);
                    var _ContractFromDate = (Convert.ToDateTime(drCNT["FromDate"]) == FormatDate.GetSQLDateMinValue) ||
                                            (drCNT["FromDate"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(drCNT["FromDate"]);
                    var _ContractNo = drCNT["ContractNo"] == DBNull.Value
                        ? ""
                        : StringFormat.SetContractNo(Convert.ToInt32(drCNT["ContractNo"]));
                    var ContractNumber = string.Format("SAGS{0}-{1}/HĐLĐ", _ContractFromDate.Year, _ContractNo);
                    subcontract_Contract1.SetParameterValue("ContractNo", ContractNumber);
                    subcontract_Contract1.SetParameterValue("ContractFromDate", _ContractFromDate.ToString("dd/MM/yyyy"));

                    var _SalaryValue = dr["SalaryValue"] == DBNull.Value
                        ? ""
                        : StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(dr["SalaryValue"].ToString().Trim()));


                    var _SalaryCode = dr["Code"] == DBNull.Value ? "" : dr["Code"].ToString().Trim();


                    var _SalaryLevel = dr["Value"] == DBNull.Value ? "" : dr["Value"].ToString().Trim();


                    var _Detail = dr["Detail"] == DBNull.Value ? "" : dr["Detail"].ToString().Trim();
                    var _Duration = dr["Duration"] == DBNull.Value ? "" : dr["Duration"].ToString().Trim();
                    var _ReplaceDuration =
                        _Duration.Replace("{14?SubContractFromDate}", _FromDate.ToString("dd/MM/yyyy"))
                            .Replace("{15?ContractNo}", ContractNumber);
                    {
                        if (_Detail == "")
                        {
                            var stD =
                                "Căn cứ Nghị định số 122/2015/NĐ-CP ngày 14/11/2015 của Chính phủ Quy định mức lương tối thiểu vùng đối với người lao động làm việc ở doanh nghiệp, liên hiệp hợp tác xã, hợp tác xã, tổ hợp tác, trang trại, hộ gia đình, cá nhân và các cơ quan, tổ chức có sử dụng lao động theo hợp đồng lao động; có hiệu lực thi hành kể từ ngày 01/01/2016. Hai bên thoả thuận bổ sung Điều 3 của Hợp đồng lao động như sau:";
                            var ChangedSalary =
                                string.Format("  + Thay đổi mức lương: {0} đồng/ tháng, Mã số: {1}, Mức: {2}",
                                    _SalaryValue, _SalaryCode, _SalaryLevel);

                            subcontract_Contract1.SetParameterValue("ChangedSalary", ChangedSalary);
                            subcontract_Contract1.SetParameterValue("Detail", stD);
                        }
                        else
                        {
                            var stD = _Detail;
                            var ChangedSalary =
                                string.Format("  + Thay đổi mức lương: {0} đồng/ tháng, Mã số: {1}, Mức: {2}",
                                    _SalaryValue, _SalaryCode, _SalaryLevel);

                            subcontract_Contract1.SetParameterValue("ChangedSalary", ChangedSalary);
                            subcontract_Contract1.SetParameterValue("Detail", stD);
                        }
                    }

                    if (_Duration == "")
                    {
                        var stDu =
                            "Phụ lục hợp đồng này có giá trị kể từ ngày {16?SubContractFromDate}\r\nPhụ lục này là bộ phận của hợp đồng lao động số {17?ContractNo}, được làm thành hai bản có giá trị như nhau, mỗi bên giữ một bản và là cơ sở để giải quyết khi có tranh chấp lao động";
                        subcontract_Contract1.SetParameterValue("Duration",
                            stDu.Replace("{16?SubContractFromDate}", _FromDate.ToString("dd/MM/yyyy"))
                                .Replace("{17?ContractNo}", ContractNumber));
                    }
                    else
                    {
                        var stDu = _ReplaceDuration;
                        subcontract_Contract1.SetParameterValue("Duration", stDu);
                    }

                    subcontract_Contract1.SetParameterValue("UserId", _UserId);
                }
                    break;


                case "Server_DAD":
                {
                    var dr = EmployeeSubContractBLL.DR_GetAllBySubContractId(_SubContractId);

                    var _FromDate = (Convert.ToDateTime(dr["FromDate"]) == FormatDate.GetSQLDateMinValue) ||
                                    (dr["FromDate"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["FromDate"]);
                    var _SubContractNo = dr["SubContractNo"] == DBNull.Value
                        ? ""
                        : StringFormat.SetContractNo(Convert.ToInt32(dr["SubContractNo"]));
                    var SubContractNumber = string.Format("SAGS{0}-{1}/PL-HĐLĐ", _FromDate.Year, _SubContractNo);
                    daD_Subcontract_Contract1.SetParameterValue("SubContractNumber", SubContractNumber);

                    var _Sex = dr["Sex"] == DBNull.Value ? true : Convert.ToBoolean(dr["Sex"]);
                    var Sex = _Sex ? "Ông" : "Bà";
                    daD_Subcontract_Contract1.SetParameterValue("Sex", Sex);

                    var FullName = dr["FullName"] == DBNull.Value ? "" : dr["FullName"].ToString().ToUpper().Trim();
                    daD_Subcontract_Contract1.SetParameterValue("FullName", FullName);

                    var _Birthday = (Convert.ToDateTime(dr["Birthday"]) == FormatDate.GetSQLDateMinValue) ||
                                    (dr["Birthday"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["Birthday"]);
                    var Birthday = string.Format("{0} tháng {1} năm {2}", _Birthday.Day, _Birthday.Month, _Birthday.Year);
                    daD_Subcontract_Contract1.SetParameterValue("Birthday", Birthday);

                    var BirthPlace = dr["BirthPlace"] == DBNull.Value ? "" : dr["BirthPlace"].ToString().Trim();
                    daD_Subcontract_Contract1.SetParameterValue("BirthPlace", BirthPlace);

                    var Nationality = dr["Nationality"] == DBNull.Value ? "" : dr["Nationality"].ToString().Trim();
                    daD_Subcontract_Contract1.SetParameterValue("Nationality", Nationality);

                    var _SubContractFromDate = (Convert.ToDateTime(dr["FromDate"]) == FormatDate.GetSQLDateMinValue) ||
                                               (dr["FromDate"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["FromDate"]);
                    daD_Subcontract_Contract1.SetParameterValue("SubContractFromDate",
                        _SubContractFromDate.ToString("dd/MM/yyyy"));

                    var _EducationName = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString().Trim();
                    var _HighestEducationName = dr["HighestLevelNameValue"] == DBNull.Value
                        ? ""
                        : dr["HighestLevelNameValue"].ToString().Trim();
                    var Education = "";
                    if (_EducationName.Contains(_HighestEducationName) || _HighestEducationName.Contains(_EducationName))
                        Education = _EducationName;
                    else
                        Education = string.Format("{0} {1}", _EducationName, _HighestEducationName);
                    daD_Subcontract_Contract1.SetParameterValue("Education", Education);

                    var _Resident = dr["Resident"] == DBNull.Value ? "" : dr["Resident"].ToString().Trim();
                    daD_Subcontract_Contract1.SetParameterValue("Resident", _Resident);

                    var _ID = dr["IdCard"] == DBNull.Value ? "" : dr["IdCard"].ToString().Trim();
                    daD_Subcontract_Contract1.SetParameterValue("ID", _ID);

                    var _DateOfIssue = (Convert.ToDateTime(dr["DateOfIssue"]) == FormatDate.GetSQLDateMinValue) ||
                                       (dr["DateOfIssue"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["DateOfIssue"]);
                    daD_Subcontract_Contract1.SetParameterValue("DateOfIssue", _DateOfIssue.ToString("dd/MM/yyyy"));

                    var _PlaceOfIssue = dr["PlaceOfIssue"] == DBNull.Value ? "" : dr["PlaceOfIssue"].ToString().Trim();
                    daD_Subcontract_Contract1.SetParameterValue("PlaceOfIssue", _PlaceOfIssue);

                    var drCNT = EmployeeContractBLL.GetActiveContractByUserIdToDT(_UserId);
                    var _ContractFromDate = (Convert.ToDateTime(drCNT["FromDate"]) == FormatDate.GetSQLDateMinValue) ||
                                            (drCNT["FromDate"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(drCNT["FromDate"]);
                    var _ContractNo = drCNT["ContractNo"] == DBNull.Value
                        ? ""
                        : StringFormat.SetContractNo(Convert.ToInt32(drCNT["ContractNo"]));
                    var ContractNumber = string.Format("SAGS{0}-{1}/HĐLĐ", _ContractFromDate.Year, _ContractNo);
                    daD_Subcontract_Contract1.SetParameterValue("ContractNo", ContractNumber);
                    daD_Subcontract_Contract1.SetParameterValue("ContractFromDate",
                        _ContractFromDate.ToString("dd/MM/yyyy"));

                    var _SalaryValue = dr["SalaryValue"] == DBNull.Value
                        ? ""
                        : StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(dr["SalaryValue"].ToString().Trim()));


                    var _SalaryCode = dr["Code"] == DBNull.Value ? "" : dr["Code"].ToString().Trim();


                    var _SalaryLevel = dr["Value"] == DBNull.Value ? "" : dr["Value"].ToString().Trim();


                    var _Detail = dr["Detail"] == DBNull.Value ? "" : dr["Detail"].ToString().Trim();
                    var _Duration = dr["Duration"] == DBNull.Value ? "" : dr["Duration"].ToString().Trim();
                    var _ReplaceDuration =
                        _Duration.Replace("{14?SubContractFromDate}", _FromDate.ToString("dd/MM/yyyy"))
                            .Replace("{15?ContractNo}", ContractNumber);
                    {
                        if (_Detail == "")
                        {
                            var stD =
                                "Căn cứ Nghị định số 122/2015/NĐ-CP ngày 14/11/2015 của Chính phủ Quy định mức lương tối thiểu vùng đối với người lao động làm việc ở doanh nghiệp, liên hiệp hợp tác xã, hợp tác xã, tổ hợp tác, trang trại, hộ gia đình, cá nhân và các cơ quan, tổ chức có sử dụng lao động theo hợp đồng lao động; có hiệu lực thi hành kể từ ngày 01/01/2016. Hai bên thoả thuận bổ sung Điều 3 của Hợp đồng lao động như sau:";
                            var ChangedSalary =
                                string.Format("  + Thay đổi mức lương: {0} đồng/ tháng, Mã số: {1}, Mức: {2}",
                                    _SalaryValue, _SalaryCode, _SalaryLevel);

                            daD_Subcontract_Contract1.SetParameterValue("ChangedSalary", ChangedSalary);
                            daD_Subcontract_Contract1.SetParameterValue("Detail", stD);
                        }
                        else
                        {
                            var stD = _Detail;
                            var ChangedSalary =
                                string.Format("  + Thay đổi mức lương: {0} đồng/ tháng, Mã số: {1}, Mức: {2}",
                                    _SalaryValue, _SalaryCode, _SalaryLevel);

                            daD_Subcontract_Contract1.SetParameterValue("ChangedSalary", ChangedSalary);
                            daD_Subcontract_Contract1.SetParameterValue("Detail", stD);
                        }
                    }

                    if (_Duration == "")
                    {
                        var stDu =
                            "Phụ lục hợp đồng này có giá trị kể từ ngày {16?SubContractFromDate}\r\nPhụ lục này là bộ phận của hợp đồng lao động số {17?ContractNo}, được làm thành hai bản có giá trị như nhau, mỗi bên giữ một bản và là cơ sở để giải quyết khi có tranh chấp lao động";
                        daD_Subcontract_Contract1.SetParameterValue("Duration",
                            stDu.Replace("{16?SubContractFromDate}", _FromDate.ToString("dd/MM/yyyy"))
                                .Replace("{17?ContractNo}", ContractNumber));
                    }
                    else
                    {
                        var stDu = _ReplaceDuration;
                        daD_Subcontract_Contract1.SetParameterValue("Duration", stDu);
                    }

                    daD_Subcontract_Contract1.SetParameterValue("UserId", _UserId);
                }
                    break;


                case "Server_CXR":
                {
                    var dr = EmployeeSubContractBLL.DR_GetAllBySubContractId(_SubContractId);

                    var _FromDate = (Convert.ToDateTime(dr["FromDate"]) == FormatDate.GetSQLDateMinValue) ||
                                    (dr["FromDate"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["FromDate"]);
                    var _SubContractNo = dr["SubContractNo"] == DBNull.Value
                        ? ""
                        : StringFormat.SetContractNo(Convert.ToInt32(dr["SubContractNo"]));
                    var SubContractNumber = string.Format("SAGS-CXR{0}-{1}/PL-HĐLĐ", _FromDate.Year, _SubContractNo);
                    cxR_Subcontract_Contract1.SetParameterValue("SubContractNumber", SubContractNumber);

                    var _Sex = dr["Sex"] == DBNull.Value ? true : Convert.ToBoolean(dr["Sex"]);
                    var Sex = _Sex ? "Ông" : "Bà";
                    cxR_Subcontract_Contract1.SetParameterValue("Sex", Sex);

                    var FullName = dr["FullName"] == DBNull.Value ? "" : dr["FullName"].ToString().ToUpper().Trim();
                    cxR_Subcontract_Contract1.SetParameterValue("FullName", FullName);

                    var _Birthday = (Convert.ToDateTime(dr["Birthday"]) == FormatDate.GetSQLDateMinValue) ||
                                    (dr["Birthday"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["Birthday"]);
                    var Birthday = string.Format("{0} tháng {1} năm {2}", _Birthday.Day, _Birthday.Month, _Birthday.Year);
                    cxR_Subcontract_Contract1.SetParameterValue("Birthday", Birthday);

                    var BirthPlace = dr["BirthPlace"] == DBNull.Value ? "" : dr["BirthPlace"].ToString().Trim();
                    cxR_Subcontract_Contract1.SetParameterValue("BirthPlace", BirthPlace);

                    var Nationality = dr["Nationality"] == DBNull.Value ? "" : dr["Nationality"].ToString().Trim();
                    cxR_Subcontract_Contract1.SetParameterValue("Nationality", Nationality);

                    var _SubContractFromDate = (Convert.ToDateTime(dr["FromDate"]) == FormatDate.GetSQLDateMinValue) ||
                                               (dr["FromDate"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["FromDate"]);
                    cxR_Subcontract_Contract1.SetParameterValue("SubContractFromDate",
                        _SubContractFromDate.ToString("dd/MM/yyyy"));

                    var _EducationName = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString().Trim();
                    var _HighestEducationName = dr["HighestLevelNameValue"] == DBNull.Value
                        ? ""
                        : dr["HighestLevelNameValue"].ToString().Trim();
                    var Education = "";
                    if (_EducationName.Contains(_HighestEducationName) || _HighestEducationName.Contains(_EducationName))
                        Education = _EducationName;
                    else
                        Education = string.Format("{0} {1}", _EducationName, _HighestEducationName);
                    cxR_Subcontract_Contract1.SetParameterValue("Education", Education);

                    var _Resident = dr["Resident"] == DBNull.Value ? "" : dr["Resident"].ToString().Trim();
                    cxR_Subcontract_Contract1.SetParameterValue("Resident", _Resident);

                    var _ID = dr["IdCard"] == DBNull.Value ? "" : dr["IdCard"].ToString().Trim();
                    cxR_Subcontract_Contract1.SetParameterValue("ID", _ID);

                    var _DateOfIssue = (Convert.ToDateTime(dr["DateOfIssue"]) == FormatDate.GetSQLDateMinValue) ||
                                       (dr["DateOfIssue"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr["DateOfIssue"]);
                    cxR_Subcontract_Contract1.SetParameterValue("DateOfIssue", _DateOfIssue.ToString("dd/MM/yyyy"));

                    var _PlaceOfIssue = dr["PlaceOfIssue"] == DBNull.Value ? "" : dr["PlaceOfIssue"].ToString().Trim();
                    cxR_Subcontract_Contract1.SetParameterValue("PlaceOfIssue", _PlaceOfIssue);

                    var drCNT = EmployeeContractBLL.GetActiveContractByUserIdToDT(_UserId);
                    var _ContractFromDate = (Convert.ToDateTime(drCNT["FromDate"]) == FormatDate.GetSQLDateMinValue) ||
                                            (drCNT["FromDate"] == DBNull.Value)
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(drCNT["FromDate"]);
                    var _ContractNo = drCNT["ContractNo"] == DBNull.Value
                        ? ""
                        : StringFormat.SetContractNo(Convert.ToInt32(drCNT["ContractNo"]));
                    var ContractNumber = string.Format("SAGS-CXR{0}-{1}/HĐLĐ", _ContractFromDate.Year, _ContractNo);
                    cxR_Subcontract_Contract1.SetParameterValue("ContractNo", ContractNumber);
                    cxR_Subcontract_Contract1.SetParameterValue("ContractFromDate",
                        _ContractFromDate.ToString("dd/MM/yyyy"));

                    var _SalaryValue = dr["SalaryValue"] == DBNull.Value
                        ? ""
                        : StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(dr["SalaryValue"].ToString().Trim()));


                    var _SalaryCode = dr["Code"] == DBNull.Value ? "" : dr["Code"].ToString().Trim();


                    var _SalaryLevel = dr["Value"] == DBNull.Value ? "" : dr["Value"].ToString().Trim();


                    var _Detail = dr["Detail"] == DBNull.Value ? "" : dr["Detail"].ToString().Trim();
                    var _Duration = dr["Duration"] == DBNull.Value ? "" : dr["Duration"].ToString().Trim();
                    var _ReplaceDuration =
                        _Duration.Replace("{14?SubContractFromDate}", _FromDate.ToString("dd/MM/yyyy"))
                            .Replace("{15?ContractNo}", ContractNumber);
                    {
                        if (_Detail == "")
                        {
                            var stD =
                                "Căn cứ Nghị định số 122/2015/NĐ-CP ngày 14/11/2015 của Chính phủ Quy định mức lương tối thiểu vùng đối với người lao động làm việc ở doanh nghiệp, liên hiệp hợp tác xã, hợp tác xã, tổ hợp tác, trang trại, hộ gia đình, cá nhân và các cơ quan, tổ chức có sử dụng lao động theo hợp đồng lao động; có hiệu lực thi hành kể từ ngày 01/01/2016. Hai bên thoả thuận bổ sung Điều 3 của Hợp đồng lao động như sau:";
                            var ChangedSalary =
                                string.Format("  + Thay đổi mức lương: {0} đồng/ tháng, Mã số: {1}, Mức: {2}",
                                    _SalaryValue, _SalaryCode, _SalaryLevel);

                            cxR_Subcontract_Contract1.SetParameterValue("ChangedSalary", ChangedSalary);
                            cxR_Subcontract_Contract1.SetParameterValue("Detail", stD);
                        }
                        else
                        {
                            var stD = _Detail;
                            var ChangedSalary =
                                string.Format("  + Thay đổi mức lương: {0} đồng/ tháng, Mã số: {1}, Mức: {2}",
                                    _SalaryValue, _SalaryCode, _SalaryLevel);

                            cxR_Subcontract_Contract1.SetParameterValue("ChangedSalary", ChangedSalary);
                            cxR_Subcontract_Contract1.SetParameterValue("Detail", stD);
                        }
                    }

                    if (_Duration == "")
                    {
                        var stDu =
                            "Phụ lục hợp đồng này có giá trị kể từ ngày {16?SubContractFromDate}\r\nPhụ lục này là bộ phận của hợp đồng lao động số {17?ContractNo}, được làm thành hai bản có giá trị như nhau, mỗi bên giữ một bản và là cơ sở để giải quyết khi có tranh chấp lao động";
                        cxR_Subcontract_Contract1.SetParameterValue("Duration",
                            stDu.Replace("{16?SubContractFromDate}", _FromDate.ToString("dd/MM/yyyy"))
                                .Replace("{17?ContractNo}", ContractNumber));
                    }
                    else
                    {
                        var stDu = _ReplaceDuration;
                        cxR_Subcontract_Contract1.SetParameterValue("Duration", stDu);
                    }

                    cxR_Subcontract_Contract1.SetParameterValue("UserId", _UserId);
                }
                    break;
            }
        }

        private void ContractTrial_PassParameters()
        {
            switch (clsGlobal.Server)
            {
                case "Server_SAGS":
                {
                    var drCNT = EmployeeContractBLL.GetContractById(_ContractId);

                    contract_Trial1.SetParameterValue("SAGS", "SAGS" + Convert.ToDateTime(drCNT["FromDate"]).Year);
                    try
                    {
                        contract_Trial1.SetParameterValue("ContractNo",
                            StringFormat.SetContractNo(int.Parse(drCNT["ContractNo"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("TenHD", drCNT["ContractTitle"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Tiêu đề hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var Sex = int.Parse(drCNT["Sex"].ToString());
                        if (Sex == 1)
                            contract_Trial1.SetParameterValue("GioiTinh", "Ông");
                        else
                            contract_Trial1.SetParameterValue("GioiTinh", "Bà");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có giới tính");
                    }
                    try
                    {
                        contract_Trial1.SetParameterValue("HoTen", drCNT["FullName"].ToString().ToUpper());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có họ tên");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("QuocTich", drCNT["Nationality"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có quốc tịch");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("NoiSinh", drCNT["BirthPlace"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi sinh");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("NgaySinh",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày sinh");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("ThangSinh",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có tháng sinh");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("NamSinh",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có năm sinh");
                    }

                    try
                    {
                        if ((drCNT["HighestLevelNameValue"].ToString() == "") ||
                            (drCNT["HighestLevelNameValue"].ToString() == null))
                        {
                            contract_Trial1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                        }
                        else
                        {
                            if (drCNT["HighestLevelName"].ToString().Equals(drCNT["HighestLevelNameValue"].ToString()))
                                contract_Trial1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                            else
                                contract_Trial1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"] + " - " + drCNT["HighestLevelNameValue"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nghề nghiệp");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("DiaChi", drCNT["Resident"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có dịa chỉ");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("CMND", drCNT["IdCard"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có CMND");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("NgayCap",
                            DateTime.Parse(drCNT["DateOfIssue"].ToString()).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày cấp");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("NoiCap", drCNT["PlaceOfIssue"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi cấp");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("LoaiHD", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }

                    try
                    {
                        var str17 = drCNT["ContractTypeCode"].ToString() == "HDKX"
                            ? string.Format("Từ ngày {0} tháng {1} năm {2}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())))
                            : string.Format("Từ ngày {0} tháng {1} năm {2} đến ngày {3} tháng {4} năm {5}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:dd}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["ToDate"].ToString())));
                        contract_Trial1.SetParameterValue("TNgay", str17);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian HĐ");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("ChucDanhChuyenMon",
                            Get_ScaleOfSalaryName(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có chức danh chuyên môn");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("CongViecPhaiLam",
                            Get_ScaleOfSalaryJobDescription(int.Parse(drCNT["ScaleOfSalaryId"].ToString()))
                                .Replace("- ", "+ "));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mô tả công việc");
                    }

                    try
                    {
                        var str22 = string.Format("{0} giờ/ ngày và {1} giờ/ tuần", drCNT["Overtime"],
                            drCNT["WorkingHour"]);
                        contract_Trial1.SetParameterValue("ThoiGioLamViec", str22);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian làm việc");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("HDTNgay",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("HDTThang",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("HDTNam",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    var Salary = Get_ScaleOfSalaryValue(int.Parse(drCNT["ScaleOfSalaryId"].ToString()),
                        int.Parse(drCNT["SalaryLevel"].ToString()));

                    try
                    {
                        contract_Trial1.SetParameterValue("MucLuong",
                            StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(Salary*0.85)) + "đồng/ tháng (85% x " +
                            StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(Salary)) + ")");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("MaLuong",
                            Get_ScaleOfSalaryCode(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mã lương");
                    }

                    try
                    {
                        contract_Trial1.SetParameterValue("Muc", drCNT["SalaryLevel"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        var str36 = int.Parse(drCNT["DirectWorking"].ToString()) == 1
                            ? " Nghỉ tuần: 1 ngày/tuần hoặc bình quân 4 ngày/tháng."
                            : " Nghỉ tuần: Thứ 7 và chủ nhật hàng tuần.";
                        contract_Trial1.SetParameterValue("NgayNghi", str36);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    var Len = "theo Quyết định số /QĐ-SAGS ngày ".Length;
                    if (drCNT["Office"].ToString().Length > Len)
                        contract_Trial1.SetParameterValue("ChucVu", drCNT["Office"].ToString());
                    else
                        contract_Trial1.SetParameterValue("ChucVu", "");


                    contract_Trial1.SetParameterValue("UserId", _UserId);
                }
                    break;


                case "Server_DAD":
                {
                    var drCNT = EmployeeContractBLL.GetContractById(_ContractId);

                    daD_Contract_Trial1.SetParameterValue("SAGS", "SAGS" + Convert.ToDateTime(drCNT["FromDate"]).Year);
                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("ContractNo",
                            StringFormat.SetContractNo(int.Parse(drCNT["ContractNo"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("TenHD", drCNT["ContractTitle"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Tiêu đề hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var Sex = int.Parse(drCNT["Sex"].ToString());
                        if (Sex == 1)
                            daD_Contract_Trial1.SetParameterValue("GioiTinh", "Ông");
                        else
                            daD_Contract_Trial1.SetParameterValue("GioiTinh", "Bà");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có giới tính");
                    }
                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("HoTen", drCNT["FullName"].ToString().ToUpper());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có họ tên");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("QuocTich", drCNT["Nationality"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có quốc tịch");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("NoiSinh", drCNT["BirthPlace"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi sinh");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("NgaySinh",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày sinh");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("ThangSinh",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có tháng sinh");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("NamSinh",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có năm sinh");
                    }

                    try
                    {
                        if ((drCNT["HighestLevelNameValue"].ToString() == "") ||
                            (drCNT["HighestLevelNameValue"].ToString() == null))
                        {
                            daD_Contract_Trial1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                        }
                        else
                        {
                            if (drCNT["HighestLevelName"].ToString().Equals(drCNT["HighestLevelNameValue"].ToString()))
                                daD_Contract_Trial1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                            else
                                daD_Contract_Trial1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"] + " - " + drCNT["HighestLevelNameValue"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nghề nghiệp");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("DiaChi", drCNT["Resident"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có dịa chỉ");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("CMND", drCNT["IdCard"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có CMND");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("NgayCap",
                            DateTime.Parse(drCNT["DateOfIssue"].ToString()).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày cấp");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("NoiCap", drCNT["PlaceOfIssue"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi cấp");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("LoaiHD", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }

                    try
                    {
                        var str17 = drCNT["ContractTypeCode"].ToString() == "HDKX"
                            ? string.Format("Từ ngày {0} tháng {1} năm {2}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())))
                            : string.Format("Từ ngày {0} tháng {1} năm {2} đến ngày {3} tháng {4} năm {5}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:dd}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["ToDate"].ToString())));
                        daD_Contract_Trial1.SetParameterValue("TNgay", str17);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian HĐ");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("ChucDanhChuyenMon",
                            Get_ScaleOfSalaryName(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có chức danh chuyên môn");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("CongViecPhaiLam",
                            Get_ScaleOfSalaryJobDescription(int.Parse(drCNT["ScaleOfSalaryId"].ToString()))
                                .Replace("- ", "+ "));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mô tả công việc");
                    }

                    try
                    {
                        var str22 = string.Format("{0} giờ/ ngày và {1} giờ/ tuần", drCNT["Overtime"],
                            drCNT["WorkingHour"]);
                        daD_Contract_Trial1.SetParameterValue("ThoiGioLamViec", str22);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian làm việc");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("HDTNgay",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("HDTThang",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("HDTNam",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    var Salary = Get_ScaleOfSalaryValue(int.Parse(drCNT["ScaleOfSalaryId"].ToString()),
                        int.Parse(drCNT["SalaryLevel"].ToString()));

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("MucLuong",
                            StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(Salary*0.85)) + "đồng/ tháng (85% x " +
                            StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(Salary)) + ")");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("MaLuong",
                            Get_ScaleOfSalaryCode(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mã lương");
                    }

                    try
                    {
                        daD_Contract_Trial1.SetParameterValue("Muc", drCNT["SalaryLevel"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        var str36 = int.Parse(drCNT["DirectWorking"].ToString()) == 1
                            ? " Nghỉ tuần: 1 ngày/tuần hoặc bình quân 4 ngày/tháng."
                            : " Nghỉ tuần: Thứ 7 và chủ nhật hàng tuần.";
                        daD_Contract_Trial1.SetParameterValue("NgayNghi", str36);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    var Len = "theo Quyết định số /QĐ-SAGS ngày ".Length;
                    if (drCNT["Office"].ToString().Length > Len)
                        daD_Contract_Trial1.SetParameterValue("ChucVu", drCNT["Office"].ToString());
                    else
                        daD_Contract_Trial1.SetParameterValue("ChucVu", "");


                    daD_Contract_Trial1.SetParameterValue("UserId", _UserId);
                }
                    break;


                case "Server_CXR":
                {
                    var drCNT = EmployeeContractBLL.GetContractById(_ContractId);

                    cxR_Contract_Trial1.SetParameterValue("SAGS",
                        "SAGS-CXR" + Convert.ToDateTime(drCNT["FromDate"]).Year);
                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("ContractNo",
                            StringFormat.SetContractNo(int.Parse(drCNT["ContractNo"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("TenHD", drCNT["ContractTitle"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Tiêu đề hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var Sex = int.Parse(drCNT["Sex"].ToString());
                        if (Sex == 1)
                            cxR_Contract_Trial1.SetParameterValue("GioiTinh", "Ông");
                        else
                            cxR_Contract_Trial1.SetParameterValue("GioiTinh", "Bà");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có giới tính");
                    }
                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("HoTen", drCNT["FullName"].ToString().ToUpper());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có họ tên");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("QuocTich", drCNT["Nationality"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có quốc tịch");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("NoiSinh", drCNT["BirthPlace"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi sinh");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("NgaySinh",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày sinh");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("ThangSinh",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có tháng sinh");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("NamSinh",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có năm sinh");
                    }

                    try
                    {
                        if ((drCNT["HighestLevelNameValue"].ToString() == "") ||
                            (drCNT["HighestLevelNameValue"].ToString() == null))
                        {
                            cxR_Contract_Trial1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                        }
                        else
                        {
                            if (drCNT["HighestLevelName"].ToString().Equals(drCNT["HighestLevelNameValue"].ToString()))
                                cxR_Contract_Trial1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                            else
                                cxR_Contract_Trial1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"] + " - " + drCNT["HighestLevelNameValue"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nghề nghiệp");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("DiaChi", drCNT["Resident"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có dịa chỉ");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("CMND", drCNT["IdCard"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có CMND");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("NgayCap",
                            DateTime.Parse(drCNT["DateOfIssue"].ToString()).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày cấp");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("NoiCap", drCNT["PlaceOfIssue"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi cấp");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("LoaiHD", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }

                    try
                    {
                        var str17 = drCNT["ContractTypeCode"].ToString() == "HDKX"
                            ? string.Format("Từ ngày {0} tháng {1} năm {2}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())))
                            : string.Format("Từ ngày {0} tháng {1} năm {2} đến ngày {3} tháng {4} năm {5}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:dd}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["ToDate"].ToString())));
                        cxR_Contract_Trial1.SetParameterValue("TNgay", str17);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian HĐ");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("ChucDanhChuyenMon",
                            Get_ScaleOfSalaryName(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có chức danh chuyên môn");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("CongViecPhaiLam",
                            Get_ScaleOfSalaryJobDescription(int.Parse(drCNT["ScaleOfSalaryId"].ToString()))
                                .Replace("- ", "+ "));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mô tả công việc");
                    }

                    try
                    {
                        var str22 = string.Format("{0} giờ/ ngày và {1} giờ/ tuần", drCNT["Overtime"],
                            drCNT["WorkingHour"]);
                        cxR_Contract_Trial1.SetParameterValue("ThoiGioLamViec", str22);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian làm việc");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("HDTNgay",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("HDTThang",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("HDTNam",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    var Salary = Get_ScaleOfSalaryValue(int.Parse(drCNT["ScaleOfSalaryId"].ToString()),
                        int.Parse(drCNT["SalaryLevel"].ToString()));

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("MucLuong",
                            StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(Salary*0.85)) + "đồng/ tháng (85% x " +
                            StringFormat.SetFormatMoneyFinal(Convert.ToDecimal(Salary)) + ")");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("MaLuong",
                            Get_ScaleOfSalaryCode(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mã lương");
                    }

                    try
                    {
                        cxR_Contract_Trial1.SetParameterValue("Muc", drCNT["SalaryLevel"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        var str36 = int.Parse(drCNT["DirectWorking"].ToString()) == 1
                            ? " Nghỉ tuần: 1 ngày/tuần hoặc bình quân 4 ngày/tháng."
                            : " Nghỉ tuần: Thứ 7 và chủ nhật hàng tuần.";
                        cxR_Contract_Trial1.SetParameterValue("NgayNghi", str36);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    var Len = "theo Quyết định số /QĐ-SAGS ngày ".Length;
                    if (drCNT["Office"].ToString().Length > Len)
                        cxR_Contract_Trial1.SetParameterValue("ChucVu", drCNT["Office"].ToString());
                    else
                        cxR_Contract_Trial1.SetParameterValue("ChucVu", "");


                    cxR_Contract_Trial1.SetParameterValue("UserId", _UserId);
                }
                    break;
            }
        }

        private void Contract_Parameter()
        {
            switch (clsGlobal.Server)
            {
                case "Server_SAGS":
                {
                    var drCNT = EmployeeContractBLL.GetContractById(_ContractId);

                    contract1.SetParameterValue("SAGS", "SAGS" + Convert.ToDateTime(drCNT["FromDate"]).Year);
                    try
                    {
                        contract1.SetParameterValue("ContractNo",
                            StringFormat.SetContractNo(int.Parse(drCNT["ContractNo"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        contract1.SetParameterValue("TenHD", drCNT["ContractTitle"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Tiêu đề hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var Sex = int.Parse(drCNT["Sex"].ToString());
                        if (Sex == 1)
                            contract1.SetParameterValue("GioiTinh", "Ông");
                        else
                            contract1.SetParameterValue("GioiTinh", "Bà");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có giới tính");
                    }

                    try
                    {
                        contract1.SetParameterValue("HoTen", drCNT["FullName"].ToString().ToUpper());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có họ tên");
                    }

                    try
                    {
                        contract1.SetParameterValue("QuocTich", drCNT["Nationality"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có quốc tịch");
                    }

                    try
                    {
                        contract1.SetParameterValue("NoiSinh", drCNT["BirthPlace"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi sinh");
                    }

                    try
                    {
                        contract1.SetParameterValue("NgaySinh",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày sinh");
                    }

                    try
                    {
                        contract1.SetParameterValue("ThangSinh",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có tháng sinh");
                    }

                    try
                    {
                        contract1.SetParameterValue("NamSinh",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có năm sinh");
                    }

                    try
                    {
                        if ((drCNT["HighestLevelNameValue"].ToString() == "") ||
                            (drCNT["HighestLevelNameValue"].ToString() == null))
                        {
                            contract1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                        }
                        else
                        {
                            if (drCNT["HighestLevelName"].ToString().Equals(drCNT["HighestLevelNameValue"].ToString()))
                                contract1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                            else
                                contract1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"] + " - " + drCNT["HighestLevelNameValue"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nghề nghiệp");
                    }

                    try
                    {
                        contract1.SetParameterValue("DiaChi", drCNT["Resident"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có dịa chỉ");
                    }

                    try
                    {
                        contract1.SetParameterValue("CMND", drCNT["IdCard"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có CMND");
                    }

                    try
                    {
                        contract1.SetParameterValue("NgayCap",
                            DateTime.Parse(drCNT["DateOfIssue"].ToString()).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày cấp");
                    }

                    try
                    {
                        contract1.SetParameterValue("NoiCap", drCNT["PlaceOfIssue"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi cấp");
                    }

                    try
                    {
                        contract1.SetParameterValue("LoaiHD", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }

                    if ((Convert.ToInt32(drCNT["ContractTypeId"]) == 6) ||
                        (drCNT["ContractTypeCode"].ToString() == "HDTV"))
                        contract1.SetParameterValue("ContractFullName", "");
                    else
                        try
                        {
                            contract1.SetParameterValue("ContractFullName", "(" + drCNT["ContractFullName"] + ")");
                        }
                        catch
                        {
                            MessageBox.Show("Chưa có loại HĐ");
                        }
                    try
                    {
                        var str17 = drCNT["ContractTypeCode"].ToString() == "HDKX"
                            ? string.Format("Từ ngày {0} tháng {1} năm {2}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())))
                            : string.Format("Từ ngày {0} tháng {1} năm {2} đến ngày {3} tháng {4} năm {5}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:dd}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["ToDate"].ToString())));
                        contract1.SetParameterValue("TNgay", str17);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian HĐ");
                    }

                    try
                    {
                        contract1.SetParameterValue("ChucDanhChuyenMon",
                            Get_ScaleOfSalaryName(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có chức danh chuyên môn");
                    }

                    try
                    {
                        contract1.SetParameterValue("CongViecPhaiLam",
                            Get_ScaleOfSalaryJobDescription(int.Parse(drCNT["ScaleOfSalaryId"].ToString()))
                                .Replace("- ", "+ "));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mô tả công việc");
                    }

                    try
                    {
                        var str22 = string.Format("{0} giờ/ ngày và {1} giờ/ tuần", drCNT["Overtime"],
                            drCNT["WorkingHour"]);
                        contract1.SetParameterValue("ThoiGioLamViec", str22);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian làm việc");
                    }

                    try
                    {
                        var str23 = "";
                        if (int.Parse(drCNT["DirectWorking"].ToString()) == 0)
                            if ((int.Parse(drCNT["DepartmentId"].ToString()) == 60) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 63) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 64) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 143))
                                str23 =
                                    " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                            else
                                str23 =
                                    " Ngoài thời giờ làm việc theo qui định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo quy định của Bộ luật lao động không quá 12 giờ/ngày; tổng số giờ làm thêm không quá 30 giờ/ tháng và không quá 200 giờ/năm.";
                        else
                            str23 =
                                " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                        contract1.SetParameterValue("LamThem", str23);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        contract1.SetParameterValue("MucLuong",
                            StringFormat.SetFormatMoneyFinal(
                                Convert.ToDecimal(Get_ScaleOfSalaryValue(int.Parse(drCNT["ScaleOfSalaryId"].ToString()),
                                    int.Parse(drCNT["SalaryLevel"].ToString())))));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        contract1.SetParameterValue("MaLuong",
                            Get_ScaleOfSalaryCode(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mã lương");
                    }

                    try
                    {
                        contract1.SetParameterValue("Muc", drCNT["SalaryLevel"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        var str36 = int.Parse(drCNT["DirectWorking"].ToString()) == 1
                            ? " Nghỉ tuần: 1 ngày/tuần hoặc bình quân 4 ngày/tháng."
                            : " Nghỉ tuần: Thứ 7 và chủ nhật hàng tuần.";
                        contract1.SetParameterValue("NgayNghi", str36);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        contract1.SetParameterValue("HDTNgay",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        contract1.SetParameterValue("HDTThang",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        contract1.SetParameterValue("HDTNam",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    var Len = "theo Quyết định số /QĐ-SAGS ngày ".Length;
                    if (drCNT["Office"].ToString().Length > Len)
                        contract1.SetParameterValue("ChucVu", drCNT["Office"].ToString());
                    else
                        contract1.SetParameterValue("ChucVu", "");
                    contract1.SetParameterValue("UserId", _UserId);
                }
                    break;


                case "Server_DAD":
                {
                    var drCNT = EmployeeContractBLL.GetContractById(_ContractId);

                    daD_Contract1.SetParameterValue("SAGS", "SAGS" + Convert.ToDateTime(drCNT["FromDate"]).Year);
                    try
                    {
                        daD_Contract1.SetParameterValue("ContractNo",
                            StringFormat.SetContractNo(int.Parse(drCNT["ContractNo"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("TenHD", drCNT["ContractTitle"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Tiêu đề hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var Sex = int.Parse(drCNT["Sex"].ToString());
                        if (Sex == 1)
                            daD_Contract1.SetParameterValue("GioiTinh", "Ông");
                        else
                            daD_Contract1.SetParameterValue("GioiTinh", "Bà");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có giới tính");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("HoTen", drCNT["FullName"].ToString().ToUpper());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có họ tên");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("QuocTich", drCNT["Nationality"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có quốc tịch");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("NoiSinh", drCNT["BirthPlace"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi sinh");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("NgaySinh",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày sinh");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("ThangSinh",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có tháng sinh");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("NamSinh",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có năm sinh");
                    }

                    try
                    {
                        if ((drCNT["HighestLevelNameValue"].ToString() == "") ||
                            (drCNT["HighestLevelNameValue"].ToString() == null))
                        {
                            daD_Contract1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                        }
                        else
                        {
                            if (drCNT["HighestLevelName"].ToString().Equals(drCNT["HighestLevelNameValue"].ToString()))
                                daD_Contract1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                            else
                                daD_Contract1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"] + " - " + drCNT["HighestLevelNameValue"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nghề nghiệp");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("DiaChi", drCNT["Resident"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có dịa chỉ");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("CMND", drCNT["IdCard"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có CMND");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("NgayCap",
                            DateTime.Parse(drCNT["DateOfIssue"].ToString()).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày cấp");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("NoiCap", drCNT["PlaceOfIssue"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi cấp");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("LoaiHD", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }

                    if ((Convert.ToInt32(drCNT["ContractTypeId"]) == 6) ||
                        (drCNT["ContractTypeCode"].ToString() == "HDTV"))
                        daD_Contract1.SetParameterValue("ContractFullName", "");
                    else
                        try
                        {
                            daD_Contract1.SetParameterValue("ContractFullName", "(" + drCNT["ContractFullName"] + ")");
                        }
                        catch
                        {
                            MessageBox.Show("Chưa có loại HĐ");
                        }
                    try
                    {
                        var str17 = drCNT["ContractTypeCode"].ToString() == "HDKX"
                            ? string.Format("Từ ngày {0} tháng {1} năm {2}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())))
                            : string.Format("Từ ngày {0} tháng {1} năm {2} đến ngày {3} tháng {4} năm {5}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:dd}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["ToDate"].ToString())));
                        daD_Contract1.SetParameterValue("TNgay", str17);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian HĐ");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("ChucDanhChuyenMon",
                            Get_ScaleOfSalaryName(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có chức danh chuyên môn");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("CongViecPhaiLam",
                            Get_ScaleOfSalaryJobDescription(int.Parse(drCNT["ScaleOfSalaryId"].ToString()))
                                .Replace("- ", "+ "));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mô tả công việc");
                    }

                    try
                    {
                        var str22 = string.Format("{0} giờ/ ngày và {1} giờ/ tuần", drCNT["Overtime"],
                            drCNT["WorkingHour"]);
                        daD_Contract1.SetParameterValue("ThoiGioLamViec", str22);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian làm việc");
                    }

                    try
                    {
                        var str23 = "";
                        if (int.Parse(drCNT["DirectWorking"].ToString()) == 0)
                            if ((int.Parse(drCNT["DepartmentId"].ToString()) == 60) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 63) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 64) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 143))
                                str23 =
                                    " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                            else
                                str23 =
                                    " Ngoài thời giờ làm việc theo qui định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo quy định của Bộ luật lao động không quá 12 giờ/ngày; tổng số giờ làm thêm không quá 30 giờ/ tháng và không quá 200 giờ/năm.";
                        else
                            str23 =
                                " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                        daD_Contract1.SetParameterValue("LamThem", str23);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("MucLuong",
                            StringFormat.SetFormatMoneyFinal(
                                Convert.ToDecimal(Get_ScaleOfSalaryValue(int.Parse(drCNT["ScaleOfSalaryId"].ToString()),
                                    int.Parse(drCNT["SalaryLevel"].ToString())))));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("MaLuong",
                            Get_ScaleOfSalaryCode(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mã lương");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("Muc", drCNT["SalaryLevel"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        var str36 = int.Parse(drCNT["DirectWorking"].ToString()) == 1
                            ? " Nghỉ tuần: 1 ngày/tuần hoặc bình quân 4 ngày/tháng."
                            : " Nghỉ tuần: Thứ 7 và chủ nhật hàng tuần.";
                        daD_Contract1.SetParameterValue("NgayNghi", str36);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("HDTNgay",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("HDTThang",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        daD_Contract1.SetParameterValue("HDTNam",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    var Len = "theo Quyết định số /QĐ-SAGS ngày ".Length;
                    if (drCNT["Office"].ToString().Length > Len)
                        daD_Contract1.SetParameterValue("ChucVu", drCNT["Office"].ToString());
                    else
                        daD_Contract1.SetParameterValue("ChucVu", "");
                    daD_Contract1.SetParameterValue("UserId", _UserId);
                }
                    break;


                case "Server_CXR":
                {
                    var drCNT = EmployeeContractBLL.GetContractById(_ContractId);

                    cxR_Contract1.SetParameterValue("SAGS", "SAGS-CXR" + Convert.ToDateTime(drCNT["FromDate"]).Year);
                    try
                    {
                        cxR_Contract1.SetParameterValue("ContractNo",
                            StringFormat.SetContractNo(int.Parse(drCNT["ContractNo"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("TenHD", drCNT["ContractTitle"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Tiêu đề hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var Sex = int.Parse(drCNT["Sex"].ToString());
                        if (Sex == 1)
                            cxR_Contract1.SetParameterValue("GioiTinh", "Ông");
                        else
                            cxR_Contract1.SetParameterValue("GioiTinh", "Bà");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có giới tính");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("HoTen", drCNT["FullName"].ToString().ToUpper());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có họ tên");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("QuocTich", drCNT["Nationality"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có quốc tịch");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("NoiSinh", drCNT["BirthPlace"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi sinh");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("NgaySinh",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày sinh");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("ThangSinh",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có tháng sinh");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("NamSinh",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có năm sinh");
                    }

                    try
                    {
                        if ((drCNT["HighestLevelNameValue"].ToString() == "") ||
                            (drCNT["HighestLevelNameValue"].ToString() == null))
                        {
                            cxR_Contract1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                        }
                        else
                        {
                            if (drCNT["HighestLevelName"].ToString().Equals(drCNT["HighestLevelNameValue"].ToString()))
                                cxR_Contract1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                            else
                                cxR_Contract1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"] + " - " + drCNT["HighestLevelNameValue"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nghề nghiệp");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("DiaChi", drCNT["Resident"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có dịa chỉ");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("CMND", drCNT["IdCard"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có CMND");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("NgayCap",
                            DateTime.Parse(drCNT["DateOfIssue"].ToString()).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày cấp");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("NoiCap", drCNT["PlaceOfIssue"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi cấp");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("LoaiHD", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }

                    if ((Convert.ToInt32(drCNT["ContractTypeId"]) == 6) ||
                        (drCNT["ContractTypeCode"].ToString() == "HDTV"))
                        cxR_Contract1.SetParameterValue("ContractFullName", "");
                    else
                        try
                        {
                            cxR_Contract1.SetParameterValue("ContractFullName", "(" + drCNT["ContractFullName"] + ")");
                        }
                        catch
                        {
                            MessageBox.Show("Chưa có loại HĐ");
                        }
                    try
                    {
                        var str17 = drCNT["ContractTypeCode"].ToString() == "HDKX"
                            ? string.Format("Từ ngày {0} tháng {1} năm {2}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())))
                            : string.Format("Từ ngày {0} tháng {1} năm {2} đến ngày {3} tháng {4} năm {5}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:dd}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["ToDate"].ToString())));
                        cxR_Contract1.SetParameterValue("TNgay", str17);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian HĐ");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("ChucDanhChuyenMon",
                            Get_ScaleOfSalaryName(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có chức danh chuyên môn");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("CongViecPhaiLam",
                            Get_ScaleOfSalaryJobDescription(int.Parse(drCNT["ScaleOfSalaryId"].ToString()))
                                .Replace("- ", "+ "));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mô tả công việc");
                    }

                    try
                    {
                        var str22 = string.Format("{0} giờ/ ngày và {1} giờ/ tuần", drCNT["Overtime"],
                            drCNT["WorkingHour"]);
                        cxR_Contract1.SetParameterValue("ThoiGioLamViec", str22);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian làm việc");
                    }

                    try
                    {
                        var str23 = "";
                        if (int.Parse(drCNT["DirectWorking"].ToString()) == 0)
                            if ((int.Parse(drCNT["DepartmentId"].ToString()) == 60) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 63) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 64) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 143))
                                str23 =
                                    " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                            else
                                str23 =
                                    " Ngoài thời giờ làm việc theo qui định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo quy định của Bộ luật lao động không quá 12 giờ/ngày; tổng số giờ làm thêm không quá 30 giờ/ tháng và không quá 200 giờ/năm.";
                        else
                            str23 =
                                " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                        cxR_Contract1.SetParameterValue("LamThem", str23);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("MucLuong",
                            StringFormat.SetFormatMoneyFinal(
                                Convert.ToDecimal(Get_ScaleOfSalaryValue(int.Parse(drCNT["ScaleOfSalaryId"].ToString()),
                                    int.Parse(drCNT["SalaryLevel"].ToString())))));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("MaLuong",
                            Get_ScaleOfSalaryCode(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mã lương");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("Muc", drCNT["SalaryLevel"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        var str36 = int.Parse(drCNT["DirectWorking"].ToString()) == 1
                            ? " Nghỉ tuần: 1 ngày/tuần hoặc bình quân 4 ngày/tháng."
                            : " Nghỉ tuần: Thứ 7 và chủ nhật hàng tuần.";
                        cxR_Contract1.SetParameterValue("NgayNghi", str36);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("HDTNgay",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("HDTThang",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        cxR_Contract1.SetParameterValue("HDTNam",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    var Len = "theo Quyết định số /QĐ-SAGS ngày ".Length;
                    if (drCNT["Office"].ToString().Length > Len)
                        cxR_Contract1.SetParameterValue("ChucVu", drCNT["Office"].ToString());
                    else
                        cxR_Contract1.SetParameterValue("ChucVu", "");
                    cxR_Contract1.SetParameterValue("UserId", _UserId);
                }
                    break;
            }
        }

        private void ContractUnder3M_Parameter()
        {
            switch (clsGlobal.Server)
            {
                case "Server_SAGS":
                {
                    var drCNT = EmployeeContractBLL.GetContractById(_ContractId);

                    contract_Under3M1.SetParameterValue("SAGS", "SAGS" + Convert.ToDateTime(drCNT["FromDate"]).Year);
                    try
                    {
                        contract_Under3M1.SetParameterValue("ContractNo",
                            StringFormat.SetContractNo(int.Parse(drCNT["ContractNo"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("TenHD", drCNT["ContractTitle"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Tiêu đề hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var Sex = int.Parse(drCNT["Sex"].ToString());
                        if (Sex == 1)
                            contract_Under3M1.SetParameterValue("GioiTinh", "Ông");
                        else
                            contract_Under3M1.SetParameterValue("GioiTinh", "Bà");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có giới tính");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("HoTen", drCNT["FullName"].ToString().ToUpper());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có họ tên");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("QuocTich", drCNT["Nationality"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có quốc tịch");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("NoiSinh", drCNT["BirthPlace"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi sinh");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("NgaySinh",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày sinh");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("ThangSinh",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có tháng sinh");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("NamSinh",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có năm sinh");
                    }

                    try
                    {
                        if ((drCNT["HighestLevelNameValue"].ToString() == "") ||
                            (drCNT["HighestLevelNameValue"].ToString() == null))
                        {
                            contract_Under3M1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                        }
                        else
                        {
                            if (drCNT["HighestLevelName"].ToString().Equals(drCNT["HighestLevelNameValue"].ToString()))
                                contract_Under3M1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                            else
                                contract_Under3M1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"] + " - " + drCNT["HighestLevelNameValue"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nghề nghiệp");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("DiaChi", drCNT["Resident"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có dịa chỉ");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("CMND", drCNT["IdCard"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có CMND");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("NgayCap",
                            DateTime.Parse(drCNT["DateOfIssue"].ToString()).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày cấp");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("NoiCap", drCNT["PlaceOfIssue"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi cấp");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("LoaiHD", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("ContractFullName", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }


                    try
                    {
                        var str17 = drCNT["ContractTypeCode"].ToString() == "HDKX"
                            ? string.Format("Từ ngày {0} tháng {1} năm {2}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())))
                            : string.Format("Từ ngày {0} tháng {1} năm {2} đến ngày {3} tháng {4} năm {5}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:dd}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["ToDate"].ToString())));
                        contract_Under3M1.SetParameterValue("TNgay", str17);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian HĐ");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("ChucDanhChuyenMon",
                            Get_ScaleOfSalaryName(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có chức danh chuyên môn");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("CongViecPhaiLam",
                            Get_ScaleOfSalaryJobDescription(int.Parse(drCNT["ScaleOfSalaryId"].ToString()))
                                .Replace("- ", "+ "));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mô tả công việc");
                    }

                    try
                    {
                        var str22 = string.Format("{0} giờ/ ngày và {1} giờ/ tuần", drCNT["Overtime"],
                            drCNT["WorkingHour"]);
                        contract_Under3M1.SetParameterValue("ThoiGioLamViec", str22);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian làm việc");
                    }

                    try
                    {
                        var str23 = "";
                        if (int.Parse(drCNT["DirectWorking"].ToString()) == 0)
                            if ((int.Parse(drCNT["DepartmentId"].ToString()) == 60) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 63) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 64) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 143))
                                str23 =
                                    " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                            else
                                str23 =
                                    " Ngoài thời giờ làm việc theo qui định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo quy định của Bộ luật lao động không quá 12 giờ/ngày; tổng số giờ làm thêm không quá 30 giờ/ tháng và không quá 200 giờ/năm.";
                        else
                            str23 =
                                " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                        contract_Under3M1.SetParameterValue("LamThem", str23);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("MucLuong",
                            StringFormat.SetFormatMoneyFinal(
                                Convert.ToDecimal(Get_ScaleOfSalaryValue(int.Parse(drCNT["ScaleOfSalaryId"].ToString()),
                                    int.Parse(drCNT["SalaryLevel"].ToString())))));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("MaLuong",
                            Get_ScaleOfSalaryCode(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mã lương");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("Muc", drCNT["SalaryLevel"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        var str36 = int.Parse(drCNT["DirectWorking"].ToString()) == 1
                            ? " Nghỉ tuần: 1 ngày/tuần hoặc bình quân 4 ngày/tháng."
                            : " Nghỉ tuần: Thứ 7 và chủ nhật hàng tuần.";
                        contract_Under3M1.SetParameterValue("NgayNghi", str36);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("HDTNgay",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("HDTThang",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        contract_Under3M1.SetParameterValue("HDTNam",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    var Len = "theo Quyết định số /QĐ-SAGS ngày ".Length;
                    if (drCNT["Office"].ToString().Length > Len)
                        contract_Under3M1.SetParameterValue("ChucVu", drCNT["Office"].ToString());
                    else
                        contract_Under3M1.SetParameterValue("ChucVu", "");
                    contract_Under3M1.SetParameterValue("UserId", _UserId);
                }
                    break;


                case "Server_DAD":
                {
                    var drCNT = EmployeeContractBLL.GetContractById(_ContractId);

                    daD_Contract_Under3M1.SetParameterValue("SAGS", "SAGS" + Convert.ToDateTime(drCNT["FromDate"]).Year);
                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("ContractNo",
                            StringFormat.SetContractNo(int.Parse(drCNT["ContractNo"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("TenHD", drCNT["ContractTitle"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Tiêu đề hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var Sex = int.Parse(drCNT["Sex"].ToString());
                        if (Sex == 1)
                            daD_Contract_Under3M1.SetParameterValue("GioiTinh", "Ông");
                        else
                            daD_Contract_Under3M1.SetParameterValue("GioiTinh", "Bà");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có giới tính");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("HoTen", drCNT["FullName"].ToString().ToUpper());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có họ tên");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("QuocTich", drCNT["Nationality"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có quốc tịch");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("NoiSinh", drCNT["BirthPlace"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi sinh");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("NgaySinh",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày sinh");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("ThangSinh",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có tháng sinh");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("NamSinh",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có năm sinh");
                    }

                    try
                    {
                        if ((drCNT["HighestLevelNameValue"].ToString() == "") ||
                            (drCNT["HighestLevelNameValue"].ToString() == null))
                        {
                            daD_Contract_Under3M1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                        }
                        else
                        {
                            if (drCNT["HighestLevelName"].ToString().Equals(drCNT["HighestLevelNameValue"].ToString()))
                                daD_Contract_Under3M1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"].ToString());
                            else
                                daD_Contract_Under3M1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"] + " - " + drCNT["HighestLevelNameValue"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nghề nghiệp");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("DiaChi", drCNT["Resident"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có dịa chỉ");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("CMND", drCNT["IdCard"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có CMND");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("NgayCap",
                            DateTime.Parse(drCNT["DateOfIssue"].ToString()).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày cấp");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("NoiCap", drCNT["PlaceOfIssue"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi cấp");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("LoaiHD", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("ContractFullName", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }


                    try
                    {
                        var str17 = drCNT["ContractTypeCode"].ToString() == "HDKX"
                            ? string.Format("Từ ngày {0} tháng {1} năm {2}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())))
                            : string.Format("Từ ngày {0} tháng {1} năm {2} đến ngày {3} tháng {4} năm {5}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:dd}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["ToDate"].ToString())));
                        daD_Contract_Under3M1.SetParameterValue("TNgay", str17);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian HĐ");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("ChucDanhChuyenMon",
                            Get_ScaleOfSalaryName(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có chức danh chuyên môn");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("CongViecPhaiLam",
                            Get_ScaleOfSalaryJobDescription(int.Parse(drCNT["ScaleOfSalaryId"].ToString()))
                                .Replace("- ", "+ "));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mô tả công việc");
                    }

                    try
                    {
                        var str22 = string.Format("{0} giờ/ ngày và {1} giờ/ tuần", drCNT["Overtime"],
                            drCNT["WorkingHour"]);
                        daD_Contract_Under3M1.SetParameterValue("ThoiGioLamViec", str22);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian làm việc");
                    }

                    try
                    {
                        var str23 = "";
                        if (int.Parse(drCNT["DirectWorking"].ToString()) == 0)
                            if ((int.Parse(drCNT["DepartmentId"].ToString()) == 60) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 63) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 64) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 143))
                                str23 =
                                    " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                            else
                                str23 =
                                    " Ngoài thời giờ làm việc theo qui định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo quy định của Bộ luật lao động không quá 12 giờ/ngày; tổng số giờ làm thêm không quá 30 giờ/ tháng và không quá 200 giờ/năm.";
                        else
                            str23 =
                                " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                        daD_Contract_Under3M1.SetParameterValue("LamThem", str23);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("MucLuong",
                            StringFormat.SetFormatMoneyFinal(
                                Convert.ToDecimal(Get_ScaleOfSalaryValue(int.Parse(drCNT["ScaleOfSalaryId"].ToString()),
                                    int.Parse(drCNT["SalaryLevel"].ToString())))));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("MaLuong",
                            Get_ScaleOfSalaryCode(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mã lương");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("Muc", drCNT["SalaryLevel"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        var str36 = int.Parse(drCNT["DirectWorking"].ToString()) == 1
                            ? " Nghỉ tuần: 1 ngày/tuần hoặc bình quân 4 ngày/tháng."
                            : " Nghỉ tuần: Thứ 7 và chủ nhật hàng tuần.";
                        daD_Contract_Under3M1.SetParameterValue("NgayNghi", str36);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("HDTNgay",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("HDTThang",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        daD_Contract_Under3M1.SetParameterValue("HDTNam",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    var Len = "theo Quyết định số /QĐ-SAGS ngày ".Length;
                    if (drCNT["Office"].ToString().Length > Len)
                        daD_Contract_Under3M1.SetParameterValue("ChucVu", drCNT["Office"].ToString());
                    else
                        daD_Contract_Under3M1.SetParameterValue("ChucVu", "");
                    daD_Contract_Under3M1.SetParameterValue("UserId", _UserId);
                }
                    break;


                case "Server_CXR":
                {
                    var drCNT = EmployeeContractBLL.GetContractById(_ContractId);

                    cxR_Contract_Under3M1.SetParameterValue("SAGS", "SAGS" + Convert.ToDateTime(drCNT["FromDate"]).Year);
                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("ContractNo",
                            StringFormat.SetContractNo(int.Parse(drCNT["ContractNo"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Hợp đồng không hợp lệ");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("TenHD", drCNT["ContractTitle"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Tiêu đề hợp đồng không hợp lệ");
                    }

                    try
                    {
                        var Sex = int.Parse(drCNT["Sex"].ToString());
                        if (Sex == 1)
                            cxR_Contract_Under3M1.SetParameterValue("GioiTinh", "Ông");
                        else
                            cxR_Contract_Under3M1.SetParameterValue("GioiTinh", "Bà");
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có giới tính");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("HoTen", drCNT["FullName"].ToString().ToUpper());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có họ tên");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("QuocTich", drCNT["Nationality"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có quốc tịch");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("NoiSinh", drCNT["BirthPlace"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi sinh");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("NgaySinh",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày sinh");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("ThangSinh",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có tháng sinh");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("NamSinh",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["Birthday"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có năm sinh");
                    }

                    try
                    {
                        if ((drCNT["HighestLevelNameValue"].ToString() == "") ||
                            (drCNT["HighestLevelNameValue"].ToString() == null))
                        {
                            cxR_Contract_Under3M1.SetParameterValue("NgheNghiep", drCNT["HighestLevelName"].ToString());
                        }
                        else
                        {
                            if (drCNT["HighestLevelName"].ToString().Equals(drCNT["HighestLevelNameValue"].ToString()))
                                cxR_Contract_Under3M1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"].ToString());
                            else
                                cxR_Contract_Under3M1.SetParameterValue("NgheNghiep",
                                    drCNT["HighestLevelName"] + " - " + drCNT["HighestLevelNameValue"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nghề nghiệp");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("DiaChi", drCNT["Resident"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có dịa chỉ");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("CMND", drCNT["IdCard"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có CMND");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("NgayCap",
                            DateTime.Parse(drCNT["DateOfIssue"].ToString()).ToString("dd/MM/yyyy"));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có ngày cấp");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("NoiCap", drCNT["PlaceOfIssue"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có nơi cấp");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("LoaiHD", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("ContractFullName", drCNT["ContractFullName"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có loại HĐ");
                    }


                    try
                    {
                        var str17 = drCNT["ContractTypeCode"].ToString() == "HDKX"
                            ? string.Format("Từ ngày {0} tháng {1} năm {2}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())))
                            : string.Format("Từ ngày {0} tháng {1} năm {2} đến ngày {3} tháng {4} năm {5}",
                                string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())),
                                string.Format("{0:dd}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:MM}", DateTime.Parse(drCNT["ToDate"].ToString())),
                                string.Format("{0:yyyy}", DateTime.Parse(drCNT["ToDate"].ToString())));
                        cxR_Contract_Under3M1.SetParameterValue("TNgay", str17);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian HĐ");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("ChucDanhChuyenMon",
                            Get_ScaleOfSalaryName(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có chức danh chuyên môn");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("CongViecPhaiLam",
                            Get_ScaleOfSalaryJobDescription(int.Parse(drCNT["ScaleOfSalaryId"].ToString()))
                                .Replace("- ", "+ "));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mô tả công việc");
                    }

                    try
                    {
                        var str22 = string.Format("{0} giờ/ ngày và {1} giờ/ tuần", drCNT["Overtime"],
                            drCNT["WorkingHour"]);
                        cxR_Contract_Under3M1.SetParameterValue("ThoiGioLamViec", str22);
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian làm việc");
                    }

                    try
                    {
                        var str23 = "";
                        if (int.Parse(drCNT["DirectWorking"].ToString()) == 0)
                            if ((int.Parse(drCNT["DepartmentId"].ToString()) == 60) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 63) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 64) ||
                                (int.Parse(drCNT["DepartmentId"].ToString()) == 143))
                                str23 =
                                    " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                            else
                                str23 =
                                    " Ngoài thời giờ làm việc theo qui định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo quy định của Bộ luật lao động không quá 12 giờ/ngày; tổng số giờ làm thêm không quá 30 giờ/ tháng và không quá 200 giờ/năm.";
                        else
                            str23 =
                                " Đối với chức danh nhân viên hàng không, ngoài thời giờ làm việc theo quy định trên, nếu do yêu cầu công việc thì người sử dụng lao động có thể thỏa thuận với người lao động để làm thêm giờ. Tổng số giờ làm việc bình thường và số giờ làm thêm theo Thông tư số 42/2011/TT-BGTVT không quá 12 giờ/ngày, không quá 232 giờ/tháng; tổng số giờ làm thêm không quá 300 giờ/năm.";
                        cxR_Contract_Under3M1.SetParameterValue("LamThem", str23);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("MucLuong",
                            StringFormat.SetFormatMoneyFinal(
                                Convert.ToDecimal(Get_ScaleOfSalaryValue(int.Parse(drCNT["ScaleOfSalaryId"].ToString()),
                                    int.Parse(drCNT["SalaryLevel"].ToString())))));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("MaLuong",
                            Get_ScaleOfSalaryCode(int.Parse(drCNT["ScaleOfSalaryId"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mã lương");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("Muc", drCNT["SalaryLevel"].ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có mức lương");
                    }

                    try
                    {
                        var str36 = int.Parse(drCNT["DirectWorking"].ToString()) == 1
                            ? " Nghỉ tuần: 1 ngày/tuần hoặc bình quân 4 ngày/tháng."
                            : " Nghỉ tuần: Thứ 7 và chủ nhật hàng tuần.";
                        cxR_Contract_Under3M1.SetParameterValue("NgayNghi", str36);
                    }
                    catch
                    {
                        MessageBox.Show("Trực tiếp/ gián tiếp");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("HDTNgay",
                            string.Format("{0:dd}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("HDTThang",
                            string.Format("{0:MM}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    try
                    {
                        cxR_Contract_Under3M1.SetParameterValue("HDTNam",
                            string.Format("{0:yyyy}", DateTime.Parse(drCNT["FromDate"].ToString())));
                    }
                    catch
                    {
                        MessageBox.Show("Chưa có thời gian hợp đồng");
                    }

                    var Len = "theo Quyết định số /QĐ-SAGS ngày ".Length;
                    if (drCNT["Office"].ToString().Length > Len)
                        cxR_Contract_Under3M1.SetParameterValue("ChucVu", drCNT["Office"].ToString());
                    else
                        cxR_Contract_Under3M1.SetParameterValue("ChucVu", "");
                    cxR_Contract_Under3M1.SetParameterValue("UserId", _UserId);
                }
                    break;
            }
        }

        private string Get_ScaleOfSalaryName(int ScaleOfSalaryId)
        {
            return ScaleOfSalariesBLL.GetOne(ScaleOfSalaryId)["PositionName"].ToString();
        }

        private string Get_ScaleOfSalaryCode(int ScaleOfSalaryId)
        {
            return ScaleOfSalariesBLL.GetOne(ScaleOfSalaryId)["Code"].ToString();
        }

        private string Get_ScaleOfSalaryJobDescription(int ScaleOfSalaryId)
        {
            return ScaleOfSalariesBLL.GetOne(ScaleOfSalaryId)["JobDescription"].ToString();
        }

        private double Get_ScaleOfSalaryValue(int ScaleOfSalaryId, int Level)
        {
            double ReturnValue = 0;

            var dr = ScaleOfSalariesBLL.GetOne(ScaleOfSalaryId);

            var Index = Level;
            switch (Index)
            {
                case 1:
                    ReturnValue = Convert.ToDouble(dr["Value1"]);
                    break;
                case 2:
                    ReturnValue = Convert.ToDouble(dr["Value2"]);
                    break;
                case 3:
                    ReturnValue = Convert.ToDouble(dr["Value3"]);
                    break;
            }

            return ReturnValue;
        }
    }
}