//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TemplateWeb.Common.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class mMenu
    {
        //For FORM needed Only.
        public List<mMenu> itemList { get; set; }


        public int intMenuID { get; set; }
        public Nullable<int> intParentID { get; set; }
        [Required]
        [Display(Name = "Menu Name")]
        public string txtMenuName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string txtDescription { get; set; }
        [Required]
        [Display(Name = "Module")]
        public Nullable<int> intModuleID { get; set; }
        public string txtLink { get; set; }
        [Required]
        [Display(Name = "Order")]
        public Nullable<int> intOrderID { get; set; }
        public bool bitActive { get; set; }
        public bool bitMethod { get; set; }
        public string txtIcon { get; set; }
        public string txtGUID { get; set; }
        public string txtCreatedBy { get; set; }
        public Nullable<System.DateTime> dtmCreatedDate { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtmUpdatedDate { get; set; }

        public bool bitParent1 { get; set; }
        public bool bitParent2 { get; set; }
        public bool bitParent3 { get; set; }
        public bool bitParent4 { get; set; }
        public bool bitChecklist { get; set; }
    }
}
