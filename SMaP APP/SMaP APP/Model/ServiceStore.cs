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
    
    public partial class ServiceStore : IBaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceStore()
        {
            this.ServiceRequest = new HashSet<ServiceRequest>();
            this.ServiceStoreServiceParams = new HashSet<ServiceStoreServiceParams>();
            this.ServiceStoreServiceParams1 = new HashSet<ServiceStoreServiceParams>();
            this.ServiceStoreUserTeams = new HashSet<ServiceStoreUserTeams>();
            this.ServiceStoreParams = new HashSet<ServiceStoreParams>();
        }
    
        public int ID { get; set; }
        public string ServiceName { get; set; }
        public int ServiceNumber { get; set; }
        public int CreatorID { get; set; }
        public int ProviderTeamID { get; set; }
        public string ServiceDescription { get; set; }
        public bool Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRequest> ServiceRequest { get; set; }
        public virtual Student Student { get; set; }
        public virtual Team Team { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceStoreServiceParams> ServiceStoreServiceParams { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceStoreServiceParams> ServiceStoreServiceParams1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceStoreUserTeams> ServiceStoreUserTeams { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceStoreParams> ServiceStoreParams { get; set; }
    }
}
