using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0.Statistic;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0.Statistic
{
    public class STAEmployeeBLL
    {
        #region private fields

        /// <summary>
        ///     Genernal
        /// </summary>
        private int _RootId;

        /// <summary>
        ///     Statistic Sex
        /// </summary>
        private int _Total;

        /// <summary>
        ///     Statistic Contract
        /// </summary>
        private int _HNghe;

        /// <summary>
        ///     Statistic EducationLevel
        /// </summary>
        private int _TDVH;

        /// <summary>
        ///     List education level
        /// </summary>
        private string _FullName;

        /// <summary>
        ///     Seniority
        /// </summary>
        private int _UserId;

        #endregion

        #region Properties Gerneral

        public int RootId
        {
            get { return _RootId; }
            set { _RootId = value; }
        }

        public string RootName { get; set; } = string.Empty;

        #endregion

        #region statistic Sex

        public int Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        public int Female { get; set; }

        public int Male { get; set; }

        public int Marriage { get; set; }

        public int Unmarried { get; set; }

        #endregion

        #region statistic Contract

        public int HNghe
        {
            get { return _HNghe; }
            set { _HNghe = value; }
        }

        public int TNgheKhac { get; set; }

        public int TViecKhac { get; set; }

        public int XDTH24T { get; set; }

        public int TVuL1 { get; set; }

        public int TNgheSC { get; set; }

        public int TViecDH { get; set; }

        public int TNgheTCap { get; set; }

        public int TNgheDH { get; set; }

        public int KoXDTH { get; set; }

        public int XDTH12T { get; set; }

        public int XDTH36T { get; set; }

        public int TVuL2 { get; set; }

        #endregion

        #region Statistic Education

        public int TDVH
        {
            get { return _TDVH; }
            set { _TDVH = value; }
        }

        public int TOIEC { get; set; }

        public int TOEFL { get; set; }

        public int IELTS { get; set; }

        public int CCAVK { get; set; }

        public int NNK { get; set; }

        public int CCTH { get; set; }

        public int GPLX { get; set; }

        public int DH { get; set; }

        public int CD { get; set; }

        public int TC { get; set; }

        public int CH { get; set; }

        public int BK { get; set; }

        public int CCK { get; set; }

        public int CCAAV { get; set; }

        public int CCBAV { get; set; }

        public int CCCAV { get; set; }

        public int CNAV { get; set; }

        #endregion

        #region List Education Level

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        public string EducationLevelValue { get; set; }

        public string EducationLevelName { get; set; }

        #endregion

        #region Seniority

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public DateTime JoinDate { get; set; }

        public DateTime JoinCompanyDate { get; set; }

        #endregion

        #region HR

        public int ParentId { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int TotalA { get; set; }

        public int TotalB { get; set; }

        #endregion

        #region public methods Get

        public static List<STAEmployeeBLL> StatisticSexMarriage()
        {
            return GenerateListSTAEmployeesFromDataTableSex(new STAEmployeeDAL().StatisticSexMarriage());
        }

        public static DataTable StatisticLeave(DateTime fromDate, DateTime toDate, int isStatistic)
        {
            var dtGet = new STAEmployeeDAL().StatisticLeave(fromDate, toDate, isStatistic);

            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("STT", typeof(int)));
            dt.Columns.Add(new DataColumn("Phòng", typeof(string)));
            dt.Columns.Add(new DataColumn("Tổng", typeof(int)));

            foreach (DataRow dr in dtGet.Rows)
            {
                var columnName = dr["LeaveDate"].ToString();
                dt.Columns.Add(new DataColumn(columnName, typeof(string)));
            }


            for (var i = 2; i < dtGet.Columns.Count; i++)
            {
                var drnew = dt.NewRow();
                var columnName = dtGet.Columns[i].ColumnName;
                var objDept = DepartmentsBLL.GetById(int.Parse(columnName));
                drnew["STT"] = objDept.DepartmentId;
                drnew["Phòng"] = objDept.DepartmentName;
                var total = 0;
                foreach (DataRow dr in dtGet.Rows)
                {
                    var columnNameNew = dr["LeaveDate"].ToString();
                    drnew[columnNameNew] = dr[columnName];
                    total = total + int.Parse(dr[columnName].ToString());
                }
                drnew["Tổng"] = total.ToString();
                dt.Rows.Add(drnew);
            }

            return dt;
        }

        public static DataTable StatisticJoin(DateTime fromDate, DateTime toDate, int isStatistic)
        {
            var dtGet = new STAEmployeeDAL().StatisticJoin(fromDate, toDate, isStatistic);

            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("STT", typeof(int)));
            dt.Columns.Add(new DataColumn("Phòng", typeof(string)));
            dt.Columns.Add(new DataColumn("Tổng", typeof(int)));

            foreach (DataRow dr in dtGet.Rows)
            {
                var columnName = dr["JoinCompanyDate"].ToString();
                dt.Columns.Add(new DataColumn(columnName, typeof(string)));
            }


            for (var i = 2; i < dtGet.Columns.Count; i++)
            {
                var drnew = dt.NewRow();
                var columnName = dtGet.Columns[i].ColumnName;
                var objDept = DepartmentsBLL.GetById(int.Parse(columnName));
                drnew["STT"] = objDept.DepartmentId;
                drnew["Phòng"] = objDept.DepartmentName;
                var total = 0;
                foreach (DataRow dr in dtGet.Rows)
                {
                    var columnNameNew = dr["JoinCompanyDate"].ToString();
                    drnew[columnNameNew] = dr[columnName];
                    total = total + int.Parse(dr[columnName].ToString());
                }
                drnew["Tổng"] = total.ToString();
                dt.Rows.Add(drnew);
            }

            return dt;
        }

        public static DataTable StatisticTotalEmployees(DateTime fromDate, DateTime toDate, int isStatistic)
        {
            var dtGet = new STAEmployeeDAL().StatisticTotalEmployees(fromDate, toDate, isStatistic);

            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("STT", typeof(int)));
            dt.Columns.Add(new DataColumn("Phòng", typeof(string)));
            //dt.Columns.Add(new DataColumn("Tổng", typeof(int)));

            foreach (DataRow dr in dtGet.Rows)
            {
                var columnName = dr["JoinCompanyDate"].ToString();
                dt.Columns.Add(new DataColumn(columnName, typeof(string)));
            }


            for (var i = 2; i < dtGet.Columns.Count; i++)
            {
                var drnew = dt.NewRow();
                var columnName = dtGet.Columns[i].ColumnName;
                var objDept = DepartmentsBLL.GetById(int.Parse(columnName));
                drnew["STT"] = objDept.DepartmentId;
                drnew["Phòng"] = objDept.DepartmentName;
                var total = 0;
                foreach (DataRow dr in dtGet.Rows)
                {
                    var columnNameNew = dr["JoinCompanyDate"].ToString();
                    drnew[columnNameNew] = dr[columnName];
                    total = total + int.Parse(dr[columnName].ToString());
                }
                //drnew["Tổng"] = total.ToString();
                dt.Rows.Add(drnew);
            }

            return dt;
        }

        public static List<STAEmployeeBLL> StatisticSexContractType()
        {
            return GenerateListSTAEmployeesFromDataTableContract(new STAEmployeeDAL().StatisticSexContractType());
        }

        public static List<STAEmployeeBLL> StatisticEducationLevel()
        {
            return GenerateListSTAEmployeesFromDataTableEducationLevel(new STAEmployeeDAL().StatisticEducationLevel());
        }

        public static List<STAEmployeeBLL> GetListEducationLevel(int rootId, int educationLevelId)
        {
            return
                GenerateListSTAEmployeesFromDataTableListEducationLevel(
                    new STAEmployeeDAL().GetListEducationLevel(rootId, educationLevelId));
        }

        public static List<STAEmployeeBLL> StatisticSeniority(int rootId, string Operator, int countYear,
            int typeCompare)
        {
            return
                GenerateListSTAEmployeesFromDataTableListSeniority(new STAEmployeeDAL().StatisticSeniority(rootId,
                    Operator, countYear, typeCompare));
        }

        public static List<STAEmployeeBLL> StatisticHumanResource(DateTime FromDateA, DateTime ToDateA,
            DateTime FromDateB, DateTime ToDateB)
        {
            return
                GenerateListSTAEmployeesFromDataTableListHR(new STAEmployeeDAL().StatisticHumanResource(FromDateA,
                    ToDateA, FromDateB, ToDateB));
        }

        #endregion

        #region private methods, generate helper methods

        private static List<STAEmployeeBLL> GenerateListSTAEmployeesFromDataTableSex(DataTable dt)
        {
            var list = new List<STAEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateSTAEmployeesFromDataRowSex(dr));

            return list;
        }

        private static List<STAEmployeeBLL> GenerateListSTAEmployeesFromDataTableContract(DataTable dt)
        {
            var list = new List<STAEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateSTAEmployeesFromDataRowContract(dr));

            return list;
        }

        private static List<STAEmployeeBLL> GenerateListSTAEmployeesFromDataTableEducationLevel(DataTable dt)
        {
            var list = new List<STAEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateSTAEmployeesFromDataRowEducationLevel(dr));

            return list;
        }

        private static List<STAEmployeeBLL> GenerateListSTAEmployeesFromDataTableListEducationLevel(DataTable dt)
        {
            var list = new List<STAEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateSTAEmployeesFromDataRowListEducationLevel(dr));

            return list;
        }

        private static List<STAEmployeeBLL> GenerateListSTAEmployeesFromDataTableListSeniority(DataTable dt)
        {
            var list = new List<STAEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateSTAEmployeesFromDataRowListSeniority(dr));

            return list;
        }

        private static List<STAEmployeeBLL> GenerateListSTAEmployeesFromDataTableListHR(DataTable dt)
        {
            var list = new List<STAEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateSTAEmployeesFromDataRowListHR(dr));

            return list;
        }

        private static STAEmployeeBLL GenerateSTAEmployeesFromDataRowSex(DataRow dr)
        {
            var obj = new STAEmployeeBLL();

            obj._RootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                ? 0
                : (int) dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID];
            obj.RootName = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                ? string.Empty
                : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME];

            obj._Total = dr["Total"] == DBNull.Value ? 0 : (int) dr["Total"];
            obj.Male = dr["Male"] == DBNull.Value ? 0 : (int) dr["Male"];
            obj.Female = dr["Female"] == DBNull.Value ? 0 : (int) dr["Female"];
            obj.Marriage = dr["Marriage"] == DBNull.Value ? 0 : (int) dr["Marriage"];
            obj.Unmarried = dr["Unmarried"] == DBNull.Value ? 0 : (int) dr["Unmarried"];

            return obj;
        }

        private static STAEmployeeBLL GenerateSTAEmployeesFromDataRowContract(DataRow dr)
        {
            var obj = new STAEmployeeBLL();

            obj._RootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                ? 0
                : (int) dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID];
            obj.RootName = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                ? string.Empty
                : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME];

            obj._HNghe = dr["HNghe"] == DBNull.Value ? 0 : (int) dr["HNghe"];
            obj.TNgheKhac = dr["TNgheKhac"] == DBNull.Value ? 0 : (int) dr["TNgheKhac"];
            obj.TViecKhac = dr["TViecKhac"] == DBNull.Value ? 0 : (int) dr["TViecKhac"];
            obj.XDTH24T = dr["XDTH24T"] == DBNull.Value ? 0 : (int) dr["XDTH24T"];
            obj.TVuL1 = dr["TVuL1"] == DBNull.Value ? 0 : (int) dr["TVuL1"];

            obj.TNgheSC = dr["TNgheSC"] == DBNull.Value ? 0 : (int) dr["TNgheSC"];
            obj.TViecDH = dr["TViecDH"] == DBNull.Value ? 0 : (int) dr["TViecDH"];
            obj.TNgheTCap = dr["TNgheTCap"] == DBNull.Value ? 0 : (int) dr["TNgheTCap"];
            obj.TNgheDH = dr["TNgheDH"] == DBNull.Value ? 0 : (int) dr["TNgheDH"];
            obj.KoXDTH = dr["KoXDTH"] == DBNull.Value ? 0 : (int) dr["KoXDTH"];

            obj.XDTH12T = dr["XDTH12T"] == DBNull.Value ? 0 : (int) dr["XDTH12T"];
            obj.XDTH36T = dr["XDTH36T"] == DBNull.Value ? 0 : (int) dr["XDTH36T"];
            obj.TVuL2 = dr["TVuL2"] == DBNull.Value ? 0 : (int) dr["TVuL2"];

            return obj;
        }

        private static STAEmployeeBLL GenerateSTAEmployeesFromDataRowEducationLevel(DataRow dr)
        {
            var obj = new STAEmployeeBLL();

            obj._RootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                ? 0
                : (int) dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID];
            obj.RootName = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                ? string.Empty
                : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME];

            obj._TDVH = dr["TDVH"] == DBNull.Value ? 0 : (int) dr["TDVH"];
            obj.TOIEC = dr["TOIEC"] == DBNull.Value ? 0 : (int) dr["TOIEC"];
            obj.TOEFL = dr["TOEFL"] == DBNull.Value ? 0 : (int) dr["TOEFL"];
            obj.IELTS = dr["IELTS"] == DBNull.Value ? 0 : (int) dr["IELTS"];
            obj.CCAVK = dr["CCAVK"] == DBNull.Value ? 0 : (int) dr["CCAVK"];

            obj.NNK = dr["NNK"] == DBNull.Value ? 0 : (int) dr["NNK"];
            obj.CCTH = dr["CCTH"] == DBNull.Value ? 0 : (int) dr["CCTH"];
            obj.GPLX = dr["GPLX"] == DBNull.Value ? 0 : (int) dr["GPLX"];
            obj.DH = dr["DH"] == DBNull.Value ? 0 : (int) dr["DH"];
            obj.CD = dr["CD"] == DBNull.Value ? 0 : (int) dr["CD"];

            obj.TC = dr["TC"] == DBNull.Value ? 0 : (int) dr["TC"];
            obj.CH = dr["CH"] == DBNull.Value ? 0 : (int) dr["CH"];
            obj.BK = dr["BK"] == DBNull.Value ? 0 : (int) dr["BK"];
            obj.CCK = dr["CCK"] == DBNull.Value ? 0 : (int) dr["CCK"];

            obj.CCAAV = dr["CCAAV"] == DBNull.Value ? 0 : (int) dr["CCAAV"];
            obj.CCBAV = dr["CCBAV"] == DBNull.Value ? 0 : (int) dr["CCBAV"];
            obj.CCCAV = dr["CCCAV"] == DBNull.Value ? 0 : (int) dr["CCCAV"];
            obj.CNAV = dr["CNAV"] == DBNull.Value ? 0 : (int) dr["CNAV"];

            return obj;
        }

        private static STAEmployeeBLL GenerateSTAEmployeesFromDataRowListEducationLevel(DataRow dr)
        {
            var obj = new STAEmployeeBLL();

            obj._FullName = dr["FullName"] == DBNull.Value ? string.Empty : (string) dr["FullName"];
            obj.EducationLevelValue = dr["EducationLevelValue"] == DBNull.Value
                ? string.Empty
                : (string) dr["EducationLevelValue"];
            obj.EducationLevelName = dr["Name"] == DBNull.Value ? string.Empty : (string) dr["Name"];
            return obj;
        }


        private static STAEmployeeBLL GenerateSTAEmployeesFromDataRowListSeniority(DataRow dr)
        {
            var obj = new STAEmployeeBLL();

            obj._UserId = dr["UserId"] == DBNull.Value ? 0 : int.Parse(dr["UserId"].ToString());
            obj._FullName = dr["FullName"] == DBNull.Value ? string.Empty : (string) dr["FullName"];
            obj.RootName = dr["RootName"] == DBNull.Value ? string.Empty : (string) dr["RootName"];
            obj.JoinDate = dr["JoinDate"] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr["JoinDate"].ToString());
            obj.JoinCompanyDate = dr["JoinCompanyDate"] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr["JoinCompanyDate"].ToString());

            return obj;
        }

        private static STAEmployeeBLL GenerateSTAEmployeesFromDataRowListHR(DataRow dr)
        {
            var obj = new STAEmployeeBLL();

            obj._RootId = dr["RootId"] == DBNull.Value ? 0 : int.Parse(dr["RootId"].ToString());
            obj.DepartmentId = dr["DepartmentId"] == DBNull.Value ? 0 : int.Parse(dr["DepartmentId"].ToString());
            obj.ParentId = dr["ParentId"] == DBNull.Value ? 0 : int.Parse(dr["ParentId"].ToString());
            obj.DepartmentName = dr["DepartmentName"] == DBNull.Value ? string.Empty : (string) dr["DepartmentName"];

            obj.TotalA = dr["TotalA"] == DBNull.Value ? 0 : int.Parse(dr["TotalA"].ToString());
            obj.TotalB = dr["TotalB"] == DBNull.Value ? 0 : int.Parse(dr["TotalB"].ToString());
            return obj;
        }

        #endregion
    }
}