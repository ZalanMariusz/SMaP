//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMaP_APP.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceTable : IBaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceTable()
        {
            this.ServiceTableField = new HashSet<ServiceTableField>();
        }
    
        public int ID { get; set; }
        public string TableName { get; set; }
        public Nullable<int> TeamID { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Team Team { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceTableField> ServiceTableField { get; set; }
    }
}