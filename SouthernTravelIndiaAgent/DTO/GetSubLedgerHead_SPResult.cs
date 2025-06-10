using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    public class GetSubLedgerHead_SPResult
    {
        public int RowID { get; set; }
        public int? LedgerHead { get; set; }
        public string SelectionName { get; set; }
        public int? SelectionType { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

}