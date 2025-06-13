using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public partial class GetHotelRooms_Fare_SPResult
    {

        private int _TypeID;

        private string _TypeName;

        private System.Nullable<bool> _SingleOccupancy;

        private System.Nullable<bool> _DoubleOccupancy;

        private System.Nullable<bool> _FourOccupancy;

        private string _OptionalName;

        private int _SeasonID;

        private string _SeasonName;

        private System.Nullable<System.DateTime> _FromDate;

        private System.Nullable<System.DateTime> _ToDate;

        private int _FareID;

        private System.Nullable<int> _MealPlanID;

        private string _MealPlan;

        private System.Nullable<decimal> _SglOccSellingPrice;

        private System.Nullable<decimal> _SellingPrice;

        private System.Nullable<decimal> _ExtraBedSellingPrice;

        private System.Nullable<decimal> _FourOccSellingPrice;

        private System.Nullable<decimal> _FourOccExtraBedSell;

        private decimal _CWBSellingPrice;

        public GetHotelRooms_Fare_SPResult()
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

        public string OptionalName
        {
            get
            {
                return this._OptionalName;
            }
            set
            {
                if ((this._OptionalName != value))
                {
                    this._OptionalName = value;
                }
            }
        }

        public int SeasonID
        {
            get
            {
                return this._SeasonID;
            }
            set
            {
                if ((this._SeasonID != value))
                {
                    this._SeasonID = value;
                }
            }
        }

        public string SeasonName
        {
            get
            {
                return this._SeasonName;
            }
            set
            {
                if ((this._SeasonName != value))
                {
                    this._SeasonName = value;
                }
            }
        }

        public System.Nullable<System.DateTime> FromDate
        {
            get
            {
                return this._FromDate;
            }
            set
            {
                if ((this._FromDate != value))
                {
                    this._FromDate = value;
                }
            }
        }

        public System.Nullable<System.DateTime> ToDate
        {
            get
            {
                return this._ToDate;
            }
            set
            {
                if ((this._ToDate != value))
                {
                    this._ToDate = value;
                }
            }
        }

        public int FareID
        {
            get
            {
                return this._FareID;
            }
            set
            {
                if ((this._FareID != value))
                {
                    this._FareID = value;
                }
            }
        }

        public System.Nullable<int> MealPlanID
        {
            get
            {
                return this._MealPlanID;
            }
            set
            {
                if ((this._MealPlanID != value))
                {
                    this._MealPlanID = value;
                }
            }
        }

        public string MealPlan
        {
            get
            {
                return this._MealPlan;
            }
            set
            {
                if ((this._MealPlan != value))
                {
                    this._MealPlan = value;
                }
            }
        }

        public System.Nullable<decimal> SglOccSellingPrice
        {
            get
            {
                return this._SglOccSellingPrice;
            }
            set
            {
                if ((this._SglOccSellingPrice != value))
                {
                    this._SglOccSellingPrice = value;
                }
            }
        }

        public System.Nullable<decimal> SellingPrice
        {
            get
            {
                return this._SellingPrice;
            }
            set
            {
                if ((this._SellingPrice != value))
                {
                    this._SellingPrice = value;
                }
            }
        }

        public System.Nullable<decimal> ExtraBedSellingPrice
        {
            get
            {
                return this._ExtraBedSellingPrice;
            }
            set
            {
                if ((this._ExtraBedSellingPrice != value))
                {
                    this._ExtraBedSellingPrice = value;
                }
            }
        }

        public System.Nullable<decimal> FourOccSellingPrice
        {
            get
            {
                return this._FourOccSellingPrice;
            }
            set
            {
                if ((this._FourOccSellingPrice != value))
                {
                    this._FourOccSellingPrice = value;
                }
            }
        }

        public System.Nullable<decimal> FourOccExtraBedSell
        {
            get
            {
                return this._FourOccExtraBedSell;
            }
            set
            {
                if ((this._FourOccExtraBedSell != value))
                {
                    this._FourOccExtraBedSell = value;
                }
            }
        }

        public decimal CWBSellingPrice
        {
            get
            {
                return this._CWBSellingPrice;
            }
            set
            {
                if ((this._CWBSellingPrice != value))
                {
                    this._CWBSellingPrice = value;
                }
            }
        }
    }

}