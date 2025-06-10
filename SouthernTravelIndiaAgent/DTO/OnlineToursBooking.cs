using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class OnlineToursBooking
    {
        public string orderid { get; set; }
        public string tourid { get; set; }
        public DateTime doj { get; set; }
        public DateTime dob { get; set; }
        public char BusEnvType { get; set; }

        public int NoofAdults { get; set; }
        public int NoofChild { get; set; }
        public int NoofAdultsTwin { get; set; }
        public int NoofAdultsTriple { get; set; }
        public int ChildWithoutbed { get; set; }
        public int SingleAdult { get; set; }

        public decimal Amount { get; set; }
        public string TourName { get; set; }

        public decimal Tax { get; set; }
        public decimal CalcTaxvalue { get; set; }
        public decimal TotalAmount { get; set; }

        public string SeatNo { get; set; }
        public string BusSerialNo { get; set; }
        public string TourSerial { get; set; }

        public int PickUppointid { get; set; }

        public decimal adultfare { get; set; }
        public decimal childfare { get; set; }
        public decimal adultstwinfare { get; set; }
        public decimal adultstriplefare { get; set; }
        public decimal childwithoutbedfare { get; set; }
        public decimal singleadultfare { get; set; }

        public decimal CreditCardFee { get; set; }
        public decimal CalcCreditCardFee { get; set; }

        public int dormitory { get; set; }
        public decimal dormitoryfare { get; set; }

        public string Remarks { get; set; }
        public string OnLineDis { get; set; }

        public int noAdultWithFood { get; set; }
        public int noChildWithFood { get; set; }

        public decimal AdultWithFoodFare { get; set; }
        public decimal ChildWithFoodFare { get; set; }

        public decimal AdvancePaid { get; set; }

        public bool IsPartialPaymentByAgent { get; set; }

        public decimal AdultServiceCharges { get; set; }
        public decimal ChildServiceCharges { get; set; }

        public decimal ServiceChargesTotal { get; set; }
        public decimal ServiceChargesTax { get; set; }
        public decimal ServiceChargesTaxVal { get; set; }

        public int rowid { get; set; }
    }


}