using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_RealEstateManagement.Models
{
    public class AgentsAndPropertiesViewModel
    {
            public IEnumerable<EstateAgentDTO> Agents { get; set; }
            public IEnumerable<PropertyDetailDTO> Properties { get; set; }
    }
}