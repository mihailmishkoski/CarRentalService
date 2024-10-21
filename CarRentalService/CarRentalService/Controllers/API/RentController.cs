using CarRentalService.Domain.Models;
using CarRentalService.Domain.Models.Exceptions;
using CarRentalService.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpPost]
        public IActionResult Create([FromBody] Rent rent)
        {
            if (ModelState.IsValid)
            {
                string? customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                try
                {
                    _rentService.CreateNewRent(rent, customerId);
                    return Ok(new { message = "Rent created successfully." });
                }
                catch (CarException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
                catch (RentException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "An error occurred while creating the rent.", details = ex.Message });
                }
            }

            return BadRequest(new { message = "Invalid rent data." });
        }
        [HttpGet("{id}")]
        public ActionResult<Car> GetRentById(Guid id)
        {
            var car = _rentService.GetRentById(id);

            if (car == null)
            {
                return NotFound("Rent not found");
            }

            return Ok(car);
        }
    }
}
