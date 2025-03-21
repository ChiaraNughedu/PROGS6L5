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
    //[Authorize(Roles = "Amministratore,User")]
    public class PrenotazioniController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrenotazioniController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            
            var prenotazioni = _context.Prenotazioni
                .Include(p => p.Cliente)
                .Include(p => p.Camera);

            return View(await prenotazioni.ToListAsync());
        }

        public IActionResult Create()
        {
            
            ViewData["ClienteId"] = new SelectList(_context.Clienti, "ClienteId", "Email"); 
            ViewData["CameraId"] = new SelectList(_context.Camere, "CameraId", "Numero");   
            return View();
        }

        
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
           
            ViewData["ClienteId"] = new SelectList(_context.Clienti, "ClienteId", "Email", prenotazione.ClienteId);
            ViewData["CameraId"] = new SelectList(_context.Camere, "CameraId", "Numero", prenotazione.CameraId);
            return View(prenotazione);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione == null) return NotFound();

            ViewData["ClienteId"] = new SelectList(_context.Clienti, "ClienteId", "Email", prenotazione.ClienteId);
            ViewData["CameraId"] = new SelectList(_context.Camere, "CameraId", "Numero", prenotazione.CameraId);
            return View(prenotazione);
        }

    
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
            
            ViewData["ClienteId"] = new SelectList(_context.Clienti, "ClienteId", "Email", prenotazione.ClienteId);
            ViewData["CameraId"] = new SelectList(_context.Camere, "CameraId", "Numero", prenotazione.CameraId);
            return View(prenotazione);
        }

        
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
