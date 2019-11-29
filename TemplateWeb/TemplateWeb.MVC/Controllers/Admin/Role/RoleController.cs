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
    public class RoleController : Controller
    {
        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Index()
        {
            try
            {
                return View(mRoleCustomBL.GetAllIndex());
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

                int intRoleID = Int32.Parse(clsRijndael.Decrypt(id));

                mRoleVW mRole = Mapper.Map<mRole, mRoleVW>(mRoleCustomBL.GetRole(intRoleID));
                mRole.mMenu = Mapper.Map<List<mMenu>, List<mMenuVW>>(mRoleCustomBL.PopulateExistedMenu(intRoleID));

                if (mRole == null)
                {
                    return HttpNotFound();
                }
                return View(mRole);
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
                mRoleVW mRole = new mRoleVW();
                mRole.mMenu = Mapper.Map<List<mMenu>, List<mMenuVW>>(mRoleCustomBL.PopulateMenu());
                mRole.txtGUID = Guid.NewGuid().ToString();

                return View(mRole);
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
        public ActionResult Create(mRoleVW mRoleVW, string btn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (btn == "Save")
                    {
                        mRole mRole = Mapper.Map<mRoleVW, mRole>(mRoleVW);
                        List<mMenu> listMenu = Mapper.Map<List<mMenuVW>, List<mMenu>>(mRoleVW.mMenu);

                        mRole.intRoleID = mRoleCustomBL.SaveRole(mRole, listMenu, GlobalClass.dLogin.userDat.txtUserName.ToString());

                        TempData["Status"] = "SUCCES";
                        TempData["Message"] = "Data is successfully saved!";

                        return RedirectToAction("/Index");
                    }
                }

                return View(mRoleVW);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

                int intRoleID = Int32.Parse(clsRijndael.Decrypt(id));

                mRoleVW mRole = Mapper.Map<mRole, mRoleVW>(mRoleCustomBL.GetRole(intRoleID));
                mRole.mMenu = Mapper.Map<List<mMenu>, List<mMenuVW>>(mRoleCustomBL.PopulateExistedMenu(intRoleID));
                mRole.txtGUID = Guid.NewGuid().ToString();

                if (mRole == null)
                {
                    return HttpNotFound();
                }

                return View(mRole);
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
        public ActionResult Edit(mRoleVW mRoleVW)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mRole mRole = Mapper.Map<mRoleVW, mRole>(mRoleVW);
                    List<mMenu> listMenu = Mapper.Map<List<mMenuVW>, List<mMenu>>(mRoleVW.mMenu);

                    mRoleCustomBL.UpdateRole(mRole, listMenu, GlobalClass.dLogin.userDat.txtUserName.ToString());

                    TempData["Status"] = "SUCCES";
                    TempData["Message"] = "Data is successfully edited!";

                    return RedirectToAction("/Index");
                }

                return View(mRoleVW);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

                int intRoleID = Int32.Parse(clsRijndael.Decrypt(id));
                mRoleVW mRole = Mapper.Map<mRole, mRoleVW>(mRoleCustomBL.GetRole(intRoleID));
                mRole.mMenu = Mapper.Map<List<mMenu>, List<mMenuVW>>(mRoleCustomBL.PopulateExistedMenu(intRoleID));
                mRole.txtGUID = Guid.NewGuid().ToString();

                if (mRole == null)
                {
                    return HttpNotFound();
                }
                return View(mRole);
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
        public ActionResult DeleteConfirmed(mRoleVW mRoleVW)
        {
            try
            {
                mRole mRole = Mapper.Map<mRoleVW, mRole>(mRoleVW);
                List<mMenu> listMenu = Mapper.Map<List<mMenuVW>, List<mMenu>>(mRoleVW.mMenu);

                mRoleCustomBL.DeleteRole(mRole, listMenu, GlobalClass.dLogin.userDat.txtUserName.ToString());

                TempData["Status"] = "SUCCES";
                TempData["Message"] = "Data is successfully deleted!";

                return RedirectToAction("/Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}