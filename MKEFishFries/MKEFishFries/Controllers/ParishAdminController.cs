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
            var user = User.Identity.GetUserId();
            People people = db.Peoples.Where(p => p.ApplicationUserId == user).Single();
            return View();
        }

        // GET: ParishAdmin/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.Peoples.Find(id);
            if (people == null)
            {
                return HttpNotFound();
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
                return RedirectToAction("Index");
            }

            return View(people);
        }
    

        // GET: ParishAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
                db.Parishes.Add(parish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ParishParish/Edit
        public ActionResult EditParish(int? id)
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

