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
    [RoutePrefix("Admin/UserAddress")]
    [Route("{action=index}")]
    public class AdminUserAddressController : Controller
    {
        DataContext db = new DataContext();
        // GET: AdminUserAddress
        public ActionResult Index()
        {
            UserAddressCityModel model = new UserAddressCityModel()
            {
                UserAddressList= db.UserAddress.Where(x => x.IsDelete == false && x.User.UserName == User.Identity.Name).ToList(),
                User=db.User.FirstOrDefault(x=>x.UserName==User.Identity.Name),
                CityList=db.City.Where(x=>x.IsDelete==false).ToList()
            };
            
            return View(model);
        }

        [Route("~/Admin/UserAddress/Selected/{id:int}")]
        public ActionResult Selected(int id)
        {
            var address = db.UserAddress.Find(id);
            if (address != null)
            {
                var allAddress = db.UserAddress.Where(x => x.UserId == address.UserId).ToList();

                foreach (var all in allAddress)
                {
                    all.Selected = false;
                }

                address.Selected = true;
                db.SaveChanges();

            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            var city = db.City.Where(x => x.Status == true && x.IsDelete == false).ToList();
            return View(city);
        }
        [HttpPost]
        public ActionResult Create(UserAddress address)
        {
            var user = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name);

            UserAddress newAddress = new UserAddress()
            {
                Title=address.Title,
                CityId=address.CityId,
                Direction=address.Direction,
                OpenAddress=address.OpenAddress,
                Status=address.Status,
                Selected=false,
                UserId=user.Id
            };

            db.UserAddress.Add(newAddress);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("~/Admin/UserAddress/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            UserAddressCityModel model = new UserAddressCityModel();
            model.UserAddress = db.UserAddress.Find(id);
            model.CityList = db.City.Where(x => x.Status == true && x.IsDelete == false).ToList();
            return View(model);
        }
        [HttpPost]
        [Route("~/Admin/UserAddress/Edit/{id:int}")]
        public ActionResult Edit(UserAddress address)
        {
            var editAddress = db.UserAddress.FirstOrDefault(x => x.Id == address.Id);

            if (editAddress != null)
            {
                editAddress.Title = address.Title;
                editAddress.Direction = address.Direction;
                editAddress.OpenAddress = address.OpenAddress;
                editAddress.CityId = address.CityId;
                editAddress.Status = address.Status;
                db.SaveChanges();

            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpGet]
        [Route("~/Admin/UserAddress/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var address = db.UserAddress.Find(id);
            if (address != null)
            {
                address.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}