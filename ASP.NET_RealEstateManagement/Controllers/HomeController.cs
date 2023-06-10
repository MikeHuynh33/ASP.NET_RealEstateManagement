using ASP.NET_RealEstateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_RealEstateManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Check if User login is Admin 
            Boolean isAdmin = false;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = ShareFunction.FindAdmin(User.Identity.Name);
            }
            ViewBag.IsAdmin = isAdmin;
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