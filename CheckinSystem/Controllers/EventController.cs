using CheckinSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheckinSystem.Controllers
{
    public class EventController : Controller
    {
        private CheckinSystemContext db = new CheckinSystemContext();

        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexDatalist(string searchString)
        {
            var MaxOfDay = from x in db.Events
                           group x by new { x.EmployeeID, x.Date } into g
                           select new
                           {
                               g.Key.EmployeeID,
                               g.Key.Date,
                               TimeOfDay = g.Max(x => x.TimeOfDay)
                           };
            var MinOfDay = from x in db.Events
                           group x by new { x.EmployeeID, x.Date } into g
                           select new
                           {
                               g.Key.EmployeeID,
                               g.Key.Date,
                               TimeOfDay = g.Min(x => x.TimeOfDay)
                           };

            var ProcessedEvent = MinOfDay.Union(MaxOfDay);
            if (!String.IsNullOrEmpty(searchString))
            {
                var EmployeeList = db.Employees.Where(s => s.Name.Contains(searchString));

                var res = from m in ProcessedEvent
                          join e in EmployeeList on m.EmployeeID equals e.ID
                          orderby e.ID
                          select new dto { Name = e.Name, Position = e.Position, Date = m.Date, TimeOfDay = m.TimeOfDay };
                var list = res.ToList();
                return View(list);
            }
            else
            {
                var res = from m in ProcessedEvent
                          join e in db.Employees on m.EmployeeID equals e.ID
                          orderby e.ID
                          select new dto { Name = e.Name, Position = e.Position, Date = m.Date, TimeOfDay = m.TimeOfDay };
                var list = res.ToList();
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult PostRequest(Event events)
        {
            int checkinid = events.EmployeeID;
            var pass = db.Employees.Any(x => x.ID == checkinid);
            if (pass == false)
            {
                return Json(new { Msg = "无效ID" });
            }


            if (ModelState.IsValid)
            {
                db.Events.Add(events);
                db.SaveChanges();
                return Content("Add Success");
            }
            return RedirectToAction("Index");
        }
    }
}