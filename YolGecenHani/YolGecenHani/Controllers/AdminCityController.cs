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
    [RoutePrefix("Admin/City")]
    [Route("{action=index}")]
    public class AdminCityController : Controller
    {
        DataContext db = new DataContext();
        // GET: AdminCity
        public ActionResult Index()
        {
            var city = db.City.Where(x => x.IsDelete == false).ToList();
            return View(city);
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(City city)
        {
            City newCity = new City()
            {
                Name=city.Name,
                Status=city.Status
            };
            db.City.Add(newCity);
            db.SaveChanges();
            
            return View();
        }
        [HttpGet]
        [Route("~/Admin/City/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var editCity = db.City.Find(id);
            if (editCity != null)
            {
                return View(editCity);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        [Route("~/Admin/City/Edit/{id:int}")]
        public ActionResult Edit(City city)
        {
            var editCity = db.City.Find(city.Id);
            if (editCity != null)
            {
                editCity.Name = city.Name;
                editCity.Status = city.Status;
                db.SaveChanges();
                ViewBag.Mesaj = "City Düzenleme Başarılı";
                return View(editCity);
            }
            return RedirectToAction("index");
        }
        [Route("~/Admin/City/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var city = db.City.Find(id);
            if (city != null)
            {
                city.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}