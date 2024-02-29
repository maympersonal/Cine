using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    public class ButacaController : Controller
    {
        private readonly CineContext _context;

        public ButacaController(CineContext context)
        {
            _context = context;
        }

        // GET: Butaca
        public async Task<IActionResult> Index()
        {
            return View(await _context.Butacas.ToListAsync());
        }

        // GET: Butaca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var butaca = await _context.Butacas
                .FirstOrDefaultAsync(m => m.IdB == id);
            if (butaca == null)
            {
                return NotFound();
            }

            return View(butaca);
        }

        // GET: Butaca/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Butaca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdB,IdS")] Butaca butaca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(butaca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(butaca);
        }

        // GET: Butaca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var butaca = await _context.Butacas.FindAsync(id);
            if (butaca == null)
            {
                return NotFound();
            }
            return View(butaca);
        }

        // POST: Butaca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdB,IdS")] Butaca butaca)
        {
            if (id != butaca.IdB)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(butaca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ButacaExists(butaca.IdB))
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
            return View(butaca);
        }

        // GET: Butaca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var butaca = await _context.Butacas
                .FirstOrDefaultAsync(m => m.IdB == id);
            if (butaca == null)
            {
                return NotFound();
            }

            return View(butaca);
        }

        // POST: Butaca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var butaca = await _context.Butacas.FindAsync(id);
            if (butaca != null)
            {
                _context.Butacas.Remove(butaca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ButacaExists(int id)
        {
            return _context.Butacas.Any(e => e.IdB == id);
        }
    }
}
