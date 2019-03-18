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
//using GoogleMaps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            // Get all parish addresses in the Parishes model/table

            //3	St. Josaphat Basilica	2333 South 6th Street	NULL	Milwaukee	WI	53215	43.0024	-87.9191	www.TheBasilica.org	4146455623	6	NULL	True
            //The GeocoderRequest object literal contains the following fields:
            //{
            //address: string,
            //}
            //ViewBag.Key = Models.Access.apiKey;
            StringBuilder stringBuilder = new StringBuilder();

            // TODO - change this query to show parishes with fish fry events in the next 7 days, 
            //  so that a Friday is included. 
            List<Parish> parishes = db.Parishes.ToList();
            foreach (Parish thisParish in parishes)
            {
                //    //// Moved to ParishAdminController CreateParish 
                //    //StringBuilder stringBuilder = new StringBuilder();
                //    //stringBuilder.Append(thisParish.Street1.Replace(" ", "+"));
                //    //stringBuilder.Append(";");
                //    //stringBuilder.Append(thisParish.City.Replace(" ", "+"));
                //    //stringBuilder.Append(";");
                //    //stringBuilder.Append(thisParish.State.Replace(" ", "+"));
                //    //string fullAddressForAPI = stringBuilder.ToString();
                //    //// example: string url = @"https://maps.googleapis.com/maps/api/geocode/json?address={stringBuilder.ToString()}1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=YOUR_API_KEY";

                //    //// TODO - replace key with new class
                //    //string key = "AIzaSyAqPB-xlRlEDxCQcWVRI0pZ9UJCHDhNzaE";
                //    //string url = @"https://maps.googleapis.com/maps/api/geocode/json?address=" +
                //    //    stringBuilder.ToString() + "&key=" + key;

                //    //WebRequest request = WebRequest.Create(url);
                //    //WebResponse response = request.GetResponse();
                //    //System.IO.Stream data = response.GetResponseStream();
                //    //StreamReader reader = new StreamReader(data);
                //    //// json-formatted string from maps api
                //    //string responseFromReader = reader.ReadToEnd();
                //    //response.Close();
                //    //var root = JsonConvert.DeserializeObject<ParishMapAPIData>(responseFromReader);
                //    //var location = root.results[0].geometry.location;
                //    //var latitude = location.lat;
                //    //var longitude = location.lng;
                //    ////foreach (var singleResult in root.results)
                //    ////{
                //    ////    var location = singleResult.geometry.location;
                //    ////    var latitude = location.lat;
                //    ////    var longitude = location.lng;
                //    ////    // Do whatever you want with them.
                //    ////}


                //    // https://www.google.com/maps/embed/v1/place?key=YOUR_API_KEY&q=Eiffel+Tower,Paris+France
                //    // The following URL parameter is required:
                //    // q defines the place to highlight on the map.It accepts a location as either 
                //    // a place name, address, or place ID.The string should be URL-escaped, so an address such as "City Hall, New York, NY" should be converted to City + Hall,New + York,NY. (The Maps Embed API supports both + and % 20 when escaping spaces.) Place IDs should be prefixed with place_id:.


                //    //var geocoder = new google.maps.Geocoder();
                //    //var latlng = new google.maps.LatLng(thisParish.Lat, thisParish.Long);
                //    //var mapOptions = { zoom: 8, center: latlng };
                //    //map = new google.maps.Map(document.getElementById('map'), mapOptions);

                //    //string url = @"https://maps.googleapis.com/maps/api/directions/json?origin=75+9th+Ave+New+York,+NY&destination=MetLife+Stadium+1+MetLife+Stadium+Dr+East+Rutherford,+NJ+07073&key=YOUR_API_KEY";

                //    //// TODO - replace key with new class
                //    // key AIzaSyAqPB-xlRlEDxCQcWVRI0pZ9UJCHDhNzaE

                //    // this is the address of St. Josaphat's Basilica
                //    //string   url = @"https://maps.googleapis.com/maps/api/geocode/json?latlng=43.0024,-87.9191&key=AIzaSyAqPB-xlRlEDxCQcWVRI0pZ9UJCHDhNzaE";

                //    //string url = @"https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=AIzaSyAqPB-xlRlEDxCQcWVRI0pZ9UJCHDhNzaE";

                //    //WebRequest request = WebRequest.Create(url);
                //    //WebResponse response = request.GetResponse();
                //    //System.IO.Stream data = response.GetResponseStream();
                //    //StreamReader reader = new StreamReader(data);
                //    //// json-formatted string from maps api
                //    //string responseFromServer = reader.ReadToEnd();
                //    //response.Close();


                //    //ValueProviderFactory valueProviderFactory = new ValueProviderFactory() { responseFromServer };
                //    //https://www.google.com/maps/embed/v1/place?key=AIzaSyAqPB-xlRlEDxCQcWVRI0pZ9UJCHDhNzaE&q=lat:43.0024+long:-87.9191q=Basilica+of+St+Josaphat
                //    ViewBag.MapURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyAqPB-xlRlEDxCQcWVRI0pZ9UJCHDhNzaE&q=lat:43.0024+long:-87.9191q=Basilica+of+St+Josaphat"
                //        + "&callback=initMap";

                //thisParish.Lat
                //thisParish.Lng



                // TODO - replace key with the new key class
                //string sampleMap = $"www.google.com/maps/embed/v1/place?key={Models.Access.apiKey}=lat:43.0024+long:-87.9191q=Basilica+of+St+Josaphat";
                //ViewBag.MapURL = "https://" + sampleMap + "&callback=initMap";

                // TODO - generate the correct URL with the geocodes of all the churches 
                //        with fish fries in the next 7 days


                //ViewBag.MapURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyAqPB-xlRlEDxCQcWVRI0pZ9UJCHDhNzaE&q=lat:43.0024+long:-87.9191"
                //        + "&callback=initMap";
                //ViewBag.MapURL = "https://maps.googleapis.com/maps/api/js?key=AIzaSyAqPB-xlRlEDxCQcWVRI0pZ9UJCHDhNzaE&callback=initMap";


                //    < script >
                //      var map;
                //    function initMap()
                //    {
                //        map = new google.maps.Map(document.getElementById('map'), {
                //        center: { lat: 43.0391, lng: -88.0697 },
                //        zoom: 6
                //    });

                
                // from https://developers.google.com/maps/documentation/embed/guide#optional_parameters :
                // https://www.google.com/maps/embed/v1/MODE?key=YOUR_API_KEY&parameters
                stringBuilder.Clear();
                stringBuilder.Append("https://www.google.com/maps/embed/v1/place?key=");
                stringBuilder.Append(Models.Access.apiKey);
                stringBuilder.Append("&q=");
                stringBuilder.Append(thisParish.Name.Replace(" ", "+"));
                stringBuilder.Append(";");
                stringBuilder.Append(""); stringBuilder.Append(thisParish.Street1.Replace(" ", "+"));
                stringBuilder.Append(";");
                stringBuilder.Append(thisParish.City.Replace(" ", "+"));
                stringBuilder.Append(";");
                stringBuilder.Append(thisParish.State.Replace(" ", "+"));
            }
            ViewBag.Map2URL = stringBuilder.ToString();
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

        //public class ParishMapAPIData
        //{
        //    public Result[] results { get; set; }
        //    public string status { get; set; }
        //}

        //public class Result
        //{
        //    public Address_Components[] address_components { get; set; }
        //    public string formatted_address { get; set; }
        //    public Geometry geometry { get; set; }
        //    public string place_id { get; set; }
        //    public string[] types { get; set; }
        //}

        //public class Geometry
        //{
        //    public Location location { get; set; }
        //    public string location_type { get; set; }
        //    public Viewport viewport { get; set; }
        //}

        //public class Location
        //{
        //    public float lat { get; set; }
        //    public float lng { get; set; }
        //}

        //public class Viewport
        //{
        //    public Northeast northeast { get; set; }
        //    public Southwest southwest { get; set; }
        //}

        //public class Northeast
        //{
        //    public float lat { get; set; }
        //    public float lng { get; set; }
        //}

        //public class Southwest
        //{
        //    public float lat { get; set; }
        //    public float lng { get; set; }
        //}

        //public class Address_Components
        //{
        //    public string long_name { get; set; }
        //    public string short_name { get; set; }
        //    public string[] types { get; set; }
        //}

        public class AddressComponent
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            public List<string> types { get; set; }
        }

        public class Bounds
        {
            public Location northeast { get; set; }
            public Location southwest { get; set; }
        }

        public class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Geometry
        {
            public Bounds bounds { get; set; }
            public Location location { get; set; }
            public string location_type { get; set; }
            public Bounds viewport { get; set; }
        }

        public class Result
        {
            public List<AddressComponent> address_components { get; set; }
            public string formatted_address { get; set; }
            public Geometry geometry { get; set; }
            public bool partial_match { get; set; }
            public List<string> types { get; set; }
        }

        public class RootObject
        {
            public List<Result> results { get; set; }
            public string status { get; set; }
        }

    }
}