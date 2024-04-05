using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SushiPOP_BE1B_2C2023_G1.Models;
using Microsoft.AspNetCore.Authorization;
namespace SushiPOP_BE1B_2C2023_G1.Controllers
{
    public class ReclamosController : Controller
    {
        private readonly DbContext _context;

        public ReclamosController(DbContext context)
        {
            _context = context;
        }

        // GET: Reclamos
        [Authorize(Roles = "EMPLEADO")]
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.Reclamo.Include(r => r.Pedido);
            return View(await dbContext.ToListAsync());
        }

        // GET: Reclamos/Details/5
        // CLIENTE SOLO PUEDE VER SU PROPIO RECLAMO (mismo caso que en pedido)
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reclamo == null)
            {
                return NotFound();
            }

            var reclamo = await _context.Reclamo
                .Include(r => r.Pedido)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reclamo == null)
            {
                return NotFound();
            }

            return View(reclamo);
        }

        // GET: Reclamos/Create
        [Authorize(Roles = "CLIENTE")]
        public IActionResult Create()
        {
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Id");
            return View();
        }

        // POST: Reclamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CLIENTE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCompleto,Email,Telefono,DetalleEnvio,PedidoId")] Reclamo reclamo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reclamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Id", reclamo.PedidoId);
            return View(reclamo);
        }

        // GET: Reclamos/Edit/5
        // Este metodo no se usa.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reclamo == null)
            {
                return NotFound();
            }

            var reclamo = await _context.Reclamo.FindAsync(id);
            if (reclamo == null)
            {
                return NotFound();
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Id", reclamo.PedidoId);
            return View(reclamo);
        }

        // POST: Reclamos/Edit/5
        // Este metodo no se usa.
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,Email,Telefono,DetalleEnvio,PedidoId")] Reclamo reclamo)
        {
            if (id != reclamo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reclamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReclamoExists(reclamo.Id))
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
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Id", reclamo.PedidoId);
            return View(reclamo);
        }

        // GET: Reclamos/Delete/5
        // NO SE PUEDE ELIMINAR UN RECLAMO
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reclamo == null)
            {
                return NotFound();
            }

            var reclamo = await _context.Reclamo
                .Include(r => r.Pedido)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reclamo == null)
            {
                return NotFound();
            }

            return View(reclamo);
        }

        // POST: Reclamos/Delete/5
        // NO SE PUEDE ELIMINAR UN RECLAMO
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reclamo == null)
            {
                return Problem("Entity set 'DbContext.Reclamo'  is null.");
            }
            var reclamo = await _context.Reclamo.FindAsync(id);
            if (reclamo != null)
            {
                _context.Reclamo.Remove(reclamo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReclamoExists(int id)
        {
          return (_context.Reclamo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
