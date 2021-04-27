using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Net_Gis_Falcon;

namespace Net_Gis_Falcon.Controllers
{
    public class PersonasistemasController : Controller
    {
        private readonly testContext _context;

        public PersonasistemasController(testContext context)
        {
            _context = context;
        }

        // GET: Personasistemas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personasistemas.ToListAsync());
        }

        // GET: Personasistemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personasistema = await _context.Personasistemas
                .FirstOrDefaultAsync(m => m.IdPersona == id);
            if (personasistema == null)
            {
                return NotFound();
            }

            return View(personasistema);
        }

        // GET: Personasistemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personasistemas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPersona,Nombre,Apellido,Email,Genero,Idioma,Contraseña,Foto,Rol,Zona")] Personasistema personasistema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personasistema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personasistema);
        }

        // GET: Personasistemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personasistema = await _context.Personasistemas.FindAsync(id);
            if (personasistema == null)
            {
                return NotFound();
            }
            return View(personasistema);
        }

        // POST: Personasistemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPersona,Nombre,Apellido,Email,Genero,Idioma,Contraseña,Foto,Rol,Zona")] Personasistema personasistema)
        {
            if (id != personasistema.IdPersona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personasistema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonasistemaExists(personasistema.IdPersona))
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
            return View(personasistema);
        }

        // GET: Personasistemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personasistema = await _context.Personasistemas
                .FirstOrDefaultAsync(m => m.IdPersona == id);
            if (personasistema == null)
            {
                return NotFound();
            }

            return View(personasistema);
        }

        // POST: Personasistemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personasistema = await _context.Personasistemas.FindAsync(id);
            _context.Personasistemas.Remove(personasistema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonasistemaExists(int id)
        {
            return _context.Personasistemas.Any(e => e.IdPersona == id);
        }
    }
}
