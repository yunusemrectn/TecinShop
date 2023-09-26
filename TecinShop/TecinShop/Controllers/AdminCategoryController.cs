using BusinessLayer.Concrete;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace TecinShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoryController : Controller
    {
        // GET: AdminCategory
        CategoryRepository categoryRepository = new CategoryRepository();
        public ActionResult Index()
        {
            return View(categoryRepository.List());
        }
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category parameter)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Insert(parameter);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("","Ekleme işleminde bir hata oluştu.");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var delete = categoryRepository.GetById(id);
            categoryRepository.Delete(delete);
            return RedirectToAction("Index");
        }
        
        public ActionResult Update(int id) 
        {
            var update = categoryRepository.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Category parameter)
        {
            if (ModelState.IsValid)
            {
                var update = categoryRepository.GetById(parameter.Id);
                update.Name = parameter.Name;
                update.Description = parameter.Description;
                categoryRepository.Update(update);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Güncelleme işleminde bir hata oluştu.");
            return View();
        }
    }
}