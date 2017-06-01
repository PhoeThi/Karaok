using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Karaok.Models
{
    public class DBConnection
    {
        private static SqlConnection dbCon = new SqlConnection(DBConnectionString());
        public static DbCommand Command;
        public static DbTransaction Transaction;
        public static DbConnection Connection;
        private static bool useTransaction;
        public static bool UseTransaction
        {
            get { return useTransaction; }
        }

        #region Helper Function
        public static object GetNull(object obj)
        {
            if (obj is String && obj.ToString() == "")
                obj = DBNull.Value;
            if (obj is DateTime && ((DateTime)obj) == DateTime.MinValue)
                obj = DBNull.Value;
            else if (obj is int && ((int)obj) == int.MinValue)
                obj = DBNull.Value;
            else if (obj is float && ((float)obj) == float.MinValue)
                obj = DBNull.Value;
            else if (obj is decimal && ((decimal)obj) == decimal.MinValue)
                obj = DBNull.Value;
            else if (obj is double && ((double)obj) == double.MinValue)
                obj = DBNull.Value;

            return obj;
        }
        #endregion


        #region Transaction Control

        public void StartTransaction()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
            Transaction = Connection.BeginTransaction();
            useTransaction = true;
        }

        public void RollbackTransaction()
        {
            if (Transaction == null || Connection.State != ConnectionState.Open) return;

            Transaction.Rollback();
            if (Connection.State == ConnectionState.Open)
                Connection.Close();

            useTransaction = false;
        }

        public void CommitTransaction()
        {
            if (Transaction == null || Connection.State != ConnectionState.Open) return;

            Transaction.Commit();
            if (Connection.State == ConnectionState.Open)
                Connection.Close();

            useTransaction = false;
        }

        #endregion
        private static string DBConnectionString()
        {
            string DBString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return DBString;
        }

        public static SqlConnection CurrentConnection()
        {

            if (dbCon.State == ConnectionState.Closed)
            {
                try
                {
                    dbCon.Open();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //finally
                //{
                //    dbCon.Open();
                //}
                
            }
            return dbCon;
        }

        public static SqlConnection CloseCurrentConnection()
        {
            if (dbCon.State == ConnectionState.Open)
            {
                try
                {
                    dbCon.Close();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally {
                    dbCon.Close();
                }
                
            }
            return dbCon;
        }
    }
}