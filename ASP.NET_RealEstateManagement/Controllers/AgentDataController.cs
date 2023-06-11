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
    public class AgentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(EstateAgentDTO))]
        public IHttpActionResult ListAgents()
        {
            List<EstateAgent> estateAgents = db.EstateAgents.ToList();
            List<EstateAgentDTO> estateAgentDTO = new List<EstateAgentDTO>();

            estateAgents.ForEach(agent =>
                estateAgentDTO.Add(new EstateAgentDTO()
                {
                    EstateAgentId = agent.EstateAgentId,
                    Name = agent.Name,
                    Email = agent.Email,
                    Phone = agent.Phone,
                    Role = agent.Role
                })); ;
            return Ok(estateAgentDTO);
        }

        [HttpPost]
        [ResponseType(typeof(EstateAgent))]
        public IHttpActionResult AddNewProperty(EstateAgent agent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.EstateAgents.Add(agent);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = agent.EstateAgentId }, agent);
        }
    }
}
