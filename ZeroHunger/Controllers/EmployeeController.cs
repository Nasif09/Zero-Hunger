using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DTOs;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Zero_HungerEntities4 DB;

        public EmployeeController()
        {
            DB = new Zero_HungerEntities4();
        }

        public ActionResult AssignedCollectRequests(int userId)
        {
            var user = Session["user"] as Registration;

            if (user == null || user.Role != "Employee" || user.UserId != userId)
            {
                return RedirectToAction("Login", "Home"); 
            }
            var assignedRequests = DB.CollectRequests
                .Where(c => c.EID == user.UserId)
                .ToList();
            return View(assignedRequests);
        }

        public ActionResult AcceptCollectRequest(int id, int employeeId)
        {
            var collectRequest = DB.CollectRequests.Find(id);

            if (collectRequest == null)
            {
                return HttpNotFound();
            }
            collectRequest.EID = employeeId;
            collectRequest.Status = "Collected";

            DB.SaveChanges();
            ViewBag.UserId = collectRequest.EID;
            return RedirectToAction("AssignedCollectRequests", new { userId = collectRequest.EID });
        }
        public ActionResult DistributedMessage(int id, int employeeId)
        {
            var collectRequest = DB.CollectRequests.Find(id);

            if (collectRequest == null)
            {
                return HttpNotFound();
            }
            collectRequest.EID = employeeId;
            collectRequest.Status = "Distributed";

            DB.SaveChanges();
            ViewBag.UserId = collectRequest.EID;
            return RedirectToAction("AssignedCollectRequests", new { userId = collectRequest.EID });
        }


        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Registration signup)
        {
            if (ModelState.IsValid)
            {

                if (signup.Role == "Employee")
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

                    DB.Registrations.Add(user);
                    DB.SaveChanges();

                    Employee employee = new Employee()
                    {
                        EmployeeName = signup.Username,
                        Contact = signup.Contact
                    };

                    DB.Employees.Add(employee);
                    DB.SaveChanges();
                }

                return RedirectToAction("ViewEmployees");
            }

            return View(signup);
        }

        [HttpGet]
        public ActionResult ViewEmployees()
        {
            var employees = DB.Employees.ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeDTO>(); });
            var mapper = new Mapper(config);
            var employeeDTO = employees.Select(r => mapper.Map<EmployeeDTO>(r)).ToList();

            return View(employeeDTO);
        }
        public ActionResult Details(int id)
        {
            var collectRequest = DB.CollectRequests.Include("FoodItems").FirstOrDefault(c => c.RequestID == id);

            if (collectRequest == null)
            {
                return HttpNotFound();
            }

            return View(collectRequest);
        }
    }
}