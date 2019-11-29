using System.Web;
using System.Web.Mvc;
using System;

using TemplateWeb.Common;
using System.Web.Security;

namespace TemplateWeb.MVC
{
    public static class GlobalClass
    {   
        public static clsLogin dLogin
        {
            get { return (clsLogin)HttpContext.Current.Session[clsGlobal.SessionName.objclsAccount]; }
        } 
    }
}