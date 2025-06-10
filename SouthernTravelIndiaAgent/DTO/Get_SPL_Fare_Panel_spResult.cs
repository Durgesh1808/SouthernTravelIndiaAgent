using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class Get_SPL_Fare_Panel_spResult
    {
        public int PanelID { get; set; }
        public int FareID { get; set; }
        public string CategoryName { get; set; }
        public decimal? Fare { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string s_month { get; set; }
        public int? CategoryID { get; set; }
        public string Season { get; set; }
        public long? SeasonID { get; set; }
        public int VehicleID { get; set; }
        public string VehicleName { get; set; }
        public int PaxID { get; set; }
        public string PaxType { get; set; }
        public int ParentID { get; set; }
        public string ParentCategory { get; set; }
        public decimal? DiscountedFare { get; set; }
        public bool IsAgent { get; set; }
        public bool IsBranch { get; set; }
        public bool IsOnline { get; set; }
    }

}