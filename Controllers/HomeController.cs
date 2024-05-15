using Microsoft.AspNetCore.Mvc;

namespace Apolchevskaya.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult xxx([FromForm]int[] CheckBox, int RadioDef)
        {
            return View();
        }
    }
}
