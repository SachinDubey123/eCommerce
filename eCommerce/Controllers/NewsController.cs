using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class NewsController : Controller
    {
        BLNews bal = new BLNews();
        // GET: News
        public ActionResult News()
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
        public ActionResult News(DLNews news, string save, List<HttpPostedFileBase> postedFiles)
        {
            try
            {
                if (news.NewsId == 0)
                {
                    if (ModelState.IsValid)
                    {
                        return View(news);
                    }
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View(news);
                    }
                }
                int action;

                if (!String.IsNullOrEmpty(save))
                {
                    action = bal.Register(news, "Insert");
                }
                else
                {
                    action = bal.Register(news, "Update");
                }
                if (action > 0)
                {
                    int NewsId = 0;
                    if (!String.IsNullOrEmpty(save))
                    {
                        List<DLNews> list = bal.LoadData();
                        DLNews p = new DLNews();
                        foreach (var l in list)
                        {
                            p = l;
                        }
                        NewsId = p.NewsId;
                    }
                    else
                    {
                        NewsId = news.NewsId;
                    }

                    foreach (HttpPostedFileBase file in postedFiles)
                    {
                        if (file != null)
                        {
                            file.SaveAs(Server.MapPath("~/Content/Images/News/" + file.FileName));
                            bool registerImage = bal.RegisterImage(NewsId, file.FileName);
                        }
                    }
                    TempData["Error"] = "Data Added Succesfully";
                }
                else
                {
                    TempData["Error"] = "Something Happens Wrong !!!!";
                }
                return Redirect("/News/News");

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public JsonResult NewsLoadData()
        {
            try
            {
                List<DLNews> list = bal.LoadData();
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

                List<DLNews> lst = bal.LoadData();
                DLNews admin = new DLNews();
                foreach (var l in lst)
                {
                    if (l.NewsId == ID)
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

        public JsonResult NewsDelete(int ID)
        {
            try
            {
                return Json(bal.NewsDelete(ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
    }
}