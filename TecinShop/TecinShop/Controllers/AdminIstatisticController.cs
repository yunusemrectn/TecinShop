using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TecinShop.Controllers
{
    public class AdminIstatisticController : Controller
    {
        // GET: AdminIstatistic
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            // Satış Sayısı
            var sales = db.Sales.Count();
            ViewBag.sales = sales;
            // Ürün Sayısı
            var products = db.Products.Count();
            ViewBag.products = products;
            // Kategori Sayısı
            var categories = db.Categories.Count();
            ViewBag.categories = categories;
            // Sepet Sayısı
            var basket = db.Sales.Count();
            ViewBag.basket = basket;
            // Kullanıcı Sayısı
            var users = db.Users.Count();
            ViewBag.users = users;
            // Toplam Satış Fiyatı
            var entity = db.Sales.ToList();
            decimal totalSalesPrice = 0;
            foreach (var item in entity)
            {
                totalSalesPrice += item.Price;
            }
            ViewBag.totalSalesPrice = totalSalesPrice;
            return View();
        }
    }
}