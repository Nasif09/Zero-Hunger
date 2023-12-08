using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ZeroHunger.DTOs;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class HomeController : Controller
    {
        private Zero_HungerEntities4 db = new Zero_HungerEntities4(); 

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDTO login)
        {
            var user = (from usr in db.Registrations
                        where usr.Username.Equals(login.Username) && usr.Password.Equals(login.Password)
                        select usr).SingleOrDefault();

            if (user != null)
            {
                Session["user"] = user;
                var returnUrl = Request["ReturnUrl"];

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else if (user.Role.Equals("Donner"))
                {
                    return RedirectToAction("Index", "CollectRequest");
                }
                else if (user.Role.Equals("Employee"))
                {
                    return RedirectToAction("AssignedCollectRequests", "Employee", new { userId = user.UserId });
                }
                else if (user.Role.Equals("NGO"))
                {
                    return RedirectToAction("ViewCollectRequests", "NGO");
                }
            }
            return View(login);
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Registration signup)
        {
            if (ModelState.IsValid)
            {
                Registration user = new Registration()
                {
                    Username = signup.Username,
                    Contact = signup.Contact,
                    Organization = signup.Organization,
                    Address = signup.Address,
                    Password = signup.Password,
                    Role = signup.Role
                };

                db.Registrations.Add(user);
                db.SaveChanges();

                if (signup.Role == "Employee")
                {
                    Employee employee = new Employee()
                    {
                        EmployeeName = signup.Username,
                        Contact = signup.Contact
                    };

                    db.Employees.Add(employee);
                    db.SaveChanges();
                }

                return RedirectToAction("Login");
            }

            return View(signup);
        }


        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult DonorDashboard()
        {
            return View();
        }

        public ActionResult EmployeeDashboard()
        {
            return View();
        }
    }
}