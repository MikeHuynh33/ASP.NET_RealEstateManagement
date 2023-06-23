using ASP.NET_RealEstateManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ASP.NET_RealEstateManagement.Controllers
{
    public class ClientController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();


        static ClientController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44336/api/");
        }

        // GET: request properties , display to view.
        [HttpGet]
        public ActionResult Index()
        {
            string url = "";
            string searchProperty = Request.QueryString["searchProperty"];
            Debug.WriteLine(searchProperty);
            if (!string.IsNullOrEmpty(searchProperty))
            {
                 url = "ClientData/ListOfProperty?searchProperty=" + searchProperty;
            }
            else
            {
                 url = "ClientData/ListOfProperty";
            }
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                List<PropertyDetailDTO> properties = response.Content.ReadAsAsync<List<PropertyDetailDTO>>().Result;
                return View(properties);
            }
            else
            {
                // Handle the unsuccessful response here
                // You can redirect to an error page or return an appropriate view
                return View("Error");
            }
        }
        // Find Property Detail and Real Esate agents in database
        [HttpGet]
        [Route("/Client/Property_Detail/{id}")]
        public ActionResult Property_Detail(int id) {

            string url = "ClientData/FindPropertyDetail/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                PropertyDetailDTO properties = response.Content.ReadAsAsync<PropertyDetailDTO>().Result;
                return View(properties);
            }
            else
            {
                // Handle the unsuccessful response here
                // You can redirect to an error page or return an appropriate view
                return View("Error");
            }
            return View();
        }
    }
}