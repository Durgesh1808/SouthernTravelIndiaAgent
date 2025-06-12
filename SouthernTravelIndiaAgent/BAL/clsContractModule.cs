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
    /// /// This class contains business logic methods related to contract management, specifically for retrieving country names.
    /// </summary>
    public class clsContractModule
    {

        /// <summary>
        /// /// This property holds the connection string used to connect to the database.
        /// </summary>
        private string pvConnectionString = string.Empty;


        /// <summary>
        /// /// This property allows setting and getting the connection string for database operations.
        /// </summary>
        public string fldConnString
        {
            get
            {
                return pvConnectionString;
            }
            set
            {
                pvConnectionString = value;
            }
        }


        /// <summary>
        /// /// This method retrieves the name of a country based on the provided country ID.
        /// </summary>
        /// <param name="iCountryID"></param>
        /// <returns></returns>
        public DataListResponse<GetCountryName_SPResult> fnGetCountryName(int iCountryID)
        {
            DataListResponse<GetCountryName_SPResult> dataListResponse = new DataListResponse<GetCountryName_SPResult>();
            List<GetCountryName_SPResult> countryList = new List<GetCountryName_SPResult>();

            using (SqlConnection con = new SqlConnection(fldConnString))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetCountryName_SP, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@i_CountryID", iCountryID);

                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GetCountryName_SPResult item = new GetCountryName_SPResult();

                                // Replace with your actual column names and types
                                item.CountryID = reader["CountryID"] != DBNull.Value ? Convert.ToInt32(reader["CountryID"]) : 0;
                                item.CountryName = reader["CountryName"] != DBNull.Value ? reader["CountryName"].ToString() : string.Empty;

                                countryList.Add(item);
                            }
                        }

                        dataListResponse.ResultList = countryList;

                        if (countryList.Count > 0)
                        {
                            dataListResponse.Status = ClsCommon.fnGetRequestStatus(pStatus: true, pbException.SUCCESS);
                        }
                        else
                        {
                            dataListResponse.Status = ClsCommon.fnGetRequestStatus(pStatus: true, pbException.ERR_DATANOT_FOUND);
                        }
                    }
                    catch (Exception)
                    {
                        dataListResponse.Status = ClsCommon.fnGetRequestStatus(pStatus: false, pbException.ERR_CATCH_BLOCK);
                    }
                }
            }

            return dataListResponse;
        }

        /// <summary>
        /// /// This method retrieves the name of a city based on the provided city ID.
        /// </summary>
        /// <param name="iCityID"></param>
        /// <returns></returns>
        public DataListResponse<GetCityName_SPResult> fnGetCityName(int iCityID)
        {
            var dataListResponse = new DataListResponse<GetCityName_SPResult>();
            string connectionString = DataLib.getConnectionString(); // Or use your `fldConnString`

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetCityName_SP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@i_CityID", iCityID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<GetCityName_SPResult> resultList = new List<GetCityName_SPResult>();

                        while (reader.Read())
                        {
                            resultList.Add(new GetCityName_SPResult
                            {
                                CityID = reader["CityID"] != DBNull.Value ? Convert.ToInt32(reader["CityID"]) : 0,
                                StateID = reader["StateID"] != DBNull.Value ? Convert.ToInt32(reader["StateID"]) : 0,
                                CityName = reader["CityName"]?.ToString(),
                                Latitude = reader["Latitude"]?.ToString(),
                                Longitude = reader["Longitude"]?.ToString(),
                                BufferKM = reader["BufferKM"] != DBNull.Value ? Convert.ToInt32(reader["BufferKM"]) : 0,
                                IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"]),
                                IsOffice = reader["IsOffice"] != DBNull.Value && Convert.ToBoolean(reader["IsOffice"]),
                                CreatedOn = reader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedOn"]) : DateTime.MinValue,
                                CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : 0,
                                LastUpdatedOn = reader["LastUpdatedOn"] as DateTime?,
                                LastUpdatedBy = reader["LastUpdatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["LastUpdatedBy"]) : null
                            });
                        }

                        dataListResponse.ResultList = resultList;
                        dataListResponse.Status = resultList.Count > 0
                            ? ClsCommon.fnGetRequestStatus(true, pbException.SUCCESS)
                            : ClsCommon.fnGetRequestStatus(true, pbException.ERR_DATANOT_FOUND);
                    }
                }
            }
            catch (Exception)
            {
                dataListResponse.Status = ClsCommon.fnGetRequestStatus(false, pbException.ERR_CATCH_BLOCK);
            }

            return dataListResponse;
        }

    }
}