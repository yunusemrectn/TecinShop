using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TecinShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DataContext db = new DataContext();
        public ActionResult Index(decimal? Total)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = db.Users.FirstOrDefault(u => u.Email == username);
                var model = db.Carts.Where(x => x.UserId == user.Id).ToList();
                var userId = db.Carts.FirstOrDefault(x => x.UserId == user.Id);
                if (model != null)
                {
                    if (userId == null)
                    {
                        ViewBag.Total = "Sepetinizde ürün bulunmamaktadır.";
                    }
                    else if (userId != null)
                    {
                        Total = db.Carts.Where(x => x.UserId == userId.UserId).Sum(x => x.Product.Price * x.Quantity);
                    }
                    return View(model);
                }
            }
            return HttpNotFound();
        }
        public ActionResult AddCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity?.Name;
                var model = db.Users.FirstOrDefault(x => x.Email == username);
                var productId = db.Products.Find(id);
                var basket = db.Carts.FirstOrDefault(x => x.UserId == model.Id && x.ProductId == id);
                if (model != null)
                {
                    if (basket != null)
                    {
                        basket.Quantity++;
                        basket.Price = productId.Price * basket.Quantity;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Cart");
                    }
                    var cart = new Cart
                    {
                        UserId = model.Id,
                        ProductId = productId.Id,
                        Quantity = 1,
                        Price = productId.Price,
                        Date = DateTime.Now,
                    };

                    db.Carts.Add(cart);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cart");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult TotalCount(int? count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                count = db.Carts.Where(x => x.UserId == model.Id).Count();
                if (count == 0)
                {
                    ViewBag.count = "";
                }

            }
            return PartialView();
        }

        public void DynamicQuantity(int id, int quantity)
        {
            var model = db.Carts.Find(id);
            model.Quantity = quantity;
            model.Price = model.Product.Price * quantity;
            db.SaveChanges();
        }

        public ActionResult Arttir(int id)
        {
            var model = db.Carts.Find(id);
            var control = model.Product.Stock - model.Quantity;
            if (control > 0)
            {
                model.Quantity++;
                model.Price = model.Product.Price * model.Quantity;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Azalt(int id)
        {
            var model = db.Carts.Find(id);
            if (model.Quantity == 1)
            {
                db.Carts.Remove(model);
                db.SaveChanges();
            }
            model.Quantity--;
            model.Price = model.Product.Price * model.Quantity;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var delete = db.Carts.Find(id);
            db.Carts.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteRange()
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var model = db.Users.FirstOrDefault(x => x.Email == username);
                var delete = db.Carts.Where(x => x.UserId == model.Id);
                db.Carts.RemoveRange(delete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
    }
}