using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H2;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H2;

namespace HRMBLL.H2
{
    public class CandidatesBLL
    {
        #region private fields

        /// <summary>
        ///     Ngoai Ngu
        /// </summary>
        private double _NNGK1;

        /// <summary>
        ///     Ngoai hinh
        /// </summary>
        private double _NHGK1;

        /// <summary>
        ///     Phong Cach
        /// </summary>
        private double _PCGK1;

        /// <summary>
        ///     Kinh Nghiem
        /// </summary>
        private double _KNGK1;

        /// <summary>
        ///     Doc hieu
        /// </summary>
        private double _DHGK1 = -1;

        /// <summary>
        ///     Dinh huong nghe nghiep
        /// </summary>
        private double _DHNNGK1;

        /// <summary>
        ///     Diem thi vong cuoi moi
        /// </summary>
        /// <summary>
        ///     Ngoai Ngu
        /// </summary>
        private double _NNLRGK1;

        /// <summary>
        ///     Ngoai hinh
        /// </summary>
        private double _NHLRGK1;

        /// <summary>
        ///     Diem thi vong cuoi
        /// </summary>
        private double _NNLR;


        /// <summary>
        ///     Ngoai Ngu
        /// </summary>
        private double _L2NNGK1;

        /// <summary>
        ///     Ngoai hinh
        /// </summary>
        private double _L2NHGK1;

        /// <summary>
        ///     Phong Cach
        /// </summary>
        private double _L2PCGK1;

        /// <summary>
        ///     Kinh Nghiem
        /// </summary>
        private double _L2KNGK1;

        /// <summary>
        /// </summary>
        private double _L2DHNNGK1;

        #endregion

        #region properties

        public int Id { get; set; }

        public int OrderNumber { get; set; }

        public int STT { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string LastName1 { get; set; }

        public string FirstName1 { get; set; }

        public int DayOfBirth { get; set; }

        public int MonthOfBirth { get; set; }

        public int YearOfBirth { get; set; }

        public bool Sex { get; set; }

        public int EducationLevelId { get; set; }

        public string Experience { get; set; }

        public double Height { get; set; }

        public string Health { get; set; } = string.Empty;

        public string HomePhone { get; set; }

        public string HandPhone { get; set; }

        public string Remark { get; set; }

        public int Type { get; set; }

        public string TypeName { get; set; }

        public string Reson { get; set; }

        public double NNGK1
        {
            get { return _NNGK1; }
            set { _NNGK1 = value; }
        }

        public double NNGK2 { get; set; }

        public double NNGK3 { get; set; }

        public double NNTB { get; set; }

        public double NHGK1
        {
            get { return _NHGK1; }
            set { _NHGK1 = value; }
        }

        public double NHGK2 { get; set; }

        public double NHGK3 { get; set; }

        public double NHTB { get; set; }

        public double PCGK1
        {
            get { return _PCGK1; }
            set { _PCGK1 = value; }
        }

        public double PCGK2 { get; set; }

        public double PCGK3 { get; set; }

        public double PCTB { get; set; }

        public double KNGK1
        {
            get { return _KNGK1; }
            set { _KNGK1 = value; }
        }

        public double KNGK2 { get; set; }

        public double KNGK3 { get; set; }

        public double KNTB { get; set; }

        public double DHGK1
        {
            get { return _DHGK1; }
            set { _DHGK1 = value; }
        }

        public double DHGK2 { get; set; } = -1;

        public double DHGK3 { get; set; }

        public double DHTB { get; set; }

        public double DHNNGK1
        {
            get { return _DHNNGK1; }
            set { _DHNNGK1 = value; }
        }

        public double DHNNGK2 { get; set; }

        public double DHNNGK3 { get; set; }

        public double DHNNTB { get; set; }

        public double VT { get; set; }

        /// <summary>
        ///     Ngoai Ngu vong cuoi moi
        /// </summary>
        public double NNLRGK1
        {
            get { return _NNLRGK1; }
            set { _NNLRGK1 = value; }
        }

        public double NNLRGK2 { get; set; }

        public double NNLRGK3 { get; set; }

        public double NNLRTB { get; set; }

        /// <summary>
        ///     Ngoai hinh vong cuoi moi
        /// </summary>
        public double NHLRGK1
        {
            get { return _NHLRGK1; }
            set { _NHLRGK1 = value; }
        }

        public double NHLRGK2 { get; set; }

        public double NHLRGK3 { get; set; }

        public double NHLRTB { get; set; }

        /// <summary>
        ///     ///////////////////
        /// </summary>
        public double NNLR
        {
            get { return _NNLR; }
            set { _NNLR = value; }
        }

        public double NHLR { get; set; }

        public double PCLR { get; set; }

        public double KNLR { get; set; }

        public int Result { get; set; }

        public string ResultName { get; set; }


        public int PositionId { get; set; }

        public int SessionId { get; set; }

        public string EducationLevelValue { get; set; }

        public List<CandidateEducationLevelsBLL> AllEducationLevelValue { get; set; }

        public string RemarkEducationLevel { get; set; }

        public string PositionName { get; set; }

        public int CreateUserId { get; set; } = 0;

        public int UpdateUserId { get; set; } = 0;


        public string RemarkR1 { get; set; } = "";

        public string RemarkR2 { get; set; } = "";

        public string RemarkR3 { get; set; } = "";

        public string RemarkLR { get; set; } = "";

        public string SessionName { get; set; } = "";

        public int UserId { get; set; }

        /// <summary>
        /// </summary>
        public double L2NNGK1
        {
            get { return _L2NNGK1; }
            set { _L2NNGK1 = value; }
        }

        public double L2NNGK2 { get; set; }

        public double L2NNGK3 { get; set; }

        public double L2NNTB { get; set; }

        public double L2NHGK1
        {
            get { return _L2NHGK1; }
            set { _L2NHGK1 = value; }
        }

        public double L2NHGK2 { get; set; }

        public double L2NHGK3 { get; set; }

        public double L2NHTB { get; set; }

        public double L2PCGK1
        {
            get { return _L2PCGK1; }
            set { _L2PCGK1 = value; }
        }

        public double L2PCGK2 { get; set; }

        public double L2PCGK3 { get; set; }

        public double L2PCTB { get; set; }

        public double L2KNGK1
        {
            get { return _L2KNGK1; }
            set { _L2KNGK1 = value; }
        }

        public double L2KNGK2 { get; set; }

        public double L2KNGK3 { get; set; }

        public double L2KNTB { get; set; }

        public double L2DHNNGK1
        {
            get { return _L2DHNNGK1; }
            set { _L2DHNNGK1 = value; }
        }

        public double L2DHNNGK2 { get; set; }

        public double L2DHNNGK3 { get; set; }

        public double L2DHNNTB { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        #endregion

        #region public methods

        public long Save()
        {
            var objDAL = new CandidatesDAL();
            if (Id <= 0)
                return objDAL.Insert(LastName, FirstName, DayOfBirth, MonthOfBirth, YearOfBirth, Sex, Experience, Height,
                    HomePhone, HandPhone, Remark, Type, Reson, Result, PositionId, SessionId, Health, CreateUserId,
                    LastName1, FirstName1, UserName, Password);
            return objDAL.Update(LastName, FirstName, DayOfBirth, MonthOfBirth, YearOfBirth, Sex, Experience, Height,
                HomePhone, HandPhone, Remark, Type, Reson, Result, PositionId, Id, SessionId, Health, UpdateUserId,
                LastName1, FirstName1, UserName, Password);
        }

        public static long UpdateForLastFirstName1(string lastName1, string firstName1, int candidateId)
        {
            return new CandidatesDAL().UpdateForLastFirstName1(lastName1, firstName1, candidateId);
        }

        public long UpdateMark_RP()
        {
            return new CandidatesDAL().UpdateMark_RP(
                NNGK1, NNGK2, NNGK3, NNTB,
                NHGK1, NHGK2, NHGK3, NHTB,
                PCGK1, PCGK2, PCGK3, PCTB,
                KNGK1, KNGK2, KNGK3, KNTB,
                DHGK1, DHGK2, DHGK3, DHTB,
                DHNNGK1, DHNNGK2, DHNNGK3, DHNNTB, Result, Id);
        }

        public long UpdateMark_RL()
        {
            return new CandidatesDAL().UpdateMark_RL(
                NNLRGK1, NNLRGK2, NNLRGK3, NNLRTB, NHLRGK1, NHLRGK2, NHLRGK3, NHLRTB, Result, Id);
        }

        public long UpdateUnSkilledLabour(int Id, string lastName, string firstname, bool sex, string birthday,
            string height, string health, string educationLevel, string Remark, int Result)
        {
            return new CandidatesDAL().UpdateMark_RL(0, 0, 0, 0, 0, 0, 0, 0, Result, Id);
        }

        public static long Delete(int id)
        {
            return new CandidatesDAL().Delete(id);
        }

        public long UpdateMark_R1()
        {
            return new CandidatesDAL().UpdateMark_R1(DHTB, DHGK1, DHGK2, DHGK3, Result, RemarkR1, Id);
        }

        public long UpdateMark_R2()
        {
            return new CandidatesDAL().UpdateMark_R2(
                NNGK1, NNGK2, NNGK3, NNTB,
                NHGK1, NHGK2, NHGK3, NHTB,
                PCGK1, PCGK2, PCGK3, PCTB,
                KNGK1, KNGK2, KNGK3, KNTB,
                DHNNGK1, DHNNGK2, DHNNGK3, DHNNTB, Result, RemarkR2, Id, DHTB);
        }

        public long UpdateMark_R3()
        {
            return new CandidatesDAL().UpdateMark_R3(
                L2NNGK1, L2NNGK2, L2NNGK3, L2NNTB,
                L2NHGK1, L2NHGK2, L2NHGK3, L2NHTB,
                L2PCGK1, L2PCGK2, L2PCGK3, L2PCTB,
                L2KNGK1, L2KNGK2, L2KNGK3, L2KNTB,
                L2DHNNGK1, L2DHNNGK2, L2DHNNGK3, L2DHNNTB, Result, RemarkR3, Id);
        }

        public long UpdateMark_RBoard()
        {
            return new CandidatesDAL().UpdateMark_RBoard(
                NNLRGK1, NNLRGK2, NNLRGK3, NNLRTB, DHGK1, NHLRGK1, NHLRGK2, NHLRGK3, NHLRTB, DHGK2, Result, RemarkLR, Id);
        }

        public long UpdateMark_Repairing_Thoery()
        {
            return new CandidatesDAL().UpdateMark_Repairing_Thoery(_NNGK1, Result, RemarkR1, Id);
        }

        public long UpdateMark_Repairing_Practice()
        {
            return new CandidatesDAL().UpdateMark_Repairing_Practice(NNGK2, Result, RemarkR2, Id);
        }

        public long UpdateMark_Repairing_Board()
        {
            return new CandidatesDAL().UpdateMark_Repairing_Board(
                PCGK1, PCGK2, PCGK3, PCTB,
                KNGK1, KNGK2, KNGK3, KNTB,
                DHNNGK1, DHNNGK2, DHNNGK3, DHNNTB, Result, RemarkR3, Id);
        }

        public long UpdateMark_Speciality_Thoery()
        {
            return new CandidatesDAL().UpdateMark_Speciality_Thoery(_NNGK1, NNGK2, Result, RemarkR1, Id);
        }

        public long UpdateMark_Speciality_Board()
        {
            return new CandidatesDAL().UpdateMark_Speciality_Board(Result, RemarkR2, Id);
        }

        public long UpdateMark_Speciality_Practice()
        {
            return new CandidatesDAL().UpdateMark_Speciality_Practice(NNGK3, Result, RemarkR3, Id);
        }

        public long UpdateMark_LoadAndUnloadCargo()
        {
            return new CandidatesDAL().UpdateMark_LoadAndUnloadCargo(_NNGK1, RemarkR1, RemarkR2, Result, RemarkR3, Id);
        }

        #endregion

        #region public methods get

        //public static List<CandidatesBLL> GetAll()
        //{
        //    return GenerateListFromDataTableExport(new CandidatesDAL().GetAll());
        //}

        //public static List<CandidatesBLL> GetByFilterExport(string firstName, int positionId, int result, int sessionId, int type, int sessionType, int TypeSort)
        //{
        //    return GenerateListFromDataTableExport(new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType, TypeSort));
        //}

        public static List<CandidatesBLL> GetByFilter(string firstName, int positionId, int result, int sessionId,
            int type, int sessionType, int TypeSort)
        {
            return
                GenerateListFromDataTable(new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type,
                    sessionType, TypeSort));
        }

        public static DataTable GetDTByFilter(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int TypeSort)
        {
            return new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType, TypeSort);
        }

        public static CandidatesBLL GetById(int id)
        {
            var list = new List<CandidatesBLL>();
            list = GenerateListFromDataTable(new CandidatesDAL().GetById(id));

            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static DataTable GetDTForTrainingByFilter(string fullName, int sessionId)
        {
            return new CandidatesDAL().GetForTrainingByFilter(fullName, sessionId);
        }

        public static DataTable GetForTrainingByFilterPreEmployee(string fullName, int sessionId)
        {
            return new CandidatesDAL().GetForTrainingByFilterPreEmployee(fullName, sessionId);
        }

        public static List<CandidatesBLL> GetForTrainingByFilter(string fullName, int sessionId)
        {
            return GenerateListFromDataTableForTraining(new CandidatesDAL().GetForTrainingByFilter(fullName, sessionId));
        }

        public static CandidatesBLL Login(string userName, string password)
        {
            var list = new List<CandidatesBLL>();
            list = GenerateListFromDataTable(new CandidatesDAL().Login(userName, password));

            if (list.Count == 1)
                return list[0];
            return null;
        }

        #endregion

        #region private methods

        private static List<CandidatesBLL> GenerateListFromDataTable(DataTable dt)
        {
            var list = new List<CandidatesBLL>();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var obj = GenerateFromDataTable(dt.Rows[i]);
                obj.STT = i + 1;
                list.Add(obj);
            }

            return list;
        }

        //private static CandidatesBLL GenerateFromDataTable(DataRow dr)
        //{
        //    CandidatesBLL c = new CandidatesBLL();
        //    c._Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value ? 0 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
        //    c._OrderNumber = dr[CandidatesKeys.FIELD_CANDIDATE_ORDER_NUMBER] == DBNull.Value ? 0 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ORDER_NUMBER].ToString());
        //    //c._SessionId = dr[CandidatesKeys.FIELD_CANDIDATE_SESSION_ID] == DBNull.Value ? 0 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SESSION_ID].ToString());
        //    c._LastName = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value ? string.Empty : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
        //    c._FirstName = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value ? string.Empty : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
        //    c._DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value ? 0 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
        //    c._MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value ? 0 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
        //    c._YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value ? 0 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
        //    c._Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value ? false : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
        //    //c._EducationLevelId = dr[CandidatesKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_ID] == DBNull.Value ? 0 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_ID].ToString());
        //    c._Experience = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value? string.Empty : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
        //    c._Height = dr[CandidatesKeys.FIELD_CANDIDATE_HEIGHT] == DBNull.Value ? 0 : double.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_HEIGHT].ToString());
        //    c._HomePhone = dr[CandidatesKeys.FIELD_CANDIDATE_HOME_PHONE] == DBNull.Value ? string.Empty : dr[CandidatesKeys.FIELD_CANDIDATE_HOME_PHONE].ToString();
        //    c._HandPhone = dr[CandidatesKeys.FIELD_CANDIDATE_HAND_PHONE] == DBNull.Value ? string.Empty : dr[CandidatesKeys.FIELD_CANDIDATE_HAND_PHONE].ToString();            
        //    c._Remark = dr[CandidatesKeys.FIELD_CANDIDATE_REMARK] == DBNull.Value ? string.Empty : dr[CandidatesKeys.FIELD_CANDIDATE_REMARK].ToString();

        //    c._Type = dr[CandidatesKeys.FIELD_CANDIDATE_TYPE] == DBNull.Value ? -1 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_TYPE].ToString());

        //    c._Health = dr[CandidatesKeys.Field_Candidate_Health] == DBNull.Value ? string.Empty : dr[CandidatesKeys.Field_Candidate_Health].ToString();

        //    if (c._Type == 1)
        //    {
        //        c._TypeName = Constants.CANDIDATE_OK_NAME;
        //    }
        //    else if (c._Result == 0)
        //    {
        //        c._TypeName = Constants.CANDIDATE_NO_OK_NAME;
        //    }
        //    else
        //    {
        //        c._TypeName = string.Empty;
        //    }

        //    c._Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value ? -1 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());

        //    c._Reson = dr[CandidatesKeys.Field_Candidate_Reson] == DBNull.Value ? string.Empty : dr[CandidatesKeys.Field_Candidate_Reson].ToString();

        //    c._NNGK1 = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());
        //    c._NNGK2 = dr[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK2].ToString());
        //    c._NNGK3 = dr[CandidatesKeys.Field_Candidate_NNGK3] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK3].ToString());
        //    c._NNTB = dr[CandidatesKeys.Field_Candidate_NNTB] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_NNTB].ToString());

        //    c._NHGK1 = dr[CandidatesKeys.Field_Candidate_NHGK1] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_NHGK1].ToString());
        //    c._NHGK2 = dr[CandidatesKeys.Field_Candidate_NHGK2] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_NHGK2].ToString());
        //    c._NHGK3 = dr[CandidatesKeys.Field_Candidate_NHGK3] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_NHGK3].ToString());
        //    c._NHTB = dr[CandidatesKeys.Field_Candidate_NHTB] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_NHTB].ToString());

        //    c._PCGK1 = dr[CandidatesKeys.Field_Candidate_PCGK1] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK1].ToString());
        //    c._PCGK2 = dr[CandidatesKeys.Field_Candidate_PCGK2] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK2].ToString());
        //    c._PCGK3 = dr[CandidatesKeys.Field_Candidate_PCGK3] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK3].ToString());
        //    c._PCTB = dr[CandidatesKeys.Field_Candidate_PCTB] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_PCTB].ToString());

        //    c._KNGK1 = dr[CandidatesKeys.Field_Candidate_KNGK1] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK1].ToString());
        //    c._KNGK2 = dr[CandidatesKeys.Field_Candidate_KNGK2] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK2].ToString());
        //    c._KNGK3 = dr[CandidatesKeys.Field_Candidate_KNGK3] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK3].ToString());
        //    c._KNTB = dr[CandidatesKeys.Field_Candidate_KNTB] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_KNTB].ToString());

        //    c._DHNNGK1 = dr[CandidatesKeys.Field_Candidate_DHNNGK1] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK1].ToString());
        //    c._DHNNGK2 = dr[CandidatesKeys.Field_Candidate_DHNNGK2] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK2].ToString());
        //    c._DHNNGK3 = dr[CandidatesKeys.Field_Candidate_DHNNGK3] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK3].ToString());
        //    c._DHNNTB = dr[CandidatesKeys.Field_Candidate_DHNNTB] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNTB].ToString());

        //    c._VT = dr[CandidatesKeys.Field_Candidate_VT] == DBNull.Value ? 0 : double.Parse(dr[CandidatesKeys.Field_Candidate_VT].ToString());

        //    c._NNLR = dr[CandidatesKeys.Field_Candidate_NNLR] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_NNLR].ToString());
        //    c._NHLR = dr[CandidatesKeys.Field_Candidate_NHLR] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_NHLR].ToString());
        //    c._PCLR = dr[CandidatesKeys.Field_Candidate_PCLR] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_PCLR].ToString());
        //    c._KNLR = dr[CandidatesKeys.Field_Candidate_KNLR] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_KNLR].ToString());


        //    if (c._Result == 4)
        //    {
        //        c._ResultName = "Đạt";
        //    }
        //    //else if (c._Result == 0)
        //    //{
        //    //    c._ResultName = Constants.CANDIDATE_NO_OK_NAME;
        //    //}
        //    else
        //    {
        //        c._ResultName = string.Empty;
        //    }
        //    c._PositionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value ? 0 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
        //    c._PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value ? string.Empty : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

        //    c._RemarkR1 = dr[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value ? string.Empty : dr[CandidatesKeys.Field_Candidate_RemarkR1].ToString();
        //    c._RemarkR2 = dr[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value ? string.Empty : dr[CandidatesKeys.Field_Candidate_RemarkR2].ToString();
        //    c._RemarkR3 = dr[CandidatesKeys.Field_Candidate_RemarkR3] == DBNull.Value ? string.Empty : dr[CandidatesKeys.Field_Candidate_RemarkR3].ToString();

        //    return c;
        //}


        //private static List<CandidatesBLL> GenerateListFromDataTableExport(DataTable dt)
        //{
        //    List<CandidatesBLL> list = new List<CandidatesBLL>();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        CandidatesBLL obj = GenerateFromDataTableExport(dt.Rows[i]);
        //        obj._STT = i + 1;
        //        list.Add(obj);
        //    }

        //    return list;
        //}

        private static CandidatesBLL GenerateFromDataTable(DataRow dr)
        {
            var c = new CandidatesBLL();
            c.Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            c.OrderNumber = dr[CandidatesKeys.FIELD_CANDIDATE_ORDER_NUMBER] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ORDER_NUMBER].ToString());
            //c._SessionId = dr[CandidatesKeys.FIELD_CANDIDATE_SESSION_ID] == DBNull.Value ? 0 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SESSION_ID].ToString());
            c.LastName = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            c.FirstName = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            c.LastName1 = dr[CandidatesKeys.Field_Candidate_LastName1] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.Field_Candidate_LastName1].ToString();
            c.FirstName1 = dr[CandidatesKeys.Field_Candidate_FirstName1] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.Field_Candidate_FirstName1].ToString();
            c.DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            c.MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            c.YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            c.Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            //c._EducationLevelId = dr[CandidatesKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_ID] == DBNull.Value ? 0 : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_ID].ToString());
            c.Experience = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
            c.Height = dr[CandidatesKeys.FIELD_CANDIDATE_HEIGHT] == DBNull.Value
                ? 0
                : double.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_HEIGHT].ToString());
            c.HomePhone = dr[CandidatesKeys.FIELD_CANDIDATE_HOME_PHONE] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.FIELD_CANDIDATE_HOME_PHONE].ToString();
            c.HandPhone = dr[CandidatesKeys.FIELD_CANDIDATE_HAND_PHONE] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.FIELD_CANDIDATE_HAND_PHONE].ToString();
            c.Remark = dr[CandidatesKeys.FIELD_CANDIDATE_REMARK] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.FIELD_CANDIDATE_REMARK].ToString();

            c.Type = dr[CandidatesKeys.FIELD_CANDIDATE_TYPE] == DBNull.Value
                ? -1
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_TYPE].ToString());

            c.Health = dr[CandidatesKeys.Field_Candidate_Health] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.Field_Candidate_Health].ToString();

            if (c.Type == 1)
                c.TypeName = Constants.CANDIDATE_OK_NAME;
            else if (c.Result == 0)
                c.TypeName = Constants.CANDIDATE_NO_OK_NAME;
            else
                c.TypeName = string.Empty;

            c.Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());

            c.Reson = dr[CandidatesKeys.Field_Candidate_Reson] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.Field_Candidate_Reson].ToString();

            c._NNGK1 = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());
            c.NNGK2 = dr[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK2].ToString());
            c.NNGK3 = dr[CandidatesKeys.Field_Candidate_NNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK3].ToString());
            c.NNTB = dr[CandidatesKeys.Field_Candidate_NNTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NNTB].ToString());

            c._NHGK1 = dr[CandidatesKeys.Field_Candidate_NHGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NHGK1].ToString());
            c.NHGK2 = dr[CandidatesKeys.Field_Candidate_NHGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NHGK2].ToString());
            c.NHGK3 = dr[CandidatesKeys.Field_Candidate_NHGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NHGK3].ToString());
            c.NHTB = dr[CandidatesKeys.Field_Candidate_NHTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NHTB].ToString());

            c._PCGK1 = dr[CandidatesKeys.Field_Candidate_PCGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK1].ToString());
            c.PCGK2 = dr[CandidatesKeys.Field_Candidate_PCGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK2].ToString());
            c.PCGK3 = dr[CandidatesKeys.Field_Candidate_PCGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK3].ToString());
            c.PCTB = dr[CandidatesKeys.Field_Candidate_PCTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_PCTB].ToString());

            c._KNGK1 = dr[CandidatesKeys.Field_Candidate_KNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK1].ToString());
            c.KNGK2 = dr[CandidatesKeys.Field_Candidate_KNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK2].ToString());
            c.KNGK3 = dr[CandidatesKeys.Field_Candidate_KNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK3].ToString());
            c.KNTB = dr[CandidatesKeys.Field_Candidate_KNTB] == DBNull.Value
                ? 0
                : double.Parse(dr[CandidatesKeys.Field_Candidate_KNTB].ToString());

            c._DHGK1 = dr[CandidatesKeys.Field_Candidate_DHGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK1].ToString());
            c.DHGK2 = dr[CandidatesKeys.Field_Candidate_DHGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK2].ToString());
            c.DHGK3 = dr[CandidatesKeys.Field_Candidate_DHGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK3].ToString());
            c.DHTB = dr[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_DHTB].ToString());

            c._DHNNGK1 = dr[CandidatesKeys.Field_Candidate_DHNNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK1].ToString());
            c.DHNNGK2 = dr[CandidatesKeys.Field_Candidate_DHNNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK2].ToString());
            c.DHNNGK3 = dr[CandidatesKeys.Field_Candidate_DHNNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK3].ToString());
            c.DHNNTB = dr[CandidatesKeys.Field_Candidate_DHNNTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNTB].ToString());

            c.VT = dr[CandidatesKeys.Field_Candidate_VT] == DBNull.Value
                ? 0
                : double.Parse(dr[CandidatesKeys.Field_Candidate_VT].ToString());

            c._NNLRGK1 = dr[CandidatesKeys.Field_Candidate_NNLRGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NNLRGK1].ToString());
            c.NNLRGK2 = dr[CandidatesKeys.Field_Candidate_NNLRGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NNLRGK2].ToString());
            c.NNLRGK3 = dr[CandidatesKeys.Field_Candidate_NNLRGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NNLRGK3].ToString());
            c.NNLRTB = dr[CandidatesKeys.Field_Candidate_NNLRTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NNLRTB].ToString());

            c._NHLRGK1 = dr[CandidatesKeys.Field_Candidate_NHLRGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NHLRGK1].ToString());
            c.NHLRGK2 = dr[CandidatesKeys.Field_Candidate_NHLRGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NHLRGK2].ToString());
            c.NHLRGK3 = dr[CandidatesKeys.Field_Candidate_NHLRGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NHLRGK3].ToString());
            c.NHLRTB = dr[CandidatesKeys.Field_Candidate_NHLRTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NHLRTB].ToString());


            c._NNLR = dr[CandidatesKeys.Field_Candidate_NNLR] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NNLR].ToString());
            c.NHLR = dr[CandidatesKeys.Field_Candidate_NHLR] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_NHLR].ToString());
            c.PCLR = dr[CandidatesKeys.Field_Candidate_PCLR] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_PCLR].ToString());
            c.KNLR = dr[CandidatesKeys.Field_Candidate_KNLR] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_KNLR].ToString());

            if (c.Result == 4)
                c.ResultName = "Đạt";
            else
                c.ResultName = string.Empty;

            c.PositionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
            c.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

            c.AllEducationLevelValue = CandidateEducationLevelsBLL.GetById(c.Id);

            c.RemarkR1 = dr[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.Field_Candidate_RemarkR1].ToString();
            c.RemarkR2 = dr[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.Field_Candidate_RemarkR2].ToString();
            c.RemarkR3 = dr[CandidatesKeys.Field_Candidate_RemarkR3] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.Field_Candidate_RemarkR3].ToString();
            c.RemarkLR = dr[CandidatesKeys.Field_Candidate_RemarkLR] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.Field_Candidate_RemarkLR].ToString();

            try
            {
                c.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            }
            catch
            {
            }

            c._L2NNGK1 = dr[CandidatesKeys.Field_Candidate_L2NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NNGK1].ToString());
            c.L2NNGK2 = dr[CandidatesKeys.Field_Candidate_L2NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NNGK2].ToString());
            c.L2NNGK3 = dr[CandidatesKeys.Field_Candidate_L2NNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NNGK3].ToString());
            c.L2NNTB = dr[CandidatesKeys.Field_Candidate_L2NNTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NNTB].ToString());

            c._L2NHGK1 = dr[CandidatesKeys.Field_Candidate_L2NHGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NHGK1].ToString());
            c.L2NHGK2 = dr[CandidatesKeys.Field_Candidate_L2NHGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NHGK2].ToString());
            c.L2NHGK3 = dr[CandidatesKeys.Field_Candidate_L2NHGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NHGK3].ToString());
            c.L2NHTB = dr[CandidatesKeys.Field_Candidate_L2NHTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NHTB].ToString());

            c._L2PCGK1 = dr[CandidatesKeys.Field_Candidate_L2PCGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2PCGK1].ToString());
            c.L2PCGK2 = dr[CandidatesKeys.Field_Candidate_L2PCGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2PCGK2].ToString());
            c.L2PCGK3 = dr[CandidatesKeys.Field_Candidate_L2PCGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2PCGK3].ToString());
            c.L2PCTB = dr[CandidatesKeys.Field_Candidate_L2PCTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2PCTB].ToString());

            c._L2KNGK1 = dr[CandidatesKeys.Field_Candidate_L2KNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2KNGK1].ToString());
            c.L2KNGK2 = dr[CandidatesKeys.Field_Candidate_L2KNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2KNGK2].ToString());
            c.L2KNGK3 = dr[CandidatesKeys.Field_Candidate_L2KNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2KNGK3].ToString());
            c.L2KNTB = dr[CandidatesKeys.Field_Candidate_L2KNTB] == DBNull.Value
                ? 0
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2KNTB].ToString());

            c._L2DHNNGK1 = dr[CandidatesKeys.Field_Candidate_L2DHNNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2DHNNGK1].ToString());
            c.L2DHNNGK2 = dr[CandidatesKeys.Field_Candidate_L2DHNNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2DHNNGK2].ToString());
            c.L2DHNNGK3 = dr[CandidatesKeys.Field_Candidate_L2DHNNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2DHNNGK3].ToString());
            c.L2DHNNTB = dr[CandidatesKeys.Field_Candidate_L2DHNNTB] == DBNull.Value
                ? -1
                : double.Parse(dr[CandidatesKeys.Field_Candidate_L2DHNNTB].ToString());

            c.UserName = dr[CandidatesKeys.Field_Candidate_UserName] == DBNull.Value
                ? ""
                : dr[CandidatesKeys.Field_Candidate_UserName].ToString();
            c.Password = dr[CandidatesKeys.Field_Candidate_Password] == DBNull.Value
                ? ""
                : dr[CandidatesKeys.Field_Candidate_Password].ToString();

            return c;
        }

        private static List<CandidatesBLL> GenerateListFromDataTableForTraining(DataTable dt)
        {
            var list = new List<CandidatesBLL>();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var obj = GenerateFromDataTableForTraining(dt.Rows[i]);
                obj.STT = i + 1;
                list.Add(obj);
            }

            return list;
        }

        private static CandidatesBLL GenerateFromDataTableForTraining(DataRow dr)
        {
            var c = new CandidatesBLL();
            c.Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            c.LastName = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            c.FirstName = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            c.DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            c.MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            c.YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            c.Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());

            c.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            c.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
            c.SessionName = dr[CandidatesKeys.Field_Candidate_SessionName] == DBNull.Value
                ? string.Empty
                : dr[CandidatesKeys.Field_Candidate_SessionName].ToString();

            return c;
        }

        #endregion
    }
}