using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class GST_GetCityListByStateIdAndSearchedCityTextResult
    {
        public string CityID { get; set; }
        public string CityName { get; set; }

        public int StateID { get; set; }
    }
}