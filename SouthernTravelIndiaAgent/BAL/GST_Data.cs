using SouthernTravelIndiaAgent.Common;
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


        /// <summary>
        /// /// This method retrieves customer details based on the provided search value and row ID.
        /// </summary>
        /// <param name="lSearchValue"></param>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public DataTable GST_fnGetCustomerDetail(string lSearchValue, int rowid)
        {
            DataTable pdtTable = new DataTable();

            // Use your actual connection string provider here
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedures.GST_GetCustomerDetail_sp, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@i_EmailorMbNo", lSearchValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@i_Rowid", rowid);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(pdtTable);
                        }
                    }
                }

                return pdtTable;
            }
            catch (Exception ex)
            {
                // Log exception if needed
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


        /// <summary>
        /// /// This method inserts final account booking information into the database.
        /// </summary>
        /// <param name="BHT"></param>
        /// <param name="OTT"></param>
        /// <param name="sCustName"></param>
        /// <param name="sAddress"></param>
        /// <param name="sState"></param>
        /// <param name="sPhone"></param>
        /// <param name="sMobile"></param>
        /// <param name="sAMobile"></param>
        /// <param name="sEmail"></param>
        /// <param name="sCompany"></param>
        /// <param name="sCity"></param>
        /// <param name="lRoomID"></param>
        /// <param name="LRoomFare"></param>
        /// <param name="lExtraBed"></param>
        /// <param name="pFareID"></param>
        /// <param name="lXetraFare"></param>
        /// <param name="lCanSendPromotions"></param>
        /// <param name="lOccupationId"></param>
        /// <param name="lOccupation"></param>
        /// <param name="lTotalCWB"></param>
        /// <param name="lCWBFare"></param>
        /// <param name="lgueststring"></param>
        /// <param name="lOrderID"></param>
        /// <param name="lTicketCode"></param>
        /// <param name="IsGSTIN"></param>
        /// <param name="CustomerGSTIN"></param>
        /// <param name="GSTINHolderName"></param>
        /// <param name="ZipCode"></param>
        /// <param name="Nationality"></param>
        /// <param name="Country"></param>
        /// <param name="AadharNo"></param>
        /// <param name="AadharNoImg"></param>
        /// <returns></returns>
        public string GST_fnInsertFinalAccBookingInfo(
    BranchHotel_Tbl BHT, clsTransactionTable OTT,
    string sCustName, string sAddress, string sState, string sPhone, string sMobile, string sAMobile,
    string sEmail, string sCompany, string sCity, decimal? lRoomID, decimal? LRoomFare,
    int? lExtraBed, string pFareID, decimal? lXetraFare, bool lCanSendPromotions, int lOccupationId,
    string lOccupation, int lTotalCWB, decimal lCWBFare, string lgueststring,
    ref string lOrderID, ref string lTicketCode, bool IsGSTIN, string CustomerGSTIN, string GSTINHolderName,
    string ZipCode, string Nationality, string Country, string AadharNo, string AadharNoImg)
        {
            string connStr = DataLib.getConnectionString();
            string status = "0";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("GST_InsertFinalAccBookingInfo_sp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // --- Input Parameters ---
                    cmd.Parameters.AddWithValue("@CustName", sCustName);
                    cmd.Parameters.AddWithValue("@Address", sAddress);
                    cmd.Parameters.AddWithValue("@State", sState);
                    cmd.Parameters.AddWithValue("@Phone", sPhone);
                    cmd.Parameters.AddWithValue("@Mobile", sMobile);
                    cmd.Parameters.AddWithValue("@AMobile", sAMobile);
                    cmd.Parameters.AddWithValue("@Email", sEmail);
                    cmd.Parameters.AddWithValue("@Company", sCompany);
                    cmd.Parameters.AddWithValue("@City", sCity);
                    cmd.Parameters.AddWithValue("@RoomID", lRoomID.HasValue ? (object)lRoomID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Roomfare", LRoomFare.HasValue ? (object)LRoomFare.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ExtraRoom", lExtraBed.HasValue ? (object)lExtraBed.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ExtraRoomFare", lXetraFare.HasValue ? (object)lXetraFare.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Noofadults", Convert.ToInt32(BHT.noofadults));
                    cmd.Parameters.AddWithValue("@Noofchildren", Convert.ToInt32(BHT.noofchildren));
                    cmd.Parameters.AddWithValue("@Noofrooms", BHT.noofrooms);
                    cmd.Parameters.AddWithValue("@Deluxe", BHT.Deluxe);
                    cmd.Parameters.AddWithValue("@SuperDeluxe", BHT.SuperDeluxe);
                    cmd.Parameters.AddWithValue("@Executive", BHT.Executive);
                    cmd.Parameters.AddWithValue("@Royal", BHT.Royal);
                    cmd.Parameters.AddWithValue("@Totalamount", BHT.Totalamount);
                    cmd.Parameters.AddWithValue("@STaxValue", BHT.STaxValue);
                    cmd.Parameters.AddWithValue("@AmtWithTax", BHT.AmtWithTax);
                    cmd.Parameters.AddWithValue("@Discount", BHT.Discount);
                    cmd.Parameters.AddWithValue("@Advance", BHT.Advance);
                    cmd.Parameters.AddWithValue("@Paymode", string.IsNullOrEmpty(BHT.Paymode) ? (object)DBNull.Value : BHT.Paymode);
                    cmd.Parameters.AddWithValue("@CheckNo", string.IsNullOrEmpty(BHT.CheckNo) ? (object)DBNull.Value : BHT.CheckNo);
                    cmd.Parameters.AddWithValue("@Bankname", string.IsNullOrEmpty(BHT.bankname) ? (object)DBNull.Value : BHT.bankname);
                    cmd.Parameters.AddWithValue("@Depttime", (object)BHT.depttime);
                    cmd.Parameters.AddWithValue("@Arrtime", (object)BHT.arrtime);
                    cmd.Parameters.AddWithValue("@Username", BHT.Username);
                    cmd.Parameters.AddWithValue("@Branchcode", BHT.Branchcode);
                    cmd.Parameters.AddWithValue("@BookingRoomTypes", string.IsNullOrEmpty(BHT.BookingRoomTypes) ? (object)DBNull.Value : BHT.BookingRoomTypes);
                    cmd.Parameters.AddWithValue("@Remarks", string.IsNullOrEmpty(BHT.Remarks) ? (object)DBNull.Value : BHT.Remarks);
                    cmd.Parameters.AddWithValue("@Noofdays", BHT.noofdays);
                    cmd.Parameters.AddWithValue("@AgentId", BHT.AgentId);
                    cmd.Parameters.AddWithValue("@FareID", pFareID);
                    cmd.Parameters.AddWithValue("@Occupancy", BHT.Occupancy);
                    cmd.Parameters.AddWithValue("@CanSendPromotions", lCanSendPromotions);
                    cmd.Parameters.AddWithValue("@I_OccupationId", lOccupationId);
                    cmd.Parameters.AddWithValue("@I_Occupation", lOccupation);
                    cmd.Parameters.AddWithValue("@I_Guestdetials", lgueststring);
                    cmd.Parameters.AddWithValue("@I_ChildWBedFare", lCWBFare);
                    cmd.Parameters.AddWithValue("@I_TotalChildWBed", lTotalCWB);

                    // Transaction Details
                    cmd.Parameters.AddWithValue("@TicketAmount", OTT.fldTicketAmount);
                    cmd.Parameters.AddWithValue("@PaymentMode", OTT.fldPaymentMode);
                    cmd.Parameters.AddWithValue("@Number", OTT.fldNumber);
                    cmd.Parameters.AddWithValue("@Credit", OTT.fldCredit);
                    cmd.Parameters.AddWithValue("@ServiceTax", OTT.fldServiceTax);
                    cmd.Parameters.AddWithValue("@ChqDate", string.IsNullOrEmpty(OTT.fldChqDate) ? (object)DBNull.Value : OTT.fldChqDate);
                    cmd.Parameters.AddWithValue("@TransType", OTT.fldTransType);
                    cmd.Parameters.AddWithValue("@AgentCredit", OTT.fldAgentCredit);
                    cmd.Parameters.AddWithValue("@AvailableBalance", OTT.fldAvailableBalance);
                    cmd.Parameters.AddWithValue("@AgentDebit", OTT.fldAgentDebit);
                    cmd.Parameters.AddWithValue("@Commission", OTT.fldCommission);
                    cmd.Parameters.AddWithValue("@Debit", OTT.fldDebit);
                    cmd.Parameters.AddWithValue("@ImpersonatingUserName", OTT.fldImpersonatingUserName);
                    cmd.Parameters.AddWithValue("@ImpersonatingBranchCode", OTT.fldImpersonatingBranchCode);
                    cmd.Parameters.AddWithValue("@Cashier", OTT.fldCashier);
                    cmd.Parameters.AddWithValue("@TransState", OTT.fldTransState);
                    cmd.Parameters.AddWithValue("@Status", OTT.fldStatus);
                    cmd.Parameters.AddWithValue("@TDS", OTT.fldTDS);
                    cmd.Parameters.AddWithValue("@OT_Discount", OTT.fldDiscount);

                    cmd.Parameters.AddWithValue("@HotelID", BHT.HotelID);

                    // GSTIN Info
                    cmd.Parameters.AddWithValue("@I_IsGSTIN", IsGSTIN);
                    cmd.Parameters.AddWithValue("@I_CustomerGSTIN", CustomerGSTIN);
                    cmd.Parameters.AddWithValue("@I_GSTHolderName", GSTINHolderName);
                    cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
                    cmd.Parameters.AddWithValue("@Nationality", Nationality);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.Parameters.AddWithValue("@Aadharno", AadharNo);
                    cmd.Parameters.AddWithValue("@AadharnoImg", AadharNoImg);

                    // Output Parameters
                    cmd.Parameters.Add("@O_OrderID", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@O_TicketID", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@O_RowID", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        lOrderID = Convert.ToString(cmd.Parameters["@O_OrderID"].Value);
                        lTicketCode = Convert.ToString(cmd.Parameters["@O_TicketID"].Value);
                        status = Convert.ToString(cmd.Parameters["@O_RowID"].Value); // used as return status
                    }
                    catch (Exception ex)
                    {
                        // log ex.Message if needed
                        status = "0";
                    }
                }
            }
            return status;
        }


        /// <summary>
        /// /// This method retrieves a list of cities based on the provided state name and search text.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="StateName"></param>
        /// <returns></returns>
        public List<GST_GetCityListByStateNameAndSearchedCityTextResult> GST_GetCityListByStateNameAndSearchedCityText(string search, string StateName)
        {
            List<GST_GetCityListByStateNameAndSearchedCityTextResult> cityList = new List<GST_GetCityListByStateNameAndSearchedCityTextResult>();
            string connectionString = DataLib.getConnectionString(); // Replace if needed

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GST_GetCityListByStateNameAndSearchedCityText, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@str", search);
                    cmd.Parameters.AddWithValue("@State", StateName);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GST_GetCityListByStateNameAndSearchedCityTextResult item = new GST_GetCityListByStateNameAndSearchedCityTextResult
                            {
                                CityID = reader["CityID"]?.ToString(),
                                CityName = reader["CityName"]?.ToString()
                            };
                            cityList.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally log the error
                cityList = null;
            }

            return cityList;
        }


        /// <summary>
        /// /// This method checks if a customer detail exists for the given Order ID and Row ID.
        /// </summary>
        /// <param name="lOrdeID"></param>
        /// <param name="lRowID"></param>
        /// <returns></returns>
        public DataTable GST_fnExistCustomerDetail(string lOrdeID, int? lRowID)
        {
            DataTable seatDataTable = null;

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
            new SqlParameter("@i_OrderID", SqlDbType.VarChar, 50)
            {
                Value = !string.IsNullOrEmpty(lOrdeID) ? (object)lOrdeID : DBNull.Value
            },
            new SqlParameter("@i_RowID", SqlDbType.Int)
            {
                Value = lRowID.HasValue ? (object)lRowID.Value : DBNull.Value
            }
                };

                seatDataTable = SqlData.GetDataTableSP(StoredProcedures.GST_ExistCustomerDetail_sp, param);
                return seatDataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        /// <summary>
        /// /// This method inserts or updates customer details in the database.
        /// </summary>
        /// <param name="pclsOnlineCustomer_tbl"></param>
        /// <param name="lOrderID"></param>
        /// <param name="AadharNo"></param>
        /// <param name="AadharNoImg"></param>
        /// <returns></returns>
        public int GST_fnInsertUpdateCustomerDetail(GST_OnlineCustomer pclsOnlineCustomer_tbl, string lOrderID, string AadharNo, string AadharNoImg)
        {
            int? lStatus = 0;
            string lPanNoImg = "";

            using (SqlConnection conn = new SqlConnection(DataLib.getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GST_InsertUpdateCustomerDetail_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    cmd.Parameters.AddWithValue("@i_RowID", (object)pclsOnlineCustomer_tbl.RowId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_FirstName", (object)pclsOnlineCustomer_tbl.FirstName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_Add1", (object)pclsOnlineCustomer_tbl.Addr1 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_State", (object)pclsOnlineCustomer_tbl.state ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_Phone", (object)pclsOnlineCustomer_tbl.PhoneNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_Mobile", (object)pclsOnlineCustomer_tbl.Mobile ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AlterMobileNo", (object)pclsOnlineCustomer_tbl.AlternativeNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_EmailID", (object)pclsOnlineCustomer_tbl.email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_FaceBookId", (object)pclsOnlineCustomer_tbl.FaceBookID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_PWS", (object)pclsOnlineCustomer_tbl.password ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_OrderID", (object)lOrderID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@I_CanSendPromotions", (object)pclsOnlineCustomer_tbl.CanSendPromotions ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_PanNo", (object)pclsOnlineCustomer_tbl.PanNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_IsPanNoVerify", (object)pclsOnlineCustomer_tbl.IsPanNoVerify ?? DBNull.Value);

                    // Pan Image (ref input/output)
                    SqlParameter panImgParam = new SqlParameter("@i_PanNoImg", SqlDbType.VarChar, 50)
                    {
                        Direction = ParameterDirection.InputOutput,
                        Value = string.IsNullOrEmpty(lPanNoImg) ? (object)DBNull.Value : lPanNoImg
                    };
                    cmd.Parameters.Add(panImgParam);

                    // Output return value
                    SqlParameter outputStatusParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputStatusParam);

                    // More input parameters
                    cmd.Parameters.AddWithValue("@i_IsGSTIN", (object)pclsOnlineCustomer_tbl.ISGSITIN ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_CustomerGSTIN", (object)pclsOnlineCustomer_tbl.CustomerGSTIN ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_GSTINHolderName", (object)pclsOnlineCustomer_tbl.GstHolderName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_Country", (object)pclsOnlineCustomer_tbl.Country ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_City", (object)pclsOnlineCustomer_tbl.City ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@i_ZipCode", (object)pclsOnlineCustomer_tbl.zipcode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Nationality", (object)pclsOnlineCustomer_tbl.Nationality ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Aadharno", (object)AadharNo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AadharnoImg", (object)AadharNoImg ?? DBNull.Value);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Get the output values
                        if (outputStatusParam.Value != DBNull.Value)
                            lStatus = Convert.ToInt32(outputStatusParam.Value);

                        if (panImgParam.Value != DBNull.Value)
                            lPanNoImg = panImgParam.Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        lStatus = -2; // You can log the error if needed
                    }
                }
            }

            return Convert.ToInt32(lStatus);
        }


    }
}