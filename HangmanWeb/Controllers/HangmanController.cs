using Microsoft.AspNetCore.Mvc;

namespace HangmanWeb.Controllers
{
    public class HangmanController : Controller
    {
        public IActionResult Hangman()
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