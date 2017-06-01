using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Karaok.Models;

namespace Karaok.Models
{
    public class SQLHelper : DBConnection
    {

        public static DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(query, CurrentConnection());
            //SqlTransaction tran = default(SqlTransaction);
            //tran = CurrentConnection().BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                adp.Fill(dt);
                //tran.Commit();
                CloseCurrentConnection();
            }
            catch (Exception ex)
            {

                //tran.Rollback();
                ex.ToString();
            }
            finally {
                CloseCurrentConnection();
            }
            
            
            return dt;
        }

        public static bool GetBoolValue(string query)
        {
            bool checkValue = false;
            SqlCommand cmd = new SqlCommand(query, CurrentConnection());
            try
            {

                Int32 count = (Int32)cmd.ExecuteScalar();
                if (count > 0)
                {
                    checkValue = true;
                }
                else
                {
                    checkValue = false;
                }
                cmd.Dispose();
                CloseCurrentConnection();
            }
            catch (Exception ex)
            {
                checkValue = false;
                ex.ToString();
            }
            finally
            {
                CloseCurrentConnection();
            }
            return checkValue;
        }

        public static string GetStringValue(string query)
        {
            string resultString = "";
            SqlCommand cmd = new SqlCommand(query, CurrentConnection());
            SqlTransaction tran = default(SqlTransaction);
            tran = CurrentConnection().BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                cmd.CommandTimeout = 0;
                resultString = cmd.ExecuteScalar().ToString();
                cmd.Transaction = tran;
                tran.Commit();
                CloseCurrentConnection();
            }
            catch (Exception ex)
            {

                tran.Rollback();
                ex.ToString();
            }
            finally {
                CloseCurrentConnection();
            }
            
            return resultString;

        }

        

        public static void ExecuteSQLQuery(string query, string parameter)
        {
            string fullQuery = query + (string.IsNullOrEmpty(parameter) ? "" : parameter).ToString();
            SqlCommand cmd = new SqlCommand(fullQuery, CurrentConnection());
            SqlTransaction tran = default(SqlTransaction);
            tran = CurrentConnection().BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                cmd.CommandTimeout = 36000;         
                cmd.ExecuteNonQuery();
                cmd.Transaction = tran;
                cmd.Dispose();
                tran.Commit();
                
            }
            catch (Exception ex)
            {

                tran.Rollback();
                ex.ToString();
            }
            finally
            { 
                CloseCurrentConnection();
            }
            

        }

        public static void ExecuteSQLQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, CurrentConnection());
            SqlTransaction tran = default(SqlTransaction);
            //tran = CurrentConnection().BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {                
                cmd.ExecuteNonQuery();
                cmd.Transaction = tran;
                cmd.CommandTimeout = 36000;
                cmd.Dispose();
                //tran.Commit();
                CloseCurrentConnection();
            }
            catch (Exception ex)
            {

                //tran.Rollback();
                ex.ToString();
            }
            finally
            {
                CloseCurrentConnection();
            }
            
        }

        public static bool ExecuteNoneQuery(string commandName, CommandType commandType, SqlParameter[] paras)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = CurrentConnection();
            SqlTransaction tran = default(SqlTransaction);
            tran = CurrentConnection().BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                
                cmd.CommandText = commandName;
                cmd.CommandType = commandType;
                cmd.CommandTimeout = 36000;

                if (paras.Length > 0)
                {
                    cmd.Parameters.AddRange(paras);
                }
                result = cmd.ExecuteNonQuery();
                cmd.Transaction = tran;
                tran.Commit();
                CloseCurrentConnection();
                return (result > 0);
            }
            catch (Exception ex)
            {

                tran.Rollback();
                ex.ToString();
            }
            finally 
            {
                CloseCurrentConnection();
            }
            return (result > 0);
        }
    }
}