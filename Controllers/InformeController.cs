using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using impoTrack.Data;
using impoTrack.Models;

namespace impoTrack.Controllers
{
    public class InformeController : Controller
    {
        private readonly impoTrackContext _context;

        public InformeController(impoTrackContext context)
        {
            _context = context;
        }

        // GET: Informe
        public async Task<IActionResult> Index()
        {
            return View(await _context.Informes.ToListAsync());
        }

        // GET: Informe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informe = await _context.Informes
                .FirstOrDefaultAsync(m => m.InformeID == id);
            if (informe == null)
            {
                return NotFound();
            }

            return View(informe);
        }

        // GET: Informe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Informe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InformeID,TipoInforme,FechaGeneracion,DatosInforme")] Informe informe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(informe);
        }

        // GET: Informe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informe = await _context.Informes.FindAsync(id);
            if (informe == null)
            {
                return NotFound();
            }
            return View(informe);
        }

        // POST: Informe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InformeID,TipoInforme,FechaGeneracion,DatosInforme")] Informe informe)
        {
            if (id != informe.InformeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformeExists(informe.InformeID))
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
            return View(informe);
        }

        // GET: Informe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informe = await _context.Informes
                .FirstOrDefaultAsync(m => m.InformeID == id);
            if (informe == null)
            {
                return NotFound();
            }

            return View(informe);
        }

        // POST: Informe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informe = await _context.Informes.FindAsync(id);
            if (informe != null)
            {
                _context.Informes.Remove(informe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformeExists(int id)
        {
            return _context.Informes.Any(e => e.InformeID == id);
        }
    }
}
