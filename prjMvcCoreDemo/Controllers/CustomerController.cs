using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult List(CKeywordViewModel vm)
        {
            dbDemoContext db = new dbDemoContext();
            IEnumerable<TCustomer> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from c in db.TCustomers
                        select c;
            else
                datas = db.TCustomers.Where(p=>p.FName.Contains(vm.txtKeyword) ||
               p.FPhone.Contains(vm.txtKeyword)||
               p.FAddress.Contains(vm.txtKeyword)||
               p.FEmail.Contains(vm.txtKeyword));
            return View(datas);
        }

        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(TCustomer p)
        {
            dbDemoContext db = new dbDemoContext();
            db.TCustomers.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int?id)
        {
            dbDemoContext db = new dbDemoContext();
            TCustomer cust = db.TCustomers.FirstOrDefault(t => t.FId == id);
            if (cust != null)
            {
                db.TCustomers.Remove(cust);
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            dbDemoContext db = new dbDemoContext();
            TCustomer cust = db.TCustomers.FirstOrDefault(t => t.FId == id);
            if (cust == null)
                return RedirectToAction("List");
            return View(cust);
        }

        [HttpPost]
        public IActionResult Edit(TCustomer p)
        {
            dbDemoContext db = new dbDemoContext();
            TCustomer cust = db.TCustomers.FirstOrDefault(t => t.FId == p.FId);
            if (cust != null)
            {
                cust.FName = p.FName;
                cust.FPhone = p.FPhone;
                cust.FEmail = p.FEmail;
                cust.FAddress = p.FAddress;
                cust.FPassword = p.FPassword;
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}
