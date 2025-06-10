using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class GetSPLTourMaster_spResult
    {
        public string tourname { get; set; }
        public string tourcode { get; set; }
        public string duration { get; set; }
        public string City { get; set; }
        public string DeptTime { get; set; }
        public bool IsNonAccommodation { get; set; }
    }

}