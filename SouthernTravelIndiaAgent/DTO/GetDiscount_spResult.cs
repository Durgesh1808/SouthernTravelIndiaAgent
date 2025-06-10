using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class GetDiscount_spResult
    {
        public int RowID { get; set; }

        public int? TourTypeID { get; set; }

        public int? TourID { get; set; }

        public int? HotelID { get; set; }

        public int? VendorID { get; set; }

        public int? RoomTypeID { get; set; }

        public decimal? FlatDiscount { get; set; }

        public decimal? PerDiscount { get; set; }

        public bool? IsFlat { get; set; }

        public bool? IsBooking { get; set; }

        public bool? IsCheckIn { get; set; }

        public bool? IsPer { get; set; }

        public DateTime? BookingFrom { get; set; }

        public DateTime? BookingTo { get; set; }

        public DateTime? CheckInFrom { get; set; }

        public DateTime? CheckOutTo { get; set; }

        public bool? IsAgent { get; set; }

        public bool? IsBranch { get; set; }

        public bool? IsOnline { get; set; }

        public bool? IsDiscountActive { get; set; }

        public string Remarks { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? LastUpdateBy { get; set; }

        public DateTime? LastUpdatedOn { get; set; }
    }

}