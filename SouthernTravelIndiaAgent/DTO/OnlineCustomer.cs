using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class OnlineCustomer
    {
        public int RowId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string PhoneNo { get; set; }
        public string AlternativeName { get; set; }
        public string RelativePhoneNo { get; set; }
        public string AlternativeNo { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public DateTime? DOB { get; set; }
        public string Company { get; set; }
        public string Occupation { get; set; }
        public string Remark { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime? DOA { get; set; }
        public char? Sex { get; set; }
        public string CallerID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Title { get; set; }
        public string FaceBookID { get; set; }
        public char? Status { get; set; }
        public bool? CanSendPromotions { get; set; }
        public string Nationality { get; set; }
        public string PassportNo { get; set; }
        public DateTime? PassportValidUpTo { get; set; }
        public string PanNo { get; set; }
        public string PanNoImg { get; set; }
        public int? IsPanNoVerify { get; set; }

        public string state { get; set; }

        public string email { get; set; }
        public string zipcode { get; set; }

    public string password { get; set; }
    }

}