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
    public class MenuController : Controller
    {
        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Index()
        {
            try
            {
                return View(mMenuCustomBL.GetAllMenus());
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

                int intMenuID = Int32.Parse(clsRijndael.Decrypt(id));
                mMenu mMenu = mMenuCustomBL.GetMenu(intMenuID);

                if (mMenu == null)
                {
                    return HttpNotFound();
                }

                ViewBag.ModuleList = DropdownClass.ModuleList();
                ViewBag.ParentList = DropdownClass.ParentList();

                return View(mMenu);
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
                mMenu mMenu = new mMenu();
                mMenu.txtGUID = Guid.NewGuid().ToString();

                ViewBag.ModuleList = DropdownClass.ModuleList();
                ViewBag.ParentList = DropdownClass.ParentList();

                return View(mMenu);
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
        public ActionResult Create(mMenu mMenu, string btn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (btn == "Save")
                    {
                        mMenu.intMenuID = mMenuCustomBL.SaveMenu(mMenu, GlobalClass.dLogin.userDat.txtUserName.ToString());

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

            ViewBag.ModuleList = DropdownClass.ModuleList();
            ViewBag.ParentList = DropdownClass.ParentList();
            return View(mMenu);
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

                int intMenuID = Int32.Parse(clsRijndael.Decrypt(id));

                mMenu mMenu = mMenuCustomBL.GetMenu(intMenuID);
                mMenu.txtGUID = Guid.NewGuid().ToString();

                if (mMenu == null)
                {
                    return HttpNotFound();
                }

                ViewBag.ModuleList = DropdownClass.ModuleList();
                ViewBag.ParentList = DropdownClass.ParentList();
                return View(mMenu);
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
        public ActionResult Edit(mMenu mMenu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mMenuCustomBL.UpdateMenu(mMenu, GlobalClass.dLogin.userDat.txtUserName.ToString());

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

            ViewBag.ModuleList = DropdownClass.ModuleList();
            ViewBag.ParentList = DropdownClass.ParentList();
            return View(mMenu);
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

                int intMenuID = Int32.Parse(clsRijndael.Decrypt(id));
                mMenu mMenu = mMenuCustomBL.GetMenu(intMenuID);
                mMenu.txtGUID = Guid.NewGuid().ToString();

                if (mMenu == null)
                {
                    return HttpNotFound();
                }

                ViewBag.ModuleList = DropdownClass.ModuleList();
                ViewBag.ParentList = DropdownClass.ParentList();
                return View(mMenu);
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
        public ActionResult DeleteConfirmed(mMenu mMenu)
        {
            try
            {
                mMenuCustomBL.DeleteMenu(mMenu, GlobalClass.dLogin.userDat.txtUserName.ToString());

                TempData["Status"] = "SUCCES";
                TempData["Message"] = "Data is successfully deleted!";

                return RedirectToAction("/Index");
            }
            catch (Exception ex)
            {
                TempData["Status"] = "FAIL";
                TempData["Message"] = "ERROR : " + ex.Message;

                ViewBag.ModuleList = DropdownClass.ModuleList();
                ViewBag.ParentList = DropdownClass.ParentList();
                return View(mMenu);
            }
        }
    }
}
