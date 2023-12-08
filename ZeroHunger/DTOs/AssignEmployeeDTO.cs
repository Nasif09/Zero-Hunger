using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZeroHunger.EF;

namespace ZeroHunger.DTOs
{
    public class AssignEmployeeDTO
    {
        public int CollectRequestID { get; set; }
        public int? AssignedEmployeeID { get; set; }
        public List<EmployeeDTO> Employees { get; set; }
    }

}