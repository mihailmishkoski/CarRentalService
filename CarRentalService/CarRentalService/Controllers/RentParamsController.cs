using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalService.Domain.Models;
using CarRentalService.Repository;
using CarRentalService.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using CarRentalService.Domain.Models.Exceptions;

namespace CarRentalService.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RentParamsController : Controller
    {
        private readonly IRentParamsService _paramsService;

        public RentParamsController(IRentParamsService paramsService)
        {
            _paramsService = paramsService;
        }

        // GET: RentParams/Edit/5
        public IActionResult Index(string ?errorMessage)
        {
            var rentParams = _paramsService.GetRentParams();
            if (rentParams == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewData["ErrorMessage"] = errorMessage;
            }
            return View(rentParams);
        }

        // POST: RentParams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Guid id, [Bind("Id,MinimumDaysForRent,DiscountPercentage,AdditionalFees")] RentParams rentParams)
        {
            if (id != rentParams.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _paramsService.UpdateRentParams(rentParams);
                }
                catch (RentParamsException ex)
                {
                    return RedirectToAction("Create", new {errorMessage = ex.Message });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rentParams);
        }

        

        private bool RentParamsExists(Guid id)
        {
            return _paramsService.GetRentParams() != null;
        }
    }
}
