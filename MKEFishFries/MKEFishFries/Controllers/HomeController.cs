using Microsoft.AspNet.Identity;
using MKEFishFries.Models;
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

            List<Parish> parishes = db.Parishes.ToList();
            //Parish thisParish;
            foreach (Parish thisParish in parishes)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(thisParish.Street1.Replace(" ", "+"));
                stringBuilder.Append(";");
                stringBuilder.Append(thisParish.City.Replace(" ", "+"));
                stringBuilder.Append(";");
                stringBuilder.Append(thisParish.State.Replace(" ", "+"));
                stringBuilder.Append(";");

                string fullAddressForAPI = stringBuilder.ToString(); 

            }




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
    }
}