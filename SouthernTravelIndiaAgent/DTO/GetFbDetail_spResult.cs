using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public partial class GetFbDetail_spResult
    {

        private int _RowId;

        private string _FirstName;

        private string _LastName;

        private string _email;

        private string _password;

        private string _Addr1;

        private string _addr2;

        private string _City;

        private string _state;

        private string _Country;

        private string _zipcode;

        private string _PhoneNo;

        private string _AlternativeName;

        private string _RelativePhoneNo;

        private string _AlternativeNo;

        private string _Mobile;

        private string _Fax;

        private System.Nullable<System.DateTime> _DOB;

        private string _company;

        private string _occupation;

        private string _remark;

        private string _maritalstatus;

        private System.Nullable<System.DateTime> _DOA;

        private System.Nullable<char> _Sex;

        private string _CallerID;

        private System.Nullable<System.DateTime> _CreateedOn;

        private System.Nullable<System.DateTime> _CreatedOn;

        private string _Title;

        private string _FaceBookID;

        public GetFbDetail_spResult()
        {
        }

        public int RowId
        {
            get
            {
                return this._RowId;
            }
            set
            {
                if ((this._RowId != value))
                {
                    this._RowId = value;
                }
            }
        }

        public string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                if ((this._FirstName != value))
                {
                    this._FirstName = value;
                }
            }
        }

        public string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                if ((this._LastName != value))
                {
                    this._LastName = value;
                }
            }
        }

        public string email
        {
            get
            {
                return this._email;
            }
            set
            {
                if ((this._email != value))
                {
                    this._email = value;
                }
            }
        }

        public string password
        {
            get
            {
                return this._password;
            }
            set
            {
                if ((this._password != value))
                {
                    this._password = value;
                }
            }
        }

        public string Addr1
        {
            get
            {
                return this._Addr1;
            }
            set
            {
                if ((this._Addr1 != value))
                {
                    this._Addr1 = value;
                }
            }
        }

        public string addr2
        {
            get
            {
                return this._addr2;
            }
            set
            {
                if ((this._addr2 != value))
                {
                    this._addr2 = value;
                }
            }
        }

        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                if ((this._City != value))
                {
                    this._City = value;
                }
            }
        }

        public string state
        {
            get
            {
                return this._state;
            }
            set
            {
                if ((this._state != value))
                {
                    this._state = value;
                }
            }
        }

        public string Country
        {
            get
            {
                return this._Country;
            }
            set
            {
                if ((this._Country != value))
                {
                    this._Country = value;
                }
            }
        }

        public string zipcode
        {
            get
            {
                return this._zipcode;
            }
            set
            {
                if ((this._zipcode != value))
                {
                    this._zipcode = value;
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

        public string AlternativeName
        {
            get
            {
                return this._AlternativeName;
            }
            set
            {
                if ((this._AlternativeName != value))
                {
                    this._AlternativeName = value;
                }
            }
        }

        public string RelativePhoneNo
        {
            get
            {
                return this._RelativePhoneNo;
            }
            set
            {
                if ((this._RelativePhoneNo != value))
                {
                    this._RelativePhoneNo = value;
                }
            }
        }

        public string AlternativeNo
        {
            get
            {
                return this._AlternativeNo;
            }
            set
            {
                if ((this._AlternativeNo != value))
                {
                    this._AlternativeNo = value;
                }
            }
        }

        public string Mobile
        {
            get
            {
                return this._Mobile;
            }
            set
            {
                if ((this._Mobile != value))
                {
                    this._Mobile = value;
                }
            }
        }

        public string Fax
        {
            get
            {
                return this._Fax;
            }
            set
            {
                if ((this._Fax != value))
                {
                    this._Fax = value;
                }
            }
        }

        public System.Nullable<System.DateTime> DOB
        {
            get
            {
                return this._DOB;
            }
            set
            {
                if ((this._DOB != value))
                {
                    this._DOB = value;
                }
            }
        }

        public string company
        {
            get
            {
                return this._company;
            }
            set
            {
                if ((this._company != value))
                {
                    this._company = value;
                }
            }
        }

        public string occupation
        {
            get
            {
                return this._occupation;
            }
            set
            {
                if ((this._occupation != value))
                {
                    this._occupation = value;
                }
            }
        }

        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                if ((this._remark != value))
                {
                    this._remark = value;
                }
            }
        }

        public string maritalstatus
        {
            get
            {
                return this._maritalstatus;
            }
            set
            {
                if ((this._maritalstatus != value))
                {
                    this._maritalstatus = value;
                }
            }
        }

        public System.Nullable<System.DateTime> DOA
        {
            get
            {
                return this._DOA;
            }
            set
            {
                if ((this._DOA != value))
                {
                    this._DOA = value;
                }
            }
        }

        public System.Nullable<char> Sex
        {
            get
            {
                return this._Sex;
            }
            set
            {
                if ((this._Sex != value))
                {
                    this._Sex = value;
                }
            }
        }

        public string CallerID
        {
            get
            {
                return this._CallerID;
            }
            set
            {
                if ((this._CallerID != value))
                {
                    this._CallerID = value;
                }
            }
        }

        public System.Nullable<System.DateTime> CreateedOn
        {
            get
            {
                return this._CreateedOn;
            }
            set
            {
                if ((this._CreateedOn != value))
                {
                    this._CreateedOn = value;
                }
            }
        }

        public System.Nullable<System.DateTime> CreatedOn
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

        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                if ((this._Title != value))
                {
                    this._Title = value;
                }
            }
        }

        public string FaceBookID
        {
            get
            {
                return this._FaceBookID;
            }
            set
            {
                if ((this._FaceBookID != value))
                {
                    this._FaceBookID = value;
                }
            }
        }
    }

}