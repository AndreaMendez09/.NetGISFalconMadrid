using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Net_Gis_Falcon
{
    public class RespuestasController : Controller
    {
        private readonly testContext _context;

        public RespuestasController(testContext context)
        {
            _context = context;
        }

        // GET: Respuestas
        public async Task<IActionResult> Index()
        {
            var testContext = _context.Respuesta.Include(r => r.NivelNavigation).Include(r => r.RespuestaPadreNavigation);
            return View(await testContext.ToListAsync());
        }

        // GET: Respuestas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta
                .Include(r => r.NivelNavigation)
                .Include(r => r.RespuestaPadreNavigation)
                .FirstOrDefaultAsync(m => m.IdRespuesta == id);
            if (respuestum == null)
            {
                return NotFound();
            }

            return View(respuestum);
        }

        // GET: Respuestas/Create
        public IActionResult Create()
        {
            ViewData["Nivel"] = new SelectList(_context.Nivels, "IdPregunta", "DescripcionPregunta");
            ViewData["RespuestaPadre"] = new SelectList(_context.Respuesta, "IdRespuesta", "CuerpoRespuesta");
            return View();
        }

        // POST: Respuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRespuesta,CuerpoRespuesta,Principal,Nivel,RespuestaPadre")] Respuestum respuestum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(respuestum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Nivel"] = new SelectList(_context.Nivels, "IdPregunta", "DescripcionPregunta", respuestum.Nivel);
            ViewData["RespuestaPadre"] = new SelectList(_context.Respuesta, "IdRespuesta", "CuerpoRespuesta", respuestum.RespuestaPadre);
            return View(respuestum);
        }

        // GET: Respuestas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta.FindAsync(id);
            if (respuestum == null)
            {
                return NotFound();
            }
            ViewData["Nivel"] = new SelectList(_context.Nivels, "IdPregunta", "DescripcionPregunta", respuestum.Nivel);
            ViewData["RespuestaPadre"] = new SelectList(_context.Respuesta, "IdRespuesta", "CuerpoRespuesta", respuestum.RespuestaPadre);
            return View(respuestum);
        }

        // POST: Respuestas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRespuesta,CuerpoRespuesta,Principal,Nivel,RespuestaPadre")] Respuestum respuestum)
        {
            if (id != respuestum.IdRespuesta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(respuestum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespuestumExists(respuestum.IdRespuesta))
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
            ViewData["Nivel"] = new SelectList(_context.Nivels, "IdPregunta", "DescripcionPregunta", respuestum.Nivel);
            ViewData["RespuestaPadre"] = new SelectList(_context.Respuesta, "IdRespuesta", "CuerpoRespuesta", respuestum.RespuestaPadre);
            return View(respuestum);
        }

        // GET: Respuestas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta
                .Include(r => r.NivelNavigation)
                .Include(r => r.RespuestaPadreNavigation)
                .FirstOrDefaultAsync(m => m.IdRespuesta == id);
            if (respuestum == null)
            {
                return NotFound();
            }

            return View(respuestum);
        }

        // POST: Respuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respuestum = await _context.Respuesta.FindAsync(id);
            _context.Respuesta.Remove(respuestum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RespuestumExists(int id)
        {
            return _context.Respuesta.Any(e => e.IdRespuesta == id);
        }
    }
}
