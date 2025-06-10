using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class Agent_ProfileResult
    {
        public int AgentId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime? DOB { get; set; }
        public char? Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string Mobile { get; set; }
        public string LandLine { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string AuthorityMember { get; set; }
        public string AgentLevel { get; set; }
        public char IsActive { get; set; }
        public char? IsApprove { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string LocalBranch { get; set; }
        public char? IsForgot { get; set; }
        public char? IsOnlineagent { get; set; }
        public bool IsWLabelAgent { get; set; }
        public bool IsSTPLPG { get; set; }
        public string WLPWD { get; set; }
        public int? ParentAgent { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string PanNo { get; set; }
    }

}