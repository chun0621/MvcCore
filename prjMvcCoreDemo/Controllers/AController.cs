using Microsoft.AspNetCore.Mvc;
using prjMvcDemo.Models;

namespace prjMvcCoreDemo.Controllers
{
    public class AController : Controller
    {
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
