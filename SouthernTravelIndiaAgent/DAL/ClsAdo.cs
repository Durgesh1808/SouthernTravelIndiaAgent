using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                    // Optionally log the exception
                    return null;
                }
                //finally
                //{
                //    if (dtResult != null)
                //    {
                //        dtResult.Dispose();
                //        dtResult = null;
                //    }
                //}
            }

            return dtResult;
        }
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
        public DataSet fnGetSeatArrangementDetail(long? lTourSrNo)
        {
            DataSet pdtDTSet = new DataSet();

            string connectionString = DataLib.getConnectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_GetSeatArrangements, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@tourserial", SqlDbType.BigInt)
                {
                    Value = (object)lTourSrNo ?? DBNull.Value
                });

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int tableIndex = 0;
                        do
                        {
                            DataTable dt = new DataTable($"Table{tableIndex++}");
                            dt.Load(reader);
                            pdtDTSet.Tables.Add(dt);
                        } while (!reader.IsClosed && reader.NextResult());
                    }

                    return pdtDTSet;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
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
        //public DataTable fncar_perkm_display_IsAC(int? lCarID)
        //{
        //    DataTable ldtRecSet = new DataTable();

        //    using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
        //    {
        //        try
        //        {
        //            conn.Open();

        //            using (SqlCommand cmd = new SqlCommand(StoredProcedures.car_perkm_display_IsAC, conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                // Input parameter
        //                cmd.Parameters.AddWithValue("@CarId", (object)lCarID ?? DBNull.Value);

        //                // Use SqlDataAdapter to fill DataTable
        //                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                {
        //                    da.Fill(ldtRecSet);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }

        //    return ldtRecSet;
        //}
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

    }
}