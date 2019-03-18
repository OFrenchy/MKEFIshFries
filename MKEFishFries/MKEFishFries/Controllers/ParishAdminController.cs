using Microsoft.AspNet.Identity;
using MKEFishFries.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
            People thisPerson = db.Peoples.Where(w => w.ApplicationUserId == thisUserID).First();
            Parish thisParish = db.Parishes.Where(w => w.AdminPersonId == thisPerson.ID).First();
            ViewBag.FirstName = thisPerson.FirstName;
            ViewBag.LastName = thisPerson.LastName;
            ViewBag.ParishId = thisParish.ID;
            ViewBag.ParishName = thisParish.Name;
            // Redirect to EventsController!!!
            return RedirectToAction("Index", "Events");
        }

        // GET: ParishAdmin/Details/5
        public ActionResult Details(int id)
        {
            People people = db.Peoples.Find(id);
            if (people == null) return HttpNotFound();
            
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
        public Parish SetLatLong(Parish parish)
        {
            // This is the geoDecoderRing 
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(parish.Street1.Replace(" ", "+"));
                stringBuilder.Append(";");
                stringBuilder.Append(parish.City.Replace(" ", "+"));
                stringBuilder.Append(";");
                stringBuilder.Append(parish.State.Replace(" ", "+"));
                // example: string url = @"https://maps.googleapis.com/maps/api/geocode/json?address={stringBuilder.ToString()}1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=YOUR_API_KEY";
                string url = @"https://maps.googleapis.com/maps/api/geocode/json?address=" +
                    stringBuilder.ToString() + "&key=" + Models.Access.apiKey;

                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                System.IO.Stream data = response.GetResponseStream();
                StreamReader reader = new StreamReader(data);
                // json-formatted string from maps api
                string responseFromServer = reader.ReadToEnd();
                response.Close();

                var root = JsonConvert.DeserializeObject<ParishMapAPIData>(responseFromServer);
                var location = root.results[0].geometry.location;
                //var latitude = location.lat;
                //var longitude = location.lng;
                ////foreach (var singleResult in root.results)
                ////{
                ////    var location = singleResult.geometry.location;
                ////    var latitude = location.lat;
                ////    var longitude = location.lng;
                ////    // Do whatever you want with them.
                ////}
                parish.Lat = location.lat;
                parish.Long = location.lng;
                return parish;
            }
            catch
            {
                return null;
            }
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
                // set lat & long from geoDecoderRing
                parish = SetLatLong(parish);
                // the user that is logged in is the AdminPersonID
                var appUserID = User.Identity.GetUserId();
                var personID = db.Peoples.Where(w => w.ApplicationUserId == appUserID).FirstOrDefault().ID;
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
        public ActionResult EditParish(int id)
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
                thisParish.RecieveComments = parish.RecieveComments;
                // if lat & long is 0, fill them in
                if (thisParish.Lat == 0)
                {
                    thisParish = SetLatLong(thisParish);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Events");
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
            Parish thisParish;
            string thisUserID = User.Identity.GetUserId();
                People thisPerson = db.Peoples.Where(w => w.ApplicationUserId == thisUserID).First();
            ViewBag.FirstName = thisPerson.FirstName;
            ViewBag.LastName = thisPerson.LastName;
            thisParish = id == null ? db.Parishes.Where(w => w.AdminPersonId == thisPerson.ID).First() : db.Parishes.Find(id);
            if (thisParish == null) return HttpNotFound();
            ViewBag.ParishName = thisParish.Name;
            ViewBag.ParishId = thisParish.ID;
            return View(thisParish);
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

    // /TODO - move somewhere else - where?

    public class ParishMapAPIData
    {
        public Result[] results { get; set; }
        public string status { get; set; }
    }

    public class Result
    {
        public Address_Components[] address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Northeast
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Southwest
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Address_Components
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }




}

