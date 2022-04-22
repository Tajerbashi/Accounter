//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NiceStore
{
    using System;
    using System.Collections.Generic;
    
    public partial class CartTB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CartTB()
        {
            this.CustomerTBs = new HashSet<CustomerTB>();
            this.PhoneTBs = new HashSet<PhoneTB>();
            this.SCPs = new HashSet<SCP>();
            this.ToolsTBs = new HashSet<ToolsTB>();
        }
    
        public int ID { get; set; }
        public int CodeCart { get; set; }
        public Nullable<System.DateTime> DateNow { get; set; }
        public Nullable<int> FactorId { get; set; }
    
        public virtual FactorTB FactorTB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerTB> CustomerTBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhoneTB> PhoneTBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCP> SCPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToolsTB> ToolsTBs { get; set; }
    }
}
