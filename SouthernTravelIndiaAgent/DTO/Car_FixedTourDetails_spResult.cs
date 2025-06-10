using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class Car_FixedTourDetails_spResult
    {
        public decimal FareId { get; set; }
        public decimal TourID { get; set; }
        public decimal TourNo { get; set; }
        public string TourName { get; set; }
        public string TourDescription { get; set; }
        public string DurationDay { get; set; }
        public string DurationNight { get; set; }
        public string Locations { get; set; }
        public string Amount { get; set; }
        public string DriverReward { get; set; }
        public string NightDetention { get; set; }
        public string PerExtraKMFare { get; set; }
        public string PerExtraHRFare { get; set; }
        public decimal CarId { get; set; }
    }

}