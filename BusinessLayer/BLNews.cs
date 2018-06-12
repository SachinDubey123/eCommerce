using DataLayer;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BLNews
    {
        SqlConnection con = new SqlConnection(Globle_Connection.connection);
        public int Register(DLNews news, string action)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcNews", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NewsId", Convert.ToInt32(news.NewsId));
                cmd.Parameters.AddWithValue("@Title", news.Title.ToString());

                cmd.Parameters.AddWithValue("@Description", news.Description.ToString());
                cmd.Parameters.AddWithValue("@image", news.Image.ToString());
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

        public List<DLNews> LoadData()
        {
            List<DLNews> lst = new List<DLNews>();
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcNews", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@newsid", 0);
                cmd.Parameters.AddWithValue("@title", 0);
                cmd.Parameters.AddWithValue("@description", 0);

                cmd.Parameters.AddWithValue("@image", 0);
                cmd.Parameters.AddWithValue("@isactive", 1);
                cmd.Parameters.AddWithValue("@Action", "Select");

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new DLNews()
                    {
                        NewsId = Convert.ToInt32(rdr[0]),
                        Title = rdr[1].ToString(),
                        Description = rdr[2].ToString(),

                        Image = rdr[9].ToString()
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

        public bool RegisterImage(int NewsId, string Filename)
        {
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcImages", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@newsid", NewsId);
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

        public int NewsDelete(int id)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcNews", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@newsid", id);
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

    }
}
