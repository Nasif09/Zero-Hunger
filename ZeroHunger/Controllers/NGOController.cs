using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DTOs;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class NGOController : Controller
    {
        private readonly Zero_HungerEntities4 DB;

        public NGOController()
        {
            DB = new Zero_HungerEntities4();
        }
        public ActionResult ViewCollectRequests()
        {
            var collectRequests = DB.CollectRequests.ToList();
            return View(collectRequests);
        }

        public ActionResult CollectRequestDetails(int id)
        {
            var collectRequest = DB.CollectRequests.Find(id);

            if (collectRequest == null)
            {
                return HttpNotFound();
            }

            return View(collectRequest);
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
        public ActionResult AssignEmployee(int id)
        {
            // Retrieve the collect request based on the provided id
            var collectRequest = DB.CollectRequests.Find(id);

            if (collectRequest == null)
            {
                return HttpNotFound();
            }

            // Retrieve the list of all employees
            var employees = DB.Employees.Select(e => new EmployeeDTO
            {
                EID = e.EID,
                EmployeeName = e.EmployeeName
                // Add other properties as needed
            }).ToList();

            // Create an AssignEmployeeDTO to pass both the collect request and the list of employees to the view
            var assignEmployeeDTO = new AssignEmployeeDTO
            {
                CollectRequestID = collectRequest.RequestID,
                AssignedEmployeeID = collectRequest.AssignedEmployeeID,
                Employees = employees
            };

            return View(assignEmployeeDTO);
        }

        [HttpPost]
        public ActionResult AssignEmployee(AssignEmployeeDTO assignEmployeeDTO)
        {
            // Retrieve the collect request based on the provided id
            var collectRequest = DB.CollectRequests.Find(assignEmployeeDTO.CollectRequestID);

            if (collectRequest == null)
            {
                return HttpNotFound();
            }

            // Update the assigned employee ID in the collect request
            collectRequest.AssignedEmployeeID = assignEmployeeDTO.AssignedEmployeeID;

            // Save changes to the database
            DB.SaveChanges();

            // Redirect to the ViewCollectRequests action or another appropriate action
            return RedirectToAction("ViewCollectRequests");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var collectRequest = DB.CollectRequests.Find(id);

            if (collectRequest == null)
            {
                return HttpNotFound();
            }
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CollectRequest, CollectRequestDTO>(); });
            var mapper = new Mapper(config);
            var data = mapper.Map<CollectRequestDTO>(collectRequest);

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(CollectRequestDTO collectRequest)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CollectRequestDTO, CollectRequest>(); });
            var mapper = new Mapper(config);
            var data = mapper.Map<CollectRequest>(collectRequest);

            if (!ModelState.IsValid)
            {
                return View(collectRequest);
            }
            DB.Entry(data).State = EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("ViewCollectRequests"); 
        }

        [HttpGet]
        public ActionResult ViewRestaurants()
        {
            var restaurants = DB.Restaurants.ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Restaurant, RestaurantDTO>(); });
            var mapper = new Mapper(config);
            var restaurantsDTO = restaurants.Select(r => mapper.Map<RestaurantDTO>(r)).ToList();

            return View(restaurantsDTO);
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}