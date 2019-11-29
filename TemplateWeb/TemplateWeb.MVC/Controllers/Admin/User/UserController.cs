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
    public class UserController : Controller
    {
        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Index()
        {
            try
            {
                return View(mUserCustomBL.GetAllIndex());
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

                int intUserID = Int32.Parse(clsRijndael.Decrypt(id));

                mUserVW mUser = Mapper.Map<mUser, mUserVW>(mUserCustomBL.GetUser(intUserID));
                mUser.mUserRoles = Mapper.Map<List<mUserRole>, List<mUserRoleVW>>(mUserCustomBL.GetUserRole(intUserID));

                ViewBag.RoleList = RoleList();
                ViewBag.CompanyList = CompanyList();

                if (mUser == null)
                {
                    return HttpNotFound();
                }
                return View(mUser);
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
                mUserVW mUser = new mUserVW();
                mUser.mUserRoles.Add(new mUserRoleVW());
                mUser.txtGUID = Guid.NewGuid().ToString();
                mUser.bitActive = true;
                mUser.txtPassword = "1234";

                ViewBag.RoleList = RoleList();
                ViewBag.CompanyList = CompanyList();

                return View(mUser);
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
        public ActionResult Create(mUserVW mUserVW, string btn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (btn == "Save")
                    {
                        mUser mUser = Mapper.Map<mUserVW, mUser>(mUserVW);
                        List<mUserRole> listUserRole = Mapper.Map<List<mUserRoleVW>, List<mUserRole>>(mUserVW.mUserRoles);

                        mUser.intUserID = mUserCustomBL.SaveUser(mUser, listUserRole, GlobalClass.dLogin.userDat.txtUserName.ToString());

                        TempData["Status"] = "SUCCES";
                        TempData["Message"] = "Data is successfully saved!";

                        return RedirectToAction("/Index");
                    }
                }

                ViewBag.RoleList = RoleList();
                ViewBag.CompanyList = CompanyList();
                return View(mUserVW);
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

                int intUserID = Int32.Parse(clsRijndael.Decrypt(id));

                mUserVW mUser = Mapper.Map<mUser, mUserVW>(mUserCustomBL.GetUser(intUserID));
                mUser.mUserRoles = Mapper.Map<List<mUserRole>, List<mUserRoleVW>>(mUserCustomBL.GetUserRole(intUserID));
                mUser.txtGUID = Guid.NewGuid().ToString();

                ViewBag.RoleList = RoleList();
                ViewBag.CompanyList = CompanyList();

                if (mUser == null)
                {
                    return HttpNotFound();
                }

                return View(mUser);
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
        public ActionResult Edit(mUserVW mUserVW)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mUser mUser = Mapper.Map<mUserVW, mUser>(mUserVW);
                    List<mUserRole> listUserRole = Mapper.Map<List<mUserRoleVW>, List<mUserRole>>(mUserVW.mUserRoles);

                    mUserCustomBL.UpdateUser(mUser, listUserRole, GlobalClass.dLogin.userDat.txtUserName.ToString());

                    TempData["Status"] = "SUCCES";
                    TempData["Message"] = "Data is successfully edited!";

                    return RedirectToAction("/Index");
                }

                ViewBag.RoleList = RoleList();
                ViewBag.CompanyList = CompanyList();
                return View(mUserVW);
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

                int intUserID = Int32.Parse(clsRijndael.Decrypt(id));
                mUserVW mUser = Mapper.Map<mUser, mUserVW>(mUserCustomBL.GetUser(intUserID));
                mUser.mUserRoles = Mapper.Map<List<mUserRole>, List<mUserRoleVW>>(mUserCustomBL.GetUserRole(intUserID));
                mUser.txtGUID = Guid.NewGuid().ToString();

                ViewBag.RoleList = RoleList();
                ViewBag.CompanyList = CompanyList();

                if (mUser == null)
                {
                    return HttpNotFound();
                }
                return View(mUser);
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
        public ActionResult DeleteConfirmed(mUserVW mUserVW)
        {
            try
            {
                mUser mUser = Mapper.Map<mUserVW, mUser>(mUserVW);
                List<mUserRole> listUserRole = Mapper.Map<List<mUserRoleVW>, List<mUserRole>>(mUserVW.mUserRoles);

                mUserCustomBL.DeleteUser(mUser, listUserRole, GlobalClass.dLogin.userDat.txtUserName.ToString());

                TempData["Status"] = "SUCCES";
                TempData["Message"] = "Data is successfully deleted!";

                return RedirectToAction("/Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<SelectListItem> RoleList()
        {
            Collection<SelectListItem> list = new Collection<SelectListItem>();
            list.Add(new SelectListItem() { Text = "-", Value = "" });

            List<mRole> dataList = new List<mRole>();
            dataList = mRoleCustomBL.GetAllRoles();

            foreach (var data in dataList)
            {
                list.Add(new SelectListItem() { Text = data.txtRoleName, Value = data.intRoleID.ToString() });
            }

            return list;
        }

        private IEnumerable<SelectListItem> CompanyList()
        {
            Collection<SelectListItem> list = new Collection<SelectListItem>();
            list.Add(new SelectListItem() { Text = "-", Value = "" });

            List<mCompany> dataList = new List<mCompany>();
            dataList = mCompanyCustomBL.GetAllCompanies();

            foreach (var data in dataList)
            {
                list.Add(new SelectListItem() { Text = data.txtCompanyName, Value = data.intCompanyID.ToString() });
            }

            return list;
        }
    }
}