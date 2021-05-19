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
    public class HistoricoEstadoesController : Controller
    {
        private readonly testContext _context;

        public HistoricoEstadoesController(testContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "operador,admin")]
        // GET: HistoricoEstadoes
        public async Task<IActionResult> Index()
        {
            var testContext = _context.HistoricoEstados.Include(h => h.EstadoNavigation).Include(h => h.OperadorNavigation).Include(h => h.PeticionNavigation);
            return View(await testContext.ToListAsync());
        }
        [Authorize(Roles = "operador,admin")]
        // GET: HistoricoEstadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoEstado = await _context.HistoricoEstados
                .Include(h => h.EstadoNavigation)
                .Include(h => h.OperadorNavigation)
                .Include(h => h.PeticionNavigation)
                .FirstOrDefaultAsync(m => m.Operador == id);
            if (historicoEstado == null)
            {
                return NotFound();
            }

            return View(historicoEstado);
        }
        [Authorize(Roles = "operador,admin")]
        // GET: HistoricoEstadoes/Create
        public IActionResult Create()
        {
            ViewData["Estado"] = new SelectList(_context.Estados, "IdEstado", "NombreEstado");
            ViewData["Operador"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            ViewData["Peticion"] = new SelectList(_context.Peticions, "IdPeticion", "LocalizacionPeticion");
            return View();
        }

        // POST: HistoricoEstadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FechaModificacion,Estado,Peticion,Operador")] HistoricoEstado historicoEstado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historicoEstado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "IdEstado", "NombreEstado", historicoEstado.Estado);
            ViewData["Operador"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", historicoEstado.Operador);
            ViewData["Peticion"] = new SelectList(_context.Peticions, "IdPeticion", "LocalizacionPeticion", historicoEstado.Peticion);
            return View(historicoEstado);
        }
        [Authorize(Roles = "operador,admin")]
        // GET: HistoricoEstadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoEstado = await _context.HistoricoEstados.FindAsync(id);
            if (historicoEstado == null)
            {
                return NotFound();
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "IdEstado", "NombreEstado", historicoEstado.Estado);
            ViewData["Operador"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", historicoEstado.Operador);
            ViewData["Peticion"] = new SelectList(_context.Peticions, "IdPeticion", "LocalizacionPeticion", historicoEstado.Peticion);
            return View(historicoEstado);
        }

        // POST: HistoricoEstadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FechaModificacion,Estado,Peticion,Operador")] HistoricoEstado historicoEstado)
        {
            if (id != historicoEstado.Operador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historicoEstado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricoEstadoExists(historicoEstado.Operador))
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
            ViewData["Estado"] = new SelectList(_context.Estados, "IdEstado", "NombreEstado", historicoEstado.Estado);
            ViewData["Operador"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", historicoEstado.Operador);
            ViewData["Peticion"] = new SelectList(_context.Peticions, "IdPeticion", "LocalizacionPeticion", historicoEstado.Peticion);
            return View(historicoEstado);
        }
        [Authorize(Roles = "operador")]
        // GET: HistoricoEstadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoEstado = await _context.HistoricoEstados
                .Include(h => h.EstadoNavigation)
                .Include(h => h.OperadorNavigation)
                .Include(h => h.PeticionNavigation)
                .FirstOrDefaultAsync(m => m.Operador == id);
            if (historicoEstado == null)
            {
                return NotFound();
            }

            return View(historicoEstado);
        }

        // POST: HistoricoEstadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historicoEstado = await _context.HistoricoEstados.FindAsync(id);
            _context.HistoricoEstados.Remove(historicoEstado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoricoEstadoExists(int id)
        {
            return _context.HistoricoEstados.Any(e => e.Operador == id);
        }
    }
}
