using DataLayer;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using BusinessLayer.BusinessLayer;
using DataLayer.DataLayer;
using UserInterface.eCommerce.FilterException;

namespace UserInterface.eCommerce.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        BLCategory bal = new BLCategory();
        // GET: Category
        public ActionResult Categories()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Categories(DLCategory categ, string save)
        {
            try
            {
                if (categ.CategoryId == 0)
                {
                    if (ModelState.IsValid)
                    {
                        return View(categ);
                    }
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View(categ);
                    }
                }

                if (!String.IsNullOrEmpty(save))
                {
                    int i = bal.Register(categ, "Insert");
                }
                else
                {
                    int i = bal.Register(categ, "Update");
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
            return Redirect("/Category/Categories");
        }

        public JsonResult CategoryLoadData()
        {
            try
            {
                List<DLCategory> list = bal.LoadData();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }
        public JsonResult GetbyID(int ID)
        {
            try
            {

                List<DLCategory> lst = bal.LoadData();
                DLCategory admin = new DLCategory();
                foreach (var l in lst)
                {
                    if (l.CategoryId == ID)
                    {
                        admin = l;
                        break;
                    }
                }
                return Json(admin, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public JsonResult CategoryDelete(int ID)
        {
            try
            {
                return Json(bal.CategoryDelete(ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

    }
}