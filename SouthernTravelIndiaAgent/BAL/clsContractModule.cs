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
    public class clsContractModule
    {
        private string pvConnectionString = string.Empty;

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

    }
}