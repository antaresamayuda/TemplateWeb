using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using TemplateWeb.Common.Entity;

namespace TemplateWeb.Common.ViewModels
{
    public class MenuViewModels
    {
        public List<mMenu> itemList { get; set; }

		[DataMember]
		public int intMenuID { get; set; }
		[DataMember]
		public int? intParentID { get; set; }
		[DataMember]
		public string txtMenuName { get; set; }
		[DataMember]
		public string txtDescription { get; set; }
		[DataMember]
		public int? intModuleID { get; set; }
		[DataMember]
		public string txtLink { get; set; }
		[DataMember]
		public int? intOrderID { get; set; }
		[DataMember]
		public bool? bitActive { get; set; }
		[DataMember]
		public string txtCreateBy { get; set; }
		[DataMember]
		public DateTime? dtCreateDate { get; set; }
		[DataMember]
		public string txtUpdateBy { get; set; }
		[DataMember]
		public DateTime? dtUpdateDate { get; set; }
        [DataMember]
        public string txtParentName { get; set; }
        [DataMember]
        public string txtModuleName { get; set; }
    }
}