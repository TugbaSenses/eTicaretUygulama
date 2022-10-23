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
    [RoutePrefix("Admin/Restaurant")]
    [Route("{action=index}")]
    public class AdminRestaurantController : Controller
    {
        DataContext db = new DataContext();
        // GET: AdminRestaurant
        [HttpGet]
        public ActionResult Index()
        {
            var user = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var userRestaurant = db.Restaurant.FirstOrDefault(x => x.UserId == user.Id && x.IsDelete == false);
            
            RestaurantCityModel model = new RestaurantCityModel()
            {
                Restaurant = userRestaurant,
                CityList=db.City.Where(x=>x.Status==true && x.IsDelete==false).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Restaurant restaurant, HttpPostedFileBase Image,HttpPostedFileBase Logo)
        {
            var user = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var userRestaurant = db.Restaurant.FirstOrDefault(x => x.UserId == user.Id && x.IsDelete == false);

            string ImageName = "", LogoName = "", ImagePath = "";

            if(Image!=null && Image.ContentLength > 0)
            {
                ImageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/restaurant"), ImageName);
                Image.SaveAs(ImagePath);
                userRestaurant.Image = ImageName;
            }
            if (Logo != null && Logo.ContentLength > 0)
            {
                LogoName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Logo.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/restaurant"), LogoName);
                Logo.SaveAs(ImagePath);
                userRestaurant.Logo = LogoName;
            }

            userRestaurant.Name = restaurant.Name;
            userRestaurant.Description = restaurant.Description;
            userRestaurant.CityId = restaurant.CityId;
            userRestaurant.Status = restaurant.Status;

            RestaurantCityModel model = new RestaurantCityModel()
            {
                Restaurant = userRestaurant,
                CityList = db.City.Where(x => x.Status == true && x.IsDelete == false).ToList()
            };

            db.SaveChanges();
            ViewBag.Mesaj = "Edit Successful";
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var user = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var userRestaurant = db.Restaurant.FirstOrDefault(x => x.UserId == user.Id && x.IsDelete == false);

            if (userRestaurant ==null) {
                var city = db.City.Where(x => x.Status == true && x.IsDelete == false).ToList();
                return View(city);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public ActionResult Create(Restaurant restaurant, HttpPostedFileBase Image, HttpPostedFileBase Logo)
        {
            var user = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name);

            Restaurant newRestaurant = new Restaurant();

            string ImageName = "", LogoName = "", ImagePath = "";

            if (Image != null && Image.ContentLength > 0)
            {
                ImageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/restaurant"), ImageName);
                Image.SaveAs(ImagePath);
                newRestaurant.Image = ImageName;
            }
            if (Logo != null && Logo.ContentLength > 0)
            {
                LogoName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Logo.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/restaurant"), LogoName);
                Logo.SaveAs(ImagePath);
                newRestaurant.Logo = LogoName;
            }

            newRestaurant.UserId = user.Id;
            newRestaurant.Name = restaurant.Name;
            newRestaurant.Description = restaurant.Description;
            newRestaurant.CityId = restaurant.CityId;
            newRestaurant.Status = restaurant.Status;

            db.Restaurant.Add(newRestaurant);

            db.SaveChanges();

            return RedirectToAction("index");
           
        }
    }
}