using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class Group_GroupBookingRequest
    {
        public decimal Id { get; set; }
        public string TourName { get; set; }
        public int? TourId { get; set; }
        public string GroupLeaderName { get; set; }
        public int? NoofAdults { get; set; }
        public int? NoofChilds { get; set; }
        public string BusType { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public char? Accommodation { get; set; }
        public char? Food { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Emailid { get; set; }
        public string AgentId { get; set; }
        public DateTime? RequestDate { get; set; }
    }

}