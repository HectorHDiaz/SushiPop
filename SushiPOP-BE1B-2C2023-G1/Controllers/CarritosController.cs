using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using SushiPOP_BE1B_2C2023_G1.Models;

namespace SushiPOP_BE1B_2C2023_G1.Controllers
{
    public class CarritosController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CarritosController(DbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // RN42. Si el carrito fue cancelado (borrado lógico), ese carrito no se muestra más y se crea uno nuevo al agregar un producto.
        // RN43. El carrito únicamente puede ser editado por el usuario cliente propietario (Edit)
        // RN44. Solo los usuarios cliente propietarios de ese carrito pueden cancelar el carrito. (Borrar)
        // RN45. No se requiere mostrar mensaje de confirmación para borrar/cancelar el carrito (Borrar)
        // RN46. El flag Procesado pasa a true una vez que el pedido es creado


        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> agregarProducto(int productoId)
        {


            if (productoId == null || _context.Producto == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
           var cliente = await _context.Cliente.Where(c => c.Email.ToUpper() == user.NormalizedEmail).FirstOrDefaultAsync();
            var pedido = await _context.Pedido.Include(p => p.Carrito).Where(p => p.Carrito.ClienteId == cliente.Id && p.Estado == 1).FirstOrDefaultAsync();

            if (pedido != null)
            {
                return NotFound("Tenes un pedido sin confirmar");
            }

            var cantidadPedidos = await _context.Pedido.Include(p => p.Carrito)
                                                        .Where(p => p.Carrito.ClienteId == cliente.Id && p.FechaCompra.Value.Date == DateTime.Now.Date).ToListAsync();
           

            if (cantidadPedidos.Count() >= 3)
            {
                return NotFound("Ya hiciste tres pedidos en el dia de hoy");
            }





            // esto seria más correcto que este al principio del controller para ahorrar recursos
            // checkear stock
            // var producto = await _context.Producto.Where(p => p.Id == productoId).FirstOrDefaultAsync();
            var producto = await _context.Producto.FindAsync(productoId);

            if (producto == null || producto.Stock <= 0)
            {
                return NotFound();
            }


            // var mycarrito = await buscarCarritoActivo(cliente);
            var carrito = await _context.Carrito.Include(c => c.Items).Where(c => c.ClienteId == cliente.Id && c.Procesado == false && c.Cancelado == false).FirstOrDefaultAsync();


            if (carrito == null)
            {
                carrito = new Carrito();
                carrito.ClienteId = cliente.Id;
                carrito.Procesado = false;
                carrito.Cancelado = false;
                carrito.Items = new List<CarritoItem>();

                _context.Add(carrito);
                await _context.SaveChangesAsync();
            }

            // buscar o crear item
            var item = carrito.Items.FirstOrDefault(i => i.ProductoId == productoId);
            DateTime hoy = DateTime.Now;
            int nroDiaSemana = (int)hoy.DayOfWeek;

            decimal precioProducto = producto.Precio;
            var descuento = await _context.Descuento.Where(d => d.ProductoId == productoId && d.Activo && d.Dia == nroDiaSemana).FirstOrDefaultAsync();

            if (descuento != null)
            {
                precioProducto =  precioProducto - (decimal)((descuento.Porcentaje * producto.Precio) / 100);
            }

            if (item == null)
            {
                item = new CarritoItem();
                item.ProductoId = productoId;
                item.Cantidad = 0;
                item.CarritoId = carrito.Id;
                item.PrecioUnitarioConDescuento = precioProducto;

                _context.Add(item);
                await _context.SaveChangesAsync();
                
            }

            if (producto.Stock < 1)
            {
                //No hay stock
                return NotFound("No hay stock");
            }

            if (producto.Stock > 0  ) { 
                item.Cantidad += 1;
                producto.Stock -= 1;
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index", "CarritoItems");
        }
        private  Carrito buscarCarritoActivo(Cliente cliente)
        {
            var carritosCliente = cliente.Carritos;

            var respuesta =  carritosCliente.Where(c => !c.Cancelado && !c.Procesado).FirstOrDefault();

            return respuesta;
        }


        // GET: Carritos
        [Authorize (Roles = "CLIENTE")] // RN41. El usuario cliente solo puede ver únicamente los productos agregados a su carrito
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var cliente = await _context.Cliente.Where(c => c.Email.ToUpper() == user.NormalizedEmail).FirstOrDefaultAsync();
            var carritos = await _context.Carrito.Where(c => c.ClienteId == cliente.Id).ToListAsync();

          
            return View(carritos);
        }

        private async void tienePedido(){
            var user = await _userManager.GetUserAsync(User);
            var cliente = await _context.Cliente.Where(c => c.Email.ToUpper() == user.NormalizedEmail).FirstOrDefaultAsync();
            var pedido = await _context.Pedido.Include(p => p.Carrito).Where(p => p.Carrito.ClienteId == cliente.Id && p.Estado == 1).FirstOrDefaultAsync();

            if (pedido != null) {
                RedirectToAction("Index", "Pedidos");
            }
            else
            {
                RedirectToAction("Index", "CarritosItems");
            }

        }

        // GET: Carritos/Details/5
        [Authorize(Roles = "CLIENTE")] // RN41. El usuario cliente solo puede ver únicamente los productos agregados a su carrito
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carrito == null)
            {
                return NotFound();
            }

            // falta el include de Item, Producto 

            var carrito = await _context.Carrito
                .Include(c => c.Cliente)
                .Include(c=>c.Items)
                .ThenInclude(i=>i.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // GET: Carritos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Id");
            return View();
        }

        // POST: Carritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "CLIENTE")] // RN40. Solo usuarios de tipo cliente pueden hacer pedidos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            Carrito carritoNuevo = new Carrito();

            if (carritoNuevo != null)
            {
                _context.Add(carritoNuevo);
                await _context.SaveChangesAsync();
                return View(carritoNuevo);
            }
            else
            {
                return NotFound();
            }

            // ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Id", carrito.ClienteId);
            // return View(carrito);
        }

        // GET: Carritos/Edit/5
        public async Task<IActionResult> Edit(int? carritoId)
        {

                var carritoBuscado = _context.Carrito.Include(c => c.Cliente).Where(c => c.Id == carritoId).FirstOrDefault();
                if (carritoBuscado != null)
                {

                    Carrito carritoNuevo = new()
                    {
                        Cancelado = false,
                        Procesado = false,
                        ClienteId = carritoBuscado.ClienteId
                    };

                    carritoBuscado.Cliente.Carritos.Add(carritoNuevo);
                    _context.Add(carritoNuevo);
                    _context.Update(carritoBuscado.Cliente);
                    await _context.SaveChangesAsync();



                    // ↓↓ carritoBuscado pasa a ser un Pedido - desarrollar ↓↓
                }
            return RedirectToAction("Create", "Pedidos");
 
        }

        // POST: Carritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Procesado,Cancelado,ClienteId")] Carrito carrito)
        {
            if (id != carrito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Id", carrito.ClienteId);
            return View(carrito);
        }

        // GET: Carritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carrito == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carrito == null)
            {
                return Problem("Entity set 'DbContext.Carrito'  is null.");
            }
            var carrito = await _context.Carrito.FindAsync(id);
            if (carrito != null)
            {
                _context.Carrito.Remove(carrito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoExists(int id)
        {
          return (_context.Carrito?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //CANCELAR COMPRA
        public async Task<IActionResult> CancelarCarrito(int? carritoId)
        {
            var carrito = await _context.Carrito.FindAsync(carritoId);
            var carritoItems = await _context.CarritoItem.Where(i=> i.CarritoId == carritoId).ToListAsync();

            if (carritoItems == null)
            {
                return NotFound();
            }
            carrito.Cancelado = true;

            foreach (var item in carritoItems)
            {
                var producto = await _context.Producto.FindAsync(item.ProductoId);
                producto.Stock += item.Cantidad;
                _context.CarritoItem.Remove(item);
                _context.Update(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
