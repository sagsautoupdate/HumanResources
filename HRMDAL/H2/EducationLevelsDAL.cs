using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H2;

namespace HRMDAL.H2
{
    public class EducationLevelsDAL : Dao
    {
        #region Methods Get

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(EducationLevelKeys.SP_EDUCATION_LEVEL_GET_ALL, null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByFilter(string name, int orderByType)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Name", SqlDbType.NVarChar, 254),
                    new SqlParameter("@OrderByType", SqlDbType.Int)
                };

                param[0].Value = name;
                param[1].Value = orderByType;

                sproc = new StoreProcedure(EducationLevelKeys.Sp_Sel_EducationLevel_All_By_Filter, param);
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

        public long Insert(string name, string remark)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Name", SqlDbType.NVarChar),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 254)
                };

                param[0].Value = name;
                param[1].Value = remark;

                sproc = new StoreProcedure(EducationLevelKeys.SP_EDUCATION_LEVEL_INSERT, param);
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

        public long Update(string name, string remark, int educationLevelId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Name", SqlDbType.NVarChar),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 254),
                    new SqlParameter("@EducationLevelId", SqlDbType.Int)
                };

                param[0].Value = name;
                param[1].Value = remark == null ? string.Empty : remark;
                param[2].Value = educationLevelId;

                sproc = new StoreProcedure(EducationLevelKeys.SP_EDUCATION_LEVEL_UPDATE, param);
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

        public long Delete(int educationLevelId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EducationLevelId", SqlDbType.Int)
                };

                param[0].Value = educationLevelId;
                sproc = new StoreProcedure(EducationLevelKeys.SP_EDUCATION_LEVEL_DELETE, param);
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