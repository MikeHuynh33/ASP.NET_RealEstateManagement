﻿using ASP.NET_RealEstateManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Routing;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Web;

namespace ASP.NET_RealEstateManagement.Controllers
{
    public class PropertyDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(PropertyDetailDTO))]
        public IHttpActionResult ListProperties()
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
                    PropertyPrice = property.PropertyPrice,
                    PropertyDescription = property.PropertyDescription,
                    PropertyStatus = property.PropertyStatus,
                    ListingDate = property.ListingDate,

                })); ;
            return Ok(propertyDetailsDTO);
        }

        [HttpPost]
        [ResponseType(typeof(PropertyDetail))]
        public IHttpActionResult AddNewProperty(PropertyDetail property)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.PropertyDetails.Add(property);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = property.PropertyID }, property);
        }


      
        [ResponseType(typeof(PropertyDetailDTO))]
        [HttpGet]
        public IHttpActionResult FindPropertyAssociateWithAgent(int? id)
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

        [ResponseType(typeof(PropertyDetailDTO))]
        [HttpGet]
        public IHttpActionResult FindProperty(int? id)
        {
            PropertyDetail foundproperty = db.PropertyDetails.Find(id);
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
            };
            if (foundproperty == null)
            {
                return NotFound();

            }
            return Ok(propertyDTO);
        }

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateProperty(int id, PropertyDetail property)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.PropertyID)
            {

                return BadRequest();
            }

            db.Entry(property).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(PropertyDetail))]
        public IHttpActionResult DeleteProperty(int id)
        {
            PropertyDetail property = db.PropertyDetails.Find(id);
            if(property == null) { return NotFound(); }
            db.PropertyDetails.Remove(property);
            db.SaveChanges();
            return Ok();
        }
        private bool PropertyExists(int id)
        {
            return db.PropertyDetails.Count(e => e.PropertyID == id) > 0;
        }

        [HttpPost]
        public IHttpActionResult PropertyUpdateAgents ([FromBody] JObject payload)
        {
            int propertyID = payload["PropertyID"].ToObject<int>();
            JArray agentSelected = (JArray)payload["AgentSelected"];
            int[] agentIds = agentSelected.ToObject<int[]>();
            // get all agents ID have association with PropertyId .
            PropertyDetail foundproperty = db.PropertyDetails.Include(p => p.Agents).SingleOrDefault(p => p.PropertyID == propertyID);
            // delete all the relationships
             foundproperty.Agents.Clear();
            // get new agents from checkbox array
            List<EstateAgent> newAgents = db.EstateAgents.Where(a => agentIds.Contains(a.EstateAgentId)).ToList();
            // add all updated agents list back to property collection.
            foreach (EstateAgent agent in newAgents)
            {
                foundproperty.Agents.Add(agent);
            }
            db.SaveChanges();

            return Ok();
        }
    }
}
