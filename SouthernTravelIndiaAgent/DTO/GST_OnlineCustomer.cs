using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class GST_OnlineCustomer : OnlineCustomer
    {
        public bool ISGSITIN { get; set; }
        public string CustomerGSTIN { get; set; }
        public string GstHolderName { get; set; }
        public string AadharNo { get; set; }
        public string AadharImg { get; set; }

        // Static method to create from SqlDataReader
        public static GST_OnlineCustomer FromDataReader(SqlDataReader reader)
        {
            return new GST_OnlineCustomer
            {
                RowId = reader["RowId"] != DBNull.Value ? Convert.ToInt32(reader["RowId"]) : 0,
                FirstName = reader["FirstName"] as string,
                LastName = reader["LastName"] as string,
                Email = reader["Email"] as string,
                Password = reader["Password"] as string,
                Addr1 = reader["Addr1"] as string,
                Addr2 = reader["Addr2"] as string,
                City = reader["City"] as string,
                State = reader["State"] as string,
                Country = reader["Country"] as string,
                Zipcode = reader["Zipcode"] as string,
                PhoneNo = reader["PhoneNo"] as string,
                AlternativeName = reader["AlternativeName"] as string,
                RelativePhoneNo = reader["RelativePhoneNo"] as string,
                AlternativeNo = reader["AlternativeNo"] as string,
                Mobile = reader["Mobile"] as string,
                Fax = reader["Fax"] as string,
                DOB = reader["DOB"] != DBNull.Value ? (DateTime?)reader["DOB"] : null,
                Company = reader["Company"] as string,
                Occupation = reader["Occupation"] as string,
                Remark = reader["Remark"] as string,
                MaritalStatus = reader["MaritalStatus"] as string,
                DOA = reader["DOA"] != DBNull.Value ? (DateTime?)reader["DOA"] : null,
                Sex = reader["Sex"] != DBNull.Value ? Convert.ToChar(reader["Sex"]) : (char?)null,
                CallerID = reader["CallerID"] as string,
                CreatedOn = reader["CreatedOn"] != DBNull.Value ? (DateTime?)reader["CreatedOn"] : null,
                Title = reader["Title"] as string,
                FaceBookID = reader["FaceBookID"] as string,
                Status = reader["Status"] != DBNull.Value ? Convert.ToChar(reader["Status"]) : (char?)null,
                CanSendPromotions = reader["CanSendPromotions"] != DBNull.Value ? (bool?)reader["CanSendPromotions"] : null,
                Nationality = reader["Nationality"] as string,
                PassportNo = reader["PassportNo"] as string,
                PassportValidUpTo = reader["PassportValidUpTo"] != DBNull.Value ? (DateTime?)reader["PassportValidUpTo"] : null,
                PanNo = reader["PanNo"] as string,
                PanNoImg = reader["PanNoImg"] as string,
                IsPanNoVerify = reader["IsPanNoVerify"] != DBNull.Value ? (int?)reader["IsPanNoVerify"] : null,

                // GST_OnlineCustomer specific
                ISGSITIN = reader["ISGSITIN"] != DBNull.Value ? Convert.ToBoolean(reader["ISGSITIN"]) : false,
                CustomerGSTIN = reader["CustomerGSTIN"] as string,
                GstHolderName = reader["GstHolderName"] as string,
                AadharNo = reader["AadharNo"] as string,
                AadharImg = reader["AadharImg"] as string
            };
        }
    }

}