using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TemplateWeb.Common.Entity;
using TemplateWeb.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading;
using System.Net;
using System.Data.Entity;
using System.Collections.ObjectModel;
using TemplateWeb.Common.ViewModels;
using AutoMapper;

namespace TemplateWeb.MVC.Controllers
{
    public class UnlockUserController : Controller
    {
        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Index()
        {
            try
            {
                return View(mUserCustomBL.GetAllUnlockIndex());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [FilterConfig.CheckSessionTimeOut()]
        public ActionResult UnlockUser(string id)
        {
            try
            {
                int intUserID = Int32.Parse(clsRijndael.Decrypt(id));
                mUserCustomBL.UnlockUser(intUserID);

                TempData["Status"] = "SUCCES";
                TempData["Message"] = "User is successfully unlocked! The password is changed to default!";

                return RedirectToAction("/Index", "UnlockUser");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}