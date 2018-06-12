using System;
using BusinessLayer;
using DataLayer;
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
    public class CustomerController : Controller
    {
        BLCustomer bal = new BLCustomer();
        // GET: Customer

        public ActionResult Home()
        {
            try
            {
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
        //[Authorize]
        [HttpPost]
        public ActionResult Signup(DLCustomer cust, string save)
        {
            try
            {
                if (cust.CustomerId == 0)
                {
                    if (ModelState.IsValid)
                    {
                        return View(cust);
                    }
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View(cust);
                    }
                }
                cust.Password = SHA1.Encode(cust.Password.ToString());

                if (!String.IsNullOrEmpty(save))
                {
                    cust.GoogleId = null;
                    int i = bal.Signup(cust, "Insert");
                }
                else
                {
                    cust.Password = null;
                    int i = bal.Signup(cust, "Update");
                }

                return Redirect("/Customer/Home");
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }
        [HttpPost]
        public ActionResult SignIn(string Email,string Password)
        {
            try
            {
                bal = new BLCustomer();
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    string email = Request["Email"];
                    string password = SHA1.Encode(Password);
                    string[] i = bal.SignIn(email, password);

                    if (i != null)
                    {
                        Session["LoginCustomer"] = i[0];
                        Session["CustomerId"] = i[1];

                        if (Session["CustomerId"] != null)
                        {
                            int CustomerId = Convert.ToInt32(Session["CustomerId"]);

                            int CartId = bal.CartCustomerExist(CustomerId);
                            List<DLProducts> lst = bal.CartItem(CartId);

                            int count = 0;
                            foreach (var val in lst)
                            {
                                count++;
                            }
                            Session["CartCount"] = count;
                        }
                        return Redirect("/Customer/Home");
                    }
                    else
                    {
                        TempData["Error"] = "Invalid email or password";
                    }
                }
                else
                {
                    TempData["Error"] = "Required Email or password";
                }
                return Redirect("/Customer/Home");

            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }
        [HttpPost]
        public ActionResult Login()
        {
            try
            {
                bal = new BLCustomer();
                DLCustomer dlcustomer = new DLCustomer();
                dlcustomer.Firstname = Request["Firstname"];

                dlcustomer.Lastname = Request["Lastname"];
                dlcustomer.GoogleId = Request["GoogleID"];
                dlcustomer.Email = Request["Email"];
                Session["LoginCustomer"] = dlcustomer.Firstname + " " + dlcustomer.Lastname;

                dlcustomer.Password = null;


                int val = bal.Signup(dlcustomer, "Insert");

                Session["CustomerId"] = bal.GPlusCustomer();
                if (Session["CustomerId"] != null)
                {
                    int CustomerId = Convert.ToInt32(Session["CustomerId"]);

                    int CartId = bal.CartCustomerExist(CustomerId);
                    if (CartId > 0)
                    {
                        List<DLProducts> lst = bal.CartItem(CartId);
                        int count = 0;
                        foreach (var v in lst)
                        {
                            count++;
                        }
                        Session["CartCount"] = count;
                    }

                }

                return Redirect("/Customer/Home");

            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }
        public ActionResult SignOut()
        {
            try
            {
                Session["LoginCustomer"] = null;
                Session["CartCount"] = 0;
                Session["CustomerId"] = null;
                return Redirect("/Customer/Home");
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
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
                    int CartId = bal.CartCustomerExist(CustomerId);

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

    }
}