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
    
    public partial class Users : IBaseModel
    {
        public int ID { get; set; }
        public string NEPTUN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public Nullable<bool> Deleted { get; set; }
    }
}
