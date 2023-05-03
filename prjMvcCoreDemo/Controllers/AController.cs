using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using prjMvcCoreDemo.Models;
using prjMvcDemo.Models;
using System.Runtime.Versioning;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace prjMvcCoreDemo.Controllers
{
    public class AController : Controller
    {
        
        IWebHostEnvironment _enviro;

        public string demoObj2Json()
        {
            TCustomer x = new TCustomer()
            {
                FId = 1,
                FName = "Marco",
                FPhone = "0912345678",
                FEmail = "marco@gmail.com",
                FAddress = "Taipei",
                FPassword = "123"
            };
            string json = JsonSerializer.Serialize(x);
            return json;
        }

        public string demoJson2Obj()
        {
            string json = demoObj2Json();
            TCustomer x = JsonSerializer.Deserialize<TCustomer>(json);
            return x.FName + "<br/>" + x.FPhone;
        }

        //注入寫法，全域變數用建構子變成p
        //也可在Program內直接帶入
        public AController(IWebHostEnvironment p)
        {
            _enviro = p;
        }

        public IActionResult fileUploadDemo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult fileUploadDemo(IFormFile photo)
        {
            //WebRootPath是客戶環境(IIS)，
            string path = _enviro.WebRootPath + "/images/001.jpg";
            //建立串流(FuileStream)，要兩個參數，路徑、若不存在則建立
            photo.CopyTo(new FileStream(path,FileMode.Create));

            return View();
        }


        public string sayHello()
        {
            return "Hello ASP.NET MVC";
        }

        public string lotto()
        {
            return (new CLottoGen()).getNumber();

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
