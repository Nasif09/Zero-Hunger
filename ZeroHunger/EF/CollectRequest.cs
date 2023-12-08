//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZeroHunger.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class CollectRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CollectRequest()
        {
            this.FoodItems = new HashSet<FoodItem>();
        }
    
        public int RequestID { get; set; }
        public Nullable<int> EID { get; set; }
        public int RID { get; set; }
        public System.DateTime RequestDay { get; set; }
        public System.DateTime MaxPreservationTime { get; set; }
        public string Status { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodItem> FoodItems { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public int? AssignedEmployeeID { get; internal set; }
    }
}
