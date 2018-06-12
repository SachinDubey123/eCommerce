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
using UserInterface.eCommerce.FilterException;

namespace UserInterface.eCommerce.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        BLProduct bal=new BLProduct();
        [HttpGet]
        public ActionResult Products()
        {
            try
            {
                ViewBag.Categories = bal.Category();
                return View();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        [HttpPost]
        public ActionResult Products(DLProducts prod, string save,List<HttpPostedFileBase> postedFiles)
        {
            try
            {
                if (prod.ProductId == 0)
                {
                    if (ModelState.IsValid)
                    {
                        return View(prod);
                    }
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View(prod);
                    }
                }
                int action;

                if (!String.IsNullOrEmpty(save))
                {
                    action = bal.Register(prod, "Insert");
                }
                else
                {
                    action = bal.Register(prod, "Update");
                }
                if (action > 0)
                {
                    int ProductId = 0;
                    if (!String.IsNullOrEmpty(save))
                    {
                        List<DLProducts> list = bal.LoadData();
                        DLProducts p = new DLProducts();
                        foreach (var l in list)
                        {
                            p = l;
                        }
                        ProductId = p.ProductId;
                    }
                    else
                    {
                        ProductId = prod.ProductId;
                    }

                    foreach (HttpPostedFileBase file in postedFiles)
                    {
                        if (file != null)
                        {
                            file.SaveAs(Server.MapPath("~/Content/Images/Products/" + file.FileName));
                            bool registerImage = bal.RegisterImage(ProductId, file.FileName);
                        }
                    }
                   TempData["Error"] = "Data Added Succesfully";
                }
                else
                {
                    TempData["Error"] = "Something Happens Wrong !!!!";
                }
                return Redirect("/Product/Products");

            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        public JsonResult ProductLoadData()
        {
            try
            {
                List<DLProducts> list = bal.LoadData();
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

                List<DLProducts> lst = bal.LoadData();
                DLProducts admin = new DLProducts();
                foreach (var l in lst)
                {
                    if (l.ProductId == ID)
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

        public JsonResult ProductDelete(int ID)
        {
            try
            {
                return Json(bal.ProductDelete(ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        public ActionResult Images(int id)
        {
            try
            {
                List<Images> imageList = bal.ProductImages(id);
                return View(imageList);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        [HttpPost]
        public ActionResult EditImage(HttpPostedFileBase file, int imageid)
        {
            try
            {
                file.SaveAs(Server.MapPath("~/Content/Images/Products/" + file.FileName));
                bool result = bal.EditImage(imageid, file.FileName);
                return View();
            }
            catch (Exception ex)
            {
                throw ex;

            }
           
        }
        [HttpPost]
        public ActionResult DeleteImage(int id)
        {
            try
            {
                bool result = bal.DeleteImage(id);
                return View();
            }
            catch (Exception ex)
            {
                throw ex;

            }
           
        }
    }
}