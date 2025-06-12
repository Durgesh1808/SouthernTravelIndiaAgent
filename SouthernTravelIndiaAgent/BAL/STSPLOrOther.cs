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
    ///  This class contains business logic methods related to special tours and other functionalities.
    /// </summary>
    /// <remarks>
    ///    This class provides methods to interact with the database for retrieving tour master details, instant booking information, and discounts.
    ///     It encapsulates the logic for connecting to the database, executing stored procedures, and returning results in a structured manner.
    ///    The methods handle nullable parameters and return types, ensuring flexibility in usage.
    ///   </remarks>
    public class STSPLOrOther
    {


        /// <summary>
        /// /// Default constructor for the STSPLOrOther class.
        /// </summary>
        public STSPLOrOther()
        {
        }


        /// <summary>
        /// // This method retrieves the special tour master details based on the provided Tour ID.
        /// </summary>
        /// <param name="lTourID"></param>
        /// <returns></returns>
        public DataTable fnGetSPLTourMaster(int? lTourID)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                string connStr = DataLib.getConnectionString();
                con = new SqlConnection(connStr);
                cmd = new SqlCommand(StoredProcedures.GetSPLTourMaster_sp, con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (lTourID.HasValue)
                    cmd.Parameters.AddWithValue("@i_TourID", lTourID.Value);
                else
                    cmd.Parameters.AddWithValue("@i_TourID", DBNull.Value);

                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                // Log error if necessary
                return null;
            }
            finally
            {
                if (da != null) da.Dispose();
                if (cmd != null) cmd.Dispose();
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Dispose();
                }
            }
        }



        /// <summary>
        ///  This method retrieves instant booking information based on the provided Tour ID.
        /// </summary>
        /// <param name="lTourID"></param>
        /// <returns></returns>
        public string fnGetInstentBooking(int? lTourID)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            string lReturnValue = "0";

            try
            {
                string connStr = DataLib.getConnectionString();
                con = new SqlConnection(connStr);
                cmd = new SqlCommand(StoredProcedures.GetInstentBooking_sp, con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameter
                cmd.Parameters.AddWithValue("@TourId", lTourID.HasValue ? (object)lTourID.Value : DBNull.Value);

                // Output parameter
                SqlParameter returnParam = new SqlParameter("@Returnvalue", SqlDbType.VarChar, 25);
                returnParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(returnParam);

                con.Open();
                cmd.ExecuteNonQuery();

                lReturnValue = returnParam.Value?.ToString() ?? "0";
            }
            catch (Exception ex)
            {
                // Log exception if needed
                lReturnValue = "0";
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Dispose();
                }
            }

            return lReturnValue;
        }


        /// <summary>
        /// // This method retrieves discount information based on the provided Tour Type and Tour ID.
        /// </summary>
        /// <param name="pTourType"></param>
        /// <param name="pTourID"></param>
        /// <returns></returns>
        public List<GetDiscount_spResult> fnGetDiscount(int pTourType, int pTourID)
        {
            var results = new List<GetDiscount_spResult>();

            string connectionString = DataLib.getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetDiscount_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters (nullable if needed)
                    cmd.Parameters.AddWithValue("@i_TourTypeID", pTourType);
                    cmd.Parameters.AddWithValue("@i_TourID", pTourID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new GetDiscount_spResult
                            {
                                RowID = reader.GetInt32(reader.GetOrdinal("RowID")),
                                TourTypeID = reader.IsDBNull(reader.GetOrdinal("TourTypeID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("TourTypeID")),
                                TourID = reader.IsDBNull(reader.GetOrdinal("TourID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("TourID")),
                                HotelID = reader.IsDBNull(reader.GetOrdinal("HotelID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("HotelID")),
                                VendorID = reader.IsDBNull(reader.GetOrdinal("VendorID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("VendorID")),
                                RoomTypeID = reader.IsDBNull(reader.GetOrdinal("RoomTypeID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                                FlatDiscount = reader.IsDBNull(reader.GetOrdinal("FlatDiscount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("FlatDiscount")),
                                PerDiscount = reader.IsDBNull(reader.GetOrdinal("PerDiscount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("PerDiscount")),
                                IsFlat = reader.IsDBNull(reader.GetOrdinal("IsFlat")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("IsFlat")),
                                IsBooking = reader.IsDBNull(reader.GetOrdinal("IsBooking")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("IsBooking")),
                                IsCheckIn = reader.IsDBNull(reader.GetOrdinal("IsCheckIn")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("IsCheckIn")),
                                IsPer = reader.IsDBNull(reader.GetOrdinal("IsPer")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("IsPer")),
                                BookingFrom = reader.IsDBNull(reader.GetOrdinal("BookingFrom")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("BookingFrom")),
                                BookingTo = reader.IsDBNull(reader.GetOrdinal("BookingTo")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("BookingTo")),
                                CheckInFrom = reader.IsDBNull(reader.GetOrdinal("CheckInFrom")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CheckInFrom")),
                                CheckOutTo = reader.IsDBNull(reader.GetOrdinal("CheckOutTo")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CheckOutTo")),
                                IsAgent = reader.IsDBNull(reader.GetOrdinal("IsAgent")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("IsAgent")),
                                IsBranch = reader.IsDBNull(reader.GetOrdinal("IsBranch")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("IsBranch")),
                                IsOnline = reader.IsDBNull(reader.GetOrdinal("IsOnline")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("IsOnline")),
                                IsDiscountActive = reader.IsDBNull(reader.GetOrdinal("IsDiscountActive")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("IsDiscountActive")),
                                Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                                CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                                LastUpdateBy = reader.IsDBNull(reader.GetOrdinal("LastUpdateBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("LastUpdateBy")),
                                LastUpdatedOn = reader.IsDBNull(reader.GetOrdinal("LastUpdatedOn")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LastUpdatedOn"))
                            };

                            results.Add(item);
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// /// This method retrieves accommodation discount information based on the provided Hotel ID and Room Type.
        /// </summary>
        /// <param name="pHotelID"></param>
        /// <param name="pRoomType"></param>
        /// <returns></returns>
        public DataTable fnGetAccDiscount(int pHotelID, int pRoomType)
        {
            string connStr = DataLib.getConnectionString();
            DataTable dtResult = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAccDiscount_sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HotelID", pHotelID);
                    cmd.Parameters.AddWithValue("@RoomTypeID", pRoomType);

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtResult);
                        }
                        return dtResult;
                    }
                    catch (Exception ex)
                    {
                        // Optionally log ex.Message
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
            }
        }

        /// <summary>
        /// /// This method retrieves hotel names based on the provided State ID and City ID using ADO.NET.
        /// </summary>
        /// <param name="pStateID"></param>
        /// <param name="pCityID"></param>
        /// <returns></returns>
        public List<GetHotelNames_SPResult> fnGetHotelNames(int pStateID, int pCityID)
        {
            List<GetHotelNames_SPResult> hotels = new List<GetHotelNames_SPResult>();
            string connStr = DataLib.getConnectionString();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("GetHotelNames_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@i_StateID", pStateID);
                    cmd.Parameters.AddWithValue("@i_CityID", pCityID);

                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GetHotelNames_SPResult hotel = new GetHotelNames_SPResult
                                {
                                    HotelID = reader.GetInt32(reader.GetOrdinal("HotelID")),
                                    VendorID = reader.GetInt32(reader.GetOrdinal("VendorID")),
                                    HotelName = reader["HotelName"]?.ToString(),
                                    HotelDesc = reader["HotelDesc"]?.ToString(),
                                    CountryID = reader["CountryID"] as int?,
                                    StateID = reader["StateID"] as int?,
                                    CityID = reader["CityID"] as int?,
                                    HotelAddress = reader["HotelAddress"]?.ToString(),
                                    Pincode = reader["Pincode"]?.ToString(),
                                    EmailID = reader["EmailID"]?.ToString(),
                                    WebSite = reader["WebSite"]?.ToString(),
                                    ISPaymentToHotel = reader["ISPaymentToHotel"] as bool?,
                                    PaymentTo = reader["PaymentTo"] as int?,
                                    BankName = reader["BankName"]?.ToString(),
                                    BankBranchName = reader["BankBranchName"]?.ToString(),
                                    Number = reader["Number"]?.ToString(),
                                    IFSCCode = reader["IFSCCode"]?.ToString(),
                                    TypeID = reader["TypeID"] as int?,
                                    Priority = reader["Priority"] as int?,
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                                    CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                    LastUpdatedOn = reader["LastUpdatedOn"] as DateTime?,
                                    LastUpdatedBy = reader["LastUpdatedBy"] as int?,
                                    PhoneNo = reader["PhoneNo"]?.ToString(),
                                    AlternatePhoneNo = reader["AlternatePhoneNo"]?.ToString(),
                                    FaxNo = reader["FaxNo"]?.ToString(),
                                    MobileNo = reader["MobileNo"]?.ToString(),
                                    ChildAgeRange = reader["ChildAgeRange"]?.ToString(),
                                    Latitude = reader["Latitude"]?.ToString(),
                                    Longitude = reader["Longitude"]?.ToString(),
                                    HotelLocation = reader["HotelLocation"]?.ToString(),
                                    GuaranteeAmount = reader["GuaranteeAmount"]?.ToString(),
                                    GuaranteeByBank = reader["GuaranteeByBank"]?.ToString(),
                                    GuaranteeByBankBranch = reader["GuaranteeByBankBranch"]?.ToString(),
                                    PaymentOption = reader["PaymentOption"] as bool?,
                                    AlternamtePhoneNo2 = reader["AlternamtePhoneNo2"]?.ToString(),
                                    ChildAgeRangeMin = reader["ChildAgeRangeMin"] as int?,
                                    ChildAgeRangeMax = reader["ChildAgeRangeMax"] as int?,
                                    ContactPerson = reader["ContactPerson"]?.ToString(),
                                    Tax = reader["Tax"] as decimal?,
                                    ChequeBankName = reader["ChequeBankName"]?.ToString(),
                                    ChequeBankBranchName = reader["ChequeBankBranchName"]?.ToString(),
                                    ChequeNo = reader["ChequeNo"]?.ToString(),
                                    TaxOption = reader["TaxOption"]?.ToString(),
                                    TOBEUSEDIN = reader["TOBEUSEDIN"]?.ToString(),
                                    IsPreferred = reader["IsPreferred"] as int?,
                                    Is24Hrs = reader["Is24Hrs"] as bool?,
                                    CheckInTime = reader["CheckInTime"] as DateTime?,
                                    BuffCheckIn = reader["BuffCheckIn"] as DateTime?,
                                    CheckOutTime = reader["CheckOutTime"] as DateTime?,
                                    BuffCheckOut = reader["BuffCheckOut"] as DateTime?,
                                    LTax = reader["LTax"] as decimal?,
                                    STax = reader["STax"] as decimal?
                                };
                                hotels.Add(hotel);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log or handle error
                        return null;
                    }
                }
            }

            return hotels;
        }



        /// <summary>
        /// /// This method retrieves the payment modes available in the system using ADO.NET.
        /// </summary>
        /// <returns></returns>
        public DataTable fnGetPaymentMode()
        {
            DataTable dtPaymentMode = new DataTable();
            string connStr = DataLib.getConnectionString();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_GetPaymentMode, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtPaymentMode);
                        }
                        return dtPaymentMode;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// /// This method retrieves hotel room types using ADO.NET.
        /// </summary>
        /// <returns></returns>
        public DataTable fnHotelRoomTypes()
        {
            DataTable dt = new DataTable();
            string connStr = DataLib.getConnectionString();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.sp_HotelRoomTypes, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

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
        /// /// This method retrieves the payment modes for tickets using ADO.NET.
        /// </summary>
        /// <returns></returns>
        public List<GetHotelRooms_Fare_SPResult> fnGetHotelRooms_Fares(int pHotelID, DateTime pCheckInDate)
        {
            List<GetHotelRooms_Fare_SPResult> resultList = new List<GetHotelRooms_Fare_SPResult>();
            string connStr = DataLib.getConnectionString();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetHotelRooms_Fare_SP, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@i_HotelID", pHotelID);
                    cmd.Parameters.AddWithValue("@i_CheckinDate", pCheckInDate);

                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new GetHotelRooms_Fare_SPResult
                                {
                                    TypeID = reader["TypeID"] != DBNull.Value ? Convert.ToInt32(reader["TypeID"]) : 0,
                                    TypeName = reader["TypeName"]?.ToString(),
                                    SingleOccupancy = reader["SingleOccupancy"] as bool?,
                                    DoubleOccupancy = reader["DoubleOccupancy"] as bool?,
                                    FourOccupancy = reader["FourOccupancy"] as bool?,
                                    OptionalName = reader["OptionalName"]?.ToString(),
                                    SeasonID = reader["SeasonID"] != DBNull.Value ? Convert.ToInt32(reader["SeasonID"]) : 0,
                                    SeasonName = reader["SeasonName"]?.ToString(),
                                    FromDate = reader["FromDate"] as DateTime?,
                                    ToDate = reader["ToDate"] as DateTime?,
                                    FareID = reader["FareID"] != DBNull.Value ? Convert.ToInt32(reader["FareID"]) : 0,
                                    MealPlanID = reader["MealPlanID"] as int?,
                                    MealPlan = reader["MealPlan"]?.ToString(),
                                    SglOccSellingPrice = reader["SglOccSellingPrice"] as decimal?,
                                    SellingPrice = reader["SellingPrice"] as decimal?,
                                    ExtraBedSellingPrice = reader["ExtraBedSellingPrice"] as decimal?,
                                    FourOccSellingPrice = reader["FourOccSellingPrice"] as decimal?,
                                    FourOccExtraBedSell = reader["FourOccExtraBedSell"] as decimal?,
                                    CWBSellingPrice = reader["CWBSellingPrice"] != DBNull.Value ? Convert.ToDecimal(reader["CWBSellingPrice"]) : 0
                                };

                                resultList.Add(model);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // handle or log error
                        return null;
                    }
                }
            }

            return resultList;
        }


        /// <summary>
        /// /// This method retrieves room type occupancy details for a specific hotel using ADO.NET.
        /// </summary>
        /// <param name="pHotelID"></param>
        /// <returns></returns>
        public List<GetRoomTypeOccupancy_SPResult> fnGetRoomTypeOccupancy(int pHotelID)
        {
            List<GetRoomTypeOccupancy_SPResult> resultList = new List<GetRoomTypeOccupancy_SPResult>();
            string connectionString = DataLib.getConnectionString(); // use your actual connection string method

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetRoomTypeOccupancy_SP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@i_HotelID", pHotelID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = new GetRoomTypeOccupancy_SPResult
                            {
                                TypeID = reader["TypeID"] != DBNull.Value ? Convert.ToInt32(reader["TypeID"]) : 0,
                                TypeName = reader["TypeName"]?.ToString(),
                                SingleOccupancy = reader["SingleOccupancy"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["SingleOccupancy"]) : null,
                                DoubleOccupancy = reader["DoubleOccupancy"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["DoubleOccupancy"]) : null,
                                FourOccupancy = reader["FourOccupancy"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["FourOccupancy"]) : null
                            };
                            resultList.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                resultList = null;
            }

            return resultList;
        }

        /// <summary>
        /// /// This method retrieves the tour ticket banner details based on the provided Tour ID and Tour Type Code.
        /// </summary>
        /// <param name="pTourID"></param>
        /// <param name="pTourTypeCode"></param>
        /// <returns></returns>
        public DataTable fnGetTourTicketBanner(int? pTourID, string pTourTypeCode)
        {
            DataTable dtResult = new DataTable();
            string connectionString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetTourTicketBanner_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameters
                    cmd.Parameters.AddWithValue("@TourID", (object)pTourID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TourTypeCode", (object)pTourTypeCode ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtResult);
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

    }
}