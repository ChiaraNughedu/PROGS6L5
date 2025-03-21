using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROGS6L5.Models;
using PROGS6L5.Data;

using System.Linq;
using System.Threading.Tasks;

namespace PROGS6L5.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class PrenotazioniController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrenotazioniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prenotazioni
        public async Task<IActionResult> Index()
        {
            // Carica Cliente e Camera associati
            var prenotazioni = _context.Prenotazioni
                .Include(p => p.Cliente)
                .Include(p => p.Camera);

            return View(await prenotazioni.ToListAsync());
        }

        // GET: Prenotazioni/Create
        public IActionResult Create()
        {
            // Popoliamo le select per Cliente e Camera
            ViewData["ClienteId"] = new SelectList(_context.Clienti, "ClienteId", "Email");  // Oppure "Nome"
            ViewData["CameraId"] = new SelectList(_context.Camere, "CameraId", "Numero");   // Oppure "Tipo"
            return View();
        }

        // POST: Prenotazioni/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prenotazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Se ModelState non è valido, ricarichiamo le select
            ViewData["ClienteId"] = new SelectList(_context.Clienti, "ClienteId", "Email", prenotazione.ClienteId);
            ViewData["CameraId"] = new SelectList(_context.Camere, "CameraId", "Numero", prenotazione.CameraId);
            return View(prenotazione);
        }

        // GET: Prenotazioni/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione == null) return NotFound();

            ViewData["ClienteId"] = new SelectList(_context.Clienti, "ClienteId", "Email", prenotazione.ClienteId);
            ViewData["CameraId"] = new SelectList(_context.Camere, "CameraId", "Numero", prenotazione.CameraId);
            return View(prenotazione);
        }

        // POST: Prenotazioni/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prenotazione prenotazione)
        {
            if (id != prenotazione.PrenotazioneId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prenotazione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrenotazioneExists(prenotazione.PrenotazioneId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            // Se ModelState non è valido, ricarichiamo le select
            ViewData["ClienteId"] = new SelectList(_context.Clienti, "ClienteId", "Email", prenotazione.ClienteId);
            ViewData["CameraId"] = new SelectList(_context.Camere, "CameraId", "Numero", prenotazione.CameraId);
            return View(prenotazione);
        }

        // GET: Prenotazioni/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var prenotazione = await _context.Prenotazioni
                .Include(p => p.Cliente)
                .Include(p => p.Camera)
                .FirstOrDefaultAsync(m => m.PrenotazioneId == id);

            if (prenotazione == null) return NotFound();

            return View(prenotazione);
        }

        // POST: Prenotazioni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione != null)
            {
                _context.Prenotazioni.Remove(prenotazione);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PrenotazioneExists(int id)
        {
            return _context.Prenotazioni.Any(e => e.PrenotazioneId == id);
        }
    }
}
