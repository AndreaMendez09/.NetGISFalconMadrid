using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Net_Gis_Falcon;
using NpgsqlTypes;

namespace Net_Gis_Falcon.Controllers
{
    public class PeticionsController : Controller
    {
        private readonly testContext _context;

        public PeticionsController(testContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "usuario,operador")]
        // GET: Peticions
        public async Task<IActionResult> Index()
        {
           
            var testContext = _context.Peticions.Include(p => p.UsuarioNavigation);
            return View(await testContext.ToListAsync());
        }
        [Authorize]
        // GET: Peticions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peticion = await _context.Peticions
                .Include(p => p.UsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdPeticion == id);
            if (peticion == null)
            {
                return NotFound();
            }

            return View(peticion);
        }
        [Authorize(Roles = "usuario")]
        // GET: Peticions/Create
        public IActionResult Create()
        {
            //Necesario una vista personalizada
            //O quitarle el campo de usuario y el de posicion, y en la query añadirlos
            //La posicion debe obtenerse automaticamente al iniciar la aplicacion, guardar en una cookie
            //Al crear una nueva peticion directamente mostrar las preguntas.
                ViewData["Usuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            return View();
        }

        // POST: Peticions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPeticion,FechaCreacion,LocalizacionPeticion,Coordenadas,PrecisionPeticion,Usuario")] Peticion peticion)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("kxrxfcgxurjcjygcjgtrcktrckyc kytciytdiytdkytc");
                Console.WriteLine(peticion.Coordenadas);

                string[] array = peticion.Coordenadas.Split(",");
                array[0] = array[0].Replace(".", ",");
                array[1] = array[1].Replace(".", ",");
                Console.WriteLine(peticion.Coordenadas);
                var point = new NpgsqlPoint(Convert.ToDouble(array[0]), Convert.ToDouble(array[1]));
                peticion.LocalizacionPeticion = point;
                _context.Add(peticion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["Usuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", peticion.Usuario);
            return View(peticion);
        }
        [Authorize]
        // GET: Peticions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peticion = await _context.Peticions.FindAsync(id);
            if (peticion == null)
            {
                return NotFound();
            }
            ViewData["Usuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", peticion.Usuario);
            return View(peticion);
        }

        // POST: Peticions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPeticion,FechaCreacion,LocalizacionPeticion,Usuario")] Peticion peticion)
        {
            if (id != peticion.IdPeticion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peticion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeticionExists(peticion.IdPeticion))
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
            ViewData["Usuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", peticion.Usuario);
            return View(peticion);
        }
        [Authorize]
        // GET: Peticions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peticion = await _context.Peticions
                .Include(p => p.UsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdPeticion == id);
            if (peticion == null)
            {
                return NotFound();
            }

            return View(peticion);
        }

        // POST: Peticions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var peticion = await _context.Peticions.FindAsync(id);
            _context.Peticions.Remove(peticion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeticionExists(int id)
        {
            return _context.Peticions.Any(e => e.IdPeticion == id);
        }
    }
}
