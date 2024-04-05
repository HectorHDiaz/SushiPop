using System;
using System.Collections.Generic;
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
    public class CarritoItemsController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CarritoItemsController(DbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: CarritoItems
        public async Task<IActionResult> Index()
        {
            // filtrar por Cliente y carrito Activo

            var user = await _userManager.GetUserAsync(User);

            var cliente = await _context.Cliente.Where(c => c.Email.ToUpper() == user.NormalizedEmail).FirstOrDefaultAsync();
           
            var carritoActivo = await _context.Carrito.Include(c => c.Items).Where(c => c.ClienteId == cliente.Id && !c.Procesado && !c.Cancelado).FirstOrDefaultAsync();

            if (carritoActivo == null )
            {
                
                Carrito carritoNuevo = new Carrito()
                {
                    ClienteId = cliente.Id,
                };

                _context.Add(carritoNuevo);
                await _context.SaveChangesAsync();

                var dbContext =  _context.CarritoItem.Where(c => c.CarritoId == carritoNuevo.Id).Include(c => c.Carrito).Include(c => c.Producto);
                return View(await dbContext.ToListAsync());
            } else
            {
                var dbContext = _context.CarritoItem.Where(c => c.CarritoId == carritoActivo.Id).Include(c => c.Carrito).Include(c => c.Producto);

            return View(await dbContext.ToListAsync());
            }

        }

        // GET: CarritoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarritoItem == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem
                .Include(c => c.Carrito)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carritoItem == null)
            {
                return NotFound();
            }

            return View(carritoItem);
        }

        // GET: CarritoItems/Create
        [Authorize(Roles = "CLIENTE")] 
        public IActionResult Create()
        {
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Id");
            return View();
        }

        // POST: CarritoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CLIENTE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PrecioUnitarioConDescuento,Cantidad,CarritoId,ProductoId")] CarritoItem carritoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carritoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", carritoItem.CarritoId);
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Id", carritoItem.ProductoId);
            return View(carritoItem);
        }

        // GET: CarritoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarritoItem == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem.FindAsync(id);
            if (carritoItem == null)
            {
                return NotFound();
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", carritoItem.CarritoId);
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Id", carritoItem.ProductoId);
            return View(carritoItem);
        }

        // POST: CarritoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrecioUnitarioConDescuento,Cantidad,CarritoId,ProductoId")] CarritoItem carritoItem)
        {
            if (id != carritoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoItemExists(carritoItem.Id))
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
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", carritoItem.CarritoId);
            ViewData["ProductoId"] = new SelectList(_context.Set<Producto>(), "Id", "Id", carritoItem.ProductoId);
            return View(carritoItem);
        }

        // GET: CarritoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarritoItem == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem
                .Include(c => c.Carrito)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            await _context.SaveChangesAsync();

            if (carritoItem == null)
            {
                return NotFound();
            }

            return View(carritoItem);
        }

        // POST: CarritoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarritoItem == null)
            {
                return Problem("Entity set 'DbContext.CarritoItem'  is null.");
            }
            var carritoItem = await _context.CarritoItem.FindAsync(id);
            var carrito = await _context.Carrito.FindAsync(carritoItem.CarritoId);
            if (carritoItem != null)
            {
                var producto = await _context.Producto.Where(p => p.Id == carritoItem.ProductoId).FirstOrDefaultAsync();
                producto.Stock += carritoItem.Cantidad;
                carrito.Items.Remove(carritoItem);
                _context.Update(producto);
                _context.CarritoItem.Remove(carritoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoItemExists(int id)
        {
          return (_context.CarritoItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
