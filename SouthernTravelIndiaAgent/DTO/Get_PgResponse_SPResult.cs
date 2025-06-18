using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public partial class Get_PgResponse_SPResult
    {

        private int _RowID;

        private string _PaymentID;

        private string _OrderID;

        private string _EmailID;

        private string _Auth;

        private System.Nullable<decimal> _Amount;

        private string _Ref;

        private string _TranID;

        private string _TrackID;

        private string _PostDate;

        private string _Result;

        private string _ErrorText;

        private string _Udf1;

        private string _Udf2;

        private string _Udf3;

        private string _Udf4;

        private string _Udf5;

        private System.DateTime _PaymentOn;

        private string _tempOrderId;

        private string _SectionName;

        public Get_PgResponse_SPResult()
        {
        }

        public int RowID
        {
            get
            {
                return this._RowID;
            }
            set
            {
                if ((this._RowID != value))
                {
                    this._RowID = value;
                }
            }
        }

        public string PaymentID
        {
            get
            {
                return this._PaymentID;
            }
            set
            {
                if ((this._PaymentID != value))
                {
                    this._PaymentID = value;
                }
            }
        }

        public string OrderID
        {
            get
            {
                return this._OrderID;
            }
            set
            {
                if ((this._OrderID != value))
                {
                    this._OrderID = value;
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

        public string Auth
        {
            get
            {
                return this._Auth;
            }
            set
            {
                if ((this._Auth != value))
                {
                    this._Auth = value;
                }
            }
        }

        public System.Nullable<decimal> Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
                }
            }
        }

        public string Ref
        {
            get
            {
                return this._Ref;
            }
            set
            {
                if ((this._Ref != value))
                {
                    this._Ref = value;
                }
            }
        }

        public string TranID
        {
            get
            {
                return this._TranID;
            }
            set
            {
                if ((this._TranID != value))
                {
                    this._TranID = value;
                }
            }
        }

        public string TrackID
        {
            get
            {
                return this._TrackID;
            }
            set
            {
                if ((this._TrackID != value))
                {
                    this._TrackID = value;
                }
            }
        }

        public string PostDate
        {
            get
            {
                return this._PostDate;
            }
            set
            {
                if ((this._PostDate != value))
                {
                    this._PostDate = value;
                }
            }
        }

        public string Result
        {
            get
            {
                return this._Result;
            }
            set
            {
                if ((this._Result != value))
                {
                    this._Result = value;
                }
            }
        }

        public string ErrorText
        {
            get
            {
                return this._ErrorText;
            }
            set
            {
                if ((this._ErrorText != value))
                {
                    this._ErrorText = value;
                }
            }
        }

        public string Udf1
        {
            get
            {
                return this._Udf1;
            }
            set
            {
                if ((this._Udf1 != value))
                {
                    this._Udf1 = value;
                }
            }
        }

        public string Udf2
        {
            get
            {
                return this._Udf2;
            }
            set
            {
                if ((this._Udf2 != value))
                {
                    this._Udf2 = value;
                }
            }
        }

        public string Udf3
        {
            get
            {
                return this._Udf3;
            }
            set
            {
                if ((this._Udf3 != value))
                {
                    this._Udf3 = value;
                }
            }
        }

        public string Udf4
        {
            get
            {
                return this._Udf4;
            }
            set
            {
                if ((this._Udf4 != value))
                {
                    this._Udf4 = value;
                }
            }
        }

        public string Udf5
        {
            get
            {
                return this._Udf5;
            }
            set
            {
                if ((this._Udf5 != value))
                {
                    this._Udf5 = value;
                }
            }
        }

        public System.DateTime PaymentOn
        {
            get
            {
                return this._PaymentOn;
            }
            set
            {
                if ((this._PaymentOn != value))
                {
                    this._PaymentOn = value;
                }
            }
        }

        public string tempOrderId
        {
            get
            {
                return this._tempOrderId;
            }
            set
            {
                if ((this._tempOrderId != value))
                {
                    this._tempOrderId = value;
                }
            }
        }

        public string SectionName
        {
            get
            {
                return this._SectionName;
            }
            set
            {
                if ((this._SectionName != value))
                {
                    this._SectionName = value;
                }
            }
        }
    }

}