using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mail;
using System.Web.Services.Description;
using System.Web.UI;

namespace SouthernTravelIndiaAgent.BAL
{
    /// <summary>
    /// // This class contains common methods and enums used across the application.
    /// </summary>
    public enum MailRequestFrom
    {
        Agent,
        Website,
        Admin,
        Branch
    }
    /// <summary>
    /// // This class is used to encapsulate a list of data items along with a request status.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataListResponse<T>
    {
        public List<T> ResultList;

        public RequestStatus Status;
    }
    /// <summary>
    /// /// This enum defines various exception codes used in the application.
    /// </summary>
    public enum pbException
    {
        [Description("Success")]
        SUCCESS = 1,
        [Description("Data not found.")]
        ERR_DATANOT_FOUND = 1001,
        [Description("Data Not Save.")]
        ERR_DATANOT_SAVE = 1002,
        [Description("Record Already Exist.")]
        ERR_RECORD_EXIST = 1003,
        [Description("Sorry For your inconvience.The Error has been Logged and will be rectified soon.")]
        ERR_CATCH_BLOCK = 1004,
        [Description("Sorry For your inconvience for saving data.")]
        ERR_SAVE_CATCH_BLOCK = 1005,
        [Description("Geocode value is not available.")]
        ERR_GEOCODE_NOT_FOUND = 1006,
        [Description("Season with same name already exist.")]
        ERR_SEASON_EXIST = 1007,
        [Description("Date range already exist.")]
        ERR_SEASON_DATE_EXIST = 1008,
        [Description("Week days already exists within same date range in some other season.")]
        ERR_WEEK_DAYS_ALREADY_EXIST = 1009,
        [Description("Sorry For your inconvience for Delete data.")]
        ERR_DELETE_CATCH_BLOCK = 1010,
        [Description("Approval levelName already exist.")]
        ERR_APP_LEVEL_EXS = 1011,
        [Description("Priority level already registered.")]
        ERR_PR_LEVEL_EXS = 1012,
        [Description("Date collapsing with previous date range.")]
        ERR_DATE_COLLAPSE = 1013
    }

    /// <summary>
    /// /// This struct is used to encapsulate the status of a request, including whether it was successful, an error number, and a description of the error.
    /// </summary>
    public struct RequestStatus
    {
        public bool Status;

        public int ErrNo;

        public string ErrDesc;
    }

    /// <summary>
    /// /// This enum defines the status of an email sent from the application.
    /// </summary>
    public enum MailStatus
    {
        Sent,
        Failed,
        Queued,
        Bounced
    }


    /// <summary>
    /// /// This enum defines the types of tours available in the application.
    /// </summary>
    public enum TOURTYPE
    {
        [Description("AL")]
        ALL_TOUR = -1,
        [Description("FT")]
        FIXED_TOUR = 1,
        [Description("ST")]
        SPECIAL_TOUR = 2,
        [Description("INT")]
        INTERNATIONAL_TOUR = 3,
        [Description("GRP")]
        GROUP_TOUR = 4,
        [Description("ADH")]
        ADHOC_TOUR = 5,
        [Description("CAB")]
        CAB_TOUR = 6,
        [Description("LTCFT")]
        LTC_FIXED_TOUR = 7,
        [Description("FTBKNG")]
        FIXED_TOUR_BOOKING = 8,
        [Description("LFTBKNG")]
        LTC_FIXED_TOUR_BOOKING = 9,
        [Description("STBKNG")]
        SPECIAL_TOUR_BOOKING = 10,
        [Description("SPAX")]
        SPECIAL_PAX_TOUR = 11,
        [Description("SPBKNG")]
        SPECIAL_PAX_TOUR_BOOKING = 12,
        [Description("HACB")]
        HAC_TOUR_BOOKING = 13,
        [Description("BAL")]
        TKT_BAL_CLEAR = 14,
        [Description("AAFD")]
        AGENT_ADD_FUND = 15,
        [Description("FRN")]
        Flight_Booking = 16,
        [Description("TRN")]
        Rail_Booking = 17,
        [Description("MBK")]
        MBK_Booking = 19,
    }

    /// <summary>
    /// /// This class contains common methods used across the application, such as date format conversion and alert messages.
    /// </summary>
    public class ClsCommon
    {

        /// <summary>
        /// Convert Date Formate from MM/dd/yyyy to dd/MM/yyyy
        /// </summary>
        /// <param name="pDate"></param>
        /// <returns> Date Formate of dd/MM/yyyy  </returns>
        public static string mmddyy2ddmmyy(string pDate)
        {
            if (pDate != null || pDate != "")
            {
                string[] DateArr3 = new string[3];
                char[] splitter1 = { '/' };
                DateArr3 = pDate.Split(splitter1);
                if (DateArr3.Length > 2)
                    return DateArr3[1] + "/" + DateArr3[0] + "/" + DateArr3[2].Substring(0, 4);
                else
                    return "";
            }
            else
                return "";
        }


        /// <summary>
        /// /// Convert Date Formate from dd/MM/yyyy to MM/dd/yyyy
        /// </summary>
        /// <param name="pDate"></param>
        /// <returns></returns>
        public static string ddmmyy2mmddyy(string pDate)
        {
            pDate = pDate.Replace("-", "/");
            if (pDate != null || pDate != "")
            {
                string[] DateArr3 = new string[3];
                char[] splitter1 = { '/' };
                DateArr3 = pDate.Split(splitter1);
                if (DateArr3.Length > 2)
                    return DateArr3[1] + "/" + DateArr3[0] + "/" + DateArr3[2].Substring(0, 4);
                else
                    return "";
            }
            else
                return "";
        }


        /// <summary>
        /// /// Show alert message on the current page
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="message"></param>
        public static void ShowAlert(System.Web.UI.Page currentPage, string message)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("');");
            currentPage.ClientScript.RegisterStartupScript(typeof(ClsCommon), "showalert", sb.ToString(), true);
        }


        /// <summary>
        /// /// Show alert message on the current page using ScriptManager
        /// </summary>
        /// <param name="Msg"></param>
        public static void ShowAlert(string msg)
        {
            string title = "Oops...";
            string icon = "warning"; // Must be lowercase
            string buttonColor = "#f2572b";

            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                // Sanitize inputs
                title = title.Replace("'", "\\'");
                msg = msg.Replace("'", "\\'");

                string script = $@"
                    Swal.fire({{
                        icon: '{icon}',
                        title: '{title}',
                        text: '{msg}',
                        confirmButtonColor: '{buttonColor}'
                    }});";

                ScriptManager.RegisterStartupScript(page, page.GetType(), "swal_alert", script, true);
            }
        }
        public static void SuccessShowAlert(string msg)
        {
            string title = "Success!";
            string icon = "success"; // Must be lowercase
            string buttonColor = "#f2572b";

            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                // Sanitize inputs
                title = title.Replace("'", "\\'");
                msg = msg.Replace("'", "\\'");

                string script = $@"
                        Swal.fire({{
                            icon: '{icon}',
                            title: '{title}',
                            text: '{msg}',
                            confirmButtonColor: '{buttonColor}'
                        }});";

                ScriptManager.RegisterStartupScript(page, page.GetType(), "swal_alert", script, true);
            }
        }



        /// <summary>
        /// /// Send an email with the specified parameters.
        /// </summary>
        /// <param name="pTO"></param>
        /// <param name="pBCC"></param>
        /// <param name="pCC"></param>
        /// <param name="pFrom"></param>
        /// <param name="pSubject"></param>
        /// <param name="pBody"></param>
        /// <param name="pFromName"></param>
        /// <param name="pFilePath"></param>
        public static void sendmail(string pTO, string pBCC, string pCC, string pFrom, string pSubject, string pBody, string pFromName, string pFilePath)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                if (pTO.ToLower().EndsWith("@temp.com"))
                {
                    pTO = pBCC;
                }

                string persistMail = ConfigurationManager.AppSettings["PersistMailORSmtpHost"]?.ToUpper();
                string authMail = ConfigurationManager.AppSettings["AuthMail"]?.ToUpper();

                if (persistMail == "TRUE")
                {
                    MailSend(pTO, pBCC, pCC, pFrom, pSubject, pBody, pFromName);
                }
                else if (authMail == "TRUE")
                {
                    AuthMail(pTO, pBCC, pCC, pFrom, pSubject, pBody, pFromName);
                }
                else
                {
                    using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                    {
                        mail.From = new MailAddress(pFrom, pFromName);
                        mail.To.Add(pTO);
                        if (!string.IsNullOrEmpty(pBCC))
                            mail.Bcc.Add(pBCC);
                        if (!string.IsNullOrEmpty(pCC))
                            mail.CC.Add(pCC);

                        mail.Subject = pSubject;
                        mail.Body = pBody;
                        mail.IsBodyHtml = true;

                        string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                        if (!string.IsNullOrEmpty(smtpHost))
                        {
                            using (SmtpClient smtp = new SmtpClient(smtpHost))
                            {
                                smtp.Send(mail);
                            }
                        }
                        else
                        {
                            throw new Exception("SMTP host not configured.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception properly (e.g., write to log file or email admin)
                // Example:
                System.Diagnostics.Debug.WriteLine("Email error: " + ex.ToString());
            }
        }


        /// <summary>
        /// /// Send an email with the specified parameters.
        /// </summary>
        /// <param name="pTO"></param>
        /// <param name="pBCC"></param>
        /// <param name="pCC"></param>
        /// <param name="pFrom"></param>
        /// <param name="pSubject"></param>
        /// <param name="pBody"></param>
        public static void SendMail(string pTO, string pBCC, string pCC, string pFrom, string pSubject, string pBody)
        {
            // Force TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                // If TO ends with @temp.com, override it with BCC
                if (!string.IsNullOrEmpty(pTO) && pTO.ToLower().EndsWith("@temp.com"))
                {
                    pTO = pBCC;
                }

                string persistMail = ConfigurationManager.AppSettings["PersistMailORSmtpHost"]?.ToUpper();
                string authMail = ConfigurationManager.AppSettings["AuthMail"]?.ToUpper();

                if (persistMail == "TRUE")
                {
                    MailSend(pTO, pBCC, pCC, pFrom, pSubject, pBody, "");
                }
                else if (authMail == "TRUE")
                {
                    AuthMail(pTO, pBCC, pCC, pFrom, pSubject, pBody, "");
                }
                else
                {
                    using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                    {
                        mail.From = new MailAddress(pFrom);
                        mail.To.Add(pTO);

                        if (!string.IsNullOrEmpty(pBCC))
                            mail.Bcc.Add(pBCC);

                        if (!string.IsNullOrEmpty(pCC))
                            mail.CC.Add(pCC);

                        mail.Subject = pSubject;
                        mail.Body = pBody;
                        mail.IsBodyHtml = true;

                        string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                        if (!string.IsNullOrEmpty(smtpHost))
                        {
                            using (SmtpClient smtp = new SmtpClient(smtpHost))
                            {
                                smtp.Send(mail);
                            }
                        }
                        else
                        {
                            throw new Exception("SMTP host not configured.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or show a meaningful message
                System.Diagnostics.Debug.WriteLine("SendMail Exception: " + ex.ToString());
            }
        }


        /// <summary>
        /// /// Send an email with the specified parameters.
        /// </summary>
        /// <param name="pTO"></param>
        /// <param name="pBCC"></param>
        /// <param name="pCC"></param>
        /// <param name="pFrom"></param>
        /// <param name="pSubject"></param>
        /// <param name="pBody"></param>
        /// <param name="pFromName"></param>
        /// /// <remarks>
        /// /// This method sends an email using the SMTP client configured in the application settings.
        /// /// It supports multiple recipients for TO, CC, and BCC fields, and allows HTML content in the body.
        /// /// </remarks>
        public static void sendmail(string pTO, string pBCC, string pCC, string pFrom, string pSubject, string pBody, string pFromName)
        {
            // Ensure TLS 1.2 is used
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailStatus statusEnum = MailStatus.Sent; // or MailStatus.Failed if caught in catch
            MailRequestFrom requestFromEnum = MailRequestFrom.Website;
            string errorMessage = "";
            try
            {
                if (pTO.ToLower().EndsWith("@temp.com"))
                {
                    pTO = pBCC;
                }

                if (ConfigurationManager.AppSettings["PersistMailORSmtpHost"]?.ToUpper() == "TRUE")
                {
                    MailSend(pTO, pBCC, pCC, pFrom, pSubject, pBody, pFromName);
                    //LogEmailToDB(0, pFrom, pTO, pCC, pBCC, pSubject, pBody, statusEnum.ToString(), errorMessage, "", "", requestFromEnum.ToString());

                    return;
                }

                if (ConfigurationManager.AppSettings["AuthMail"]?.ToUpper() == "TRUE")
                {
                    AuthMail(pTO, pBCC, pCC, pFrom, pSubject, pBody, pFromName);
                    //LogEmailToDB(0, pFrom, pTO, pCC, pBCC, pSubject, pBody, statusEnum.ToString(), errorMessage, "", "", requestFromEnum.ToString());

                    return;
                }

                using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                {
                    mail.From = new MailAddress(pFrom, pFromName);

                    // Multiple recipients (comma-separated)
                    foreach (var address in pTO.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                        mail.To.Add(address);

                    if (!string.IsNullOrEmpty(pCC))
                    {
                        foreach (var address in pCC.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                            mail.CC.Add(address);
                    }

                    if (!string.IsNullOrEmpty(pBCC))
                    {
                        foreach (var address in pBCC.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                            mail.Bcc.Add(address);
                    }

                    mail.Subject = pSubject;
                    mail.Body = pBody;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                        if (!string.IsNullOrEmpty(smtpHost))
                        {
                            smtp.Host = smtpHost;
                        }
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                statusEnum = MailStatus.Failed;
                errorMessage = ex.ToString();
                LogEmailToDB(0, pFrom, pTO, pCC, pBCC, pSubject, pBody, statusEnum.ToString(), errorMessage, "", "", requestFromEnum.ToString());

            }
          
        }


        /// <summary>
        /// /// Send an email with the specified parameters.
        /// </summary>
        /// <param name="pTO"></param>
        /// <param name="pBCC"></param>
        /// <param name="pCC"></param>
        /// <param name="pFrom"></param>
        /// <param name="pSubject"></param>
        /// <param name="pBody"></param>
        /// <param name="pFromName"></param>
        public static void MailSend(string pTO, string pBCC, string pCC, string pFrom, string pSubject, string pBody, string pFromName)
        {
            SendSmtpMail(pTO, pBCC, pCC, pFrom, pSubject, pBody, pFromName, null);
        }

        /// <summary>
        /// /// Send an authenticated email with the specified parameters.
        /// </summary>
        /// <param name="pTO"></param>
        /// <param name="pBCC"></param>
        /// <param name="pCC"></param>
        /// <param name="pFrom"></param>
        /// <param name="pSubject"></param>
        /// <param name="pBody"></param>
        /// <param name="pFromName"></param>
        /// <param name="pFileName"></param>
        public static void AuthMail(string pTO, string pBCC, string pCC, string pFrom, string pSubject, string pBody, string pFromName, string pFileName = null)
        {
            MailStatus statusEnum = MailStatus.Sent; // or MailStatus.Failed if caught in catch
            MailRequestFrom requestFromEnum = MailRequestFrom.Website;
            string errorMessage = "";
            try
            {
                string smtpHost = ConfigurationManager.AppSettings["AuthMailSmtpIP"];
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["AuthMailSmtpPort"] ?? "587");
                string smtpUser = ConfigurationManager.AppSettings[pFrom.ToLower() + "_UserName"];
                string smtpPass = ConfigurationManager.AppSettings[pFrom.ToLower() + "_Password"];

                SendSmtpMail(pTO, pBCC, pCC, smtpUser, pSubject, pBody, pFromName, pFileName, smtpHost, smtpPort, smtpUser, smtpPass, true);
            }
            catch (Exception ex)
            {
                statusEnum = MailStatus.Failed;
                errorMessage = ex.ToString();
                LogEmailToDB(0, pFrom, pTO, pCC, pBCC, pSubject, pBody, statusEnum.ToString(), errorMessage, "", "", requestFromEnum.ToString());
            }
            //finally
            //{
            //    LogEmailToDB(0, pFrom, pTO, pCC, pBCC, pSubject, pBody, statusEnum.ToString(), errorMessage, "", "", requestFromEnum.ToString());
            //}

        }

        /// <summary>
        /// // /// Send an SMTP email with the specified parameters.
        /// </summary>
        /// <param name="pTO"></param>
        /// <param name="pBCC"></param>
        /// <param name="pCC"></param>
        /// <param name="pFrom"></param>
        /// <param name="pSubject"></param>
        /// <param name="pBody"></param>
        /// <param name="pFromName"></param>
        /// <param name="pFileName"></param>
        /// <param name="smtpHost"></param>
        /// <param name="smtpPort"></param>
        /// <param name="smtpUser"></param>
        /// <param name="smtpPass"></param>
        /// <param name="enableSsl"></param>
        private static void SendSmtpMail(
 string pTO, string pBCC, string pCC, string pFrom, string pSubject, string pBody, string pFromName,
 string pFileName = null,
 string smtpHost = null,
 int smtpPort = 25,
 string smtpUser = null,
 string smtpPass = null,
 bool enableSsl = false)
        {
            System.Net.Mail.MailMessage mail = null;
            SmtpClient smtp = null;
            MailStatus statusEnum = MailStatus.Sent; // or MailStatus.Failed if caught in catch
            MailRequestFrom requestFromEnum = MailRequestFrom.Website;
            string errorMessage = "";
            try
            {
                mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress(pFrom, pFromName);

                foreach (var addr in pTO.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
                    mail.To.Add(addr.Trim());

                if (!string.IsNullOrWhiteSpace(pCC))
                    foreach (var addr in pCC.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
                        mail.CC.Add(addr.Trim());

                if (!string.IsNullOrWhiteSpace(pBCC))
                    foreach (var addr in pBCC.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
                        mail.Bcc.Add(addr.Trim());

                mail.Subject = pSubject;
                mail.Body = pBody;
                mail.IsBodyHtml = true;

                if (!string.IsNullOrWhiteSpace(pFileName) && File.Exists(pFileName))
                {
                    mail.Attachments.Add(new Attachment(pFileName));
                }

                smtpHost = smtpHost ?? ConfigurationManager.AppSettings["SmtpHost"];
                smtpPort = smtpPort == 0 ? Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"] ?? "25") : smtpPort;

                smtp = new SmtpClient(smtpHost, smtpPort)
                {
                    EnableSsl = enableSsl
                };

                if (!string.IsNullOrWhiteSpace(smtpUser))
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                else
                    smtp.UseDefaultCredentials = true;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                statusEnum = MailStatus.Failed;
                errorMessage = ex.ToString();
                LogEmailToDB(0, pFrom, pTO, pCC, pBCC, pSubject, pBody, statusEnum.ToString(), errorMessage, "", "", requestFromEnum.ToString());
            }
            finally
            {
                // Dispose explicitly if needed (MailMessage attachments can lock files)
                if (mail != null)
                {
                    foreach (var attachment in mail.Attachments)
                    {
                        attachment.Dispose();
                    }
                    mail.Dispose();
                }

                if (smtp != null)
                {
                    smtp.Dispose();
                }
                LogEmailToDB(0, pFrom, pTO, pCC, pBCC, pSubject, pBody, statusEnum.ToString(), errorMessage, "", "", requestFromEnum.ToString());

            }
        }



        /// <summary>
        /// /// Log email details to the database.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="status"></param>
        /// <param name="errorMessage"></param>
        /// <param name="messageId"></param>
        /// <param name="attachments"></param>
        /// <param name="requestFrom"></param>
        public static void LogEmailToDB(int userId, string sender, string recipient, string cc, string bcc,
                                     string subject, string body, string status, string errorMessage,
                                     string messageId, string attachments, string requestFrom)
        {
            string connStr = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_InsertEmailLog, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@userid", userId);
                cmd.Parameters.AddWithValue("@sender", (object)sender ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@recipient", (object)recipient ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@cc", (object)cc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@bcc", (object)bcc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@subject", (object)subject ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@body", (object)body ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@status", (object)status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@error_message", (object)errorMessage ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@message_id", (object)messageId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@attachments", (object)attachments ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@requestfrom", (object)requestFrom ?? DBNull.Value); // New parameter

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// /// This method retrieves tours at a glance for a specific day, month, year, and branch code.
        /// </summary>
        /// <param name="pDay"></param>
        /// <param name="pMonth"></param>
        /// <param name="pYear"></param>
        /// <param name="pBranchCode"></param>
        /// <returns></returns>
        public static DataTable Fixed_ToursAtAGlance(string pDay, string pMonth, string pYear, string pBranchCode)
        {
            DataTable ldtToursAtAGlance = null;
            ClsAdo clsObj = null;
            DataSet ldsTourVacantSeats = null;
            try
            {
                #region Optimize Code
                /* SqlParameter[] lParam = new SqlParameter[4];
                lParam[0] = new SqlParameter("@I_Day", pDay);
                lParam[1] = new SqlParameter("@I_Month", pMonth);
                lParam[2] = new SqlParameter("@I_Year", pYear);
                lParam[3] = new SqlParameter("@I_Branch", pBranchCode);
                DataSet lds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString,
                    "Fixed_DatewiseTours_Vacantseats", lParam);*/
                #endregion
                clsObj = new ClsAdo();
                ldsTourVacantSeats = clsObj.fnFixed_DatewiseTours_Vacantseats(pDay, pMonth, pYear, pBranchCode);
                if (ldsTourVacantSeats != null ? ldsTourVacantSeats.Tables.Count > 0 : false)
                {
                    ldtToursAtAGlance = ldsTourVacantSeats.Tables[0];
                }
                return ldtToursAtAGlance != null ? ldtToursAtAGlance.Copy() : null;
            }
            catch (Exception Ex)
            {
                return null;
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (ldtToursAtAGlance != null)
                {
                    ldtToursAtAGlance.Dispose();
                    ldtToursAtAGlance = null;
                }
                if (ldsTourVacantSeats != null)
                {
                    ldsTourVacantSeats.Dispose();
                    ldsTourVacantSeats = null;
                }
            }
        }


        /// <summary>
        /// /// Convert a string to an integer, returning 0 if the string is null, empty, or not a valid integer.// If the string is "&nbsp;", it also returns 0.
        /// </summary>
        /// <param name="strInt"></param>
        /// <returns></returns>
        public static int ConvertStringint(object strInt)
        {
            int intval = 0;
            if (string.IsNullOrEmpty(Convert.ToString(strInt)) || strInt == "&nbsp;")
            {
                intval = 0;
            }
            else
            {
                try
                {
                    intval = Convert.ToInt32(strInt);
                }
                catch (Exception ex)
                {
                    intval = 0;
                }
            }
            return intval;
        }

        /// <summary>
        /// /// This method adds a fixed pickup place for a given tour ID.
        /// </summary>
        /// <param name="pTourID"></param>
        /// <returns></returns>
        public static int Add_Fixed_PickupPlace(int pTourID)
        {
            #region Optimize Code
            /*SqlParameter[] param1 = new SqlParameter[1];
            param1[0] = new SqlParameter("@tourno", pTourID);
            DataSet dsDefault = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "Fixed_Default_PickupAddress", param1);*/
            #endregion
            ClsAdo clsObj = null;
            DataSet dsDefault = null;
            try
            {
                clsObj = new ClsAdo();

                dsDefault = clsObj.fnFixed_Default_PickupAddress(pTourID);
                if (dsDefault != null && dsDefault.Tables.Count > 0 && dsDefault.Tables[0].Rows.Count > 0)
                {
                    string arrtime = "06:00:00 AM";
                    string dept = Convert.ToString(dsDefault.Tables[0].Rows[0]["departuretime"]);
                    if (dept.Length > 2)
                    {
                        DateTime dt = DateTime.Parse(dept).AddMinutes(-30);
                        string[] ddd = Convert.ToString(dt).Split(' ');
                        arrtime = ddd[1] + " " + ddd[2];
                    }
                    #region Optimize Code
                    /*SqlParameter[] paramins = new SqlParameter[8];
                paramins[0] = new SqlParameter("@rowId", Convert.ToInt32(dsDefault.Tables[0].Rows[0]["Rowid"]));
                paramins[1] = new SqlParameter("@tourno", pTourID);
                paramins[2] = new SqlParameter("@PickPlace", Convert.ToString(dsDefault.Tables[0].Rows[0]["branchname"]).Replace("'", "''"));
                paramins[3] = new SqlParameter("@strarrHrs", arrtime);
                paramins[4] = new SqlParameter("@strDropHrs", arrtime);
                paramins[5] = new SqlParameter("@active", "Y");
                paramins[6] = new SqlParameter("@bcode", Convert.ToString(dsDefault.Tables[0].Rows[0]["branchcode"]));
                paramins[7] = new SqlParameter("@strAddress", Convert.ToString(dsDefault.Tables[0].Rows[0]["address"]).Replace("'", "''"));
                return DataLib.InsStoredProcData("dispup_sp", paramins);*/
                    #endregion
                    int Val = clsObj.fnInsertUpdatedispup(Convert.ToInt32(dsDefault.Tables[0].Rows[0]["Rowid"]), pTourID,
                        Convert.ToString(dsDefault.Tables[0].Rows[0]["branchname"]).Replace("'", "''"),
                        arrtime, arrtime, 'Y', Convert.ToString(dsDefault.Tables[0].Rows[0]["branchcode"]),
                        Convert.ToString(dsDefault.Tables[0].Rows[0]["address"]).Replace("'", "''"));
                    return Val;
                }
                else
                    return 2;
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (dsDefault != null)
                {
                    dsDefault.Dispose();
                    dsDefault = null;
                }
            }
        }


        /// <summary>
        /// /// This method blocks seats for a specific tour and returns a boolean indicating success or failure.
        /// </summary>
        /// <param name="pSeats"></param>
        /// <param name="pTourSerial"></param>
        /// <param name="pBlockedString"></param>
        /// <returns></returns>
        public static bool Block_Seats(string pSeats, int pTourSerial, string pBlockedString)
        {
            int val = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.BlockUnBlockSeats_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TSerial", SqlDbType.VarChar, 15) { Value = pTourSerial.ToString() });
                    cmd.Parameters.Add(new SqlParameter("@Seat", SqlDbType.VarChar, -1) { Value = pSeats }); // VarChar(MAX) = -1
                    cmd.Parameters.Add(new SqlParameter("@BlockedString", SqlDbType.VarChar, 150) { Value = pBlockedString });
                    cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Bit) { Value = true });

                    // Output parameter for return value
                    SqlParameter outputParam = new SqlParameter("@ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (outputParam.Value != DBNull.Value)
                    {
                        val = Convert.ToInt32(outputParam.Value);
                    }
                }
            }
            catch (Exception)
            {
                val = -1;
            }

            return val > 0;
        }


        /// <summary>
        /// /// This method unblocks seats for a specific tour.
        /// </summary>
        /// <param name="pSeats"></param>
        /// <param name="pTourSerial"></param>
        public static void UnBlock_Seats(string pSeats, int pTourSerial)
        {
            try
            {
                int status = new ClsAdo().fnBlockUnBlockSeats_sp(pTourSerial.ToString(), pSeats, "NULL", false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// /// This method inserts a new online tours booking for an agent.
        /// </summary>
        /// <param name="pOrderID"></param>
        /// <param name="pTourID"></param>
        /// <param name="pDOJ"></param>
        /// <param name="pDOB"></param>
        /// <param name="pBusEnvType"></param>
        /// <param name="pAdults"></param>
        /// <param name="pChilds"></param>
        /// <param name="pTwin"></param>
        /// <param name="pTriple"></param>
        /// <param name="pChildNoBed"></param>
        /// <param name="pSingle"></param>
        /// <param name="pTourName"></param>
        /// <param name="pAmount"></param>
        /// <param name="pTaxPercent"></param>
        /// <param name="pTax"></param>
        /// <param name="pTotalAmount"></param>
        /// <param name="pSeatNos"></param>
        /// <param name="pBusSerial"></param>
        /// <param name="pTourSerial"></param>
        /// <param name="pPickupPointID"></param>
        /// <param name="pAdultFare"></param>
        /// <param name="pChildFare"></param>
        /// <param name="pTwinFare"></param>
        /// <param name="pTripleFare"></param>
        /// <param name="pChildNoBedFare"></param>
        /// <param name="pSingleFare"></param>
        /// <param name="pCCFee"></param>
        /// <param name="pCCAmount"></param>
        /// <param name="pDormitory"></param>
        /// <param name="pDormitoryFare"></param>
        /// <param name="pRemarks"></param>
        /// <param name="pOnlineDiscont"></param>
        /// <param name="pNoAWFood"></param>
        /// <param name="pNoCWFood"></param>
        /// <param name="pAdWFoodfare"></param>
        /// <param name="pCWFoodfare"></param>
        /// <param name="pAdvancePaid"></param>
        /// <param name="lIsPartialPaymentByAgent"></param>
        /// <param name="ServiceChargesTotal"></param>
        /// <param name="ServiceChargesTax"></param>
        /// <param name="ServiceChargesTaxVal"></param>
        /// <param name="AdultServiceCharges"></param>
        /// <param name="ChildServiceCharges"></param>
        /// <returns></returns>
        public static int Insert_OnlineToursBookingAgent(string pOrderID, int pTourID, DateTime pDOJ, DateTime pDOB, char pBusEnvType,
       int pAdults, int pChilds, int pTwin, int pTriple, int pChildNoBed, int pSingle, string pTourName, decimal pAmount,
       decimal pTaxPercent, decimal pTax, decimal pTotalAmount, string pSeatNos, string pBusSerial, string pTourSerial,
       int pPickupPointID, decimal pAdultFare, decimal pChildFare, decimal pTwinFare, decimal pTripleFare, decimal pChildNoBedFare,
       decimal pSingleFare, decimal pCCFee, decimal pCCAmount, int pDormitory, decimal pDormitoryFare, string pRemarks, string pOnlineDiscont,
       int pNoAWFood, int pNoCWFood, decimal pAdWFoodfare, decimal pCWFoodfare, decimal pAdvancePaid, bool lIsPartialPaymentByAgent,
        decimal ServiceChargesTotal, decimal ServiceChargesTax, decimal ServiceChargesTaxVal, decimal AdultServiceCharges, decimal ChildServiceCharges)
        {

            ClsAdo clsObj = null;
            OnlineToursBooking clsTblObj = null;
            try
            {
                clsObj = new ClsAdo();
                clsTblObj = new OnlineToursBooking();
                clsTblObj.orderid = pOrderID;
                clsTblObj.tourid = Convert.ToString(pTourID);
                clsTblObj.doj = pDOJ;
                clsTblObj.dob = pDOB;
                clsTblObj.BusEnvType = pBusEnvType;
                clsTblObj.NoofAdults = Convert.ToInt16(pAdults);
                clsTblObj.NoofChild = Convert.ToInt16(pChilds);
                clsTblObj.NoofAdultsTwin = Convert.ToInt16(pTwin);
                clsTblObj.NoofAdultsTriple = Convert.ToInt16(pTriple);
                clsTblObj.ChildWithoutbed = Convert.ToInt16(pChildNoBed);
                clsTblObj.SingleAdult = Convert.ToInt16(pSingle);
                clsTblObj.Amount = decimal.Round(pAmount);
                clsTblObj.TourName = pTourName;
                clsTblObj.Tax = pTaxPercent;
                clsTblObj.CalcTaxvalue = pTax;
                clsTblObj.TotalAmount = decimal.Round(pTotalAmount);
                clsTblObj.SeatNo = pSeatNos;
                clsTblObj.BusSerialNo = pBusSerial;
                clsTblObj.TourSerial = Convert.ToString(pTourSerial);
                clsTblObj.PickUppointid = pPickupPointID;
                clsTblObj.adultfare = pAdultFare;
                clsTblObj.childfare = pChildFare;
                clsTblObj.adultstwinfare = pTwinFare;
                clsTblObj.adultstriplefare = pTripleFare;
                clsTblObj.childwithoutbedfare = pChildNoBedFare;
                clsTblObj.singleadultfare = pSingleFare;
                clsTblObj.CreditCardFee = pCCFee;
                clsTblObj.CalcCreditCardFee = decimal.Round(pCCAmount);
                clsTblObj.Remarks = pRemarks;
                clsTblObj.dormitory = Convert.ToInt16(pDormitory);
                clsTblObj.dormitoryfare = pDormitoryFare;
                clsTblObj.AdvancePaid = pAdvancePaid;
                clsTblObj.OnLineDis = pOnlineDiscont;
                clsTblObj.noAdultWithFood = pNoAWFood;
                clsTblObj.noChildWithFood = pNoCWFood;
                clsTblObj.AdultWithFoodFare = Convert.ToDecimal(pAdWFoodfare);
                clsTblObj.ChildWithFoodFare = Convert.ToDecimal(pCWFoodfare);
                clsTblObj.IsPartialPaymentByAgent = lIsPartialPaymentByAgent;
                clsTblObj.AdultServiceCharges = Convert.ToDecimal(AdultServiceCharges);
                clsTblObj.ChildServiceCharges = Convert.ToDecimal(ChildServiceCharges);
                clsTblObj.ServiceChargesTotal = ServiceChargesTotal;
                clsTblObj.ServiceChargesTax = Convert.ToDecimal(ServiceChargesTax);
                clsTblObj.ServiceChargesTaxVal = Convert.ToDecimal(ServiceChargesTaxVal);
                int val = clsObj.fnInsertbookAgent(clsTblObj);
                return val;
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (clsTblObj != null)
                {
                    clsTblObj = null;
                }
            }
        }

        /// <summary>
        /// /// This method updates an existing online tours booking for an agent.
        /// </summary>
        /// <param name="pJDate"></param>
        /// <param name="pBusEnvType"></param>
        /// <param name="pAdults"></param>
        /// <param name="pChilds"></param>
        /// <param name="pTwin"></param>
        /// <param name="pTriple"></param>
        /// <param name="pChildNoBed"></param>
        /// <param name="pSingle"></param>
        /// <param name="pAmount"></param>
        /// <param name="pTax"></param>
        /// <param name="pTaxAmount"></param>
        /// <param name="pTotalAmt"></param>
        /// <param name="pSeatNos"></param>
        /// <param name="pBusSerial"></param>
        /// <param name="pTourSerial"></param>
        /// <param name="pPickupID"></param>
        /// <param name="pAdultFare"></param>
        /// <param name="pChildFare"></param>
        /// <param name="pTwinFare"></param>
        /// <param name="pTripleFare"></param>
        /// <param name="pChildNoBedFare"></param>
        /// <param name="pSingleFare"></param>
        /// <param name="pCCFee"></param>
        /// <param name="pCCAmount"></param>
        /// <param name="pRowID"></param>
        /// <param name="pDormitory"></param>
        /// <param name="pDormitoryFare"></param>
        /// <param name="pRemarks"></param>
        /// <param name="pNoAWFood"></param>
        /// <param name="pNoCWFood"></param>
        /// <param name="pAdWFoodfare"></param>
        /// <param name="pCWFoodfare"></param>
        /// <param name="pAdvancePaid"></param>
        /// <param name="lIsPartialPaymentByAgent"></param>
        /// <param name="ServiceChargesTotal"></param>
        /// <param name="ServiceChargesTax"></param>
        /// <param name="ServiceChargesTaxVal"></param>
        /// <param name="AdultServiceCharges"></param>
        /// <param name="ChildServiceCharges"></param>
        /// <returns></returns>
        public static int Update_OnlineToursBookingAgent(DateTime pJDate, char pBusEnvType, int pAdults, int pChilds, int pTwin,
     int pTriple, int pChildNoBed, int pSingle, decimal pAmount, decimal pTax, decimal pTaxAmount, decimal pTotalAmt,
     string pSeatNos, string pBusSerial, int pTourSerial, int pPickupID, decimal pAdultFare, decimal pChildFare,
     decimal pTwinFare, decimal pTripleFare, decimal pChildNoBedFare, decimal pSingleFare, decimal pCCFee, decimal pCCAmount,
     int pRowID, int pDormitory, decimal pDormitoryFare, string pRemarks, int pNoAWFood, int pNoCWFood, decimal pAdWFoodfare, decimal pCWFoodfare, decimal pAdvancePaid,
     bool lIsPartialPaymentByAgent, decimal ServiceChargesTotal, decimal ServiceChargesTax, decimal ServiceChargesTaxVal, decimal AdultServiceCharges, decimal ChildServiceCharges)
        {

            ClsAdo clsObj = null;
            OnlineToursBooking clsTblObj = null;
            try
            {
                clsObj = new ClsAdo();
                clsTblObj = new OnlineToursBooking();
                clsTblObj.doj = pJDate;
                clsTblObj.BusEnvType = pBusEnvType;
                clsTblObj.NoofAdults = Convert.ToInt16(pAdults);
                clsTblObj.NoofChild = Convert.ToInt16(pChilds);
                clsTblObj.NoofAdultsTwin = Convert.ToInt16(pTwin);
                clsTblObj.NoofAdultsTriple = Convert.ToInt16(pTriple);
                clsTblObj.ChildWithoutbed = Convert.ToInt16(pChildNoBed);
                clsTblObj.SingleAdult = Convert.ToInt16(pSingle);
                clsTblObj.Amount = decimal.Round(pAmount);
                clsTblObj.Tax = pTaxAmount;
                clsTblObj.CalcTaxvalue = pTax;
                clsTblObj.TotalAmount = decimal.Round(pTotalAmt);
                clsTblObj.SeatNo = pSeatNos;
                clsTblObj.BusSerialNo = pBusSerial;
                clsTblObj.TourSerial = Convert.ToString(pTourSerial);
                clsTblObj.PickUppointid = pPickupID;
                clsTblObj.adultfare = pAdultFare;
                clsTblObj.childfare = pChildFare;
                clsTblObj.adultstwinfare = pTwinFare;
                clsTblObj.adultstriplefare = pTripleFare;
                clsTblObj.childwithoutbedfare = pChildNoBedFare;
                clsTblObj.singleadultfare = pSingleFare;
                clsTblObj.CreditCardFee = pCCFee;
                clsTblObj.CalcCreditCardFee = decimal.Round(pCCAmount);
                clsTblObj.rowid = pRowID;
                clsTblObj.dormitory = Convert.ToInt16(pDormitory);
                clsTblObj.dormitoryfare = pDormitoryFare;
                clsTblObj.Remarks = pRemarks;
                clsTblObj.noAdultWithFood = pNoAWFood;
                clsTblObj.noChildWithFood = pNoCWFood;
                clsTblObj.AdultWithFoodFare = Convert.ToDecimal(pAdWFoodfare);
                clsTblObj.ChildWithFoodFare = Convert.ToDecimal(pCWFoodfare);
                clsTblObj.AdvancePaid = pAdvancePaid;
                clsTblObj.IsPartialPaymentByAgent = lIsPartialPaymentByAgent;
                clsTblObj.AdultServiceCharges = Convert.ToDecimal(AdultServiceCharges);
                clsTblObj.ChildServiceCharges = Convert.ToDecimal(ChildServiceCharges);
                clsTblObj.ServiceChargesTotal = ServiceChargesTotal;
                clsTblObj.ServiceChargesTax = Convert.ToDecimal(ServiceChargesTax);
                clsTblObj.ServiceChargesTaxVal = Convert.ToDecimal(ServiceChargesTaxVal);
                int val = clsObj.fnUpdatebookAgent(clsTblObj);
                return val;
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (clsTblObj != null)
                {
                    clsTblObj = null;
                }
            }
        }


        /// <summary>
        /// /// This method retrieves the request status based on a boolean status and an exception enum.
        /// </summary>
        /// <param name="pStatus"></param>
        /// <param name="pException"></param>
        /// <returns></returns>
        public static RequestStatus fnGetRequestStatus(bool pStatus, pbException pException)
        {
            RequestStatus result = default(RequestStatus);
            result.Status = pStatus;
            result.ErrNo = Convert.ToInt32(pException);
            result.ErrDesc = fnGetEnumDescription(pException);
            return result;
        }


        /// <summary>
        /// /// This method retrieves the description of an enum value.
        /// </summary>
        /// <param name="pValue"></param>
        /// <returns></returns>
        internal static string fnGetEnumDescription(Enum pValue)
        {
            FieldInfo field = pValue.GetType().GetField(pValue.ToString());
            DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
            return (array.Length > 0) ? array[0].Description : pValue.ToString();
        }

        /// <summary>
        /// /// This method generates a row of HTML for costing details.
        /// </summary>
        /// <param name="pCostType"></param>
        /// <param name="pNoOfPerson"></param>
        /// <param name="pSinglePersonFare"></param>
        /// <param name="pTotalFare"></param>
        /// <returns></returns>
        public static string GetCostingRow(string pCostType, decimal pNoOfPerson, decimal pSinglePersonFare, decimal pTotalFare)
        {
            StringBuilder lReturnValue = new StringBuilder();
            lReturnValue.Append(" <tr><td width=\"35%\"><SPAN class=\"cgi\">" + pCostType + "</SPAN></td>");
            lReturnValue.Append("<td width=\"5%\"><SPAN class=\"hlinks\">" + pNoOfPerson + "</SPAN></td>");
            lReturnValue.Append("<td width=\"5%\"><SPAN class=\"hlinks\">" + " X " + "</SPAN></td>");
            lReturnValue.Append("<td width=\"5%\"><SPAN class=\"hlinks\">" + pSinglePersonFare + "</SPAN></td>");
            lReturnValue.Append("<td width=\"5%\"><SPAN class=\"hlinks\">" + "&nbsp;=&nbsp;" + "</SPAN></td>");
            lReturnValue.Append("<td width=\"5%\"><SPAN class=\"hlinks\">" + pTotalFare + "</SPAN></td>");
            lReturnValue.Append("<td width=\"40%\"><SPAN class=\"hlinks\">&nbsp;</SPAN></td>");
            lReturnValue.Append("</tr>");
            return lReturnValue.ToString();
        }

        /// <summary>
        /// /// This method retrieves the tour ticket banner for a specific tour ID and type code.
        /// </summary>
        /// <param name="pTourID"></param>
        /// <param name="pTourTypeCode"></param>
        /// <param name="pUser"></param>
        /// <returns></returns>
        public static string GetTourTicketBanner(int pTourID, string pTourTypeCode, string pUser)
        {
            STSPLOrOther lSPLToDB;
            DataTable dtTourTicket = null;
            StringBuilder lFinalHTML = new StringBuilder();
            lSPLToDB = null;
            try
            {
                lSPLToDB = new STSPLOrOther();
                dtTourTicket = new DataTable();
                dtTourTicket = lSPLToDB.fnGetTourTicketBanner(pTourID, pTourTypeCode);

                if (dtTourTicket != null && dtTourTicket.Rows.Count > 0)
                {
                    lFinalHTML.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0' align='left' border='0'>");
                    lFinalHTML.Append("<tr>");
                    for (int pCount = 0; pCount < dtTourTicket.Rows.Count; pCount++)
                    {
                        for (int pSequence = 1; pSequence <= 10; pSequence++)
                        {
                            if (pSequence == Convert.ToInt32(dtTourTicket.Rows[pCount]["ImageSequence"]))
                            {
                                string hrfLink = string.Empty;
                                if (dtTourTicket.Rows[pCount]["Link"].ToString() != "")
                                {
                                    hrfLink = dtTourTicket.Rows[pCount]["Link"].ToString();
                                }
                                string imgURL = string.Empty;
                                if (dtTourTicket.Rows[pCount]["ImageUrl"].ToString() != "")
                                {
                                    imgURL = dtTourTicket.Rows[pCount]["ImageUrl"].ToString().Replace('\\', '/');
                                }
                                string appPath = System.Web.HttpContext.Current.Request.Url.Host.ToString();
                                if (appPath.ToLower() == "southerntravelsindia")
                                {
                                    appPath = "http://" + appPath;
                                    appPath += ".com/";
                                }
                                else if (appPath.ToLower() == "localhost")
                                {
                                    appPath = "http://" + appPath;
                                    appPath += ":4541//SouthernTravels_New/ControlPanel/";
                                }
                                else if (pUser == "EndUser")
                                {
                                    appPath = "";
                                }
                                else
                                {
                                    appPath = "../";
                                }
                                appPath = "http://www.southerntravelsindia.com/";
                                StringBuilder strLink = new StringBuilder();
                                if (hrfLink != string.Empty)
                                {
                                    strLink.Append("<a href='" + hrfLink + "' target='_blank'><img src='" + appPath + imgURL + "' width='228'  border='0' /></a>");
                                }
                                else
                                {
                                    strLink.Append("<img src='" + appPath + imgURL + "' width='228'  border='0' />");
                                }
                                if (dtTourTicket.Rows[pCount]["Alignment"].ToString() != "")
                                {
                                    lFinalHTML.Append("<td aling='" + dtTourTicket.Rows[pCount]["Alignment"].ToString() + "'>");
                                }
                                else
                                {
                                    lFinalHTML.Append("<td>");
                                }
                                lFinalHTML.Append(strLink.ToString());
                                lFinalHTML.Append("</td>");
                            }
                        }
                    }
                    lFinalHTML.Append("</tr>");
                    lFinalHTML.Append("</table>");
                }
                else
                {
                    string appPath = System.Web.HttpContext.Current.Request.Url.Host.ToString();
                    if (appPath.ToLower() == "southerntravelsindia")
                    {
                        appPath = "http://" + appPath;
                        appPath += ".com/";
                    }
                    else if (appPath.ToLower() == "localhost")
                    {
                        appPath = "http://" + appPath;
                        appPath += ":2532/Site/";
                    }
                    else if (pUser == "EndUser")
                    {
                        appPath = "";
                    }
                    else
                    {
                        appPath = "../";
                    }
                    appPath = "http://www.southerntravelsindia.com/";
                    lFinalHTML.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0' align='left' border='0'>");
                    lFinalHTML.Append("<tr>");
                    lFinalHTML.Append("<td>");
                    lFinalHTML.Append(" <a href='http://www.facebook.com/SouthernTravels?sk=app_190322544333196' target='_blank'><img src='" + appPath + "images/fb.jpg' width='228'  border='0' /></a>");
                    lFinalHTML.Append("</td>");
                    lFinalHTML.Append("<td align=center>");
                    lFinalHTML.Append("<a href='http://www.southerntravelsindia.com/International-GroupDeparture.aspx'><img src='" + appPath + "images/singapore1.jpg' width='228'  border='0' /></a>");
                    lFinalHTML.Append("</td>");
                    lFinalHTML.Append("<td align=right>");
                    lFinalHTML.Append("<a href='http://www.southerntravelsindia.com/hotel-southern-jaipur.aspx'><img src='" + appPath + "images/southerngrand.jpg' width='228'  border='0' /></a>");
                    lFinalHTML.Append("</td>");
                    lFinalHTML.Append("</tr>");
                    lFinalHTML.Append("</table>");
                }
            }
            finally
            {
                if (lSPLToDB != null)
                {
                    lSPLToDB = null;
                }
                if (dtTourTicket != null)
                {
                    dtTourTicket.Dispose();
                    dtTourTicket = null;
                }
            }
            return lFinalHTML.ToString();
        }

        private static byte[] EncryptionData(byte[] clearData, byte[] Key, byte[] IV)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Rijndael alg = Rijndael.Create();
                alg.Key = Key;
                alg.IV = IV;
                CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(clearData, 0, clearData.Length);
                cs.Close();
                byte[] encryptedData = ms.ToArray();
                return encryptedData;
            }
            catch
            {
                return null;
            }

        }
        static string _Password = "SouthernTravels";
        static byte[] _Salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
        /// <summary>
        /// /// This method encrypts a given string using a password and salt.
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public static string Encrypt(string inputText)
        {
            string encryptedValue = "";
            try
            {

                byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(inputText);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(_Password, _Salt);
                byte[] encryptedData = EncryptionData(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                encryptedValue = Convert.ToBase64String(encryptedData);

                // VSR: Escape String (URL Encode) whenever passing encrypted data in URL
                return encryptedValue;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// /// This method sends a seat request email with the provided details.
        /// </summary>
        /// <param name="pRefno"></param>
        /// <param name="pTourName"></param>
        /// <param name="pGroupLeader"></param>
        /// <param name="pJdate"></param>
        /// <param name="pAdults"></param>
        /// <param name="pChild"></param>
        /// <param name="pBusType"></param>
        /// <param name="pAddress"></param>
        /// <param name="pCity"></param>
        /// <param name="pState"></param>
        /// <param name="pLandLine"></param>
        /// <param name="pMobile"></param>
        /// <param name="pEmail"></param>
        public static void SeatRequest(string pRefno, string pTourName, string pGroupLeader, DateTime pJdate, string pAdults, string pChild, string pBusType, string pAddress, string pCity, string pState, string pLandLine, string pMobile, string pEmail)
        {
            System.Text.StringBuilder lsb = new System.Text.StringBuilder();
            lsb.Append("Reference No: " + pRefno + ",<br />");
            lsb.Append("Dear Manager,<br />");
            lsb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Southern Travels<br />");
            lsb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Subject - Request for Seats  - " + pTourName + "<br />");
            lsb.Append("&nbsp;&nbsp;&nbsp;Sir I am " + pGroupLeader + ",<br />");
            lsb.Append("I was Requesting You to Please Arrange Seats. Details are as follows:<br />");
            lsb.Append("Tour Name: " + pTourName + "<br />");
            lsb.Append("Journey date: " + pJdate + "<br />");
            lsb.Append("No Of Adults: " + pAdults + "<br />");
            lsb.Append("No Of Childs: " + pChild + "<br />");
            lsb.Append("Bus Type(AC/NonAC): " + pBusType + "<br />");
            lsb.Append("Address: " + pAddress + "<br />");
            lsb.Append("City: " + pCity + "<br />");
            lsb.Append("State: " + pState + "<br />");
            lsb.Append("Phone No: " + pLandLine + "<br />");
            lsb.Append("Mobile No: " + pMobile + "<br />");
            lsb.Append("e-Mail: " + pEmail + "<br />");
            if (pEmail == "")
                pEmail = "request@seat.com";
            string lToEmail = ConfigurationSettings.AppSettings["SeatRequestTO"].ToString();
            sendmail(lToEmail, ConfigurationSettings.AppSettings["SeatRequestBCC"].ToString(), "", pEmail, pRefno + ": Request For Arrangement of Seats: - " + pTourName, lsb.ToString(), "");
        }



        public static void LogAndSendError(string error)
        {
            string connectionString = DataLib.getConnectionString(); // Ensure this returns a valid connection string

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.LogError_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Error", error);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Send the error mail after logging
                string toMail = ConfigurationManager.AppSettings["errormail"];
                ClsCommon.sendmail(
                    toMail, "", "",
                     ConfigurationManager.AppSettings["TicketEmail"],
                    "Error Has Been Caught in Application_Error event",
                    error, ""
                );
            }
            catch (Exception ex)
            {
                // Log to file, event log, or ignore to avoid recursion
                // Optional: log to fallback location
            }
        }
    }
}