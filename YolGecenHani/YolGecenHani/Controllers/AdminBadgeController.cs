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
    [RoutePrefix("Admin/Badge")]
    [Route("{action=index}")]
    public class AdminBadgeController : Controller
    {
        DataContext db = new DataContext();
        // GET: AdminBadge
        public ActionResult Index()
        {
            var badge = db.Badge.Where(x => x.IsDelete == false).ToList();
            return View(badge);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Badge badge,HttpPostedFileBase Image)
        {
            Badge newBadge = new Badge();
            if(Image!=null && Image.ContentLength > 0)
            {
                string ImageName = "", ImagePath = "";
                ImageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/badge"),ImageName);
                Image.SaveAs(ImagePath);

                newBadge.Image = ImageName;
            }
            newBadge.Name = badge.Name;
            newBadge.Description = badge.Description;
            newBadge.Score = badge.Score;
            newBadge.BadgeColor = badge.BadgeColor;
            newBadge.Status = badge.Status;

            db.Badge.Add(newBadge);
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        [Route("~/Admin/Badge/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var editBadge = db.Badge.Find(id);
            if (editBadge != null)
            {
                return View(editBadge);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        [Route("~/Admin/Badge/Edit/{id:int}")]
        public ActionResult Edit(Badge badge, HttpPostedFileBase Image)
        {
            var editBadge = db.Badge.Find(badge.Id);
            if (Image != null && Image.ContentLength > 0)
            {
                string ImageName = "", ImagePath = "";
                ImageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/badge"), ImageName);
                Image.SaveAs(ImagePath);

                editBadge.Image = ImageName;
            }
            editBadge.Name = badge.Name;
            editBadge.Description = badge.Description;
            editBadge.Score = badge.Score;
            editBadge.BadgeColor = badge.BadgeColor;
            editBadge.Status = badge.Status;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [Route("~/Admin/Badge/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var badge = db.Badge.Find(id);
            if (badge != null)
            {
                badge.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}