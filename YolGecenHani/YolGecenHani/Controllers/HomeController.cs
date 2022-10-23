using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YolGecenHani.Models;
using YolGecenHani.Models.EntityModel;
using YolGecenHani.Models.HomeModel;

namespace YolGecenHani.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult HomeProduct()
        {
            HomeProductModel model = new HomeProductModel();
            model.Category = db.Category.ToList();
            model.Discount = db.Discount.ToList();
            model.Product = db.Product.Where(x => x.Status == true && x.IsDelete == false).ToList();
            return PartialView(model);
        }

      
    }
}