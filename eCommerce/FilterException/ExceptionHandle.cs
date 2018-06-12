using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserInterface.eCommerce.FilterException
{
    public class ExceptionHandle :IExceptionFilter
    {
        SqlConnection con = new SqlConnection(Globle_Connection.connection);

        public void OnException(ExceptionContext filterContext)
        {
            int result;
            try
            {
                Exception e = filterContext.Exception;
                string source = e.StackTrace.ToString();
                string error = e.Message.ToString();
                filterContext.ExceptionHandled = true;
                SqlCommand cmd = new SqlCommand("StoredProcException", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@source", source);
                cmd.Parameters.AddWithValue("@error", error);
                cmd.Parameters.AddWithValue("@Action", "Insert");
                //cmd.Parameters.AddRange(sp);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
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