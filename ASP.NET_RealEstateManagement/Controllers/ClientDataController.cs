using ASP.NET_RealEstateManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace ASP.NET_RealEstateManagement.Controllers
{
    public class ClientDataController : ApiController
    {
        // set up HTTPS solid connection
        private static readonly HttpClient client;
        // convert data into Json and insert them into HEADER REQUEST
        private JavaScriptSerializer jss = new JavaScriptSerializer();
     
        // Get all Property from database for client to view them.
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(PropertyDetailDTO))]
        public IHttpActionResult ListOfProperty()
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
                    Amenities = property.Amenities,
                    ImageFileNames = property.ImageFileNames,
                    PropertyPrice = property.PropertyPrice,
                    PropertyDescription = property.PropertyDescription,
                    PropertyStatus = property.PropertyStatus,
                    ListingDate = property.ListingDate,
                }));
            
           return Ok(propertyDetailsDTO);
        }

        [HttpGet]
        [ResponseType(typeof(PropertyDetailDTO))]
        public IHttpActionResult ListOfProperty(string searchProperty)
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
                    Amenities = property.Amenities,
                    ImageFileNames= property.ImageFileNames,
                    PropertyPrice = property.PropertyPrice,
                    PropertyDescription = property.PropertyDescription,
                    PropertyStatus = property.PropertyStatus,
                    ListingDate = property.ListingDate,
                }));
            if (!string.IsNullOrEmpty(searchProperty))
            {
                Debug.WriteLine("searchProperty");
                propertyDetailsDTO = propertyDetailsDTO
                .Where(p => p.PropertyAddress.ToLower().Contains(searchProperty.ToLower()))
                .ToList();
            }
            if (propertyDetailsDTO.Count == 0)
            {
                return NotFound();
            }
            return Ok(propertyDetailsDTO);
        }

        [ResponseType(typeof(PropertyDetailDTO))]
        [HttpGet]
        public IHttpActionResult FindPropertyDetail(int? id)
        {
            PropertyDetail foundproperty = db.PropertyDetails.Include(p => p.Agents).SingleOrDefault(p => p.PropertyID == id);
            var agents = foundproperty.Agents.ToList();
            PropertyDetailDTO propertyDTO = new PropertyDetailDTO()
            {
                PropertyID = foundproperty.PropertyID,
                PropertyType = foundproperty.PropertyType,
                PropertyAddress = foundproperty.PropertyAddress,
                PropertySize = foundproperty.PropertySize,
                NumberOfBedrooms = foundproperty.NumberOfBedrooms,
                NumberOfBathrooms = foundproperty.NumberOfBathrooms,
                Amenities = foundproperty.Amenities,
                ImageFileNames = foundproperty.ImageFileNames,
                PropertyPrice = foundproperty.PropertyPrice,
                PropertyDescription = foundproperty.PropertyDescription,
                PropertyStatus = foundproperty.PropertyStatus,
                ListingDate = foundproperty.ListingDate,
                Agents = agents.Select(agent => new EstateAgentDTO
                {
                    EstateAgentId = agent.EstateAgentId,
                    Name = agent.Name,
                    Email = agent.Email,
                    Phone = agent.Phone,
                    Role = agent.Role
                }).ToList()
            };
            if (foundproperty == null)
            {
                return NotFound();

            }
            return Ok(propertyDTO);
        }
    }
}
