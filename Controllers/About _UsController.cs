using Microsoft.AspNetCore.Mvc;

namespace scope_project_2.Controllers
{
    public class About_UsController : Controller
    {
        public IActionResult about_us()
        {
            return View();
        }
    }
}
