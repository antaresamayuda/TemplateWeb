using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateWeb.MVC.Controllers
{
    public class HomeController : Controller
    {
        [FilterConfig.CheckSessionTimeOut()]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        [FilterConfig.CheckSessionTimeOut()]
        public ActionResult Authorization()
        {
            TempData["Status"] = "FAIL";
            TempData["Message"] = "Authorization Failed!!!";

            return View();
        }

        public string Encrypt(string id)
        {
            var encryptedID = Common.clsRijndael.Encrypt(id);

            return encryptedID;
        }

        public string Decrypt(string text)
        {
            var decryptedTEXT = Common.clsRijndael.Decrypt(text);

            return decryptedTEXT;
        }
         
    }
}
