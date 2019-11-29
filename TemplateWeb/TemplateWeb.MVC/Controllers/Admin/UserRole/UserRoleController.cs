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

namespace TemplateWeb.MVC.Controllers
{
    public class UserRoleController : Controller
    {
        // GET: UserRole
        [FilterConfig.CheckSessionTimeOut()]
        [FilterConfig.CheckAuthorization()]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [FilterConfig.CheckSessionTimeOut()]
        public ActionResult InitiateData()
        {
            try
            {
                List<mUserRole> returnList = new List<mUserRole>();
                mUserRole headerDat = mUserRoleCustomBL.CreateBlankmUserRole();
                return Json(clsAPI.CreateResult(true, headerDat, string.Empty, string.Empty));
            }
            catch (Exception ex)
            {
                return Json(clsAPI.CreateError(ex));
            }
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [FilterConfig.CheckSessionTimeOut()]
        public ActionResult GetData(string txtID)
        {
            try
            {
                mUserRole retDat = new mUserRole();
                if (!txtID.Equals(string.Empty))
                {
                    retDat = mUserRoleCustomBL.GetMUserRole(clsGlobal.ParseToInteger(txtID));
                }

                return Json(clsAPI.CreateResult(true, retDat, string.Empty, string.Empty));
            }
            catch (Exception ex)
            {
                return Json(clsAPI.CreateError(ex));
            }
        }


        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [FilterConfig.CheckSessionTimeOut()]
        public ActionResult SaveData(string data)
        {
            try
            {
                bool bitSuccess = false;
                mUserRole objDat = new mUserRole();
                string txtStatus = string.Empty;
                if (!data.Equals(string.Empty))
                {
                    JObject jsonDat = JObject.Parse(data);
                    objDat = mUserRoleCustomBL.parseFromJSON(jsonDat);
                    if (mUserRoleCustomBL.IsExistMUserRole(objDat.intUserRoleID))
                    {
                        //Update 
                        bitSuccess = mUserRoleCustomBL.UpdateMUserRole(objDat, GlobalClass.dLogin.userDat.intUserID.ToString(), GlobalClass.dLogin.txtLangID);
                        txtStatus = "Data Successfully Saved";
                    }
                    else
                    {
                        //Create 
                        objDat.intUserRoleID = mUserRoleCustomBL.SaveMUserRole(objDat, GlobalClass.dLogin.userDat.intUserID.ToString(), GlobalClass.dLogin.txtLangID);
                        txtStatus = "Data Successfully Saved";
                        bitSuccess = true;
                    }
                }
                return Json(clsAPI.CreateResult(bitSuccess, objDat, txtStatus, string.Empty));
            }
            catch (Exception ex)
            {
                return Json(clsAPI.CreateError(ex));
            }
        }


        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [FilterConfig.CheckSessionTimeOut()]
        public ActionResult DeleteData(string data)
        {
            try
            {
                bool bitSuccess = false;
                mUserRole objDat = new mUserRole();
                string txtStatus = string.Empty;
                if (!data.Equals(string.Empty))
                {
                    JObject jsonDat = JObject.Parse(data);
                    objDat = mUserRoleCustomBL.parseFromJSON(jsonDat);
                    if (mUserRoleCustomBL.IsExistMUserRole(objDat.intUserRoleID))
                    {
                        //Delete 
                        bitSuccess = mUserRoleCustomBL.DeleteMUserRole(objDat.intUserRoleID);
                        txtStatus = "Data Successfully Deleted";
                    }
                }
                return Json(clsAPI.CreateResult(bitSuccess, mUserRoleCustomBL.CreateBlankmUserRole(), txtStatus, string.Empty));
            }
            catch (Exception ex)
            {
                return Json(clsAPI.CreateError(ex));
            }
        }


       
    }
}