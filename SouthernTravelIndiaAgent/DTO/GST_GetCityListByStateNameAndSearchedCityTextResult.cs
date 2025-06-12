using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public partial class GST_GetCityListByStateNameAndSearchedCityTextResult
    {

        private string _CityID;

        private string _CityName;

        public GST_GetCityListByStateNameAndSearchedCityTextResult()
        {
        }

        public string CityID
        {
            get
            {
                return this._CityID;
            }
            set
            {
                if ((this._CityID != value))
                {
                    this._CityID = value;
                }
            }
        }

        public string CityName
        {
            get
            {
                return this._CityName;
            }
            set
            {
                if ((this._CityName != value))
                {
                    this._CityName = value;
                }
            }
        }
    }

}