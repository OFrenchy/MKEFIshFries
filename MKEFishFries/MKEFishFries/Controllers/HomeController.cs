using MKEFishFries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MKEFishFries.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        [HttpGet]
        public ActionResult Index()
        {
            var tempLatitudes = new List<double>();
            var tempLongitudes = new List<double>();
            var names = new List<string>();
            
            // Get all parish addresses in the Parishes model/table

            //ViewBag.Key = Models.Access.apiKey;

            // TODO - change this query to show parishes with fish fry events in the next 7 days, 
            //  so that a Friday is included. 
            List<int> events = db.Events.Select(e=>e.ParishId).ToList();
            List<Parish> parishes = db.Parishes.ToList();
            List<Parish> parishesWithEvents = new List<Parish>();
            var listOfParishEvents = db.Parishes.Select(p => p.listOfEvents).ToList();
            List<Event> specificEvents = new List<Event>();
            List<int?> filteredParishIds = new List<int?>();
            DateTime nextSevenDays = DateTime.Today.AddDays(6);
            //need list of parish id ints
            List<int> parishIds = db.Parishes.Select(p => p.ID).ToList();
            foreach (int id in parishIds)
            {
                foreach(int parishId in events)
                {
                    if (id == parishId)
                    {
                        if(db.Events.Where(e => e.ParishId == id).Select(e=>e.Date).First() >= DateTime.Today 
                            && db.Events.Where(e => e.ParishId == id).Select(e => e.Date).First() <= nextSevenDays)
                        {
                            parishesWithEvents.Add(parishes.Where(p => p.ID == id).Select(p => p).SingleOrDefault());
                        }
                    }
                }
            }
            
            foreach (Parish thisParish in parishesWithEvents)
            { 
                tempLatitudes.Add(thisParish.Lat);
                tempLongitudes.Add(thisParish.Long);
                names.Add(thisParish.Name);
                listOfParishEvents.Add(db.Events.Where(e => e.ParishId == thisParish.ID).Select(e => e).ToList());
                filteredParishIds.Add(db.Parishes.Where(p => p.ID == thisParish.ID).Select(p => p.ID).Single());
            }
            //ViewBag.Map2URL = stringBuilder.ToString();
            var namesToArray = names.ToArray();
            var specificEventsToArray = listOfParishEvents.ToArray();
            var latitudesToArray = tempLatitudes.ToArray();
            var longitudesToArray = tempLongitudes.ToArray();
            var latitudes = latitudesToArray;
            var longitudes = longitudesToArray;
            var filteredParishIdsToArray = filteredParishIds.ToArray(); 
            ViewBag.Names = namesToArray;
            ViewBag.Events = specificEventsToArray;
            ViewBag.Latitudes = latitudes;
            ViewBag.Longitudes = longitudes;
            ViewBag.ParishIds = filteredParishIdsToArray;
            //stringBuilder.Clear();
            //stringBuilder.Append("https://www.google.com/maps/embed/v1/place?key=");
            //stringBuilder.Append(Models.Access.apiKey);
            //stringBuilder.Append(";");
            //ViewBag.Site = stringBuilder;
            //ViewBag.Key = Models.Access.apiKey;
            return View(parishesWithEvents);
        }




 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        

        //    public class AddressComponent
        //    {
        //        public string long_name { get; set; }
        //        public string short_name { get; set; }
        //        public List<string> types { get; set; }
        //    }

        //    public class Bounds
        //    {
        //        public Location northeast { get; set; }
        //        public Location southwest { get; set; }
        //    }

        //    public class Location
        //    {
        //        public double lat { get; set; }
        //        public double lng { get; set; }
        //    }

        //    public class Geometry
        //    {
        //        public Bounds bounds { get; set; }
        //        public Location location { get; set; }
        //        public string location_type { get; set; }
        //        public Bounds viewport { get; set; }
        //    }

        //    public class Result
        //    {
        //        public List<AddressComponent> address_components { get; set; }
        //        public string formatted_address { get; set; }
        //        public Geometry geometry { get; set; }
        //        public bool partial_match { get; set; }
        //        public List<string> types { get; set; }
        //    }

        //    public class RootObject
        //    {
        //        public List<Result> results { get; set; }
        //        public string status { get; set; }
        //    }

    }
}