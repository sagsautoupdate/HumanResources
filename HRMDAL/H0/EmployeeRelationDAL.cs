using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class EmployeeRelationDAL : Dao
    {
        #region Insert, Update, Delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int? UserId,
            int? RelationTypeId,
            string RFullName,
            int? RDayOfBirth,
            int? RMonthOfBirth,
            int? RYearOfBirth,
            string RNativePlace,
            string RResident,
            string RLive,
            string Before1975,
            string After1975,
            string Participate,
            bool? Died,
            string DiedCause,
            string Others)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@RelationTypeId", SqlDbType.Int, 4),
                    new SqlParameter("@RFullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RDayOfBirth", SqlDbType.Int, 4),
                    new SqlParameter("@RMonthOfBirth", SqlDbType.Int, 4),
                    new SqlParameter("@RYearOfBirth", SqlDbType.Int, 4),
                    new SqlParameter("@RNativePlace", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RResident", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RLive", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Before1975", SqlDbType.NText),
                    new SqlParameter("@After1975", SqlDbType.NText),
                    new SqlParameter("@Participate", SqlDbType.NVarChar),
                    new SqlParameter("@Died", SqlDbType.Bit, 1),
                    new SqlParameter("@DiedCause", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Others", SqlDbType.NVarChar, 254)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (RelationTypeId.HasValue)
                    param[1].Value = RelationTypeId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (RFullName == null)
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = RFullName;
                if (RDayOfBirth.HasValue)
                    param[3].Value = RDayOfBirth.Value;
                else
                    param[3].Value = DBNull.Value;
                if (RMonthOfBirth.HasValue)
                    param[4].Value = RMonthOfBirth.Value;
                else
                    param[4].Value = DBNull.Value;
                if (RYearOfBirth.HasValue)
                    param[5].Value = RYearOfBirth.Value;
                else
                    param[5].Value = DBNull.Value;
                if (RNativePlace == null)
                    param[6].Value = DBNull.Value;
                else
                    param[6].Value = RNativePlace;
                if (RResident == null)
                    param[7].Value = DBNull.Value;
                else
                    param[7].Value = RResident;
                if (RLive == null)
                    param[8].Value = DBNull.Value;
                else
                    param[8].Value = RLive;
                if (Before1975 == null)
                    param[9].Value = DBNull.Value;
                else
                    param[9].Value = Before1975;
                if (After1975 == null)
                    param[10].Value = DBNull.Value;
                else
                    param[10].Value = After1975;
                if (Participate == null)
                    param[11].Value = DBNull.Value;
                else
                    param[11].Value = Participate;
                if (Died.HasValue)
                    param[12].Value = Died.Value;
                else
                    param[12].Value = DBNull.Value;
                if (DiedCause == null)
                    param[13].Value = DBNull.Value;
                else
                    param[13].Value = DiedCause;
                if (Others == null)
                    param[14].Value = DBNull.Value;
                else
                    param[14].Value = Others;

                sproc = new StoreProcedure(EmployeeRelationKeys.Sp_EmployeeRelation_Insert, param);
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

        public long Update(int? UserId,
            int? RelationTypeId,
            string RFullName,
            int? RDayOfBirth,
            int? RMonthOfBirth,
            int? RYearOfBirth,
            string RNativePlace,
            string RResident,
            string RLive,
            string Before1975,
            string After1975,
            string Participate,
            bool? Died,
            string DiedCause,
            string Others,
            long? UserRelationId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@RelationTypeId", SqlDbType.Int, 4),
                    new SqlParameter("@RFullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RDayOfBirth", SqlDbType.Int, 4),
                    new SqlParameter("@RMonthOfBirth", SqlDbType.Int, 4),
                    new SqlParameter("@RYearOfBirth", SqlDbType.Int, 4),
                    new SqlParameter("@RNativePlace", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RResident", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RLive", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Before1975", SqlDbType.NText),
                    new SqlParameter("@After1975", SqlDbType.NText),
                    new SqlParameter("@Participate", SqlDbType.NVarChar),
                    new SqlParameter("@Died", SqlDbType.Bit, 1),
                    new SqlParameter("@DiedCause", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Others", SqlDbType.NVarChar, 254),
                    new SqlParameter("@UserRelationId", SqlDbType.BigInt, 8)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (RelationTypeId.HasValue)
                    param[1].Value = RelationTypeId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (RFullName == null)
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = RFullName;
                if (RDayOfBirth.HasValue)
                    param[3].Value = RDayOfBirth.Value;
                else
                    param[3].Value = DBNull.Value;
                if (RMonthOfBirth.HasValue)
                    param[4].Value = RMonthOfBirth.Value;
                else
                    param[4].Value = DBNull.Value;
                if (RYearOfBirth.HasValue)
                    param[5].Value = RYearOfBirth.Value;
                else
                    param[5].Value = DBNull.Value;
                if (RNativePlace == null)
                    param[6].Value = DBNull.Value;
                else
                    param[6].Value = RNativePlace;
                if (RResident == null)
                    param[7].Value = DBNull.Value;
                else
                    param[7].Value = RResident;
                if (RLive == null)
                    param[8].Value = DBNull.Value;
                else
                    param[8].Value = RLive;
                if (Before1975 == null)
                    param[9].Value = DBNull.Value;
                else
                    param[9].Value = Before1975;
                if (After1975 == null)
                    param[10].Value = DBNull.Value;
                else
                    param[10].Value = After1975;
                if (Participate == null)
                    param[11].Value = DBNull.Value;
                else
                    param[11].Value = Participate;
                if (Died.HasValue)
                    param[12].Value = Died.Value;
                else
                    param[12].Value = DBNull.Value;
                if (DiedCause == null)
                    param[13].Value = DBNull.Value;
                else
                    param[13].Value = DiedCause;
                if (Others == null)
                    param[14].Value = DBNull.Value;
                else
                    param[14].Value = Others;
                if (UserRelationId.HasValue)
                    param[15].Value = UserRelationId.Value;
                else
                    param[15].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeRelationKeys.Sp_EmployeeRelation_Update, param);
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


        public long Delete(long? UserRelationId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserRelationId", SqlDbType.BigInt, 8)
                };

                if (UserRelationId.HasValue)
                    param[0].Value = UserRelationId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeRelationKeys.Sp_EmployeeRelation_Delete, param);
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

        #region Get

        public DataTable GetByUserRelationId(int UserRelationId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserRelationId", SqlDbType.Int, 4)
                };

                param[0].Value = UserRelationId;

                sproc = new StoreProcedure("Sel_H0_EmployeeRelation_By_UserRelationId", param);
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

        public DataTable GetByFilter(int? RelationTypeId, int? UserId, int? Type)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RelationTypeId", SqlDbType.Int, 4),
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Type", SqlDbType.Int, 4)
                };

                if (RelationTypeId.HasValue)
                    param[0].Value = RelationTypeId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserId.HasValue)
                    param[1].Value = UserId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Type.HasValue)
                    param[2].Value = Type.Value;
                else
                    param[2].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeRelationKeys.Sp_EmployeeRelation_Get_By_Filter, param);
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

        #endregion
    }
}