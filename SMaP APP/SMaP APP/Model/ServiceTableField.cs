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
    
    public partial class ServiceTableField : IBaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceTableField()
        {
            this.ServiceStoreParams = new HashSet<ServiceStoreParams>();
        }
    
        public int ID { get; set; }
        public Nullable<int> TableID { get; set; }
        public string FieldName { get; set; }
        public int FieldTypeID { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Dictionary Dictionary { get; set; }
        public virtual ServiceTable ServiceTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceStoreParams> ServiceStoreParams { get; set; }
    }
}
