using DataLayer;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessLayer.BusinessLayer
{
    public class BLProduct
    {
        
        SqlConnection con = new SqlConnection(Globle_Connection.connection);
        public int Register(DLProducts prod, string action)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", Convert.ToInt32(prod.ProductId));
                cmd.Parameters.AddWithValue("@Name", prod.Name.ToString());
                
                cmd.Parameters.AddWithValue("@description", prod.Description.ToString());

                cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(prod.Price));

                cmd.Parameters.AddWithValue("@sku", prod.SKU.ToString());
                cmd.Parameters.AddWithValue("@upc", prod.UPC.ToString());
                cmd.Parameters.AddWithValue("@totalqty", Convert.ToInt32(prod.TotalQty));
                cmd.Parameters.AddWithValue("@brandname", prod.BrandName.ToString());

                cmd.Parameters.AddWithValue("@isoutofstock", Convert.ToString(prod.IsOutOfStock));
                cmd.Parameters.AddWithValue("@isfeaturedprod", Convert.ToString(prod.IsFeaturedProduct));
                cmd.Parameters.AddWithValue("@islatestprod", Convert.ToString(prod.IsLatestProduct));
                cmd.Parameters.AddWithValue("@ispopular", Convert.ToString(prod.IsPopular));
                cmd.Parameters.AddWithValue("@remarks", prod.Remarks.ToString());

                cmd.Parameters.AddWithValue("@categoryid", Convert.ToInt32(prod.CategoryId));
                cmd.Parameters.AddWithValue("@fashionfor", prod.FashionFor.ToString());
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
        public List<DLProducts> LoadData()
        {
            List<DLProducts> prod = new List<DLProducts>();
            DataTable ds = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@isactive", 1);
                cmd.Parameters.AddWithValue("@Action", "Select");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                foreach (DataRow rdr in ds.Rows)
                {
                    prod.Add(new DLProducts
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
                        FashionFor = rdr[14].ToString(),
                        CategoryName = rdr[17].ToString(),
                        Image = ProductImages(Convert.ToInt32(rdr[0]))
                    });
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
            return prod;
        }
        public List<Images> ProductImages(int ProductId)
        {
            DataTable ds = new DataTable();
            List<Images> imgs = new List<Images>();
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcImages", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productId", ProductId);
                cmd.Parameters.AddWithValue("@Action", "SelectProductImages");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();

                foreach (DataRow rdr in ds.Rows)
                {
                    imgs.Add(new Images
                    {
                        ImageId = Convert.ToInt32(rdr[0]),
                        ProductId = Convert.ToInt32(rdr[1]),
                        Filename = rdr[2].ToString()
                    });
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
            return imgs;
        }
        public int ProductDelete(int id)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);
                cmd.Parameters.AddWithValue("@isactive", 0);
                cmd.Parameters.AddWithValue("@Action", "Delete");

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
        public List<SelectListItem> Category()
        {
            List<SelectListItem> category = new List<SelectListItem>();
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@name", 0);

              

                cmd.Parameters.AddWithValue("@Action", "Select");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    category.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                    cmd.Dispose();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return category;
         }

        public bool RegisterImage(int ProductId, string Filename)
        {
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcImages", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productId", ProductId);
                cmd.Parameters.AddWithValue("@filename", Filename);
                
                cmd.Parameters.AddWithValue("@Action", "Insert");
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

       public bool EditImage(int ImageId, string Filename)
        {
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcImages", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@imageId", ImageId);
                cmd.Parameters.AddWithValue("@filename", Filename);
                cmd.Parameters.AddWithValue("@Action", "Update");
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
        public bool DeleteImage(int ImageId)
        {
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcImages", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@imageId", ImageId);
                cmd.Parameters.AddWithValue("@Action", "Delete");
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
    }
}

