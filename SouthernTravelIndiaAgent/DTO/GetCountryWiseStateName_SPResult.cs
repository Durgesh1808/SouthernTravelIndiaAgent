using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class GetCountryWiseStateName_SPResult
    {
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public int RegionID { get; set; }
        public string StateName { get; set; }
        public bool IsActive { get; set; }
        public bool IsOffice { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public int? LastUpdatedBy { get; set; }
    }
}