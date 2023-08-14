using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using scope_project_2.Models;
using Microsoft.Build.Globbing;
using Microsoft.AspNetCore.Http.Metadata;
using System.Drawing;

namespace scope_project_2.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]


        public IActionResult Registration(registriation reg, List<IFormFile> imag)
        {
            var email = reg.Email;
            string last_file_name = string.Empty;

            if (ModelState.IsValid)
            {
                foreach (var file in imag)
                {
                    if (file != null && file.Length > 0)
                    {
                        string File_Name = file.FileName;
                        string Upload_Path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images");
                        if (!Directory.Exists(Upload_Path))
                        {
                            Directory.CreateDirectory(Upload_Path);
                        }

                        string Upload_Folder = Path.Combine(Upload_Path, File_Name);
                        last_file_name = File_Name;
                        using (FileStream UploadStream = new FileStream(Upload_Folder, FileMode.OpenOrCreate))
                        {
                            file.CopyTo(UploadStream);
                        }
                    }
                }

                //Generate a random password
                Random ran = new Random();
                var rand = ran.Next(1000, 10000);

                // Send email with the password
                string senderEmail = "bharathibharathi5323@gmail.com";
                string senderPassword = "nlcowugzzquhivhh";
                string receiverEmail = email;
                string message = "Your password: " + rand;
                using (MailMessage mailMessage = new MailMessage(senderEmail, receiverEmail, "Contact", message))
                {
                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtpClient.Send(mailMessage);
                    }
                }

                // Store data in the database
                var _f = reg.F_N;
                var _l = reg.L_N;
                var gen = reg.gender;
                var dob = reg.DOB;
                var phone = reg.Phone;
                var cou = reg.Country;
                var state = reg.State;
                var city = reg.City;


                string read = reg.Read;
                string play = reg.Play;
                string cook = reg.Cook;

                read = reg.Read == "true" ? "Reading" : null;
                play = reg.Play == "true" ? "Playing" : null;
                cook = reg.Cook == "true" ? "Cooking" : null;

                string[] arr = new string[] { read, play, cook };
                var fl_st = string.Join(" ", arr);

                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Registration;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    SqlCommand checkEmailCmd = new SqlCommand("SELECT COUNT(*) FROM register WHERE email = @email", con);
                    checkEmailCmd.Parameters.AddWithValue("@email", email);
                    int emailCount = (int)checkEmailCmd.ExecuteScalar();

                    if (emailCount >= 1)
                    {
                        ViewBag.ErrorMessage = "Email already exists. Please use a different email address.";
                        return View("error1");
                    }

                    cmd.CommandText = "INSERT INTO register (first_name, last_name, gender, dob, email, phone, country, state, city, hobbies, c_pass, images) VALUES (@_f, @_L, @gen, @dob, @email, @phone, @cou, @state, @city, @hobbies, @c_pass, @images)";
                    cmd.Parameters.AddWithValue("@_f", _f);
                    cmd.Parameters.AddWithValue("@_L", _l);
                    cmd.Parameters.AddWithValue("@gen", gen);
                    cmd.Parameters.AddWithValue("@dob", dob);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@cou", cou);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@hobbies", fl_st);
                    cmd.Parameters.AddWithValue("@c_pass", rand);
                    cmd.Parameters.AddWithValue("@images", last_file_name);
                    cmd.ExecuteNonQuery();
                }

                return View("sucess");
            }
            else
            {



                ModelState.AddModelError("", "Please fill the all field");
                return View();

            }


        }


    }
}
