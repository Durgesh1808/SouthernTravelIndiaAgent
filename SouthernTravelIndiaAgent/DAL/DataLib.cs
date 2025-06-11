using SouthernTravelIndiaAgent.BAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace SouthernTravelIndiaAgent.DAL
{

    /// <summary>
    /// // DataLib class provides methods for database operations, including executing stored procedures, retrieving data, and managing connections.
    /// </summary>

    public class DataLib
    {

        // Static lists to hold seat numbers and bus numbers
        public static System.Collections.Generic.List<int> vacentSeatNumber = new System.Collections.Generic.List<int>();
        public static System.Collections.Generic.List<int> occupiedSeatNumber = new System.Collections.Generic.List<int>();
        public static System.Collections.Generic.List<string> bus_vac_Number = new System.Collections.Generic.List<string>();

        /// <summary>
        /// // Enum for different types of database connections.
        /// </summary>
        public enum Connection
        {
            ConnectionString
        }

        /// <summary>
        /// /// Retrieves the connection string from the application settings.
        /// </summary>
        /// <returns></returns>
        public static string getConnectionString()
        {
            return ConfigurationSettings.AppSettings["southernconn"];
        }


        /// <summary>
        /// /// Cleans a given string by removing potentially harmful characters and HTML tags.
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string funClear(string strText)
        {
            if (strText == null || strText.Length == 0) return "";
            return replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(strText, "<", ""), ">", ""), "--", ""), "'", ""), ";", ""), "+", ""), "\"", ""), "/", ""), "&quot;", ""), "&lt", ""), "&gt", ""), "&#40", ""), "&#41", ""), "&#35", ""), "&#38", ""), "&apos;", ""), "\u0027", ""), "(", ""), ")", "");
        }

        /// <summary>
        /// /// Replaces occurrences of a specified substring within a string with another substring.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="findwhat"></param>
        /// <param name="replWith"></param>
        /// <returns></returns>
        private static string replace(string text, string findwhat, string replWith)
        {
            return text.Replace(findwhat, replWith);
        }


        /// <summary>
        /// /// Executes a stored procedure with the provided parameters and returns an integer result.
        /// </summary>
        /// <param name="strSP"></param>
        /// <param name="arrSPParam"></param>
        /// <returns></returns>
        public static int InsStoredProcData(string strSP, SqlParameter[] arrSPParam)
        {
            int strTour = 0;
            string paramOut = "";

            SqlConnection pvConnection = new SqlConnection();
            SqlCommand localSqlCommand = new SqlCommand();
            localSqlCommand.Connection = GetConnection(pvConnection);
            localSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            localSqlCommand.CommandText = strSP;
            try
            {

                if (arrSPParam != null)
                {
                    strTour = 0;
                    foreach (SqlParameter param in arrSPParam)
                    {
                        localSqlCommand.Parameters.Add(param);
                        if (param.Direction == ParameterDirection.Output)
                        {
                            paramOut = param.ParameterName;
                            strTour = 1;
                        }
                    }
                }
                localSqlCommand.ExecuteNonQuery();
                localSqlCommand.Dispose();
                if (strTour == 1)
                {
                    strTour = Convert.ToInt32(localSqlCommand.Parameters[paramOut].Value);
                }
            }
            catch (Exception ex)
            {
                strTour = -1;
            }
            finally
            {
                CloseConnection(pvConnection);
                if (pvConnection != null)
                {
                    pvConnection = null;
                }
                if (localSqlCommand != null)
                {
                    localSqlCommand.Dispose();
                    localSqlCommand = null;
                }

            }
            return strTour;
        }


        /// <summary>
        /// /// Gets a SQL connection using the connection string defined in the application settings.
        /// </summary>
        /// <param name="conInvitesConnection"></param>
        /// <returns></returns>
        public static SqlConnection GetConnection(SqlConnection conInvitesConnection)
        {

            //Dim conInvitesConnection As New OracleConnection
            conInvitesConnection = new SqlConnection(getConnectionString());
            //conInvitesConnection.ConnectionString = ConString;
            conInvitesConnection.Open();
            return conInvitesConnection;
        }


        /// <summary>
        /// /// Closes the SQL connection and disposes of it.
        /// </summary>
        /// <param name="conInvitesConnection"></param>
        /// <returns></returns>
        public static SqlConnection CloseConnection(SqlConnection conInvitesConnection)
        {
            conInvitesConnection.Close();
            conInvitesConnection.Dispose();
            return conInvitesConnection;
        }


        /// <summary>
        /// /// Generates a unique code based on the provided Enquery type.
        /// </summary>
        /// <param name="Enquerytype"></param>
        /// <returns></returns>
        public static string Code(string Enquerytype)
        {
            DataTable dtcode = null;
            dtcode = clsBLL.Getcode(Enquerytype);
            if (dtcode != null && dtcode.Rows.Count > 0)
            {
                int code = Convert.ToInt32(dtcode.Rows[0][0]);
                return Convert.ToString(Enquerytype + code.ToString("000000"));
            }
            else
                return Convert.ToString(Enquerytype + "000001");
        }

        /// <summary>
        /// /// Sends an HTTP SOAP request with the provided XML string to the specified URI and returns the response as a string.
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="uri"></param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static string HttpSOAPRequest(String xmlString, string uri, string proxy)
        {
            string resultStr = "";
            WebRequest req = null;
            WebResponse rsp = null;
            Stream stm;
            StreamReader r;
            StreamWriter writer;
            XmlDocument doc;
            try
            {

                doc = new XmlDocument();
                doc.LoadXml(xmlString);
                req = WebRequest.Create(uri);
                // req.Timeout = 30000;
                req.Method = "POST";        // Post method
                req.ContentType = "text/xml";     // content type
                                                  // Wrap the request stream with a text-based writer
                writer = new StreamWriter(req.GetRequestStream());
                // Write the xml text into the stream
                writer.WriteLine(xmlString);
                writer.Close();
                // Send the data to the webserver
                rsp = req.GetResponse();
                stm = rsp.GetResponseStream();
                r = new StreamReader(stm);
                // process SOAP return doc here
                resultStr = r.ReadToEnd();
            }
            catch (Exception ex)
            {
                resultStr = ex.ToString();
            }
            finally
            {
                req = null;
                rsp = null;
                stm = null;
                r = null;
                writer = null;
                doc = null;
            }
            return resultStr;
        }


        /// <summary>
        /// /// Retrieves a string value from the database based on the provided SQL query and connection type.
        /// </summary>
        /// <param name="connType"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string GetStringData(Connection connType, string sql)
        {
            return GetStringData(GetConnectionString(connType), sql);
        }


        /// <summary>
        /// /// Executes a SQL query and returns a single string value.
        /// </summary>
        /// <param name="connectStr"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static string GetStringData(string connectStr, string sql)
        {
            string retValue = "";
            using (SqlConnection localSqlConnection = new SqlConnection(connectStr))
            {
                using (SqlCommand localSqlCommand = new SqlCommand(sql, localSqlConnection))
                {
                    localSqlCommand.CommandTimeout = 1000;
                    localSqlCommand.Connection.Open();
                    retValue = Convert.ToString(localSqlCommand.ExecuteScalar());
                }
            }
            return retValue;
        }

        /// <summary>
        /// /// Retrieves the connection string based on the specified connection type.
        /// </summary>
        /// <param name="connType"></param>
        /// <returns></returns>
        public static string GetConnectionString(Connection connType)
        {
            string retValue = "";
            AppSettingsReader appReader = new AppSettingsReader();
            switch (connType)
            {
                case Connection.ConnectionString:
                    {
                        //				retValue = appReader.GetValue("Main.ConnectionStringZD",typeof(string)).ToString();
                        retValue = appReader.GetValue("southernconn", typeof(string)).ToString();
                        break;
                    }
            }
            return retValue;
        }


        /// <summary>
        /// /// Retrieves a DataTable based on the provided SQL query and connection type.
        /// </summary>
        /// <param name="connType"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(Connection connType, string sql)
        {
            return GetDataTable(GetConnectionString(connType), sql);
        }


        /// <summary>
        /// /// Executes a SQL query and returns the result as a DataTable.
        /// </summary>
        /// <param name="connectStr"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static DataTable GetDataTable(string connectStr, string sql)
        {
            DataTable theTable = new DataTable();
            using (SqlConnection localSqlConnection = new SqlConnection(connectStr))
            {
                using (SqlDataAdapter localSqlAdapter = new SqlDataAdapter(sql, localSqlConnection))
                {
                    localSqlAdapter.Fill(theTable);
                }
            }
            return theTable;
        }


        /// <summary>
        /// /// Generates a unique ticket code for SPL (Southern Passenger Line) based on the last ticket number in the database.
        /// </summary>
        /// <returns></returns>
        public static string SPLticketCode()
        {
            #region Optimize Code
            /* string str = "select top 1 replace( ticketno,'spl','') as ticketno from spl_tourenquiry(nolock) where status='S' order by [id] desc";
            string rr = DataLib.GetStringData(DataLib.Connection.ConnectionString, str);*/
            #endregion
            // rr = "000000";
            ClsAdo clsObj = null;
            try
            {
                clsObj = new ClsAdo();
                string rr = clsObj.fnSPLticketCode();
                int tt = 0;

                if (rr.Trim().Length > 0)
                {
                    tt = Convert.ToInt32(rr);
                }
                try
                {
                    tt = Convert.ToInt32(rr);
                }
                catch (Exception ex)
                {
                }
                finally
                {

                    tt++;
                }

                return Convert.ToString("SPL" + tt.ToString("000000"));
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
            }
        }


        /// <summary>
        /// /// Generates a unique PNR (Passenger Name Record) number for SPL tours based on the last PNR number in the database.
        /// </summary>
        /// <returns></returns>
        public static string SPLpnr()
        {
            #region Optimize Code
            /*string str = "select top 1 Pnrno from spl_tourenquiry(nolock) order by id desc";
            string rr = DataLib.GetStringData(DataLib.Connection.ConnectionString, str);*/
            #endregion
            ClsAdo clsObj = null;
            try
            {
                clsObj = new ClsAdo();
                string rr = clsObj.fnGetSPLpnr();
                int tt = 100;

                if (rr.Trim().Length > 0)
                {
                    rr = rr.Substring(0, 3);
                }
                try
                {
                    tt = Convert.ToInt32(rr);
                }
                catch (Exception ex)
                {
                }
                finally
                {

                    if (tt == 999)
                        tt = 100;
                    else
                        tt++;
                }

                return Convert.ToString(tt) + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Year.ToString("00") + System.DateTime.Now.Second.ToString("00") + System.DateTime.Now.Millisecond.ToString("000");
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
            }
        }


        /// <summary>
        /// /// Executes a SQL command and returns the number of rows affected or the new ID value if requested.
        /// </summary>
        /// <param name="connType"></param>
        /// <param name="sql"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public static int ExecuteSQL1(Connection connType, string sql, bool ret)
        {
            return ExecuteSQL(GetConnectionString(connType), sql, false);
        }
        private static int ExecuteSQL(string connectStr, string sql, bool getIDVal)
        {
            int newID = 0;

            using (SqlConnection localSqlConnection = new SqlConnection(connectStr))
            {
                using (SqlCommand localSqlCommand = new SqlCommand(sql, localSqlConnection))
                {
                    localSqlCommand.CommandTimeout = 15;
                    localSqlCommand.Connection.Open();
                    if (getIDVal == true)
                    {
                        if (localSqlCommand.CommandText.IndexOf("@@Identity") > 0)
                        {
                            localSqlCommand.CommandText = "SET NOCOUNT ON " + localSqlCommand.CommandText;
                        }
                        else
                        {
                            localSqlCommand.CommandText = "SET NOCOUNT ON " + localSqlCommand.CommandText + "SELECT @@IDENTITY";
                        }
                        using (SqlDataReader localSqlReader = localSqlCommand.ExecuteReader())
                        {
                            if (localSqlReader.Read() == true)
                            {
                                newID = Convert.ToInt32(localSqlReader[0].ToString());
                            }
                        }
                    }
                    else
                    {
                        newID = localSqlCommand.ExecuteNonQuery();
                    }
                }
            }
            return newID;
        }


        /// <summary>
        /// /// Retrieves the service tax value based on the provided tax string.
        /// </summary>
        /// <param name="strTax"></param>
        /// <returns></returns>
        public static string GetserviceTax(string strTax)
        {
            try
            {

                return new ClsAdo().fnGetTAXValue(strTax);
            }
            catch
            {
                return null; // or handle/log exception as needed
            }
        }


        /// <summary>
        /// /// Executes a stored procedure with the provided parameters and returns a DataSet.
        /// </summary>
        /// <param name="connType"></param>
        /// <param name="strSP"></param>
        /// <param name="arrSPParam"></param>
        /// <returns></returns>
        public static DataSet GetStoredProcData(Connection connType, string strSP, SqlParameter[] arrSPParam)
        {
            return GetStoredProcData(GetConnectionString(connType), strSP, arrSPParam);
        }
        private static DataSet GetStoredProcData(string connectStr, string strSP, SqlParameter[] arrSPParam)
        {
            DataSet ds = new DataSet();
            using (SqlConnection localSqlConnection = new SqlConnection(connectStr))
            {
                using (SqlCommand localSqlCommand = localSqlConnection.CreateCommand())
                {
                    localSqlCommand.Connection = localSqlConnection;
                    localSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    localSqlCommand.CommandText = strSP;
                    localSqlCommand.CommandTimeout = 1000;
                    if (arrSPParam != null)
                    {
                        foreach (SqlParameter param in arrSPParam)
                        {
                            localSqlCommand.Parameters.Add(param);
                        }
                    }
                    using (SqlDataAdapter localSqlAdapter = new SqlDataAdapter(localSqlCommand))
                    {
                        localSqlAdapter.SelectCommand = localSqlCommand;
                        localSqlAdapter.Fill(ds);
                        localSqlAdapter.SelectCommand = null;
                    }
                }
            }
            return ds;
        }


        /// <summary>
        /// /// Generates a unique PNR (Passenger Name Record) number based on the last order ID in the database.
        /// </summary>
        /// <returns></returns>
        public static string pnr()
        {
            #region Optimize Code
            /*string str = "select top 1 orderid from onlinetoursbooking order by rowid desc";
            string rr = DataLib.GetStringData(DataLib.Connection.ConnectionString, str);*/
            #endregion
            ClsAdo clsObj = null;
            try
            {
                clsObj = new ClsAdo();
                string rr = clsObj.fnGetOrderID();
                rr = rr.Substring(0, 3);
                string ss = "";
                int tt = 100;
                try
                {
                    tt = Convert.ToInt32(rr);
                }
                catch (Exception ex)
                {
                    ss = "Y";
                }
                finally
                {
                    if (ss == "Y")
                        tt = 100;
                    else if (tt == 999)
                        tt = 100;
                    else
                        tt = tt;

                    tt = tt + 1;
                }
                //Random R = new Random();R.Next(100, 999).ToString()
                return Convert.ToString(tt) + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Year + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00") + System.DateTime.Now.Millisecond.ToString("000");
                //return Convert.ToString(tt);
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
            }
        }


        /// <summary>
        /// For Displaying the seating chart
        /// </summary>
        /// <param name="SeaterType"></param>
        /// <param name="tourserial"></param>
        /// <param name="busno"></param>
        /// <returns></returns>
        public static StringBuilder seatarr(string SeaterType, int tourserial, int busno)
        {
            #region Optimize Code
            /*string str;
            StringBuilder Chart = new StringBuilder();
            DataTable vacent = new DataTable();
            SqlParameter[] paramseat = new SqlParameter[1];
            paramseat[0] = new SqlParameter("@tourserial", tourserial);
            str = "Select * from SeatArrangement(nolock) where TourSerial=@tourserial";
            vacent = DataLib.GetDataTableWithparam(DataLib.Connection.ConnectionString, str, paramseat);
            SqlParameter[] paramseat1 = new SqlParameter[1];
            paramseat1[0] = new SqlParameter("@tourserial", tourserial);
            string seater = "select bustype from busallot where rowid=@tourserial";
            string bt = DataLib.GetStringDataWithParam(DataLib.Connection.ConnectionString, seater, paramseat1);
            string btype = bt.Substring(0, 2);
            int ss = Convert.ToInt32(btype);
            SeaterType = btype + " Seater";
            SqlParameter[] paramseat2 = new SqlParameter[1];
            paramseat2[0] = new SqlParameter("@SeaterType", SeaterType);
            string a1 = "select seatdesign from SeatingChartMaster where bustype=@SeaterType";
            string de = DataLib.GetStringDataWithParam(DataLib.Connection.ConnectionString, a1, paramseat2);*/
            #endregion

            StringBuilder Chart = new StringBuilder();

            ClsAdo clsObj = null;
            DataSet ldtRecord = null;
            DataTable vacent = null;
            vacentSeatNumber.Clear();   
            occupiedSeatNumber.Clear(); 
            try
            {
                clsObj = new ClsAdo();
                int? lTourSrNo = tourserial;
                ldtRecord = clsObj.fnGetSeatArrangementDetail(lTourSrNo);

                if (ldtRecord != null && ldtRecord.Tables.Count > 0)
                {
                    vacent = ldtRecord.Tables[0];
                    int pBType = Convert.ToInt32(ldtRecord.Tables[1].Rows[0]["bustype"]);

                    string pSeatDes = ldtRecord.Tables[2].Rows[0]["seatdesign"].ToString();

                    string[] pSeatDesign;
                    pSeatDesign = pSeatDes.Split(',');


                    Chart.Append("<div class=\"selectiondiv\" > <div class=\"frontsection\"><div class=\"seat-d\">");

                    Chart.Append("<img src=\"Assets/images/seat-d.png\"></div>  ");
                    if (pBType < 14)
                    {
                        Chart.Append("</div> <div class=\"backsection\">");
                    }
                    else
                    {
                        Chart.Append("<div class=\"seat-c\"><img src=\"Assets/images/seat-c.png\"></div>");
                        Chart.Append("<div class=\"bus-entry\"><img src=\"Assets/images/bus-entry.jpg\"></div></div> <div class=\"backsection\">");
                        Chart.Append("<div class=\"bus-midspace\">&nbsp;</div>");
                    }
                    Chart.Append("<table id=chart" + busno + "2 width='100%' border='0' cellspacing='0' cellpadding='0'>");
                    int pCount = 0;



                    for (int i = 0; i < pSeatDesign.Length; i++)
                    {
                        string pColm = pSeatDesign[i];
                        if (i == 0)
                        {
                            Chart.Append("<tr>");
                        }

                        if (pColm == "b")
                        {
                            Chart.Append("<td width='70px' bgColor='#fff' ></td>");
                        }
                        else if (pColm == "z")
                        {
                            Chart.Append("<td width='70px' bgColor='#fff' colspan=\"3\" style='color:#ffffff;font-weight:normal'><h1>" + "Seater Type - " + SeaterType + " Seater" + "</h1></td>");
                        }
                        else if ((pColm != "b") && (pColm != "n"))
                        {
                            pCount = pCount + 1;
                            if (pCount <= pBType)
                            {
                                if (vacent.Rows[0]["s" + pColm] == DBNull.Value)
                                {
                                    Chart.Append("<td ID='td" + busno + "" + pColm + "' width='70px' align='center' height='50px'> <input type='checkbox' ID='" + busno + "chk" + pColm + "' value='" + busno + "" + pColm + "' style='display:none;' /><div class='TB_avbl' style='cursor:pointer;' onclick=\"javascript:checkUnheck('" + busno + "chk" + pColm + "',this);\"><span>" + pColm + "</span></div></td>");
                                    vacentSeatNumber.Add(int.Parse(pColm)); 
                                    bus_vac_Number.Add(busno + "_" + pColm);
                                }
                                else
                                {
                                    Chart.Append("<td align='center' width='70px' height='50px' ><div class='TB_Bkd'><span>" + pColm + "</span></div></td>");
                                    occupiedSeatNumber.Add(int.Parse(pColm)); 
                                }
                            }
                        }
                        if (pColm == "n")
                        {
                            Chart.Append("</tr>");
                            Chart.Append("<tr>");
                        }
                    }
                    Chart.Append("</tr>");
                    Chart.Append("</table></div></div>");
                 
                }
                return Chart;
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (vacent != null)
                {
                    vacent.Dispose();
                    vacent = null;
                }
                if (ldtRecord != null)
                {
                    ldtRecord.Dispose();
                    ldtRecord = null;
                }
            }
        }

        /// <summary>
        /// /// Generates a unique PNR (Passenger Name Record) number for car bookings based on the last cab booking ID in the database.
        /// </summary>
        /// <returns></returns>
        public static string CABpnr()
        {
            #region Optimize Code
            /*string str = "select top 1 cabid from tbl_CarBookings_Log(nolock) order by id desc";
            string rr = DataLib.GetStringData(DataLib.Connection.ConnectionString, str);*/
            #endregion
            ClsAdo clsObj = null;
            try
            {
                clsObj = new ClsAdo();
                string rr = clsObj.fnGetCABpnr();
                int tt = 100;

                if (rr.Trim().Length > 0)
                {
                    rr = rr.Substring(0, 3);
                }
                try
                {
                    tt = Convert.ToInt32(rr);
                }
                catch (Exception ex)
                {
                }
                finally
                {

                    if (tt == 999)
                        tt = 100;
                    else
                        tt++;
                }

                return Convert.ToString(tt) + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Year + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00") + System.DateTime.Now.Millisecond.ToString("000");
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
            }
        }
    }
}