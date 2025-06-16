using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SouthernTravelIndiaAgent.DAL
{
    public class ClsAdo
    {
        private static string connectionString;
        public ClsAdo()
        {
            connectionString = DataLib.getConnectionString();
        }
        /// <summary>
        /// Retrieves agent session details based on the provided user ID.     
        /// </summary>
        /// <param name="lUserID"></param>
        /// <returns></returns>
        public DataTable fnagent_session_details(string lUserID)
        {
            DataTable dtSessionDetails = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.agent_session_details, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter (adjust size as needed)
                    cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.VarChar, 50) { Value = lUserID });

                    conn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtSessionDetails);
                    }
                }

                return dtSessionDetails;
            }
            catch (Exception ex)
            {
                // Optionally log ex
                return null;
            }
        }

        /// <summary>
        ///  Saves the agent login information including AgentID and IP Address.
        /// </summary>
        /// <param name="pAgentID"></param>
        /// <param name="pIPAddress"></param>
        /// <returns></returns>
        public int fnSaveAgentLogInInfo(int? pAgentID, string pIPAddress)
        {
            int status = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.SaveAgentLogInInfo_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parameters
                        cmd.Parameters.Add("@AgentID", SqlDbType.Int).Value = (object)pAgentID ?? DBNull.Value;
                        cmd.Parameters.Add("@IpAddress", SqlDbType.VarChar, 50).Value = pIPAddress;

                        // Open and execute
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();

                        // If needed, you can return result here
                        status = 1; // success
                    }
                }
                catch (Exception ex)
                {
                    status = -1; // failure
                                 // Optionally log the error
                }
            }

            return status;
        }
        /// <summary>
        /// // Retrieves agent profile information based on the provided user ID.
        /// </summary>
        /// <param name="lUserID"></param>
        /// <returns></returns>
        public List<Agent_ProfileResult> fnAgent_Profile(string lUserID)
        {
            List<Agent_ProfileResult> results = new List<Agent_ProfileResult>();

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.Agent_Profile, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@I_Userid", lUserID);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Agent_ProfileResult agent = new Agent_ProfileResult();

                            agent.AgentId = reader.GetInt32(reader.GetOrdinal("AgentId"));
                            agent.UserId = reader["UserId"] as string;
                            agent.Password = reader["Password"] as string;
                            agent.Fname = reader["Fname"] as string;
                            agent.Lname = reader["Lname"] as string;
                            agent.DOB = reader["DOB"] as DateTime?;
                            agent.Gender = reader["Gender"] as string != null ? (char?)Convert.ToChar(reader["Gender"]) : null;
                            agent.Address = reader["Address"] as string;
                            agent.City = reader["City"] as string;
                            agent.Pin = reader["Pin"] as string;
                            agent.Mobile = reader["Mobile"] as string;
                            agent.LandLine = reader["LandLine"] as string;
                            agent.Fax = reader["Fax"] as string;
                            agent.Email = reader["Email"] as string;
                            agent.AuthorityMember = reader["AuthorityMember"] as string;
                            agent.AgentLevel = reader["AgentLevel"] as string;
                            agent.IsActive = Convert.ToChar(reader["IsActive"]);
                            agent.IsApprove = reader["IsApprove"] as string != null ? (char?)Convert.ToChar(reader["IsApprove"]) : null;
                            agent.ApprovedBy = reader["ApprovedBy"] as string;
                            agent.ApprovedOn = reader["ApprovedOn"] as DateTime?;
                            agent.State = reader["state"] as string;
                            agent.Country = reader["country"] as string;
                            agent.LocalBranch = reader["LocalBranch"] as string;
                            agent.IsForgot = reader["IsForgot"] as string != null ? (char?)Convert.ToChar(reader["IsForgot"]) : null;
                            agent.IsOnlineagent = reader["IsOnlineagent"] as string != null ? (char?)Convert.ToChar(reader["IsOnlineagent"]) : null;
                            agent.IsWLabelAgent = reader["IsWLabelAgent"] != DBNull.Value && Convert.ToBoolean(reader["IsWLabelAgent"]);
                            agent.IsSTPLPG = reader["IsSTPLPG"] != DBNull.Value && Convert.ToBoolean(reader["IsSTPLPG"]);
                            agent.WLPWD = reader["WLPWD"] as string;
                            agent.ParentAgent = reader["ParentAgent"] as int?;
                            agent.CreatedOn = reader["CreatedOn"] as DateTime?;
                            agent.PanNo = reader["PanNo"] as string;

                            results.Add(agent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle/log the exception
                    return null;
                }
            }

            return results;
        }
        /// <summary>
        ///Changes the password for an agent.
        /// </summary>
        /// <param name="pAgentID"></param>
        /// <param name="pOldPassword"></param>
        /// <param name="pNewPassword"></param>
        /// <returns></returns>
        public int fnAgent_changepassword(string pAgentID, string pOldPassword, string pNewPassword)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.Agent_changepassword, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@I_AgentID", SqlDbType.VarChar, 20) { Value = pAgentID },
                    new SqlParameter("@I_OldPassword", SqlDbType.VarChar, 50) { Value = pOldPassword },
                    new SqlParameter("@I_NewPassword", SqlDbType.VarChar, 50) { Value = pNewPassword },
                    new SqlParameter("@O_Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    }
                };
                try
                {
                    // This executes the SP and fills the output parameter
                    SqlData.ExecuteNonQuerySP(StoredProcedures.Agent_changepassword, sqlParams);

                    // Retrieve the output parameter value
                    result = Convert.ToInt32(sqlParams[3].Value);

                }
                catch (Exception ex)
                {
                    result = 0; // or log the error
                }
            }

            return result;
        }
        /// <summary>
        /// Updates the agent's profile information.
        /// </summary>
        /// <param name="pAgentUserID"></param>
        /// <param name="pEmailID"></param>
        /// <param name="pFName"></param>
        /// <param name="pLName"></param>
        /// <param name="pDOB"></param>
        /// <param name="pGender"></param>
        /// <param name="pAddress"></param>
        /// <param name="pCity"></param>
        /// <param name="pMobile"></param>
        /// <param name="pLandline"></param>
        /// <param name="pFax"></param>
        /// <param name="pPanNo"></param>
        /// <returns></returns>
        public int fnAgent_UpdateProfile(
       string pAgentUserID,
       string pEmailID,
       string pFName,
       string pLName,
       DateTime pDOB,
       char pGender,
       string pAddress,
       string pCity,
       string pMobile,
       string pLandline,
       string pFax,
       string pPanNo)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.Agent_UpdateProfile, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@I_UserID", pAgentUserID);
                    cmd.Parameters.AddWithValue("@I_EmailID", pEmailID);
                    cmd.Parameters.AddWithValue("@I_FirstName", pFName);
                    cmd.Parameters.AddWithValue("@I_LastName", pLName);
                    cmd.Parameters.AddWithValue("@I_DOB", pDOB);
                    cmd.Parameters.AddWithValue("@I_Gender", pGender);
                    cmd.Parameters.AddWithValue("@I_Address", pAddress);
                    cmd.Parameters.AddWithValue("@I_City", pCity);
                    cmd.Parameters.AddWithValue("@I_Mobile", pMobile);
                    cmd.Parameters.AddWithValue("@I_LandLine", pLandline);
                    cmd.Parameters.AddWithValue("@I_Fax", pFax);
                    cmd.Parameters.AddWithValue("@I_PanNo", pPanNo);

                    SqlParameter outParam = new SqlParameter("@O_Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outParam);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        result = Convert.ToInt32(outParam.Value);
                    }
                    catch (Exception ex)
                    {
                        // Log ex as needed
                        result = 0;
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// Retrieves agent information based on the provided email ID and password.
        /// </summary>
        /// <remarks>This method executes a stored procedure to validate the agent's credentials and
        /// retrieve their information. Ensure that the provided email ID and password match the expected format and
        /// values.</remarks>
        /// <param name="lEmailID">The email address of the agent. This parameter cannot be null or empty.</param>
        /// <param name="lPws">The password associated with the agent's account. This parameter cannot be null or empty.</param>
        /// <returns>A <see cref="DataTable"/> containing the agent's information if the operation is successful. Returns <see
        /// langword="null"/> if an error occurs during the operation.</returns>
        public DataTable fnAgent_ForgotPassword(string lEmailID, string lPws)
        {
            DataTable dt = new DataTable();

            string connectionString = DataLib.getConnectionString(); // Your method to get connection string

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.Agent_ForgotPassword, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters as expected by the stored procedure
                        cmd.Parameters.Add(new SqlParameter("@I_Emailid", SqlDbType.VarChar, 75)).Value = lEmailID;
                        cmd.Parameters.Add(new SqlParameter("@I_Password", SqlDbType.VarChar, 50)).Value = lPws;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception)
            {
                // Optionally log error
                return null;
            }
        }

        /// <summary>
        /// Retrieves pending entries for agent add funds based on various parameters.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <param name="lAgentame"></param>
        /// <param name="lTransID"></param>
        /// <param name="lAmount"></param>
        /// <param name="pvStatus"></param>
        /// <param name="lTransNo"></param>
        /// <param name="lDepositorName"></param>
        /// <returns>A <see cref="DataSet"/> containing pending entries for agent add funds.</returns>
        
        public DataSet fnGetagent_addfunds_Pendingentry(
    int? lAgentID,
    string lAgentame,
    int? lTransID,
    decimal? lAmount,
    string pvStatus,
    string lTransNo,
    string lDepositorName)
        {
            DataSet pdtDTSet = new DataSet();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("agent_addfunds_Pendingentry", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@I_AgentId", (object)lAgentID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_AgentName", (object)lAgentame ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_TransId", (object)lTransID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_Amount", (object)lAmount ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_Status", (object)pvStatus ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_TransactionNo", (object)lTransNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_DepositorName", (object)lDepositorName ?? DBNull.Value);

                    try
                    {
                        con.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(pdtDTSet);  // fills all result sets into DataSet.Tables
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optionally log the error 'ex'
                        return null;
                    }
                }
            }

            return pdtDTSet;
        }

        /// <summary>
        /// Retrieves agent transaction report based on various parameters.
        /// </summary>
        /// <param name="lFromAgent"></param>
        /// <param name="lAgentID"></param>
        /// <param name="lTransType"></param>
        /// <param name="lDateFrom"></param>
        /// <param name="lDateTo"></param>
        /// <param name="lBranchCode"></param>
        /// <returns></returns>
        public DataTable fnGetAgent_Transaction_Report(
    char? lFromAgent,
    int? lAgentID,
    int? lTransType,
    string lDateFrom,
    string lDateTo,
    string lBranchCode)
        {
            DataTable dt = new DataTable();
            string connStr = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.AgentTransaction_RPT_SP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with null check
                    cmd.Parameters.Add(new SqlParameter("@I_FromAgent", SqlDbType.Char, 1)
                    {
                        Value = (object)lFromAgent ?? DBNull.Value
                    });
                    cmd.Parameters.Add(new SqlParameter("@I_AgentId", SqlDbType.Int)
                    {
                        Value = (object)lAgentID ?? DBNull.Value
                    });
                    cmd.Parameters.Add(new SqlParameter("@I_TransTypeID", SqlDbType.Int)
                    {
                        Value = (object)lTransType ?? DBNull.Value
                    });
                    cmd.Parameters.Add(new SqlParameter("@I_FromDate", SqlDbType.VarChar, 20)
                    {
                        Value = string.IsNullOrEmpty(lDateFrom) ? (object)DBNull.Value : lDateFrom
                    });
                    cmd.Parameters.Add(new SqlParameter("@I_ToDate", SqlDbType.VarChar, 20)
                    {
                        Value = string.IsNullOrEmpty(lDateTo) ? (object)DBNull.Value : lDateTo
                    });
                    cmd.Parameters.Add(new SqlParameter("@I_BranchCode", SqlDbType.VarChar, 20)
                    {
                        Value = string.IsNullOrEmpty(lBranchCode) ? (object)DBNull.Value : lBranchCode
                    });

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                return dt;
            }
            catch (Exception)
            {
                // Optional: Log exception here
                return null;
            }
        }
        /// <summary>
        /// /// Retrieves transaction types for agents based on the provided agent character.
        /// </summary>
        /// <param name="i_AtAgent"></param>
        /// <returns></returns>
        public DataTable fnAgent_Transactiontypes(char? i_AtAgent)
        {
            DataTable dt = new DataTable();
            string connStr = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.Agent_Transactiontypes, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@I_AtAgent", SqlDbType.Char, 1)
                    {
                        Value = (object)i_AtAgent ?? DBNull.Value
                    });

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                return dt;
            }
            catch (Exception)
            {
                // Log exception if needed
                return null;
            }
        }
        /// <summary>
        /// /// Retrieves the available balance for a specific agent.   
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <returns></returns>
        public DataSet fnGetAgent_Availablebalance(int? lAgentID)
        {
            DataSet ds = new DataSet();
            string connStr = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.Agent_Availablebalance, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@AgentId", SqlDbType.Int)
                    {
                        Value = (object)lAgentID ?? DBNull.Value
                    });

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }

                return ds;
            }
            catch (Exception)
            {
                // Log or handle exception as needed
                return null;
            }
        }
        /// <summary>
        /// /// Retrieves the commission for an agent based on the tour number and agent level.
        /// </summary>
        /// <param name="lTourNo"></param>
        /// <param name="lAgentLevel"></param>
        /// <returns></returns>
        public double fnGetAgentCommission(int lTourNo, int lAgentLevel)
        {
            double agentCommission = 0;
            string connStr = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAgentCommission_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@I_TourNo", SqlDbType.Int) { Value = lTourNo });
                    cmd.Parameters.Add(new SqlParameter("@I_AgentLevel", SqlDbType.Int) { Value = lAgentLevel });

                    // Output parameter for commission
                    SqlParameter outputParam = new SqlParameter("@O_ReturnValue", SqlDbType.Float)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (outputParam.Value != DBNull.Value)
                        agentCommission = Convert.ToDouble(outputParam.Value);
                }

                return agentCommission;
            }
            catch (Exception)
            {
                // Log exception if necessary
                return 0;
            }
        }
        /// <summary>
        /// /// Retrieves active fixed tours from the database.
        /// </summary>
        /// <returns></returns>
        public DataTable fnGetFixedToursActive()
        {
            DataTable dtResult = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.Fixedtours_Active, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    da.Fill(dtResult);
                }

                return dtResult;
            }
            catch (Exception ex)
            {
                // You can log exception here if needed
                return null;
            }
            finally
            {
                if (dtResult != null)
                {
                    dtResult.Dispose();
                }
            }
        }
        /// <summary>
        /// /// Retrieves origin branches for fixed tours.
        /// </summary>
        /// <returns></returns>
        public DataTable fnFixed_OriginBranches()
        {
            DataTable dtResult = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.Fixed_OriginBranches, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    da.Fill(dtResult);
                }

                return dtResult;
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                return null;
            }
            finally
            {
                if (dtResult != null)
                {
                    dtResult.Dispose();
                }
            }
        }
        /// <summary>
        /// /// Retrieves vacant seats for fixed tours based on the specified date and branch.
        /// </summary>
        /// <param name="lDay"></param>
        /// <param name="lMonth"></param>
        /// <param name="lYear"></param>
        /// <param name="lBranch"></param>
        /// <returns></returns>
        public DataSet fnFixed_DatewiseTours_Vacantseats(string lDay, string lMonth, string lYear, string lBranch)
        {
            DataSet dsResult = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.Fixed_DatewiseTours_Vacantseats, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@I_Day", lDay);
                    cmd.Parameters.AddWithValue("@I_Month", lMonth);
                    cmd.Parameters.AddWithValue("@I_Year", lYear);
                    cmd.Parameters.AddWithValue("@I_Branch", lBranch);

                    // Fill the DataSet
                    conn.Open();
                    da.Fill(dsResult);
                }

                return dsResult;
            }
            catch (Exception ex)
            {
                // Optional: Log exception
                return null;
            }
            finally
            {
                if (dsResult != null)
                {
                    dsResult.Dispose();
                }
            }
        }
        /// <summary>
        /// /// Retrieves branch-wise tour information based on the provided HR and branch code.
        /// </summary>
        /// <param name="pHR"></param>
        /// <param name="pBranch"></param>
        /// <returns></returns>
        public DataTable fnBranchWiseTour(int? pHR, string pBranch)
        {
            DataTable dtResult = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.BranchWiseTour_Sp, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@Hr", (object)pHR ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Branch", pBranch ?? (object)DBNull.Value);

                    // Fill the DataTable
                    conn.Open();
                    da.Fill(dtResult);
                }

                return dtResult;
            }
            catch (Exception ex)
            {
                // Optional: Log error
                return null;
            }
            finally
            {
                if (dtResult != null)
                {
                    dtResult.Dispose();
                    dtResult = null;
                }
            }
        }
        /// <summary>
        /// /// Retrieves sub-ledger head information based on the provided row ID.
        /// </summary>
        /// <param name="pRowID"></param>
        /// <returns></returns>
        public List<GetSubLedgerHead_SPResult> fnGetSubLedgerHead(int pRowID)
        {
            List<GetSubLedgerHead_SPResult> resultList = new List<GetSubLedgerHead_SPResult>();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetSubLedgerHead_SP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@i_LedgerHead", pRowID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GetSubLedgerHead_SPResult item = new GetSubLedgerHead_SPResult
                            {
                                RowID = reader["RowID"] != DBNull.Value ? Convert.ToInt32(reader["RowID"]) : 0,
                                LedgerHead = reader["LedgerHead"] != DBNull.Value ? (int?)Convert.ToInt32(reader["LedgerHead"]) : null,
                                SelectionName = reader["SelectionName"] != DBNull.Value ? reader["SelectionName"].ToString() : null,
                                SelectionType = reader["SelectionType"] != DBNull.Value ? (int?)Convert.ToInt32(reader["SelectionType"]) : null,
                                IsActive = reader["IsActive"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["IsActive"]) : null,
                                CreatedBy = reader["CreatedBy"] != DBNull.Value ? reader["CreatedBy"].ToString() : null,
                                CreatedOn = reader["CreatedOn"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreatedOn"]) : null
                            };

                            resultList.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: log exception
                return null;
            }

            return resultList;
        }

        /// <summary>
        /// /// Saves the agent logout information including AgentID and IP Address.
        /// </summary>
        /// <param name="pAgentID"></param>
        /// <param name="pIPAddress"></param>
        /// <returns></returns>
        public int fnSaveAgentLogoutInfo(int? pAgentID, string pIPAddress)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.SaveAgentLogoutInfo_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AgentID", (object)pAgentID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IpAddress", string.IsNullOrEmpty(pIPAddress) ? DBNull.Value : (object)pIPAddress);

                    conn.Open();
                    result = cmd.ExecuteNonQuery(); 
                }
            }
            catch (Exception ex)
            {
                // log exception if needed
                result = -1;
            }

            return result;
        }

        /// <summary>
        /// /// Retrieves valid hotel information from the database.
        /// </summary>
        /// <returns></returns>
        public DataSet fnGetHotelInfoValid()
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetHotelInfoValid_sp, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    da.Fill(ds); // This will fill all result sets into ds.Tables[0], ds.Tables[1], etc.
                }

                return ds;
            }
            catch (Exception ex)
            {
                // Log ex if needed
                return null;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// /// Retrieves active city categories from the database.
        /// </summary>
        /// <returns></returns>
        public DataTable fnGetActive_Cat_City()
        {
            string connStr = DataLib.getConnectionString(); // Your connection string
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetActive_Cat_City_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optionally log the exception here
                    return null;
                }
                finally
                {
                    if (dtResult != null)
                    {
                        dtResult.Dispose();
                    }
                }
            }

            return dtResult;
        }

        /// <summary>
        /// /// Retrieves car transfer types based on the provided city ID and user type.
        /// </summary>
        /// <param name="pCityID"></param>
        /// <param name="lUserType"></param>
        /// <returns></returns>
        public DataTable fnCar_TransferTypes_CityWise(int? pCityID, string lUserType)
        {
            string connStr = DataLib.getConnectionString(); // your connection string
            DataTable dtResult = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.Car_TransferTypes_CityWise, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.Add("@cityid", SqlDbType.Int).Value = (object)pCityID ?? DBNull.Value;
                        cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 10).Value = lUserType ?? string.Empty;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
               
            }

            return dtResult;
        }

        /// <summary>
        /// /// Retrieves special tours based on the provided zone ID.
        /// </summary>
        /// <param name="pZoneID"></param>
        /// <returns></returns>
        public DataTable fnSpecialTours_Zonewise(int pZoneID)
        {
            DataTable pdtTable = new DataTable();


            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.SpecialTours_Zonewise, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ZoneId", pZoneID);

                try
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(pdtTable);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (pdtTable != null)
                    {
                        pdtTable.Dispose();
                    }
                }
            }

            return pdtTable;
        }

        /// <summary>
        /// /// Retrieves the SPL ticket code from the database.
        /// </summary>
        /// <returns></returns>
        public string fnSPLticketCode()
        {
            string ticketCode = "";
            string connStr = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetSPLticketCode_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add OUTPUT parameter
                        SqlParameter outputParam = new SqlParameter("@o_SPLticketCode", SqlDbType.VarChar, 1000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Retrieve the output value
                        ticketCode = outputParam.Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Log exception if needed
                    ticketCode = null;
                }
            }

            return ticketCode;
        }

        /// <summary>
        /// /// Retrieves the SPL PNR number from the database.
        /// </summary>
        /// <returns></returns>
        public string fnGetSPLpnr()
        {
            string pnrNo = "";
            string connStr = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetSPLpnr_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Define output parameter
                        SqlParameter outputParam = new SqlParameter("@o_Pnrno", SqlDbType.VarChar, 1000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Get the output value
                        pnrNo = outputParam.Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception if needed
                    pnrNo = null;
                }
            }

            return pnrNo;
        }

        /// <summary>
        /// /// Retrieves recent bookings for fixed tours based on the request source and ID.
        /// </summary>
        /// <param name="lRqstFrom"></param>
        /// <param name="lID"></param>
        /// <returns></returns>
        public DataTable fnFixed_RecentBookings(string lRqstFrom, string lID)
        {
            DataTable dtResult = new DataTable();
            string connStr = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.Fixed_RecentBookings, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        cmd.Parameters.Add(new SqlParameter("@I_RequestFrom", SqlDbType.VarChar, 10) { Value = lRqstFrom });
                        cmd.Parameters.Add(new SqlParameter("@I_ID", SqlDbType.VarChar, 15) { Value = lID });

                        // Fill DataTable
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optionally log exception
                    return null;
                }
            }

            return dtResult;
        }

        /// <summary>
        /// /// Retrieves fixed tour details based on the provided city ID.
        /// </summary>
        /// <param name="lCityID"></param>
        /// <returns></returns>
        public DataTable fnCar_FixedTourDetails(decimal? lCityID)
        {
            DataTable ldtRecSet = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.Car_FixedTourDetails_sp, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramCityId = new SqlParameter("@CityId", SqlDbType.Decimal);
                        if (lCityID.HasValue)
                            paramCityId.Value = lCityID.Value;
                        else
                            paramCityId.Value = DBNull.Value;

                        cmd.Parameters.Add(paramCityId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return null;
            }

            return ldtRecSet;
        }

        /// <summary>
        /// /// Retrieves special tour fare panel information based on the fare type, tour ID, and LTC status.
        /// </summary>
        /// <param name="i_FareType"></param>
        /// <param name="i_TourID"></param>
        /// <param name="i_IsLTC"></param>
        /// <param name="o_IsAccomodation"></param>
        /// <param name="o_ReturnValue"></param>
        /// <param name="o_TourName"></param>
        /// <param name="o_Notes"></param>
        /// <param name="o_IsQuery"></param>
        /// <returns></returns>
        public List<Get_SPL_Fare_Panel_spResult> Get_SpecialTour_FarePanel(
                    string i_FareType, int? i_TourID, bool? i_IsLTC,
                    out bool? o_IsAccomodation, out int? o_ReturnValue,
                    out string o_TourName, out string o_Notes, out bool? o_IsQuery)
        {
            var list = new List<Get_SPL_Fare_Panel_spResult>();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.Get_SpecialTour_FarePanel_NEW_sp, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input Parameters
                cmd.Parameters.AddWithValue("@I_FareType", i_FareType ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@I_TourID", i_TourID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@I_IsLTC", i_IsLTC ?? (object)DBNull.Value);

                // Output Parameters
                SqlParameter p1 = new SqlParameter("@O_IsAccomodation", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                SqlParameter p2 = new SqlParameter("@O_ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.Output };
                SqlParameter p3 = new SqlParameter("@O_TourName", SqlDbType.VarChar, 200) { Direction = ParameterDirection.Output };
                SqlParameter p4 = new SqlParameter("@O_Notes", SqlDbType.VarChar, 1000) { Direction = ParameterDirection.Output };
                SqlParameter p5 = new SqlParameter("@O_IsQuery", SqlDbType.Bit) { Direction = ParameterDirection.Output };

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new Get_SPL_Fare_Panel_spResult
                        {
                            PanelID = dr.GetInt32(dr.GetOrdinal("PanelID")),
                            FareID = dr.GetInt32(dr.GetOrdinal("FareID")),
                            CategoryName = dr["CategoryName"] as string,
                            Fare = dr["Fare"] as decimal?,
                            FromDate = dr["FromDate"] as DateTime?,
                            ToDate = dr["ToDate"] as DateTime?,
                            s_month = dr["s_month"] as string,
                            CategoryID = dr["CategoryID"] as int?,
                            Season = dr["Season"] as string,
                            SeasonID = dr["SeasonID"] as long?,
                            VehicleID = dr.GetInt32(dr.GetOrdinal("VehicleID")),
                            VehicleName = dr["VehicleName"] as string,
                            PaxID = dr.GetInt32(dr.GetOrdinal("PaxID")),
                            PaxType = dr["PaxType"] as string,
                            ParentID = dr.GetInt32(dr.GetOrdinal("ParentID")),
                            ParentCategory = dr["ParentCategory"] as string,
                            DiscountedFare = dr["DiscountedFare"] as decimal?,
                            IsAgent = Convert.ToBoolean(dr["IsAgent"]),
                            IsBranch = Convert.ToBoolean(dr["IsBranch"]),
                            IsOnline = Convert.ToBoolean(dr["IsOnline"])
                        };

                        list.Add(item);
                    }
                }

                // Get Output Values
                o_IsAccomodation = p1.Value != DBNull.Value ? (bool?)Convert.ToBoolean(p1.Value) : null;
                o_ReturnValue = p2.Value != DBNull.Value ? (int?)Convert.ToInt32(p2.Value) : null;
                o_TourName = p3.Value?.ToString();
                o_Notes = p4.Value?.ToString();
                o_IsQuery = p5.Value != DBNull.Value ? (bool?)Convert.ToBoolean(p5.Value) : null;
            }

            return list;
        }

        /// <summary>
        /// /// Retrieves the tax value based on the specified tax type.
        /// </summary>
        /// <param name="taxType"></param>
        /// <returns></returns>
        public string fnGetTAXValue(string taxType)
        {
            string taxValue = null;
            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_GetTAXValue, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TaxType", taxType);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            taxValue = reader["taxvalue"] != DBNull.Value ? reader["taxvalue"].ToString() : null;
                        }
                    }
                }
            }
            return taxValue;
        }

        /// <summary>
        /// // Retrieves special tour master information based on the provided tour ID.
        /// </summary>
        /// <param name="pOrderId"></param>
        /// <param name="pAgentID"></param>
        /// <param name="pAvaiBal"></param>
        /// <returns></returns>
        public DataTable fnGetAgentPayDetail(string pOrderId, int? pAgentID, ref string pAvaiBal)
        {
            DataTable ldtRecSet = new DataTable();
            string connStr = DataLib.getConnectionString();
            int? lStatus = 0;

            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAgentPayDetail_sp, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.AddWithValue("@OrderID", pOrderId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentID", pAgentID.HasValue ? (object)pAgentID.Value : DBNull.Value);

                // Output parameters
                SqlParameter outBal = new SqlParameter("@o_AvailableBalance", SqlDbType.VarChar, 50);
                outBal.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outBal);

                SqlParameter outStatus = new SqlParameter("@o_ReturnValue", SqlDbType.Int);
                outStatus.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outStatus);

                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ldtRecSet);

                    // Assign output value
                    pAvaiBal = outBal.Value?.ToString();
                }
                catch (Exception ex)
                {
                    // Log exception if needed
                    ldtRecSet = null;
                }
            }

            return ldtRecSet;
        }

        /// <summary>
        /// /// Retrieves the agent level commission based on the agent ID, tour number, table column, and table name.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <param name="lTourNo"></param>
        /// <param name="lTBLCol"></param>
        /// <param name="lTBLName"></param>
        /// <param name="lIsPers"></param>
        /// <returns></returns>
        public string fnGetAgentLavelComm(int? lAgentID, string lTourNo, string lTBLCol, string lTBLName, ref bool? lIsPers)
        {
            string lAgentComm = "0";

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.AgentLavelComm_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Input parameters
                        cmd.Parameters.AddWithValue("@AgentId", (object)lAgentID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TourNo", (object)lTourNo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@tblCol", (object)lTBLCol ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@tblName", (object)lTBLName ?? DBNull.Value);

                        // Output parameters
                        SqlParameter isPersParam = new SqlParameter("@ISPers", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Value = (object)lIsPers ?? DBNull.Value
                        };
                        cmd.Parameters.Add(isPersParam);

                        SqlParameter commParam = new SqlParameter("@AgntComm", SqlDbType.NVarChar, 250)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Value = lAgentComm
                        };
                        cmd.Parameters.Add(commParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Get the output values
                        if (isPersParam.Value != DBNull.Value)
                            lIsPers = Convert.ToBoolean(isPersParam.Value);

                        if (commParam.Value != DBNull.Value)
                            lAgentComm = commParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lAgentComm = "0"; // Fallback default
            }

            return lAgentComm;
        }

        /// <summary>
        /// /// Retrieves the child pax count based on the provided tour ID and date.
        /// </summary>
        /// <param name="lTourID"></param>
        /// <param name="lTourDate"></param>
        /// <param name="lCategoryID"></param>
        /// <param name="lPaxID"></param>
        /// <param name="o_ReturnValue"></param>
        /// <returns></returns>
        public DataTable fnGetSpecialTourFare(int? lTourID, DateTime? lTourDate, int? lCategoryID, int? lPaxID, out int? o_ReturnValue)
        {
            o_ReturnValue = null;
            DataTable dtTable = new DataTable();

            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetSpecialTourFare_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.AddWithValue("@I_TourID", (object)lTourID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@I_TourDate", (object)lTourDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@I_CategoryID", (object)lCategoryID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@I_PaxID", (object)lPaxID ?? DBNull.Value);

                // Output parameter
                SqlParameter outParam = new SqlParameter("@O_ReturnValue", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                try
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtTable);
                    }

                    // Read output parameter value
                    if (outParam.Value != DBNull.Value)
                        o_ReturnValue = Convert.ToInt32(outParam.Value);

                    return dtTable;
                }
                catch (Exception)
                {
                    // Handle or log exception as needed
                    return null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves the list of countries based on the specified region ID.
        /// </summary>
        /// <param name="lRegionId"></param>
        /// <returns></returns>
        public DataTable fnGetCountry(int? lRegionId)
        {
            DataTable dtTable = new DataTable();
            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetInternational_Country_SP, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add input parameter with null handling
                cmd.Parameters.AddWithValue("@I_RegionId", (object)lRegionId ?? DBNull.Value);

                try
                {
                    conn.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtTable);
                    }

                    return dtTable;
                }
                catch (Exception)
                {
                    // Handle/log exception if needed
                    return null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves whether service tax is applicable for the specified tour number.
        /// </summary>
        /// <param name="lTourNo"></param>
        /// <returns></returns>
        public string fnGetServiceTaxIsAcc(int? lTourNo)
        {
            string connectionString = DataLib.getConnectionString();
            string lTaxValue = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetServiceTaxIsAcc_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameter
                cmd.Parameters.AddWithValue("@i_TourNo", (object)lTourNo ?? DBNull.Value);

                // Output parameter
                SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Read the output value
                    lTaxValue = outputParam.Value?.ToString();
                    return lTaxValue;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves the service tax value based on the specified tax type.
        /// </summary>
        /// <param name="pTaxType"></param>
        /// <returns></returns>
        public decimal fnGetServiceTaxValue(string pTaxType)
        {
            string connectionString = DataLib.getConnectionString();
            decimal lTaxValue = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetServiceTaxValue_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameter
                cmd.Parameters.AddWithValue("@I_TaxType", pTaxType ?? (object)DBNull.Value);

                // Output parameter
                SqlParameter outputParam = new SqlParameter("@I_OReturnValue", SqlDbType.Float)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Read the output value
                    if (outputParam.Value != DBNull.Value)
                    {
                        lTaxValue = Convert.ToDecimal(outputParam.Value);
                    }

                    return lTaxValue;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// /// Retrieves the LTC charges from the database.
        /// </summary>
        /// <returns></returns>
        public string fnGetLTCCharges()
        {
            string connectionString = DataLib.getConnectionString();
            decimal lLTCCharges = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetLTCCharges_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Output parameter
                SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.Decimal)
                {
                    Precision = 18, // adjust based on your DB definition
                    Scale = 2,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (outputParam.Value != DBNull.Value)
                        lLTCCharges = Convert.ToDecimal(outputParam.Value);

                    return lLTCCharges.ToString();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves a new order ID from the database.
        /// </summary>
        /// <returns></returns>
        public string fnGetOrderID()
        {
            string connectionString = DataLib.getConnectionString();
            string lTaxValue = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetOrderID_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Output parameter
                SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Read the output value
                    lTaxValue = outputParam.Value?.ToString();
                    return lTaxValue;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves seat arrangement details based on the provided tour serial number.
        /// </summary>
        /// <param name="lTourSrNo"></param>
        /// <returns></returns>
        public DataSet fnGetSeatArrangementDetail(long? lTourSrNo)
        {
            DataSet pdtDTSet = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
            new SqlParameter("@tourserial", SqlDbType.BigInt)
            {
                Value = (object)lTourSrNo ?? DBNull.Value
            }
                };

                pdtDTSet = SqlData.GetDataSetSP(StoredProcedures.sp_GetSeatArrangement, param);

                return pdtDTSet;
            }
            catch (Exception ex)
            {
                // Optionally log the exception (e.g., to file, event log, or database)
                return null;
            }
            finally
            {
                if (pdtDTSet != null)
                {
                    pdtDTSet.Dispose();
                    pdtDTSet = null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves pickup details based on the provided row ID.
        /// </summary>
        /// <param name="lRowID"></param>
        /// <returns></returns>
        public DataTable fnGetPickupDetail(int? lRowID)
        {
            DataTable ldtRecSet = new DataTable();

            string connectionString = DataLib.getConnectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetPickupDetail_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@RowID", SqlDbType.Int)
                {
                    Value = (object)lRowID ?? DBNull.Value
                });

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ldtRecSet.Load(reader);
                    }

                    return ldtRecSet;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (ldtRecSet != null)
                    {
                        ldtRecSet.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// /// Retrieves fixed tour fare details based on the provided tour number, date, LTC status, and request source.
        /// </summary>
        /// <param name="lTourNo"></param>
        /// <param name="lTourDate"></param>
        /// <param name="lisLTC"></param>
        /// <param name="lReqFrom"></param>
        /// <returns></returns>
        public DataSet fnGetFixedTour_Fare(int? lTourNo, DateTime? lTourDate, char? lisLTC, string lReqFrom)
        {
            DataSet pdtDTSet = new DataSet();
            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.Get_FixedTour_Fare, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TourNo", (object)lTourNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TourDate", (object)lTourDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsLTC", (object)lisLTC ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RequestFrom", (object)lReqFrom ?? DBNull.Value);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int tableIndex = 0;
                        do
                        {
                            DataTable table = new DataTable("Table" + (++tableIndex));
                            table.Load(reader);
                            pdtDTSet.Tables.Add(table);
                        } while (!reader.IsClosed && reader.NextResult());
                    }

                    return pdtDTSet;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (pdtDTSet != null)
                    {
                        pdtDTSet.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// /// Retrieves vacant seats for a specific tour and date.
        /// </summary>
        /// <param name="lTourNo"></param>
        /// <param name="lTourDate"></param>
        /// <returns></returns>
        public DataSet fnGetjdates_vacantseats(int? lTourNo, int? lTourDate)
        {
            DataSet pdsTable = new DataSet();
            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.jdates_vacantseats, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tourno", (object)lTourNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@jdate", (object)lTourDate ?? DBNull.Value);

                try
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(pdsTable);
                    }

                    return pdsTable;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (pdsTable != null)
                    {
                        pdsTable.Dispose();
                        pdsTable = null;
                    }
                }
            }
        }


        /// <summary>
        /// /// Retrieves pickup address and departure time for a specific tour number.
        /// </summary>
        /// <param name="lTourNo"></param>
        /// <returns></returns>
        public DataTable fnFixed_PickupAddr_DeptTime(int? lTourNo)
        {
            DataTable pdtTable = new DataTable();
            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.Fixed_PickupAddr_DeptTime, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tourno", (object)lTourNo ?? DBNull.Value);

                try
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(pdtTable);
                    }

                    return pdtTable;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (pdtTable != null)
                    {
                        pdtTable.Dispose();
                        pdtTable = null;
                    }
                }
            }
        }


        /// <summary>
        /// /// Retrieves the default pickup address for a specific tour number.
        /// </summary>
        /// <param name="lTourNo"></param>
        /// <returns></returns>
        public DataSet fnFixed_Default_PickupAddress(int? lTourNo)
        {
            DataSet pdsTable = new DataSet();
            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.Fixed_Default_PickupAddress, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tourno", (object)lTourNo ?? DBNull.Value);

                try
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(pdsTable);
                    }

                    return pdsTable;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (pdsTable != null)
                    {
                        pdsTable.Dispose();
                        pdsTable = null;
                    }
                }
            }
        }


        /// <summary>
        /// /// Inserts or updates a pickup point in the database.
        /// </summary>
        /// <param name="lRowID"></param>
        /// <param name="lTourNo"></param>
        /// <param name="lPckUpPlace"></param>
        /// <param name="lstrArrHrs"></param>
        /// <param name="lstrDroHrs"></param>
        /// <param name="lActive"></param>
        /// <param name="lBCode"></param>
        /// <param name="lAddress"></param>
        /// <returns></returns>
        public int fnInsertUpdatedispup(int? lRowID, int? lTourNo, string lPckUpPlace, string lstrArrHrs, string lstrDroHrs, char lActive,
    string lBCode, string lAddress)
        {
            int lStatus = 0;
            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.dispup_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@rowId", (object)(short?)lRowID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tourno", (object)(short?)lTourNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PickPlace", (object)lPckUpPlace ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@strarrHrs", (object)lstrArrHrs ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@strDropHrs", (object)lstrDroHrs ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@active", (object)lActive);
                cmd.Parameters.AddWithValue("@bcode", (object)lBCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@strAddress", (object)lAddress ?? DBNull.Value);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lStatus = 1; // Success
                }
                catch (Exception)
                {
                    lStatus = -1; // Failure
                }
            }

            return lStatus;
        }


        /// <summary>
        /// /// Retrieves multiple pickup points for a specific tour number.
        /// </summary>
        /// <param name="lTourNo"></param>
        /// <returns></returns>
        public DataTable fnGetMultiplePickupPoint(int? lTourNo)
        {
            DataTable ldtRecSet = new DataTable();
            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetMultiplePickupPoint_sp, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add input parameter
                cmd.Parameters.AddWithValue("@TourNo", (object)lTourNo ?? DBNull.Value);

                try
                {
                    conn.Open();
                    adapter.Fill(ldtRecSet); // Fills the DataTable with the result
                    return ldtRecSet;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (ldtRecSet != null)
                    {
                        ldtRecSet.Dispose();
                        ldtRecSet = null;
                    }
                }
            }
        }

        /// <summary>
        /// /// Retrieves the seat arrangement for a specific tour serial number.
        /// </summary>
        /// <param name="pTourSrNo"></param>
        /// <returns></returns>
        public DataTable fnGetSeatArrangement(decimal? pTourSrNo)
        {
            DataTable ldtRecSet = new DataTable();
            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.SeatArrangement_sp, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add input parameter
                cmd.Parameters.AddWithValue("@i_TourSerial", (object)pTourSrNo ?? DBNull.Value);

                try
                {
                    conn.Open();
                    adapter.Fill(ldtRecSet); // Fill result into DataTable
                    return ldtRecSet;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (ldtRecSet != null)
                    {
                        ldtRecSet.Dispose();
                        ldtRecSet = null;
                    }
                }
            }
        }

        /// <summary>
        /// /// Blocks or unblocks seats for a specific tour based on the provided parameters.
        /// </summary>
        /// <param name="lTourSrNo"></param>
        /// <param name="lSeats"></param>
        /// <param name="lBlkString"></param>
        /// <param name="lBklOrUnBlh"></param>
        /// <returns></returns>
        public int fnBlockUnBlockSeats_sp(string lTourSrNo, string lSeats, string lBlkString, bool? lBklOrUnBlh)
        {
            int status = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.BlockUnBlockSeats_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TSerial", lTourSrNo);
                cmd.Parameters.AddWithValue("@Seat", lSeats);
                cmd.Parameters.AddWithValue("@BlockedString", lBlkString);
                cmd.Parameters.AddWithValue("@flag", (object)lBklOrUnBlh ?? DBNull.Value);

                SqlParameter returnParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
                returnParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(returnParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    status = (returnParam.Value != DBNull.Value) ? Convert.ToInt32(returnParam.Value) : 0;
                }
                catch (Exception ex)
                {
                    status = -1;
                    // Log or rethrow if needed
                }
            }

            return status;
        }

        /// <summary>
        /// /// Retrieves the pickup master row ID based on the provided tour number, section, and row ID.
        /// </summary>
        /// <param name="lTourNo"></param>
        /// <param name="pFromSection"></param>
        /// <param name="pROwID"></param>
        /// <returns></returns>
        public int fnGetPickUpMAsterRowID(int? lTourNo, string pFromSection, int? pROwID)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetPickUpMAsterRowID_sp, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@i_TourNo", lTourNo.HasValue ? (object)lTourNo.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@I_FromSection", string.IsNullOrEmpty(pFromSection) ? (object)DBNull.Value : pFromSection);
                        cmd.Parameters.AddWithValue("@I_RowID", pROwID.HasValue ? (object)pROwID.Value : DBNull.Value);

                        SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        if (outputParam.Value != DBNull.Value)
                        {
                            result = Convert.ToInt32(outputParam.Value);
                        }
                    }
                }
                catch (Exception)
                {
                    result = 0;
                }
            }

            return result;
        }

        /// <summary>
        /// /// Retrieves bus allotment details based on the provided tour serial number, bus environment type, and bus number.
        /// </summary>
        /// <param name="lTourSr"></param>
        /// <param name="lBusEnv"></param>
        /// <param name="lBusNo"></param>
        /// <returns></returns>
        public DataTable fnGetBusAllot_Detail(string lTourSr, string lBusEnv, string lBusNo)
        {
            DataTable pdtTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetBusAllot_Detail_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@i_TourSerial", lTourSr);
                cmd.Parameters.AddWithValue("@i_BusEnvType", lBusEnv);
                cmd.Parameters.AddWithValue("@i_Busno", lBusNo);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        pdtTable.Load(reader); // Load the first result set into DataTable
                    }
                }
                catch (Exception ex)
                {
                    // Log exception if needed
                    return null;
                }
            }

            return pdtTable;
        }

        /// <summary>
        /// /// Retrieves the number of seats available for a specific tour and date.
        /// </summary>
        /// <param name="tourNo"></param>
        /// <param name="tourDate"></param>
        /// <returns></returns>
        public DataSet fnGetNoOfSeats_ADO(int tourNo, string tourDate)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_NoOfSeats, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tourNo", tourNo);
                    cmd.Parameters.AddWithValue("@tDate", tourDate);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// /// Retrieves the number of seats available for a specific tour and date.
        /// </summary>
        /// <param name="tourNo"></param>
        /// <param name="tourDate"></param>
        /// <returns></returns>
        public DataSet fnGetNoOfSeats(int tourNo, string tourDate)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_NoOfSeats, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tourNo", tourNo);
                    cmd.Parameters.AddWithValue("@tDate", tourDate);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// /// Checks the bus type for a specific tour and journey date.
        /// </summary>
        /// <param name="JDate"></param>
        /// <param name="lTourID"></param>
        /// <returns></returns>
        public string fnChkBusTypeAgent(DateTime? JDate, int? lTourID)
        {
            string lBusType = "0";
            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.ChkBusTypeAgent_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.AddWithValue("@i_TourNo", (object)lTourID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@i_JourneyDate", (object)JDate ?? DBNull.Value);

                // Output parameter
                SqlParameter outputParam = new SqlParameter("@o_BusType", SqlDbType.VarChar, 10)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return Convert.ToString(outputParam.Value);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// /// Validates the discount agent based on the journey date and tour ID.
        /// </summary>
        /// <param name="JDate"></param>
        /// <param name="lTourID"></param>
        /// <returns></returns>
        public string fnValidateDiscountAgent(DateTime? JDate, int? lTourID)
        {
            string connectionString = DataLib.getConnectionString();
            int? lStatus = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.ValidateDiscountAgent_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.AddWithValue("@i_JourneyDate", (object)JDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@i_TourId", (object)lTourID ?? DBNull.Value);

                // Output parameter
                SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lStatus = (int?)outputParam.Value;
                    return Convert.ToString(lStatus);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves the tour serial number for a specific journey date, hour, and tour number.
        /// </summary>
        /// <param name="lJDate"></param>
        /// <param name="lHour"></param>
        /// <param name="lTourNo"></param>
        /// <returns></returns>
        public DataTable fnFixed_TourSerial(DateTime? lJDate, string lHour, int? lTourNo)
        {
            string connectionString = DataLib.getConnectionString();
            DataTable pdtTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.Fixed_TourSerial, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.AddWithValue("@Jdate", (object)lJDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@hr", (object)lHour ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tourno", (object)lTourNo ?? DBNull.Value);

                try
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(pdtTable);
                    }

                    return pdtTable;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (pdtTable != null)
                    {
                        pdtTable.Dispose();
                        pdtTable = null;
                    }
                }
            }
        }

        /// <summary>
        /// /// Inserts a new booking for an agent into the database.
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public int fnInsertbookAgent(OnlineToursBooking T)
        {
            int status = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertBookingAgent_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add all parameters (match order and type from SP definition)
                cmd.Parameters.AddWithValue("@orderid", T.orderid);
                cmd.Parameters.AddWithValue("@tourno", T.tourid);
                cmd.Parameters.AddWithValue("@doj", T.doj);
                cmd.Parameters.AddWithValue("@dob", T.dob);
                cmd.Parameters.AddWithValue("@env", T.BusEnvType);
                cmd.Parameters.AddWithValue("@adults", T.NoofAdults);
                cmd.Parameters.AddWithValue("@child", T.NoofChild);
                cmd.Parameters.AddWithValue("@adultstwin", T.NoofAdultsTwin);
                cmd.Parameters.AddWithValue("@adultstriple", T.NoofAdultsTriple);
                cmd.Parameters.AddWithValue("@childbed", T.ChildWithoutbed);
                cmd.Parameters.AddWithValue("@singleadults", T.SingleAdult);
                cmd.Parameters.AddWithValue("@TourName", T.TourName);
                cmd.Parameters.AddWithValue("@amt", T.Amount);
                cmd.Parameters.AddWithValue("@tax", T.Tax);
                cmd.Parameters.AddWithValue("@taxamt", T.CalcTaxvalue);
                cmd.Parameters.AddWithValue("@tot", T.TotalAmount);
                cmd.Parameters.AddWithValue("@tempstr", T.SeatNo);
                cmd.Parameters.AddWithValue("@BusserialNo", T.BusSerialNo);
                cmd.Parameters.AddWithValue("@TourSerial", T.TourSerial);
                cmd.Parameters.AddWithValue("@pkpID", T.PickUppointid);
                cmd.Parameters.AddWithValue("@afare", T.adultfare);
                cmd.Parameters.AddWithValue("@cfare", T.childfare);
                cmd.Parameters.AddWithValue("@a2fare", T.adultstwinfare);
                cmd.Parameters.AddWithValue("@a3fare", T.adultstriplefare);
                cmd.Parameters.AddWithValue("@cbfare", T.childwithoutbedfare);
                cmd.Parameters.AddWithValue("@safare", T.singleadultfare);
                cmd.Parameters.AddWithValue("@ccfee", T.CreditCardFee);
                cmd.Parameters.AddWithValue("@ccamt", T.CalcCreditCardFee);
                cmd.Parameters.AddWithValue("@dormitory", T.dormitory);
                cmd.Parameters.AddWithValue("@dormitoryfare", T.dormitoryfare);
                cmd.Parameters.AddWithValue("@rmarks", T.Remarks);
                cmd.Parameters.AddWithValue("@OnLineDis", T.OnLineDis);
                cmd.Parameters.AddWithValue("@noAdultWithFood", T.noAdultWithFood);
                cmd.Parameters.AddWithValue("@noChildWithFood", T.noChildWithFood);
                cmd.Parameters.AddWithValue("@AdultWithFoodFare", T.AdultWithFoodFare);
                cmd.Parameters.AddWithValue("@ChildWithFoodFare", T.ChildWithFoodFare);
                cmd.Parameters.AddWithValue("@Advancepaid", T.AdvancePaid);
                cmd.Parameters.AddWithValue("@IsPartialPaymentByAgent", T.IsPartialPaymentByAgent);
                cmd.Parameters.AddWithValue("@AdultServiceCharge", T.AdultServiceCharges);
                cmd.Parameters.AddWithValue("@ChildServiceCharge", T.ChildServiceCharges);
                cmd.Parameters.AddWithValue("@ServiceChargesTotal", T.ServiceChargesTotal);
                cmd.Parameters.AddWithValue("@ServiceChargesTax", T.ServiceChargesTax);
                cmd.Parameters.AddWithValue("@ServiceChargesTaxVal", T.ServiceChargesTaxVal);

                // Add Output parameter
                SqlParameter returnParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(returnParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    status = Convert.ToInt32(returnParam.Value);
                }
                catch (Exception ex)
                {
                    status = -1;
                    // Optionally log exception
                }
            }

            return status;
        }

        /// <summary>
        /// /// Updates an existing booking for an agent in the database.
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public int fnUpdatebookAgent(OnlineToursBooking T)
        {
            int lStatus = 0;

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateBookingAgent_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@jdate", (object)T.doj ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@env", (object)T.BusEnvType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@adults", (object)T.NoofAdults ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@child", (object)T.NoofChild ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@adultstwin", (object)T.NoofAdultsTwin ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@adultstriple", (object)T.NoofAdultsTriple ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@childbed", (object)T.ChildWithoutbed ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@singleadult", (object)T.SingleAdult ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@amt", (object)T.Amount ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tax", (object)T.Tax ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@taxamt", (object)T.CalcTaxvalue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tot", (object)T.TotalAmount ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tempstr", (object)T.SeatNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BusserialNo", (object)T.BusSerialNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TourSerial", (object)T.TourSerial ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@pkpID", (object)T.PickUppointid ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@afare", (object)T.adultfare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@cfare", (object)T.childfare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@a2fare", (object)T.adultstwinfare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@a3fare", (object)T.adultstriplefare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@cbfare", (object)T.childwithoutbedfare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@safare", (object)T.singleadultfare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ccfee", (object)T.CreditCardFee ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ccamt", (object)T.CalcCreditCardFee ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Rowid", (object)T.rowid ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dormitory", (object)T.dormitory ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dormitoryfare", (object)T.dormitoryfare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@noAdultWithFood", (object)T.noAdultWithFood ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@noChildWithFood", (object)T.noChildWithFood ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AdultWithFoodFare", (object)T.AdultWithFoodFare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ChildWithFoodFare", (object)T.ChildWithFoodFare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@rmarks", (object)T.Remarks ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Advancepaid", (object)T.AdvancePaid ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsPartialPaymentByAgent", (object)T.IsPartialPaymentByAgent ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AdultServiceCharge", (object)T.AdultServiceCharges ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ChildServiceCharge", (object)T.ChildServiceCharges ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ServiceChargesTotal", (object)T.ServiceChargesTotal ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ServiceChargesTax", (object)T.ServiceChargesTax ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ServiceChargesTaxVal", (object)T.ServiceChargesTaxVal ?? DBNull.Value);

                    SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        lStatus = Convert.ToInt32(outputParam.Value);
                    }
                    catch (Exception)
                    {
                        lStatus = -1;
                    }
                }
            }

            return lStatus;
        }


        /// <summary>
        /// /// Retrieves special tour fare based on the provided tour ID, date, category ID, and pax ID.
        /// </summary>
        /// <param name="lTourID"></param>
        /// <param name="lTourDate"></param>
        /// <param name="lCategoryID"></param>
        /// <param name="lPaxID"></param>
        /// <returns></returns>
        public DataTable fnGetSpecialTourFare(int? lTourID, DateTime? lTourDate, int? lCategoryID, int? lPaxID)
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetSpecialTourFare_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    cmd.Parameters.AddWithValue("@I_TourID", (object)lTourID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_TourDate", (object)lTourDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_CategoryID", (object)lCategoryID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_PaxID", (object)lPaxID ?? DBNull.Value);

                    // Output Parameter
                    SqlParameter outParam = new SqlParameter("@O_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outParam);

                    try
                    {
                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtResult);
                        }

                        // Optional: Retrieve the output value if needed
                        int returnValue = (outParam.Value != DBNull.Value) ? Convert.ToInt32(outParam.Value) : 0;

                        return dtResult;
                    }
                    catch (Exception ex)
                    {
                        // Log exception as needed
                        return null;
                    }
                    finally
                    {
                        dtResult.Dispose();
                    }
                }
            }
        }


        /// <summary>
        /// /// Retrieves car details based on the provided city ID, transfer ID, sub-transfer ID, and company ID.
        /// </summary>
        /// <param name="lCityId"></param>
        /// <param name="lTransferId"></param>
        /// <param name="lSubTransferId"></param>
        /// <param name="lCompanyId"></param>
        /// <returns></returns>
        public DataTable fnGetCar_Local(int? lCityId, int? lTransferId, int? lSubTransferId, int lCompanyId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_GetCar_Local, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cityid", (object)lCityId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@transferid", (object)lTransferId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@subtransferid", (object)lSubTransferId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CompanyID", lCompanyId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            // Log error if needed
                            return null;
                        }
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// /// Retrieves fixed car details based on the provided tour ID.
        /// </summary>
        /// <param name="lTourId"></param>
        /// <returns></returns>
        public DataTable fnGetCar_fixed(int? lTourId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_GetCar_fixed, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TourID", (object)lTourId ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            // Optional: Log the error
                            return null;
                        }
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// /// Retrieves city details based on the provided city ID.
        /// </summary>
        /// <param name="lCityId"></param>
        /// <returns></returns>
        public DataTable fnGetFind_City(int? lCityId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetFind_City__sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@i_CityID", (object)lCityId ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            // Optionally log the error
                            return null;
                        }
                    }
                }
            }

            return dt;
        }


        /// <summary>
        /// /// Books a cab for an agent based on the provided parameters.
        /// </summary>
        /// <param name="cityid"></param>
        /// <param name="fareId"></param>
        /// <param name="transferId"></param>
        /// <param name="noOfPax"></param>
        /// <param name="farePerCar"></param>
        /// <param name="noOfCars"></param>
        /// <param name="pickTupTime"></param>
        /// <param name="pickupAddress"></param>
        /// <param name="dropOffAddress"></param>
        /// <param name="emailId"></param>
        /// <param name="fare"></param>
        /// <param name="sTax"></param>
        /// <param name="cC"></param>
        /// <param name="netTotal"></param>
        /// <param name="payMode"></param>
        /// <param name="discount"></param>
        /// <param name="advance"></param>
        /// <param name="tourCategoty"></param>
        /// <param name="isExist"></param>
        /// <param name="tourName"></param>
        /// <param name="carName"></param>
        /// <param name="perExtraKMFare"></param>
        /// <param name="perExtraHRFare"></param>
        /// <param name="agentId"></param>
        /// <param name="duration"></param>
        /// <param name="originCity"></param>
        /// <param name="returnStatus"></param>
        /// <param name="cabId"></param>
        /// <returns></returns>
        public int CabBookingAgent(int? cityid, int? fareId, int? transferId, int? noOfPax, decimal? farePerCar, int? noOfCars, DateTime? pickTupTime, string pickupAddress,
    string dropOffAddress, string emailId, decimal? fare, decimal? sTax, decimal? cC, decimal? netTotal, string payMode, decimal? discount, decimal? advance,
    string tourCategoty, bool isExist, string tourName, string carName, string perExtraKMFare, string perExtraHRFare, int? agentId, string duration, string originCity,
    ref int? returnStatus, ref string cabId)
        {
            int status = 0;

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.CabBookingAgent, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cityid", (object)cityid ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FareId", (object)fareId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TransferId", (object)transferId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NoOfPax", (object)noOfPax ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FarePerCar", (object)farePerCar ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NoOfCars", (object)noOfCars ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PickTupTime", (object)pickTupTime ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PickupAddress", (object)pickupAddress ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DropOffAddress", (object)dropOffAddress ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EmailId", (object)emailId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Fare", (object)fare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@STax", (object)sTax ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CC", (object)cC ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NetTotal", (object)netTotal ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PayMode", (object)payMode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Discount", (object)discount ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Advance", (object)advance ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TourCategoty", (object)tourCategoty ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsExist", isExist);
                    cmd.Parameters.AddWithValue("@TourName", (object)tourName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CarName", (object)carName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PerExtraKMFare", (object)perExtraKMFare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PerExtraHRFare", (object)perExtraHRFare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentId", (object)agentId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@duration", (object)duration ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@OriginCity", (object)originCity ?? DBNull.Value);

                    // Output parameters
                    SqlParameter outReturnStatus = new SqlParameter("@ReturnStatus", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outReturnStatus);

                    SqlParameter outCabId = new SqlParameter("@CabId", SqlDbType.VarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outCabId);

                    try
                    {
                        con.Open();
                        status = cmd.ExecuteNonQuery();

                        // Read output values
                        returnStatus = outReturnStatus.Value != DBNull.Value ? (int?)Convert.ToInt32(outReturnStatus.Value) : null;
                        cabId = outCabId.Value?.ToString();
                    }
                    catch (Exception ex)
                    {
                        status = -2;
                        returnStatus = -2;
                        cabId = null;
                        // Optional: log exception
                    }
                }
            }

            return status;
        }

        /// <summary>
        /// /// Retrieves local transfer details based on the provided fare ID.
        /// </summary>
        /// <param name="lFareId"></param>
        /// <returns></returns>
        public DataTable fnCar_LocalTransfer(int? lFareId)
        {
            DataTable ldtUserRoles = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_Car_LocalTransfer, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FareId", (object)lFareId ?? DBNull.Value);

                    try
                    {
                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtUserRoles);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optional: log the error
                        return null;
                    }
                }
            }

            return ldtUserRoles;
        }
        public DataTable fnCar_GetFixedTour(int? lFareId)
        {
            DataTable ldtUserRoles = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_Car_GetFixedTour, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FareId", (object)lFareId ?? DBNull.Value);

                    try
                    {
                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtUserRoles);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optionally log the error
                        return null;
                    }
                }
            }

            return ldtUserRoles;
        }

        /// <summary>
        /// /// Retrieves local sub-transfer details based on the provided fare ID.
        /// </summary>
        /// <param name="lFareId"></param>
        /// <returns></returns>
        public DataTable fnCar_LocalSubTransfer(int? lFareId)
        {
            DataTable ldtUserRoles = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_Car_LocalSubTransfer, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter and handle null
                    cmd.Parameters.AddWithValue("@FareId", (object)lFareId ?? DBNull.Value);

                    try
                    {
                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtUserRoles);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log if needed
                        return null;
                    }
                }
            }

            return ldtUserRoles;
        }

        /// <summary>
        /// /// Retrieves the list of states from the database.
        /// </summary>
        /// <returns></returns>
        public DataTable fnGetState()
        {
            DataTable dtState = new DataTable();
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                string connStr = DataLib.getConnectionString(); // Your connection string
                con = new SqlConnection(connStr);
                cmd = new SqlCommand(StoredProcedures.sp_GetState, con);
                cmd.CommandType = CommandType.StoredProcedure;

                da = new SqlDataAdapter(cmd);
                da.Fill(dtState);

                return dtState;
            }
            catch (Exception ex)
            {
                // Optional: log exception
                return null;
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                {
                    if (con.State != ConnectionState.Closed)
                        con.Close();
                    con.Dispose();
                }
                if (dtState != null)
                    dtState.Dispose(); // Optional: only if you don’t return it
            }
        }

        /// <summary>
        /// /// Inserts a new group booking request into the database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int fnInsertGroup_GroupBookingRequest(Group_GroupBookingRequest request)
        {
            int status = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.insert_Group_GroupBookingRequest, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    cmd.Parameters.AddWithValue("@TourName", (object)request.TourName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TourId", (object)request.TourId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GroupLeaderName", (object)request.GroupLeaderName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NoofAdults", (object)request.NoofAdults ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NoofChilds", (object)request.NoofChilds ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BusType", (object)request.BusType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DepartureDate", (object)request.DepartureDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ArrivalDate", (object)request.ArrivalDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Accommodation", (object)request.Accommodation ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Food", (object)request.Food ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object)request.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", (object)request.City ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@State", (object)request.State ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PinCode", (object)request.PinCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PhoneNo", (object)request.PhoneNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MobileNo", (object)request.MobileNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Emailid", (object)request.Emailid ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentId", (object)request.AgentId ?? DBNull.Value);

                    // Add output parameter
                    SqlParameter outputParam = new SqlParameter("@O_Id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    // Open and execute
                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Read the output parameter
                    status = Convert.ToInt32(outputParam.Value);
                }
            }
            catch (Exception ex)
            {
                // Handle/log the exception as needed
                status = -1; // You can define custom error codes
            }

            return status;
        }

        /// <summary>
        /// /// Inserts a new payment record for HDFC PG into the database.
        /// </summary>
        /// <param name="lPaymentID"></param>
        /// <param name="lOrderID"></param>
        /// <param name="lEmailId"></param>
        /// <param name="lAuth"></param>
        /// <param name="lAmount"></param>
        /// <param name="lRef"></param>
        /// <param name="lTranID"></param>
        /// <param name="lTrackID"></param>
        /// <param name="lPostDate"></param>
        /// <param name="lResult"></param>
        /// <param name="lErrorText"></param>
        /// <param name="lUDF1"></param>
        /// <param name="lUDF2"></param>
        /// <param name="lUDF3"></param>
        /// <param name="lUDF4"></param>
        /// <param name="lUDF5"></param>
        /// <param name="lUDF6"></param>
        /// <param name="lSectionName"></param>
        /// <returns></returns>
        public int fnInsertPaymentHDFCPG(
 string lPaymentID, string lOrderID, string lEmailId, string lAuth,
 decimal lAmount, string lRef, string lTranID, string lTrackID, string lPostDate,
 string lResult, string lErrorText, string lUDF1, string lUDF2, string lUDF3,
 string lUDF4, string lUDF5, string lUDF6, string lSectionName)
        {
            int returnValue = 0;

            string connStr = DataLib.getConnectionString(); // Your method for connection string

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertPaymentHDFCPG_SP, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input Parameters
                cmd.Parameters.AddWithValue("@I_PaymentID", lPaymentID);
                cmd.Parameters.AddWithValue("@I_OrderID", lOrderID);
                cmd.Parameters.AddWithValue("@I_EmailID", lEmailId);
                cmd.Parameters.AddWithValue("@I_Auth", lAuth);
                cmd.Parameters.AddWithValue("@I_Amount", lAmount);
                cmd.Parameters.AddWithValue("@I_Ref", lRef);
                cmd.Parameters.AddWithValue("@I_TranID", lTranID);
                cmd.Parameters.AddWithValue("@I_TrackID", lTrackID);
                cmd.Parameters.AddWithValue("@I_PostDate", lPostDate);
                cmd.Parameters.AddWithValue("@I_Result", lResult);
                cmd.Parameters.AddWithValue("@I_ErrorText", lErrorText);
                cmd.Parameters.AddWithValue("@I_Udf1", lUDF1);
                cmd.Parameters.AddWithValue("@I_Udf2", lUDF2);
                cmd.Parameters.AddWithValue("@I_Udf3", lUDF3);
                cmd.Parameters.AddWithValue("@I_Udf4", lUDF4);
                cmd.Parameters.AddWithValue("@I_Udf5", lUDF5);
                cmd.Parameters.AddWithValue("@I_Udf6", lUDF6);
                cmd.Parameters.AddWithValue("@I_SectionName", lSectionName);

                // Output parameter
                SqlParameter outputParam = new SqlParameter("@I_RetrunValue", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    returnValue = Convert.ToInt32(outputParam.Value);
                }
                catch (Exception ex)
                {
                    // Optionally log the error
                    returnValue = -2;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// /// Inserts a new payment detail record into the database.
        /// </summary>
        /// <param name="lOrderID"></param>
        /// <param name="lItemCode"></param>
        /// <param name="lAmount"></param>
        /// <param name="lBankName"></param>
        /// <param name="lIsPaid"></param>
        /// <param name="lGatewayBID"></param>
        /// <param name="lOrderDetail"></param>
        /// <param name="lCurrency"></param>
        /// <param name="lPayMode"></param>
        /// <param name="lCcChargeAmt"></param>
        /// <param name="lIsHDFC"></param>
        /// <param name="lIP"></param>
        /// <param name="lTotalAmt"></param>
        /// <param name="EMIMonth"></param>
        /// <param name="SectionName"></param>
        /// <returns></returns>
        public int fninsert_tbl_PaymentDetails(string lOrderID, string lItemCode, decimal? lAmount, string lBankName, char? lIsPaid, string lGatewayBID,
  string lOrderDetail, string lCurrency, string lPayMode, decimal lCcChargeAmt, bool? lIsHDFC, string lIP, decimal? lTotalAmt, string EMIMonth, string SectionName)
        {
            int result = -1;

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.insert_tbl_PaymentDetails, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@orderId", string.IsNullOrEmpty(lOrderID) ? (object)DBNull.Value : lOrderID);
                        cmd.Parameters.AddWithValue("@itemCode", string.IsNullOrEmpty(lItemCode) ? (object)DBNull.Value : lItemCode);
                        cmd.Parameters.AddWithValue("@Amount", lAmount.HasValue ? (object)lAmount.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@BankName", string.IsNullOrEmpty(lBankName) ? (object)DBNull.Value : lBankName);
                        cmd.Parameters.AddWithValue("@IsPaid", lIsPaid.HasValue ? (object)lIsPaid.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@gatewayBID", string.IsNullOrEmpty(lGatewayBID) ? (object)DBNull.Value : lGatewayBID);
                        cmd.Parameters.AddWithValue("@orderDetails", string.IsNullOrEmpty(lOrderDetail) ? (object)DBNull.Value : lOrderDetail);
                        cmd.Parameters.AddWithValue("@currency", string.IsNullOrEmpty(lCurrency) ? (object)DBNull.Value : lCurrency);
                        cmd.Parameters.AddWithValue("@payMode", string.IsNullOrEmpty(lPayMode) ? (object)DBNull.Value : lPayMode);
                        cmd.Parameters.AddWithValue("@ccChargeAmt", lCcChargeAmt); // assuming always passed
                        cmd.Parameters.AddWithValue("@isHDFC", lIsHDFC.HasValue ? (object)lIsHDFC.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@ip", string.IsNullOrEmpty(lIP) ? (object)DBNull.Value : lIP);
                        cmd.Parameters.AddWithValue("@EMIMonth", string.IsNullOrEmpty(EMIMonth) ? (object)DBNull.Value : EMIMonth);
                        cmd.Parameters.AddWithValue("@SectionName", string.IsNullOrEmpty(SectionName) ? (object)DBNull.Value : SectionName);
                        cmd.Parameters.AddWithValue("@TotalAmount", lTotalAmt.HasValue ? (object)lTotalAmt.Value : DBNull.Value);

                        con.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    result = -1;
                }
            }

            return result;
        }

        /// <summary>
        /// /// Retrieves the current CAB PNR from the database.
        /// </summary>
        /// <returns></returns>
        public string fnGetCABpnr()
        {
            string lReturnValue = "";

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetCABpnr_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Output parameter
                        SqlParameter outputParam = new SqlParameter("@ReturnValue", SqlDbType.VarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        // Execute
                        cmd.ExecuteNonQuery();

                        // Read output
                        lReturnValue = outputParam.Value != DBNull.Value ? outputParam.Value.ToString() : "0";
                    }
                }
                catch (Exception ex)
                {
                    lReturnValue = "0"; // or log ex
                }
            }

            return lReturnValue;
        }


        /// <summary>
        /// /// Displays the per kilometer fare details for cars based on the provided city ID and company ID.
        /// </summary>
        /// <param name="lCityID"></param>
        /// <param name="lCompanyID"></param>
        /// <returns></returns>
        public DataTable fncar_perkm_display(int? lCityID, int lCompanyID)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.car_perkm_display, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        cmd.Parameters.AddWithValue("@cityid", (object)lCityID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@I_CompanyID", lCompanyID);

                        // Fill DataTable with results
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optionally log error
                    return null;
                }
            }

            return ldtRecSet;
        }

        /// <summary>
        /// /// Retrieves the car per kilometer fare master details based on the provided city ID, car ID, and company ID.
        /// </summary>
        /// <param name="lCityID"></param>
        /// <param name="lCarID"></param>
        /// <param name="lCompanyID"></param>
        /// <returns></returns>
        public DataTable fnGetCarPerKMFareMaster(int? lCityID, int? lCarID, int lCompanyID)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetCarPerKMFareMaster_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@CityID", (object)lCityID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CarID", (object)lCarID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@I_CompanyID", lCompanyID);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optionally log exception
                    return null;
                }
            }

            return ldtRecSet;
        }

        /// <summary>
        /// /// Inserts a new car tailermade log entry into the database.
        /// </summary>
        /// <param name="pclsCarTailermade_log_tbl"></param>
        /// <returns></returns>
        public int fnInsertCarTailermade_perkm_log(tbl_CarTailermade_log pclsCarTailermade_log_tbl)
        {
            int lStatus = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.insert_tbl_CarTailermade_perkm_log, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters with null checks
                        cmd.Parameters.AddWithValue("@CabId", (object)pclsCarTailermade_log_tbl.CabId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CityId", (object)pclsCarTailermade_log_tbl.CityId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Tourname", (object)pclsCarTailermade_log_tbl.Tourname ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@VehicleId", (object)pclsCarTailermade_log_tbl.VehicleId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Noofpax", (object)pclsCarTailermade_log_tbl.Noofpax ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ac", (object)pclsCarTailermade_log_tbl.Ac ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@StartDate", (object)pclsCarTailermade_log_tbl.StartDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@EndDate", (object)pclsCarTailermade_log_tbl.EndDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PickupAddress", (object)pclsCarTailermade_log_tbl.PickupAddress ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DropAddress", (object)pclsCarTailermade_log_tbl.DropAddress ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PlacesCovered", (object)pclsCarTailermade_log_tbl.PlacesCovered ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Islumpsum", (object)pclsCarTailermade_log_tbl.Islumpsum ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Amount", (object)pclsCarTailermade_log_tbl.Amount ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@STax", (object)pclsCarTailermade_log_tbl.STax ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PerKmFare", (object)pclsCarTailermade_log_tbl.PerKmFare ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ApproxKm", (object)pclsCarTailermade_log_tbl.ApproxKm ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ApproxDays", (object)pclsCarTailermade_log_tbl.ApproxDays ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@NightHalt", (object)pclsCarTailermade_log_tbl.NightHalt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DriverReward", (object)pclsCarTailermade_log_tbl.DriverReward ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Advance", (object)pclsCarTailermade_log_tbl.Advance ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserName", (object)pclsCarTailermade_log_tbl.UserName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BranchCode", (object)pclsCarTailermade_log_tbl.BranchCode ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@isdiscount", (object)pclsCarTailermade_log_tbl.isdiscount ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@noofnighthalts", (object)pclsCarTailermade_log_tbl.noofnighthalts ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@noofdriverrewards", (object)pclsCarTailermade_log_tbl.noofdriverrewards ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MinKmPerDay", (object)pclsCarTailermade_log_tbl.MinKmPerDay ?? DBNull.Value);

                        lStatus = Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    lStatus = -2; // error
                }
            }

            return lStatus;
        }

        /// <summary>
        /// /// Retrieves the sub-transfer types based on the provided transfer ID.
        /// </summary>
        /// <param name="pTransferID"></param>
        /// <returns></returns>
        public DataTable fnCar_SubTransfertypes(int? pTransferID)
        {
            DataTable dtSubTransferTypes = new DataTable();

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.Car_SubTransfertypes, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transferid", (object)pTransferID ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            conn.Open();
                            da.Fill(dtSubTransferTypes);
                        }
                        catch (Exception ex)
                        {
                            // Log or handle the error as needed
                            return null;
                        }
                    }
                }
            }

            return dtSubTransferTypes;
        }


        /// <summary>
        /// /// Retrieves customer details based on the provided search value (email or mobile number).
        /// </summary>
        /// <param name="lSearchValue"></param>
        /// <returns></returns>
        public DataTable fnGetCustomerDetail(string lSearchValue)
        {
            DataTable pdtTable = new DataTable();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                conn = new SqlConnection(DataLib.getConnectionString());
                cmd = new SqlCommand(StoredProcedures.GetCustomerDetail_sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@i_EmailorMbNo", lSearchValue);

                da = new SqlDataAdapter(cmd);
                da.Fill(pdtTable);
                return pdtTable;
            }
            catch (Exception ex)
            {
                // Log or handle exception
                return null;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
                if (da != null)
                    da.Dispose();
                if (cmd != null)
                    cmd.Dispose();
                if (pdtTable != null)
                    pdtTable.Dispose();
            }
        }
        /// <summary>
        /// /// Retrieves the available balance for a specific agent based on the agent ID.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <returns></returns>
        public DataTable fnGetAgent_AvailableBalance(int? lAgentID)
        {
            DataTable dtResult = new DataTable();
            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_GetAgent_AvailableBalance, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Agentid", (object)lAgentID ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            da.Fill(dtResult);
                        }
                        catch (Exception ex)
                        {
                            // Optional: log error
                            return null;
                        }
                    }
                }
            }
            return dtResult;
        }

        /// <summary>
        /// /// Retrieves the agent commission details based on the trip type and agent level ID.
        /// </summary>
        /// <param name="lTripType"></param>
        /// <param name="lAgentLavelID"></param>
        /// <returns></returns>
        public DataTable fnAgentCommission(string lTripType, int? lAgentLavelID)
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAgentCommission, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    cmd.Parameters.AddWithValue("@TripType", (object)lTripType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentLavelID", (object)lAgentLavelID ?? DBNull.Value);

                    // Output Parameter
                    SqlParameter outParam = new SqlParameter("@ReturnValue", SqlDbType.VarChar, 50);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtResult);
                        }

                        // Optional: You can get the return value like this
                        string returnValue = Convert.ToString(cmd.Parameters["@ReturnValue"].Value);
                        // Use returnValue if needed
                    }
                    catch (Exception ex)
                    {
                        // Optional: Log the exception
                        return null;
                    }
                }
            }

            return dtResult;
        }

        /// <summary>
        /// /// Retrieves the agent view report based on the provided agent ID, transaction type ID, and date range.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <param name="lTransTypeID"></param>
        /// <param name="lFromDate"></param>
        /// <param name="lToDate"></param>
        /// <returns></returns>
        public DataTable fnGetAgent_ViewReport(int? lAgentID, int? lTransTypeID, DateTime? lFromDate, DateTime? lToDate)
        {
            DataTable dtReport = new DataTable();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.agent_viewreport, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@I_AgentId", (object)lAgentID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_TransTypeID", (object)lTransTypeID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_FromDate", (object)lFromDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_ToDate", (object)lToDate ?? DBNull.Value);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtReport);
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: Log exception
                dtReport = null;
            }

            return dtReport;
        }

        /// <summary>
        /// /// Retrieves the agent fixed tour duplicate information based on the provided agent ID.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <returns></returns>
        public DataTable fnGetAgentFixedTourDupInfo(int? lAgentID)
        {
            DataTable dtResult = new DataTable();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAgentFixedTourDupInfo_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AgentID", (object)lAgentID ?? DBNull.Value);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtResult);
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally log exception
                dtResult = null;
            }

            return dtResult;
        }

        /// <summary>
        /// /// Retrieves the agent fixed tour duplicate ticket information based on the provided parameters.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <param name="lPnr"></param>
        /// <param name="lTicketNo"></param>
        /// <param name="llPhNo"></param>
        /// <param name="lEmailID"></param>
        /// <param name="lJDate"></param>
        /// <returns></returns>
        public DataSet fnGetAgentFixedTourDupTKT(string lAgentID, string lPnr, string lTicketNo, string llPhNo, string lEmailID, DateTime? lJDate)
        {
            DataSet dsResult = new DataSet();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAgentFixedTourDupTKT_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AgentID", (object)lAgentID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PNR", (object)lPnr ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TicketNo", (object)lTicketNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PhNo", (object)llPhNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)lEmailID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@JDate", (object)lJDate ?? DBNull.Value);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dsResult); // Automatically fills all result sets into DataTable[0], [1], etc.
                    }
                }
            }
            catch (Exception ex)
            {
                dsResult = null; // Or log the exception
            }

            return dsResult;
        }

        /// <summary>
        /// /// Retrieves the ticket code by order detail from the database.
        /// </summary>
        /// <param name="pTicketCode"></param>
        /// <returns></returns>
        public DataTable TicketCodeByOrderDetail(string pTicketCode)
        {
            DataTable dtResult = new DataTable();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_TicketCodeByOrderDetail, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TicketCode", (object)pTicketCode ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtResult);
                    }
                }
            }
            catch (Exception ex)
            {
                dtResult = null; // You can log the exception if needed
            }

            return dtResult;
        }


        /// <summary>
        /// /// Retrieves the ticket panel details based on the provided order ID.
        /// </summary>
        /// <param name="lOrderID"></param>
        /// <returns></returns>
        public DataSet fnGetTickets_PanelDetail(string lOrderID)
        {
            DataSet dsResult = new DataSet();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_GetTickets_Panel, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Orderid", (object)lOrderID ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        da.Fill(dsResult); // This will fill all result sets into the DataSet
                    }
                }
            }
            catch (Exception ex)
            {
                dsResult = null; // Handle/log exception if needed
            }

            return dsResult;
        }

        /// <summary>
        /// /// Retrieves the duplicate ticket print agent information based on the provided order ID, row ID, and bus number.
        /// </summary>
        /// <param name="lOrderID"></param>
        /// <param name="lRowID"></param>
        /// <param name="lBusNo"></param>
        /// <returns></returns>
        public DataSet fnGetDupTicketPrintAgent(string lOrderID, int? lRowID, string lBusNo)
        {
            DataSet dsResult = new DataSet();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.DupTicketPrintAgent_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderID", (object)lOrderID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RowID", (object)lRowID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BusNo", (object)lBusNo ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        da.Fill(dsResult); // Automatically fills all result sets into DataSet tables
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                dsResult = null;
            }

            return dsResult;
        }
        public DataTable fnGetCancelTktHierarchy(string pTktCode, ref int? pHierarchyCount)
        {
            DataTable dtResult = new DataTable();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetCancelTktHierarchy_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    cmd.Parameters.AddWithValue("@I_CurrentTkt", (object)pTktCode ?? DBNull.Value);

                    // Output parameter
                    SqlParameter outputParam = new SqlParameter("@O_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtResult);
                    }

                    // Retrieve output parameter value
                    if (outputParam.Value != DBNull.Value)
                    {
                        pHierarchyCount = Convert.ToInt32(outputParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally log the error
                return null;
            }

            return dtResult;
        }

        /// <summary>
        /// /// Retrieves the agent hotel account duplicate ticket information based on the provided parameters.
        /// </summary>
        /// <param name="lPNR"></param>
        /// <param name="lTicketNo"></param>
        /// <param name="llPhNo"></param>
        /// <param name="lEmailID"></param>
        /// <param name="lJDate"></param>
        /// <param name="lAgentID"></param>
        /// <returns></returns>
        public DataSet fnAgentHTLAccDupTKT(string lPNR, string lTicketNo, string llPhNo, string lEmailID, DateTime? lJDate, string lAgentID)
        {
            DataSet dsResult = new DataSet();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.AgentHTLAccDupTKT, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@PNR", (object)lPNR ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TicketNo", (object)lTicketNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PhNo", (object)llPhNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)lEmailID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@JDate", (object)lJDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentID", (object)lAgentID ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsResult);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                return null;
            }

            return dsResult;
        }

        /// <summary>
        /// /// Retrieves the room fare details along with the room type based on the accommodation booking ID.
        /// </summary>
        /// <param name="lAccBookID"></param>
        /// <returns></returns>
        public DataTable fnGetRoomFareWithRoomType(int? lAccBookID)
        {
            DataTable dtResult = new DataTable();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_GetRoomFareWithRoomType, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter
                    cmd.Parameters.AddWithValue("@AccmBookID", (object)lAccBookID ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtResult);
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                return null;
            }

            return dtResult;
        }

        /// <summary>
        /// /// Retrieves the account data to be sent via email based on the provided ticket number.
        /// </summary>
        /// <param name="lTicketNo"></param>
        /// <returns></returns>
        public DataTable fnGetAccDattTOMail(string lTicketNo)
        {
            DataTable dtResult = new DataTable();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAccDattTOMail_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter
                    cmd.Parameters.AddWithValue("@ticketno", string.IsNullOrEmpty(lTicketNo) ? DBNull.Value : (object)lTicketNo);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtResult);
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: Log the exception if needed
                return null;
            }

            return dtResult;
        }

        /// <summary>
        /// /// Retrieves the hotel receipt details based on the provided order ID.
        /// </summary>
        /// <param name="lOrderID"></param>
        /// <returns></returns>
        public DataTable fnHotelReceipt_Details(string lOrderID)
        {
            DataTable ldtRecSet = new DataTable();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_HotelReceipt_Details, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@OrderId", lOrderID);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                }

                return ldtRecSet;
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return null;
            }
            finally
            {
                if (ldtRecSet != null)
                {
                    ldtRecSet.Dispose();
                }
            }
        }

        /// <summary>
        /// /// Retrieves the payment mode or term details based on the provided ticket number.
        /// </summary>
        /// <param name="lTicketNo"></param>
        /// <returns></returns>
        public DataSet fnGetPayModeOrTerm(string lTicketNo)
        {
            DataSet dsResults = new DataSet();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetPayModeOrTerm_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TicketNo", lTicketNo);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {

                            da.Fill(dsResults);
                        }
                    }
                }

                return dsResults;
            }
            catch (Exception ex)
            {
                // Optionally log exception
                return null;
            }
            finally
            {
                if (dsResults != null)
                {
                    dsResults.Dispose();
                }
            }
        }

        /// <summary>
        /// // Retrieves the email ID of an agent based on the provided agent ID.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <returns></returns>
        public string fnGetEmailIDAgent(int? lAgentID)
        {
            string connectionString = DataLib.getConnectionString();
            string lReturnValue = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetEmailIDAgent_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Input parameter
                        cmd.Parameters.AddWithValue("@AgentID", lAgentID.HasValue ? (object)lAgentID.Value : DBNull.Value);

                        // Output parameter
                        SqlParameter outParam = new SqlParameter("@ReturnEmail", SqlDbType.VarChar, 250);
                        outParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        lReturnValue = outParam.Value != DBNull.Value ? outParam.Value.ToString() : "";
                    }
                }

                return lReturnValue;
            }
            catch (Exception ex)
            {
                return "0"; // Default error return, as in original
            }
        }

        /// <summary>
        /// /// Retrieves the email security information based on the provided branch code and user name.
        /// </summary>
        /// <param name="lBranchCode"></param>
        /// <param name="lUserName"></param>
        /// <returns></returns>
        public string fnEmailSecurity(string lBranchCode, string lUserName)
        {
            string connectionString = DataLib.getConnectionString();
            string lReturnValue = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.EmailSecurity_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Input parameters
                        cmd.Parameters.AddWithValue("@BranchCode", string.IsNullOrEmpty(lBranchCode) ? DBNull.Value : (object)lBranchCode);
                        cmd.Parameters.AddWithValue("@UserName", string.IsNullOrEmpty(lUserName) ? DBNull.Value : (object)lUserName);

                        // Output parameter
                        SqlParameter outParam = new SqlParameter("@o_ReturnValue", SqlDbType.VarChar, 250)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        lReturnValue = outParam.Value != DBNull.Value ? outParam.Value.ToString() : "";
                    }
                }

                return lReturnValue;
            }
            catch (Exception ex)
            {
                return "0"; // Matches original fallback on error
            }
        }


        /// <summary>
        /// /// Inserts or updates agent balance clearance information in the database.
        /// </summary>
        /// <param name="lType"></param>
        /// <param name="lRefNo"></param>
        /// <param name="lBranchCode"></param>
        /// <param name="lUserID"></param>
        /// <param name="lTicketAmt"></param>
        /// <param name="lAdvancePaid"></param>
        /// <param name="lTourName"></param>
        /// <param name="lPayMode"></param>
        /// <param name="lNumber"></param>
        /// <param name="lBankName"></param>
        /// <param name="lChqDate"></param>
        /// <param name="lCredit"></param>
        /// <param name="lTransType"></param>
        /// <param name="lBrnachClear"></param>
        /// <param name="lAmtPaid"></param>
        /// <returns></returns>
        public int fnInsertUpdateAgentBalanceClearence(
    string lType, string lRefNo, string lBranchCode, string lUserID,
    decimal? lTicketAmt, decimal? lAdvancePaid, string lTourName,
    string lPayMode, string lNumber, string lBankName, string lChqDate,
    decimal? lCredit, int? lTransType, decimal? lBrnachClear, decimal lAmtPaid)
        {
            int lStatus = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertUpdateAgentBalanceClearence_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Input Parameters
                        cmd.Parameters.AddWithValue("@iType", (object)lType ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@RefNo", (object)lRefNo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BranchCode", (object)lBranchCode ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserId", (object)lUserID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TicketAmount", (object)lTicketAmt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AdvancePaid", (object)lAdvancePaid ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TourName", (object)lTourName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PaymentMode", (object)lPayMode ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Number", (object)lNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BankName", (object)lBankName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@chqdate", (object)lChqDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@credit", (object)lCredit ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TransactionType", (object)lTransType ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BranchClear", (object)lBrnachClear ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AmountPaid", lAmtPaid);

                        // Output parameter
                        SqlParameter outputParam = new SqlParameter("@ReturnValue", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Read output parameter
                        lStatus = outputParam.Value != DBNull.Value ? Convert.ToInt32(outputParam.Value) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                lStatus = -1;
            }

            return lStatus;
        }


        /// <summary>
        /// /// Retrieves the international tour balance details based on the provided ticket number.
        /// </summary>
        /// <param name="lTicket"></param>
        /// <returns></returns>
        public DataSet fnInternationalTourBal(string lTicket)
        {
            DataSet pdtDTSet = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.InternationalTourBal_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TicketNo", lTicket ?? (object)DBNull.Value);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(pdtDTSet);
                        }
                    }
                }

                return pdtDTSet;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (pdtDTSet != null)
                {
                    pdtDTSet.Dispose();
                    pdtDTSet = null;
                }
            }
        }


        /// <summary>
        /// /// Retrieves the tour balance details for a specific agent based on the provided ticket number, agent ID, and type.
        /// </summary>
        /// <param name="lTicket"></param>
        /// <param name="lAgentID"></param>
        /// <param name="lType"></param>
        /// <returns></returns>
        public DataSet fnAllTourBalAgent(string lTicket, int lAgentID, string lType)
        {
            DataSet pdtDTSet = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.AllTourBalAgent_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TicketNo", string.IsNullOrEmpty(lTicket) ? (object)DBNull.Value : lTicket);
                        cmd.Parameters.AddWithValue("@AgentID", lAgentID);
                        cmd.Parameters.AddWithValue("@Type", string.IsNullOrEmpty(lType) ? (object)DBNull.Value : lType);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(pdtDTSet);
                        }
                    }
                }

                return pdtDTSet;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (pdtDTSet != null)
                {
                    pdtDTSet.Dispose();
                    pdtDTSet = null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves the agent fixed tour information based on the provided parameters.
        /// </summary>
        /// <param name="lPNR"></param>
        /// <param name="lEmail"></param>
        /// <param name="lTktCode"></param>
        /// <param name="JDate"></param>
        /// <returns></returns>
        public DataSet fnAgentFixedTourInfo(string lPNR, string lEmail, string lTktCode, DateTime JDate)
        {
            DataSet pdtDTSet = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.AgentFixedTourInfo_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PNRNO", string.IsNullOrEmpty(lPNR) ? (object)DBNull.Value : lPNR);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(lEmail) ? (object)DBNull.Value : lEmail);
                        cmd.Parameters.AddWithValue("@TicketNo", string.IsNullOrEmpty(lTktCode) ? (object)DBNull.Value : lTktCode);
                        cmd.Parameters.AddWithValue("@journeydate", JDate);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(pdtDTSet);
                        }
                    }
                }

                return pdtDTSet;
            }
            catch (Exception ex)
            {
                // Log error if needed
                return null;
            }
            finally
            {
                if (pdtDTSet != null)
                {
                    pdtDTSet.Dispose();
                    pdtDTSet = null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves the cancellation ticket string based on the provided ticket code.
        /// </summary>
        /// <param name="lTktCode"></param>
        /// <returns></returns>
        public DataTable fnbr_can_ticketstring(string lTktCode)
        {
            DataTable ldtRecSet = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.br_can_ticketstring, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ticketno", string.IsNullOrEmpty(lTktCode) ? (object)DBNull.Value : lTktCode);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                }

                return ldtRecSet;
            }
            catch (Exception ex)
            {
                // Optionally log the exception: ex.Message
                return null;
            }
            finally
            {
                if (ldtRecSet != null)
                {
                    ldtRecSet.Dispose();
                    ldtRecSet = null;
                }
            }
        }


        /// <summary>
        /// /// Retrieves the agent fixed tour detail based on the provided parameters.
        /// </summary>
        /// <param name="lPNR"></param>
        /// <param name="lEmail"></param>
        /// <param name="lTktCode"></param>
        /// <param name="JDate"></param>
        /// <returns></returns>
        public DataTable fnAgentFixedTourDetail(string lPNR, string lEmail, string lTktCode, DateTime JDate)
        {
            DataTable ldtRecSet = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.AgentFixedTourDetail_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PNRNO", string.IsNullOrEmpty(lPNR) ? (object)DBNull.Value : lPNR);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(lEmail) ? (object)DBNull.Value : lEmail);
                        cmd.Parameters.AddWithValue("@TicketCode", string.IsNullOrEmpty(lTktCode) ? (object)DBNull.Value : lTktCode);
                        cmd.Parameters.AddWithValue("@journeydate", JDate);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                }

                return ldtRecSet;
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return null;
            }
            finally
            {
                if (ldtRecSet != null)
                {
                    ldtRecSet.Dispose();
                    ldtRecSet = null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves the agent cancel ticket print information based on the provided ticket number.
        /// </summary>
        /// <param name="lTicketNo"></param>
        /// <returns></returns>
        public DataSet fnAgentCancelTktPrint(string lTicketNo)
        {
            DataSet pdtDTSet = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@TicketNo", SqlDbType.BigInt)
                        {
                            Value = (object)lTicketNo ?? DBNull.Value
                        }
                };

                pdtDTSet = SqlData.GetDataSetSP(StoredProcedures.AgentCancelTktPrint_sp, param);

                return pdtDTSet;
            }
            catch (Exception ex)
            {
                // Optionally log the exception (e.g., to file, event log, or database)
                return null;
            }
            finally
            {
                if (pdtDTSet != null)
                {
                    pdtDTSet.Dispose();
                    pdtDTSet = null;
                }
            }
        }

        /// <summary>
        /// /// Saves the branch to agent booking information in the database.
        /// </summary>
        /// <param name="pTicketNo"></param>
        /// <param name="pBranchUserID"></param>
        /// <param name="pAgentID"></param>
        /// <param name="pIPAdd"></param>
        /// <returns></returns>
        public int fnSaveBranchToAgentBooking(string pTicketNo, int pBranchUserID, int pAgentID, string pIPAdd)
        {
            int lStatus = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.SaveBranchToAgentBooking_SP, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@I_TicketNo", string.IsNullOrEmpty(pTicketNo) ? (object)DBNull.Value : pTicketNo);
                        cmd.Parameters.AddWithValue("@I_BranchUserID", pBranchUserID);
                        cmd.Parameters.AddWithValue("@I_AgentID", pAgentID);
                        cmd.Parameters.AddWithValue("@I_IPAddress", string.IsNullOrEmpty(pIPAdd) ? (object)DBNull.Value : pIPAdd);

                        SqlParameter outputParam = new SqlParameter("@O_ReturnValue", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        if (outputParam.Value != DBNull.Value)
                        {
                            lStatus = Convert.ToInt32(outputParam.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lStatus = -1;
                // Optionally log the exception here
            }

            return lStatus;
        }


        /// <summary>
        /// /// Retrieves the agent cancellation ticket string based on the provided ticket number.
        /// </summary>
        /// <param name="lTicketNo"></param>
        /// <returns></returns>
        public DataSet fnAgent_can_ticketstring(string lTicketNo)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.Agent_can_ticketstring, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TicketNo", string.IsNullOrEmpty(lTicketNo) ? (object)DBNull.Value : lTicketNo);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        da.Fill(ds); // Fills all result sets into separate tables in the DataSet
                    }
                }

                return ds;
            }
            catch (Exception ex)
            {
                // Optionally log the error
                return null;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// /// Retrieves the cancellation percentage based on the tour number, number of days left, and type.
        /// </summary>
        /// <param name="pTourNo"></param>
        /// <param name="PNoOfDaysLeft"></param>
        /// <param name="pType"></param>
        /// <param name="pReturnValue"></param>
        /// <returns></returns>
        public decimal fnGetCancelPercentage(int pTourNo, int PNoOfDaysLeft, bool pType, ref int? pReturnValue)
        {
            decimal cancelPercentage = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetFixedTourCancelPerc_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@I_TourNo", pTourNo);
                    cmd.Parameters.AddWithValue("@I_NoOfDaysLeft", PNoOfDaysLeft);
                    cmd.Parameters.AddWithValue("@I_CanType", pType);

                    SqlParameter percParam = new SqlParameter("@O_CancPercentage", SqlDbType.Float)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(percParam);

                    SqlParameter returnParam = new SqlParameter("@O_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(returnParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    double? lCanPerc = percParam.Value != DBNull.Value ? Convert.ToDouble(percParam.Value) : 0;
                    pReturnValue = returnParam.Value != DBNull.Value ? Convert.ToInt32(returnParam.Value) : (int?)null;

                    return Convert.ToDecimal(lCanPerc);
                }
            }
            catch (Exception ex)
            {
                pReturnValue = -1;
                return 0;
            }
        }

        /// <summary>
        /// /// Retrieves the agent branch code information from the database.
        /// </summary>
        /// <returns></returns>
        public DataTable fnAgentBranchCode()
        {
            DataTable ldtRecSet = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.AgentBranchCode_sp, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    da.Fill(ldtRecSet);

                    return ldtRecSet;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (ldtRecSet != null)
                {
                    ldtRecSet.Dispose();
                    ldtRecSet = null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves the journey date based on the provided date and tour ID.
        /// </summary>
        /// <param name="lJDate"></param>
        /// <param name="lTourID"></param>
        /// <returns></returns>
        public string fnJourneyDate(DateTime? lJDate, int? lTourID)
        {
            string lReturnValue = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.JourneyDate_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@JourneyDate", (object)lJDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TourNo", (object)lTourID ?? DBNull.Value);

                    SqlParameter outputParam = new SqlParameter("@ReturnValue", SqlDbType.VarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lReturnValue = outputParam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                return "0";
            }

            return lReturnValue;
        }

        /// <summary>
        /// /// Retrieves the service tax value for a cancellation ticket based on the provided ticket number.
        /// </summary>
        /// <param name="lTicketNo"></param>
        /// <returns></returns>
        public string fnGetSTaxForCanTkt(string lTicketNo)
        {
            string lTaxValue = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetSTaxForCanTkt_SP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@I_TicketNo", lTicketNo ?? (object)DBNull.Value);

                    SqlParameter outputParam = new SqlParameter("@O_ReturnValue", SqlDbType.VarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lTaxValue = outputParam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                lTaxValue = null;
            }

            return lTaxValue;
        }

        /// <summary>
        /// /// Retrieves the agent commission details based on the provided trip type and agent level ID.
        /// </summary>
        /// <param name="lTripType"></param>
        /// <param name="lAgentID"></param>
        /// <param name="lTourID"></param>
        /// <returns></returns>
        public DataTable fnAgentCommission(string lTripType, int? lAgentID, int lTourID)
        {
            DataTable ldtRecSet = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAgentCommission_New_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameters
                    cmd.Parameters.AddWithValue("@TripType", lTripType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentID", lAgentID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TourNo", lTourID);

                    // Output parameter
                    SqlParameter outputParam = new SqlParameter("@ReturnValue", SqlDbType.VarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ldtRecSet);
                    }

                    // Optional: read the return value
                    string lStatus = outputParam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle/log error as needed
                return null;
            }

            return ldtRecSet;
        }


        /// <summary>
        /// /// Updates the fixed tour cancellation information in the database.
        /// </summary>
        /// <param name="lCanCharges"></param>
        /// <param name="lRefundAmt"></param>
        /// <param name="lTicketNo"></param>
        /// <param name="lCanNoTick"></param>
        /// <param name="lBranchUserID"></param>
        /// <param name="lAgentID"></param>
        /// <param name="lAgentCredit"></param>
        /// <param name="lAvailableBal"></param>
        /// <param name="lAgentDebit"></param>
        /// <param name="lTicketAmt"></param>
        /// <param name="lCommission"></param>
        /// <param name="lsBranchCode"></param>
        /// <param name="lPayMode"></param>
        /// <param name="lDebit"></param>
        /// <param name="lCredit"></param>
        /// <param name="lUserName"></param>
        /// <param name="lBranchCode"></param>
        /// <param name="lSTax"></param>
        /// <param name="lTDS"></param>
        /// <param name="ISPartial"></param>
        /// <param name="lRemarks"></param>
        /// <param name="Overriddenpes"></param>
        /// <param name="IsOverridden"></param>
        /// <param name="ServiceCanChrg"></param>
        /// <param name="lSerRemarks"></param>
        /// <param name="lOverRiddenSerPers"></param>
        /// <param name="IsOverRiddenSer"></param>
        /// <param name="lImpersonateUID"></param>
        /// <param name="lImpersonateBranchCode"></param>
        /// <returns></returns>
        public int fnUpdateFixedTourCancelInfo(
    decimal? lCanCharges, decimal? lRefundAmt, string lTicketNo,
    int? lCanNoTick, string lBranchUserID, int? lAgentID, decimal? lAgentCredit, decimal? lAvailableBal,
    decimal? lAgentDebit, decimal? lTicketAmt, decimal? lCommission, string lsBranchCode, string lPayMode,
    decimal? lDebit, decimal? lCredit, string lUserName, string lBranchCode, decimal? lSTax, decimal? lTDS,
    bool? ISPartial, string lRemarks, decimal? Overriddenpes, char? IsOverridden, decimal ServiceCanChrg,
    string lSerRemarks, decimal lOverRiddenSerPers, bool IsOverRiddenSer, string lImpersonateUID,
    string lImpersonateBranchCode)
        {
            int lStatus = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateFixedTourCancelInfo_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@CanCharges", lCanCharges.HasValue ? (object)lCanCharges.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@RefundAmt", lRefundAmt.HasValue ? (object)lRefundAmt.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TicketNo", (object)lTicketNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CanNoTick", lCanNoTick.HasValue ? (object)lCanNoTick.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@BranchUserId", (object)lBranchUserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@sBranchCode", (object)lsBranchCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentId", lAgentID.HasValue ? (object)lAgentID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentCredit", lAgentCredit.HasValue ? (object)lAgentCredit.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@AvailableBalance", lAvailableBal.HasValue ? (object)lAvailableBal.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentDebit", lAgentDebit.HasValue ? (object)lAgentDebit.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TicketAmount", lTicketAmt.HasValue ? (object)lTicketAmt.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Commission", lCommission.HasValue ? (object)lCommission.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@PaymentMode", (object)lPayMode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Debit", lDebit.HasValue ? (object)lDebit.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Credit", lCredit.HasValue ? (object)lCredit.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@UserName", (object)lUserName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BranchCode", (object)lBranchCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Servicetax", lSTax.HasValue ? (object)lSTax.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TDS", lTDS.HasValue ? (object)lTDS.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@ISPartial", ISPartial.HasValue ? (object)ISPartial.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Remarks", (object)lRemarks ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Overriddenpes", Overriddenpes.HasValue ? (object)Overriddenpes.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@IsOverridden", IsOverridden.HasValue ? (object)IsOverridden.Value.ToString() : DBNull.Value);
                cmd.Parameters.AddWithValue("@RemarksServiceCharge", (object)lSerRemarks ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ServiceChargeOverPerc", (object)lOverRiddenSerPers);
                cmd.Parameters.AddWithValue("@IsServiceChargeOver", (object)IsOverRiddenSer);
                cmd.Parameters.AddWithValue("@CanServiceCharge", (object)ServiceCanChrg);
                cmd.Parameters.AddWithValue("@I_ImpersonateUID", (object)lImpersonateUID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@I_ImpersonateBranchCode", (object)lImpersonateBranchCode ?? DBNull.Value);

                // Output parameter
                SqlParameter outParam = new SqlParameter("@ReturnValue", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lStatus = outParam.Value != DBNull.Value ? Convert.ToInt32(outParam.Value) : 0;
                }
                catch (Exception)
                {
                    lStatus = 0; // Handle/log exception as needed
                }
            }

            return lStatus;
        }

        /// <summary>
        /// /// Retrieves the tour detail based on the provided parameters.
        /// </summary>
        /// <param name="lTourNo"></param>
        /// <param name="lJDate"></param>
        /// <param name="lTicketNo"></param>
        /// <param name="RowID"></param>
        /// <param name="PkpRowID"></param>
        /// <param name="SeatNo"></param>
        /// <returns></returns>
        public int fnGetTourDetail(int? lTourNo, DateTime? lJDate, string lTicketNo, ref int? RowID, ref int? PkpRowID, ref string SeatNo)
        {
            int lStatus = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetTourDetail_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.AddWithValue("@TourNo", lTourNo.HasValue ? (object)lTourNo.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@JourneyDate", lJDate.HasValue ? (object)lJDate.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@TicketNo", (object)lTicketNo ?? DBNull.Value);

                // Output parameters
                SqlParameter paramRowID = new SqlParameter("@ToursRowID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramRowID);

                SqlParameter paramPkpRowID = new SqlParameter("@PickupMasterRowId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramPkpRowID);

                SqlParameter paramSeatNo = new SqlParameter("@SeatNo", SqlDbType.VarChar, -1)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramSeatNo);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Extract output parameter values
                    RowID = paramRowID.Value != DBNull.Value ? (int?)Convert.ToInt32(paramRowID.Value) : null;
                    PkpRowID = paramPkpRowID.Value != DBNull.Value ? (int?)Convert.ToInt32(paramPkpRowID.Value) : null;
                    SeatNo = paramSeatNo.Value != DBNull.Value ? Convert.ToString(paramSeatNo.Value) : null;
                }
                catch (Exception ex)
                {
                    // Handle exception or log if needed
                    return -1;
                }
            }

            return lStatus; // Return 0 by default (same as original)
        }

        /// <summary>
        /// //
        /// </summary>
        /// <param name="pTktNo"></param>
        /// <param name="pTotalAmt"></param>
        /// <param name="pAdvance"></param>
        /// <returns></returns>
        public int fnSaveONTranAdv(string pTktNo, decimal? pTotalAmt, ref double? pAdvance)
        {
            int lStatus = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.SaveONTranAdv_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input Parameters
                cmd.Parameters.AddWithValue("@TicketNo", (object)pTktNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalAmt", pTotalAmt.HasValue ? (object)pTotalAmt.Value : DBNull.Value);

                // Output Parameter
                SqlParameter paramAdvance = new SqlParameter("@ReturnValue", SqlDbType.Float)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramAdvance);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Retrieve output value
                    pAdvance = paramAdvance.Value != DBNull.Value ? (double?)Convert.ToDouble(paramAdvance.Value) : null;
                }
                catch (Exception ex)
                {
                    lStatus = -1;
                }
            }

            return lStatus;
        }

        /// <summary>
        /// /// Retrieves the bus allotment record based on the provided tour serial number and bus number.
        /// </summary>
        /// <param name="lTourSrNo"></param>
        /// <param name="lBusNo"></param>
        /// <returns></returns>
        public DataTable fnAgentGetBusAllotRec(int? lTourSrNo, string lBusNo)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetBusAllotRec_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add input parameters
                cmd.Parameters.AddWithValue("@TourSerial", lTourSrNo.HasValue ? (object)lTourSrNo.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@BusNo", (object)lBusNo ?? DBNull.Value);

                try
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ldtRecSet);
                    }

                    return ldtRecSet;
                }
                catch (Exception ex)
                {
                    // Optionally log the exception
                    return null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves the multi-seating chart with seats based on the provided ticket ID and seats.
        /// </summary>
        /// <param name="lTicketID"></param>
        /// <param name="lSeats"></param>
        /// <returns></returns>
        public string fnGetMultiSeattingChartWithSeats(string lTicketID, string lSeats)
        {
            string lReturnSeats = "";

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetMultiSeattingChartWithSeats_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.AddWithValue("@Old_TicketCode", (object)lTicketID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@i_SeatNo", (object)lSeats ?? DBNull.Value);

                // Output parameter
                SqlParameter outputSeats = new SqlParameter("@ReturnSeats", SqlDbType.VarChar, 5000)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputSeats);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lReturnSeats = outputSeats.Value != DBNull.Value ? outputSeats.Value.ToString() : "";
                }
                catch (Exception ex)
                {
                    // Optional: Log the exception
                    // Keep lReturnSeats as empty or previously assigned
                }
            }

            return lReturnSeats;
        }


        /// <summary>
        /// /// Inserts agent fixed tour information into the database.
        /// </summary>
        /// <param name="lorderid"></param>
        /// <param name="ltourid"></param>
        /// <param name="ldoj"></param>
        /// <param name="ldob"></param>
        /// <param name="lBusEnvType"></param>
        /// <param name="lNoOfAdults"></param>
        /// <param name="lNoofchild"></param>
        /// <param name="lNoofAdultsTwin"></param>
        /// <param name="lNoofAdultsTriple"></param>
        /// <param name="lChildWithoutbed"></param>
        /// <param name="lSingleAdult"></param>
        /// <param name="lTourName"></param>
        /// <param name="lAmount"></param>
        /// <param name="lTax"></param>
        /// <param name="lCalcTaxValue"></param>
        /// <param name="lTotalAmount"></param>
        /// <param name="lseatno"></param>
        /// <param name="lBusSerialNo"></param>
        /// <param name="lTourSerial"></param>
        /// <param name="lPickupPointID"></param>
        /// <param name="lAdultfare"></param>
        /// <param name="lChildfare"></param>
        /// <param name="ladultstwinfare"></param>
        /// <param name="ladultstriplefare"></param>
        /// <param name="lchildwithoutbedfare"></param>
        /// <param name="lsingleadultfare"></param>
        /// <param name="lCreditCardFee"></param>
        /// <param name="lCalcCreditCardFee"></param>
        /// <param name="ldormitory"></param>
        /// <param name="ldormitoryfare"></param>
        /// <param name="lEmailD"></param>
        /// <param name="lTicketNo"></param>
        /// <param name="lCanRowID"></param>
        /// <param name="noAdultWithFood"></param>
        /// <param name="noChildWithFood"></param>
        /// <param name="AdultWithFoodFare"></param>
        /// <param name="ChildWithFoodFare"></param>
        /// <param name="lAdvance"></param>
        /// <param name="lIsPartialPaymentByAgent"></param>
        /// <param name="lAdultServiceCharge"></param>
        /// <param name="ChildServiceCharge"></param>
        /// <param name="ServiceChargeTotal"></param>
        /// <param name="ServiceChargeTax"></param>
        /// <param name="ServiceChargeTaxValue"></param>
        /// <returns></returns>
        public int fnInsertAgentFixTourInfo(ref string lorderid, string ltourid, DateTime? ldoj, DateTime? ldob, char? lBusEnvType, int? lNoOfAdults,
    int? lNoofchild, int? lNoofAdultsTwin, int? lNoofAdultsTriple, int? lChildWithoutbed, int? lSingleAdult, string lTourName,
    decimal? lAmount, decimal? lTax, decimal? lCalcTaxValue, decimal? lTotalAmount, string lseatno, string lBusSerialNo, string lTourSerial,
    int? lPickupPointID, decimal? lAdultfare, decimal? lChildfare, decimal? ladultstwinfare, decimal? ladultstriplefare,
    decimal? lchildwithoutbedfare, decimal? lsingleadultfare, decimal? lCreditCardFee, decimal? lCalcCreditCardFee,
    int ldormitory, decimal? ldormitoryfare, string lEmailD, string lTicketNo, string lCanRowID, int noAdultWithFood, int noChildWithFood,
    decimal AdultWithFoodFare, decimal ChildWithFoodFare, decimal lAdvance, bool lIsPartialPaymentByAgent,
    decimal lAdultServiceCharge, decimal ChildServiceCharge, decimal ServiceChargeTotal, decimal ServiceChargeTax, decimal ServiceChargeTaxValue)
        {
            int returnStatus = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertAgentFixTourInfo_sp, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add all input parameters (null-safe)
                cmd.Parameters.AddWithValue("@tourid", (object)ltourid ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@doj", (object)ldoj ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@dob", (object)ldob ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BusEnvType", (object)lBusEnvType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NoOfAdults", (object)lNoOfAdults ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Noofchild", (object)lNoofchild ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NoofAdultsTwin", (object)lNoofAdultsTwin ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NoofAdultsTriple", (object)lNoofAdultsTriple ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ChildWithoutbed", (object)lChildWithoutbed ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SingleAdult", (object)lSingleAdult ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TourName", (object)lTourName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Amount", (object)lAmount ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tax", (object)lTax ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CalcTaxValue", (object)lCalcTaxValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalAmount", (object)lTotalAmount ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@seatno", (object)lseatno ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BusSerialNo", (object)lBusSerialNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TourSerial", (object)lTourSerial ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PickupPointID", (object)lPickupPointID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Adultfare", (object)lAdultfare ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Childfare", (object)lChildfare ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@adultstwinfare", (object)ladultstwinfare ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@adultstriplefare", (object)ladultstriplefare ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@childwithoutbedfare", (object)lchildwithoutbedfare ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@singleadultfare", (object)lsingleadultfare ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreditCardFee", (object)lCreditCardFee ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CalcCreditCardFee", (object)lCalcCreditCardFee ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@dormitory", (object)ldormitory);
                cmd.Parameters.AddWithValue("@dormitoryfare", (object)ldormitoryfare ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EmailID", (object)lEmailD ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TicketNo", (object)lTicketNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CanRowid", (object)lCanRowID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@noAdultWithFood", noAdultWithFood);
                cmd.Parameters.AddWithValue("@noChildWithFood", noChildWithFood);
                cmd.Parameters.AddWithValue("@AdultWithFoodFare", (object)AdultWithFoodFare);
                cmd.Parameters.AddWithValue("@ChildWithFoodFare", (object)ChildWithFoodFare);
                cmd.Parameters.AddWithValue("@AdavncePaid", (object)lAdvance);
                cmd.Parameters.AddWithValue("@IsPartialPaymentByAgent", (object)lIsPartialPaymentByAgent);
                cmd.Parameters.AddWithValue("@AdultServiceCharge", (object)lAdultServiceCharge);
                cmd.Parameters.AddWithValue("@ChildServiceCharge", (object)ChildServiceCharge);
                cmd.Parameters.AddWithValue("@ServiceChargesTotal", (object)ServiceChargeTotal);
                cmd.Parameters.AddWithValue("@ServiceChargesTax", (object)ServiceChargeTax);
                cmd.Parameters.AddWithValue("@ServiceChargesTaxVal", (object)ServiceChargeTaxValue);

                // Output parameter
                SqlParameter outOrderId = new SqlParameter("@returnValue", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outOrderId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lorderid = outOrderId.Value != DBNull.Value ? outOrderId.Value.ToString() : "";
                    returnStatus = 1;
                }
                catch (Exception ex)
                {
                    returnStatus = 0;
                }
            }

            return returnStatus;
        }


        /// <summary>
        /// /// Retrieves special tour ticket information based on the provided PNR, email, and journey date.
        /// </summary>
        /// <param name="lPNR"></param>
        /// <param name="lEmail"></param>
        /// <param name="JDate"></param>
        /// <returns></returns>
        public DataTable fnAgentSplTourTicket(string lPNR, string lEmail, DateTime JDate)
        {
            DataTable ldtRecSet = new DataTable();
            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetSplTourTicket_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@PNRNO", lPNR ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", lEmail ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@journeydate", JDate);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optionally log error
                        return null;
                    }
                }
            }
            return ldtRecSet;
        }


        /// <summary>
        /// /// Retrieves the special vehicle master information based on the provided vehicle ID.
        /// </summary>
        /// <param name="lVehicleID"></param>
        /// <returns></returns>
        public DataTable fnAgentSplVehiclemaster(string lVehicleID)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetSplVehiclemaster_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    cmd.Parameters.AddWithValue("@vehicleid", string.IsNullOrEmpty(lVehicleID) ? (object)DBNull.Value : lVehicleID);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error if needed
                        return null;
                    }
                }
            }

            return ldtRecSet;
        }


        /// <summary>
        /// /// Retrieves the weekend cancellation percentage based on the provided tour ID and number of days.
        /// </summary>
        /// <param name="lTourID"></param>
        /// <param name="lNooFDays"></param>
        /// <returns></returns>
        public DataTable fnspl_weekend_cancelpercentage(int? lTourID, int? lNooFDays)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.spl_weekend_cancelpercentage, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with null-checking
                    cmd.Parameters.AddWithValue("@tourid", lTourID.HasValue ? (object)lTourID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@noofdays", lNooFDays.HasValue ? (object)lNooFDays.Value : DBNull.Value);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optionally log the error
                        return null;
                    }
                }
            }

            return ldtRecSet;
        }

        /// <summary>
        /// /// Retrieves the payment mode based on the provided payment mode string.
        /// </summary>
        /// <param name="lPayMode"></param>
        /// <returns></returns>
        public DataTable fnPayMode(string lPayMode)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetPayMode_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    cmd.Parameters.AddWithValue("@PaymentMode", string.IsNullOrEmpty(lPayMode) ? DBNull.Value : (object)lPayMode);

                    try
                    {
                        conn.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }

                        return ldtRecSet;
                    }
                    catch (Exception ex)
                    {
                        // Optional: log the error
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// /// Retrieves the special tour cancellation ticket information based on the provided PNR, email, and journey date.
        /// </summary>
        /// <param name="lPNR"></param>
        /// <param name="lEmail"></param>
        /// <param name="JDate"></param>
        /// <returns></returns>
        public DataTable fnAgentSplTourCancelTicket(string lPNR, string lEmail, DateTime JDate)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetSplTourCancelTicket_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    cmd.Parameters.AddWithValue("@PNRNO", string.IsNullOrEmpty(lPNR) ? DBNull.Value : (object)lPNR);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(lEmail) ? DBNull.Value : (object)lEmail);
                    cmd.Parameters.AddWithValue("@JourneyDate", JDate);

                    try
                    {
                        conn.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }

                        return ldtRecSet;
                    }
                    catch (Exception ex)
                    {
                        // Optionally log the error
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// /// Updates the special tour cancellation information in the database.
        /// </summary>
        /// <param name="lCanCharges"></param>
        /// <param name="lRefundAmt"></param>
        /// <param name="lTicketNo"></param>
        /// <param name="lCanNoTick"></param>
        /// <param name="lBranchUserID"></param>
        /// <param name="lAgentID"></param>
        /// <param name="lAgentCredit"></param>
        /// <param name="lAvailableBal"></param>
        /// <param name="lAgentDebit"></param>
        /// <param name="lTicketAmt"></param>
        /// <param name="lCommission"></param>
        /// <param name="lsBranchCode"></param>
        /// <param name="lPayMode"></param>
        /// <param name="lDebit"></param>
        /// <param name="lCredit"></param>
        /// <param name="lUserName"></param>
        /// <param name="lBranchCode"></param>
        /// <param name="lSTax"></param>
        /// <param name="lTDS"></param>
        /// <returns></returns>
        public int fnUpdateSPLTourCancelInfo(
      decimal? lCanCharges, decimal? lRefundAmt, string lTicketNo,
      int? lCanNoTick, string lBranchUserID, int? lAgentID, decimal? lAgentCredit, decimal? lAvailableBal,
      decimal? lAgentDebit, decimal? lTicketAmt, decimal? lCommission,
      string lsBranchCode, string lPayMode, decimal? lDebit, decimal? lCredit,
      string lUserName, string lBranchCode, decimal? lSTax, decimal? lTDS)
        {
            int lStatus = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand("UpdateSPLTourCancelInfo_sp", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add all input parameters
                cmd.Parameters.AddWithValue("@CanCharges", (object)lCanCharges ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RefundAmt", (object)lRefundAmt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TicketNo", (object)lTicketNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CanNoTick", (object)lCanNoTick ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BranchUserId", (object)lBranchUserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@sBranchCode", (object)lsBranchCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentId", (object)lAgentID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentCredit", (object)lAgentCredit ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AvailableBalance", (object)lAvailableBal ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AgentDebit", (object)lAgentDebit ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TicketAmount", (object)lTicketAmt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Commission", (object)lCommission ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PaymentMode", (object)lPayMode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Debit", (object)lDebit ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Credit", (object)lCredit ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UserName", (object)lUserName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BranchCode", (object)lBranchCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Servicetax", (object)lSTax ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TDS", (object)lTDS ?? DBNull.Value);

                // Add Return Output parameter
                SqlParameter returnParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
                returnParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(returnParam);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lStatus = (int)(returnParam.Value ?? 0);
                }
                catch (Exception ex)
                {
                    lStatus = 0;
                    // You may log ex.Message here
                }
            }

            return lStatus;
        }


        /// <summary>
        /// //Retrieves the Fix Tour Balance Receiptt information based on the provided lPnr.
        /// </summary>
        /// <param name="lPnr"></param>
        /// <returns></returns>
        public DataSet fnFixTourBalancecReceiptt(string lPnr)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.FixTourBalancecReceipt_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Pnr", lPnr);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            con.Open();
                            da.Fill(ds);
                        }
                        catch (Exception ex)
                        {
                            // Log error here
                            return null;
                        }
                    }
                }
            }

            return ds;
        }


        /// <summary>
        /// //Retrieves the SPL Tour Balance Receiptt information based on the provided lPnr.
        /// </summary>
        /// <param name="lPnr"></param>
        /// <returns></returns>

        public DataSet fnSPLTourBalancecReceipt(string lPnr)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.SPLTourBalancecReceipt_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Pnr", lPnr);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            con.Open();
                            da.Fill(ds); // Fills all result sets into ds.Tables[0], ds.Tables[1]
                        }
                        catch (Exception ex)
                        {
                            // Optionally log exception
                            return null;
                        }
                    }
                }
            }

            return ds;
        }


        /// <summary>
        /// //Retrieves the International Tour Balance Receiptt information based on the provided lPnr.
        /// </summary>
        /// <param name="lPnr"></param>
        /// <returns></returns>
        public DataSet fnINTTourBalancecReceipt(string lPnr)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.INTTourBalancecReceipt_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Pnr", lPnr);

                    try
                    {
                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds); // This will fill ds.Tables[0], ds.Tables[1], ds.Tables[2]
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optionally log the exception here
                        return null;
                    }
                }
            }

            return ds;
        }

        /// <summary>
        /// //Retrieves the Initial Duty Slip  information based on the provided lPnr.
        /// </summary>
        /// <param name="lPNR"></param>
        /// <returns></returns>
        public DataTable fnChkInitialDutySlip(string lPNR)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.ChkInitialDutySlip_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PNRNO", lPNR);

                    try
                    {
                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optionally log exception
                        return null;
                    }
                }
            }

            return dt;
        }
        /// <summary>
        ///  Retrieves the Agent Car Cancellation  information based on the provided lPnr,lEmail,JDate.
        /// </summary>
        /// <param name="lPNR"></param>
        /// <param name="lEmail"></param>
        /// <param name="JDate"></param>
        /// <returns></returns>

        public DataTable fnAgentCarcancellation(string lPNR, string lEmail, DateTime JDate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_AgentCarcancellation, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PNRNO", lPNR);
                    cmd.Parameters.AddWithValue("@email", lEmail);
                    cmd.Parameters.AddWithValue("@journeydate", JDate);
                    try
                    {
                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
            return dt;
        }

        /// <summary>
        ///  Retrieves the Agent Car Cancellation Ticket  information based on the provided lPnr,lEmail,JDate.
        /// </summary>
        /// <param name="lPNR"></param>
        /// <param name="lEmail"></param>
        /// <param name="JDate"></param>
        /// <returns></returns>
        public DataTable fnAgentCancelTicket(string lPNR, string lEmail, DateTime JDate)
        {
            DataTable dt = new DataTable();
            using(SqlConnection con= new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd= new SqlCommand(StoredProcedures.GetCancelTicket_sp,con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PNRNO", lPNR);
                    cmd.Parameters.AddWithValue("@email", lEmail);
                    cmd.Parameters.AddWithValue("@journeydate", JDate);
                    try
                    {
                        con.Open();
                        using (SqlDataAdapter da= new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }

                }
            }
            return dt;
        }


        /// <summary>
        /// ///updating the Cancel Car Info 
        /// </summary>
        /// <param name="canCharges"></param>
        /// <param name="refundAmt"></param>
        /// <param name="ticketNo"></param>
        /// <param name="canNoTick"></param>
        /// <param name="branchUserId"></param>
        /// <param name="sBranchCode"></param>
        /// <param name="agentId"></param>
        /// <param name="agentCredit"></param>
        /// <param name="availableBalance"></param>
        /// <param name="agentDebit"></param>
        /// <param name="ticketAmount"></param>
        /// <param name="commission"></param>
        /// <param name="paymentMode"></param>
        /// <param name="debit"></param>
        /// <param name="credit"></param>
        /// <param name="userName"></param>
        /// <param name="branchCode"></param>
        /// <param name="servicetax"></param>
        /// <param name="tDS"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>

        public int fnUpdateCarCancelInfo(
    decimal? canCharges, decimal? refundAmt, string ticketNo, int? canNoTick, string branchUserId, string sBranchCode,
    int? agentId, decimal? agentCredit, decimal? availableBalance, decimal? agentDebit, decimal? ticketAmount, decimal? commission, string paymentMode,
    decimal? debit, decimal? credit, string userName, string branchCode, decimal? servicetax, decimal? tDS, ref int? returnValue)
        {
            int? lStatus = 0;

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateCarCancelInfo_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CanCharges", (object)canCharges ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RefundAmt", (object)refundAmt ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TicketNo", (object)ticketNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CanNoTick", (object)canNoTick ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BranchUserId", (object)branchUserId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@sBranchCode", (object)sBranchCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentId", (object)agentId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentCredit", (object)agentCredit ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AvailableBalance", (object)availableBalance ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentDebit", (object)agentDebit ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TicketAmount", (object)ticketAmount ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Commission", (object)commission ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PaymentMode", (object)paymentMode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Debit", (object)debit ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Credit", (object)credit ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserName", (object)userName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BranchCode", (object)branchCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Servicetax", (object)servicetax ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TDS", (object)tDS ?? DBNull.Value);

                    SqlParameter outParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        returnValue = Convert.ToInt32(outParam.Value);
                        lStatus = 1; // assuming success means 1
                    }
                    catch (Exception ex)
                    {
                        lStatus = 0;
                        // Optionally log the exception
                    }
                }
            }

            return Convert.ToInt32(lStatus);
        }


        /// <summary>
        /// /// Retrieves the cancellation percentage for hotel accounts based on the number of hours left before cancellation.
        /// </summary>
        /// <param name="PNoOfHoursLeft"></param>
        /// <returns></returns>

        public decimal fnGetHAccCancelPerc(int PNoOfHoursLeft)
        {
            double? lCanPerc = 0;
            int? lStatus = 0;

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetHAccCancelPerc_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@I_NoOfHoursLeft", PNoOfHoursLeft);

                    SqlParameter canPercParam = new SqlParameter("@O_CancPercentage", SqlDbType.Float);
                    canPercParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(canPercParam);

                    SqlParameter returnStatusParam = new SqlParameter("@O_ReturnValue", SqlDbType.Int);
                    returnStatusParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(returnStatusParam);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        lCanPerc = Convert.ToDouble(canPercParam.Value);
                        lStatus = Convert.ToInt32(returnStatusParam.Value);

                        return Convert.ToDecimal(lCanPerc);
                    }
                    catch (Exception ex)
                    {
                        // Optional: Log the exception
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// /// Retrieves the hotel cancellation percentage based on the number of hours left and hotel ID.
        /// </summary>
        /// <param name="PNoOfHoursLeft"></param>
        /// <param name="pHotelID"></param>
        /// <returns></returns>
        public decimal fnGetHotelCancelPerc(int PNoOfHoursLeft, int pHotelID)
        {
            double? lCanPerc = 0;
            int? lStatus = 0;

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetHotelCancelPerc_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameters
                    cmd.Parameters.AddWithValue("@I_NoOfHoursLeft", PNoOfHoursLeft);
                    cmd.Parameters.AddWithValue("@i_HotelID", pHotelID);

                    // Output parameters
                    SqlParameter canPercParam = new SqlParameter("@O_CancPercentage", SqlDbType.Float)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(canPercParam);

                    SqlParameter returnValParam = new SqlParameter("@O_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(returnValParam);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

                        lCanPerc = Convert.ToDouble(canPercParam.Value);
                        lStatus = Convert.ToInt32(returnValParam.Value);

                        return Convert.ToDecimal(lCanPerc);
                    }
                    catch (Exception ex)
                    {
                        // Optional: log exception
                        return 0;
                    }
                }
            }
        }


        /// <summary>
        /// /// Saves the hotel account cancellation information into the database.
        /// </summary>
        /// <param name="lTicketNo"></param>
        /// <param name="lCancelCharges"></param>
        /// <param name="lRefundAmt"></param>
        /// <param name="lNooFPax"></param>
        /// <param name="lUserID"></param>
        /// <param name="lBranchCode"></param>
        /// <param name="lPayMode"></param>
        /// <param name="lTicketAmt"></param>
        /// <param name="lCancelRemarks"></param>
        /// <param name="transactionname"></param>
        /// <param name="agentId"></param>
        /// <param name="agentCredit"></param>
        /// <param name="availableBalance"></param>
        /// <param name="agentDebit"></param>
        /// <param name="commission"></param>
        /// <param name="debit"></param>
        /// <param name="credit"></param>
        /// <param name="Stax"></param>
        /// <param name="TDS"></param>
        /// <param name="isOverridden"></param>
        /// <param name="lOverriddenPers"></param>
        /// <param name="lImpersonateUID"></param>
        /// <param name="lImpersonateBranchCode"></param>
        /// <returns></returns>
        public int fnSaveHACCancelInfo(string lTicketNo, decimal? lCancelCharges, decimal? lRefundAmt,
    int? lNooFPax, string lUserID, string lBranchCode, string lPayMode, decimal? lTicketAmt,
    string lCancelRemarks, string transactionname, int? agentId, decimal agentCredit,
    decimal availableBalance, decimal agentDebit, decimal commission, decimal debit,
    decimal credit, decimal Stax, decimal TDS, char? isOverridden, decimal? lOverriddenPers,
    string lImpersonateUID, string lImpersonateBranchCode)
        {
            int? lStatus = 0;

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.SaveHACCancelInfo_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@TicketNo", lTicketNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CancellationAmt", lCancelCharges ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@RefundAmt", lRefundAmt ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CanNoPax", lNooFPax ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserId", lUserID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@BranchCode", lBranchCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@paymode", lPayMode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ticketamt", lTicketAmt ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CancelRemarks", lCancelRemarks ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TransactionName", transactionname ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentId", agentId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentCredit", agentCredit);
                    cmd.Parameters.AddWithValue("@AvailableBalance", availableBalance);
                    cmd.Parameters.AddWithValue("@AgentDebit", agentDebit);
                    cmd.Parameters.AddWithValue("@Commission", commission);
                    cmd.Parameters.AddWithValue("@Debit", debit);
                    cmd.Parameters.AddWithValue("@Credit", credit);
                    cmd.Parameters.AddWithValue("@servicetax", Stax);
                    cmd.Parameters.AddWithValue("@TDS", TDS);
                    cmd.Parameters.AddWithValue("@IsOverridden", isOverridden.HasValue ? (object)isOverridden.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Overriddenpes", lOverriddenPers ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_ImpersonateUID", lImpersonateUID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_ImpersonateBranchCode", lImpersonateBranchCode ?? (object)DBNull.Value);

                    // Output parameter
                    SqlParameter returnParam = new SqlParameter("@ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(returnParam);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        lStatus = Convert.ToInt32(returnParam.Value);
                    }
                    catch (Exception ex)
                    {
                        lStatus = -1;
                    }
                }
            }

            return lStatus ?? -1;
        }

        /// <summary>
        /// /// Retrieves the hotel account cancellation information based on the provided PNR, email, and check-in date.
        /// </summary>
        /// <param name="lPnrNo"></param>
        /// <param name="lEmail"></param>
        /// <param name="lCheckInDate"></param>
        /// <returns></returns>
        public DataTable fnBranchHACCancelInfo(string lPnrNo, string lEmail, DateTime? lCheckInDate)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.BranchHACCancelInfo_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@PNRNO", lPnrNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", lEmail ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CheckIndate", lCheckInDate ?? (object)DBNull.Value);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle/log exception as needed
                        return null;
                    }
                }
            }

            return ldtRecSet;
        }


        /// <summary>
        /// /// Retrieves the room type occupancy information based on the provided account booking ID.
        /// </summary>
        /// <param name="lAccBookID"></param>
        /// <returns></returns>
        public DataTable fnGetRoomTypeOccupancyNew(int? lAccBookID)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetRoomTypeOccupancyNew_SP, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter
                    cmd.Parameters.AddWithValue("@RowID", lAccBookID ?? (object)DBNull.Value);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log exception
                        return null;
                    }
                }
            }

            return ldtRecSet;
        }

        /// <summary>
        /// /// Cancels the receipt based on the provided ticket number.
        /// </summary>
        /// <param name="lTicketo"></param>
        /// <returns></returns>
        public DataTable fnAccCancelReceipt(string lTicketo)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.AccCancelReceipt_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter
                    cmd.Parameters.AddWithValue("@TicketNo", string.IsNullOrEmpty(lTicketo) ? (object)DBNull.Value : lTicketo);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log exception here
                        return null;
                    }
                }
            }

            return ldtRecSet;
        }


        /// <summary>
        /// // Retrieves the agent car tour duplication information based on the provided agent ID.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <returns></returns>
        public DataTable fnGetAgentCarTourDupInfo(int? lAgentID)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAgentCarTourDupInfo_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    cmd.Parameters.AddWithValue("@AgentID", lAgentID.HasValue ? (object)lAgentID.Value : DBNull.Value);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optional: log or handle the exception as needed
                        return null;
                    }
                }
            }

            return ldtRecSet;
        }

        /// <summary>
        /// // Retrieves the agent car tour duplication ticket information based on the provided parameters.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <param name="lPnr"></param>
        /// <param name="lTicketNo"></param>
        /// <param name="llPhNo"></param>
        /// <param name="lEmailID"></param>
        /// <param name="lJDate"></param>
        /// <returns></returns>
        public DataSet fnGetAgentCarTourDupTKT(string lAgentID, string lPnr, string lTicketNo, string llPhNo, string lEmailID, DateTime? lJDate)
        {
            DataSet pdtDTSet = new DataSet();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAgentCarTourDupTKT_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with proper null handling
                    cmd.Parameters.AddWithValue("@AgentID", string.IsNullOrEmpty(lAgentID) ? DBNull.Value : (object)lAgentID);
                    cmd.Parameters.AddWithValue("@PNR", string.IsNullOrEmpty(lPnr) ? DBNull.Value : (object)lPnr);
                    cmd.Parameters.AddWithValue("@TicketNo", string.IsNullOrEmpty(lTicketNo) ? DBNull.Value : (object)lTicketNo);
                    cmd.Parameters.AddWithValue("@PhNo", string.IsNullOrEmpty(llPhNo) ? DBNull.Value : (object)llPhNo);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(lEmailID) ? DBNull.Value : (object)lEmailID);
                    cmd.Parameters.AddWithValue("@JDate", lJDate.HasValue ? (object)lJDate.Value : DBNull.Value);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(pdtDTSet);
                        }

                        return pdtDTSet;
                    }
                    catch (Exception ex)
                    {
                        // Optional: log exception
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// /// Retrieves the email and cab booking information based on the provided customer ID.
        /// </summary>
        /// <param name="lCustID"></param>
        /// <param name="EmailID"></param>
        /// <param name="CabID"></param>
        /// <returns></returns>
        public int fnGetEmailCarBooking(string lCustID, ref string EmailID, ref string CabID)
        {
            int lStatus = 0;

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetEmailCarBooking_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    cmd.Parameters.AddWithValue("@customerrowid", string.IsNullOrEmpty(lCustID) ? DBNull.Value : (object)lCustID);

                    // Output parameters
                    SqlParameter paramEmail = new SqlParameter("@ReturnEmail", SqlDbType.VarChar, 150)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramEmail);

                    SqlParameter paramCabID = new SqlParameter("@ReturnCabID", SqlDbType.VarChar, 150)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramCabID);

                    try
                    {
                        con.Open();
                        lStatus = cmd.ExecuteNonQuery();

                        // Assign output values
                        EmailID = paramEmail.Value != DBNull.Value ? paramEmail.Value.ToString() : null;
                        CabID = paramCabID.Value != DBNull.Value ? paramCabID.Value.ToString() : null;

                        return lStatus;
                    }
                    catch (Exception ex)
                    {
                        // Optional: log the exception
                        return -1;
                    }
                }
            }
        }

        /// <summary>
        /// /// Retrieves the agent car ticket information based on the provided cab ID.
        /// </summary>
        /// <param name="lCabID"></param>
        /// <returns></returns>
        public DataTable fnGetAgent_CarTicket(string lCabID)
        {
            DataTable ldtRecSet = new DataTable();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_Agent_CarTicket, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    cmd.Parameters.AddWithValue("@Cabid", string.IsNullOrEmpty(lCabID) ? DBNull.Value : (object)lCabID);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ldtRecSet);
                        }

                        return ldtRecSet;
                    }
                    catch (Exception ex)
                    {
                        // Optional: log error
                        return null;
                    }
                    finally
                    {
                        if (ldtRecSet != null)
                        {
                            ldtRecSet.Dispose();
                            ldtRecSet = null;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// /// Retrieves the agent special package tour duplication information based on the provided agent ID.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <returns></returns>
        public DataTable fnGetAgentSPLTourDupInfo(int? lAgentID)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dtResult = new DataTable();

            try
            {
                conn = new SqlConnection(DataLib.getConnectionString());
                cmd = new SqlCommand(StoredProcedures.GetAgentSPLTourDupInfo_sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.AddWithValue("@AgentID", (object)lAgentID ?? DBNull.Value);

                da = new SqlDataAdapter(cmd);
                da.Fill(dtResult);

                return dtResult;
            }
            catch (Exception ex)
            {
                // Log or handle the error as needed
                return null;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();

                if (da != null) da.Dispose();
                if (cmd != null) cmd.Dispose();
                if (dtResult != null) dtResult.Dispose();
            }
        }

        /// <summary>
        /// /// Retrieves the agent special package tour duplication ticket information based on the provided parameters.
        /// </summary>
        /// <param name="lAgentID"></param>
        /// <param name="lTicketNo"></param>
        /// <param name="llPhNo"></param>
        /// <param name="lEmailID"></param>
        /// <param name="lJDate"></param>
        /// <returns></returns>

        public DataSet fnGetAgentSPLTourDupTKT(string lAgentID, string lTicketNo, string llPhNo, string lEmailID, DateTime? lJDate)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet dsResult = new DataSet();

            try
            {
                conn = new SqlConnection(DataLib.getConnectionString());
                cmd = new SqlCommand(StoredProcedures.GetAgentSPLTourDupTKT_sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@AgentID", (object)lAgentID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TicketNo", (object)lTicketNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PhNo", (object)llPhNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)lEmailID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@JDate", (object)lJDate ?? DBNull.Value);

                da = new SqlDataAdapter(cmd);
                da.Fill(dsResult);

                return dsResult;
            }
            catch (Exception ex)
            {
                // Optionally log ex.Message
                return null;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();

                if (da != null) da.Dispose();
                if (cmd != null) cmd.Dispose();
                if (dsResult != null) dsResult.Dispose();
            }
        }


        /// <summary>
        /// /// Retrieves the special ticket information based on the provided ID.
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public DataSet fnSpecial_Ticket_Print(int? pID)
        {
            DataSet pdtDTSet = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@id", SqlDbType.BigInt)
                        {
                            Value = (object)pID ?? DBNull.Value
                        }
                };

                pdtDTSet = SqlData.GetDataSetSP(StoredProcedures.Special_Ticket, param);

                return pdtDTSet;
            }
            catch (Exception ex)
            {
                // Optionally log the exception (e.g., to file, event log, or database)
                return null;
            }
            finally
            {
                if (pdtDTSet != null)
                {
                    pdtDTSet.Dispose();
                    pdtDTSet = null;
                }
            }
        }

        /// <summary>
        /// /// Retrieves the tour information based on the provided hour parameter.
        /// </summary>
        /// <param name="lHour"></param>
        /// <returns></returns>
        public DataTable fnGetTour(int? lHour)
        {
            DataTable tourDatable = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@i_Hr", SqlDbType.BigInt)
                    {
                        Value = (object)lHour ?? DBNull.Value
                    }
                };

                tourDatable = SqlData.GetDataTableSP(StoredProcedures.GetTour_sp, param);

                return tourDatable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// /// Deletes the online tours booking based on the provided row ID.
        /// </summary>
        /// <param name="lRowID"></param>
        /// <returns></returns>
        public int fnDeleteOnlineToursBooking(string lRowID)
        {
            int resultStatus = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.DeleteOnlineToursBooking_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    cmd.Parameters.Add(new SqlParameter("@i_RowID", SqlDbType.VarChar, 50) { Value = lRowID });

                    // Output parameter
                    SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Retrieve output value
                    resultStatus = Convert.ToInt32(outputParam.Value);
                }
            }
            catch (Exception ex)
            {
                // Log ex.Message if needed
                resultStatus = 0;
            }

            return resultStatus;
        }


        /// <summary>
        /// /// Retrieves the passenger count based on the provided hour parameter.
        /// </summary>
        /// <param name="lHour"></param>
        /// <returns></returns>
        public DataTable fnGetPaxCount(string orderid)
        {
            DataTable tourDatable = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@orderID", SqlDbType.VarChar, 50)
                        {
                            Value = string.IsNullOrEmpty(orderid) ? DBNull.Value : (object)orderid
                        }
                };

                tourDatable = SqlData.GetDataTableSP(StoredProcedures.GetPaxCount_sp, param);

                return tourDatable;
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return null;
            }
        }


        /// <summary>
        /// /// Retrieves the booking information for tours based on the provided order ID for agents.
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable fnGetToursBookingInfoAgent(string orderid)
        {
            DataTable tourDatable = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@orderID", SqlDbType.VarChar, 50)
                        {
                            Value = string.IsNullOrEmpty(orderid) ? DBNull.Value : (object)orderid
                        }
                };

                tourDatable = SqlData.GetDataTableSP(StoredProcedures.GetToursBookingInfoAgent_sp, param);

                return tourDatable;
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return null;
            }
        }


        /// <summary>
        /// /// Retrieves the total amount for a tour booking based on the provided order ID.
        /// </summary>
        /// <param name="lOrderID"></param>
        /// <returns></returns>
        public string fnGetTourBookTotalAmt(string lOrderID)
        {
            string lTotalAmt = "0";

            try
            {
                using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetTourBookTotalAmt_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.VarChar, 50)
                        {
                            Value = string.IsNullOrEmpty(lOrderID) ? DBNull.Value : (object)lOrderID
                        });

                        SqlParameter outputParam = new SqlParameter("@o_TotalAmt", SqlDbType.VarChar, 250)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        lTotalAmt = outputParam.Value != DBNull.Value ? outputParam.Value.ToString() : "0";
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally log ex
                lTotalAmt = "0";
            }

            return lTotalAmt;
        }


        /// <summary>
        /// /// Retrieves the row-wise tour ID based on the provided row ID.
        /// </summary>
        /// <param name="lRowID"></param>
        /// <returns></returns>
        public int fnGet_RowWiseTourID(int? lRowID)
        {
            int lTourID = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.RowWiseTourID_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Input parameter
                        cmd.Parameters.Add(new SqlParameter("@i_RowID", SqlDbType.Int));
                        cmd.Parameters["@i_RowID"].Value = (object)lRowID ?? DBNull.Value;

                        // Output parameter
                        SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Get the value of the output parameter
                        if (outputParam.Value != DBNull.Value)
                        {
                            lTourID = Convert.ToInt32(outputParam.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lTourID = 0;
                    // Log exception if needed
                }
            }

            return lTourID;
        }

        /// <summary>
        /// /// Retrieves the journey date based on the provided hours and tour number.
        /// </summary>
        /// <param name="pHours"></param>
        /// <param name="pTourNo"></param>
        /// <returns></returns>

        public DataTable fnGetJourneyDate(int? pHours, int? pTourNo)
        {
            DataTable tourDataTable = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
            new SqlParameter("@Hours", SqlDbType.Int)
            {
                Value = pHours.HasValue ? (object)pHours.Value : DBNull.Value
            },
            new SqlParameter("@TourNo", SqlDbType.Int)
            {
                Value = pTourNo.HasValue ? (object)pTourNo.Value : DBNull.Value
            }
                };

                tourDataTable = SqlData.GetDataTableSP(StoredProcedures.GetJourneyDate_sp, param); // Make sure this matches your SP name

                return tourDataTable;
            }
            catch (Exception ex)
            {
                // Log ex if needed
                return null;
            }
        }

        /// <summary>
        /// /// Retrieves the tour serial number based on the provided tour ID and journey date.
        /// </summary>
        /// <param name="lTourID"></param>
        /// <param name="lJDate"></param>
        /// <returns></returns>
        public int fnGet_TourWiseTourSr(int? lTourID, DateTime? lJDate)
        {
            int lTourSr = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.TourWiseTourSr_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    cmd.Parameters.AddWithValue("@i_TourID", (object)lTourID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_JDate", (object)lJDate ?? DBNull.Value);

                    // Output Parameter
                    SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        if (outputParam.Value != DBNull.Value)
                            lTourSr = Convert.ToInt32(outputParam.Value);
                    }
                    catch (Exception ex)
                    {
                        lTourSr = 0; // Optional: Log exception
                    }
                }
            }

            return lTourSr;
        }


        public DataTable fnGetSeatDetail(int? lRowID)
        {
            DataTable seatDataTable = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@i_RowID", SqlDbType.Int)
                    {
                        Value = lRowID.HasValue ? (object)lRowID.Value : DBNull.Value
                    }
                };

                seatDataTable = SqlData.GetDataTableSP(StoredProcedures.SeatDetail_sp, param);

                return seatDataTable;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public int fnUpdaterTourDeatil(int? lRowID, DateTime? lJDate, string lTourSerial, string lBusSerialNo, string lSeatNo)
        {
            int lStatus = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.UpdaterTourDeatil_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    cmd.Parameters.AddWithValue("@i_RowID", (object)lRowID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_JDate", (object)lJDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_TourSerial", (object)lTourSerial ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_BusSerialNo", (object)lBusSerialNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_SeatNo", (object)lSeatNo ?? DBNull.Value);

                    // Output Parameter
                    SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        if (outputParam.Value != DBNull.Value)
                            lStatus = Convert.ToInt32(outputParam.Value);
                    }
                    catch (Exception ex)
                    {
                        lStatus = 0; // Optionally log the exception
                    }
                }
            }

            return lStatus;
        }

        public DataTable fnGetAlertSeatFull(string pOrderId)
        {
            DataTable tourDatable = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@OrderID", SqlDbType.VarChar, 50)
                        {
                            Value = string.IsNullOrEmpty(pOrderId) ? DBNull.Value : (object)pOrderId
                        }
                };

                tourDatable = SqlData.GetDataTableSP(StoredProcedures.GetAlertSeatFull_sp, param);

                return tourDatable;
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return null;
            }
        }


        /// <summary>
        /// /// Inserts an online passenger agent record into the database.
        /// </summary>
        /// <param name="lEmail"></param>
        /// <param name="lOrderID"></param>
        /// <param name="lName"></param>
        /// <param name="lAge"></param>
        /// <param name="lSex"></param>
        /// <returns></returns>
        public int fnInsertOnlinePassengerAgent(string lEmail, string lOrderID, string lName, short? lAge, char? lSex)
        {
            int lStatus = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertOnlinePassengerAgent_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    cmd.Parameters.AddWithValue("@i_Orderid", (object)lOrderID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_Name", (object)lName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_Age", (object)lAge ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_Sex", (object)lSex ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)lEmail ?? DBNull.Value);

                    // Output Parameter
                    SqlParameter outputParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        if (outputParam.Value != DBNull.Value)
                            lStatus = Convert.ToInt32(outputParam.Value);
                    }
                    catch (Exception ex)
                    {
                        lStatus = 0; // Optional: log exception
                    }
                }
            }

            return lStatus;
        }

        /// <summary>
        /// /// Inserts a record into the SmsSend_tbl table with the provided parameters.
        /// </summary>
        /// <param name="pCustID"></param>
        /// <param name="pMblNo"></param>
        /// <param name="pUserID"></param>
        /// <param name="pMsg"></param>
        /// <param name="pUserName"></param>
        /// <param name="pBranchCode"></param>
        /// <param name="TransactionType"></param>
        /// <param name="TicketNo"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public int fninsert_SmsSend_tbl(decimal? pCustID, string pMblNo, string pUserID, string pMsg, string pUserName, string pBranchCode, string TransactionType,
    string TicketNo, string OrderId)
        {
            int lStatus = 1;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.insert_SmsSend_tbl, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    cmd.Parameters.AddWithValue("@CustomerId", (object)pCustID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MobileNo", (object)pMblNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserIP", (object)pUserID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@message", (object)pMsg ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@username", (object)pUserName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@branchcode", (object)pBranchCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TransactionType", (object)TransactionType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TicketNo", (object)TicketNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@OrderId", (object)OrderId ?? DBNull.Value);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();  // No output parameter
                        lStatus = 0; // Success
                    }
                    catch (Exception ex)
                    {
                        lStatus = 1; // Error
                                     // Optional: log the exception
                    }
                }
            }

            return lStatus;
        }


        /// <summary>
        /// /// Retrieves the customer row ID from the database.
        /// </summary>
        /// <returns></returns>
        public int fnGetCustRowID()
        {
            int? lRowID = 0;

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetCustRowID_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Define output parameter
                    SqlParameter rowIdParam = new SqlParameter("@o_RowID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(rowIdParam);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        if (rowIdParam.Value != DBNull.Value)
                            lRowID = Convert.ToInt32(rowIdParam.Value);
                    }
                    catch (Exception ex)
                    {
                        lRowID = 0; // Default/failure value
                    }
                }
            }

            return Convert.ToInt32(lRowID);
        }



        /// <summary>
        /// // Retrieves the revised tour fare based on the provided tour ID.
        /// </summary>
        /// <param name="tourid"></param>
        /// <returns></returns>

        public DataTable fnGetRevisedTourFare(int tourid)
        {
            DataTable tourDatable = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@tourId", SqlDbType.Int)
                        {
                            Value = tourid
                        }
                };

                tourDatable = SqlData.GetDataTableSP(StoredProcedures.GetRevisedTourFare, param);
                return tourDatable;
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return null;
            }
        }


        /// <summary>
        /// /// Retrieves the discount tour master information based on the provided tour ID.
        /// </summary>
        /// <param name="tourid"></param>
        /// <returns></returns>

        public string fnGetDiscountTourMaster(int tourid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@tourId", SqlDbType.Int) { Value = tourid }
                };

                object result = SqlData.ExecuteScalarSP(StoredProcedures.GetDiscountTourMaster, param);
                return result != null ? result.ToString() : null;
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return null;
            }
        }



        /// <summary>
        /// /// Retrieves the revised fare with category based on the provided tour ID.
        /// </summary>
        /// <param name="tourid"></param>
        /// <returns></returns>
        public DataTable fnGetRevisedFareWithCategory(int tourid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@tourid", SqlDbType.Int) { Value = tourid }
                };

                return SqlData.GetDataTableSP(StoredProcedures.GetRevisedFareWithCategory, param);
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return null;
            }
        }
        /// <summary>
        /// /// Retrieves the fare with category based on the provided tour ID.
        /// </summary>
        /// <param name="tourid"></param>
        /// <returns></returns>
        public DataTable fnGetFareWithCategory(int tourid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
            new SqlParameter("@tourid", SqlDbType.Int) { Value = tourid }
                };

                return SqlData.GetDataTableSP("GetFareWithCategory", param);
            }
            catch (Exception ex)
            {
                // Optionally log or handle the exception
                return null;
            }
        }

    }
}
