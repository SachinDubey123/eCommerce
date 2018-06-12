using System;
using DataLayer;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataLayer;

namespace BusinessLayer.BusinessLayer
{
    public class BLCategory
    {
        SqlConnection con = new SqlConnection(Globle_Connection.connection);
        public int Register(DLCategory categ, string action)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", categ.CategoryId);
                cmd.Parameters.AddWithValue("@name", categ.Name.ToString());
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

        public List<DLCategory> LoadData()
        {
            List<DLCategory> lst = new List<DLCategory>();
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
                    lst.Add(new DLCategory()
                    {
                        CategoryId = Convert.ToInt32(rdr[0]),
                        Name = rdr[1].ToString(),

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


        public int CategoryDelete(int id)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", 0);
               
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


    }
}