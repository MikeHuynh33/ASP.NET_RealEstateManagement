using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_RealEstateManagement.Models
{
    public class PropertyDetailAndAllAgentsViewModel
    {
        public PropertyDetailDTO Properties { get; set; }
        public IEnumerable<EstateAgentDTO> AllAgents { get; set; }
    }

}