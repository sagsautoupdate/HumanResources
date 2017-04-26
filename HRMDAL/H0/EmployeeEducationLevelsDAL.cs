using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class EmployeeEducationLevelsDAL : Dao
    {
        #region Methods Get       

        public DataTable GetById(int userId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = userId;

                sproc = new StoreProcedure(EmployeeEducationLevelKeys.SP_EMPLOYEE_EDUCATION_LEVEL_GET_BY_ID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByFilter(int userId, int EducationLevelId, string EducationLevelValue)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelValue", SqlDbType.NVarChar, 4000)
                };

                param[0].Value = userId;
                param[1].Value = EducationLevelId;
                param[2].Value = EducationLevelValue;

                sproc = new StoreProcedure("Sel_H0_EmployeeEducationLevel_By_Filter", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        #endregion

        #region methods inset, update , delete

        public long Insert(int userId, int educationLevelId, string educationLevelValue, string remark)
            //, string trainingPlace, string trainingDepartment, string major, DateTime graduatingYear, string grade, string profession)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelValue", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                    /*new SqlParameter("@TrainingPlace",SqlDbType.NVarChar, 1000),
                    new SqlParameter("@TrainingDepartment",SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Major",SqlDbType.NVarChar, 254),
                    new SqlParameter("@GraduatingYear",SqlDbType.DateTime),
                    new SqlParameter("@Grade",SqlDbType.NVarChar, 254),
                    new SqlParameter("@Profession",SqlDbType.NVarChar, 254),*/
                };

                param[0].Value = userId;
                param[1].Value = educationLevelId;
                param[2].Value = educationLevelValue;
                param[3].Value = remark;
                /*param[4].Value = trainingPlace;
                param[5].Value = trainingDepartment;
                param[6].Value = major;
                param[7].Value = graduatingYear;
                param[8].Value = grade;
                param[9].Value = profession;*/

                sproc = new StoreProcedure(EmployeeEducationLevelKeys.SP_EMPLOYEE_EDUCATION_LEVEL_INSERT, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long UpdateHighest(int userId, int educationLevelId, int Id)
            //, string trainingPlace, string trainingDepartment, string major, DateTime graduatingYear, string grade, string profession)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelId", SqlDbType.Int),
                    new SqlParameter("@Id", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = educationLevelId;
                param[2].Value = Id;

                sproc = new StoreProcedure(EmployeeEducationLevelKeys.SP_EMPLOYEE_EDUCATION_LEVEL_UPDATE_HIGHEST, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long Update(int userId, int educationLevelId, string educationLevelValue, string remark, int id)
            //, string trainingPlace, string trainingDepartment, string major, DateTime graduatingYear, string grade, string profession)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelValue", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Id", SqlDbType.Int)
                    /*new SqlParameter("@TrainingPlace",SqlDbType.NVarChar, 1000),
                    new SqlParameter("@TrainingDepartment",SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Major",SqlDbType.NVarChar, 254),
                    new SqlParameter("@GraduatingYear",SqlDbType.DateTime),
                    new SqlParameter("@Grade",SqlDbType.NVarChar, 254),
                    new SqlParameter("@Profession",SqlDbType.NVarChar, 254),*/
                };

                param[0].Value = userId;
                param[1].Value = educationLevelId;
                param[2].Value = educationLevelValue;
                param[3].Value = remark == null ? string.Empty : remark;
                param[4].Value = id;
                /*param[5].Value = trainingPlace;
                param[6].Value = trainingDepartment;
                param[7].Value = major;
                param[8].Value = graduatingYear;
                param[9].Value = grade;
                param[10].Value = profession;*/

                sproc = new StoreProcedure(EmployeeEducationLevelKeys.SP_EMPLOYEE_EDUCATION_LEVEL_UPDATEV1, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long Delete(int id)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Id", SqlDbType.Int)
                };

                param[0].Value = id;
                sproc = new StoreProcedure(EmployeeEducationLevelKeys.SP_EMPLOYEE_EDUCATION_LEVEL_DELETE, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        #endregion
    }
}