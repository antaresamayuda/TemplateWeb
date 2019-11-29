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

namespace TemplateWeb.MVC.Controllers
{
    public class ModuleController : Controller
    {
        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Index()
        {
            try
            {
                return View(mModuleCustomBL.GetAllModules());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Details(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int intModuleID = Int32.Parse(clsRijndael.Decrypt(id));
                mModule mModule = mModuleCustomBL.GetModule(intModuleID);

                if (mModule == null)
                {
                    return HttpNotFound();
                }
                return View(mModule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Create()
        {
            try
            {
                mModule mModule = new mModule();
                mModule.txtGUID = Guid.NewGuid().ToString();

                return View(mModule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Create(mModule mModule, string btn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (btn == "Save")
                    {
                        mModule.intModuleID = mModuleCustomBL.SaveModule(mModule, GlobalClass.dLogin.userDat.txtUserName.ToString());

                        TempData["Status"] = "SUCCES";
                        TempData["Message"] = "Data is successfully saved!";

                        return RedirectToAction("/Index");
                    }
                }          
            }
            catch (Exception ex)
            {
                TempData["Status"] = "FAIL";
                TempData["Message"] = "ERROR : " + ex.Message;
            }

            return View(mModule);
        }

        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int intModuleID = Int32.Parse(clsRijndael.Decrypt(id));

                mModule mModule = mModuleCustomBL.GetModule(intModuleID);
                mModule.txtGUID = Guid.NewGuid().ToString();

                if (mModule == null)
                {
                    return HttpNotFound();
                }

                return View(mModule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Edit(mModule mModule)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mModuleCustomBL.UpdateModule(mModule, GlobalClass.dLogin.userDat.txtUserName.ToString());

                    TempData["Status"] = "SUCCES";
                    TempData["Message"] = "Data is successfully edited!";

                    return RedirectToAction("/Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Status"] = "FAIL";
                TempData["Message"] = "ERROR : " + ex.Message;
            }

            return View(mModule);
        }

        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int intModuleID = Int32.Parse(clsRijndael.Decrypt(id));
                mModule mModule = mModuleCustomBL.GetModule(intModuleID);
                mModule.txtGUID = Guid.NewGuid().ToString();

                if (mModule == null)
                {
                    return HttpNotFound();
                }
                return View(mModule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult DeleteConfirmed(mModule mModule)
        {
            try
            {
                mModuleCustomBL.DeleteModule(mModule, GlobalClass.dLogin.userDat.txtUserName.ToString());

                TempData["Status"] = "SUCCES";
                TempData["Message"] = "Data is successfully deleted!";

                return RedirectToAction("/Index");
            }
            catch (Exception ex)
            {
                TempData["Status"] = "FAIL";
                TempData["Message"] = "ERROR : " + ex.Message;

                return View(mModule);
            }
        }
    }
}
