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
    
    public partial class ServiceStoreParams : IBaseModel
    {
        public int ID { get; set; }
        public int ServiceID { get; set; }
        public bool InOut { get; set; }
        public Nullable<int> ServiceTableFieldID { get; set; }
        public string ParamName { get; set; }
        public bool IsCustom { get; set; }
        public Nullable<int> CustomParamTypeID { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Dictionary Dictionary { get; set; }
        public virtual ServiceStore ServiceStore { get; set; }
        public virtual ServiceTableField ServiceTableField { get; set; }
    }
}
