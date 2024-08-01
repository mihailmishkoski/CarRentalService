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
    public class ReturnsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReturnsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Returns
        public async Task<IActionResult> Index()
        {
            return View(await _context.Returns.ToListAsync());
        }

        // GET: Returns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @return = await _context.Returns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // GET: Returns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Returns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentId,ReturnDate,LateFee,Id")] Return @return)
        {
            if (ModelState.IsValid)
            {
                @return.Id = Guid.NewGuid();
                _context.Add(@return);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@return);
        }

        // GET: Returns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @return = await _context.Returns.FindAsync(id);
            if (@return == null)
            {
                return NotFound();
            }
            return View(@return);
        }

        // POST: Returns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RentId,ReturnDate,LateFee,Id")] Return @return)
        {
            if (id != @return.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@return);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @return = await _context.Returns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // POST: Returns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var @return = await _context.Returns.FindAsync(id);
            if (@return != null)
            {
                _context.Returns.Remove(@return);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReturnExists(Guid id)
        {
            return _context.Returns.Any(e => e.Id == id);
        }
    }
}
