using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class tbl_CarTailermade_log
    {
        public string CabId { get; set; }
        public int CityId { get; set; }
        public string Tourname { get; set; }
        public int VehicleId { get; set; }
        public int Noofpax { get; set; }
        public char Ac { get; set; } // Assuming 'Ac' is a string like "Yes"/"No" or "AC"/"Non-AC"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PickupAddress { get; set; }
        public string DropAddress { get; set; }
        public string PlacesCovered { get; set; }
        public char Islumpsum { get; set; } // 'Y' or 'N'
        public int Amount { get; set; }
        public int STax { get; set; }
        public decimal PerKmFare { get; set; }
        public int ApproxKm { get; set; }
        public int ApproxDays { get; set; }
        public int NightHalt { get; set; }
        public int DriverReward { get; set; }
        public int Advance { get; set; }
        public string UserName { get; set; }
        public string BranchCode { get; set; }
        
        public int noofnighthalts { get; set; } 
        public int noofdriverrewards { get; set; }
        public int MinKmPerDay { get; set; }
        public char isdiscount {  get; set; }
    }

}