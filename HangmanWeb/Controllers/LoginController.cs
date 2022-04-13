using Microsoft.AspNetCore.Mvc;

namespace HangmanWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FormsTestPost()
        {
            Console.WriteLine("please work");
            return Content("Hello, " + HttpContext.Request.Form["username"] + ". You are " + HttpContext.Request.Form["password"] + " years old!");
        }
    }
}
