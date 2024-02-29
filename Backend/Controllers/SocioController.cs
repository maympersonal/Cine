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
    public class SocioController : Controller
    {
        private readonly CineContext _context;

        public SocioController(CineContext context)
        {
            _context = context;
        }

        // GET: Socio
        public async Task<IActionResult> Index()
        {
            var cineContext = _context.Socios.Include(s => s.CiNavigation);
            return View(await cineContext.ToListAsync());
        }

        // GET: Socio/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .Include(s => s.CiNavigation)
                .FirstOrDefaultAsync(m => m.Ci == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // GET: Socio/Create
        public IActionResult Create()
        {
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci");
            return View();
        }

        // POST: Socio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ci,NombreS,Apellidos,Puntos,Codigo,Contrasena")] Socio socio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci", socio.Ci);
            return View(socio);
        }

        // GET: Socio/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios.FindAsync(id);
            if (socio == null)
            {
                return NotFound();
            }
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci", socio.Ci);
            return View(socio);
        }

        // POST: Socio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Ci,NombreS,Apellidos,Puntos,Codigo,Contrasena")] Socio socio)
        {
            if (id != socio.Ci)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocioExists(socio.Ci))
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
            ViewData["Ci"] = new SelectList(_context.Clientes, "Ci", "Ci", socio.Ci);
            return View(socio);
        }

        // GET: Socio/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .Include(s => s.CiNavigation)
                .FirstOrDefaultAsync(m => m.Ci == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // POST: Socio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var socio = await _context.Socios.FindAsync(id);
            if (socio != null)
            {
                _context.Socios.Remove(socio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocioExists(string id)
        {
            return _context.Socios.Any(e => e.Ci == id);
        }
    }
}
