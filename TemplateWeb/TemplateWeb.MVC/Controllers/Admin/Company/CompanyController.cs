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
    public class CompanyController : Controller
    {
        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Index()
        {
            try
            {
                return View(mCompanyCustomBL.GetAllCompanies());
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

                int intCompanyID = Int32.Parse(clsRijndael.Decrypt(id));
                mCompany mCompany = mCompanyCustomBL.GetCompany(intCompanyID);

                if (mCompany == null)
                {
                    return HttpNotFound();
                }
                return View(mCompany);
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
                mCompany mCompany = new mCompany();
                mCompany.txtGUID = Guid.NewGuid().ToString();

                ViewBag.BidangUsahaList = DropdownClass.BidangUsahaList();

                return View(mCompany);
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
        public ActionResult Create(mCompany mCompany, string btn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (btn == "Save")
                    {
                        mCompany.intCompanyID = mCompanyCustomBL.SaveCompany(mCompany, GlobalClass.dLogin.userDat.txtUserName.ToString());

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

            ViewBag.BidangUsahaList = DropdownClass.BidangUsahaList();
            return View(mCompany);
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

                int intCompanyID = Int32.Parse(clsRijndael.Decrypt(id));
                                
                mCompany mCompany = mCompanyCustomBL.GetCompany(intCompanyID);
                mCompany.txtGUID = Guid.NewGuid().ToString();

                if (mCompany == null)
                {
                    return HttpNotFound();
                }

                ViewBag.BidangUsahaList = DropdownClass.BidangUsahaList();
                return View(mCompany);
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
        public ActionResult Edit(mCompany mCompany)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mCompanyCustomBL.UpdateCompany(mCompany, GlobalClass.dLogin.userDat.txtUserName.ToString());

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

            ViewBag.BidangUsahaList = DropdownClass.BidangUsahaList();
            return View(mCompany);
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

                int intCompanyID = Int32.Parse(clsRijndael.Decrypt(id));
                mCompany mCompany = mCompanyCustomBL.GetCompany(intCompanyID);
                mCompany.txtGUID = Guid.NewGuid().ToString();

                if (mCompany == null)
                {
                    return HttpNotFound();
                }
                return View(mCompany);
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
        public ActionResult DeleteConfirmed(mCompany mCompany)
        {
            try
            {
                mCompanyCustomBL.DeleteCompany(mCompany, GlobalClass.dLogin.userDat.txtUserName.ToString());

                TempData["Status"] = "SUCCES";
                TempData["Message"] = "Data is successfully deleted!";

                return RedirectToAction("/Index");
            }
            catch (Exception ex)
            {
                TempData["Status"] = "FAIL";
                TempData["Message"] = "ERROR : " + ex.Message;

                ViewBag.BidangUsahaList = DropdownClass.BidangUsahaList();
                return View(mCompany);
            }           
        }
    }
}