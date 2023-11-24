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
            this.DistributionRecords = new HashSet<DistributionRecord>();
            this.FoodItems = new HashSet<FoodItem>();
        }
    
        public int RequestID { get; set; }
        public int EID { get; set; }
        public int RID { get; set; }
        public System.TimeSpan RequestTime { get; set; }
        public System.TimeSpan MaxPreservationTime { get; set; }
        public string Status { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Restaurent Restaurent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DistributionRecord> DistributionRecords { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodItem> FoodItems { get; set; }
    }
}
