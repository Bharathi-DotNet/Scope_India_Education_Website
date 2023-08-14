using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Ocsp;
using scope_project_2.Models;

namespace scope_project_2.Controllers
{
    public class EditController : Controller
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Registration;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(edit edit)
        {
            //Using Cookies
            string _email = HttpContext.Request.Cookies["email"];
            //Using Session
            if (HttpContext.Session.GetString("user_email") != null)
            {
                _email = (HttpContext.Session.GetString("user_email"));
            }
            var _f = edit.F_N;
            var _l = edit.L_N;
            var gen = edit.gender;
            var dob = edit.DOB;
            var email = edit.Email;
            var phone = edit.Phone;
            var cou = edit.Country;
            var state = edit.State;
            var city = edit.city;

            string read = edit.Read;
            string play = edit.Play;
            string cook = edit.Cook;

            read = edit.Read == "true" ? "Reading" : null;
            play = edit.Play == "true" ? "Playing" : null;
            cook = edit.Cook == "true" ? "Cooking" : null;

            string[] arr = new string[] { read, play, cook };
            var fl_st = string.Join(" ", arr);

            if (_f != null || _l != null || gen != null || dob != null || email != null || phone != null || cou != null || state != null || city != null || fl_st != null)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Update first_name
                    if (_f != null)
                    {
                        SqlCommand com = new SqlCommand("UPDATE register SET first_name = @_f WHERE email = @_email", con);
                        com.Parameters.AddWithValue("@_f", _f);
                        com.Parameters.AddWithValue("@_email", _email);
                        com.ExecuteNonQuery();
                    }

                    // Update last_name
                    if (_l != null)
                    {
                        SqlCommand com = new SqlCommand("UPDATE register SET last_name = @_l WHERE email = @_email", con);
                        com.Parameters.AddWithValue("@_l", _l);
                        com.Parameters.AddWithValue("@_email", _email);
                        com.ExecuteNonQuery();
                    }

                    // Update gender
                    if (gen != null)
                    {
                        SqlCommand com = new SqlCommand("UPDATE register SET gender = @gen WHERE email = @_email", con);
                        com.Parameters.AddWithValue("@gen", gen);
                        com.Parameters.AddWithValue("@_email", _email);
                        com.ExecuteNonQuery();
                    }

                    // Update dob
                    if (dob != null)
                    {
                        SqlCommand com = new SqlCommand("UPDATE register SET dob = @dob WHERE email = @_email", con);
                        com.Parameters.AddWithValue("@dob", dob);
                        com.Parameters.AddWithValue("@_email", _email);
                        com.ExecuteNonQuery();
                    }

                    // Update email
                    if (email != null)
                    {
                        SqlCommand com = new SqlCommand("UPDATE register SET email = @email WHERE email = @_email", con);
                        com.Parameters.AddWithValue("@email", email);
                        com.Parameters.AddWithValue("@_email", _email);
                        com.ExecuteNonQuery();
                    }

                    // Update phone
                    if (phone != null)
                    {
                        SqlCommand com = new SqlCommand("UPDATE register SET phone = @phone WHERE email = @_email", con);
                        com.Parameters.AddWithValue("@phone", phone);
                        com.Parameters.AddWithValue("@_email", _email);
                        com.ExecuteNonQuery();
                    }

                    // Update country
                    if (cou != null)
                    {
                        SqlCommand com = new SqlCommand("UPDATE register SET country = @cou WHERE email = @_email", con);
                        com.Parameters.AddWithValue("@cou", cou);
                        com.Parameters.AddWithValue("@_email", _email);
                        com.ExecuteNonQuery();
                    }

                    // Update state
                    if (state != null)
                    {
                        SqlCommand com = new SqlCommand("UPDATE register SET state = @state WHERE email = @_email", con);
                        com.Parameters.AddWithValue("@state", state);
                        com.Parameters.AddWithValue("@_email", _email);
                        com.ExecuteNonQuery();
                    }

                    // Update city
                    if (city != null)
                    {
                        SqlCommand com = new SqlCommand("UPDATE register SET city = @city WHERE email = @_email", con);
                        com.Parameters.AddWithValue("@city", city);
                        com.Parameters.AddWithValue("@_email", _email);
                        com.ExecuteNonQuery();
                    }

                    // Update hobbies
                    if (fl_st != null)
                    {
                        SqlCommand com = new SqlCommand("UPDATE register SET hobbies = @fl_st WHERE email = @_email", con);
                        com.Parameters.AddWithValue("@fl_st", fl_st);
                        com.Parameters.AddWithValue("@_email", _email);
                        com.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                return BadRequest("Null values");
            }

            return RedirectToAction("Edit_success");
        }

        public IActionResult Edit_Success()
        {
            return View();
        }

    }
}
