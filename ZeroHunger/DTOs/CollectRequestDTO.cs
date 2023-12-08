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
        public Nullable<int> EID { get; set; }
        public int RID { get; set; }
        public System.DateTime RequestDay { get; set; }
        public System.DateTime MaxPreservationTime { get; set; }
        public string Status { get; set; }

    }
}