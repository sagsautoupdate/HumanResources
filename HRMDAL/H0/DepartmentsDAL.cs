using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class DepartmentsDAL : Dao
    {
        #region methods inset, update , delete

        public long InsertV1(int parentId, string departmentCode, string departmentName, string departmentNameE,
            string description, bool direct, int RootId, int Level, int Sortby, string DepartmentFullName)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ParentId", SqlDbType.Int),
                    new SqlParameter("@DepartmentCode", SqlDbType.VarChar, 10),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DepartmentNameE", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Direct", SqlDbType.Int),
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@Level", SqlDbType.Int),
                    new SqlParameter("@Sortby", SqlDbType.Int),
                    new SqlParameter("@DepartmentFullName", SqlDbType.NVarChar, 254)
                };

                param[0].Value = parentId;
                param[1].Value = departmentCode;
                param[2].Value = departmentName;
                param[3].Value = departmentNameE;
                param[4].Value = description;
                param[5].Value = direct;
                param[6].Value = RootId;
                param[7].Value = Level;
                param[8].Value = Sortby;
                param[9].Value = DepartmentFullName;

                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_INSERTV1, param);
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

        public long UpdateV1(string departmentCode, string departmentName, string departmentNameE, string description,
            bool direct, int RootId, int Level, int Sortby, string DepartmentFullName, int departmentId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentCode", SqlDbType.VarChar, 10),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DepartmentNameE", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Direct", SqlDbType.Int),
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@Level", SqlDbType.Int),
                    new SqlParameter("@Sortby", SqlDbType.Int),
                    new SqlParameter("@DepartmentFullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DepartmentId", SqlDbType.Int)
                };

                param[0].Value = departmentCode;
                param[1].Value = departmentName;
                param[2].Value = departmentNameE;
                param[3].Value = description;
                param[4].Value = direct;
                param[5].Value = RootId;
                param[6].Value = Level;
                param[7].Value = Sortby;
                param[8].Value = DepartmentFullName;
                param[9].Value = departmentId;
                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_UPDATEV1, param);
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

        public long Insert(int parentId, string departmentCode, string departmentName, string departmentNameE,
            string description)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ParentId", SqlDbType.Int),
                    new SqlParameter("@DepartmentCode", SqlDbType.VarChar, 10),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DepartmentNameE", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 254)
                };

                param[0].Value = parentId;
                param[1].Value = departmentCode;
                param[2].Value = departmentName;
                param[3].Value = departmentNameE;
                param[4].Value = description;

                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_INSERT, param);
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

        public long Update(string departmentCode, string departmentName, string departmentNameE, string description,
            int departmentId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentCode", SqlDbType.VarChar, 10),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DepartmentNameE", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DepartmentId", SqlDbType.Int)
                };

                param[0].Value = departmentCode;
                param[1].Value = departmentName;
                param[2].Value = departmentNameE;
                param[3].Value = description;
                param[4].Value = departmentId;
                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_UPDATE, param);
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

        public long Delete(int departmentId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int)
                };

                param[0].Value = departmentId;

                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_DELETE, param);
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

        #region Methods Get       

        public DataTable GetMaxSortNumber(int parentId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ParentId", SqlDbType.Int)
                };
                param[0].Value = parentId;

                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_MAX_SORT, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAllRoots(int DirectOrInDirect)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DirectOrInDirect", SqlDbType.Int)
                };
                param[0].Value = DirectOrInDirect;

                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_GET_ALL_ROOT, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAllDepartments()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_GET_ALL, null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetDepartmentRoot()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_GET_ROOT, null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetDepartmentRootBySub(int subId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SubId", SqlDbType.Int)
                };
                param[0].Value = subId;
                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_GET_ROOT_BY_SUB_ID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetDepartmentSubLevel(int parentId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ParentId", SqlDbType.Int)
                };
                param[0].Value = parentId;
                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_GETALL_SUB_LEVEL, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetDepartmentSubLevelByRootIdDepartmentId(int parentId, int rootId, int departmentId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ParentId", SqlDbType.Int),
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@DepartmentId", SqlDbType.Int)
                };
                param[0].Value = parentId;
                param[1].Value = rootId;
                param[2].Value = departmentId;

                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENTS_SUBLEVEL_BY_ROOTID_DEPARTMENTID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetById(int departmentId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int)
                };
                param[0].Value = departmentId;
                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_GET_BY_ID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByIdV1(int departmentId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int)
                };
                param[0].Value = departmentId;
                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_GET_BY_IDV1, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByIds(string departmentIds)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentIds", SqlDbType.VarChar, 254)
                };

                param[0].Value = departmentIds == null ? string.Empty : departmentIds;

                sproc = new StoreProcedure(DepartmentKeys.SP_SEL_H0_DEPARTMENTS_BY_IDS, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByIdsRoot(string departmentIds, int rootId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@RootId", SqlDbType.Int)
                };

                param[0].Value = departmentIds == null ? string.Empty : departmentIds;
                param[1].Value = rootId;
                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_GET_BY_IDS_ROOTID, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }


        public DataTable GetByRoot(int rootId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RootId", SqlDbType.Int)
                };

                param[0].Value = rootId;
                sproc = new StoreProcedure(DepartmentKeys.SP_SEL_H0_DEPARTMENTS_BY_ROOTID, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        #endregion
    }
}