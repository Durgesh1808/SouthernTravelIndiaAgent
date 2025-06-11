using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.Common
{
    /// <summary>
    ///   /// This class provides methods to interact with the SQL database, including executing queries, stored procedures, and retrieving data.
    /// </summary>
    public class SqlData
    {

        /// <summary>
        /// /// This enum defines the different types of connection modules that can be used to connect to the database.
        /// </summary>
        public enum CONNECTION_MODULE
        {
            REGULAR = 1,
        }
        /// <summary>
        /// /// This static string holds the connection string for the SQL database, retrieved from the application configuration settings.
        /// </summary>

        public static string connStringServer = ConfigurationManager.AppSettings["southernconn"];

            private static SqlConnection GetConnection(CONNECTION_MODULE module)
            {
                if (module == CONNECTION_MODULE.REGULAR)
                {
                    return new SqlConnection(connStringServer);
                }
                return new SqlConnection(connStringServer);
            }

        /// <summary>
        /// /// This method retrieves a DataTable based on the provided SQL query or stored procedure, parameters, command type, and SQL connection.
        /// </summary>
        /// <param name="queryOrSP"></param>
        /// <param name="param"></param>
        /// <param name="comandType"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>

        private static DataTable GetDataTable(string queryOrSP, SqlParameter[] param, CommandType comandType, SqlConnection sqlConn)
            {
                DataTable dt = new DataTable();
                using (SqlCommand sqlCmd = new SqlCommand(queryOrSP, sqlConn))
                {
                    sqlCmd.CommandType = comandType;
                    sqlCmd.CommandTimeout = 500;
                    if (param != null)
                        sqlCmd.Parameters.AddRange(param);
                    SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);
                    sqlDA.Fill(dt);
                    sqlCmd.Parameters.Clear();
                }
                return dt;
            }


        /// <summary>
        /// /// This method retrieves a DataSet based on the provided SQL query or stored procedure, parameters, command type, and SQL connection.
        /// </summary>
        /// <param name="queryOrSP"></param>
        /// <param name="param"></param>
        /// <param name="comandType"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        private static DataSet GetDataSet(string queryOrSP, SqlParameter[] param, CommandType comandType, SqlConnection sqlConn)
            {
                DataSet ds = new DataSet();
                using (SqlCommand sqlCmd = new SqlCommand(queryOrSP, sqlConn))
                {
                    sqlCmd.CommandType = comandType;
                    sqlCmd.CommandTimeout = 500;
                    if (param != null)
                        sqlCmd.Parameters.AddRange(param);
                    SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);
                    sqlDA.Fill(ds);
                    sqlCmd.Parameters.Clear();
                }
                return ds;
            }

        /// <summary>
        /// /// This method retrieves a DataSet based on the provided stored procedure name and parameters.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="param"></param>
        /// <returns></returns>

        public static DataSet GetDataSetSP(string sqlSP, SqlParameter[] param)
            {
                return GetDataSet(sqlSP, param, CommandType.StoredProcedure, GetConnection(CONNECTION_MODULE.REGULAR));
            }


        /// <summary>
        /// /// This method retrieves a DataSet based on the provided SQL query and parameters.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>

        public static DataSet GetDataSetQuery(string sqlQuery, SqlParameter[] param)
            {
                return GetDataSet(sqlQuery, param, CommandType.Text, GetConnection(CONNECTION_MODULE.REGULAR));
            }

        /// <summary>
        /// /// This method retrieves a DataSet based on the provided stored procedure name, parameters, and connection module.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="param"></param>
        /// <param name="module"></param>
        /// <returns></returns>

        public static DataSet GetDataSetSP(string sqlSP, SqlParameter[] param, CONNECTION_MODULE module)
            {
                return GetDataSet(sqlSP, param, CommandType.StoredProcedure, GetConnection(module));
            }

        /// <summary>
        /// /// This method retrieves a DataSet based on the provided SQL query, parameters, and connection module.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <param name="module"></param>
        /// <returns></returns>

        public static DataSet GetDataSetQuery(string sqlQuery, SqlParameter[] param, CONNECTION_MODULE module)
            {
                return GetDataSet(sqlQuery, param, CommandType.Text, GetConnection(module));
            }

        /// <summary>
        /// /// This method retrieves a DataSet based on the provided stored procedure name and connection module.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="param"></param>
        /// <returns></returns>

        public static DataTable GetDataTableSP(string sqlSP, SqlParameter[] param)
            {
                return GetDataTable(sqlSP, param, CommandType.StoredProcedure, GetConnection(CONNECTION_MODULE.REGULAR));
            }

        /// <summary>
        /// /// This method retrieves a DataTable based on the provided SQL query and parameters.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable GetDataTableQuery(string sqlQuery, SqlParameter[] param)
            {
                return GetDataTable(sqlQuery, param, CommandType.Text, GetConnection(CONNECTION_MODULE.REGULAR));
            }

        /// <summary>
        /// /// This method retrieves a DataTable based on the provided stored procedure name and connection module.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <returns></returns>
        public static DataTable GetDataTableSP(string sqlSP)
            {
                return GetDataTable(sqlSP, null, CommandType.StoredProcedure, GetConnection(CONNECTION_MODULE.REGULAR));
            }

        /// <summary>
        /// /// This method retrieves a DataTable based on the provided SQL query and connection module.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public static DataTable GetDataTableQuery(string sqlQuery)
            {
                return GetDataTable(sqlQuery, null, CommandType.Text, GetConnection(CONNECTION_MODULE.REGULAR));
            }

        /// <summary>
        /// /// This method retrieves a DataTable based on the provided stored procedure name, parameters, command type, and connection module.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="param"></param>
        /// <param name="module"></param>
        /// <returns></returns>

        public static DataTable GetDataTableSP(string sqlSP, SqlParameter[] param, CONNECTION_MODULE module)
            {
                return GetDataTable(sqlSP, param, CommandType.StoredProcedure, GetConnection(module));
            }


        /// <summary>
        /// /// This method retrieves a DataTable based on the provided SQL query, parameters, command type, and connection module.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static DataTable GetDataTableQuery(string sqlQuery, SqlParameter[] param, CONNECTION_MODULE module)
            {
                return GetDataTable(sqlQuery, param, CommandType.Text, GetConnection(module));
            }


        /// <summary>
        /// /// This method retrieves a DataTable based on the provided stored procedure name and connection module.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static DataTable GetDataTableSP(string sqlSP, CONNECTION_MODULE module)
            {
                return GetDataTable(sqlSP, null, CommandType.StoredProcedure, GetConnection(module));
            }

        /// <summary>
        /// /// This method retrieves a DataTable based on the provided SQL query and connection module.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static DataTable GetDataTableQuery(string sqlQuery, CONNECTION_MODULE module)
            {
                return GetDataTable(sqlQuery, null, CommandType.Text, GetConnection(module));
            }

        /* Execute Reader*/
        /// <summary>
        /// /// This method retrieves a SqlDataReader based on the provided SQL query or stored procedure, parameters, command type, and SQL connection.
        /// </summary>
        /// <param name="queryOrSP"></param>
        /// <param name="param"></param>
        /// <param name="comandType"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        private static SqlDataReader GetDataReader(string queryOrSP, SqlParameter[] param, CommandType comandType, SqlConnection sqlConn)
            {
                SqlCommand sqlCmd = new SqlCommand(queryOrSP, sqlConn);
                sqlCmd.CommandType = comandType;
                if (param != null)
                    sqlCmd.Parameters.AddRange(param);
                sqlCmd.CommandTimeout = sqlConn.ConnectionTimeout;
                sqlCmd.Connection.Open();
                SqlDataReader dataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                sqlCmd.Parameters.Clear();
                return dataReader;
            }
        /// <summary>
        /// /// This method retrieves a SqlDataReader based on the provided SQL stored procedure and parameters.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReaderSP(string sqlSP, SqlParameter[] param)
            {
                return GetDataReader(sqlSP, param, CommandType.StoredProcedure, GetConnection(CONNECTION_MODULE.REGULAR));
            }


        /// <summary>
        /// /// This method retrieves a SqlDataReader based on the provided SQL query and parameters.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReaderQuery(string sqlQuery, SqlParameter[] param)
            {
                return GetDataReader(sqlQuery, param, CommandType.Text, GetConnection(CONNECTION_MODULE.REGULAR));
            }


        /// <summary>
        /// /// This method retrieves a SqlDataReader based on the provided SQL stored procedure name and connection module.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReaderSP(string sqlSP)
            {
                return GetDataReader(sqlSP, null, CommandType.StoredProcedure, GetConnection(CONNECTION_MODULE.REGULAR));
            }


        /// <summary>
        /// /// This method retrieves a SqlDataReader based on the provided SQL query and connection module.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReaderQuery(string sqlQuery)
            {
                return GetDataReader(sqlQuery, null, CommandType.Text, GetConnection(CONNECTION_MODULE.REGULAR));
            }


        /// <summary>
        /// /// This method retrieves a SqlDataReader based on the provided SQL stored procedure name, parameters, command type, and connection module.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="param"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReaderSP(string sqlSP, SqlParameter[] param, CONNECTION_MODULE module)
            {
                return GetDataReader(sqlSP, param, CommandType.StoredProcedure, GetConnection(module));
            }


        /// <summary>
        /// /// This method retrieves a SqlDataReader based on the provided SQL query, parameters, command type, and connection module.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReaderQuery(string sqlQuery, SqlParameter[] param, CONNECTION_MODULE module)
            {
                return GetDataReader(sqlQuery, param, CommandType.Text, GetConnection(module));
            }


        /// <summary>
        /// /// This method retrieves a SqlDataReader based on the provided SQL stored procedure name and connection module.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReaderSP(string sqlSP, CONNECTION_MODULE module)
            {
                return GetDataReader(sqlSP, null, CommandType.StoredProcedure, GetConnection(module));
            }


        /// <summary>
        /// /// This method retrieves a SqlDataReader based on the provided SQL query and connection module.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReaderQuery(string sqlQuery, CONNECTION_MODULE module)
            {
                return GetDataReader(sqlQuery, null, CommandType.Text, GetConnection(module));
            }


        /// <summary>
        /// /// This method executes a non-query SQL command using the provided SQL query or stored procedure, parameters, command type, and SQL connection, returning the number of rows affected.
        /// </summary>
        /// <param name="queryOrSP"></param>
        /// <param name="param"></param>
        /// <param name="comandType"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        private static int ExecuteNonQuery(string queryOrSP, SqlParameter[] param, CommandType comandType, SqlConnection sqlConn)
            {
                int rowAffected = 0;
                try
                {
                    using (SqlCommand sqlCmd = new SqlCommand(queryOrSP, sqlConn))
                    {
                        sqlCmd.CommandType = comandType;
                        if (param != null)
                            sqlCmd.Parameters.AddRange(param);
                        sqlCmd.Connection.Open();
                        sqlCmd.CommandTimeout = 500;
                        rowAffected = sqlCmd.ExecuteNonQuery();
                        sqlCmd.Connection.Close();
                        sqlCmd.Parameters.Clear();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return rowAffected;
            }
        /// <summary>
        /// /// This method executes a non-query SQL command using the provided SQL stored procedure and parameters, returning the number of rows affected.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteNonQuerySP(string sqlSP, SqlParameter[] param)
            {
                return ExecuteNonQuery(sqlSP, param, CommandType.StoredProcedure, GetConnection(CONNECTION_MODULE.REGULAR));
            }
        /// <summary>
        /// /// This method executes a non-query SQL command using the provided SQL query, parameters, command type, and SQL connection, returning the number of rows affected.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryQuery(string sqlQuery, SqlParameter[] param)
            {
                return ExecuteNonQuery(sqlQuery, param, CommandType.Text, GetConnection(CONNECTION_MODULE.REGULAR));
            }
        /// <summary>
        /// /// This method executes a non-query SQL command using the provided SQL stored procedure, parameters, command type, and SQL connection, returning the number of rows affected.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="param"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static int ExecuteNonQuerySP(string sqlSP, SqlParameter[] param, CONNECTION_MODULE module)
            {
                return ExecuteNonQuery(sqlSP, param, CommandType.StoredProcedure, GetConnection(module));
            }
        /// <summary>
        /// /// This method executes a non-query SQL command using the provided SQL query or stored procedure, parameters, command type, and SQL connection, returning the number of rows affected.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryQuery(string sqlQuery, SqlParameter[] param, CONNECTION_MODULE module)
            {
                return ExecuteNonQuery(sqlQuery, param, CommandType.Text, GetConnection(module));
            }
        /// <summary>
        /// /// This method executes a scalar query using the provided SQL query or stored procedure, parameters, command type, and SQL connection, returning the result as an object.
        /// </summary>
        /// <param name="queryOrSP"></param>
        /// <param name="param"></param>
        /// <param name="comandType"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        private static object ExecuteScalar(string queryOrSP, SqlParameter[] param, CommandType comandType, SqlConnection sqlConn)
            {
                object data = new object();
                try
                {
                    using (SqlCommand sqlCmd = new SqlCommand(queryOrSP, sqlConn))
                    {
                        sqlCmd.CommandType = comandType;
                        if (param != null)
                            sqlCmd.Parameters.AddRange(param);
                        sqlCmd.Connection.Open();
                        data = sqlCmd.ExecuteScalar();
                        sqlCmd.Connection.Close();
                        sqlCmd.Parameters.Clear();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return data;
            }
        /// <summary>
        /// /// This method executes a scalar query using the provided SQL stored procedure and parameters, returning the result as an object.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalarSP(string sqlSP, SqlParameter[] param)
            {
                return ExecuteScalar(sqlSP, param, CommandType.StoredProcedure, GetConnection(CONNECTION_MODULE.REGULAR));
            }
        /// <summary>
        /// /// This method executes a scalar query using the provided SQL query and parameters, returning the result as an object.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalarQuery(string sqlQuery, SqlParameter[] param)
            {
                return ExecuteScalar(sqlQuery, param, CommandType.Text, GetConnection(CONNECTION_MODULE.REGULAR));
            }
        /// <summary>
        /// /// This method executes a scalar query using a stored procedure and returns the result as an object.
        /// </summary>
        /// <param name="sqlSP"></param>
        /// <param name="param"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static object ExecuteScalarSP(string sqlSP, SqlParameter[] param, CONNECTION_MODULE module)
            {
                return ExecuteScalar(sqlSP, param, CommandType.StoredProcedure, GetConnection(module));
            }
        /// <summary>
        /// /// This method executes a scalar query and returns the result as an object.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="param"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static object ExecuteScalarQuery(string sqlQuery, SqlParameter[] param, CONNECTION_MODULE module)
            {
                return ExecuteScalar(sqlQuery, param, CommandType.Text, GetConnection(module));
            }
        }

}