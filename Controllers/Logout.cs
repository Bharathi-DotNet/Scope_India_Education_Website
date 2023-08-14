using Microsoft.AspNetCore.Mvc;

namespace scope_project_2.Controllers
{
    public class Logout : Controller
    {
        public IActionResult logout()
        {
            HttpContext.Response.Cookies.Delete("email");
            HttpContext.Response.Cookies.Delete("pass");
            HttpContext.Session.Remove("user_email");
            HttpContext.Session.Remove("user_pass");
            return RedirectToRoute(new { Action = "_login", Controller = "Login" });
        }
    }
}
