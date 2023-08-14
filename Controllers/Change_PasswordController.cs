using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using scope_project_2.Models;

namespace scope_project_2.Controllers
{
    public class Change_PasswordController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Registration;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        [HttpGet]
        public IActionResult ch_pass()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ch_pass(ch_pass pass)
        {


            return View();
        }

        public IActionResult recheck_pass()
        {
            return View();
        }
        [HttpPost]
        public IActionResult recheck_pass(recheck_pass pass)
        {
            string old_pass = pass.Old_pass;
            string confirm_pass = pass.confirm_pass;

            //Using Cokkies
            string email = HttpContext.Request.Cookies["email"];
            //Using Session
            if (HttpContext.Session.GetString("user_email") != null)
            {
               email= HttpContext.Session.GetString("user_email");
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT email, c_pass FROM register WHERE email = @Email", con);
                cmd.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string ema = reader["email"].ToString();
                        string passs = reader["c_pass"].ToString();
                        if (email == ema)
                        {
                            if (old_pass == passs)
                            {
                                // Close the SqlDataReader before executing the update query
                                reader.Close();

                                // Update the password
                                cmd.CommandText = "UPDATE register SET c_pass = @NewPass WHERE email = @Email";
                                cmd.Parameters.AddWithValue("@NewPass", confirm_pass);
                                cmd.ExecuteNonQuery();

                                //delete the cookies and redirect to the login page
                                HttpContext.Response.Cookies.Delete("email");
                                HttpContext.Response.Cookies.Delete("pass");
                                return RedirectToRoute(new { Action = "_login", Controller = "Login" });
                               

                            }
                            else
                            {
                                ModelState.AddModelError("Old_pass", "Invalid old password.");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email not found.");
                    }
                }
            }

            // If the old password doesn't match or the email is not found, return the view with the errors.
            return View();
        }


  
    }
}
