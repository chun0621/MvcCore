using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;
using System.Text.Json;

namespace prjMvcCoreDemo.Controllers
{
    public class ShoppingController : Controller
    {

        public IActionResult List()
        {

            dbDemoContext db = new dbDemoContext();

            var datas = from c in db.TProducts
                        select c;

            return View(datas);
        }

        public ActionResult AddToCart(int? id)
        {
            if (id == null)
                return RedirectToAction("List");

            ViewBag.FId = id;
            return View();
        }

        [HttpPost]
        //畫面上的物件，非共用的view物件(View Model)，稱MVVM 
        public ActionResult AddToCart(CAddToCartViewModel vm)
        {
            //撈產品
            dbDemoContext db = new dbDemoContext();
            TProduct prod = db.TProducts.FirstOrDefault(t => t.FId == vm.txtFId);
            if (prod != null)
            {
                string json = "";
                List<CShoppingCartItem> cart = null;
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
                {
                    json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
                    cart=JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
                }
                else
                {
                    cart= new List<CShoppingCartItem>();
                }
                
                CShoppingCartItem item = new CShoppingCartItem();
                item.productId = vm.txtFId;
                item.price= (decimal)prod.FPrice;
                item.count = vm.txtCount;
                item.product = prod;
                cart.Add(item);
                json = JsonSerializer.Serialize(cart);
                HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST,json);
            }

            return RedirectToAction("List");
        }
    }
}
