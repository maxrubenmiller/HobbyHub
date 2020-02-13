using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HobbyHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HobbyHub.Controllers
{
    public class HomeController : Controller
    {

        private HomeContext dbContext;
        public HomeController(HomeContext context)
        {
            dbContext =  context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User register)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any( u => u.Username == register.Username))
                {
                    ModelState.AddModelError("Username","That USername is already in use!");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    register.Password = Hasher.HashPassword(register, register.Password);

                    dbContext.Users.Add(register);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetString("UserUsername", register.Username);
                    return RedirectToAction("Dashboard", "Hobby");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost("login")]
        public IActionResult LogIn(LoginUser login)
        {
            if(ModelState.IsValid)
            {
                User userInDb = dbContext.Users.FirstOrDefault( u => u.Username == login.LoginUsername );
                if( userInDb == null)
                {
                    ModelState.AddModelError("LoginUsername", "Invalid Username/Password");
                    return View("SignIn");
                }
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(login,userInDb.Password, login.LoginPassword);
                if( result == 0)
                {
                    ModelState.AddModelError("LoginUsername", "Invalid Username/Password");
                    return View("SignIn");
                }
                HttpContext.Session.SetString("UserUsername", login.LoginUsername);
                return RedirectToAction("Dashboard", "Hobby");
            }
            else
            {
                return View("SignIn");
            }
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            User userInDb = dbContext.Users.FirstOrDefault( u => u.Username == HttpContext.Session.GetString("UserUsername"));
            if(userInDb == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index");
            }
            return View(userInDb);
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
