using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EntityLayer.Entities;

namespace TecinShop.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        DataContext db = new DataContext();
        public ActionResult Index(int sayfa=1)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = db.Users.FirstOrDefault(x => x.Email == username);
                var model = db.Sales.Where(x => x.UserId == user.Id).ToList().ToPagedList(sayfa,6);
                return View(model);
            }
            return HttpNotFound();
        }
        public ActionResult Buy(int id)
        {
            var model = db.Carts.FirstOrDefault(x => x.Id == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Buy2(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = db.Carts.FirstOrDefault(x => x.Id == id);
                    var sales = new Sales
                    {
                        UserId = model.UserId,
                        Product = model.Product,
                        Quantity = model.Quantity,
                        Price = model.Price,
                        Image = model.Image,
                        Date = DateTime.Now,
                    };
                    db.Carts.Remove(model);
                    db.Sales.Add(sales);
                    db.SaveChanges();
                    ViewBag.message = "Satın alma işlemi başarılı!";
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Satın alma işlemi başarısız..!";
            }
            return View("message");
        }
        public ActionResult BuyAll(decimal? Total)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = db.Users.FirstOrDefault(x => x.Email == username);
                var model = db.Carts.Where(x => x.UserId == user.Id).ToList();
                var userId = db.Carts.FirstOrDefault(x => x.UserId == user.Id);
                if (model != null)
                {
                    if (userId == null)
                    {
                        ViewBag.total = "Sepetinizde ürün bulunmamaktadır.";
                    }
                    else if (userId != null)
                    {
                        Total = db.Carts.Where(x => x.UserId == userId.UserId).Sum(x => x.Product.Price * x.Quantity);
                        ViewBag.total = "Toplam Tutar = " + Total + " TL";
                    }
                    return View(model);
                }
                return View();
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult BuyAll2() 
        {
            var username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(x => x.Email == username);
            var model = db.Carts.Where(x => x.UserId == user.Id).ToList();
            int row = 0;
            foreach (var item in model)
            {
                var sales = new Sales
                {
                    UserId = model[row].UserId,
                    Product = model[row].Product,
                    Quantity = model[row].Quantity,
                    Price = model[row].Price,
                    Image = model[row].Image,
                    Date = model[row].Date
                };
                db.Sales.Add(sales);
                db.SaveChanges();
                row++;
            }
            db.Carts.RemoveRange(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
    }
}