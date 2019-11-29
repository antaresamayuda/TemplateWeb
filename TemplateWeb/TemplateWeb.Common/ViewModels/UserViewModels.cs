using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using TemplateWeb.Common.Entity;

namespace TemplateWeb.Common.ViewModels
{
    public class UserViewModels
    {
        public int intUserID { get; set; }
        [Required]
        public string txtUserName { get; set; }
        [Required]
        public string txtName { get; set; }
        public int intCompanyID { get; set; }
        [Required]
        public string txtCompanyName { get; set; }
        public int intRoleID { get; set; }
        [Required]
        public string txtRoleName { get; set; }
        public string txtAddress { get; set; }
        public string txtPhoneNumber { get; set; }
        [Required]
        public string txtEmail { get; set; }
        public Nullable<bool> bitActive { get; set; }
        public string txtPassword { get; set; }
        public string txtCreatedBy { get; set; }
        public Nullable<System.DateTime> dtmCreatedDate { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtmUpdatedDate { get; set; }
    }
}