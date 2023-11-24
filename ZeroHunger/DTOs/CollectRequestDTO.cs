using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.DTOs
{
    public class CollectRequestDTO
    {
        public int RequestID { get; set; }
        public int EID { get; set; }
        public int RID { get; set; }
        public System.TimeSpan RequestTime { get; set; }
        public System.TimeSpan MaxPreservationTime { get; set; }
        [RegularExpression("^(Requested|Accepted|Collected|Distributed)$", ErrorMessage = "Invalid status value.")]
        public string Status { get; set; }

    }
}