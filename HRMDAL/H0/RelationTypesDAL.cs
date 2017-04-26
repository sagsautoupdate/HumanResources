using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class RelationTypesDAL : Dao
    {
        #region Methods Get

        public DataTable GetById(int positionId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RelationTypeId", SqlDbType.Int, 4)
                };
                param[0].Value = positionId;
                sproc = new StoreProcedure("Sel_H0_RelationType_By_Id", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(RelationTypeKeys.SP_RELATION_TYPE_GET_ALL, null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAllRelationGroup()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_RelationTypeGroup_All", null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByFilter(string relationTypeName, int type)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RelationTypeName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = relationTypeName == null ? string.Empty : relationTypeName;
                param[1].Value = type;

                sproc = new StoreProcedure(RelationTypeKeys.SP_RELATION_TYPE_GET_BY_FILTER, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByFilterByType(int type)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = type;

                sproc = new StoreProcedure(RelationTypeKeys.SP_RELATION_TYPE_GET_BY_TYPE, param);
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

        #region methods insert, update , delete

        public long Insert(string relationTypeName, string description, int type)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RelationTypeName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = relationTypeName;
                param[1].Value = description;
                param[2].Value = type;

                sproc = new StoreProcedure(RelationTypeKeys.SP_RELATION_TYPE_INSERT, param);
                identity = sproc.Run();
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

        public long Update(string relationTypeName, string description, int type, int relationTypeId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RelationTypeName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@RelationTypeId", SqlDbType.Int)
                };

                param[0].Value = relationTypeName;
                param[1].Value = description == null ? string.Empty : description;
                param[2].Value = type;
                param[3].Value = relationTypeId;

                sproc = new StoreProcedure(RelationTypeKeys.SP_RELATION_TYPE_UPDATE, param);
                identity = sproc.Run();
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

        public long Delete(int relationTypeId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RelationTypeId", SqlDbType.Int)
                };

                param[0].Value = relationTypeId;

                sproc = new StoreProcedure(RelationTypeKeys.SP_RELATION_TYPE_DELETE, param);
                identity = sproc.Run();
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