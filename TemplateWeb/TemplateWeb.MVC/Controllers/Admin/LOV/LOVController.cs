using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateWeb.Common.Entity;
using TemplateWeb.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TemplateWeb.MVC;

public class LOVController : System.Web.Mvc.Controller
{
    public ActionResult Index()
    {

        return View();
    }

    [HttpPost()]
    [ValidateAntiForgeryToken()]
    public ActionResult GetDataSource(string data)
    {
        try
        {
            JArray jarrayDat = default(JArray);
            List<clsMLOV> returnList = new List<clsMLOV>();

            if (!data.Equals(string.Empty))
            {
                jarrayDat = (JArray)JsonConvert.DeserializeObject(data);
                if (jarrayDat != null)
                {
                    foreach (JObject jObj in jarrayDat)
                    {
                        string txtModule = jObj[clsGlobal.Request.Mdl].ToString();
                        string txtFunction = jObj[clsGlobal.Request.Fnc].ToString();
                        string txtQuery = jObj[clsGlobal.Request.Query].ToString();
                        decimal decTotalRow = 0;
                        returnList = ProcessData(txtModule, txtFunction, txtQuery, 0, 0, string.Empty, ref decTotalRow);
                    }
                }
            }

            return Json(clsAPI.CreateResult(true, returnList, string.Empty, string.Empty));
        }
        catch (Exception ex)
        {
            return Json(clsAPI.CreateError(ex));
        }
    }

    [HttpPost()]
    public ActionResult GetDataSourceJSONPaging(string data)
    {
        int intStart = clsGlobal.ParseToInteger(Request.Form["start"]);
        int intLength = clsGlobal.ParseToInteger(Request.Form["length"]);
        string txtSearch = Request.Form["search[value]"].ToString();
        int intDraw = clsGlobal.ParseToInteger(Request.Form["draw"]);
        decimal decTotalRow = decimal.Zero;
        intStart += 1;
        try
        {
            JArray jarrayDat = default(JArray);
            List<clsMLOV> returnList = new List<clsMLOV>();

            if (!data.Equals(string.Empty))
            {
                jarrayDat = (JArray)JsonConvert.DeserializeObject(data);
                if (jarrayDat != null)
                {
                    foreach (JObject jObj in jarrayDat)
                    {
                        string txtModule = jObj[clsGlobal.Request.Mdl].ToString();
                        string txtFunction = jObj[clsGlobal.Request.Fnc].ToString();
                        string txtQuery = jObj[clsGlobal.Request.Query].ToString();

                        returnList = ProcessData(txtModule, txtFunction, txtQuery, intStart, intStart + (intLength - 1), txtSearch, ref decTotalRow);
                    }
                }
            }

            return Json(clsAPIPaging.CreateResultJSONPaging(intDraw, (int)decTotalRow, (int)decTotalRow, returnList), JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(clsAPI.CreateError(ex));
        }
    }

    [HttpGet()]
    public ActionResult GetDataSource(string txtModule, string txtFunction, string txtQuery)
    {
        try
        {
            List<clsMLOV> returnList = new List<clsMLOV>();
            decimal decTotalRow = 0;
            returnList = ProcessData(txtModule, txtFunction, txtQuery, 0, 0, string.Empty, ref decTotalRow);
            return Json(clsAPI.CreateResult(true, returnList, string.Empty, string.Empty), JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(clsAPI.CreateError(ex));
        }
    }

    public List<clsMLOV> ProcessData(string txtModule, string txtFunction, string txtQuery, int intStart, int intEnd, string txtSearch, ref decimal decTotalRow)
    {
        List<clsMLOV> retList = new List<clsMLOV>();

        switch (txtModule.Trim().ToUpper())
        {
            
            //case "ROLE":
            //    retList = mRoleCustomBL.GetListLOVmRoleLOVPaging(GlobalClass.dLogin.userDat.intUserID.ToString(), intStart, intEnd, txtSearch, ref decTotalRow);
            //    break;
            //case "ROLEACCESS":
            //    retList = mRoleAccessCustomBL.GetListLOVmRoleAccessLOVPaging(GlobalClass.dLogin.userDat.intUserID.ToString(), intStart, intEnd, txtSearch, ref decTotalRow);
            //    break;
            
            //case "USER":
            //    retList = mUserCustomBL.GetListLOVmUserLOVPaging(GlobalClass.dLogin.userDat.intUserID.ToString(), intStart, intEnd, txtSearch, ref decTotalRow);
            //    break;
            //case "USERROLE":
            //    retList = mUserRoleCustomBL.GetListLOVmUserRoleLOVPaging(GlobalClass.dLogin.userDat.intUserID.ToString(), intStart, intEnd, txtSearch, ref decTotalRow);
            //    break;
        }
        return retList;
    }

}
