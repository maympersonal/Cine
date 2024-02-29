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
    public class EfectivoController : Controller
    {
        private readonly CineContext _context;

        public EfectivoController(CineContext context)
        {
            _context = context;
        }

        // GET: Efectivo
        public async Task<IActionResult> Index()
        {
            var cineContext = _context.Efectivos.Include(e => e.IdPgNavigation);
            return View(await cineContext.ToListAsync());
        }

        // GET: Efectivo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var efectivo = await _context.Efectivos
                .Include(e => e.IdPgNavigation)
                .FirstOrDefaultAsync(m => m.IdPg == id);
            if (efectivo == null)
            {
                return NotFound();
            }

            return View(efectivo);
        }

        // GET: Efectivo/Create
        public IActionResult Create()
        {
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg");
            return View();
        }

        // POST: Efectivo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPg,CantidadE")] Efectivo efectivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(efectivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", efectivo.IdPg);
            return View(efectivo);
        }

        // GET: Efectivo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var efectivo = await _context.Efectivos.FindAsync(id);
            if (efectivo == null)
            {
                return NotFound();
            }
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", efectivo.IdPg);
            return View(efectivo);
        }

        // POST: Efectivo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPg,CantidadE")] Efectivo efectivo)
        {
            if (id != efectivo.IdPg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(efectivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EfectivoExists(efectivo.IdPg))
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
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", efectivo.IdPg);
            return View(efectivo);
        }

        // GET: Efectivo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var efectivo = await _context.Efectivos
                .Include(e => e.IdPgNavigation)
                .FirstOrDefaultAsync(m => m.IdPg == id);
            if (efectivo == null)
            {
                return NotFound();
            }

            return View(efectivo);
        }

        // POST: Efectivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var efectivo = await _context.Efectivos.FindAsync(id);
            if (efectivo != null)
            {
                _context.Efectivos.Remove(efectivo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EfectivoExists(int id)
        {
            return _context.Efectivos.Any(e => e.IdPg == id);
        }
    }
}
