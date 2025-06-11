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
    ///  This class contains business logic methods related to GST data operations.
    /// </summary>
    /// <remarks>
    /// This class is responsible for interacting with the database to retrieve city lists based on state and search criteria,
    /// and for inserting final special tour information for agents.
    ///  It encapsulates the logic for these operations, making it easier to maintain and test.
    /// </remarks>
    public class GST_Data
    {

        /// <summary>
        /// /// This method retrieves a list of cities based on the provided state ID and search text.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="StateId"></param>
        /// <returns></returns>
        public List<GST_GetCityListByStateIdAndSearchedCityTextResult> GST_GetCityListByStateIdAndSearchedCityText(string search, int? StateId)
        {
            List<GST_GetCityListByStateIdAndSearchedCityTextResult> cityList = new List<GST_GetCityListByStateIdAndSearchedCityTextResult>();

            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GST_GetCityListByStateIdAndSearchedCityText, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@search", string.IsNullOrEmpty(search) ? (object)DBNull.Value : search);
                    cmd.Parameters.AddWithValue("@StateId", StateId.HasValue ? (object)StateId.Value : DBNull.Value);

                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GST_GetCityListByStateIdAndSearchedCityTextResult city = new GST_GetCityListByStateIdAndSearchedCityTextResult();

                                // Sample property mapping — replace with actual column/property names
                                city.CityID = reader["CityID"] != DBNull.Value ? reader["CityID"].ToString() : null;
                                city.CityName = reader["CityName"] != DBNull.Value ? reader["CityName"].ToString() : null;
                                city.StateID = reader["StateID"] != DBNull.Value ? Convert.ToInt32(reader["StateID"]) : 0;

                                cityList.Add(city);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }

            return cityList;
        }


        /// <summary>
        /// /// This method inserts final special tour information for an agent into the database.
        /// </summary>
        /// <param name="pclsspl_tourEnquiry_tbl"></param>
        /// <param name="pclsOnlineTrans_tbl"></param>
        /// <param name="lPws"></param>
        /// <param name="lTitle"></param>
        /// <param name="lAlternateMBLNO"></param>
        /// <param name="lCanSendPromotions"></param>
        /// <param name="totalChildWithoutMatress"></param>
        /// <param name="totalChildWithMatress"></param>
        /// <param name="Nationality"></param>
        /// <param name="Country"></param>
        /// <param name="AadharNo"></param>
        /// <param name="AadharNoImg"></param>
        /// <returns></returns>
        public long GST_InsertFinalSPLTourInfoAgent(GST_spl_tourEnquiry pclsspl_tourEnquiry_tbl, OnlineTransactionTable pclsOnlineTrans_tbl,
    string lPws, string lTitle, string lAlternateMBLNO, bool lCanSendPromotions, int totalChildWithoutMatress,
    int totalChildWithMatress, string Nationality, string Country, string AadharNo, string AadharNoImg)
        {
            long lStatus = 0;
            using (SqlConnection con = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GST_InsertFinalSPLTourInfoAgent_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var T = pclsspl_tourEnquiry_tbl;
                    var OT = pclsOnlineTrans_tbl;

                    // Add all parameters
                    cmd.Parameters.AddWithValue("@Email", (object)T.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FirstName", (object)T.FirstName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object)T.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@JourneyDate", (object)T.JourneyDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TourName", (object)T.TourName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TotalPax", (object)T.TotalPax ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CarType", (object)T.CarType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FareCategoryType", (object)T.FareCategoryType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PassengerPerVehicle", (object)T.PassengerPerVehicle ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", (object)T.Status ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tourid", (object)T.TourId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@fare", (object)T.Fare ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SingleSharing", (object)T.SingleSharing ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PickUpVeh", (object)T.PickUpVeh ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PickVehNo", (object)T.PickVehNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PickTime", (object)T.PickTime ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DropVeh", (object)T.DropVeh ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DropVehNo", (object)T.DropVehNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DropTime", (object)T.DropTime ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mobile", (object)T.Mobile ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object)T.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CCCharges", (object)T.CCCharges ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Comments", (object)T.Comments ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@station", (object)T.station ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PkStation", (object)T.PkStation ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BranchCode", (object)T.BranchCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentId", (object)T.AgentId ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@TransType", (object)OT.TransType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentCredit", (object)OT.AgentCredit ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AvailableBalance", (object)OT.AvailableBalance ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AgentDebit", (object)OT.AgentDebit ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TicketAmount", (object)OT.TicketAmount ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Commission", (object)OT.Commission ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserName", (object)OT.UserName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TransState", (object)OT.TransState ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PaymentMode", (object)OT.PaymentMode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ServiceTax", (object)OT.ServiceTax ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TDS", (object)OT.TDS ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@i_PWS", (object)lPws ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_Title", (object)lTitle ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AlternativeNo", (object)lAlternateMBLNO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@debit", (object)OT.Debit ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@credit", (object)OT.Credit ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_CanSendPromotions", lCanSendPromotions);

                    SqlParameter pOut = new SqlParameter("@o_ReturnValue", SqlDbType.BigInt)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(pOut);

                    cmd.Parameters.AddWithValue("@I_TotalChildWithMatress", totalChildWithMatress);
                    cmd.Parameters.AddWithValue("@I_TotalChildwithoutMatress", totalChildWithoutMatress);

                    cmd.Parameters.AddWithValue("@i_State", (object)T.State ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_City", (object)T.City ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_ZipCode", (object)T.ZipCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_IsGSTIN", (object)T.ISGSITIN ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_CustomerGSTIN", (object)T.CustomerGSTIN ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_GSTHolderName", (object)T.GstHolderName ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@Nationality", (object)Nationality ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Country", (object)Country ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Aadharno", (object)AadharNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AadharnoImg", (object)AadharNoImg ?? DBNull.Value);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        if (pOut.Value != DBNull.Value)
                            lStatus = Convert.ToInt64(pOut.Value);
                    }
                    catch (Exception ex)
                    {
                        // log ex if needed
                        lStatus = 0;
                    }
                }
            }
            return lStatus;
        }

    }
}