using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class SpecialTours_ZonewiseResult
    {
        public int Tourid { get; set; }
        public string Tourcode { get; set; }
        public string TourName { get; set; }
        public string Duration { get; set; }
        public decimal? Fare { get; set; }
    }

}