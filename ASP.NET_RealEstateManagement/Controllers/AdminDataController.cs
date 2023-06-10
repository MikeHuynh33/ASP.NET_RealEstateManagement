using ASP.NET_RealEstateManagement.Migrations;
using ASP.NET_RealEstateManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;

namespace ASP.NET_RealEstateManagement.Controllers
{
    public class AdminDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(EstateAgentDTO))]
        public IHttpActionResult ListAgents()
        {
            List<EstateAgent> estateAgents = db.EstateAgents.ToList();
            List<EstateAgentDTO> estateAgentDTO = new List<EstateAgentDTO>();

            estateAgents.ForEach(agent => 
                estateAgentDTO.Add(new EstateAgentDTO(){
                    EstateAgentId = agent.EstateAgentId,
                    Name = agent.Name,
                    Email = agent.Email,
                    Phone = agent.Phone ,
                    Role = agent.Role 
                }));;
            return Ok(estateAgentDTO);
        }
        /*
        [HttpGet]
        [Route("api/AdminData/Property")]
        [ResponseType(typeof(EstateAgentDTO))]
        public IHttpActionResult ListProperty(string address)
        {
            List<PropertyDetail> propertyDetails = db.PropertyDetails.ToList();
            List<PropertyDetailDTO> propertyDetailsDTO = new List<PropertyDetailDTO>();

            propertyDetails.ForEach(property =>
                propertyDetailsDTO.Add(new PropertyDetailDTO()
                {
                     PropertyID = property.PropertyID,
                    PropertyType = property.PropertyType,
                    PropertyAddress = property.PropertyAddress,
                    PropertySize = property.PropertySize,
                    NumberOfBedrooms = property.NumberOfBedrooms,
                    NumberOfBathrooms = property.NumberOfBathrooms,
                    Amenities = property.Amenities ,
                    PropertyPrice = property.PropertyPrice ,
                    PropertyDescription = property.PropertyDescription,
                    PropertyStatus = property.PropertyStatus,


                })); ;
            return Ok(propertyDetailsDTO);
        }
        */

    }
}
