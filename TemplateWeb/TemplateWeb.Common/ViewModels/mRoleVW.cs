using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TemplateWeb.Common.ViewModels
{
    public class mRoleVW
    {
        public mRoleVW()
        {
            mMenu = new List<mMenuVW>();
        }
        public int intRoleID { get; set; }
        public string txtRoleName { get; set; }
        public string txtGUID { get; set; }
        public string txtCreatedBy { get; set; }
        public Nullable<System.DateTime> dtmCreatedDate { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtmUpdatedDate { get; set; }
        public virtual List<mMenuVW> mMenu { get; set; }
    }

    public class mMenuVW
    {
        public mMenuVW()
        {
            bitChecklist = false;
        }
        public int intMenuID { get; set; }
        public Nullable<int> intParentID { get; set; }
        public string txtMenuName { get; set; }
        public string txtDescription { get; set; }
        public Nullable<int> intModuleID { get; set; }
        public string txtLink { get; set; }
        public Nullable<int> intOrderID { get; set; }
        public Nullable<bool> bitActive { get; set; }
        public string txtIcon { get; set; }
        public bool bitChecklist { get; set; }
        public bool bitParent1 { get; set; }
        public bool bitParent2 { get; set; }
        public bool bitParent3 { get; set; }
        public bool bitParent4 { get; set; }
    }
}