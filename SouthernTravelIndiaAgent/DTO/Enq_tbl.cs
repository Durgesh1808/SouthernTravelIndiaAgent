using System;
using System.ComponentModel;


namespace SouthernTravelIndiaAgent.DTO
{
    public partial class Enq_tbl : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _RowId;

        private string _Description;

        private string _Name;

        private string _Email;

        private string _Phone;

        private string _Fax;

        private string _Street;

        private string _City;

        private string _Zip;

        private string _Country;

        private System.Nullable<int> _Adults;

        private System.Nullable<int> _Child;

        private System.Nullable<System.DateTime> _Transactiondate;

        private System.Nullable<System.DateTime> _ArrivalDate;

        private System.Nullable<System.DateTime> _DepDate;

        private string _EnqType;

        private string _Refno;

        private string _captcha;

        private string _PanNo;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate();
        partial void OnCreated();
        partial void OnRowIdChanging(int value);
        partial void OnRowIdChanged();
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
        partial void OnPhoneChanging(string value);
        partial void OnPhoneChanged();
        partial void OnFaxChanging(string value);
        partial void OnFaxChanged();
        partial void OnStreetChanging(string value);
        partial void OnStreetChanged();
        partial void OnCityChanging(string value);
        partial void OnCityChanged();
        partial void OnZipChanging(string value);
        partial void OnZipChanged();
        partial void OnCountryChanging(string value);
        partial void OnCountryChanged();
        partial void OnAdultsChanging(System.Nullable<int> value);
        partial void OnAdultsChanged();
        partial void OnChildChanging(System.Nullable<int> value);
        partial void OnChildChanged();
        partial void OnTransactiondateChanging(System.Nullable<System.DateTime> value);
        partial void OnTransactiondateChanged();
        partial void OnArrivalDateChanging(System.Nullable<System.DateTime> value);
        partial void OnArrivalDateChanged();
        partial void OnDepDateChanging(System.Nullable<System.DateTime> value);
        partial void OnDepDateChanged();
        partial void OnEnqTypeChanging(string value);
        partial void OnEnqTypeChanged();
        partial void OnRefnoChanging(string value);
        partial void OnRefnoChanged();
        partial void OncaptchaChanging(string value);
        partial void OncaptchaChanged();
        partial void OnPanNoChanging(string value);
        partial void OnPanNoChanged();
        #endregion

        public Enq_tbl()
        {
            OnCreated();
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
                    this.OnRowIdChanging(value);
                    this.SendPropertyChanging();
                    this._RowId = value;
                    this.SendPropertyChanged("RowId");
                    this.OnRowIdChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this.OnDescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Description = value;
                    this.SendPropertyChanged("Description");
                    this.OnDescriptionChanged();
                }
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this.OnNameChanging(value);
                    this.SendPropertyChanging();
                    this._Name = value;
                    this.SendPropertyChanged("Name");
                    this.OnNameChanged();
                }
            }
        }

        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                if ((this._Email != value))
                {
                    this.OnEmailChanging(value);
                    this.SendPropertyChanging();
                    this._Email = value;
                    this.SendPropertyChanged("Email");
                    this.OnEmailChanged();
                }
            }
        }

        public string Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                if ((this._Phone != value))
                {
                    this.OnPhoneChanging(value);
                    this.SendPropertyChanging();
                    this._Phone = value;
                    this.SendPropertyChanged("Phone");
                    this.OnPhoneChanged();
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
                    this.OnFaxChanging(value);
                    this.SendPropertyChanging();
                    this._Fax = value;
                    this.SendPropertyChanged("Fax");
                    this.OnFaxChanged();
                }
            }
        }

        public string Street
        {
            get
            {
                return this._Street;
            }
            set
            {
                if ((this._Street != value))
                {
                    this.OnStreetChanging(value);
                    this.SendPropertyChanging();
                    this._Street = value;
                    this.SendPropertyChanged("Street");
                    this.OnStreetChanged();
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
                    this.OnCityChanging(value);
                    this.SendPropertyChanging();
                    this._City = value;
                    this.SendPropertyChanged("City");
                    this.OnCityChanged();
                }
            }
        }

        public string Zip
        {
            get
            {
                return this._Zip;
            }
            set
            {
                if ((this._Zip != value))
                {
                    this.OnZipChanging(value);
                    this.SendPropertyChanging();
                    this._Zip = value;
                    this.SendPropertyChanged("Zip");
                    this.OnZipChanged();
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
                    this.OnCountryChanging(value);
                    this.SendPropertyChanging();
                    this._Country = value;
                    this.SendPropertyChanged("Country");
                    this.OnCountryChanged();
                }
            }
        }

        public System.Nullable<int> Adults
        {
            get
            {
                return this._Adults;
            }
            set
            {
                if ((this._Adults != value))
                {
                    this.OnAdultsChanging(value);
                    this.SendPropertyChanging();
                    this._Adults = value;
                    this.SendPropertyChanged("Adults");
                    this.OnAdultsChanged();
                }
            }
        }

        public System.Nullable<int> Child
        {
            get
            {
                return this._Child;
            }
            set
            {
                if ((this._Child != value))
                {
                    this.OnChildChanging(value);
                    this.SendPropertyChanging();
                    this._Child = value;
                    this.SendPropertyChanged("Child");
                    this.OnChildChanged();
                }
            }
        }

        public System.Nullable<System.DateTime> Transactiondate
        {
            get
            {
                return this._Transactiondate;
            }
            set
            {
                if ((this._Transactiondate != value))
                {
                    this.OnTransactiondateChanging(value);
                    this.SendPropertyChanging();
                    this._Transactiondate = value;
                    this.SendPropertyChanged("Transactiondate");
                    this.OnTransactiondateChanged();
                }
            }
        }

        public System.Nullable<System.DateTime> ArrivalDate
        {
            get
            {
                return this._ArrivalDate;
            }
            set
            {
                if ((this._ArrivalDate != value))
                {
                    this.OnArrivalDateChanging(value);
                    this.SendPropertyChanging();
                    this._ArrivalDate = value;
                    this.SendPropertyChanged("ArrivalDate");
                    this.OnArrivalDateChanged();
                }
            }
        }

        public System.Nullable<System.DateTime> DepDate
        {
            get
            {
                return this._DepDate;
            }
            set
            {
                if ((this._DepDate != value))
                {
                    this.OnDepDateChanging(value);
                    this.SendPropertyChanging();
                    this._DepDate = value;
                    this.SendPropertyChanged("DepDate");
                    this.OnDepDateChanged();
                }
            }
        }

        public string EnqType
        {
            get
            {
                return this._EnqType;
            }
            set
            {
                if ((this._EnqType != value))
                {
                    this.OnEnqTypeChanging(value);
                    this.SendPropertyChanging();
                    this._EnqType = value;
                    this.SendPropertyChanged("EnqType");
                    this.OnEnqTypeChanged();
                }
            }
        }

        public string Refno
        {
            get
            {
                return this._Refno;
            }
            set
            {
                if ((this._Refno != value))
                {
                    this.OnRefnoChanging(value);
                    this.SendPropertyChanging();
                    this._Refno = value;
                    this.SendPropertyChanged("Refno");
                    this.OnRefnoChanged();
                }
            }
        }

        public string captcha
        {
            get
            {
                return this._captcha;
            }
            set
            {
                if ((this._captcha != value))
                {
                    this.OncaptchaChanging(value);
                    this.SendPropertyChanging();
                    this._captcha = value;
                    this.SendPropertyChanged("captcha");
                    this.OncaptchaChanged();
                }
            }
        }

        public string PanNo
        {
            get
            {
                return this._PanNo;
            }
            set
            {
                if ((this._PanNo != value))
                {
                    this.OnPanNoChanging(value);
                    this.SendPropertyChanging();
                    this._PanNo = value;
                    this.SendPropertyChanged("PanNo");
                    this.OnPanNoChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}