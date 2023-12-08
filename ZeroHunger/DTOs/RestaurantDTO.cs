using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.DTOs
{
    public class RestaurantDTO
    {
        public int RID { get; set; }

        [Required(ErrorMessage = "Restaurant Name is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string RestaurantName { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Contact is required.")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Contact { get; set; }
    }
}