using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class HTCVCatalogueDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public int Insert(string HTCVCatalogueName, string MarkDisplay, double? MarkDefault, int? TypeDisplay)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HTCVCatalogueName", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@MarkDisplay", SqlDbType.VarChar, 50),
                    new SqlParameter("@MarkDefault", SqlDbType.Float, 8),
                    new SqlParameter("@TypeDisplay", SqlDbType.Int, 4)
                };
                if (HTCVCatalogueName == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = HTCVCatalogueName;
                if (MarkDisplay == null)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = MarkDisplay;
                if (MarkDefault.HasValue)
                    param[2].Value = MarkDefault.Value;
                else
                    param[2].Value = DBNull.Value;
                if (TypeDisplay.HasValue)
                    param[3].Value = TypeDisplay.Value;
                else
                    param[3].Value = DBNull.Value;

                sproc = new StoreProcedure(HTCVCatalogueKeys.Sp_Ins_H1_HTCVCatalogue, param);
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

        public int Update(string HTCVCatalogueName, string MarkDisplay, double? MarkDefault, int? TypeDisplay,
            int? HTCVCatalogueId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HTCVCatalogueName", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@MarkDisplay", SqlDbType.VarChar, 50),
                    new SqlParameter("@MarkDefault", SqlDbType.Float, 8),
                    new SqlParameter("@TypeDisplay", SqlDbType.Int, 4),
                    new SqlParameter("@HTCVCatalogueId", SqlDbType.Int, 4)
                };

                if (HTCVCatalogueName == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = HTCVCatalogueName;
                if (MarkDisplay == null)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = MarkDisplay;
                if (MarkDefault.HasValue)
                    param[2].Value = MarkDefault.Value;
                else
                    param[2].Value = DBNull.Value;
                if (TypeDisplay.HasValue)
                    param[3].Value = TypeDisplay.Value;
                else
                    param[3].Value = DBNull.Value;
                if (HTCVCatalogueId.HasValue)
                    param[4].Value = HTCVCatalogueId.Value;
                else
                    param[4].Value = DBNull.Value;

                sproc = new StoreProcedure(HTCVCatalogueKeys.Sp_Upd_H1_HTCVCatalogue, param);
                sproc.Run();

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

        public int Delete(int? HTCVCatalogueId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HTCVCatalogueId", SqlDbType.Int, 4)
                };

                if (HTCVCatalogueId.HasValue)
                    param[0].Value = HTCVCatalogueId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(HTCVCatalogueKeys.Sp_Del_H1_HTCVCatalogue, param);
                sproc.Run();
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

        #region Methods GET

        public DataTable GetByFilter(string HTCVCatalogueName, string MarkDisplay, double? MarkDefault, int? TypeDisplay)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HTCVCatalogueName", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@MarkDisplay", SqlDbType.VarChar, 50),
                    new SqlParameter("@MarkDefault", SqlDbType.Float, 8),
                    new SqlParameter("@TypeDisplay", SqlDbType.Int, 4)
                };
                if (HTCVCatalogueName == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = HTCVCatalogueName;
                if (MarkDisplay == null)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = MarkDisplay;
                if (MarkDefault.HasValue)
                    param[2].Value = MarkDefault.Value;
                else
                    param[2].Value = DBNull.Value;
                if (TypeDisplay.HasValue)
                    param[3].Value = TypeDisplay.Value;
                else
                    param[3].Value = DBNull.Value;

                sproc = new StoreProcedure(HTCVCatalogueKeys.SP_Sel_H1_HTCVCatalogueByFilter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetById(int? HTCVCatalogueId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HTCVCatalogueId", SqlDbType.Int, 4)
                };
                if (HTCVCatalogueId.HasValue)
                    param[0].Value = HTCVCatalogueId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(HTCVCatalogueKeys.SP_Sel_H1_HTCVCatalogueById, param);
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