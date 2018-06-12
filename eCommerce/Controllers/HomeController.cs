using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using UserInterface.eCommerce.FilterException;

namespace UserInterface.eCommerce.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
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
    }
}