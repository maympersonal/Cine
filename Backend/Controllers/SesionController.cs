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
    public class SesionController : Controller
    {
        private readonly CineContext _context;

        public SesionController(CineContext context)
        {
            _context = context;
        }

        // GET: Sesion
        public async Task<IActionResult> Index()
        {
            var cineContext = _context.Sesions.Include(s => s.IdPNavigation).Include(s => s.IdSNavigation);
            return View(await cineContext.ToListAsync());
        }

        // GET: Sesion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesion = await _context.Sesions
                .Include(s => s.IdPNavigation)
                .Include(s => s.IdSNavigation)
                .FirstOrDefaultAsync(m => m.IdP == id);
            if (sesion == null)
            {
                return NotFound();
            }

            return View(sesion);
        }

        // GET: Sesion/Create
        public IActionResult Create()
        {
            ViewData["IdP"] = new SelectList(_context.Peliculas, "IdP", "IdP");
            ViewData["IdS"] = new SelectList(_context.Salas, "IdS", "IdS");
            return View();
        }

        // POST: Sesion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdP,IdS,Fecha")] Sesion sesion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sesion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdP"] = new SelectList(_context.Peliculas, "IdP", "IdP", sesion.IdP);
            ViewData["IdS"] = new SelectList(_context.Salas, "IdS", "IdS", sesion.IdS);
            return View(sesion);
        }

        // GET: Sesion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesion = await _context.Sesions.FindAsync(id);
            if (sesion == null)
            {
                return NotFound();
            }
            ViewData["IdP"] = new SelectList(_context.Peliculas, "IdP", "IdP", sesion.IdP);
            ViewData["IdS"] = new SelectList(_context.Salas, "IdS", "IdS", sesion.IdS);
            return View(sesion);
        }

        // POST: Sesion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdP,IdS,Fecha")] Sesion sesion)
        {
            if (id != sesion.IdP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sesion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesionExists(sesion.IdP))
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
            ViewData["IdP"] = new SelectList(_context.Peliculas, "IdP", "IdP", sesion.IdP);
            ViewData["IdS"] = new SelectList(_context.Salas, "IdS", "IdS", sesion.IdS);
            return View(sesion);
        }

        // GET: Sesion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesion = await _context.Sesions
                .Include(s => s.IdPNavigation)
                .Include(s => s.IdSNavigation)
                .FirstOrDefaultAsync(m => m.IdP == id);
            if (sesion == null)
            {
                return NotFound();
            }

            return View(sesion);
        }

        // POST: Sesion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sesion = await _context.Sesions.FindAsync(id);
            if (sesion != null)
            {
                _context.Sesions.Remove(sesion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SesionExists(int id)
        {
            return _context.Sesions.Any(e => e.IdP == id);
        }
    }
}
