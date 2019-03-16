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
    //Stjoeadmin1!@abc.com


    public class ParishAdminController : Controller
    {
        ApplicationDbContext db;
        public ParishAdminController()
        {
            db = new ApplicationDbContext();
        }
        // GET: ParishAdmin
        public ActionResult Index()
        {
            // Stjoeadmin1!@abc.com

            string thisUserID = User.Identity.GetUserId();
            //People people = db.Peoples.Where(p => p.ApplicationUserId == user).Single();

            People thisPerson = db.Peoples.Where(w => w.ApplicationUserId == thisUserID).First();
            Parish thisParish = db.Parishes.Where(w => w.AdminPersonId == thisPerson.ID).First();
            ViewBag.FirstName = thisPerson.FirstName;
            ViewBag.LastName = thisPerson.LastName;
            ViewBag.ParishId = thisParish.ID;
            ViewBag.ParishName = thisParish.Name;

            // If there are no events for this parish, should we, instead of showing the Index view,
            // go right to Add an event?  Redirect to 

            if (db.Events.Where(w => w.ParishId == thisParish.ID).Count() == 0)
            {
                // if there are no events for this church yet, create an empty list instead of a null collection
                return View(new List<Event>());
                //return RedirectToAction("Create", "Events");
            }
            return View();
        }

        // GET: ParishAdmin/Details/5
        public ActionResult Details(int id)
        {

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            People people = db.Peoples.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            
            // find out if this person is "attached" to a church
            int? churchID =   db.Parishes.Where(w => w.AdminPersonId == id).Select(s => s.ID).First();
            // if null, ?? TODO - enable the 'claim a parish' & 'add a parish' function
            if (churchID == null)
            {

            } 





           
            return View();
        }

        // GET: ParishAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParishAdmin/Create
        [HttpPost]
        public ActionResult Create(People people)
        {
            if (ModelState.IsValid)
            {
                people.ApplicationUserId = User.Identity.GetUserId();
                db.Peoples.Add(people);
                db.SaveChanges();
                // TODO - fix this, there's no model for Index
                //return RedirectToAction("Index");
                //return RedirectToAction("Details",);
                
                return RedirectToAction("CreateParish", "ParishAdmin");

                //return RedirectToAction("Details", new { id = people.ID });
            }

            return View(people);
        }
    

        // GET: ParishAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            People people = db.Peoples.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            return View(people);
        }

        // POST: ParishAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(People people)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                db.Entry(people).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(people);
        }    

        // GET: ParishAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            People people = db.Peoples.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            return View(people);
        }

        // POST: ParishAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            People people = db.Peoples.Find(id);
            db.Peoples.Remove(people);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ParishParish/CREATE
        public ActionResult CreateParish()
        {
            return View();
        }

        //POST: ParishParish/CREATE
        [HttpPost]
        public ActionResult CreateParish(Parish parish)
        {
            try
            {
                // TODO - add the Google Maps latitude & longitude lookup, add that info to the fields
                // parish.Street1
                // parish.Street2
                // parish.City
                // parish.State
                // parish.Zip
                // GoogleMapsAPIGetLatAndLongFromAddress(parish.Street1, parish.City, parish.State);

                parish.Lat = 1;
                parish.Long = 1;
                var appUserID = User.Identity.GetUserId();
                var personID = db.Peoples.Where(w => w.ApplicationUserId == appUserID).FirstOrDefault().ID  ;
                parish.AdminPersonId = personID;
                db.Parishes.Add(parish);
                db.SaveChanges();
                return RedirectToAction("Index", "ParishAdmin");
            }
            catch
            {
                return View();
            }
        }

        // GET: ParishProfile/Edit
        public ActionResult EditProfile(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Parish parish = db.Parishes.Find(id);
            if (parish == null)
            {
                return HttpNotFound();
            }
            return View(parish);
        }

        // POST: ParishParish/Edit
        [HttpPost]
        public ActionResult EditParish(int id, FormCollection collection, Parish parish)
        {
            try
            {
                Parish thisParish = db.Parishes.Find(id);
                thisParish.Name = parish.Name;
                thisParish.Street1 = parish.Street1;
                thisParish.Street2 = parish.Street2;
                thisParish.City = parish.City;
                thisParish.State = parish.State;
                thisParish.Zip = parish.Zip;
                thisParish.WebsiteURL = parish.WebsiteURL;
                thisParish.Phone = parish.Phone;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(parish);
            }
        }

        // GET: ParishParish/Delete
        public ActionResult DeleteParish(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parish parish = db.Parishes.Find(id);
            if (parish == null)
            {
                return HttpNotFound();
            }
            return View(parish);
        }

        // POST: ParishParish/Delete
        [HttpPost]
        public ActionResult DeleteParish(int id)
        {
            Parish parish = db.Parishes.Find(id);
            db.Parishes.Remove(parish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DetailsParish(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parish parish = db.Parishes.Find(id);
            if (parish == null)
            {
                return HttpNotFound();
            }
            return View(parish);
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

