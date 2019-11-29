using System;
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
using TemplateWeb.Common.ViewModels;
using AutoMapper;
using TemplateWeb.MVC;

namespace TemplateWeb.Common
{
    public class UserAccessAuthorization
    {
        public static string UserAccess(string txtUrl)
        {
            mRoleAccess RoleAccessDat = mRoleAccessCustomBL.GetPrivilegeUserUrl(GlobalClass.dLogin.intRoleID, txtUrl);

            if (RoleAccessDat == null)
            {
                return "none";
            }
            else
            {
                return "normal";
            }           
        }
    }
}
