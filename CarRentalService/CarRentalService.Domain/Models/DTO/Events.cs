using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Models.DTO
{
    public class Events
    {
        public String EventName { get; set; }
        public String HostName { get; set; }
        public bool isPartnerEvent { get; set; }

        public String ImageUrl { get; set; }
    }
}
