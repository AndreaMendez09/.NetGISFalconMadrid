using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_NetGIS.Data;
using MVC_NetGIS.Models;

namespace MVC_NetGIS.Controllers
{
    public class PreguntasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreguntasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Preguntas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Preguntas.ToListAsync());
        }

        // GET: Preguntas/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Preguntas/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String PalabraABuscar) //Esto el name del input
        {
            //El where filtra la lista
            return View("Index",await _context.Preguntas.Where(j=> j.Question.Contains(PalabraABuscar)).ToListAsync());
        }

        // GET: Preguntas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preguntas = await _context.Preguntas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preguntas == null)
            {
                return NotFound();
            }

            return View(preguntas);
        }

        // GET: Preguntas/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Preguntas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] Preguntas preguntas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preguntas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preguntas);
        }

        // GET: Preguntas/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preguntas = await _context.Preguntas.FindAsync(id);
            if (preguntas == null)
            {
                return NotFound();
            }
            return View(preguntas);
        }

        // POST: Preguntas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] Preguntas preguntas)
        {
            if (id != preguntas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preguntas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreguntasExists(preguntas.Id))
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
            return View(preguntas);
        }

        // GET: Preguntas/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preguntas = await _context.Preguntas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preguntas == null)
            {
                return NotFound();
            }

            return View(preguntas);
        }

        // POST: Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preguntas = await _context.Preguntas.FindAsync(id);
            _context.Preguntas.Remove(preguntas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreguntasExists(int id)
        {
            return _context.Preguntas.Any(e => e.Id == id);
        }
    }
}
