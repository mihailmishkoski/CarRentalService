﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalService.Domain.Models;
using CarRentalService.Repository;
using System.Runtime.ConstrainedExecution;
using CarRentalService.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using CarRentalService.Domain.Models.Exceptions;


namespace CarRentalService.Web.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        // GET: Cars
        public IActionResult Index()
        {
            return View(carService.GetCars());
        }

        // GET: Cars/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(ColorVehicle)));
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Name,Description,Model,DateManufactured,KilometersTraveled,Color,LicensePlate,PricePerDay,Id")] Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    carService.CreateNewCar(car);
                    return RedirectToAction(nameof(Index));
                }
                catch(CarException ex)
                {
                    ModelState.AddModelError("LicensePlate", ex.Message);
                }
            }
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(ColorVehicle)));
            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(ColorVehicle)), car.Color);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, [Bind("Name,Description,Model,DateManufactured,KilometersTraveled,Color,LicensePlate,PricePerDay,Id")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCar = carService.GetCarById(id);
                    carService.UpdateCar(car);
                }
                catch (CarException ex)
                {
                    ModelState.AddModelError("LicensePlate", ex.Message);
                }
                
            }
            ViewBag.ColorOptions = new SelectList(Enum.GetValues(typeof(ColorVehicle)), car.Color);
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var car = carService.GetCarById(id);
            if (car != null)
            {
                carService.DeleteCar(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(Guid id)
        {
            return carService.GetCarById(id) != null;
        }
    }
}
