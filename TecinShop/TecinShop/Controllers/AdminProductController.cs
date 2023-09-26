using BusinessLayer.Concrete;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace TecinShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        // GET: AdminProduct
        ProductRepository productRepository = new ProductRepository();
        DataContext db = new DataContext();
        public ActionResult Index(int sayfa=1)
        {
            return View(productRepository.List().ToPagedList(sayfa,6));
        }
        public ActionResult Create()
        {
            List<SelectListItem> entity1 = (from i in db.Categories.ToList() 
            select new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList();
            ViewBag.ktgr = entity1;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product data, HttpPostedFileBase File) 
        { 
            if (ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Hata oluştu.");
            }
            string path = Path.Combine("~/Content/Image/" + File.FileName);
            File.SaveAs(Server.MapPath(path));
            data.Image = File.FileName.ToString();
            productRepository.Insert(data);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var delete = productRepository.GetById(id);
            productRepository.Delete(delete);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            List<SelectListItem> entity1 = (from i in db.Categories.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.Name,
                                                Value = i.Id.ToString()
                                            }).ToList();
            ViewBag.ktgr = entity1;
            var update = productRepository.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Product data, HttpPostedFileBase File)
        {
            var update = productRepository.GetById(data.Id);
            if (!ModelState.IsValid)
            {
                if (File == null)
                {
                    update.Name = data.Name;
                    update.Description = data.Description;
                    update.Price = data.Price;
                    update.Stock = data.Stock;
                    update.Popular = data.Popular;
                    update.IsApproved = data.IsApproved;
                    update.CategoryId = data.CategoryId;
                    productRepository.Update(update);
                    return RedirectToAction("Index");
                }
                else
                {
                    update.Name = data.Name;
                    update.Description = data.Description;
                    update.Price = data.Price;
                    update.Stock = data.Stock;
                    update.Popular = data.Popular;
                    update.IsApproved = data.IsApproved;
                    update.CategoryId = data.CategoryId;
                    string path = Path.Combine("~/Content/Image/" + File.FileName);
                    File.SaveAs(Server.MapPath(path));
                    update.Image = File.FileName.ToString();
                    productRepository.Update(update);
                    return RedirectToAction("Index");
                }
            }
            return View(update);
        }
        public ActionResult CriticalStock()
        {
            var critic = db.Products.Where(x => x.Stock <= 20).ToList();
            return View(critic);
        }
        public ActionResult StockCount() 
        {
            if (User.Identity.IsAuthenticated)
            {
                var count = db.Products.Where(x => x.Stock < 20).Count();
                ViewBag.count = count;
                var azalan = db.Products.Where(x => x.Stock == 20).Count();
                ViewBag.azalan = azalan;
            }
            return PartialView();
        }
    }
}