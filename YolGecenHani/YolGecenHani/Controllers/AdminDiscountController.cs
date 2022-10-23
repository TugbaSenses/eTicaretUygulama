using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YolGecenHani.Models;
using YolGecenHani.Models.EntityModel;
using YolGecenHani.Models.AdminModel;
using System.IO;

namespace YolGecenHani.Controllers
{
    [Authorize]
    [RoutePrefix("Admin/Discount")]
    [Route("{action=index}")]
    public class AdminDiscountController : Controller
    {
        DataContext db = new DataContext();
        // GET: AdminRole
        public ActionResult Index()
        {
            var discount = db.Discount.Where(x => x.IsDelete == false).ToList();
            return View(discount);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Discount discount)
        {
            Discount newDiscount = new Discount()
            {
                DiscountPrice = discount.DiscountPrice,
                DiscountRate = discount.DiscountRate,
                Status = discount.Status
            };
            db.Discount.Add(newDiscount);
            db.SaveChanges();
            ViewBag.Mesaj = "Discount Ekleme Başarılı";


            return View();
        }
        [HttpGet]
        [Route("~/Admin/Discount/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var discount = db.Discount.Find(id);
            if (discount != null)
            {
                return View(discount);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        [Route("~/Admin/Discount/Edit/{Id:int}")]
        public ActionResult Edit(Discount discount)
        {
            var editDiscount = db.Discount.Find(discount.Id);
            if (editDiscount != null)
            {

                editDiscount.DiscountPrice = discount.DiscountPrice;
                editDiscount.DiscountRate = discount.DiscountRate;
                editDiscount.Status = discount.Status;
                db.SaveChanges();
                ViewBag.Mesaj = "Discount Düzenleme Başarılı";

                return View(editDiscount);
            }
            return RedirectToAction("index");
        }

        [Route("~/Admin/Discount/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var discount = db.Discount.Find(id);
            if (discount != null)
            {
                discount.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }

}