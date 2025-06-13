using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class GetCityName_SPResult
    {
        private int _CityID;

        private int _StateID;

        private string _CityName;

        private string _Latitude;

        private string _Longitude;

        private int _BufferKM;

        private bool _IsActive;

        private bool _IsOffice;

        private DateTime _CreatedOn;

        private int _CreatedBy;

        private DateTime? _LastUpdatedOn;

        private int? _LastUpdatedBy;

        public int CityID
        {
            get
            {
                return _CityID;
            }
            set
            {
                if (_CityID != value)
                {
                    _CityID = value;
                }
            }
        }

        public int StateID
        {
            get
            {
                return _StateID;
            }
            set
            {
                if (_StateID != value)
                {
                    _StateID = value;
                }
            }
        }

        public string CityName
        {
            get
            {
                return _CityName;
            }
            set
            {
                if (_CityName != value)
                {
                    _CityName = value;
                }
            }
        }

        public string Latitude
        {
            get
            {
                return _Latitude;
            }
            set
            {
                if (_Latitude != value)
                {
                    _Latitude = value;
                }
            }
        }

        public string Longitude
        {
            get
            {
                return _Longitude;
            }
            set
            {
                if (_Longitude != value)
                {
                    _Longitude = value;
                }
            }
        }

        public int BufferKM
        {
            get
            {
                return _BufferKM;
            }
            set
            {
                if (_BufferKM != value)
                {
                    _BufferKM = value;
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                }
            }
        }

        public bool IsOffice
        {
            get
            {
                return _IsOffice;
            }
            set
            {
                if (_IsOffice != value)
                {
                    _IsOffice = value;
                }
            }
        }

        public DateTime CreatedOn
        {
            get
            {
                return _CreatedOn;
            }
            set
            {
                if (_CreatedOn != value)
                {
                    _CreatedOn = value;
                }
            }
        }

        public int CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                if (_CreatedBy != value)
                {
                    _CreatedBy = value;
                }
            }
        }

        public DateTime? LastUpdatedOn
        {
            get
            {
                return _LastUpdatedOn;
            }
            set
            {
                if (_LastUpdatedOn != value)
                {
                    _LastUpdatedOn = value;
                }
            }
        }

        public int? LastUpdatedBy
        {
            get
            {
                return _LastUpdatedBy;
            }
            set
            {
                if (_LastUpdatedBy != value)
                {
                    _LastUpdatedBy = value;
                }
            }
        }
    }

}