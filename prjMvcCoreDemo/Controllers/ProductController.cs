using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class ProductController : Controller
    {
        private IWebHostEnvironment _enviro;

        public ProductController(IWebHostEnvironment p)
        {
            _enviro = p;
        }


        public IActionResult List(CKeywordViewModel vm)
        {
            dbDemoContext db = new dbDemoContext();
            IEnumerable<TProduct> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from c in db.TProducts
                        select c;
            else
                datas = db.TProducts.Where(p => p.FName.Contains(vm.txtKeyword));
            return View(datas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TProduct p)
        {
            dbDemoContext db = new dbDemoContext();
            db.TProducts.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct cust = db.TProducts.FirstOrDefault(t => t.FId == id);
            if (cust != null)
            {
                db.TProducts.Remove(cust);
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct cust = db.TProducts.FirstOrDefault(t => t.FId == id);
            if (cust == null)
                return RedirectToAction("List");
            return View(cust);
        }

        [HttpPost]
        public IActionResult Edit(CProductWrap p)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct prod = db.TProducts.FirstOrDefault(t => t.FId == p.FId);
            if (prod != null)
            {
                if (p.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _enviro.WebRootPath + "/images/" + photoName;
                    p.photo.CopyTo(new FileStream(path,FileMode.Create));
                    prod.FImagePath = photoName;
                }
                prod.FName = p.FName;
                prod.FQty = p.FQty;
                prod.FCost = p.FCost;
                prod.FPrice = p.FPrice;
               
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}