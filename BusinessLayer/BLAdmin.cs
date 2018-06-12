using DataLayer;
using DataLayer.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace BusinessLayer.BusinessLayer
{
    public class BLAdmin
    {

        SqlConnection con = new SqlConnection(Globle_Connection.connection);
        public int Register(DLAdmin admin,string action)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", admin.AdminId);
                cmd.Parameters.AddWithValue("@uname", admin.Username.ToString());
                cmd.Parameters.AddWithValue("@fname", admin.Firstname.ToString());
                cmd.Parameters.AddWithValue("@lname", admin.Lastname.ToString());
                cmd.Parameters.AddWithValue("@gender", admin.Gender.ToString());
                cmd.Parameters.AddWithValue("@mobile", admin.Mobile.ToString());
                cmd.Parameters.AddWithValue("@email", admin.Email.ToString());
                cmd.Parameters.Add(new SqlParameter("@password", System.Data.SqlDbType.NVarChar)).Value = SHA1.Encode(admin.Password.ToString());
                cmd.Parameters.AddWithValue("@role", admin.Role.ToString());
                cmd.Parameters.AddWithValue("@photo", admin.Photo.ToString());
                cmd.Parameters.AddWithValue("@isactive", 1);
                cmd.Parameters.AddWithValue("@Action",action);

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
        public List<DLAdmin> LoadData()
        {
            List<DLAdmin> lst = new List<DLAdmin>();
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uname", 0);
                cmd.Parameters.AddWithValue("@fname", 0);
                cmd.Parameters.AddWithValue("@lname", 0);
                cmd.Parameters.AddWithValue("@gender", 0);
                cmd.Parameters.AddWithValue("@mobile", 0);
                cmd.Parameters.AddWithValue("@email", 0);
                cmd.Parameters.AddWithValue("@password", 0);
                cmd.Parameters.AddWithValue("@role", 0);
                cmd.Parameters.AddWithValue("@photo", 0);
                cmd.Parameters.AddWithValue("@isactive", 1);
                cmd.Parameters.AddWithValue("@Action", "Select");

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new DLAdmin()
                    {
                        AdminId = Convert.ToInt32(rdr[0]),
                        Username = rdr[1].ToString(),
                        Firstname = rdr[2].ToString(),
                        Lastname = rdr[3].ToString(),
                        Gender = rdr[4].ToString(),
                        Mobile = rdr[5].ToString(),
                        Email = rdr[6].ToString(),
                        Password = rdr[7].ToString(),
                        Role = rdr[8].ToString(),
                        Photo = rdr[9].ToString()
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
        public bool IsValid(string email, string password,string Valid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.AddWithValue("@uname", 0);
                cmd.Parameters.AddWithValue("@fname", 0);
                cmd.Parameters.AddWithValue("@lname", 0);
                cmd.Parameters.AddWithValue("@gender", 0);
                cmd.Parameters.AddWithValue("@mobile", 0);
                cmd.Parameters.AddWithValue("@role", 0);
                cmd.Parameters.AddWithValue("@photo", 0);
                cmd.Parameters.AddWithValue("@isactive", 1);
                if (Valid == "CheckValid")
                {
                    cmd.Parameters.AddWithValue("@password", SHA1.Encode(password));
                    cmd.Parameters.AddWithValue("@Action", "SelectEmailPass");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Action", "SelectEmail");
                }
                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader rdr = cmd.ExecuteReader();
                
                if (rdr.HasRows)
                {
                    rdr.Dispose();
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    rdr.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                return false;
            }
        }
        public void SendActivationEmail(string Email, Guid code,string action)
        {
            if(ActivationCode(code, action))
            {
                using (MailMessage mm = new MailMessage("15ceuod013.ce2018@gmail.com", Email))
                {
                    mm.Subject = "Account Activation";
                    string body = "Hello,";
                    body += "<br /><br />Please use activation code to activate your account: " + code;
                    body += "<br /><br />Thanks";
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("15ceuod013.ce2018@gmail.com","09016736400" );
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }
        }
        public bool ActivationCode(Guid code,string action)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcActivation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@Action", action);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                var i = cmd.ExecuteNonQuery();

                if (i > 0)
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
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }

        }
        public bool ChangePassword(string Email,string Password)
        {
            //try
            //{
                SqlCommand cmd = new SqlCommand("StoredProcUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
            cmd.Parameters.AddWithValue("@id", 0);
            cmd.Parameters.AddWithValue("@uname", 0);
                cmd.Parameters.AddWithValue("@fname", 0);
                cmd.Parameters.AddWithValue("@lname", 0);
                cmd.Parameters.AddWithValue("@gender", 0);
                cmd.Parameters.AddWithValue("@mobile", 0);
                cmd.Parameters.AddWithValue("@role", 0);
                cmd.Parameters.AddWithValue("@photo", 0);
                cmd.Parameters.AddWithValue("@isactive", 1);
                cmd.Parameters.AddWithValue("@password", SHA1.Encode(Password));
                cmd.Parameters.AddWithValue("@Action", "ChangePassword");
                cmd.Parameters.AddWithValue("@email", Email);
                int rdr = cmd.ExecuteNonQuery();

                if (rdr>0)
                {
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    cmd.Dispose();
                    return false;
                }
            //}
            //catch (Exception ex)
            //{
            //    Console.Write(ex);
            //    return false;
            //}
           
        }
        public int UserDelete(int id)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("StoredProcUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@uname", 0);
                cmd.Parameters.AddWithValue("@fname", 0);
                cmd.Parameters.AddWithValue("@lname", 0);
                cmd.Parameters.AddWithValue("@gender", 0);
                cmd.Parameters.AddWithValue("@mobile", 0);
                cmd.Parameters.AddWithValue("@email", 0);
                cmd.Parameters.AddWithValue("@password", 0);
                cmd.Parameters.AddWithValue("@role", 0);
                cmd.Parameters.AddWithValue("@photo", 0);
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