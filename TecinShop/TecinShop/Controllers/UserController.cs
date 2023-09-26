using DataAccessLayer.Context;
using EntityLayer.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TecinShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        DataContext db = new DataContext();
        public ActionResult Update()
        {
            var username = (string)Session["Mail"];
            var entities = db.Users.FirstOrDefault(x => x.Email == username);
            return View(entities);
        }
        [HttpPost]
        public ActionResult Update(User data)
        {
            var username = (string)Session["Mail"];
            var user = db.Users.Where(x => x.Email == username).FirstOrDefault(x => x.Email == username);
            user.Name = data.Name;
            user.Surname = data.Surname;
            user.UserName = data.UserName;
            user.Email = data.Email;
            user.Password = data.Password;
            db.SaveChanges();
            Session["Name"] = data.Name;
            Session["Surname"] = data.Surname;
            return RedirectToAction("Index","Home");
        }

        public ActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PasswordReset(string email)
        {
            var mail = db.Users.Where(x => x.Email == email).SingleOrDefault();
            if (mail != null)
            {
                Random rnd = new Random();
                int newPassword = rnd.Next();
                User password = new User();
                //mail.Password = Crypto.Hash(Convert.ToString(newPassword),"MD5");
                mail.Password = Convert.ToString(newPassword);
                db.SaveChanges();
                /*
                 * Eskisi gibi gmail adresimiz ve şifremizi girerek mail gönderemiyoruz.
                 * Google güvenlik ayarlarında 3. taraf uygulamaları bölümünde bir bağlantı oluşturuyorsunuz.
                 * Size vermiş olduğu key ve mail adresinizi girerek mail gönderme işlemini yapabilirsiniz.
                 */
                MimeMessage mimeMessage = new MimeMessage();
                var name = password.Name + " " + password.Surname;
                MailboxAddress mailboxAddressFrom = new MailboxAddress("TecinShop", "mail adresiniz");
                mimeMessage.From.Add(mailboxAddressFrom);
                MailboxAddress mailboxAddressTo = new MailboxAddress(name, email);
                mimeMessage.To.Add(mailboxAddressTo);
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Yeni Şifreniz : " + newPassword.ToString();
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = "Geçici şifreniz";
                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("mail adresiniz", "key");
                client.Send(mimeMessage);
                client.Disconnect(true);
                ViewBag.error = "Şifreniz başarıyla gönderilmiştir.";
            }
            else
            {
                ViewBag.error = "Hata oluştu tekrar deneyiniz.";
            }
            return View();
        }
    }
}