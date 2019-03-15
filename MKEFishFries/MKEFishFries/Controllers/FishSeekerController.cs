using Microsoft.AspNet.Identity;
using MKEFishFries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MKEFishFries.Controllers
{
    public class FishSeekerController : Controller
    {
        ApplicationDbContext db;
        public FishSeekerController()
        {
            db = new ApplicationDbContext();
        }
        // GET: FishSeeker
        public ActionResult Index()
        {
            try
            {
                var user = User.Identity.GetUserId();
                string userFirstName = db.Peoples.Where(c => c.ApplicationUserId == user).Select(c => c.FirstName).Single();
                ViewBag.Name = userFirstName;
                return View();
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
            var userId = User.Identity.GetUserId();
            var user = db.Peoples.Where(c => c.ApplicationUserId == userId).Single();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Peoples.Add(person);
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FishSeeker/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FishSeeker/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, People person)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Peoples.Where(c => c.ApplicationUserId == userId).Single();
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

        // GET: FishSeeker/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FishSeeker/Delete/5
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
    }
}
