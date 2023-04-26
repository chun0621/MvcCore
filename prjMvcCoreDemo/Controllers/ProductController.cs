using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class ProductController : Controller
    {
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
        public IActionResult Edit(TProduct p)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct prod = db.TProducts.FirstOrDefault(t => t.FId == p.FId);
            if (prod != null)
            {
                prod.FName = p.FName;
                prod.FQty = p.FQty;
                prod.FCost = p.FCost;
                prod.FPrice = p.FPrice;
                prod.FImagePath = p.FImagePath;
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}