using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public partial class sp_HotelRoomTypesResult
    {

        private decimal _RoomTypeId;

        private string _RoomType;

        private string _Fare;

        private char _activated;

        private decimal _ExtraBedFare;

        public sp_HotelRoomTypesResult()
        {
        }

        public decimal RoomTypeId
        {
            get
            {
                return this._RoomTypeId;
            }
            set
            {
                if ((this._RoomTypeId != value))
                {
                    this._RoomTypeId = value;
                }
            }
        }

        public string RoomType
        {
            get
            {
                return this._RoomType;
            }
            set
            {
                if ((this._RoomType != value))
                {
                    this._RoomType = value;
                }
            }
        }

        public string Fare
        {
            get
            {
                return this._Fare;
            }
            set
            {
                if ((this._Fare != value))
                {
                    this._Fare = value;
                }
            }
        }

        public char activated
        {
            get
            {
                return this._activated;
            }
            set
            {
                if ((this._activated != value))
                {
                    this._activated = value;
                }
            }
        }

        public decimal ExtraBedFare
        {
            get
            {
                return this._ExtraBedFare;
            }
            set
            {
                if ((this._ExtraBedFare != value))
                {
                    this._ExtraBedFare = value;
                }
            }
        }
    }

}