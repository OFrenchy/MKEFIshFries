using Microsoft.AspNet.Identity;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
using MKEFishFries.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mail;
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

        //// Post: ParishAdmin/DetailsParish/5
        //[HttpPost]
        //public ActionResult DetailsParish(int id, int parishId, string Subject, string MessageBody)  // to send emails:
        //{

        //    // TODO - Add a "Send Mail" link to DetailsParish.cshtml

        //    // TODO - add "Subject" text box & "MessageBody" text box to DetailsParish.cshtml, 
        //    // pass their values to the parameters of this method, reference them in the .Subject & .Body below 

        //    // Query the db ContactsList for peopleToContact
        //    // TODO - need to include the table with the emails, & first name;  
        //    // might need to include more than 1 to get to the table with email addresses
        //    var peopleToContact = db.ContactLists.Where(c => c.ParishId == parishId && c.PeopleId == id).ToList();

        //    // if no records, or if Subject & MessageBody are "", return 

        //    using (var client = new MailKit.Net.Smtp.SmtpClient())
        //    {
        //        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        //        // TODO - determine whose mail account we're going to use, plug in the details below
        //        client.Connect("smtp.friends.com", 587, false);
        //        client.Authenticate("joey", "password");
        //        foreach (var thisRecord in peopleToContact)
        //        {
        //            var message = new MimeMessage();
        //            message.From.Add(new MailboxAddress("", ""));  // TODO - determine whose mail account we're going to use
        //            message.To.Add(new MailboxAddress("", ""));
        //            message.Subject = "";  //"Subject" text box 

        //            message.Body = new TextPart("plain")
        //            {
        //                Text = @""  //"MessageBody" text box ; replace "Dear <FirstName>," with "Dear Jack," from the user's FirstName field
        //            };
        //            client.Send(message);
        //        }
        //        client.Disconnect(true);
        //    }
        //    // TODO - return where?  Redirect?
        //    return View();
        //}
        //// GET: ParishAdmin/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

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
        public async Task<Parish> SetLatLong(Parish parish)
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

                // httpclient

                WebRequest request = WebRequest.Create(url);
                WebResponse response = await request.GetResponseAsync();
                System.IO.Stream data = response.GetResponseStream();
                // tried this System.IO.Stream data = await GetGoogleGeocodeResponse(url);
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
        public async Task<ActionResult> CreateParish(Parish parish)
        {
            try
            {
                // set lat & long from geoDecoderRing
                parish = await SetLatLong(parish);
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
        public async Task<ActionResult> EditParish(int id, FormCollection collection, Parish parish)
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
                    thisParish = await SetLatLong(thisParish);
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

        public void SendMail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", ""));
            message.To.Add(new MailboxAddress("", ""));
            message.Subject = "";

            message.Body = new TextPart("plain")
            {
                Text = @""
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.friends.com", 587, false);

                client.Authenticate("joey", "password");

                client.Send(message);
                client.Disconnect(true);
            }
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

