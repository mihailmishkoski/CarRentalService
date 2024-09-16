using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalService.Domain.Models;
using CarRentalService.Repository;
using CarRentalService.Service.Implementation;
using System.Security.Claims;
using CarRentalService.Service.Interface;
using CarRentalService.Domain.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace CarRentalService.Web.Controllers
{
    [Authorize]
    public class RentsController : Controller
    {
        private readonly IRentService rentService;
        private readonly ICarService carService;
        private readonly IUserService userService;
        private readonly RentParameters rentParameters;
        public RentsController(IRentService rentService, ICarService carService, IUserService userService, RentParameters rentParameters)
        {
            this.carService = carService;
            this.rentService = rentService;
            this.userService = userService;
            this.rentParameters = rentParameters;
        }

        // GET: Rents
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var rents = rentService.GetRents();

            ViewBag.Title = "All Rents";
            return View(rents);
        }
        // GET: Rents
        public IActionResult MyRents()
        {
            string? customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rents = userService.GetMyRents(customerId);

            ViewBag.Title = "My Rents";
            return View("Index", rents);
        }
        // GET: Rents/Details/5
        public IActionResult Details(Guid id)
        {
            var rent = rentService.GetRentById(id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // GET: Rents/Create
        public IActionResult Create(Guid id, string ?errorMessage)
        {
            var car = carService.GetCarById(id);
            string? customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = userService.GetCustomerById(customerId);
            ViewData["Car"] = car;
            ViewData["Customer"] = customer;
            if(errorMessage!=null)
            {
                ViewData["ErrorMessage"] = errorMessage;
            }
            return View();
        }

        // POST: Rents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CarId,RentDate,ReturnDate,RentAmount")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                string? customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                try
                {
                    rentService.CreateNewRent(rent, customerId);
                }
                catch (CarNotAvailableException ex)
                {
                    return RedirectToAction("Create", new { id = rent.CarId, errorMessage = ex.Message });
                }
                catch (RentNotAvailableException ex)
                {
                    return RedirectToAction("Create", new { id = rent.CarId, errorMessage = ex.Message });
                }
                return RedirectToAction("Index", "Cars");
            }
            else { return View(rent); }
        }

        // GET: Rents/Edit/5
        public IActionResult Edit(Guid id)
        {

            var rent = rentService.GetRentById(id);
            if (rent == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(carService.GetCars(), "Id", "Description", rent.CarId);
            return View(rent);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("CarId,RentDate,ReturnDate,RentAmount,Id")] Rent rent)
        {
            if (id != rent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingRent = rentService.GetRentById(id);
                rent.isActive = existingRent.isActive;
                rent.CustomerId = existingRent.CustomerId;
                try
                {
                    rentService.UpdateRent(rent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentExists(rent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Cars");
            }
            ViewData["CarId"] = new SelectList(carService.GetCars(), "Id", "Description", rent.CarId);
            return View(rent);
        }

        // GET: Rents/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = rentService.GetRentById(id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            rentService.DeleteRent(id);

            return RedirectToAction(nameof(Index));
        }

        private bool RentExists(Guid id)
        {
            return rentService.GetRentById(id) != null;
        }
    }
}
