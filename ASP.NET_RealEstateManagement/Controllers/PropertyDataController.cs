using ASP.NET_RealEstateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ASP.NET_RealEstateManagement.Controllers
{
    public class PropertyDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        [ResponseType(typeof(PropertyDetailDTO))]
        public IHttpActionResult ListProperty()
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
    }
}
