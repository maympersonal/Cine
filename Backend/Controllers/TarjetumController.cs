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
    public class TarjetumController : Controller
    {
        private readonly CineContext _context;

        public TarjetumController(CineContext context)
        {
            _context = context;
        }

        // GET: Tarjetum
        public async Task<IActionResult> Index()
        {
            var cineContext = _context.Tarjeta.Include(t => t.CiNavigation);
            return View(await cineContext.ToListAsync());
        }

        // GET: Tarjetum/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjetum = await _context.Tarjeta
                .Include(t => t.CiNavigation)
                .FirstOrDefaultAsync(m => m.CodigoT == id);
            if (tarjetum == null)
            {
                return NotFound();
            }

            return View(tarjetum);
        }

        // GET: Tarjetum/Create
        public IActionResult Create()
        {
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci");
            return View();
        }

        // POST: Tarjetum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoT,Ci")] Tarjetum tarjetum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarjetum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci", tarjetum.Ci);
            return View(tarjetum);
        }

        // GET: Tarjetum/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjetum = await _context.Tarjeta.FindAsync(id);
            if (tarjetum == null)
            {
                return NotFound();
            }
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci", tarjetum.Ci);
            return View(tarjetum);
        }

        // POST: Tarjetum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoT,Ci")] Tarjetum tarjetum)
        {
            if (id != tarjetum.CodigoT)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarjetum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarjetumExists(tarjetum.CodigoT))
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
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci", tarjetum.Ci);
            return View(tarjetum);
        }

        // GET: Tarjetum/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarjetum = await _context.Tarjeta
                .Include(t => t.CiNavigation)
                .FirstOrDefaultAsync(m => m.CodigoT == id);
            if (tarjetum == null)
            {
                return NotFound();
            }

            return View(tarjetum);
        }

        // POST: Tarjetum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tarjetum = await _context.Tarjeta.FindAsync(id);
            if (tarjetum != null)
            {
                _context.Tarjeta.Remove(tarjetum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarjetumExists(string id)
        {
            return _context.Tarjeta.Any(e => e.CodigoT == id);
        }
    }
}
