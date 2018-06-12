using BusinessLayer;
using BusinessLayer.BusinessLayer;
using DataLayer;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserInterface.eCommerce.FilterException;

namespace UserInterface.eCommerce.Controllers
{
    public class AccountController : Controller
    {
        BLAdmin bal = new BLAdmin();
        [AllowAnonymous]
        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(DLAdmin ad, string ReturnUrl = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(ad.Email) && !string.IsNullOrEmpty(ad.Password))
                {
                    if (bal.IsValid(ad.Email, ad.Password, "CheckValid"))
                    {
                        BLAdmin bal = new BLAdmin();
                        FormsAuthentication.SetAuthCookie(ad.Email, true);
                        var dal = bal.LoadData();
                        foreach (var e in dal)
                        {
                            if (e.Email == ad.Email)
                            {
                                Session["Role"] = e.Role;
                                Session["Photo"] = e.Photo;
                                break;
                            }
                        }
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return Redirect("/Home/Dashboard");

                        }
                    }
                    else
                    {
                        TempData["Error"] = "Invalid Email Id Or Password";
                    }
                }
                else
                {
                    TempData["Error"] = "Required Email Id Or Password";
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            try
            {
                Session["Photo"] = null;
                Session["Role"] = null;
                FormsAuthentication.SignOut();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public void ForgotPassword(DLAdmin admin)
        {
            try
            {
                if (bal.IsValid(admin.Email, "", "SelectEmail"))
                {
                    Guid activationCode = Guid.NewGuid();
                    bal.SendActivationEmail(admin.Email, activationCode, "Insert");
                    Response.Redirect("../Account/ActivatePassword");
                }
                else
                {
                    TempData["Error"] = "Email not exist!!!";
                    Response.Redirect("../Account/Login");
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
       }
        
        [HttpGet]
        public ActionResult ActivatePassword()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ActivatePassword(DLAdmin admin)
        {
            try
            {
                if (Request["code"].ToString() != "")
                {
                    try
                    {
                        Guid activationCode = new Guid(Request["code"].ToString());
                        if (bal.ActivationCode(activationCode, "Delete"))
                        {
                            return Redirect("../Account/RePassword");
                        }
                        else
                        {
                            TempData["Error"] = "Invalid Activation code...!!!";
                            return View();
                        }
                    }
                    catch (Exception e)
                    {
                        TempData["Error"] = "Exception...!!! " + e.Message;
                        return View();
                    }
                }
                else
                {
                    TempData["Error"] = "Please enter valid Activation code...!!!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        [HttpGet]
        public ActionResult RePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RePassword(DLAdmin admin)
        {
            try
            {
                var email = Request["EmailID"].ToString();
                var pass = Request["Password"].ToString();
                var conpass = Request["ConPassword"].ToString();

                if (pass.Equals(conpass))
                {
                    if (bal.ChangePassword(email, pass))
                    {
                        return Redirect("../Account/Login");
                    }
                    else
                    {
                        TempData["Error"] = "Password not changed";
                        return View();
                    }
                }
                else
                {
                    TempData["Error"]= "Password not matched";
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}