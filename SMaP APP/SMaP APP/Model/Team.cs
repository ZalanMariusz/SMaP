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
    
    public partial class Team : IBaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            this.ServiceRequest = new HashSet<ServiceRequest>();
            this.ServiceRequest1 = new HashSet<ServiceRequest>();
            this.ServiceStore = new HashSet<ServiceStore>();
            this.ServiceTable = new HashSet<ServiceTable>();
            this.ServiceStoreUserTeams = new HashSet<ServiceStoreUserTeams>();
        }
    
        public int ID { get; set; }
        public int SessionGroupID { get; set; }
        public string TeamName { get; set; }
        public Nullable<int> TeamCaptain { get; set; }
        public bool Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRequest> ServiceRequest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRequest> ServiceRequest1 { get; set; }
        public virtual SessionGroup SessionGroup { get; set; }
        public virtual Student Student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceStore> ServiceStore { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceTable> ServiceTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceStoreUserTeams> ServiceStoreUserTeams { get; set; }
    }
}
