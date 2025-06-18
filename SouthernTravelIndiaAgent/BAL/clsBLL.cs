using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.BAL
{
    /// <summary>
    /// /// This class contains business logic methods related to agent validation.
    /// </summary>
    public class clsBLL
    {
        /// <summary>
        /// /// This method validates an agent by checking their user ID against the database.
        /// </summary>
        /// <param name="pDescription"></param>
        /// <param name="pName"></param>
        /// <param name="pEmail"></param>
        /// <param name="pPhone"></param>
        /// <param name="pFax"></param>
        /// <param name="pStreet"></param>
        /// <param name="pCity"></param>
        /// <param name="pZIP"></param>
        /// <param name="pCountry"></param>
        /// <param name="pAdults"></param>
        /// <param name="pChild"></param>
        /// <param name="pArrivalDate"></param>
        /// <param name="pDeptDate"></param>
        /// <param name="pRequestType"></param>
        /// <param name="pRefNo"></param>
        /// <param name="pCaptcha"></param>
        /// <param name="pPanNo"></param>
        /// <returns></returns>
        public static int EnquiryTable_Entry(string pDescription, string pName, string pEmail, string pPhone,
 string pFax, string pStreet, string pCity, string pZIP, string pCountry,
 int pAdults, int pChild, DateTime pArrivalDate, DateTime pDeptDate,
 string pRequestType, string pRefNo, string pCaptcha, string pPanNo)
        {
            int result = 0;
            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.ins_Enq_tbl, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Desc", pDescription ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@uName", pName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", pEmail ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Phone", pPhone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Fax", pFax ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Street", pStreet ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@City", pCity ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Zip", pZIP ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Country", pCountry ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Adults", pAdults);
                        cmd.Parameters.AddWithValue("@Child", pChild);
                        cmd.Parameters.AddWithValue("@ArrivDate", pArrivalDate);
                        cmd.Parameters.AddWithValue("@DepDate", pDeptDate);
                        cmd.Parameters.AddWithValue("@type", pRequestType ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@refno", pRefNo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@captcha", pCaptcha ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@I_PanNo", pPanNo ?? (object)DBNull.Value);

                        // Output parameter
                        SqlParameter outParam = new SqlParameter("@res", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outParam);

                        cmd.ExecuteNonQuery();

                        result = outParam.Value != DBNull.Value ? Convert.ToInt32(outParam.Value) : 0;
                    }
                }
                catch (Exception ex)
                {
                    result = 2;
                }
            }

            return result;
        }

        /// <summary>
        /// For Getting the Enquery Reference Code
        /// </summary>
        /// <param name="pEnqType"></param>
        /// <returns> Reference Code </returns>
        public static DataTable Getcode(string pEnqType)
        {
            DataTable dtResult = new DataTable();
            string connStr = DataLib.getConnectionString(); // Fetch the connection string from config

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.Get_EnqueryCode, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add the parameter for the stored procedure
                        cmd.Parameters.AddWithValue("@type", pEnqType);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optional: log error
                    return null;
                }
            }

            return dtResult.Rows.Count > 0 ? dtResult.Copy() : null;
        }

        /// <summary>
        /// Get special Tour's Fare
        /// </summary>
        /// <param name="pTourID"></param>
        /// <param name="pJourneyDate"></param>
        /// <returns></returns>
        public static DataTable GetSpecialTourFare(int pTourID, DateTime pJourneyDate, int pCategoryID, int pPaxID)
        {
            DataTable ldtSpecialTourFare = null;
            ClsAdo clsObj = null;
            try
            {
                #region Optimize Code
                /*SqlParameter[] lParam = new SqlParameter[5];
                lParam[0] = new SqlParameter("@I_TourID", pTourID);
                lParam[1] = new SqlParameter("@I_TourDate", SqlDbType.SmallDateTime);
                lParam[1].Value = pJourneyDate;
                lParam[2] = new SqlParameter("@I_CategoryID", pCategoryID);
                lParam[3] = new SqlParameter("@I_PaxID", pPaxID);
                lParam[4] = new SqlParameter("@O_ReturnValue", 0);
                lParam[4].Direction = ParameterDirection.Output;
                DataSet lds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString,
                    "GetSpecialTourFare_sp", lParam);

                if (lds != null ? lds.Tables.Count > 0 : false)
                {
                    ldtSpecialTourFare = lds.Tables[0];
                }*/
                #endregion
                clsObj = new ClsAdo();
                ldtSpecialTourFare = clsObj.fnGetSpecialTourFare(pTourID, pJourneyDate, pCategoryID, pPaxID);
                return ldtSpecialTourFare != null ? ldtSpecialTourFare.Copy() : null;
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
                if (ldtSpecialTourFare != null)
                {
                    ldtSpecialTourFare.Dispose();
                    ldtSpecialTourFare = null;
                }
            }
        }

        /// <summary>
        /// /// This method inserts payment details into the PaymentDetails table for HDFC payments.
        /// </summary>
        /// <param name="pOrderID"></param>
        /// <param name="pItemCode"></param>
        /// <param name="pAmount"></param>
        /// <param name="pBankName"></param>
        /// <param name="pPaidStatus"></param>
        /// <param name="pPayMode"></param>
        /// <param name="pIsHdfc"></param>
        /// <param name="EMIMonth"></param>
        /// <param name="SectionName"></param>
        /// <returns></returns>
        public static int PaymentTable_Entry_hdfc(string pOrderID, string pItemCode, decimal pAmount, string pBankName,
    char pPaidStatus, string pPayMode, char pIsHdfc, string EMIMonth, string SectionName)
        {
            #region Optimize Code
            /*SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@orderId", pOrderID);
            param[1] = new SqlParameter("@itemCode", pItemCode);
            param[2] = new SqlParameter("@Amount", pAmount);
            param[3] = new SqlParameter("@BankName", pBankName);
            param[4] = new SqlParameter("@IsPaid", pPaidStatus);
            param[5] = new SqlParameter("@gatewayBID", "");
            param[6] = new SqlParameter("@orderDetails", "");
            param[7] = new SqlParameter("@currency", "INR");
            param[8] = new SqlParameter("@payMode", pPayMode);       
            param[9] = new SqlParameter("@ccChargeAmt", Convert.ToDecimal("0"));
            param[10] = new SqlParameter("@isHDFC", pIsHdfc);
            param[11] = new SqlParameter("@ip", Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]));
            param[12] = new SqlParameter("@TotalAmount", pAmount);
            return DataLib.InsStoredProcData("insert_tbl_PaymentDetails", param);*/
            #endregion
            ClsAdo pclsObj = null;
            try
            {
                pclsObj = new ClsAdo();
                bool lIsHdfc = false;
                int? lStatus = 0;
                if (pIsHdfc == '1')
                    lIsHdfc = true;
                int val = pclsObj.fninsert_tbl_PaymentDetails(pOrderID, pItemCode, pAmount, pBankName, pPaidStatus,
                               "", "", "INR", pPayMode, Convert.ToDecimal("0"), lIsHdfc,
                               Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]), pAmount, EMIMonth, SectionName);
                return val;
            }
            finally
            {
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
            }
        }

        /// <summary>
        /// /// This method inserts a new entry into the Enquiry table with the provided details.
        /// </summary>
        /// <param name="pDescription"></param>
        /// <param name="pName"></param>
        /// <param name="pEmail"></param>
        /// <param name="pPhone"></param>
        /// <param name="pFax"></param>
        /// <param name="pStreet"></param>
        /// <param name="pCity"></param>
        /// <param name="pZIP"></param>
        /// <param name="pCountry"></param>
        /// <param name="pAdults"></param>
        /// <param name="pChild"></param>
        /// <param name="pArrivalDate"></param>
        /// <param name="pDeptDate"></param>
        /// <param name="pRequestType"></param>
        /// <param name="pRefNo"></param>
        /// <param name="pCaptcha"></param>
        /// <returns></returns>

        public static int EnquiryTable_Entry(string pDescription, string pName, string pEmail, string pPhone,
   string pFax, string pStreet, string pCity, string pZIP, string pCountry, int pAdults, int pChild, DateTime pArrivalDate,
   DateTime pDeptDate, string pRequestType, string pRefNo, string pCaptcha)
        {
            #region Optimize Code
            /*SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@Desc", pDescription);
            param[1] = new SqlParameter("@uName", pName);
            param[2] = new SqlParameter("@Email", pEmail);
            param[3] = new SqlParameter("@Phone", pPhone);
            param[4] = new SqlParameter("@Fax", pFax);
            param[5] = new SqlParameter("@Street", pStreet);
            param[6] = new SqlParameter("@City", pCity);
            param[7] = new SqlParameter("@Zip", pZIP);
            param[8] = new SqlParameter("@Country", pCountry);
            param[9] = new SqlParameter("@Adults", pAdults);
            param[10] = new SqlParameter("@Child", pChild);
            param[11] = new SqlParameter("@ArrivDate", pArrivalDate);
            param[12] = new SqlParameter("@DepDate", pDeptDate);
            param[13] = new SqlParameter("@type", pRequestType);
            param[14] = new SqlParameter("@refno", pRefNo);
            param[15] = new SqlParameter("@captcha", pCaptcha);
            param[16] = new SqlParameter("@res", 0);
            param[16].Direction = ParameterDirection.Output;
            return DataLib.InsStoredProcData("ins_Enq_tbl", param);*/
            #endregion
            ClsAdo pClsObj = null;
            Enq_tbl ObjTbl = null;
            try
            {
                pClsObj = new ClsAdo();
                ObjTbl = new Enq_tbl();
                ObjTbl.Description = pDescription;
                ObjTbl.Name = pName;
                ObjTbl.Email = pEmail;
                ObjTbl.Phone = pPhone;
                ObjTbl.Fax = pFax;
                ObjTbl.Street = pStreet;
                ObjTbl.City = pCity;
                ObjTbl.Zip = pZIP;
                ObjTbl.Country = pCountry;
                ObjTbl.Adults = pAdults;
                ObjTbl.Child = pChild;
                ObjTbl.ArrivalDate = pArrivalDate;
                ObjTbl.DepDate = pDeptDate;
                ObjTbl.EnqType = pRequestType;
                ObjTbl.Refno = pRefNo;
                ObjTbl.captcha = pCaptcha;
                ObjTbl.PanNo = "";
                int val = pClsObj.fnins_Enq_tbl(ObjTbl);
                return val;
            }
            finally
            {
                if (pClsObj != null)
                {
                    pClsObj = null;
                }
                if (ObjTbl != null)
                {
                    ObjTbl = null;
                }
            }
        }
    }
}