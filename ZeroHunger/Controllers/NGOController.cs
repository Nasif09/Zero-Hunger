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
        private readonly Zero_HungerEntities3 DB;

        public NGOController()
        {
            DB = new Zero_HungerEntities3();
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