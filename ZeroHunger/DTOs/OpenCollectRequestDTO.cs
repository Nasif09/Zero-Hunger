using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.DTOs
{
    public class OpenCollectRequestDTO
    {
        public int RequestID { get; set; }
        public int EID { get; set; }
        [Required(ErrorMessage = "Insert Your Restaurent ID")]
        public int RID { get; set; }
        [Required(ErrorMessage = "Insert RequestTime")]
        public System.TimeSpan RequestTime { get; set; }
        [Required(ErrorMessage = "Insert MaxPreservationTime")]
        public System.TimeSpan MaxPreservationTime { get; set; }
        public string Status { get; set; }
    }
}