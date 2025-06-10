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
    public class STSPLOrOther
    {
        public STSPLOrOther()
        {
        }
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

    }
}