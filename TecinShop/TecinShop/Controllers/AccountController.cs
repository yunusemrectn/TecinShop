using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace TecinShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        DataContext db = new DataContext();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User data)
        {
            var info = db.Users.FirstOrDefault(x => x.Email == data.Email && x.Password == data.Password);
            if (info != null)
            {
                FormsAuthentication.SetAuthCookie(info.Email, false);
                Session["Mail"] = info.Email.ToString();
                Session["Name"] = info.Name.ToString();
                Session["Surname"] = info.Surname.ToString();
                Session["UserId"] = info.Id.ToString();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.error = "Mail veya şifreniz hatalı";
            return View(data);
        }

        [HttpPost]
        public ActionResult Register(User data) 
        {
            if (ModelState.IsValid) 
            {
                db.Users.Add(data);
                data.Role = "User";
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Hatalı");
            return View("Login", data);
        }
        public ActionResult LogOutt()
        {
            FormsAuthentication.SignOut();
            Session.Remove("Ad");
            return RedirectToAction("Index", "Home");
        }
    }
}