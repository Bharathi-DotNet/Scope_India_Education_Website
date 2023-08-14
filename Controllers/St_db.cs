using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Bcpg.OpenPgp;
using scope_project_2.Models;
using System.Collections;
using System.Data.SqlTypes;

namespace scope_project_2.Controllers
{
    public class St_dbController : Controller
    {

        [HttpGet]
        public IActionResult St_db1()
        {

            string forgot_email = HttpContext.Request.Cookies["email"];

            if (HttpContext.Session.GetString("user_email") != null)
            {
                forgot_email = HttpContext.Session.GetString("user_email");

            }


            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Registration;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            // Checking for email and password
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT email, c_pass FROM register WHERE email = @Email", con))
                {
                    cmd.Parameters.AddWithValue("@Email", forgot_email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string db_email = (string)reader["email"];
                            string db_pass = reader["c_pass"].ToString();

                            if (forgot_email == db_email)
                            {
                                // Successful login
                                SqlConnection conn = new SqlConnection();
                                conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Registration;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                                conn.Open();

                                SqlCommand cmdd = new SqlCommand();
                                cmdd.CommandText = "SELECT first_name, last_name, gender, dob, email, phone, country, state, city, hobbies, images FROM register WHERE email = @Email";
                                cmdd.Parameters.AddWithValue("@Email", db_email);
                                cmdd.Connection = conn;
                                SqlDataReader dr = cmdd.ExecuteReader();

                                List<string[]> arr = new List<string[]>();
                                while (dr.Read())
                                {
                                    string[] ar = new string[11];
                                    ar[0] = dr["first_name"].ToString();
                                    ar[1] = dr["last_name"].ToString();
                                    ar[2] = dr["gender"].ToString();
                                    ar[3] = dr["dob"].ToString();
                                    ar[4] = dr["email"].ToString();
                                    ar[5] = dr["phone"].ToString();
                                    ar[6] = dr["country"].ToString();
                                    ar[7] = dr["state"].ToString();
                                    ar[8] = dr["city"].ToString();
                                    ar[9] = dr["hobbies"].ToString();
                                    ar[10] = dr["images"].ToString();

                                    arr.Add(ar);
                                }

                                ViewBag.arr = arr;
                                return View();


                            }
                        }
                    }
                }
            }

            return View();
        }



    }
}


