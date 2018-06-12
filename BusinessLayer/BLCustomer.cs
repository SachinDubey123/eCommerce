using DataLayer;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessLayer
{
    public class BLCustomer
    {
        BLProduct balProduct = new BLProduct();
        SqlConnection con = new SqlConnection(Globle_Connection.connection);
        public int Signup(DLCustomer customer, string action)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", customer.CustomerId);
                cmd.Parameters.AddWithValue("@firstname", customer.Firstname.ToString());
                cmd.Parameters.AddWithValue("@lastname", customer.Lastname.ToString());
                cmd.Parameters.AddWithValue("@email", customer.Email.ToString());
                cmd.Parameters.AddWithValue("@googleId", customer.GoogleId);
                cmd.Parameters.AddWithValue("@password", customer.Password);
                cmd.Parameters.AddWithValue("@isactive", 1);
                cmd.Parameters.AddWithValue("@Action", action);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return i;
        }

        public string[] SignIn(String email, String password)
        {

            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@isactive", 1);
                cmd.Parameters.AddWithValue("@Action", "SelectEmailPass");

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader i = cmd.ExecuteReader();
                if (i.HasRows)
                {
                    string[] LoginCustomer = { null, null };
                    while (i.Read())
                    {
                        LoginCustomer[0] = i[1].ToString() + " " + i[2].ToString();

                        LoginCustomer[1] = i[0].ToString();
                    }
                    return LoginCustomer;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            //return i;
        }
        
        public int CartCustomerExist(int CustomerId)
        {
            int cartId = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@customerid", CustomerId);
                cmd.Parameters.AddWithValue("@Action", "CartCustomerExist");

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader i = cmd.ExecuteReader();
                //if(i.HasRows)
                //{
                    while (i.Read())
                    {
                        cartId = Convert.ToInt32(i[0]);
                    }
                //}
                
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return cartId;
           
        }

        public int GPlusCustomer()
        {
            int CustomerId = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Action", "GPlusCustomer");

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader i = cmd.ExecuteReader();


                while (i.Read())
                {
                    CustomerId = Convert.ToInt32(i[0]);
                    
                }
                i.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return CustomerId;

        }
        
        public bool AddCart(int CustomerId)
        {
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                
              
                cmd.Parameters.AddWithValue("@customerid", CustomerId);
                cmd.Parameters.AddWithValue("@action", "Insert");

                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (result>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return false;
            }
        }

        public decimal CartPrice(int CustomerId)
        {
            decimal price = 0;
           
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@customerid", CustomerId);
                cmd.Parameters.AddWithValue("@productId", 0);
                cmd.Parameters.AddWithValue("@Action", "CartPrice");

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader i = cmd.ExecuteReader();

                
                while (i.Read())
                {
                    price = Convert.ToDecimal(i[0]);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return price;

        }

        public bool DeleteCart(int CartId,int productId)
        {
            int result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCartItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@cartId",CartId );
                cmd.Parameters.AddWithValue("@productId", productId);
         
                cmd.Parameters.AddWithValue("@Action", "Delete");

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public List<DLProducts> FilterProduct(string CategoryId,string FashionFor,string action)
        {
            List<DLProducts> lst = new List<DLProducts>();
            try
            {
                SqlCommand cmd = new SqlCommand("Filter", con);
                cmd.Parameters.AddWithValue("@Category",CategoryId);

                cmd.Parameters.AddWithValue("@FashionFor", FashionFor);
                cmd.Parameters.AddWithValue("@Action", action);

                cmd.CommandType = CommandType.StoredProcedure;
              
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new DLProducts()
                    {
                        ProductId = Convert.ToInt32(rdr[0]),
                        Name = rdr[1].ToString(),
                       
                        Description = rdr[2].ToString(),
                        Price = Convert.ToDecimal(rdr[3]),
                        SKU = rdr[4].ToString(),
                        UPC = rdr[5].ToString(),
                        TotalQty = Convert.ToInt32(rdr[6]),
                        BrandName = rdr[7].ToString(),
                        IsOutOfStock = rdr[8].ToString(),
                        IsFeaturedProduct = rdr[9].ToString(),
                        IsLatestProduct = rdr[10].ToString(),
                        IsPopular = rdr[11].ToString(),
                        Remarks = rdr[12].ToString(),
                        CategoryId = Convert.ToInt32(rdr[13]),
                        FashionFor = rdr[14].ToString(),
                        Image = balProduct.ProductImages(Convert.ToInt32(rdr[0]))

                    });
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return lst;

        }

        public List<DLProducts> CartItem(int CartId)
        {
            List<DLProducts> prod = new List<DLProducts>();
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCartItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cartId", CartId);
                cmd.Parameters.AddWithValue("@action", "SelectCartItemProduct");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader i = cmd.ExecuteReader();
                 while (i.Read())
                {
                    prod.Add(new DLProducts()
                    {
                        ProductId = Convert.ToInt32(i[0]),
                        Name = i[1].ToString(),

                        Description = i[2].ToString(),
                        Price = Convert.ToDecimal(i[3]),
                        SKU = i[4].ToString(),
                        UPC = i[5].ToString(),
                        TotalQty = Convert.ToInt32(i[22]),
                        BrandName = i[7].ToString(),
                        IsOutOfStock = i[8].ToString(),
                        IsFeaturedProduct = i[9].ToString(),
                        IsPopular = i[10].ToString(),
                        IsLatestProduct = i[11].ToString(),
                        Remarks = i[12].ToString(),
                        FashionFor = i[14].ToString(),
                        CartItemId = Convert.ToInt32(i[19]),
                        Image = balProduct.ProductImages(Convert.ToInt32(i[0]))
                    });
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return prod;

        }

        public bool CartProductExist(int CartId, int ProductId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCartItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cartId", CartId);
                cmd.Parameters.AddWithValue("@productId", ProductId);
                cmd.Parameters.AddWithValue("@action", "CartProductExist");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public bool AddQuantity(int CartId,int productId)
        {
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCartItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cartId", CartId);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@action", "AddQuantity");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public bool AddCartItem(int CartId,int productId)
        {
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCartItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cartId", CartId);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@action", "Insert");
                if(con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

       public bool EditQuantity(int CartItemId, int Quantity)
        {
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCartItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", CartItemId);
                cmd.Parameters.AddWithValue("@quantity", Quantity);
                cmd.Parameters.AddWithValue("@Action", "EditQuantity");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if(result>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

    }

}

