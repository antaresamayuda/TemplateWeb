using System.Web;
using System.Web.Mvc;
using System;

using TemplateWeb.Common;
using System.Web.Security;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TemplateWeb.Common.Entity;

namespace TemplateWeb.MVC
{
    public class DropdownClass
    {
        public DropdownClass()
        {
        }

        public static IEnumerable<SelectListItem> ModuleList()
        {
            Collection<SelectListItem> list = new Collection<SelectListItem>();
            list.Add(new SelectListItem() { Text = "-", Value = "" });

            List<mModule> dataList = new List<mModule>();
            dataList = mModuleCustomBL.GetAllModules();

            foreach (var data in dataList)
            {
                list.Add(new SelectListItem() { Text = data.txtModuleName, Value = data.intModuleID.ToString() });
            }

            return list;
        }

        public static IEnumerable<SelectListItem> ParentList()
        {
            Collection<SelectListItem> list = new Collection<SelectListItem>();
            list.Add(new SelectListItem() { Text = "-", Value = "0" });

            List<mMenu> dataList = new List<mMenu>();
            dataList = mMenuCustomBL.GetAllMenus();

            foreach (var data in dataList)
            {
                list.Add(new SelectListItem() { Text = data.txtMenuName, Value = data.intMenuID.ToString() });
            }

            return list;
        }

        public static IEnumerable<SelectListItem> BidangUsahaList()
        {
            Collection<SelectListItem> list = new Collection<SelectListItem>();
            list.Add(new SelectListItem() { Text = "-", Value = "" });

            List<mBidangUsaha> dataList = new List<mBidangUsaha>();
            dataList = mCompanyCustomBL.GetAllBidangUsaha();

            foreach (var data in dataList)
            {
                list.Add(new SelectListItem() { Text = data.txtBidangUsaha, Value = data.txtBidangUsaha });
            }

            return list;
        }
    }
}