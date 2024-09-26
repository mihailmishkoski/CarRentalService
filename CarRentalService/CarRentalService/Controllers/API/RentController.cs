using CarRentalService.Domain.Models;
using CarRentalService.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IRentService _rentService;

        public RentController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [HttpGet("[action]")]
        public List<Rent> GetAllRents()
        {
            return _rentService.GetRents();
        }
    }
}
