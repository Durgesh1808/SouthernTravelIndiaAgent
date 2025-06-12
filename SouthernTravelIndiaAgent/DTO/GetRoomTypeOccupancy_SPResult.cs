using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public partial class GetRoomTypeOccupancy_SPResult
    {

        private int _TypeID;

        private string _TypeName;

        private System.Nullable<bool> _SingleOccupancy;

        private System.Nullable<bool> _DoubleOccupancy;

        private System.Nullable<bool> _FourOccupancy;

        public GetRoomTypeOccupancy_SPResult()
        {
        }

        public int TypeID
        {
            get
            {
                return this._TypeID;
            }
            set
            {
                if ((this._TypeID != value))
                {
                    this._TypeID = value;
                }
            }
        }

        public string TypeName
        {
            get
            {
                return this._TypeName;
            }
            set
            {
                if ((this._TypeName != value))
                {
                    this._TypeName = value;
                }
            }
        }

        public System.Nullable<bool> SingleOccupancy
        {
            get
            {
                return this._SingleOccupancy;
            }
            set
            {
                if ((this._SingleOccupancy != value))
                {
                    this._SingleOccupancy = value;
                }
            }
        }

        public System.Nullable<bool> DoubleOccupancy
        {
            get
            {
                return this._DoubleOccupancy;
            }
            set
            {
                if ((this._DoubleOccupancy != value))
                {
                    this._DoubleOccupancy = value;
                }
            }
        }

        public System.Nullable<bool> FourOccupancy
        {
            get
            {
                return this._FourOccupancy;
            }
            set
            {
                if ((this._FourOccupancy != value))
                {
                    this._FourOccupancy = value;
                }
            }
        }
    }

}