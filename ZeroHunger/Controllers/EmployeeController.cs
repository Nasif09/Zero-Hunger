using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Zero_HungerEntities3 DB;

        public EmployeeController()
        {
            DB = new Zero_HungerEntities3();
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