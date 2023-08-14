using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using scope_project_2.Models;
using Microsoft.CodeAnalysis.Operations;
using Humanizer;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class LoginController : Controller
{
    private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Registration;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    private readonly string senderEmail = "bharathibharathi5323@gmail.com";
    private readonly string senderPassword = "nlcowugzzquhivhh";

    public IActionResult _login(login log, int a)
    {
        if (HttpContext.Request.Cookies["email"] != null)
        {
            string email = HttpContext.Request.Cookies["email"];
            string pass = HttpContext.Request.Cookies["pass"];

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT first_name, last_name, gender, dob, email, phone, country, state, city, hobbies, images,c_pass FROM register WHERE email = @Email", con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<string[]> arr = new List<string[]>();
                    while (dr.Read())
                    {
                        string[] ar = new string[14];
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
                        ar[11] = dr["c_pass"].ToString();

                        arr.Add(ar);
                        ViewBag.arr = arr;

                        if (email == ar[4])
                        {
                            if (pass == ar[11])
                            {
                                // Successful login
                                return View("Displaying", log);
                            }
                        }
                    }

                    // Close the SqlDataReader after reading data.
                    dr.Close();
                }
            }
        }
        return View();
    }


    [HttpPost]
    public IActionResult _login(login login)
    {
        var email = login.email;
        var pass = login.pass;

        //Using Cookies
        var keepme = login.keepme;
        if (keepme == true)
        {
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddMinutes(3);
            HttpContext.Response.Cookies.Append("email", email, cookie);
            HttpContext.Response.Cookies.Append("pass", pass, cookie);
        }
        //Using Session

        HttpContext.Session.SetString("user_email", email);
        HttpContext.Session.SetString("user_pass", pass);


        //Model Validation
        if (ModelState.IsValid)
        {
            // Checking for email and password
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT email, c_pass FROM register WHERE email = @Email", con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string db_pass = reader["c_pass"].ToString();

                            if (pass == db_pass)
                            {
                                // Successful login
                                SqlConnection conn = new SqlConnection();
                                conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Registration;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                                conn.Open();

                                SqlCommand cmdd = new SqlCommand();
                                cmdd.CommandText = "SELECT first_name, last_name, gender, dob, email, phone, country, state, city, hobbies, images FROM register WHERE email = @Email";
                                cmdd.Parameters.AddWithValue("@Email", email);
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
                                return View("Displaying", login);
                            }
                            else
                            {
                                // Invalid password
                                ModelState.AddModelError("pass", "Invalid email or password");
                                return View("_login", login); // Return the login view with the model data
                            }
                        }
                    }
                }
            }
        }
        else
        {
            ModelState.AddModelError("from", "Something field is not a valid value");
            return View("_login", login); // Return the login view with the model data
        }

        return View("error");
    }

    //Forgot Controller
    [HttpGet]
    public IActionResult forgot()
    {
        return View();
    }
    [HttpPost]
    public IActionResult forgot(Forgot forgot)
    {
        //Sending Email For User to Forgot Password
        Random rnd = new Random();
        int rand = rnd.Next(1000, 10000);

        string sender = "bharathibharathi5323@gmail.com";
        string sender_pass = "srnloypyakpnjqnt";
        string reciver = forgot.ema;
        string sub = "Forgot_PassWord" + " " + rand;

        MailMessage mess = new MailMessage(sender, reciver, "contact to", sub);
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential(sender, sender_pass);
        client.Send(mess);

        //Storing The DataBase 
        string email = forgot.ema;

        SqlConnection con = new SqlConnection();
        con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Registration;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        con.Open();


        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT email, c_pass FROM register WHERE email=@paramEmail";
        cmd.Parameters.AddWithValue("@paramEmail", email);


        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            string ema = (string)reader["email"];
            string pass = (string)reader["c_pass"];
            reader.Close();
            if (ema == email)
            {
                cmd.CommandText = "UPDATE register SET c_pass=@pass WHERE email=@paramEmail";
                cmd.Parameters.AddWithValue("@pass", rand);
                cmd.ExecuteNonQuery();
            }
            else
            {
                return RedirectToAction("error");
            }

        }
        return View();
    }

    //Search For Courses 

    public IActionResult Search_For_Course()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Search_For_Course(Display display)
    {
        var dis = display.Selected;
        TempData["dis"] = dis;
        return View("course_selected", "Login");
    }
    public IActionResult course_selected()
    {
        return View();
    }

    //Displaying
    public IActionResult Displaying()
    {
        return View();
    }
}
