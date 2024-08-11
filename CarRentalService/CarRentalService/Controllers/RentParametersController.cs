using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalService.Domain.Models;
using CarRentalService.Repository;

namespace CarRentalService.Web.Controllers
{
    public class RentParametersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentParametersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RentParameters
        public async Task<IActionResult> Index()
        {
            return View(await _context.RentParameters.ToListAsync());
        }

        // GET: RentParameters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentParameters = await _context.RentParameters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentParameters == null)
            {
                return NotFound();
            }

            return View(rentParameters);
        }

        // GET: RentParameters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RentParameters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MinimumRentDays,Discount,Id")] RentParameters rentParameters)
        {
            if (ModelState.IsValid)
            {
                rentParameters.Id = Guid.NewGuid();
                _context.Add(rentParameters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rentParameters);
        }

        // GET: RentParameters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentParameters = await _context.RentParameters.FindAsync(id);
            if (rentParameters == null)
            {
                return NotFound();
            }
            return View(rentParameters);
        }

        // POST: RentParameters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MinimumRentDays,Discount,Id")] RentParameters rentParameters)
        {
            if (id != rentParameters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentParameters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentParametersExists(rentParameters.Id))
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
            return View(rentParameters);
        }

        // GET: RentParameters/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentParameters = await _context.RentParameters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentParameters == null)
            {
                return NotFound();
            }

            return View(rentParameters);
        }

        // POST: RentParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rentParameters = await _context.RentParameters.FindAsync(id);
            if (rentParameters != null)
            {
                _context.RentParameters.Remove(rentParameters);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentParametersExists(Guid id)
        {
            return _context.RentParameters.Any(e => e.Id == id);
        }
    }
}
