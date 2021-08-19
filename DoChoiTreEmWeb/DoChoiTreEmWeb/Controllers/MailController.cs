using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using DoChoiTreEmWeb.Models;
using System.IO;

namespace DoChoiTreEmWeb.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMail(Mail model)
        {
            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("phongha676@gmail.com", "Phong123."),
                EnableSsl = true
            };
            // tạo email
            var message = new MailMessage();
            message.From = new MailAddress(model.Form);
            message.ReplyToList.Add(model.Form);
            message.To.Add("1924801030235@student.tdmu.edu.vn");
            message.Subject = model.Subject;
            message.Body = model.Notes;

            var f = Request.Files["attachment"];
            var path = Path.Combine(Server.MapPath("~/UpFile"), f.FileName);
            if (!System.IO.File.Exists(path))
            {
                f.SaveAs(path);
            }
            /// KHai báo thư viện using System.Net.Mine
            Attachment data = new Attachment(Server.MapPath("~/UpFile/" + f.FileName), MediaTypeNames.Application.Octet);
            message.Attachments.Add(data);
            mail.Send(message);
            return View("SendMail");
        }
    }
}