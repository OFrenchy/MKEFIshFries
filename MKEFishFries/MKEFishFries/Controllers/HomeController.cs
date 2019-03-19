using Microsoft.AspNet.Identity;
using MKEFishFries.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MailKit.Net.Smtp;

namespace MKEFishFries.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var tempLatitudes = new List<double>();
            var tempLongitudes = new List<double>();
            // Get all parish addresses in the Parishes model/table

            //ViewBag.Key = Models.Access.apiKey;

            // TODO - change this query to show parishes with fish fry events in the next 7 days, 
            //  so that a Friday is included. 
            List<int> events = db.Events.Select(e=>e.ParishId).ToList();
            List<Parish> parishes = db.Parishes.ToList();
            List<Parish> parishesWithEvents = new List<Parish>();
            //need list of parish id ints
            List<int> parishIds = db.Parishes.Select(p => p.ID).ToList();
            foreach (int id in parishIds)
            {
                foreach(int parishId in events)
                {
                    if (id == parishId)
                    {
                        parishesWithEvents.Add(parishes.Where(p=>p.ID == id).Select(p=>p).SingleOrDefault());
                    }
                }
            }
            
            foreach (Parish thisParish in parishesWithEvents)
            {
                
                tempLatitudes.Add(thisParish.Lat);
                tempLongitudes.Add(thisParish.Long);

              
            }
            //ViewBag.Map2URL = stringBuilder.ToString();
            var latitudesToArray = tempLatitudes.ToArray();
            var longitudesToArray = tempLongitudes.ToArray();
            var latitudes = latitudesToArray;
            var longitudes = longitudesToArray;
            ViewBag.Latitudes = latitudes;
            ViewBag.Longitudes = longitudes;
            //stringBuilder.Clear();
            //stringBuilder.Append("https://www.google.com/maps/embed/v1/place?key=");
            //stringBuilder.Append(Models.Access.apiKey);
            //stringBuilder.Append(";");
            //ViewBag.Site = stringBuilder;
            //ViewBag.Key = Models.Access.apiKey;
            return View();
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