using ASP.NET_RealEstateManagement.Models;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using System.Web.Http.Description;
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

        [HttpGet]
        // GET: /Admin
        [Route("/Admin")]
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

            string sec_url = "PropertyData/ListProperties";
            HttpResponseMessage sec_response = client.GetAsync(sec_url).Result;
            IEnumerable<PropertyDetailDTO> properties = sec_response.Content.ReadAsAsync<IEnumerable<PropertyDetailDTO>>().Result;

            AgentsAndPropertiesViewModel viewModel = new AgentsAndPropertiesViewModel
            {
                Agents = agents,
                Properties = properties
            };
            return View(viewModel);
        }

   


        //GET /AddNewProperty Create Form View.  
        [HttpGet]
        [Route("/Admin/AddNewProperty")]
        public ActionResult AddNewProperty()
        {
            return View();
        }
        

        //GET /Admin/Property/Id
        [HttpGet]
        [Route("/Admin/PropertyDetail/{id}")]
        public ActionResult PropertyDetail(int id) {

            string url = "PropertyData/FindProperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);
            PropertyDetailDTO foundproperty = response.Content.ReadAsAsync<PropertyDetailDTO>().Result;
                
            return View(foundproperty);
        }
        
        //POST: Admin Adding new Property
        [HttpPost]
        [Route("/Admin/NewProperty")]
        public ActionResult NewProperty(PropertyDetail property )
        {
            Debug.WriteLine("the json payload is :");
           
            string url = "PropertyData/AddNewProperty";


            string jsonpayload = jss.Serialize(property);
            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("/Admin");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //Get :  Edit property
        [Route("Admin/PropertyEdit/{id}")]
        [HttpGet]
        public ActionResult PropertyEdit(int id) {

            string url = "PropertyData/FindProperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PropertyDetailDTO SelectedProperty = response.Content.ReadAsAsync <PropertyDetailDTO>().Result;
            return View(SelectedProperty); 
        }

        //Post : Update Property after submit.
        [HttpPost]
        [Route("Admin/PropertyUpdate/{id}")]
        public ActionResult PropertyUpdate(int id ,PropertyDetail property)
        {
            string url = "PropertyData/UpdateProperty/"+ id;
            string jsonpayload = jss.Serialize(property);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("Admin/DeleteConfirm/{id}")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "PropertyData/FindProperty/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PropertyDetailDTO SelectedProperty = response.Content.ReadAsAsync<PropertyDetailDTO>().Result;
            return View(SelectedProperty);
        }

        [HttpPost]
        [Route("Admin/DeleteProperty/{id}")]
        public ActionResult DeleteProperty(int id)
        {
            string url = "PropertyData/DeleteProperty/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //GET /AddNewAgent Create Form View.  
        [HttpGet]
        [Route("/Admin/AddNewAgent")]
        public ActionResult AddNewAgent()
        {
            return View();
        }

        //POST: Admin Adding new Agent 
        [HttpPost]
        [Route("/Admin/NewAgent")]
        public ActionResult NewAgent(EstateAgent property)
        {
            Debug.WriteLine("the json payload is :");

            string url = "AgentData/AddNewAgent";
            string jsonpayload = jss.Serialize(property);
            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("/Admin");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //Get :  Edit property
        [Route("Admin/AgentEdit/{id}")]
        [HttpGet]
        public ActionResult AgentEdit(int id)
        {
            string url = "AgentData/FindAgent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EstateAgentDTO SelectedAgent = response.Content.ReadAsAsync<EstateAgentDTO>().Result;
            return View(SelectedAgent);
        }

        //Post : Update Property after submit.
        [HttpPost]
        [Route("Admin/AgentUpdate/{id}")]
        public ActionResult AgentUpdate(int id, EstateAgent agent)
        {
            string url = "AgentData/UpdateAgent/" + id;
            string jsonpayload = jss.Serialize(agent);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("Admin/DeleteAgentConfirm/{id}")]
        public ActionResult DeleteAgentConfirm(int id)
        {
            string url = "AgentData/FindAgent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EstateAgentDTO SelectedAgent = response.Content.ReadAsAsync<EstateAgentDTO>().Result;
            return View(SelectedAgent);
        }

        [HttpPost]
        [Route("Admin/DeleteAgent/{id}")]
        public ActionResult DeleteAgent(int id)
        {
            string url = "AgentData/DeleteAgent/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}