using Microsoft.AspNet.Identity;
using MKEFishFries.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MKEFishFries.Controllers
{
    public class VisitorController : Controller
    {
        ApplicationDbContext db;
        public VisitorController()
        {
            db = new ApplicationDbContext();
        }
        // GET: FishSeeker
        public ActionResult Index()
        {

            try
            {
                string thisUserID = User.Identity.GetUserId();
                People thisPerson = db.Peoples.Where(w => w.ApplicationUserId == thisUserID).First();
                ViewBag.Name = thisPerson.FirstName + " " + thisPerson.LastName;
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: FishSeeker/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                var user = db.Peoples.Where(c => c.ApplicationUserId == userId).Single();
                return View(user);
            }
            catch
            {
                return RedirectToAction("Index");

            }
        }

        // GET: FishSeeker/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Peoples, "Id", "Name");
            return View();
        }

        // POST: FishSeeker/Create
        [HttpPost]
        public ActionResult Create(People person)
        {
            //Creating a Visitor
            try
            {
                if (ModelState.IsValid)
                {
                    person.ApplicationUserId = User.Identity.GetUserId();
                    db.Peoples.Add(person);
                    db.SaveChanges();
                }
                    return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: FishSeeker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: FishSeeker/Edit/5
        [HttpPost]
        public ActionResult Edit(People person)
        {
         
            var userId = User.Identity.GetUserId();
            var user = db.Peoples.Where(c => c.ApplicationUserId == userId).Single();
            try
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FishSeeker/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    People person = db.Peoples.Find(id);
                    db.Peoples.Remove(person);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
