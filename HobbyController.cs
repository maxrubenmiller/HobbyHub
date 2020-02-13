using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HobbyHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HobbyHub.Controllers
{
   [Route("hobbies")]
    public class HobbyController : Controller
    {
        private HomeContext dbContext;
        public HobbyController(HomeContext context)
        {
            dbContext = context;
        }


        [HttpGet("Hobby")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetString("UserUsername") == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            else
            {
                User userInDb = dbContext.Users.Include(u => u.PlannedHobbies).FirstOrDefault(u=> u.Username == HttpContext.Session.GetString("UserUsername"));
                ViewBag.User = userInDb;

                List<Hobby> AllHobbies = dbContext.Hobbies.Include(h => h.GuestList).ThenInclude(r => r.Guest).ToList();

                return View(AllHobbies);

            }
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            if(HttpContext.Session.GetString("UserUsername") == null)
            {
                return RedirectToAction("LogOut", "Home");
            }
            else
            {
                User userInDb = dbContext.Users.Include(u => u.PlannedHobbies).FirstOrDefault(u => u.Username == HttpContext.Session.GetString("UserUsername"));
                ViewBag.User = userInDb;

                return View();
            }

        }

        
        
        [HttpPost("create")]
        public IActionResult Create(Hobby plan)
        {
            if(HttpContext.Session.GetString("UserUsername") == null)
            {
                return RedirectToAction("LogOut", "Home");
            } 
            else
            {
                if(ModelState.IsValid)
                {
                    dbContext.Hobbies.Add(plan);
                    dbContext.SaveChanges();
                    return Redirect($"show/{plan.HobbyId}");
                }
                else
                {
                    User userInDb = dbContext.Users.Include(u => u.PlannedHobbies).FirstOrDefault(u => u.Username == HttpContext.Session.GetString("UserUsername"));
                    ViewBag.User = userInDb;

                    return View("New");
                }
            }
        }

        [HttpGet("show/{hobbyId}")]
        public IActionResult Show(int hobbyId)
        {
           if(HttpContext.Session.GetString("UserUsername") == null)
            {
                return RedirectToAction("LogOut", "Home");
            } 
            else
            {
                Hobby flapjack = dbContext.Hobbies.Include(h => h.GuestList).ThenInclude(r => r.Guest).FirstOrDefault(h => h.HobbyId == hobbyId);

                User userInDb = dbContext.Users.Include(u => u.PlannedHobbies).FirstOrDefault(u => u.Username == HttpContext.Session.GetString("UserUsername"));
                ViewBag.User = userInDb;

                return View(flapjack);
            } 
        }

        [HttpGet("rsvp/{hobbyId}/{userId}/status")]

        public IActionResult RSVP(int hobbyId, int userId, string status)
        {
           if(HttpContext.Session.GetString("UserUsername") == null)
            {
                return RedirectToAction("LogOut", "Home");
            } 
            else
            {
                Rsvp response = new Rsvp();
                response.UserId = userId;
                response.HobbyId = hobbyId;
                dbContext.Rsvps.Add(response);
                dbContext.SaveChanges();
            } 
            return RedirectToAction("Dashboard");
        }
        
    }
}