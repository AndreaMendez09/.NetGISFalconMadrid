using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Net_Gis_Falcon
{
    public class ZonasOperadoresController : Controller
    {
        private readonly testContext _context;

        public ZonasOperadoresController(testContext context)
        {
            _context = context;
        }

        // GET: ZonasOperadores
        public async Task<IActionResult> Index()
        {
            var testContext = _context.OperadoresZonas.Include(o => o.OperadorNavigation).Include(o => o.ZonaNavigation);
            return View(await testContext.ToListAsync());
        }

        // GET: ZonasOperadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operadoresZona = await _context.OperadoresZonas
                .Include(o => o.OperadorNavigation)
                .Include(o => o.ZonaNavigation)
                .FirstOrDefaultAsync(m => m.Operador == id);
            if (operadoresZona == null)
            {
                return NotFound();
            }

            return View(operadoresZona);
        }

        // GET: ZonasOperadores/Create
        public IActionResult Create()
        {
            ViewData["Operador"] = new SelectList(_context.Usuarios.Where(g => g.Rol == 1), "IdUsuario", "Apellido");
            ViewBag.Cuantos = new SelectList(_context.Usuarios.Where(g => g.Rol == 1), "IdUsuario", "Apellido").Count();
            ViewData["Zona"] = new SelectList(_context.Zonas, "IdZona", "NombreZona");
            return View();
        }

        // POST: ZonasOperadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Operador,Zona")] OperadoresZona operadoresZona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operadoresZona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Operador"] = new SelectList(_context.Usuarios.Where(g => g.Rol == 1), "IdUsuario", "Apellido", operadoresZona.Operador);
            ViewData["Zona"] = new SelectList(_context.Zonas, "IdZona", "NombreZona", operadoresZona.Zona);
            return View(operadoresZona);
        }

        // GET: ZonasOperadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operadoresZona = await _context.OperadoresZonas.FindAsync(id);
            if (operadoresZona == null)
            {
                return NotFound();
            }
            ViewData["Operador"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", operadoresZona.Operador);
            ViewData["Zona"] = new SelectList(_context.Zonas, "IdZona", "NombreZona", operadoresZona.Zona);
            return View(operadoresZona);
        }

        // POST: ZonasOperadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Operador,Zona")] OperadoresZona operadoresZona)
        {
            if (id != operadoresZona.Operador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operadoresZona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperadoresZonaExists(operadoresZona.Operador))
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
            ViewData["Operador"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", operadoresZona.Operador);
            ViewData["Zona"] = new SelectList(_context.Zonas, "IdZona", "NombreZona", operadoresZona.Zona);
            return View(operadoresZona);
        }

        // GET: ZonasOperadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operadoresZona = await _context.OperadoresZonas
                .Include(o => o.OperadorNavigation)
                .Include(o => o.ZonaNavigation)
                .FirstOrDefaultAsync(m => m.Operador == id);
            if (operadoresZona == null)
            {
                return NotFound();
            }

            return View(operadoresZona);
        }

        // POST: ZonasOperadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operadoresZona = await _context.OperadoresZonas.FindAsync(id);
            _context.OperadoresZonas.Remove(operadoresZona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperadoresZonaExists(int id)
        {
            return _context.OperadoresZonas.Any(e => e.Operador == id);
        }
    }
}
