using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MKEFishFries.Models;

namespace MKEFishFries.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult Index()
        {
            // Stjoeadmin1!@abc.com
            Parish thisParish;
            var events = db.Events.Include(e => e.People);
            string thisUserID = User.Identity.GetUserId();
            string userRole = db.Users.Where(u => u.Id == thisUserID).First().UserRole;
            People thisPerson = db.Peoples.Where(w => w.ApplicationUserId == thisUserID).First();
            
            // If the user is a ParishAdmin, filter the view for that parish only
            if (userRole == "ParishAdmin" )
            {
                thisParish = db.Parishes.Where(w => w.AdminPersonId == thisPerson.ID).First();
                ViewBag.ParishId = thisParish.ID;
                ViewBag.ParishName = thisParish.Name;
                events = events.Where(e => e.ParishId == thisParish.ID).Include(e => e.People);
            }
            else
            {
                ViewBag.ParishId = 0;
                ViewBag.ParishName = "";
            }
            ViewBag.FirstName = thisPerson.FirstName;
            ViewBag.LastName = thisPerson.LastName;
            return View(events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                RedirectToAction("Index", "Search");
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            // Stjoeadmin1!@abc.com
            string thisUserID = User.Identity.GetUserId();
            People thisPerson = db.Peoples.Where(w => w.ApplicationUserId == thisUserID).First();
            Parish thisParish = db.Parishes.Where(w => w.AdminPersonId == thisPerson.ID).First();
            ViewBag.ParishId = thisParish.ID;

            ViewBag.SponserPersonId = new SelectList(db.Peoples, "ID", "FirstName");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ParishId,Date,EventName,EventDescription,StartTime,EndTime,Price,FoodDescription,CarryOutOption,SponserPersonId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.ParishId = 1;
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SponserPersonId = new SelectList(db.Peoples, "ID", "FirstName", @event.SponserPersonId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.SponserPersonId = new SelectList(db.Peoples, "ID", "FirstName", @event.SponserPersonId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParishId,Date,EventName,EventDescription,StartTime,EndTime,Price,FoodDescription,CarryOutOption,SponserPersonId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SponserPersonId = new SelectList(db.Peoples, "ID", "FirstName", @event.SponserPersonId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
