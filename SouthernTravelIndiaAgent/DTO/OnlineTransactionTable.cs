using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class OnlineTransactionTable
    {
        public int TransType { get; set; }
        public decimal AgentCredit { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal AgentDebit { get; set; }
        public decimal TicketAmount { get; set; }
        public decimal Commission { get; set; }
        public string UserName { get; set; }
        public char TransState { get; set; }
        public string PaymentMode { get; set; }
        public decimal ServiceTax { get; set; }
        public decimal TDS { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        // Optional: other fields that were commented out or might exist in DB
        // public int AgentId { get; set; }
        // public string RefNo { get; set; }
        // public string BranchCode { get; set; }
        // public string Status { get; set; }
    }

}