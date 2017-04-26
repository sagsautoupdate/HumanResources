using System.Data;
using HRMDAL.H1;

namespace HRMBLL.H1
{
    /// <summary>
    ///     Author: Giang
    ///     Content: BLL Coefficient Type
    /// </summary>
    public class CoefficientTypeBLL
    {
        #region Constructor

        public CoefficientTypeBLL(int coefficientTypeId, string coefficientName, int salaryRegulationId, int dataType)
        {
            CoefficientTypeId = coefficientTypeId;
            CoefficientName = coefficientName;
            SalaryRegulationId = salaryRegulationId;
            DataType = dataType;
        }

        #endregion

        #region Private Fields

        #endregion

        #region Properties

        public int CoefficientTypeId { get; set; }

        public string CoefficientName { get; set; }

        public int SalaryRegulationId { get; set; }

        public double Level1 { get; set; }

        public double Month1 { get; set; }

        public double Level2 { get; set; }

        public double Month2 { get; set; }

        public double Level3 { get; set; }

        public double Month3 { get; set; }

        public double Level4 { get; set; }

        public double Month4 { get; set; }

        public double Level5 { get; set; }

        public double Month5 { get; set; }

        public double Level6 { get; set; }

        public double Month6 { get; set; }

        public double Level7 { get; set; }

        public double Month7 { get; set; }

        public double Level8 { get; set; }

        public double Month8 { get; set; }

        public double Level9 { get; set; }

        public double Month9 { get; set; }

        public double Level10 { get; set; }

        public double Month10 { get; set; }

        public double Level11 { get; set; }

        public double Month11 { get; set; }

        public double Level12 { get; set; }

        public double Month12 { get; set; }

        public int DataType { get; set; }

        #endregion

        #region Get

        public static DataTable GetAll(int type, int salaryregulationid)
        {
            return new CoefficientTypeDAL().GetAll(type, salaryregulationid);
        }

        public static DataRow GetValue_By_ID_Level(int CoefficientTypeId, int Level, int Type, int SalaryRegulationId)
        {
            var dt = new CoefficientTypeDAL().GetValue_By_ID_Level(CoefficientTypeId, Level, Type, SalaryRegulationId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetCoefficientTypeId_By_CoefficientName(int DataType, int SalaryRegulationId,
            string CoefficientName)
        {
            var dt = new CoefficientTypeDAL().GetCoefficientTypeId_By_CoefficientName(DataType, SalaryRegulationId,
                CoefficientName);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetCoefficientType_By_Name(string CoefficientName)
        {
            var dt = new CoefficientTypeDAL().GetCoefficientType_By_Name(CoefficientName);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        #endregion

        #region Inset, Update, Delete

        public static long Insert(string coefficientName,
            double value_1, double month_1, double value_2, double month_2, double value_3, double month_3,
            double value_4, double month_4, double value_5, double month_5, double value_6, double month_6,
            double value_7, double month_7, double value_8, double month_8, double value_9, double month_9,
            double value_10, double month_10, double value_11, double month_11, double value_12, double month_12,
            int dataType, int salaryRegulationId)
        {
            var objDAL = new CoefficientTypeDAL();
            return objDAL.Insert(coefficientName,
                value_1, month_1, value_2, month_2, value_3, month_3,
                value_4, month_4, value_5, month_5, value_6, month_6,
                value_7, month_7, value_8, month_8, value_9, month_9,
                value_10, month_10, value_11, month_11, value_12, month_12,
                dataType, salaryRegulationId);
        }

        public static long Update(string coefficientName,
            double value_1, double month_1, double value_2, double month_2, double value_3, double month_3,
            double value_4, double month_4, double value_5, double month_5, double value_6, double month_6,
            double value_7, double month_7, double value_8, double month_8, double value_9, double month_9,
            double value_10, double month_10, double value_11, double month_11, double value_12, double month_12,
            int dataType, int salaryRegulationId, int coefficientTypeId)
        {
            var objDAL = new CoefficientTypeDAL();
            return objDAL.Update(coefficientName,
                value_1, month_1, value_2, month_2, value_3, month_3,
                value_4, month_4, value_5, month_5, value_6, month_6,
                value_7, month_7, value_8, month_8, value_9, month_9,
                value_10, month_10, value_11, month_11, value_12, month_12,
                dataType, salaryRegulationId, coefficientTypeId);
        }

        public static long DeleteByNameId(int coefficientTypeId)
        {
            var objDAL = new CoefficientTypeDAL();
            return objDAL.Delete(coefficientTypeId);
        }

        #endregion
    }
}