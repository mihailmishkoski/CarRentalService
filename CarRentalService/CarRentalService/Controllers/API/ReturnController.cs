using CarRentalService.Domain.Models.Exceptions;
using CarRentalService.Domain.Models;
using CarRentalService.Service.Implementation;
using CarRentalService.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReturnController : ControllerBase
    {
        private readonly IReturnService _returnService;
        private readonly IRentService _rentService;
        private readonly IRentParamsService _rentParamsService;

        public ReturnController(IReturnService returnService, IRentService rentService, IRentParamsService rentParamsService)
        {
            _returnService = returnService;
            _rentService = rentService;
            _rentParamsService = rentParamsService;
        }

        // GET: api/Return
        [HttpGet("[action]")]
        public IActionResult GetAllReturns()
        {
            var returns = _returnService.GetReturns();

            if (returns == null || !returns.Any())
            {
                return NotFound("No returns found.");
            }

            return Ok(returns);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Return returnModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rent = _rentService.GetRentById(returnModel.RentId);
            if (rent == null)
            {
                return NotFound("Rent not found.");
            }

            try
            {
                returnModel.LateFee = _rentParamsService.CalculateLateFee(rent, returnModel.ReturnDate, _rentParamsService.GetRentParams().AdditionalFees);
                _returnService.CreateNewReturn(returnModel);
                return Ok("Return created successfully.");
            }
            catch (ReturnException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
