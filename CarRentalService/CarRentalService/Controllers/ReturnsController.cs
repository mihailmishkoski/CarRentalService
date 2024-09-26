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
using System.Security.Claims;
using CarRentalService.Domain.Models.Exceptions;

namespace CarRentalService.Web.Controllers
{
    [Authorize]
    public class ReturnsController : Controller
    {

        private readonly IReturnService _returnService;

        private readonly IRentService _rentService;

        private readonly IRentParamsService _rentParamsService;
        public ReturnsController(IReturnService returnService, IRentService rentService, IRentParamsService rentParamsService)
        {
            _returnService = returnService;
            _rentService = rentService;
            _rentParamsService = rentParamsService;
        }

        // GET: Returns
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_returnService.GetReturns());
        }
        // GET: Returns
        public IActionResult MyReturns()
        {
            string? customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View("Index", _returnService.GetMyReturns(customerId));
        }
        // GET: Returns/Details/5
        [Authorize(Roles = "Admin")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @return = _returnService.GetReturnById(id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // GET: Returns/Create
        
        public IActionResult Create(Guid rentId)
        {
            var rentParams = _rentParamsService.GetRentParams();

            var rent = _rentService.GetRentById(rentId);
            var returnModel = new Return
            {
                RentId = rent.Id,
                Rent = rent,
                ReturnDate = DateTime.Now,
                LateFee = _rentParamsService.CalculateLateFee(rent, DateTime.Now, rentParams.AdditionalFees)
            };

            return View(returnModel);
        }

        // POST: Returns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create([Bind("RentId,ReturnDate,LateFee")] Return @return)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _returnService.CreateNewReturn(@return);
                }
                catch(ReturnException ex)
                {
                    ModelState.AddModelError("ReturnErrorMessage", ex.Message);
                }
                if (User.IsInRole("Admin"))
                {
                    // Redirect to the Returns Index if the user is an admin
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Redirect to the Cars controller's Index if the user is not an admin
                    return RedirectToAction("Index", "Cars");
                }
            }

            return View(@return);
        }

        // GET: Returns/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @return = _returnService.GetReturnById(id);
            if (@return == null)
            {
                return NotFound();
            }
            var rent = _rentService.GetRentById(@return.RentId);
            var returnModel = new Return
            {
                RentId = rent.Id,
                Rent = rent,
                ReturnDate = (DateTime)rent.ReturnDate,
                LateFee = @return.LateFee
            };
            return View(returnModel);
        }

        // POST: Returns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, [Bind("RentId,ReturnDate,LateFee,Id")] Return @return)
        {
            if (id != @return.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var OldReturn = _returnService.GetReturnById(id);
                @return.TotalPrice = OldReturn.TotalPrice;
                try
                {
                    _returnService.UpdateReturn(@return);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnExists(@return.Id))
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
            return View(@return);
        }

        // GET: Returns/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @return = _returnService.GetReturnById(id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // POST: Returns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var @return = _returnService.GetReturnById(id);
            if (@return != null)
            {
                _rentService.DeleteRent(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ReturnExists(Guid id)
        {
            return _returnService.GetReturnById(id) != null;
        }
    }
}
