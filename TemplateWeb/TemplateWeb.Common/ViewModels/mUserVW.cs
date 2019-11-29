using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TemplateWeb.Common.ViewModels
{
    public class mUserVW
    {
        public mUserVW()
        {
            mUserRoles = new List<mUserRoleVW>();
        }

        public int intUserID { get; set; }
        public string txtUserName { get; set; }
        public string txtName { get; set; }
        public int intCompanyID { get; set; }
        public string txtCompanyName { get; set; }
        public string txtAddress { get; set; }
        public string txtPhoneNumber { get; set; }
        public string txtEmail { get; set; }
        public Nullable<bool> bitActive { get; set; }
        public string txtPassword { get; set; }
        public string txtGUID { get; set; }
        public string txtCreatedBy { get; set; }
        public Nullable<System.DateTime> dtmCreatedDate { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtmUpdatedDate { get; set; }
        public virtual List<mUserRoleVW> mUserRoles { get; set; }
    }

    public class mUserRoleVW
    {
        public int intUserRoleID { get; set; }
        public int intRoleID { get; set; }
        public int intUserID { get; set; }
        public string txtCreatedBy { get; set; }
        public Nullable<System.DateTime> dtmCreatedDate { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtmUpdatedDate { get; set; }

        public virtual mRoleVW mRole { get; set; }
        public virtual mUserVW mUser { get; set; }
    }

    public class mUserChangePasswordVW
    {
        public int intUserID { get; set; }     
        [Required]
        [Display(Name = "Password")]
        public string txtPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "New Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string txtNewPassword { get; set; }
        [Required]
        [Display(Name = "Retype New Password")]
        [DataType(DataType.Password)]
        [Compare("txtNewPassword", ErrorMessage = "The passwords do not match.")]
        public string txtRetypeNewPassword { get; set; }
        public string txtName { get; set; }
    }
}