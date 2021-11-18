using SubscriptionPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.ObjectModel;
using System;
using System.Management.Automation;

namespace SubscriptionPortal.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {            
            return View();
        }
        public List<UserModel> PutValue()
        {
            var users = new List<UserModel>
            {                
                new UserModel{Userid=1,UserName="ravi",UserPassword="abc123"},
                new UserModel{Userid=2,UserName="puran",UserPassword="abc123"},
                new UserModel{Userid=3,UserName="mukesh",UserPassword="abc123"},
                new UserModel{Userid=4,UserName="admin",UserPassword="abc123"},
                new UserModel{Userid=5,UserName="ochuser",UserPassword="abc123"},
                new UserModel{Userid=6,UserName="yruser",UserPassword="abc123"},
            };

            return users;
        }

        [HttpPost]
        public IActionResult Verify(UserModel usr)
        {
            var u = PutValue();
            var ui = u.Where(u => u.UserName.Equals(usr.UserName)).Select(j => j.Userid); 
            var ue = u.Where(u => u.UserName.Equals(usr.UserName));
            var up = ue.Where(p => p.UserPassword.Equals(usr.UserPassword));
           
            if (up.Count() == 1)
            {

                ViewBag.message = "Login Sucess";
                ViewBag.Userid = ui.ToList().FirstOrDefault();
                TempData["userid"] = ui.ToList().FirstOrDefault();
                //HttpContext.Session.Set("SessionKeyUserid", ViewBag.Userid);
                HttpContext.Session.SetString("userid", ui.ToList().FirstOrDefault().ToString());                

                //return View("~/Views/Emp/Index.cshtml");
                //return View("../Emp/Index");
                return RedirectToAction("Index", "Subscription");
                //return View("/Views/LoginEmp/IndexViews/Emp/Index");

            }
            else
            {
                ViewBag.message = "Login Failed";
                return View("Login");
            }
        }
    }
}
