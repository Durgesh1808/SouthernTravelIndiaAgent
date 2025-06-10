using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.BAL
{
    public class clsBLL
    {
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
    }
}