using Microsoft.AspNetCore.Mvc;

namespace scope_project_2.Controllers
{
    public class welcomeController : Controller
    {
        public IActionResult home()
        {
            return View();
        }  public IActionResult about_us()
        {
            return View();
        }  public IActionResult Contact()
        {
            return View();
        }  public IActionResult Registration()
        {
            return View();
        }  
        public IActionResult login()
        {
            return View();
        }     
        public IActionResult Student_DashBoard()
        {

            return View();
        }
    }
}
