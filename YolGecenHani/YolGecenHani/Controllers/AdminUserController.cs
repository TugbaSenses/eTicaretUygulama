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
    [RoutePrefix("Admin/User")]
    [Route("{action=index}")]
    public class AdminUserController : Controller
    {
        DataContext db = new DataContext();
        // GET: AdminUser
        public ActionResult Index()
        {
            UserRoleBadgeModel model = new UserRoleBadgeModel() {
                UserList = db.User.Where(x => x.IsDelete == false).ToList(),
                Role=db.Role.ToList(),
                Badge=db.Badge.ToList()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var role = db.Role.Where(x=>x.Status==true && x.IsDelete==false).ToList();
            return View(role);
        }
        [HttpPost]
        public ActionResult Create(User user, HttpPostedFileBase Image)
        {
            var userControl = db.User.FirstOrDefault(x => x.UserName == user.UserName || x.Email == user.Email);
            if (userControl == null)
            {
                User newUser = new User();
                string ImagePath = "", ImageName = "";
                if(Image!=null && Image.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/users"), ImageName);
                    Image.SaveAs(ImagePath);
                    newUser.Image = ImageName;
                }
                newUser.UserName = user.UserName;
                newUser.Email = user.Email;
                newUser.Password = user.Password;
                newUser.Name = user.Name;
                newUser.Surname = user.Surname;
                newUser.Gender = user.Gender;
                newUser.RoleId = user.RoleId;

                db.User.Add(newUser);
                db.SaveChanges();

                ViewBag.Mesaj = "Kullanıcı Kaydı Başarılı";

            }
            else
            {
                ViewBag.Mesaj = "Username veya Email Sistemde Kayıtlı. Lütfen Başka Bir Username veya Email Kullanınız.";
            }
            var role = db.Role.Where(x => x.Status == true && x.IsDelete == false).ToList();
            return View(role);

            //return RedirectToAction("Create");
        }
        [HttpGet]
        [Route("~/Admin/User/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var user = db.User.Find(id);
            if (user != null)
            {
                UserRoleBadgeModel model = new UserRoleBadgeModel()
                {
                    User = user,
                    Role = db.Role.ToList(),
                    Badge = db.Badge.ToList()
                    
                };
                return View(model);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        [Route("~/Admin/User/Edit/{Id:int}")]
        public ActionResult Edit(User user,HttpPostedFileBase Image)
        {
            var editUser = db.User.Find(user.Id);

            if(Image!=null && Image.ContentLength > 0)
            {
                string ImageName = "", ImagePath = "";
                ImageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/users"), ImageName);
                Image.SaveAs(ImagePath);
                editUser.Image = ImageName;
            }
            editUser.UserName = user.UserName;
            editUser.Email = user.Email;
            editUser.Name = user.Name;
            editUser.Surname = user.Surname;
            editUser.Phone = user.Phone;
            editUser.BirthDay = user.BirthDay;
            editUser.Password = user.Password;
            editUser.RoleId = user.RoleId;
            editUser.Gender = user.Gender;
            editUser.Money = user.Money;
            editUser.MoneyPoint = user.MoneyPoint;
            editUser.ErrorPasswordEntry = user.ErrorPasswordEntry;
            editUser.IsActive = user.IsActive;
            editUser.Status = user.Status;
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }
        [Route("~/Admin/User/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var user = db.User.Find(id);
            if (user != null)
            {
                user.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}