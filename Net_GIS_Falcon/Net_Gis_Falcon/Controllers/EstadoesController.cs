using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Net_Gis_Falcon;

namespace Net_Gis_Falcon.Controllers
{
    public class EstadoesController : Controller
    {
        private readonly testContext _context;

        public EstadoesController(testContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "operador,admin")]
        // GET: Estadoes
        public async Task<IActionResult> Index()
        {
            var testContext = _context.Estados.Include(e => e.PadreNavigation);
            return View(await testContext.ToListAsync());
        }
        [Authorize(Roles = "operador,admin")]
        // GET: Estadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados
                .Include(e => e.PadreNavigation)
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }
        [Authorize(Roles = "admin")]
        // GET: Estadoes/Create
        public IActionResult Create()
        {
            ViewData["Padre"] = new SelectList(_context.Estados, "IdEstado", "NombreEstado");
            return View();
        }

        // POST: Estadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstado,NombreEstado,Esfinal,ColorEstado,Padre")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Padre"] = new SelectList(_context.Estados, "IdEstado", "NombreEstado", estado.Padre);
            return View(estado);
        }
        [Authorize(Roles = "admin")]
        // GET: Estadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }
            ViewData["Padre"] = new SelectList(_context.Estados, "IdEstado", "NombreEstado", estado.Padre);
            return View(estado);
        }

        // POST: Estadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstado,NombreEstado,Esfinal,ColorEstado,Padre")] Estado estado)
        {
            if (id != estado.IdEstado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoExists(estado.IdEstado))
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
            ViewData["Padre"] = new SelectList(_context.Estados, "IdEstado", "NombreEstado", estado.Padre);
            return View(estado);
        }
        [Authorize(Roles = "admin")]
        // GET: Estadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados
                .Include(e => e.PadreNavigation)
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Estadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoExists(int id)
        {
            return _context.Estados.Any(e => e.IdEstado == id);
        }
    }
}
