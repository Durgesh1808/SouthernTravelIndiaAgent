using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class AgentTransaction_RPT_SPResult
    {
        public int RowID { get; set; }
        public int? AgentID { get; set; }
        public string RefNo { get; set; }
        public decimal? AgentCredit { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string ChqNo { get; set; }
        public string TourName { get; set; }
        public decimal? ServiceTax { get; set; }
        public decimal? Commission { get; set; }
        public decimal? TDS { get; set; }
        public decimal? TicketAmount { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? GCOMM { get; set; }
        public decimal? AdvancePaid { get; set; }
        public string TicketDetails { get; set; }
        public decimal? AgentDebit { get; set; }
    }

}