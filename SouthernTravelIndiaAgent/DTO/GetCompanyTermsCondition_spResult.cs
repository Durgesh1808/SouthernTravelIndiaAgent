using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public partial class GetCompanyTermsCondition_spResult
    {

        private string _TermsCondition;

        public GetCompanyTermsCondition_spResult()
        {
        }

        public string TermsCondition
        {
            get
            {
                return this._TermsCondition;
            }
            set
            {
                if ((this._TermsCondition != value))
                {
                    this._TermsCondition = value;
                }
            }
        }
    }
}