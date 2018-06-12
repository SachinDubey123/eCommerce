using BusinessLayer;
using BusinessLayer.BusinessLayer;
using DataLayer;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using UserInterface.eCommerce.FilterException;

namespace UserInterface.eCommerce.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        BLAdmin bal = new BLAdmin();
        public ActionResult Users()
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
        public ActionResult Users(DLAdmin admin, string save)
        {
            try
            {
                var Photo = Request.Files["Photo"];
                string ImageName = Photo.FileName;
                string PhysicalPath = Server.MapPath("~/Content/Images/Photos/" + ImageName);
                Photo.SaveAs(PhysicalPath);
                admin.Photo = ImageName;

                if (!String.IsNullOrEmpty(save))
                {
                    List<DLAdmin> dallist = bal.LoadData();
                    DLAdmin dal = new DLAdmin();
                    foreach(var m in dallist)
                    {
                       if(m.Email== admin.Email)
                        {
                            TempData["Error"] = "Email already exists!!";
                            return Redirect("/User/Users");
                        }
                    }
                    int i = bal.Register(admin, "Insert");
                }
                else
                {
                    int i = bal.Register(admin, "Update");
                }

                return Redirect("/User/Users");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public JsonResult AdminLoadData()
        {
            try
            {
                List<DLAdmin> list = bal.LoadData();
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
                List<DLAdmin> lst = bal.LoadData();
                DLAdmin admin = new DLAdmin();
                foreach (var l in lst)
                {
                    if (l.AdminId == ID)
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
        public JsonResult UserDelete(int ID)
        {
            try
            {
                return Json(bal.UserDelete(ID), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}