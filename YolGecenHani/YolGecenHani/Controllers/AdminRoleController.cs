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
    [RoutePrefix("Admin/Role")]
    [Route("{action=index}")]
    public class AdminRoleController : Controller
    {
        DataContext db = new DataContext();
        // GET: AdminRole
        public ActionResult Index()
        {
            var role = db.Role.Where(x => x.IsDelete == false).ToList();
            return View(role);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Role role)
        {
            var roleControl = db.Role.FirstOrDefault(x => x.Name == role.Name);
            if (roleControl == null)
            {
                Role newRole = new Role()
                {
                    Name=role.Name,
                   Status=role.Status
                };
                db.Role.Add(newRole);
                db.SaveChanges();
                ViewBag.Mesaj = "Role Ekleme Başarılı";

            }
            else
            {
                ViewBag.Mesaj = "Aynı Role Name kullanılamaz.";
            }
            return View();
        }
        [HttpGet]
        [Route("~/Admin/Role/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var role = db.Role.Find(id);
            if (role != null)
            {
                return View(role);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        [Route("~/Admin/Role/Edit/{Id:int}")]
        public ActionResult Edit(Role role)
        {
            var editRole = db.Role.Find(role.Id);
            if (role != null)
            {
               
                    editRole.Name = role.Name;
                    editRole.Status = role.Status;
                    db.SaveChanges();
                    ViewBag.Mesaj = "Role Düzenleme Başarılı";
               
                    return View(editRole);
            }
            return RedirectToAction("index");
        }

        [Route("~/Admin/Role/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var role = db.Role.Find(id);
            if (role != null)
            {
                role.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
    
}