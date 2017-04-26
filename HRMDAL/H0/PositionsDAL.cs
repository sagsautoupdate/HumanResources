using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class PositionsDAL : Dao
    {
        #region Methods Get

        public DataTable GetPositionId(int positionId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionId", SqlDbType.Int, 4)
                };
                param[0].Value = positionId;
                sproc = new StoreProcedure("Sel_H0_Positions_By_Id", param);
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
                sproc = new StoreProcedure(PositionKeys.Sp_Sel_H0_Positions_All, null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByFilter(string positionName, int LevelPosition, int DepartmentId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@LevelPosition", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4)
                };

                param[0].Value = positionName;
                param[1].Value = LevelPosition;
                param[2].Value = DepartmentId;

                sproc = new StoreProcedure(PositionKeys.Sp_Sel_H0_Positions_ByFilter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetPositionLeader(string positionName, int LevelPosition, int DepartmentId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@LevelPosition", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4)
                };

                param[0].Value = positionName;
                param[1].Value = LevelPosition;
                param[2].Value = DepartmentId;

                sproc = new StoreProcedure(PositionKeys.Sp_Sel_H0_PositionsLeader_ByFilter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetOneByDepartmentId_LevelPosition(int DepartmentId, int LevelPosition)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4),
                    new SqlParameter("@LevelPosition", SqlDbType.Int, 4)
                };

                param[0].Value = DepartmentId;
                param[1].Value = LevelPosition;


                sproc = new StoreProcedure(PositionKeys.Sp_Sel_H0_Positions_ByDepartmentId_LevelPosition, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/08/2014
        ///     Content: Lay ds position
        /// </summary>
        /// <param name="Hide"></param>
        /// <returns></returns>
        public DataTable GetDataByHide(int Hide)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Hide", SqlDbType.Int)
                };

                param[0].Value = Hide;

                sproc = new StoreProcedure(PositionKeys.Sp_Sel_H0_Positions_All, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAllToDTForEducationContract()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                };

                sproc = new StoreProcedure("Sel_H0_Positions_For_EducationContract", null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAllView()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    //new SqlParameter("@Hide",SqlDbType.Int)
                };

                //param[0].Value = Hide;

                sproc = new StoreProcedure(PositionKeys.Sp_Sel_H0_PositionsV1, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        //public DataTable GetByIds(string positionIds)
        //{

        //    Debug.Assert(sproc == null);
        //    DataTable datatable = new DataTable();
        //    try
        //    {
        //        SqlParameter[] param = 
        //        {                                
        //            new SqlParameter("@PositionIds",SqlDbType.VarChar,1000),					
        //        };

        //        param[0].Value = positionIds;

        //        sproc = new StoreProcedure(PositionKeys.SP_POSITION_GET_BY_IDs, param);
        //        sproc.RunFill(datatable);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return datatable;

        //}


        //public DataTable GetIsRecruitment()
        //{

        //    Debug.Assert(sproc == null);
        //    DataTable datatable = new DataTable();
        //    try
        //    {
        //        sproc = new StoreProcedure(PositionKeys.SP_POSITION_IS_RECRUITMENT, null);
        //        sproc.RunFill(datatable);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return datatable;

        //}

        #endregion

        #region methods inset, update , delete

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/09/2014
        ///     Content: Them JobDescription
        /// </summary>
        /// <returns></returns>
        public long Insert_V1(string positionName, string description, int LevelPosition, int DepartmentId,
            string JobDecription, int Hide, double F, double Om)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@LevelPosition", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4),
                    new SqlParameter("@JobDescription", SqlDbType.NText),
                    new SqlParameter("@Hide", SqlDbType.Int),
                    new SqlParameter("@F", SqlDbType.Float),
                    new SqlParameter("@Om", SqlDbType.Float)
                };

                param[0].Value = positionName;
                param[1].Value = description;
                param[2].Value = LevelPosition;
                param[3].Value = DepartmentId;
                param[4].Value = JobDecription;
                param[5].Value = Hide;
                param[6].Value = F;
                param[7].Value = Om;

                sproc = new StoreProcedure(PositionKeys.Sp_Ins_H0_PositionV1, param);
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

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/09/2014
        ///     Content: Them JobDescription
        /// </summary>
        /// <returns></returns>
        public long Update_V1(int positionId, string positionName, string description, int LevelPosition,
            int DepartmentId, string JobDecription, double F, double Om)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@LevelPosition", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4),
                    new SqlParameter("@JobDescription", SqlDbType.NText),
                    new SqlParameter("@F", SqlDbType.Float),
                    new SqlParameter("@Om", SqlDbType.Float)
                };

                param[0].Value = positionId;
                param[1].Value = positionName;
                param[2].Value = description;
                param[3].Value = LevelPosition;
                param[4].Value = DepartmentId;
                param[5].Value = JobDecription;
                param[6].Value = F;
                param[7].Value = Om;

                sproc = new StoreProcedure(PositionKeys.Sp_Upd_H0_PositionV1, param);
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

        public long Insert(string positionName, string description, int LevelPosition, int DepartmentId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@LevelPosition", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4)
                };

                param[0].Value = positionName;
                param[1].Value = description;
                param[2].Value = LevelPosition;
                param[3].Value = DepartmentId;

                sproc = new StoreProcedure(PositionKeys.Sp_Ins_H0_Position, param);
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

        public long Update(int positionId, string positionName, string description, int LevelPosition, int DepartmentId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@LevelPosition", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4)
                };

                param[0].Value = positionId;
                param[1].Value = positionName;
                param[2].Value = description;
                param[3].Value = LevelPosition;
                param[4].Value = DepartmentId;

                sproc = new StoreProcedure(PositionKeys.Sp_Upd_H0_Position, param);
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

        public long Delete(int positionId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionId", SqlDbType.Int)
                };

                param[0].Value = positionId;

                sproc = new StoreProcedure(PositionKeys.SP_POSITION_DELETE, param);
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