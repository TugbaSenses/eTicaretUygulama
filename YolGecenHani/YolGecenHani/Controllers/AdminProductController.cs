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
    [RoutePrefix("Admin/Product")]
    [Route("{action=index}")]
    public class AdminProductController : Controller
    {
        DataContext db = new DataContext();
        // GET: AdminProduct
        public ActionResult Index()
        {
            RestaurantProductModel model = new RestaurantProductModel();
            var user = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name);
            model.Restaurant = db.Restaurant.FirstOrDefault(x => x.UserId == user.Id);
            if (model.Restaurant != null)
            {
                model.CategoryList = db.Category.Where(x => x.RestaurantId == model.Restaurant.Id).ToList();
                var productlist = db.Product.ToList();
                var cp = from category in model.CategoryList
                         from product in productlist.Where(x => x.CategoryId == category.Id && x.IsDelete == false).ToList()

                         select new
                         {
                             product.Id,
                             product.Name,
                             product.IsDelete,
                             product.Status,
                             product.Image,
                             product.DiscountId,
                             product.Description,
                             product.CategoryId,
                             product.Price,
                             product.Category,
                             product.Discount
                         };
                List<Product> pl = new List<Product>();
                foreach (var item in cp)
                {
                    Product product = new Product();
                    product.Id = item.Id;
                    product.Name = item.Name;
                    product.Description = item.Description;
                    product.CategoryId = item.CategoryId;
                    product.DiscountId = item.DiscountId;
                    product.Price = item.Price;
                    product.Status = item.Status;
                    product.Image = item.Image;
                    product.IsDelete = item.IsDelete;
                    product.Category = item.Category;
                    product.Discount = item.Discount;

                    pl.Add(product);
                }

                model.ProductList = pl.ToList();
                return View(model);
            }
            else
            {
                return Redirect("~/Admin/Restaurant");
            }
            
        }
        [HttpGet]
        public ActionResult Create()
        {
            RestaurantProductModel model = new RestaurantProductModel();
            var user = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name);
            model.Restaurant = db.Restaurant.FirstOrDefault(x => x.UserId == user.Id);
            model.CategoryList = db.Category.Where(x => x.RestaurantId == model.Restaurant.Id && x.IsDelete==false).ToList();
            model.DiscountList = db.Discount.Where(x => x.IsDelete == false).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Product product,HttpPostedFileBase Image)
        {
            Product newProduct = new Product();
            if(Image!=null && Image.ContentLength > 0)
            {
                string ImageName = "", ImagePath = "";
                ImageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/product"), ImageName);
                Image.SaveAs(ImagePath);

                newProduct.Image = ImageName;
            }
            newProduct.Name = product.Name;
            newProduct.Description = product.Description;
            newProduct.Price = product.Price;
            newProduct.CategoryId = product.CategoryId;
            newProduct.DiscountId = product.DiscountId;
            newProduct.Status = product.Status;

            db.Product.Add(newProduct);
            db.SaveChanges();

            return Redirect("~/Admin/Product/Edit/"+newProduct.Id);
        }

        [HttpGet]
        [Route("~/Admin/Product/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            RestaurantProductModel model = new RestaurantProductModel();
            var user = db.User.FirstOrDefault(x => x.UserName == User.Identity.Name);
            model.Restaurant = db.Restaurant.FirstOrDefault(x => x.UserId == user.Id);
            model.CategoryList = db.Category.Where(x => x.RestaurantId == model.Restaurant.Id && x.IsDelete == false).ToList();
            model.DiscountList = db.Discount.Where(x => x.IsDelete == false).ToList();
            model.Product = db.Product.FirstOrDefault(x => x.Id == id && x.IsDelete==false);
            if (model.Product != null)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("index");
            }

        }
        [HttpPost]
        [Route("~/Admin/Product/Edit/{id:int}")]
        public ActionResult Edit(Product product,HttpPostedFileBase Image)
        {
            var editProduct = db.Product.FirstOrDefault(x => x.Id == product.Id);
            if (editProduct != null)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    string ImageName = "", ImagePath = "";
                    ImageName = Guid.NewGuid().ToString().Substring(0, 8) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Admin/assets/images/product"), ImageName);
                    Image.SaveAs(ImagePath);

                    editProduct.Image = ImageName;
                }
                editProduct.Name = product.Name;
                editProduct.Description = product.Description;
                editProduct.Price = product.Price;
                editProduct.CategoryId = product.CategoryId;
                editProduct.DiscountId = product.DiscountId;
                editProduct.Status = product.Status;
                
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        [Route("~/Admin/Product/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = db.Product.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                product.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}