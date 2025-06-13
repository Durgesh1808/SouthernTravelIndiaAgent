using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public partial class GetHotelNames_SPResult
    {

        private int _HotelID;

        private int _VendorID;

        private string _HotelName;

        private string _HotelDesc;

        private System.Nullable<int> _CountryID;

        private System.Nullable<int> _StateID;

        private System.Nullable<int> _CityID;

        private string _HotelAddress;

        private string _Pincode;

        private string _EmailID;

        private string _WebSite;

        private System.Nullable<bool> _ISPaymentToHotel;

        private System.Nullable<int> _PaymentTo;

        private string _BankName;

        private string _BankBranchName;

        private string _Number;

        private string _IFSCCode;

        private System.Nullable<int> _TypeID;

        private System.Nullable<int> _Priority;

        private bool _IsActive;

        private System.DateTime _CreatedOn;

        private int _CreatedBy;

        private System.Nullable<System.DateTime> _LastUpdatedOn;

        private System.Nullable<int> _LastUpdatedBy;

        private string _PhoneNo;

        private string _AlternatePhoneNo;

        private string _FaxNo;

        private string _MobileNo;

        private string _ChildAgeRange;

        private string _Latitude;

        private string _Longitude;

        private string _HotelLocation;

        private string _GuaranteeAmount;

        private string _GuaranteeByBank;

        private string _GuaranteeByBankBranch;

        private System.Nullable<bool> _PaymentOption;

        private string _AlternamtePhoneNo2;

        private System.Nullable<int> _ChildAgeRangeMin;

        private System.Nullable<int> _ChildAgeRangeMax;

        private string _ContactPerson;

        private System.Nullable<decimal> _Tax;

        private string _ChequeBankName;

        private string _ChequeBankBranchName;

        private string _ChequeNo;

        private string _TaxOption;

        private string _TOBEUSEDIN;

        private System.Nullable<int> _IsPreferred;

        private System.Nullable<bool> _Is24Hrs;

        private System.Nullable<System.DateTime> _CheckInTime;

        private System.Nullable<System.DateTime> _BuffCheckIn;

        private System.Nullable<System.DateTime> _CheckOutTime;

        private System.Nullable<System.DateTime> _BuffCheckOut;

        private System.Nullable<decimal> _LTax;

        private System.Nullable<decimal> _STax;

        public GetHotelNames_SPResult()
        {
        }

        public int HotelID
        {
            get
            {
                return this._HotelID;
            }
            set
            {
                if ((this._HotelID != value))
                {
                    this._HotelID = value;
                }
            }
        }

        public int VendorID
        {
            get
            {
                return this._VendorID;
            }
            set
            {
                if ((this._VendorID != value))
                {
                    this._VendorID = value;
                }
            }
        }

        public string HotelName
        {
            get
            {
                return this._HotelName;
            }
            set
            {
                if ((this._HotelName != value))
                {
                    this._HotelName = value;
                }
            }
        }

        public string HotelDesc
        {
            get
            {
                return this._HotelDesc;
            }
            set
            {
                if ((this._HotelDesc != value))
                {
                    this._HotelDesc = value;
                }
            }
        }

        public System.Nullable<int> CountryID
        {
            get
            {
                return this._CountryID;
            }
            set
            {
                if ((this._CountryID != value))
                {
                    this._CountryID = value;
                }
            }
        }

        public System.Nullable<int> StateID
        {
            get
            {
                return this._StateID;
            }
            set
            {
                if ((this._StateID != value))
                {
                    this._StateID = value;
                }
            }
        }

        public System.Nullable<int> CityID
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

        public string HotelAddress
        {
            get
            {
                return this._HotelAddress;
            }
            set
            {
                if ((this._HotelAddress != value))
                {
                    this._HotelAddress = value;
                }
            }
        }

        public string Pincode
        {
            get
            {
                return this._Pincode;
            }
            set
            {
                if ((this._Pincode != value))
                {
                    this._Pincode = value;
                }
            }
        }

        public string EmailID
        {
            get
            {
                return this._EmailID;
            }
            set
            {
                if ((this._EmailID != value))
                {
                    this._EmailID = value;
                }
            }
        }

        public string WebSite
        {
            get
            {
                return this._WebSite;
            }
            set
            {
                if ((this._WebSite != value))
                {
                    this._WebSite = value;
                }
            }
        }

        public System.Nullable<bool> ISPaymentToHotel
        {
            get
            {
                return this._ISPaymentToHotel;
            }
            set
            {
                if ((this._ISPaymentToHotel != value))
                {
                    this._ISPaymentToHotel = value;
                }
            }
        }

        public System.Nullable<int> PaymentTo
        {
            get
            {
                return this._PaymentTo;
            }
            set
            {
                if ((this._PaymentTo != value))
                {
                    this._PaymentTo = value;
                }
            }
        }

        public string BankName
        {
            get
            {
                return this._BankName;
            }
            set
            {
                if ((this._BankName != value))
                {
                    this._BankName = value;
                }
            }
        }

        public string BankBranchName
        {
            get
            {
                return this._BankBranchName;
            }
            set
            {
                if ((this._BankBranchName != value))
                {
                    this._BankBranchName = value;
                }
            }
        }

        public string Number
        {
            get
            {
                return this._Number;
            }
            set
            {
                if ((this._Number != value))
                {
                    this._Number = value;
                }
            }
        }

        public string IFSCCode
        {
            get
            {
                return this._IFSCCode;
            }
            set
            {
                if ((this._IFSCCode != value))
                {
                    this._IFSCCode = value;
                }
            }
        }

        public System.Nullable<int> TypeID
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


        public System.Nullable<int> Priority
        {
            get
            {
                return this._Priority;
            }
            set
            {
                if ((this._Priority != value))
                {
                    this._Priority = value;
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                if ((this._IsActive != value))
                {
                    this._IsActive = value;
                }
            }
        }
        public System.DateTime CreatedOn
        {
            get
            {
                return this._CreatedOn;
            }
            set
            {
                if ((this._CreatedOn != value))
                {
                    this._CreatedOn = value;
                }
            }
        }
        public int CreatedBy
        {
            get
            {
                return this._CreatedBy;
            }
            set
            {
                if ((this._CreatedBy != value))
                {
                    this._CreatedBy = value;
                }
            }
        }

        public System.Nullable<System.DateTime> LastUpdatedOn
        {
            get
            {
                return this._LastUpdatedOn;
            }
            set
            {
                if ((this._LastUpdatedOn != value))
                {
                    this._LastUpdatedOn = value;
                }
            }
        }

        public System.Nullable<int> LastUpdatedBy
        {
            get
            {
                return this._LastUpdatedBy;
            }
            set
            {
                if ((this._LastUpdatedBy != value))
                {
                    this._LastUpdatedBy = value;
                }
            }
        }

        public string PhoneNo
        {
            get
            {
                return this._PhoneNo;
            }
            set
            {
                if ((this._PhoneNo != value))
                {
                    this._PhoneNo = value;
                }
            }
        }

        public string AlternatePhoneNo
        {
            get
            {
                return this._AlternatePhoneNo;
            }
            set
            {
                if ((this._AlternatePhoneNo != value))
                {
                    this._AlternatePhoneNo = value;
                }
            }
        }

        public string FaxNo
        {
            get
            {
                return this._FaxNo;
            }
            set
            {
                if ((this._FaxNo != value))
                {
                    this._FaxNo = value;
                }
            }
        }
        public string MobileNo
        {
            get
            {
                return this._MobileNo;
            }
            set
            {
                if ((this._MobileNo != value))
                {
                    this._MobileNo = value;
                }
            }
        }
        public string ChildAgeRange
        {
            get
            {
                return this._ChildAgeRange;
            }
            set
            {
                if ((this._ChildAgeRange != value))
                {
                    this._ChildAgeRange = value;
                }
            }
        }
        public string Latitude
        {
            get
            {
                return this._Latitude;
            }
            set
            {
                if ((this._Latitude != value))
                {
                    this._Latitude = value;
                }
            }
        }

        public string Longitude
        {
            get
            {
                return this._Longitude;
            }
            set
            {
                if ((this._Longitude != value))
                {
                    this._Longitude = value;
                }
            }
        }

        public string HotelLocation
        {
            get
            {
                return this._HotelLocation;
            }
            set
            {
                if ((this._HotelLocation != value))
                {
                    this._HotelLocation = value;
                }
            }
        }

        public string GuaranteeAmount
        {
            get
            {
                return this._GuaranteeAmount;
            }
            set
            {
                if ((this._GuaranteeAmount != value))
                {
                    this._GuaranteeAmount = value;
                }
            }
        }


        public string GuaranteeByBank
        {
            get
            {
                return this._GuaranteeByBank;
            }
            set
            {
                if ((this._GuaranteeByBank != value))
                {
                    this._GuaranteeByBank = value;
                }
            }
        }


        public string GuaranteeByBankBranch
        {
            get
            {
                return this._GuaranteeByBankBranch;
            }
            set
            {
                if ((this._GuaranteeByBankBranch != value))
                {
                    this._GuaranteeByBankBranch = value;
                }
            }
        }


        public System.Nullable<bool> PaymentOption
        {
            get
            {
                return this._PaymentOption;
            }
            set
            {
                if ((this._PaymentOption != value))
                {
                    this._PaymentOption = value;
                }
            }
        }


        public string AlternamtePhoneNo2
        {
            get
            {
                return this._AlternamtePhoneNo2;
            }
            set
            {
                if ((this._AlternamtePhoneNo2 != value))
                {
                    this._AlternamtePhoneNo2 = value;
                }
            }
        }


        public System.Nullable<int> ChildAgeRangeMin
        {
            get
            {
                return this._ChildAgeRangeMin;
            }
            set
            {
                if ((this._ChildAgeRangeMin != value))
                {
                    this._ChildAgeRangeMin = value;
                }
            }
        }

        public System.Nullable<int> ChildAgeRangeMax
        {
            get
            {
                return this._ChildAgeRangeMax;
            }
            set
            {
                if ((this._ChildAgeRangeMax != value))
                {
                    this._ChildAgeRangeMax = value;
                }
            }
        }

        public string ContactPerson
        {
            get
            {
                return this._ContactPerson;
            }
            set
            {
                if ((this._ContactPerson != value))
                {
                    this._ContactPerson = value;
                }
            }
        }

        public System.Nullable<decimal> Tax
        {
            get
            {
                return this._Tax;
            }
            set
            {
                if ((this._Tax != value))
                {
                    this._Tax = value;
                }
            }
        }

        public string ChequeBankName
        {
            get
            {
                return this._ChequeBankName;
            }
            set
            {
                if ((this._ChequeBankName != value))
                {
                    this._ChequeBankName = value;
                }
            }
        }

        public string ChequeBankBranchName
        {
            get
            {
                return this._ChequeBankBranchName;
            }
            set
            {
                if ((this._ChequeBankBranchName != value))
                {
                    this._ChequeBankBranchName = value;
                }
            }
        }

        public string ChequeNo
        {
            get
            {
                return this._ChequeNo;
            }
            set
            {
                if ((this._ChequeNo != value))
                {
                    this._ChequeNo = value;
                }
            }
        }

        public string TaxOption
        {
            get
            {
                return this._TaxOption;
            }
            set
            {
                if ((this._TaxOption != value))
                {
                    this._TaxOption = value;
                }
            }
        }

        public string TOBEUSEDIN
        {
            get
            {
                return this._TOBEUSEDIN;
            }
            set
            {
                if ((this._TOBEUSEDIN != value))
                {
                    this._TOBEUSEDIN = value;
                }
            }
        }

        public System.Nullable<int> IsPreferred
        {
            get
            {
                return this._IsPreferred;
            }
            set
            {
                if ((this._IsPreferred != value))
                {
                    this._IsPreferred = value;
                }
            }
        }

        public System.Nullable<bool> Is24Hrs
        {
            get
            {
                return this._Is24Hrs;
            }
            set
            {
                if ((this._Is24Hrs != value))
                {
                    this._Is24Hrs = value;
                }
            }
        }

        public System.Nullable<System.DateTime> CheckInTime
        {
            get
            {
                return this._CheckInTime;
            }
            set
            {
                if ((this._CheckInTime != value))
                {
                    this._CheckInTime = value;
                }
            }
        }

        public System.Nullable<System.DateTime> BuffCheckIn
        {
            get
            {
                return this._BuffCheckIn;
            }
            set
            {
                if ((this._BuffCheckIn != value))
                {
                    this._BuffCheckIn = value;
                }
            }
        }

        public System.Nullable<System.DateTime> CheckOutTime
        {
            get
            {
                return this._CheckOutTime;
            }
            set
            {
                if ((this._CheckOutTime != value))
                {
                    this._CheckOutTime = value;
                }
            }
        }

        public System.Nullable<System.DateTime> BuffCheckOut
        {
            get
            {
                return this._BuffCheckOut;
            }
            set
            {
                if ((this._BuffCheckOut != value))
                {
                    this._BuffCheckOut = value;
                }
            }
        }

        public System.Nullable<decimal> LTax
        {
            get
            {
                return this._LTax;
            }
            set
            {
                if ((this._LTax != value))
                {
                    this._LTax = value;
                }
            }
        }

        public System.Nullable<decimal> STax
        {
            get
            {
                return this._STax;
            }
            set
            {
                if ((this._STax != value))
                {
                    this._STax = value;
                }
            }
        }
    }

}