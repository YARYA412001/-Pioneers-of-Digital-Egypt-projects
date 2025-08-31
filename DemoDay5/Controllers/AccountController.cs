using Microsoft.AspNetCore.Mvc;

namespace DemoDay4.Controllers
{
    public class AccountController : Controller
    {
        ITIContext context;
        public AccountController()
        {
            context = new ITIContext();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User u) 
        {
            if (u != null) 
            {
                // add
                context.Users.Add(u);
                // save 
                context.SaveChanges();
                // return
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string userName,string password) 
        {
            var user = context.Users.FirstOrDefault(u => u.userName == userName && u.password == password);
            if (user != null) 
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.msg= "Invalid username or password";
            return View();
        }

    }
}
