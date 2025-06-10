using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class GST_spl_tourEnquiry
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public DateTime JourneyDate { get; set; }  // Consider changing to DateTime if applicable
        public string TourName { get; set; }
        public int TotalPax { get; set; }
        public string CarType { get; set; }
        public string FareCategoryType { get; set; }
        public string PassengerPerVehicle { get; set; }
        public char Status { get; set; }
        public int TourId { get; set; }
        public decimal Fare { get; set; }
        public string SingleSharing { get; set; }
        public string PickUpVeh { get; set; }
        public string PickVehNo { get; set; }
        public string PickTime { get; set; }
        public string DropVeh { get; set; }
        public string DropVehNo { get; set; }
        public string DropTime { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public decimal CCCharges { get; set; }
        public string Comments { get; set; }
        public string station { get; set; }
        public string PkStation { get; set; }
        public string BranchCode { get; set; }
        public int AgentId { get; set; }
        public decimal AdvancePaid { get; set; }
        public string ZipCode { get; set; }
        public bool ISGSITIN { get; set; }
        public string CustomerGSTIN { get; set; }
        public string GstHolderName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }       // From ddlCountry.SelectedValue
        public string Nationality { get; set; }   // From ddlNationality.SelectedValue
        public string AadharNo { get; set; }
        public string AadharNoImg { get; set; }
    }

}