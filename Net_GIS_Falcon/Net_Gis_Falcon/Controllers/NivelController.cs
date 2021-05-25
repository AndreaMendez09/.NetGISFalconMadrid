using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Net_Gis_Falcon
{
    public class NivelController : Controller
    {
        private readonly testContext _context;

        public NivelController(testContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin")]
        // GET: Nivel
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nivels.ToListAsync());
        }

        // GET: Nivel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivel = await _context.Nivels
                .FirstOrDefaultAsync(m => m.IdPregunta == id);
            if (nivel == null)
            {
                return NotFound();
            }

            return View(nivel);
        }
        [Authorize(Roles = "admin")]
        // GET: Nivel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nivel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPregunta,DescripcionPregunta,Nivel1")] Nivel nivel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nivel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nivel);
        }
        [Authorize(Roles = "admin")]
        // GET: Nivel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivel = await _context.Nivels.FindAsync(id);
            if (nivel == null)
            {
                return NotFound();
            }
            return View(nivel);
        }

        // POST: Nivel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPregunta,DescripcionPregunta,Nivel1")] Nivel nivel)
        {
            if (id != nivel.IdPregunta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nivel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NivelExists(nivel.IdPregunta))
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
            return View(nivel);
        }
        [Authorize(Roles = "admin")]
        // GET: Nivel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivel = await _context.Nivels
                .FirstOrDefaultAsync(m => m.IdPregunta == id);
            if (nivel == null)
            {
                return NotFound();
            }

            return View(nivel);
        }

        // POST: Nivel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nivel = await _context.Nivels.FindAsync(id);
            _context.Nivels.Remove(nivel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NivelExists(int id)
        {
            return _context.Nivels.Any(e => e.IdPregunta == id);
        }

    }
}
