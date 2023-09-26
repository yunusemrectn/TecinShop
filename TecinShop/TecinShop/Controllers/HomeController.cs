using BusinessLayer.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;

namespace TecinShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ProductRepository productRepository = new ProductRepository();

        public ActionResult Index(int sayfa=1)
        {
            return View(productRepository.GetIsApprovedProduct().ToPagedList(sayfa,6));
        }
    }
}