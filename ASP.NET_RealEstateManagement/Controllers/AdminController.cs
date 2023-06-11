using ASP.NET_RealEstateManagement.Models;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ASP.NET_RealEstateManagement.Controllers
{
    public class AdminController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AdminController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44336/api/");
        }
        // GET: Admin
        public ActionResult Index()
        {
            Boolean isAdmin = false;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = ShareFunction.FindAdmin(User.Identity.Name);
            }
            ViewBag.IsAdmin = isAdmin;

            string url = "AgentData/ListAgents";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<EstateAgentDTO> agents = response.Content.ReadAsAsync<IEnumerable<EstateAgentDTO>>().Result;

            string sec_url = "PropertyData/ListPropertyies";
            HttpResponseMessage sec_response = client.GetAsync(sec_url).Result;
            IEnumerable<PropertyDetailDTO> properties = sec_response.Content.ReadAsAsync<IEnumerable<PropertyDetailDTO>>().Result;

            AgentsAndPropertiesViewModel viewModel = new AgentsAndPropertiesViewModel
            {
                Agents = agents,
                Properties = properties
            };


            return View(viewModel);
        }
    }
}