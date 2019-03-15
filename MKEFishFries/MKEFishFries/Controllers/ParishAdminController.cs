using MKEFishFries.Models;
using System;
using System.Collections.Generic;
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
        // GET: ParishAdmin
        public ActionResult Index()
        {
            return View();
        }

        // GET: ParishAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ParishAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParishAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ParishAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ParishAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ParishAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ParishAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ParishProfile/CREATE
        public ActionResult CreateProfile()
        {
            return View();
        }

        //POST: ParishProfile/CREATE
        [HttpPost]
        public ActionResult CreateProfile(Parish parish)
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

        // GET: ParishProfile/Edit
        public ActionResult EditProfile(int id)
        {
            List<Parish> ListofParishes = db.Parishes.ToList();
            return View();
        }

        // POST: ParishProfile/Edit
        [HttpPost]
        public ActionResult EditProfile(int id, FormCollection collection, Parish parish)
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
                return View();
            }
        }

        // GET: ParishProfile/Delete
        public ActionResult DeleteProfile(int? id)
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

        // POST: ParishProfile/Delete
        [HttpPost]
        public ActionResult DeleteProfile(int id)
        {
            try
            {
                db.Parishes.Remove(db.Parishes.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

