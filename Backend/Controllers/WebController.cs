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
    public class WebController : Controller
    {
        private readonly CineContext _context;

        public WebController(CineContext context)
        {
            _context = context;
        }

        // GET: Web
        public async Task<IActionResult> Index()
        {
            var cineContext = _context.Webs.Include(w => w.CodigoTNavigation).Include(w => w.IdPgNavigation);
            return View(await cineContext.ToListAsync());
        }

        // GET: Web/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var web = await _context.Webs
                .Include(w => w.CodigoTNavigation)
                .Include(w => w.IdPgNavigation)
                .FirstOrDefaultAsync(m => m.IdPg == id);
            if (web == null)
            {
                return NotFound();
            }

            return View(web);
        }

        // GET: Web/Create
        public IActionResult Create()
        {
            ViewData["CodigoT"] = new SelectList(_context.Tarjeta, "CodigoT", "CodigoT");
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg");
            return View();
        }

        // POST: Web/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPg,CodigoT,Cantidad")] Web web)
        {
            if (ModelState.IsValid)
            {
                _context.Add(web);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoT"] = new SelectList(_context.Tarjeta, "CodigoT", "CodigoT", web.CodigoT);
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", web.IdPg);
            return View(web);
        }

        // GET: Web/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var web = await _context.Webs.FindAsync(id);
            if (web == null)
            {
                return NotFound();
            }
            ViewData["CodigoT"] = new SelectList(_context.Tarjeta, "CodigoT", "CodigoT", web.CodigoT);
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", web.IdPg);
            return View(web);
        }

        // POST: Web/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPg,CodigoT,Cantidad")] Web web)
        {
            if (id != web.IdPg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(web);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebExists(web.IdPg))
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
            ViewData["CodigoT"] = new SelectList(_context.Tarjeta, "CodigoT", "CodigoT", web.CodigoT);
            ViewData["IdPg"] = new SelectList(_context.Pagos, "IdPg", "IdPg", web.IdPg);
            return View(web);
        }

        // GET: Web/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var web = await _context.Webs
                .Include(w => w.CodigoTNavigation)
                .Include(w => w.IdPgNavigation)
                .FirstOrDefaultAsync(m => m.IdPg == id);
            if (web == null)
            {
                return NotFound();
            }

            return View(web);
        }

        // POST: Web/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var web = await _context.Webs.FindAsync(id);
            if (web != null)
            {
                _context.Webs.Remove(web);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebExists(int id)
        {
            return _context.Webs.Any(e => e.IdPg == id);
        }
    }
}
