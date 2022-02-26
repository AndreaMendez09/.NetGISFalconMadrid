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
    public class ZonasController : Controller
    {
        private readonly testContext _context;

        public ZonasController(testContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "operador,admin")]
        // GET: Zonas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zonas.ToListAsync());
        }

        // GET: Zonas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zona = await _context.Zonas
                .FirstOrDefaultAsync(m => m.IdZona == id);
            if (zona == null)
            {
                return NotFound();
            }

            return View(zona);
        }
        [Authorize(Roles = "admin")]
        // GET: Zonas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zonas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZona,NombreZona,DescripcionZona,Coordenadas,GeometriaZona")] Zona zona)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(zona.Coordenadas);
                Console.WriteLine(zona.DescripcionZona);

                var point = new NpgsqlPoint();

                string[] array = zona.Coordenadas.Split(": ");
                var polygon = new NpgsqlPolygon(array.Length - 1);
                Console.WriteLine(array);
                string[] array2 = new string[(array.Length-1)*2];

                for (int i = 0; i < array.Length-1; i++)
                {
                    array2 = array[i].Split(",");
                    Console.WriteLine(array2[0]);
                    Console.WriteLine(array2[1]);
                    array2[0] = array2[0].Replace(".",",");
                    array2[1] = array2[1].Replace(".", ",");
                    point = new NpgsqlPoint(Convert.ToDouble(array2[0]), Convert.ToDouble(array2[1]));
                    polygon.Insert(i, point);
                }
                zona.GeometriaZona = polygon;
                _context.Add(zona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zona);
        }
        [Authorize(Roles = "admin")]
        // GET: Zonas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zona = await _context.Zonas.FindAsync(id);
            if (zona == null)
            {
                return NotFound();
            }
            return View(zona);
        }

        // POST: Zonas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdZona,NombreZona,DescripcionZona,GeometriaZona")] Zona zona)
        {
            if (id != zona.IdZona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZonaExists(zona.IdZona))
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
            return View(zona);
        }
        [Authorize(Roles = "admin")]
        // GET: Zonas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zona = await _context.Zonas
                .FirstOrDefaultAsync(m => m.IdZona == id);
            if (zona == null)
            {
                return NotFound();
            }

            return View(zona);
        }

        // POST: Zonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zona = await _context.Zonas.FindAsync(id);
            _context.Zonas.Remove(zona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZonaExists(int id)
        {
            return _context.Zonas.Any(e => e.IdZona == id);
        }
    }
}
