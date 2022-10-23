using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YolGecenHani.Models;
using YolGecenHani.Models.EntityModel;
using System.Web.Security;
using System.Text.RegularExpressions;
using YolGecenHani.Models.AdminModel;
using System.IO;

namespace YolGecenHani.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(User user)
        {
            var userControl = db.User.FirstOrDefault(x => x.UserName == user.UserName || x.Email == user.Email);

            if (userControl != null)
            {
                ViewBag.Mesaj = "Kullanıcı adı yada Email Adresi sisteme kayıtlı. Lütfen farklı bir hesap ismi ile devam ediniz yada giriş yapınız.";
            }
            else
            {
                
                
                if (passwordControl(user.Password) == false)
                {
                    ViewBag.Mesaj = "Şifre tanımlarken  en az 8 karakter ve 1 büyük harf, 1 küçük harf, rakam ve özel karakter içermelidir.";
                }
                else
                {
                    db.User.Add(user);
                    db.SaveChanges();
                    ViewBag.Mesaj = "Tebrikler kaydınız başarlı bir şekilde oluştu.";
                }
            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user,bool Remember,int? checkout)
        {
            var userControl = db.User.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (userControl != null)
            {
                if (userControl.Status==false && userControl.IsActive==false)
                {
                    ViewBag.Mesaj = "Kullanıcı Aktif Değil. Bağlı olduğu Email ("+userControl.Email.Substring(0,2)+"....."+ userControl.Email.Substring(userControl.Email.Length-8,8) + ") adresinden aktif edebilirsiniz.";
                }
                else if (userControl.Status == false)
                {
                    ViewBag.Mesaj = "Kullanıcı Durumu Pasif. Kullanıcı Aktif Etmek İçin Yetkili Birim İle İletişime Geçiniz";
                }
                else if (userControl.IsDelete == true)
                {
                    ViewBag.Mesaj = "Bu Hesap Silinmiştir Lüften Farkı Bir Hesap İle Devam Ediniz.";
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userControl.UserName, Remember);
                    if (checkout == 1)
                    {
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                    return RedirectToAction("Index");

                }
                return View();
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı Adı yada Şifre Hatalı. Lütfen Tekrar Deneyiniz.";
                return View();
            }
            
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult UserProfile()
        {
            UserRoleBadgeModel model = new UserRoleBadgeModel()
            {
                User=db.User.FirstOrDefault(x=>x.UserName==User.Identity.Name),
                Role=db.Role.ToList(),
                Badge=db.Badge.ToList()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult UserEditInformation()
        {
            UserRoleBadgeModel model = new UserRoleBadgeModel()
            {
                User = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult UserEditInformation(User user,HttpPostedFileBase Image)
        {
            UserRoleBadgeModel model = new UserRoleBadgeModel();

            model.User = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name);
            
            if(Image!=null && Image.ContentLength > 0)
            {
                string ImagePath = "", ImageName = "";

                ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/users"), ImageName);
                Image.SaveAs(ImagePath);

                model.User.Image = ImageName;
            }

            model.User.Name = user.Name;
            model.User.Surname = user.Surname;
            model.User.Phone = user.Phone;
            model.User.BirthDay = user.BirthDay;
            model.User.Gender = user.Gender;
            
            db.SaveChanges();

            return View(model);
        }
        [HttpGet]
        public ActionResult EditPassword()
        {
            UserRoleBadgeModel model = new UserRoleBadgeModel()
            {
                User = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult EditPassword(string Password,string NewPassword,string NewPasswordRepeat)
        {
            UserRoleBadgeModel model = new UserRoleBadgeModel()
            {
                User = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name)
            };

            if (model.User.Password == Password)
            {
                if (passwordControl(NewPassword))
                {
                    if (NewPassword == NewPasswordRepeat)
                    {
                        model.User.Password = NewPassword;
                        db.SaveChanges();
                        ViewBag.Mesaj = "Şifre Değiştirme Başarılı.";
                    }
                    else
                    {
                        ViewBag.Mesaj = "Yeni Şifre ve Yeni Şifre Tekrar Uyuşmıyor. Lütfen Tekrar Deneyiniz.";
                    }
                }
                else
                {
                    ViewBag.Mesaj = "Şifre tanımlarken  en az 8 karakter ve 1 büyük harf, 1 küçük harf, rakam ve özel karakter içermelidir.";
                }
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcıy ait şifre hatalı. Lütfen tekrar deneyiniz";
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult UserGrade()
        {
            UserRoleBadgeModel model = new UserRoleBadgeModel()
            {
                User = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name),
                Role = db.Role.ToList(),
                Badge = db.Badge.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult UserGrade(User user,string MoneyPoint)
        {
            double money = Convert.ToDouble(MoneyPoint.Replace('.', ','));

            UserRoleBadgeModel model = new UserRoleBadgeModel()
            {
                User = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name),
                Role = db.Role.ToList(),
                Badge = db.Badge.ToList()
            };

            model.User.RoleId = user.RoleId;
            if (money != 0) { 
                model.User.MoneyPoint += money;
            }
            db.SaveChanges();
            ViewBag.Mesaj = "Düzenleme Başarılı.";
            return View(model);
        }

        public static bool passwordControl(string Password)
        {
            Regex pc = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#$%@!?.,_^*+/-]).{8,}$");

            return pc.IsMatch(Password);
        }
    }

}