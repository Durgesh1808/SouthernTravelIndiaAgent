using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public partial class sp_GetPaymentModeResult
    {

        private decimal _Rowid;

        private string _PaymentMode;

        private string _Remarks;

        public sp_GetPaymentModeResult()
        {
        }

        public decimal Rowid
        {
            get
            {
                return this._Rowid;
            }
            set
            {
                if ((this._Rowid != value))
                {
                    this._Rowid = value;
                }
            }
        }

        public string PaymentMode
        {
            get
            {
                return this._PaymentMode;
            }
            set
            {
                if ((this._PaymentMode != value))
                {
                    this._PaymentMode = value;
                }
            }
        }

        public string Remarks
        {
            get
            {
                return this._Remarks;
            }
            set
            {
                if ((this._Remarks != value))
                {
                    this._Remarks = value;
                }
            }
        }
    }

}