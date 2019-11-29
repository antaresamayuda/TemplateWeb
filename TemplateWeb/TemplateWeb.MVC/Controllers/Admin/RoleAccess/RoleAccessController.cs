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
    public class RoleAccessController : Controller
    {
        // GET: RoleAccess
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
                List<mRoleAccess> returnList = new List<mRoleAccess>();
                mRoleAccess headerDat = mRoleAccessCustomBL.CreateBlankmRoleAccess();
                return Json(clsAPI.CreateResult(true, headerDat, string.Empty, string.Empty));
            }
            catch (Exception ex)
            {
                return Json(clsAPI.CreateError(ex));            
            }
        }

        [HttpPost()]
        public ActionResult PopulateRole()
        {
            try
            {
                List<mRole> returnList = new List<mRole>();
                returnList = mRoleCustomBL.GetAllRoles();
                return Json(clsAPI.CreateResult(true, returnList, string.Empty, string.Empty));
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
                mRoleAccess retDat = new mRoleAccess();
                if (!txtID.Equals(string.Empty))
                {
                    retDat = mRoleAccessCustomBL.GetMRoleAccess(clsGlobal.ParseToInteger(txtID));
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
                mRoleAccess objDat = new mRoleAccess();
                string txtStatus = string.Empty;
                if (!data.Equals(string.Empty))
                {
                    JObject jsonDat = JObject.Parse(data);
                    objDat = mRoleAccessCustomBL.parseFromJSON(jsonDat);
                    mRoleAccessCustomBL.ValidateInput(objDat, GlobalClass.dLogin.userDat.intUserID.ToString(), GlobalClass.dLogin.txtLangID);
                    if (mRoleAccessCustomBL.IsExistMRoleAccess(objDat.intRoleAccessID))
                    {
                        //Update 
                        bitSuccess = mRoleAccessCustomBL.UpdateMRoleAccess(objDat, GlobalClass.dLogin.userDat.intUserID.ToString(), GlobalClass.dLogin.txtLangID);
                        txtStatus = "Data Successfully Saved";
                    }
                    else
                    {
                        //Create 
                        objDat.intRoleAccessID = mRoleAccessCustomBL.SaveMRoleAccess(objDat, GlobalClass.dLogin.userDat.intUserID.ToString(), GlobalClass.dLogin.txtLangID);
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
                mRoleAccess objDat = new mRoleAccess();
                string txtStatus = string.Empty;
                if (!data.Equals(string.Empty))
                {
                    JObject jsonDat = JObject.Parse(data);
                    objDat = mRoleAccessCustomBL.parseFromJSON(jsonDat);
                    if (mRoleAccessCustomBL.IsExistMRoleAccess(objDat.intRoleAccessID))
                    {
                        //Delete 
                        bitSuccess = mRoleAccessCustomBL.DeleteMRoleAccess(objDat.intRoleAccessID);
                        txtStatus = "Data Successfully Deleted";
                    }
                }
                return Json(clsAPI.CreateResult(bitSuccess, mRoleAccessCustomBL.CreateBlankmRoleAccess(), txtStatus, string.Empty));
            }
            catch (Exception ex)
            {
                return Json(clsAPI.CreateError(ex));
            }
        }

        
    }
}