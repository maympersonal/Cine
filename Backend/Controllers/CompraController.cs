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
    public class CompraController : Controller
    {
        private readonly CineContext _context;

        public CompraController(CineContext context)
        {
            _context = context;
        }

        // GET: Compra
        public async Task<IActionResult> Index()
        {
            var cineContext = _context.Compras.Include(c => c.CiNavigation).Include(c => c.IdPgNavigation).Include(c => c.Sesion);
            return View(await cineContext.ToListAsync());
        }

        // GET: Compra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.CiNavigation)
                .Include(c => c.IdPgNavigation)
                .Include(c => c.Sesion)
                .FirstOrDefaultAsync(m => m.IdP == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compra/Create
        public IActionResult Create()
        {
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci");
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg");
            ViewData["IdP"] = new SelectList(_context.Sesions, "IdP", "IdP");
            return View();
        }

        // POST: Compra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdP,IdS,Fecha,Ci,IdPg")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci", compra.Ci);
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", compra.IdPg);
            ViewData["IdP"] = new SelectList(_context.Sesions, "IdP", "IdP", compra.IdP);
            return View(compra);
        }

        // GET: Compra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci", compra.Ci);
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", compra.IdPg);
            ViewData["IdP"] = new SelectList(_context.Sesions, "IdP", "IdP", compra.IdP);
            return View(compra);
        }

        // POST: Compra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdP,IdS,Fecha,Ci,IdPg")] Compra compra)
        {
            if (id != compra.IdP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.IdP))
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
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci", compra.Ci);
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", compra.IdPg);
            ViewData["IdP"] = new SelectList(_context.Sesions, "IdP", "IdP", compra.IdP);
            return View(compra);
        }

        // GET: Compra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.CiNavigation)
                .Include(c => c.IdPgNavigation)
                .Include(c => c.Sesion)
                .FirstOrDefaultAsync(m => m.IdP == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.IdP == id);
        }
    }
}
