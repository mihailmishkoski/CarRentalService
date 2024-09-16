using CarRentalService.Domain.Models;
using CarRentalService.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly ICarService _carService;

        public APIController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("[action]")]
        public List<Car> GetAllCars()
        {
            return _carService.GetCars();
        }
        
    }
}
