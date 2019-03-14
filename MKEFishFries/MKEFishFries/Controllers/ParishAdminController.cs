using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MKEFishFries.Controllers
{
    public class ParishAdminController : Controller
    {
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
    }
}
