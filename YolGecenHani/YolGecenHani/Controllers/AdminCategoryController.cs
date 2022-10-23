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
    [RoutePrefix("Admin/Category")]
    [Route("{action=index}")]
    public class AdminCategoryController : Controller
    {
        DataContext db = new DataContext();
        public int userRestauran()
        {
            var user = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var userRestauran = db.Restaurant.FirstOrDefault(x => x.UserId == user.Id);
            return userRestauran.Id;
        }
        // GET: AdminRole
        public ActionResult Index()
        {
            int id = userRestauran();
            var category = db.Category.Where(x => x.IsDelete == false && x.RestaurantId==id).ToList();
            return View(category);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category,HttpPostedFileBase Image)
        {
            int id = userRestauran();
            var categoryControl = db.Category.FirstOrDefault(x => x.Name == category.Name && x.RestaurantId==id);
            if (categoryControl == null)
            {
                Category newCategory = new Category();

                if(Image!=null && Image.ContentLength > 0)
                {
                    string ImageName = "", ImagePath = "";
                    ImageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/category"), ImageName);
                    Image.SaveAs(ImagePath);

                    newCategory.Image = ImageName;
                }

                newCategory.Name = category.Name;
                newCategory.Description = category.Description;
                newCategory.Status = category.Status;
                newCategory.RestaurantId = userRestauran();
                
                db.Category.Add(newCategory);
                db.SaveChanges();
                ViewBag.Mesaj = "Category Ekleme Başarılı";

            }
            else
            {
                ViewBag.Mesaj = "Aynı Category Name Kullanılamaz.";
            }
            return View();
        }
        [HttpGet]
        [Route("~/Admin/Category/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var category = db.Category.Find(id);
            if (category != null)
            {
                return View(category);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        [Route("~/Admin/Category/Edit/{Id:int}")]
        public ActionResult Edit(Category category,HttpPostedFileBase Image)
        {
            var editCategory = db.Category.Find(category.Id);
            if (editCategory != null)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    string ImageName = "", ImagePath = "";
                    ImageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/category"), ImageName);
                    Image.SaveAs(ImagePath);

                    editCategory.Image = ImageName;
                }

                editCategory.Name = category.Name;
                editCategory.Status = category.Status;
                editCategory.Description = category.Description;
                db.SaveChanges();
                ViewBag.Mesaj = "Category Düzenleme Başarılı";

                return View(editCategory);
            }
            return RedirectToAction("index");
        }

        [Route("~/Admin/Category/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var category = db.Category.Find(id);
            if (category != null)
            {
                category.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }

}