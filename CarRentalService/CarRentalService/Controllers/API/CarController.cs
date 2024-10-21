using CarRentalService.Domain.Models;
using CarRentalService.Domain.Models.Exceptions;
using CarRentalService.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CarRentalService.Web.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/Car
        [HttpGet]
        public ActionResult<List<Car>> GetAllCars()
        {
            var cars = _carService.GetCars();
            return Ok(cars);
        }

        // GET: api/Car/{id}
        [HttpGet("{id}")]
        public ActionResult<Car> GetCarById(Guid id)
        {
            var car = _carService.GetCarById(id);

            if (car == null)
            {
                return NotFound("Car not found");
            }

            return Ok(car);
        }

        // POST: api/Car
        [HttpPost]
        public ActionResult CreateCar([FromBody] Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _carService.CreateNewCar(car);
                    return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car); 
                }
                catch (CarException ex)
                {
                    return BadRequest(new { message = ex.Message }); 
                }
            }

            return BadRequest("Invalid car data");
        }

        // PUT: api/Car/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCar(Guid id, [FromBody] Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCar = _carService.GetCarById(id);
                    if (existingCar == null)
                    {
                        return NotFound("Car not found");
                    }
                    existingCar.Name = car.Name;
                    existingCar.Description = car.Description;
                    existingCar.Model = car.Model;
                    existingCar.DateManufactured = car.DateManufactured;
                    existingCar.KilometersTraveled = car.KilometersTraveled;
                    existingCar.Color = car.Color;
                    existingCar.LicensePlate = car.LicensePlate;
                    existingCar.PricePerDay = car.PricePerDay;
 
                    _carService.UpdateCar(existingCar);
                    return NoContent(); 
                }
                catch (CarException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            return BadRequest("Invalid car data");
        }


        // DELETE: api/Car/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCar(Guid id)
        {
            var car = _carService.GetCarById(id);

            if (car == null)
            {
                return NotFound("Car not found");
            }

            _carService.DeleteCar(id);
            return NoContent(); 
        }
    }
}
