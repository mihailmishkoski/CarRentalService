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

namespace CarRentalService.Web.Controllers
{
    public class RentsController : Controller
    {
        private readonly IRentService rentService;
        private readonly ICarService carService;
        private readonly IUserService userService;
        public RentsController(IRentService rentService, ICarService carService, IUserService userService)
        {
            this.carService = carService;
            this.rentService = rentService;
            this.userService = userService;
        }

        // GET: Rents
        public IActionResult Index()
        {
            
            var rents = rentService.GetRents();
            return View(rents);
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
        public IActionResult Create(Guid id)
        {
            var car = carService.GetCarById(id);
            string? customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = userService.GetCustomerById(customerId);
            ViewData["Car"] = car;
            ViewData["Customer"] = customer;
            return View();
        }

        // POST: Rents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CarId,RentDate,ReturnDate,RentAmount,Id")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                string ?customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                rentService.CreateNewRent(rent,customerId);
                return RedirectToAction("Index", "Cars");
            }
            ViewData["CarId"] = new SelectList(carService.GetCars(), "Id", "Name", rent.CarId);
            return View(rent);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(carService.GetCars(), "Id", "Description", rent.CarId);
            return View(rent);
        }

        // GET: Rents/Delete/5
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
