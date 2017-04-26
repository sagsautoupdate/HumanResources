using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class CoefficientNamesDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        //public int Insert(string name, string description)
        //{
        //    int identity = 0;
        //    Debug.Assert(sproc == null);
        //    try
        //    {
        //        SqlParameter[] param = 
        //        {
        //            new SqlParameter("@LNS_CoefficientName",SqlDbType.NVarChar, 100),
        //            new SqlParameter("@LNS_CoefficientNameDescription",SqlDbType.NVarChar, 1000),

        //        };
        //        param[0].Value = name;
        //        param[1].Value = description;        

        //        sproc = new StoreProcedure(CoefficientNameKeys.SP_LNS_COEFFICIENT_NAME_INSERT, param);
        //        identity = sproc.Run();
        //        sproc.Dispose();

        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }
        //    return identity;
        //}

        //public int Update(int id, string name, string description)
        //{
        //    int identity = 0;
        //    Debug.Assert(sproc == null);
        //    try
        //    {
        //        SqlParameter[] param =    
        //        {
        //            new SqlParameter("@LNS_CoefficientNameId",SqlDbType.Int),
        //            new SqlParameter("@LNS_CoefficientName",SqlDbType.NVarChar, 100),
        //            new SqlParameter("@LNS_CoefficientNameDescription",SqlDbType.NVarChar, 1000),
        //        };

        //        param[0].Value = id;
        //        param[1].Value = name;
        //        param[2].Value = description;

        //        sproc = new StoreProcedure(CoefficientNameKeys.SP_LNS_COEFFICIENT_NAME_UPDATE, param);
        //        sproc.Run();
        //        sproc.Dispose();

        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }
        //    return identity;
        //}

        //public int Delete(int id)
        //{
        //    int identity = 0;
        //    Debug.Assert(sproc == null);
        //    try
        //    {
        //        SqlParameter[] param = 
        //        {
        //            new SqlParameter("@LNS_CoefficientNameId",SqlDbType.Int)
        //        };

        //        param[0].Value = id;

        //        sproc = new StoreProcedure(CoefficientNameKeys.SP_LNS_COEFFICIENT_NAME_DELETE, param);
        //        sproc.Run();
        //        sproc.Dispose();

        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }
        //    return identity;
        //}

        #endregion

        #region Get
        public DataTable GetAllNames(int type)
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
                sproc = new StoreProcedure(CoefficientNameKeys.Sp_Sel_H1_CoefficientNames_All, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByInUseTypeId(bool InUse, int TypeId, int Type)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@InUse", SqlDbType.Bit),
                    new SqlParameter("@TypeId", SqlDbType.Int),
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = InUse;
                param[1].Value = TypeId;
                param[2].Value = Type;
                sproc = new StoreProcedure(CoefficientNameKeys.Sp_Sel_H1_CoefficientNamesByInUseTypeId, param);
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
    }
}