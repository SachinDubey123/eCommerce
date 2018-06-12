using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessLayer.BusinessLayer
{
    public class BLOrder
    {
        BLProduct bal = new BLProduct();
        SqlConnection con = new SqlConnection(Globle_Connection.connection);
        public bool CartOrder(int CartId, int CustomerId)
        {
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcOrderCart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cartId", CartId);
                cmd.Parameters.AddWithValue("@customerId", CustomerId);
                cmd.Parameters.AddWithValue("@Action", "CopyCart");
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

        public List<DLProducts> DisplayOrder(int CustomerId, string action)
        {

            List<DLProducts> list = new List<DLProducts>();
            DataTable ds = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcOrderCart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", CustomerId);
                cmd.Parameters.AddWithValue("@Action", action);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
                if (action == "DisplayOrder")
                {
                    foreach (DataRow rdr in ds.Rows)
                    {
                        list.Add(new DLProducts
                        {
                            ProductId = Convert.ToInt32(rdr[0]),
                            Name = rdr[1].ToString(),
                            Description = rdr[2].ToString(),
                            Price = Convert.ToDecimal(rdr[3]),
                            SKU = rdr[4].ToString(),
                            UPC = rdr[5].ToString(),
                            TotalQty = Convert.ToInt32(rdr[19]),
                            BrandName = rdr[7].ToString(),
                            IsOutOfStock = rdr[8].ToString(),
                            IsFeaturedProduct = rdr[9].ToString(),
                            IsLatestProduct = rdr[10].ToString(),
                            IsPopular = rdr[11].ToString(),
                            Remarks = rdr[12].ToString(),
                            FashionFor = rdr[14].ToString(),
                            Image = bal.ProductImages(Convert.ToInt32(rdr[0]))
                        });
                    }
                }
                else
                {
                    foreach (DataRow rdr in ds.Rows)
                    {
                        list.Add(new DLProducts
                        {
                            ProductId = Convert.ToInt32(rdr[0]),
                            Name = rdr[1].ToString(),
                            Description = rdr[2].ToString(),
                            Price = Convert.ToDecimal(rdr[3]),
                            SKU = rdr[4].ToString(),
                            UPC = rdr[5].ToString(),
                            TotalQty = Convert.ToInt32(rdr[19]),
                            BrandName = rdr[7].ToString(),
                            IsOutOfStock = rdr[8].ToString(),
                            IsFeaturedProduct = rdr[9].ToString(),
                            IsLatestProduct = rdr[10].ToString(),
                            IsPopular = rdr[11].ToString(),
                            Remarks = rdr[12].ToString(),
                            FashionFor = rdr[14].ToString(),
                            CreatedDate = Convert.ToDateTime(rdr[24]),
                            OrderCartItemId = Convert.ToInt32(rdr[16]),
                            Image = bal.ProductImages(Convert.ToInt32(rdr[0]))
                        });
                    }
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
            return list;


        }

        public List<SelectListItem> CSCList(string spname)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            try
            {
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Select");

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()

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
        public List<DLAddresses> AddressExist(int CustomerId)
        {
            List<DLAddresses> list = new List<DLAddresses>();
            DataTable ds = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcAddress", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", CustomerId);
                cmd.Parameters.AddWithValue("@Action", "AddressExist");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                foreach (DataRow rdr in ds.Rows)
                {
                    list.Add(new DLAddresses
                    {
                        AddressId = Convert.ToInt32(rdr[0]),
                        CustomerId = Convert.ToInt32(rdr[1]),
                        FullName = rdr[2].ToString(),
                        Mobile = rdr[3].ToString(),
                        PinCode = Convert.ToInt32(rdr[4]),
                        Address = rdr[5].ToString(),
                        CityId = Convert.ToInt32(rdr[6]),
                        StateId = Convert.ToInt32(rdr[7]),
                        CountryId = Convert.ToInt32(rdr[8]),
                        AddressType = rdr[9].ToString(),
                    });
                }
                cmd.Dispose();
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
            return list;
        }
        public bool Address(DLAddresses ad, string action)
        {
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcAddress", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", ad.CustomerId);
                cmd.Parameters.AddWithValue("@fullname", ad.FullName);
                cmd.Parameters.AddWithValue("@mobile", ad.Mobile);
                cmd.Parameters.AddWithValue("@pincode", ad.PinCode);
                cmd.Parameters.AddWithValue("@address", ad.Address);
                cmd.Parameters.AddWithValue("@cityId", ad.CityId);
                cmd.Parameters.AddWithValue("@stateId", ad.StateId);
                cmd.Parameters.AddWithValue("@countryId", ad.CountryId);
                cmd.Parameters.AddWithValue("@type", ad.AddressType);
                cmd.Parameters.AddWithValue("@Action", action);
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

        public static List<SelectListItem> PopulateDropDown(string spname, int value)
        {
            SqlConnection con = new SqlConnection(Globle_Connection.connection);

            try
            {
                List<SelectListItem> prod = new List<SelectListItem>();

                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (spname == "StoredProcStates")
                {
                    cmd.Parameters.AddWithValue("@contryId", value);
                }
                if (spname == "StoredProcCities")
                {
                    cmd.Parameters.AddWithValue("@stateId", value);
                }
                cmd.Parameters.AddWithValue("@Action", "Select");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader list = cmd.ExecuteReader();
               
                while (list.Read())
                {
                    prod.Add(new SelectListItem
                    {
                        Text = list[1].ToString(),
                        Value = list[0].ToString()
                    });
                }
                return prod;
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

