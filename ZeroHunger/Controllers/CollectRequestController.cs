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
    public class CollectRequestController : Controller
    {
        private readonly Zero_HungerEntities4 DB; 

        public CollectRequestController()
        {
            DB = new Zero_HungerEntities4();
        }

        public ActionResult Index()
        {
            var collectRequests = DB.CollectRequests.ToList();
            return View(collectRequests);
        }

        [HttpGet]
        public ActionResult OpenCollectRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OpenCollectRequest(OpenCollectRequestDTO openCollectRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(openCollectRequest);
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<OpenCollectRequestDTO, CollectRequest>());
            var mapper = new Mapper(config);
            var collectRequest = mapper.Map<CollectRequest>(openCollectRequest);

            collectRequest.Status = "Requested";

            DB.CollectRequests.Add(collectRequest);
            DB.SaveChanges();

            return RedirectToAction("Index");
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
            foreach (var key in ModelState.Keys)
            {
                var modelState = ModelState[key];
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine($"Error in {key}: {error.ErrorMessage}");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var collectRequest = DB.CollectRequests.Find(id);

            if (collectRequest == null)
            {
                return HttpNotFound();
            }
            var foodItemsToRemove = DB.FoodItems.Where(fi => fi.RequestID == id);
            DB.FoodItems.RemoveRange(foodItemsToRemove);
            DB.CollectRequests.Remove(collectRequest);
            DB.SaveChanges();
            return RedirectToAction("Index");
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