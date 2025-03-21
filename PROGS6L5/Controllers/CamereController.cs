using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROGS6L5.Models;
using PROGS6L5.Data;
using System.Linq;
using System.Threading.Tasks;
using PROGS6L5.ViewModels;

namespace PROGS6L5.Controllers
{
   // [Authorize(Roles = "Amministratore")]
    public class CamereController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CamereController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var camere = await _context.Camere.ToListAsync();
            return View(camere);
        }

      
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CamereViewModel camereViewModel)
        {
            if (ModelState.IsValid)
            {
                var camera = new Camera
                {
                   Numero = camereViewModel.Numero,
                   Tipo = camereViewModel.Tipo,
                   Prezzo = camereViewModel.Prezzo,
                };

                _context.Camere.Add(camera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(camereViewModel);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var camera = await _context.Camere.FindAsync(id);
            if (camera == null) return NotFound();

            return View(camera);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Camera camera)
        {
            if (id != camera.CameraId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Camere.Update(camera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CameraExists(camera.CameraId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(camera);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var camera = await _context.Camere
                .FirstOrDefaultAsync(m => m.CameraId == id);
            if (camera == null) return NotFound();

            return View(camera);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camera = await _context.Camere.FindAsync(id);
            if (camera != null)
            {
                _context.Camere.Remove(camera);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CameraExists(int id)
        {
            return _context.Camere.Any(e => e.CameraId == id);
        }
    }
}
