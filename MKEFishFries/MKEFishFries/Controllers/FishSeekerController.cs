﻿using Microsoft.AspNet.Identity;
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
            return View();
        }

        // GET: FishSeeker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FishSeeker/Create
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

        // GET: FishSeeker/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FishSeeker/Edit/5
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
