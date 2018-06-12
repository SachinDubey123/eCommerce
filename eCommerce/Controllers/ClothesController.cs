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
    public class ClothesController : Controller
    {
        //    // GET: Clothes
        //    BLCategory balCategory = new BLCategory();
        BLProduct balProduct = new BLProduct();
        BLCustomer balCustomer = new BLCustomer();
        BLCategory balCategory = new BLCategory();
        BLOrder balOrder = new BLOrder();
        public ActionResult Clothes()
        {
            try
            {

                ViewBag.CategoryList = balCategory.LoadData();

                if (Session["CustomerId"] != null)
                {
                    Session["CartCount"] = CartCount();
                }
                return View();

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        [HttpPost]
         public ActionResult Clothes(DLProducts prod)
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

        public ActionResult QuickView(int Id)
        {
            try
            {

                List<DLProducts> list = balProduct.LoadData();
                DLProducts p = new DLProducts();
                foreach (var prod in list)
                {
                    if (prod.ProductId == Id)
                    {
                        p = prod;
                        break;
                    }

                }
                return View(p);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ActionResult AddCart(int id)
        {
            try
            {
                if (Session["CustomerId"] != null)
                {
                    int CustomerId = Convert.ToInt32(Session["CustomerId"]);
                    int CartId = balCustomer.CartCustomerExist(CustomerId);
                    if (CartId == 0)
                    {
                        bool addCart = balCustomer.AddCart(CustomerId);
                        CartId = balCustomer.CartCustomerExist(CustomerId);
                    }
                    bool ProductIdExist = balCustomer.CartProductExist(CartId, id);

                    if (ProductIdExist)
                    {
                       if(!balCustomer.AddQuantity(CartId, id))
                        {
                            TempData["Error"] = "Product out of stock...";
                        }
                    }
                    else
                    {
                      if(!balCustomer.AddCartItem(CartId, id))
                        {
                            TempData["Error"] = "Product out of stock...";
                        }
                    }
                }
                else
                {
                    TempData["Error"] = "Please SignIn first...";
                }
                //Session["CartCount"] = CartCount();
                return Redirect("/Clothes/Clothes");

            }
            catch (Exception ex)
            {
                throw ex;

            }
            }

        public PartialViewResult GetCartProducts()
        {
            try
            {
                int CustomerId = Convert.ToInt32(Session["CustomerId"]);
                int CartId = balCustomer.CartCustomerExist(CustomerId);
                //if (CartId > 0)
                //{
                //    decimal pric = balCustomer.CartPrice(CartId);
                //    ViewBag.Total = pric;
                //}
                //else
                //    ViewBag.Total = 0;
                List<DLProducts> list = balCustomer.CartItem(CartId);
                Session["CartCount"] = CartCount();

                return PartialView("_cartProduct", list);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        public PartialViewResult DeleteCart(int id)
        {
            try
            {
                int CustomerId = Convert.ToInt32(Session["CustomerId"]);
                int CartId = balCustomer.CartCustomerExist(CustomerId);
                var val = balCustomer.DeleteCart(CartId, id);
                Session["CartCount"] = CartCount();
                return GetCartProducts();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        public ActionResult GetProduct(string SelectCategory, string SelectFashionFor)
        {
            try
            {
                List<DLProducts> list = new List<DLProducts>();
                if (!String.IsNullOrEmpty(SelectCategory) && !String.IsNullOrEmpty(SelectFashionFor))
                {
                    SelectCategory = SelectCategory.Trim();
                    SelectFashionFor = SelectFashionFor.Trim();
                    list = balCustomer.FilterProduct(SelectCategory, SelectFashionFor, "FashionForCategory");
                }
                else if (!String.IsNullOrEmpty(SelectCategory))
                {
                    SelectCategory = SelectCategory.Trim();
                    SelectFashionFor = SelectFashionFor.Trim();
                    list = balCustomer.FilterProduct(SelectCategory, SelectFashionFor, "Category");
                }
                else if (!String.IsNullOrEmpty(SelectFashionFor))
                {
                    SelectCategory = SelectCategory.Trim();
                    SelectFashionFor = SelectFashionFor.Trim();
                    list = balCustomer.FilterProduct(SelectCategory, SelectFashionFor, "FashionFor");
                }
                else
                {
                    list = balProduct.LoadData();
                }
                ViewBag.ProductList = list;
                return PartialView("_PartialProduct");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CartCount()
        {
            try
            {
                BLCustomer bal = new BLCustomer();
                int count = 0;
                if (Session["CustomerId"] != null)
                {
                    int CustomerId = Convert.ToInt32(Session["CustomerId"]);
                    int CartId = balCustomer.CartCustomerExist(CustomerId);

                    List<DLProducts> lst = bal.CartItem(CartId);
                    foreach (var v in lst)
                    {
                        count++;
                    }
                    Session["CartCount"] = count;


                }
                return count;

            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        [HttpPost]
        public PartialViewResult EditQuantity(int CartItemId, int Quantity)
        {
            try
            {

                if (!balCustomer.EditQuantity(CartItemId, Quantity))
                {
                    TempData["Error"] = "Product out of stock...";
                }
                return GetCartProducts();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

       
    }
}