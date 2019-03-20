using Microsoft.AspNet.Identity;
using MKEFishFries.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
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
        // GET
        public ActionResult VisitorActions(int id)
        {
            // Custcust1!@gmail.com
            VisitorActionsViewModel visitorActionsViewModel = new VisitorActionsViewModel()
            {
                Parishes = new Parish(),
                ContactList = new ContactList(),
                Donations = new Donation()
            };
            ViewBag.ParishID = id;
            return View(visitorActionsViewModel);
        }
        [HttpPost]
        public ActionResult VisitorActions(VisitorActionsViewModel visitorActionsViewModel, string SignUp, string Comment)
        {
            Parish thisParish = new Parish();
            People thisPerson = new People();
            // Custcust1!@gmail.com
            // Comment:  if the Parish is Receiving comments, prepend to the Comments field
            if (Comment != "")
            {
                // TODO - add the HiddenFor to the control based on thisParish.RecieveComments
                thisParish = db.Parishes.Where(p => p.ID == visitorActionsViewModel.Parishes.ID).First();
                if (thisParish.RecieveComments)
                {
                    // Get the sender's first name:
                    string thisUserID = User.Identity.GetUserId();
                    thisPerson = db.Peoples.Where(w => w.ApplicationUserId == thisUserID).First();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("=========================\n");
                    stringBuilder.Append(thisPerson.FirstName);
                    stringBuilder.Append(" ");
                    stringBuilder.Append(thisPerson.LastName);
                    stringBuilder.Append(" says:\n");
                    stringBuilder.Append(Comment);
                    stringBuilder.Append("\n");
                    stringBuilder.Append(thisParish.Comments ?? "");
                    //stringBuilder.Append(asdf);
                    //thisParish.Comments =
                    //    "=========================\n" +
                    //    thisPerson.FirstName + " " + thisPerson.LastName + " says:\n" +
                    //    Comment + "\n" +
                    //    thisParish.Comments ?? "";
                    thisParish.Comments = stringBuilder.ToString();
                    db.SaveChanges();
                }
            }
            if (SignUp.ToUpper() == "Y")
            {
                // search the ContactList table for a record with the parishID & user's ID;  
                // if the record is not there, add it

                // if the parish or the user is null, get them 
                if (thisParish.ID == 0)
                {
                    thisParish = db.Parishes.Where(p => p.ID == visitorActionsViewModel.Parishes.ID).First();
                }
                if (thisPerson.ID == 0)
                {
                    string thisUserID = User.Identity.GetUserId();
                    thisPerson = db.Peoples.Where(w => w.ApplicationUserId == thisUserID).First();
                }
                // Search 
                int count = db.ContactLists.Where(c => c.ParishId == thisParish.ID && c.PeopleId == thisPerson.ID).Count();
                if (count == 0)
                {
                    ContactList contactList = new ContactList();
                    contactList.ParishId = thisParish.ID;
                    contactList.PeopleId = thisPerson.ID;
                    db.ContactLists.Add(contactList);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        ////public ActionResult VisitorActions(string stripeToken, string DollarAmount)
        //public ActionResult VisitorActions(string stripeToken)
        //{
        //    StripeConfiguration.SetApiKey("sk_test_mdDGBM56VRabusYFI96kpuGh00PrprigoK");

        //    var options = new ChargeCreateOptions
        //    {
        //        Amount = 2500,
        //        //Amount = int.Parse( DollarAmount + "00"),
        //        Currency = "usd",
        //        Description = "Charge for jenny.rosen@example.com",
        //        SourceId = "tok_mastercard" // obtained with Stripe.js,

        //    };
        //    var service = new ChargeService();
        //    Charge charge = service.Create(options);
        //    //var model = new ChargeViewModel();
        //    //model.ChargeId = charge.Id;
        //    //return View("VisitorActions", model);
        //    return View();
        //}
        [HttpPost]
        public ActionResult VisitorActionsPayment(string stripeToken, string SignUp, string Comment)
        {
            StripeConfiguration.SetApiKey("sk_test_mdDGBM56VRabusYFI96kpuGh00PrprigoK");

            var options = new ChargeCreateOptions
            {
                Amount = 2500,
                Currency = "usd",
                Description = "Charge for jenny.rosen@example.com",
                SourceId = stripeToken // obtained with Stripe.js,

            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            var model = new ChargeViewModel();
            model.ChargeId = charge.Id;
            return View("DonationConfirmation", model);
            //return View();
        }

        // GET: FishSeeker
        public ActionResult Index()
        {
            var parishes = db.Parishes;
            return View(parishes.ToList());
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


//﻿using Microsoft.AspNet.Identity;
//using MKEFishFries.Models;
//using Stripe;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;

//namespace MKEFishFries.Controllers
//{
//    public class VisitorController : Controller
//    {
//        ApplicationDbContext db;
//        public VisitorController()
//        {
//            db = new ApplicationDbContext();
//        }
//        // GET
//        public ActionResult VisitorActions(int id)
//        {
//            // Custcust1!@gmail.com
//            VisitorActionsViewModel visitorActionsViewModel = new VisitorActionsViewModel()
//            {
//                Parishes = new Parish(),
//                ContactList = new ContactList(),
//                Donations = new Donation()
//            };
//            ViewBag.ParishID = id;
//            return View(visitorActionsViewModel);
//        }
//        [HttpPost]
//        public ActionResult VisitorActions(VisitorActionsViewModel visitorActionsViewModel, string SignUp, string Comment)
//        {
//            Parish thisParish = new Parish();
//            People thisPerson = new People();
//            // Custcust1!@gmail.com
//            // Comment:  if the Parish is Receiving comments, prepend to the Comments field
//            if (Comment != "")
//            {
//                thisParish = db.Parishes.Where(p => p.ID == visitorActionsViewModel.Parishes.ID).First();
//                if (thisParish.RecieveComments)
//                {
//                    // Get the sender's first name:
//                    string thisUserID = User.Identity.GetUserId();
//                    thisPerson = db.Peoples.Where(w => w.ApplicationUserId == thisUserID).First();
//                    thisParish.Comments =
//                        "=========================\n" +
//                        thisPerson.FirstName + " " + thisPerson.LastName + " says:\n" +
//                        Comment + "\n" +
//                        thisParish.Comments ?? "";
//                    db.SaveChanges();
//                }
//            }
//            if (SignUp.ToUpper() == "Y")
//            {
//                // search the ContactList table for a record with the parishID & user's ID;  
//                // if the record is not there, add it

//                // if the parish or the user is null, get them 
//                if (thisParish.ID == 0)
//                {
//                    thisParish = db.Parishes.Where(p => p.ID == visitorActionsViewModel.Parishes.ID).First();
//                }
//                if (thisPerson.ID == 0)
//                {
//                    string thisUserID = User.Identity.GetUserId();
//                    thisPerson = db.Peoples.Where(w => w.ApplicationUserId == thisUserID).First();
//                }
//                // Search 
//                int count = db.ContactLists.Where(c => c.ParishId == thisParish.ID && c.PeopleId == thisPerson.ID).Count();
//                if (count == 0)
//                {
//                    ContactList contactList = new ContactList();
//                    contactList.ParishId = thisParish.ID;
//                    contactList.PeopleId = thisPerson.ID;
//                    db.ContactLists.Add(contactList);
//                    db.SaveChanges();
//                }
//            }
//            return RedirectToAction("Index");
//        }
        
//        [HttpPost]
//        public ActionResult VisitorActionsPayment(string stripeToken, string SignUp, string Comment)
//        {
//            StripeConfiguration.SetApiKey("sk_test_mdDGBM56VRabusYFI96kpuGh00PrprigoK");

//            var options = new ChargeCreateOptions
//            {
//                Amount = 2500,
//                Currency = "usd",
//                Description = "Charge for jenny.rosen@example.com",
//                SourceId = stripeToken // obtained with Stripe.js,
               
//            };
//            var service = new ChargeService();
//            Charge charge = service.Create(options);
//            var model = new ChargeViewModel();
//            model.ChargeId = charge.Id;
//            return View("DonationConfirmation", model);
//            //return View();
//        }

        

//        // GET: FishSeeker
//        public ActionResult Index()
//        {
//            var parishes = db.Parishes;
//            return View(parishes.ToList());
//        }
//        // GET: FishSeeker/Details/5
//        public ActionResult Details(int id)
//        {
//            try
//            {
//                string userId = User.Identity.GetUserId();
//                var user = db.Peoples.Where(c => c.ApplicationUserId == userId).Single();
//                return View(user);
//            }
//            catch
//            {
//                return RedirectToAction("Index");
//            }
//        }

//        // GET: FishSeeker/Create
//        public ActionResult Create()
//        {
//            ViewBag.ID = new SelectList(db.Peoples, "Id", "Name");
//            return View();
//        }

//        // POST: FishSeeker/Create
//        [HttpPost]
//        public ActionResult Create(People person)
//        {
//            //Creating a Visitor
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    person.ApplicationUserId = User.Identity.GetUserId();
//                    db.Peoples.Add(person);
//                    db.SaveChanges();
//                }
//                    return RedirectToAction("Index","Home");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: FishSeeker/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            return View();
//        }

//        // POST: FishSeeker/Edit/5
//        [HttpPost]
//        public ActionResult Edit(People person)
//        {
         
//            var userId = User.Identity.GetUserId();
//            var user = db.Peoples.Where(c => c.ApplicationUserId == userId).Single();
//            try
//            {
//                db.Entry(person).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: FishSeeker/Delete/5
//        public ActionResult Delete(int id)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    People person = db.Peoples.Find(id);
//                    db.Peoples.Remove(person);
//                    db.SaveChanges();
//                }
//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}

