using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace TecinShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        DataContext db = new DataContext();
        
        public ActionResult Index(int sayfa=1)
        {
            return View(db.Users.Where(x => x.Role == "User").ToList().ToPagedList(sayfa, 8));
        }
        public ActionResult Delete(int id) 
        {
            var delete = db.Users.Where(x=>x.Id == id).FirstOrDefault();
            db.Users.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}