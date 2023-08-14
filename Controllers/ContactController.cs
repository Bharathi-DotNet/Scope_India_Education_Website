using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using scope_project_2.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics.Metrics;

namespace scope_project_2.Controllers
{

    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult contact()
        {

            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult contact(Contact contact)
        {

            if (ModelState.IsValid)
            {

                // Sender's email credentials
                string senderEmail = "bharathibharathi5323@gmail.com";
                string senderPassword = "nlcowugzzquhivhh";

                // Receiver's email
                string? receiverEmail = contact.Email;

                // Mail message details

                string body = "Name:" + contact.Name + "\n" +
                    "Email :'" + contact.Email + "'\n" + "subject :'" + contact.Subject + "'\n" + "message :'" + contact.Message + "'";

                // Create the mail message
                MailMessage mailMessage = new MailMessage(senderEmail, receiverEmail, "contact data", body);

                // Setup the SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true; // Set to true for SSL/TLS
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);


                // Send the email
                smtpClient.Send(mailMessage);


                return View("sucess");
            }
            else
            {
                return Error();
            }


        }

        public IActionResult sucess()
        {

            return View();
        }
        public IActionResult Error()
        {

            return View();
        }


    }

}
