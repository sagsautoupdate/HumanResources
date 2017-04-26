using System;
using System.Data;
using HRMDAL.H1;

namespace HRMBLL.H1
{
    public class ScaleOfSalariesBLL
    {
        #region Private Fields

        #endregion

        #region Properties

        public int ScaleOfSalaryId { get; set; }

        public string PositionName { get; set; }

        public string Code { get; set; }

        public double Value { get; set; }

        #endregion

        #region Get Methods

        public static DataTable GetAll()
        {
            return new ScaleOfSalariesDAL().GetAll();
        }

        public static DataTable GetAllWithFilter(int Active)
        {
            return new ScaleOfSalariesDAL().GetAllWithFilter(Active);
        }

        public static DataRow GetOne(int ScaleOfSalaryId)
        {
            var dt = new ScaleOfSalariesDAL().GetOne(ScaleOfSalaryId);
            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetByName(string PositionName)
        {
            var dt = new ScaleOfSalariesDAL().GetByName(PositionName);
            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataTable DT_GetByName(string PositionName)
        {
            return new ScaleOfSalariesDAL().GetByName(PositionName);
        }

        #endregion

        #region Insert, Update, Delete

        public static long Insert_Coefficient(string PositionName, string Code, double Value1, double Value2,
            double Value3,
            string JobDescription, DateTime AppliedDate)
        {
            return new ScaleOfSalariesDAL().Insert_Coefficient(PositionName, Code, Value1, Value2, Value3,
                JobDescription,
                AppliedDate);
        }

        public static long Update_Coefficient(int ScaleOfSalaryId, string PositionName, string Code, double Value1,
            double Value2,
            double Value3, string JobDescription, DateTime AppliedDate)
        {
            return new ScaleOfSalariesDAL().Update_Coefficient(ScaleOfSalaryId, PositionName, Code, Value1, Value2,
                Value3,
                JobDescription, AppliedDate);
        }

        public static long Insert(string PositionName, string Code, double Value1, double Value2, double Value3,
            string JobDescription, DateTime AppliedDate)
        {
            return new ScaleOfSalariesDAL().Insert(PositionName, Code, Value1, Value2, Value3, JobDescription,
                AppliedDate);
        }

        public static long Update(int ScaleOfSalaryId, string PositionName, string Code, double Value1, double Value2,
            double Value3, string JobDescription, DateTime AppliedDate)
        {
            return new ScaleOfSalariesDAL().Update(ScaleOfSalaryId, PositionName, Code, Value1, Value2, Value3,
                JobDescription, AppliedDate);
        }

        public static long Delete(int ScaleOfSalaryId)
        {
            return new ScaleOfSalariesDAL().Delete(ScaleOfSalaryId);
        }

        #endregion
    }
}