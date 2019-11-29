using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using TemplateWeb.Filters;
using TemplateWeb.MVC.Models;
using TemplateWeb.Common;
using TemplateWeb.Common.Entity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BotDetect.Web.Mvc;
using TemplateWeb.Common.ViewModels;
using System.Net.Mail;
using System.Net;

namespace TemplateWeb.MVC.Controllers
{
    public class AccountController : Controller
    {
        private LoginContext context = new LoginContext();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = HttpUtility.UrlDecode(returnUrl);
            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [CaptchaValidation("txtCaptcha", "SampleCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool bolSuccess = false;
                clsLogin dLogin = new clsLogin();
                dLogin.txtLangID = clsMMainConstant.CONFIGURATION.DefaultValue.DefaultLangID;

                mUser userDat = mUserCustomBL.GetMUserbyTxtUserName(model.txtUserName.Trim());
                mUserRecord record = mUserCustomBL.GetUserRecord(userDat.intUserID);

                if(record.dtmLastWrongPass != null)
                {
                    if (DateTime.Now.AddDays(-1) > record.dtmLastWrongPass)
                    {
                        record.intWrongPassCount = 0;
                        record.dtmLastWrongPass = null;
                    }
                }
                
                if (DateTime.Now.AddMonths(-3) > record.dtmLastLogin)
                {
                    var idlink = Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/ResetPassword/" + clsRijndael.Encrypt(Guid.NewGuid() + "," + userDat.intUserID.ToString() + "," + DateTime.Now.ToString());
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("noreply@banksinarmas.com");
                    mailMessage.Subject = "Reset Expired Password - Template Web";
                    mailMessage.Body = "Dear " + userDat.txtName + ", here the link to reset your expired password<br/><br/><a href='" + idlink + "'>" + idlink + "</a>";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(userDat.txtEmail));

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.banksinarmas.com";
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Port = 25;

                    smtpClient.Send(mailMessage);

                    ModelState.AddModelError("", "The password is expired (3 months). Reset link has been sucessfully sent to your email!");
                }
                else if (record.bitLock == true)
                {
                    ModelState.AddModelError("", "The user name is locked. Please contact User Management or Administrator!");
                }
                else
                {
                    if (userDat.txtPassword == model.txtPassword)
                    {
                        return p_SetUserLoginAndRedirect(userDat, model, returnUrl);
                    }

                    else
                    {
                        record.intWrongPassCount = record.intWrongPassCount + 1;
                        record.dtmLastWrongPass = DateTime.Now;

                        if (record.intWrongPassCount == 3)
                        {
                            record.bitLock = true;                           
                            ModelState.AddModelError("", "The user name is locked. Please contact User Management or Administrator!");
                        }
                        else
                        {
                            ModelState.AddModelError("", "The user name or password is incorrect.");
                        }                       
                    }
                }

                model.txtCaptcha = "";
                mUserCustomBL.UpdateRecord(record);
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ChooseRole(string returnUrl)
        {
            clsLogin dLogin = new clsLogin();
            dLogin = (clsLogin)Session[clsGlobal.SessionName.objclsAccount];
            ViewBag.txtUserName = dLogin.userDat.txtUserName;
            ViewBag.ReturnUrl = returnUrl;
            return View();

        }
        [HttpPost]
        [AllowAnonymous] 
        public ActionResult ChooseRole(LoginModel model, string returnUrl)
        {
            clsLogin dLogin = new clsLogin();
            dLogin = (clsLogin)Session[clsGlobal.SessionName.objclsAccount];
            dLogin.intRoleID = clsGlobal.ParseToInteger(model.txtRole);
            Session[clsGlobal.SessionName.objclsAccount] = dLogin;
            
            if(returnUrl == null)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                return Redirect(returnUrl);
            }

            
        }

        private ActionResult p_SetUserLoginAndRedirect(mUser objclsAccount,LoginModel model, string returnUrl)
        {
            List<mUserRole> userRoleList = mUserRoleCustomBL.GetAllmUserRoleByRoleAndUser(0, objclsAccount.intUserID);
            clsLogin dLogin = new clsLogin();
            bool bitChooseRole = false;
            bool bitNoRole = false;
            if (userRoleList.Count == 1)
            {
                dLogin.intRoleID = clsGlobal.ParseToInteger(userRoleList[0].intRoleID);
            }
            else if (userRoleList.Count > 1)
            {
                bitChooseRole = true;
                bitNoRole = false;
            }
            else
            {
                bitNoRole = true;
            }

            dLogin.txtLangID = clsMMainConstant.CONFIGURATION.DefaultValue.DefaultLangID;
            dLogin.userDat = objclsAccount;
            Session[clsGlobal.SessionName.objclsAccount] = dLogin;
            Session.Timeout = 120;

            string sUserLogin = objclsAccount.intUserID.ToString();
            HttpCookie cookie = FormsAuthentication.GetAuthCookie(sUserLogin, true);
            FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(cookie.Value);
            FormsAuthenticationTicket Newticket = new FormsAuthenticationTicket(sUserLogin, true, 1000);
            String strEncryptedTicket = FormsAuthentication.Encrypt(oldTicket);
            cookie.Value = strEncryptedTicket;
            HttpContext.Response.Cookies.Add(cookie);
            FormsAuthentication.SetAuthCookie(sUserLogin, true);

            //Update Record
            mUserRecord record = mUserCustomBL.GetUserRecord(objclsAccount.intUserID);
            record.dtmLastLogin = DateTime.Now;
            record.intWrongPassCount = 0;
            record.dtmLastWrongPass = null;
            mUserCustomBL.UpdateRecord(record);

            //RedirectToLocal(returnUrl);
            if (bitChooseRole == true)
            {
                return RedirectToAction("ChooseRole", "Account", new { returnUrl = returnUrl });
            }
            else if (bitNoRole == true)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if(returnUrl == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
                
            }
        }
                  
        public ActionResult LogOff()
        { 
            Session[clsGlobal.SessionName.objclsAccount] = null; 
            Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        public ActionResult ChangePassword()
        {
            mUserChangePasswordVW VW = new mUserChangePasswordVW();
            VW.intUserID = GlobalClass.dLogin.userDat.intUserID;
            return View(VW);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        [FilterConfig.CheckSessionTimeOut()]
        public ActionResult ChangePassword(mUserChangePasswordVW VW)
        {
            mUserRecord record = mUserCustomBL.GetUserRecord(GlobalClass.dLogin.userDat.intUserID);
           
            if (VW.txtPassword != GlobalClass.dLogin.userDat.txtPassword)
            {
                TempData["Status"] = "FAIL";
                TempData["Message"] = "Password Lama is incorrect!";

                return View(VW);
            }

            if (VW.txtNewPassword != VW.txtRetypeNewPassword)
            {
                TempData["Status"] = "FAIL";
                TempData["Message"] = "New password is not match!";

                return View(VW);
            }

            if(VW.txtNewPassword == record.txtPreviousPass1 || VW.txtNewPassword == record.txtPreviousPass2 || VW.txtNewPassword == record.txtPreviousPass3)
            {
                TempData["Status"] = "FAIL";
                TempData["Message"] = "You have used this password before. Please insert another new password!";

                return View(VW);
            }

            mUserCustomBL.ChangePassword(VW, GlobalClass.dLogin.userDat.txtUserName);

            //Update Record
            record.txtPreviousPass3 = record.txtPreviousPass2;
            record.txtPreviousPass2 = record.txtPreviousPass1;
            record.txtPreviousPass1 = VW.txtNewPassword;
            record.intWrongPassCount = 0;
            record.dtmLastWrongPass = null;
            record.dtmLastLogin = DateTime.Now;
            mUserCustomBL.UpdateRecord(record);

            clsLogin dLogin = new clsLogin();
            dLogin.intRoleID = GlobalClass.dLogin.intRoleID;
            dLogin.txtLangID = clsMMainConstant.CONFIGURATION.DefaultValue.DefaultLangID;
            dLogin.userDat = mUserCustomBL.GetUser(VW.intUserID);
            Session[clsGlobal.SessionName.objclsAccount] = dLogin;

            TempData["Status"] = "SUCCES";
            TempData["Message"] = "Password is successfully changed!";

            return RedirectToAction("Index", "Home");
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public ActionResult PopulateRole(string txtUserName)
        {
            List<mUserRole> itemList = new List<mUserRole>();
            List<mRole> retList = new List<mRole>();
            if (!txtUserName.Trim().Equals(string.Empty))
            {
                //Use SQL user
                mUser userDat = mUserCustomBL.GetMUserbyTxtUserName(txtUserName.Trim()); 
                if ((userDat != null))
                {
                    //Populate Role berdasarkan user
                    itemList = mUserRoleCustomBL.GetAllmUserRoleByRoleAndUser(0, userDat.intUserID);
                    foreach (mUserRole itemDat in itemList)
                    {
                        mRole roleDat = mRoleCustomBL.GetRole(itemDat.intRoleID);
                        if (roleDat != null)
                        {
                            retList.Add(roleDat);
                        }
                    }
                }
            }

            return Json(retList);
        }
         
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ForgetPassword(LoginModel model)
        {
            var Message = "";
            var Error = "";

            mUser userDat = mUserCustomBL.GetMUserbyTxtUserName(model.txtUserName.Trim());
            
            if (userDat != null)
            {
                mUserRecord record = mUserCustomBL.GetUserRecord(userDat.intUserID);

                try
                {
                    if(record.bitLock == true)
                    {
                        Error = "The user name is locked. Please contact User Management or Administrator!";

                    }
                    else
                    {
                        var idlink = Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/ResetPassword/" + clsRijndael.Encrypt(Guid.NewGuid() + "," + userDat.intUserID.ToString() + "," + DateTime.Now.ToString());
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress("noreply@banksinarmas.com");
                        mailMessage.Subject = "Reset Password - Template Web";
                        mailMessage.Body = "Dear " + userDat.txtName + ", here the link to reset your password<br/><br/><a href='" + idlink + "'>" + idlink + "</a>";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(userDat.txtEmail));

                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = "mail.banksinarmas.com";
                        smtpClient.EnableSsl = true;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.UseDefaultCredentials = true;
                        smtpClient.Port = 25;

                        smtpClient.Send(mailMessage);

                        Message = "Reset link has been sucessfully sent to your email. Please check your inbox or spam folder!";
                    }
                    
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                }               
            }
            else
            {
                Error = "Sorry username is not found!";
            }

            return RedirectToAction("ResetLinkSent", "Account", new { Message = Message, Error = Error });
        }

        [AllowAnonymous]
        public ActionResult ResetLinkSent(string Message, string Error)
        {
            if(Message != "")
            {
                ViewBag.Message = Message;
            }

            if (Error != "")
            {
                ViewBag.Error = Error;
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string id)
        {
            int intUserID = Int32.Parse((clsRijndael.Decrypt(id)).Split(',')[1].ToString());

            if (DateTime.Now < DateTime.Parse((clsRijndael.Decrypt(id)).Split(',')[2].ToString()).AddMinutes(15))
            {
                mUser userDat = mUserCustomBL.GetUser(intUserID);

                mUserChangePasswordVW VW = new mUserChangePasswordVW();
                VW.intUserID = intUserID;
                VW.txtName = userDat.txtName;

                return View(VW);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ResetPassword(mUserChangePasswordVW VW)
        {
            mUserRecord record = mUserCustomBL.GetUserRecord(VW.intUserID);

            if (VW.txtNewPassword != VW.txtRetypeNewPassword)
            {
                ViewBag.Error = "New password is not match!";

                return View(VW);
            }
            else if (VW.txtNewPassword == record.txtPreviousPass1 || VW.txtNewPassword == record.txtPreviousPass2 || VW.txtNewPassword == record.txtPreviousPass3)
            {
                ViewBag.Error = "You have used this password before. Please insert another new password!";

                return View(VW);
            }
            else
            {
                mUserCustomBL.ChangePassword(VW, VW.txtName);

                //Update Record
                record.txtPreviousPass3 = record.txtPreviousPass2;
                record.txtPreviousPass2 = record.txtPreviousPass1;
                record.txtPreviousPass1 = VW.txtNewPassword;
                record.intWrongPassCount = 0;
                record.dtmLastLogin = DateTime.Now;
                mUserCustomBL.UpdateRecord(record);

                ViewBag.Message = "Password is sucessfully reset!";

                return View(VW);
            }
        }

        #endregion
    }
}
