using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.BusinessLayer;
using System.Data.Entity.Infrastructure;
using DataLayer.DataLayer;
using UserInterface.eCommerce.FilterException;

namespace UserInterface.eCommerce.Controllers
{
    public class OrderController : Controller
    {
        BLOrder balOrder = new BLOrder();
        // GET: Order
        public ActionResult Orders()
        {
            if (Session["CustomerId"] != null)
            {
                int CustomerId = Convert.ToInt32(Session["CustomerId"]);
                ViewBag.OrderList = balOrder.DisplayOrder(CustomerId, "DisplayOrder");

                ViewBag.Countries = BLOrder.PopulateDropDown("StoredProcCountries",0);

                ViewBag.States = balOrder.CSCList("StoredProcStates");
                ViewBag.Cities = balOrder.CSCList("StoredProcCities");
                ViewBag.TotalOrderList = balOrder.DisplayOrder(CustomerId, "OrderList");

                List<DLAddresses> list = balOrder.AddressExist(CustomerId);
                if (list.Count != 0)
                {
                    return View(list[0]);
                }
            }
            else
            {
                TempData["Error"] = "Please SignIn first";
                return Redirect("/Customer/Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Address(DLAddresses ad, string Save)
        {
            try
            {
                string Action;
                if (!String.IsNullOrEmpty(Save))
                {
                    Action = "Insert";
                }
                else
                {
                    Action = "Update";
                }
                ad.CustomerId = Convert.ToInt32(Session["CustomerId"]);
                balOrder.Address(ad, Action);

                return Redirect("/Order/Orders");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        BLCustomer balCustomer = new BLCustomer();
        public ActionResult CheckOut()
        {
            int CustomerId = Convert.ToInt32(Session["CustomerId"]);
            int CartId = balCustomer.CartCustomerExist(CustomerId);
            if (balOrder.CartOrder(CartId, CustomerId))
            {
                Session["CartCount"] = 0;
                return Redirect("/Order/OrderList");
            }
            else
            {
                TempData["Error"] = "No Product in your Cart";
                return Redirect("/Clothes/Clothes");
            }
        }

        public ActionResult OrderList()
        {
            if (Session["CustomerId"] != null)
            {
                int CustomerId = Convert.ToInt32(Session["CustomerId"]);
                ViewBag.TotalOrderList = balOrder.DisplayOrder(CustomerId, "OrderList");
                return View();
            }
            else
            {
                TempData["Error"] = "No Product in your Cart";
                return Redirect("/Clothes/Clothes");
            }
        }

        public JsonResult AjaxMethod(string type, int value)
        {
            DLAddresses model = new DLAddresses();
            switch (type)
            {
                case "CountryId":
                    model.States = BLOrder.PopulateDropDown("StoredProcStates", value);
                    break;
                case "StateId":
                    model.Cities = BLOrder.PopulateDropDown("StoredProcCities",value);
                    break;
            }
            return Json(model);
            
        }
        public JsonResult GetbyID()
        {
            int CustomerId = Convert.ToInt32(Session["CustomerId"]);
            List<DLAddresses> list = balOrder.AddressExist(CustomerId);
            return Json(list[0], JsonRequestBehavior.AllowGet);
        }
        public JsonResult fillcity(int id)
        {
            DLAddresses model = new DLAddresses();
            model.Cities = BLOrder.PopulateDropDown("StoredProcCities", id);
            return Json(model.Cities);
        }
        public JsonResult fillstate(int id)
        {
            DLAddresses model = new DLAddresses();
            model.States = BLOrder.PopulateDropDown("StoredProcStates",id);
            return Json(model.States);
        }
    }
}