using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Models
{
    public class BaseEnum
    {
        [Key]
        public Guid Id { get; set; }
    }
}
