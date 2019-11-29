using System.Web;
using System.Web.Mvc;
using System;

using TemplateWeb.Common;
using System.Web.Security;
using System.Security;
using TemplateWeb.Common.Entity;
using System.Collections.Generic;

namespace TemplateWeb.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }


        //public static clsLogin dLogin
        //{
        //    get { return (clsLogin)HttpContext.Current.Session[clsGlobal.SessionName.objclsAccount]; }
        //}
         
        [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
        public class CheckSessionTimeOutAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
            {
                dynamic context = filterContext.HttpContext;
                if (context.Session != null)
                {
                    if (GlobalClass.dLogin == null)
                    {
                        if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                        {
                            mUser objclsAccount = new mUser();
                            objclsAccount = mUserCustomBL.GetMUserbyTxtUserName(filterContext.HttpContext.User.Identity.Name.Trim());

                            if (objclsAccount != null)
                            {
                                clsLogin dLogin = new clsLogin();
                                dLogin.txtLangID = clsMMainConstant.CONFIGURATION.DefaultValue.DefaultLangID;
                                dLogin.userDat = objclsAccount;
                                 
                                //Role 
                                if (objclsAccount != null)
                                {
                                    List<mUserRole> userRoleList = mUserRoleCustomBL.GetAllmUserRoleByRoleAndUser(0,objclsAccount.intUserID);
                                    if (userRoleList != null)
                                    {
                                        if (userRoleList.Count > 0)
                                        {
                                            dLogin.intRoleID = clsGlobal.ParseToInteger(userRoleList[0].intRoleID);
                                        }
                                    }
                                }
                                filterContext.HttpContext.Session[clsGlobal.SessionName.objclsAccount] = dLogin;
                                filterContext.HttpContext.Session.Timeout = 60; 
                            }
                            else
                            {
                                //Check jika user Login masih ada. 
                                HttpContext.Current.Session.Clear();
                                FormsAuthentication.SignOut();

                                //  string redirectTo = clsPathHelper.AppVirtualDirectory + "/Account/Login";
                                string redirectTo = "~/Account/Login"; //+ HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri);
                                filterContext.Result = new RedirectResult(redirectTo);
                            }
                        }
                        else
                        {

                            //Check jika user Login masih ada. 
                            HttpContext.Current.Session.Clear();
                            FormsAuthentication.SignOut();

                            //  string redirectTo = clsPathHelper.AppVirtualDirectory + "/Account/Login";
                            string redirectTo = "~/Account/Login?returnUrl=" + HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri);
                            filterContext.Result = new RedirectResult(redirectTo);
                        }

                    } 
                }
                base.OnActionExecuting(filterContext);
            }
        }

        [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
        public class CheckAuthorizationAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
            {
                dynamic context = filterContext.HttpContext;
                if (context.Session != null)
                {
                    if (GlobalClass.dLogin != null)
                    {
                        //Check jika user bisa akses atau tidak.
                        string txtUrl = filterContext.HttpContext.Request.CurrentExecutionFilePath.ToString();
                        mRoleAccess RoleAccessDat = mRoleAccessCustomBL.GetPrivilegeUserUrl(GlobalClass.dLogin.intRoleID, txtUrl);

                        if (RoleAccessDat == null)
                        {
                            string redirectTo = "~/Home/Authorization";
                            filterContext.Result = new RedirectResult(redirectTo);
                        }

                        //Update log
                        mUserLog logs = new mUserLog();
                        logs.intUserID = GlobalClass.dLogin.userDat.intUserID;
                        logs.txtUrl = txtUrl;
                        logs.dtmLog = DateTime.Now;
                        mUserCustomBL.SaveLog(logs);
                    }

                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
}