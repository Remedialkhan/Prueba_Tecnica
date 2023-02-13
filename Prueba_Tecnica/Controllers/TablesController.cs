using Microsoft.AspNetCore.Mvc;

namespace Prueba_Tecnica.Controllers
{
    public class TablesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
