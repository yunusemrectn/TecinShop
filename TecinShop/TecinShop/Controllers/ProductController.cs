using BusinessLayer.Concrete;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TecinShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ProductRepository productRepository = new ProductRepository();
        DataContext db = new DataContext();
        public PartialViewResult PopularProduct()
        {
            var popular = productRepository.GetPopularProduct();
            ViewBag.popular = popular;
            return PartialView();
        }
        [Route("product/productdetails/{id}/{name}")]
        public ActionResult ProductDetails(int id)
        {
            var details = productRepository.GetById(id);
            var comment = db.Comments.Where(x=> x.ProductId == id).ToList();
            ViewBag.comment = comment;
            return View(details);
        }
        [HttpPost]
        public ActionResult Comment(Comment Data)
        {
           if (User.Identity.IsAuthenticated) 
            {
                db.Comments.Add(Data);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}