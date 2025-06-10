using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class GetAgentPayDetail_spResult
    {
        public decimal? Amount { get; set; }
        public decimal? CalcTaxvalue { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? CalcCreditCardFee { get; set; }
        public decimal? AdvancePaid { get; set; }
    }

}