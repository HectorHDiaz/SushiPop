using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SushiPOP_BE1B_2C2023_G1.Models;

namespace SushiPOP_BE1B_2C2023_G1.Controllers
{
    public class ProductosController : Controller
    {
        private readonly DbContext _context;

        public ProductosController(DbContext context)
        {
            _context = context;
        }

        // GET: Productos
        // RN27. No es necesario iniciar sesión para ver los productos.

        public async Task<IActionResult> Index()
        {
            var dbContext = _context.Producto.Include(p => p.Categoria);
            return View(await dbContext.ToListAsync());
        }

        public async Task<IActionResult> FiltrarProducto(string? nombreCategoria)
        {
            if (nombreCategoria == null || _context.Producto == null)
            {
                return NotFound();
            }

            var listaCategorias = await _context.Producto
                                                .Include(p => p.Categoria)
                                                .Where(p => p.Categoria.Nombre == nombreCategoria)
                                                .ToListAsync();

            return View("Index", listaCategorias);
        }

        // GET: Productos/Details/5
        // RN29.Si el usuario logueado es empleado, mostrar la acción de editar
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        // RN25. El nombre del producto es único
        // RN26. No puede utilizarse la misma foto para más de un producto
        [Authorize (Roles = "EMPLEADO")] // RN24. Solo usuarios empleados pueden crear productos
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // RN23. No es posible crear un producto sin categoría.
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Precio,Foto,Stock,Costo,CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productos/Edit/5
        [Authorize(Roles = "EMPLEADO")] // RN30.Solo usuarios de tipo empleados pueden editar.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", producto.CategoriaId);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // RN31. No es posible editar: nombre, foto, categoría
        [Authorize(Roles = "EMPLEADO")] // RN30.Solo usuarios de tipo empleados pueden editar.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Precio,Foto,Stock,Costo,CategoriaId")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productos/Delete/5
        // PRODUCTOS NO TIENE BORRAR
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Producto == null)
            {
                return Problem("Entity set 'DbContext.Producto'  is null.");
            }
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Producto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
