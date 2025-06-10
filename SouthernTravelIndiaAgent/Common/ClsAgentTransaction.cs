using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.Common
{
    public class ClsAgentTransaction
    {
        public ClsAgentTransaction()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static DataSet AgentDetails(int pAgentID)
        {
            DataSet lds = null;
            try
            {
                SqlParameter[] lParam = new SqlParameter[1];
                lParam[0] = new SqlParameter("@agentId", pAgentID);
                lds = SqlData.GetDataSetSP(StoredProcedures.GetAgentdetails_sp, lParam);
                if (lds != null ? lds.Tables.Count > 0 : false)
                {
                    return lds.Copy();
                }
                else
                    return null;
            }
            catch (Exception Ex)
            {
                return null;
            }
            finally
            {
                if (lds != null)
                {
                    lds.Dispose();
                    lds = null;
                }
            }
        }
        public static DataTable Agent_Details(string pAgentuserID)
        {
            DataTable ldtAgentdetails = null;
            ClsAdo clsObj = null;
            try
            {
                #region Optimize Code
                /*SqlParameter[] lParam = new SqlParameter[1];
                lParam[0] = new SqlParameter("@userid", pAgentuserID);
                DataSet lds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString,
                    "agent_session_details", lParam);
                if (lds != null ? lds.Tables.Count > 0 : false)
                {
                    ldtAgentdetails = lds.Tables[0];
                }*/
                #endregion
                clsObj = new ClsAdo();
                ldtAgentdetails = clsObj.fnagent_session_details(pAgentuserID);
                return ldtAgentdetails != null ? ldtAgentdetails.Copy() : null;
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
                if (ldtAgentdetails != null)
                {
                    ldtAgentdetails.Dispose();
                    ldtAgentdetails = null;
                }
            }
        }
        public List<Agent_ProfileResult> Agent_Profile(string pAgentuserID)
        {
            ClsAdo clsObj = null;
            List<Agent_ProfileResult> lResult = null;
            try
            {
                #region Optimize Code
                /*SqlParameter[] lParam = new SqlParameter[1];
                lParam[0] = new SqlParameter("@I_Userid", pAgentuserID);
                DataSet lds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString,
                    "Agent_Profile", lParam);
                if (lds != null ? lds.Tables.Count > 0 : false)
                {
                    lAgentProfile = lds.Tables[0];
                }*/
                #endregion
                clsObj = new ClsAdo();
                lResult = new List<Agent_ProfileResult>();
                lResult = clsObj.fnAgent_Profile(pAgentuserID);

                return lResult;
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
                if (lResult != null)
                {
                    lResult = null;
                }
            }
        }
        public static int Agent_ChangePassword(string pAgentID, string pOldPassword, string pNewPassword)
        {
            int result;
            ClsAdo clsObj = null;
            try
            {
                #region Optimize Code
                /*SqlParameter[] lParam = new SqlParameter[4];
                lParam[0] = new SqlParameter("@I_AgentID", pAgentID);
                lParam[1] = new SqlParameter("@I_OldPassword", pOldPassword);
                lParam[2] = new SqlParameter("@I_NewPassword", pNewPassword);
                lParam[3] = new SqlParameter("@O_Result", 0);
                lParam[3].Direction = ParameterDirection.Output;
                result = DataLib.InsStoredProcData("Agent_changepassword", lParam);*/
                #endregion
                clsObj = new ClsAdo();
                result = clsObj.fnAgent_changepassword(pAgentID, pOldPassword, pNewPassword);
                return result;
            }
            catch (Exception Ex)
            {
                result = 2;
                return result;
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
            }
        }
        public static int Agent_UpdateProfile(string pAgentUserID, string pEmailID, string pFName, string pLName,
            DateTime pDOB, char pGender, string pAddress, string pCity, string pMobile, string pLandline, string pFax, string pPanNo)
        {
            int result;
            ClsAdo clsObj = null;
            try
            {
                #region Optimize Code
                /*SqlParameter[] lParam = new SqlParameter[12];
                lParam[0] = new SqlParameter("@I_UserID", pAgentUserID);
                lParam[1] = new SqlParameter("@I_EmailID", pEmailID);
                lParam[2] = new SqlParameter("@I_FirstName", pFName);
                lParam[3] = new SqlParameter("@I_LastName", pLName);
                lParam[4] = new SqlParameter("@I_DOB", pDOB);
                lParam[5] = new SqlParameter("@I_Gender", pGender);
                lParam[6] = new SqlParameter("@I_Address", pAddress);
                lParam[7] = new SqlParameter("@I_city", pCity);
                lParam[8] = new SqlParameter("@I_Mobile", pMobile);
                lParam[9] = new SqlParameter("@I_LandLine", pLandline);
                lParam[10] = new SqlParameter("@I_Fax", pFax);
                lParam[11] = new SqlParameter("@O_Result", 0);           
                lParam[11].Direction = ParameterDirection.Output;
                result = DataLib.InsStoredProcData("Agent_changepassword", lParam);*/
                #endregion
                clsObj = new ClsAdo();
                result = clsObj.fnAgent_UpdateProfile(pAgentUserID, pEmailID, pFName, pLName, pDOB, pGender, pAddress,
                    pCity, pMobile, pLandline, pFax, pPanNo);

                return result;
            }
            catch (Exception Ex)
            {
                result = 2;
                return result;
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
            }
        }
        public static DataTable Agent_ForgotPassword(string pAgentEmail, string pAgentResetPassword)
        {
            DataTable ldtAgentForgotPassword = null;
            ClsAdo clsObj = null;
            try
            {
                #region Optimize Code
                /*
                SqlParameter[] lParam = new SqlParameter[2];
                lParam[0] = new SqlParameter("@I_Emailid", pAgentEmail);
                lParam[1] = new SqlParameter("@I_Password", pAgentResetPassword);
                DataSet lds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString,
                    "Agent_ForgotPassword", lParam);
                if (lds != null ? lds.Tables.Count > 0 : false)
                {
                    ldtAgentForgotPassword = lds.Tables[0];
                }
                 */
                #endregion
                clsObj = new ClsAdo();
                ldtAgentForgotPassword = clsObj.fnAgent_ForgotPassword(pAgentEmail, pAgentResetPassword);
                return ldtAgentForgotPassword != null ? ldtAgentForgotPassword.Copy() : null;
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
                if (ldtAgentForgotPassword != null)
                {
                    ldtAgentForgotPassword.Dispose();
                    ldtAgentForgotPassword = null;
                }
            }
        }
        /// <summary>
        /// Get the Agent Commission for Fixed Departure Tours According to Tour Wise
        /// </summary>
        /// <param name="pTourNo"></param>
        /// <param name="pAgentLevel"></param>
        /// <returns> String Value of Commission </returns>
        public static string Agent_FixedTourCommission(int pTourNo, int pAgentLevel)
        {
            string lAgentCommission = "";
            ClsAdo clsObj = null;
            try
            {
                #region Optimize Code
                /* SqlParameter[] lParam = new SqlParameter[3];
                lParam[0] = new SqlParameter("@I_TourNo", pTourNo);
                lParam[1] = new SqlParameter("@I_AgentLevel", pAgentLevel);
                lParam[2] = new SqlParameter("@O_ReturnValue", 0);
                lParam[2].Direction = ParameterDirection.Output;
                lAgentCommission = Convert.ToString(DataLib.InsStoredProcData("GetAgentCommission_sp", lParam));*/
                #endregion
                clsObj = new ClsAdo();
                lAgentCommission = Convert.ToString(clsObj.fnGetAgentCommission(pTourNo, pAgentLevel));
                if ((lAgentCommission != null) && (lAgentCommission != ""))
                {
                    return lAgentCommission;
                }
                else
                    return "0";
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
            }
        }
        public static DataTable Agent_Availablebalance(int pAgentID)
        {
            ClsAdo clsObj = null;
            DataTable ldtAgentBalance = null;
            DataSet lds = null;
            try
            {
                #region Optimize Code
                /* SqlParameter[] lParam = new SqlParameter[1];
                lParam[0] = new SqlParameter("@AgentId", pAgentID);
                DataSet lds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString,
                    "Agent_Availablebalance", lParam);
                if (lds != null ? lds.Tables.Count > 0 : false)
                {
                    ldtAgentBalance = lds.Tables[0];
                }*/
                #endregion
                clsObj = new ClsAdo();
                lds = clsObj.fnGetAgent_Availablebalance(pAgentID);
                if (lds != null ? lds.Tables.Count > 0 : false)
                {
                    ldtAgentBalance = lds.Tables[0];
                }
                return ldtAgentBalance != null ? ldtAgentBalance.Copy() : null;
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
                if (ldtAgentBalance != null)
                {
                    ldtAgentBalance.Dispose();
                    ldtAgentBalance = null;
                }
                if (lds != null)
                {
                    lds.Dispose();
                    lds = null;
                }
            }
        }
        public static DataTable Agent_TransactionTypes(char? pAtAgent)
        {
            DataTable ldtAgentTransactionTypes = null;
            ClsAdo clsObj = null;
            try
            {
                #region Commented
                //SqlParameter[] lParam = new SqlParameter[1];
                //lParam[0] = new SqlParameter("@I_AtAgent", pAtAgent);
                //DataSet lds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString,
                //    "Agent_Transactiontypes", lParam);
                //if (lds != null ? lds.Tables.Count > 0 : false)
                //{
                //    ldtAgentTransactionTypes = lds.Tables[0];
                //}
                #endregion

                clsObj = new ClsAdo();

                ldtAgentTransactionTypes = clsObj.fnAgent_Transactiontypes(pAtAgent);

                return ldtAgentTransactionTypes != null ? ldtAgentTransactionTypes.Copy() : null;
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
                if (ldtAgentTransactionTypes != null)
                {
                    ldtAgentTransactionTypes.Dispose();
                    ldtAgentTransactionTypes = null;
                }
            }
        }
        public static DataTable Agent_Transaction_Report(char pIsFromAgent, int pAgentID, int pTransTypeId, string pFromDate, string pToDate, string pBranchCode)
        {
            DataTable ldtAgentTransaction_Report = null;
            ClsAdo clsObj = null;
            try
            {
                #region Commented
                /* SqlParameter[] lParam = new SqlParameter[6];
                lParam[0] = new SqlParameter("@I_FromAgent", pIsFromAgent);
                lParam[1] = new SqlParameter("@I_AgentId", pAgentID);
                lParam[2] = new SqlParameter("@I_TransTypeID", pTransTypeId);
                lParam[3] = new SqlParameter("@I_FromDate", pFromDate);
                lParam[4] = new SqlParameter("@I_ToDate", pToDate);
                lParam[5] = new SqlParameter("@I_BranchCode", pBranchCode);
                DataSet lds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString,
                    "agent_Transaction_report", lParam);
                if (lds != null ? lds.Tables.Count > 0 : false)
                {
                    ldtAgentTransaction_Report = lds.Tables[0];
                }*/
                #endregion

                clsObj = new ClsAdo();

                ldtAgentTransaction_Report = clsObj.fnGetAgent_Transaction_Report(pIsFromAgent, pAgentID, pTransTypeId, pFromDate, pToDate, pBranchCode);

                return ldtAgentTransaction_Report != null ? ldtAgentTransaction_Report.Copy() : null;
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
                if (ldtAgentTransaction_Report != null)
                {
                    ldtAgentTransaction_Report.Dispose();
                    ldtAgentTransaction_Report = null;
                }
            }
        }
        public static int AddFundsByNetBanking(int pAgentID, decimal pCredit, decimal pBalance, string pStatus,
            string pRefNo, int pTransTypeNo, string pUserName, string pBranchCode, string pTCode, decimal pCCCharge)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@agentId", pAgentID);
            param[1] = new SqlParameter("@Credit", pCredit);
            param[2] = new SqlParameter("@Balance", pBalance);
            param[3] = new SqlParameter("@status", pStatus);
            param[4] = new SqlParameter("@refno", pRefNo);
            param[5] = new SqlParameter("@transtype", pTransTypeNo);
            param[6] = new SqlParameter("@username", pUserName);
            param[7] = new SqlParameter("@branchcode", pBranchCode);
            param[8] = new SqlParameter("@Tcode", pTCode);
            param[9] = new SqlParameter("@CCCharge", pCCCharge);
            return DataLib.InsStoredProcData("Agent_AddFunds", param);
        }
        public static string agent_addfunds_Pendingentry(int pAgentID, string pAgentName, int pTransactionTypeId, decimal pAmount, string pStatus,
            string pTransactionNo, string pDepositorName)
        {
            string Transno = "";
            DataSet ds = null;
            ClsAdo pclsObj = null;
            #region Optimize Code
            /* SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@I_AgentId", pAgentID);
            param[1] = new SqlParameter("@I_AgentName", pAgentName);
            param[2] = new SqlParameter("@I_TransId", pTransactionTypeId);
            param[3] = new SqlParameter("@I_Amount", pAmount);
            param[4] = new SqlParameter("@I_Status", pStatus);
            param[5] = new SqlParameter("@I_TransactionNo", pTransactionNo);
            param[6] = new SqlParameter("@I_DepositorName", pDepositorName);
            //param[7] = new SqlParameter("@O_TransactionID", "0");
            //param[7].Direction = ParameterDirection.Output;
            DataSet ds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "agent_addfunds_Pendingentry", param);*/
            #endregion
            try
            {
                pclsObj = new ClsAdo();
                ds = pclsObj.fnGetagent_addfunds_Pendingentry(pAgentID, pAgentName, pTransactionTypeId,
                    pAmount, pStatus, pTransactionNo, pDepositorName);

                if (ds.Tables[0].Rows.Count > 0)
                    Transno = Convert.ToString(ds.Tables[0].Rows[0][0]);
                return Transno;
            }
            finally
            {
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }
    }
}