using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SushiPOP_BE1B_2C2023_G1.Models;

namespace SushiPOP_BE1B_2C2023_G1.Controllers
{
    public class ReservasController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ReservasController(DbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        // GET: Reservas
        [Authorize(Roles = "EMPLEADO, CLIENTE, ADMIN")] // RN14 = solo empleados pueden ver reservas, UNn cliente solo puede ver su reserva
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("CLIENTE"))
            {
                var usuario = await _userManager.GetUserAsync(User);

                var reservasCliente = await _context.Reserva.Include(r => r.Cliente).Where(r => r.Cliente.Email.ToUpper() == usuario.NormalizedEmail).ToListAsync();

                return View(reservasCliente);
            }

            var dbContext = _context.Reserva.Include(r => r.Cliente);
            return View(await dbContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        [Authorize(Roles = "EMPLEADO, CLIENTE")] // 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        // RN13 = Un usuario cliente no puede tener más de una reserva para un mismo día.
         [Authorize(Roles = "CLIENTE")] // RN12 = solo usuarios clientes pueden realizar reservas
        public async Task<IActionResult> Create(string? nombreLocal)
        {
            var usuario = await _userManager.GetUserAsync(User);
            var cliente = _context.Cliente.FirstOrDefault(c => c.Email.ToUpper() == usuario.NormalizedEmail);

            var reserva = await _context.Reserva.Where(r => r.ClienteId == cliente.Id && r.FechaHora.Date == DateTime.Now.Date).FirstOrDefaultAsync();

            if (reserva == null)
            {
                Reserva nuevaReserva = new Reserva()
                {
                    Local = nombreLocal,
                    ClienteId = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido
                };  

            return View(nuevaReserva);
            } else
            {
                return NotFound("RN13 = Un usuario cliente no puede tener más de una reserva para un mismo día.");
            }


        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CLIENTE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Local,FechaHora,Confirmada,Nombre,Apellido,ClienteId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                if(reserva.FechaHora < DateTime.Now)
                {
                    return NotFound("La fecha no puede ser anterior a hoy");
                }
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Id", reserva.ClienteId);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        [Authorize(Roles = "EMPLEADO")] // RN14 = solo empleados pueden ver reservas, CHECKEAR PDF
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null || reserva.Confirmada)
            {
                return NotFound("La reserva es nula o ya ha sido confirmada");
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Id", reserva.ClienteId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // RN15 = Una vez confirmada la reserva, no es posible editarla.
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Local,FechaHora,Confirmada,Nombre,Apellido,ClienteId")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Id", reserva.ClienteId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reserva == null)
            {
                return Problem("Entity set 'DbContext.Reserva'  is null.");
            }
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva != null)
            {
                _context.Reserva.Remove(reserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
          return (_context.Reserva?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
