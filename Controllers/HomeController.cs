using Resume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Resume.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DownloadFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "PDF/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "Ronit_Resume.pdf");
            string fileName = "RonitPatel_Resume.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public ActionResult Index(ContactMe e)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    StringBuilder message = new StringBuilder();
                    MailAddress from = new MailAddress(e.Email.ToString());
                    message.Append("Name: " + e.Name + "\n");
                    message.Append("Email: " + e.Email + "\n");
                    message.Append("Subject: " + e.Subject + "\n");
                    message.Append("Message:" +e.Message);

                    MailMessage db = new MailMessage();
                    db.From = from;//Email which you are getting 
                                                         //from contact us page 
                    db.To.Add("patelronit222@gmail.com");//Where mail will be sent 
                    db.Subject = e.Subject;
                    db.Body = message.ToString();
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("patelronit222@gmail.com", "9879985126");

                    smtp.EnableSsl = true;

                    smtp.Send(db);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }

            return View();
        }
    }
}