//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TemplateWeb.DAL.Edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class mRole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mRole()
        {
            this.mRoleAccesses = new HashSet<mRoleAccess>();
            this.mUserRoles = new HashSet<mUserRole>();
        }
    
        public int intRoleID { get; set; }
        public string txtRoleName { get; set; }
        public string txtGUID { get; set; }
        public string txtCreatedBy { get; set; }
        public Nullable<System.DateTime> dtmCreatedDate { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtmUpdatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mRoleAccess> mRoleAccesses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mUserRole> mUserRoles { get; set; }
    }
}
