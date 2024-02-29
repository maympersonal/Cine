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
    public class PuntoController : Controller
    {
        private readonly CineContext _context;

        public PuntoController(CineContext context)
        {
            _context = context;
        }

        // GET: Punto
        public async Task<IActionResult> Index()
        {
            var cineContext = _context.Puntos.Include(p => p.IdPgNavigation);
            return View(await cineContext.ToListAsync());
        }

        // GET: Punto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punto = await _context.Puntos
                .Include(p => p.IdPgNavigation)
                .FirstOrDefaultAsync(m => m.IdPg == id);
            if (punto == null)
            {
                return NotFound();
            }

            return View(punto);
        }

        // GET: Punto/Create
        public IActionResult Create()
        {
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg");
            return View();
        }

        // POST: Punto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPg,Gastados")] Punto punto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(punto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", punto.IdPg);
            return View(punto);
        }

        // GET: Punto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punto = await _context.Puntos.FindAsync(id);
            if (punto == null)
            {
                return NotFound();
            }
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", punto.IdPg);
            return View(punto);
        }

        // POST: Punto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPg,Gastados")] Punto punto)
        {
            if (id != punto.IdPg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(punto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuntoExists(punto.IdPg))
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
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", punto.IdPg);
            return View(punto);
        }

        // GET: Punto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punto = await _context.Puntos
                .Include(p => p.IdPgNavigation)
                .FirstOrDefaultAsync(m => m.IdPg == id);
            if (punto == null)
            {
                return NotFound();
            }

            return View(punto);
        }

        // POST: Punto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var punto = await _context.Puntos.FindAsync(id);
            if (punto != null)
            {
                _context.Puntos.Remove(punto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuntoExists(int id)
        {
            return _context.Puntos.Any(e => e.IdPg == id);
        }
    }
}
