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
    public class PedidosController : Controller
    {
        private readonly DbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public PedidosController(DbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // RN48. Una vez creado el pedido, cambiar a True el atributo Procesado del carrito asociado
        // RN49. El cliente no puede crear un pedido nuevo si tiene uno en estado Sin confirmar.
        // RN50. El cliente solo puede realizar 3 pedidos por día.
        // RN51. Si el cliente tiene 10 pedidos en estado Entregado en los últimos 30 días, el costo de envío es gratis.
        // RN52. Los usuarios cliente pueden ver los pedidos de los últimos 3 meses (90 días).
        // RN53. Los usuarios empleados ven los pedidos del día que estén en estado != 5 || 6
        // RN55. Los usuarios clientes pueden cancelar el pedido solo si está en estado 1.


        // GET: Pedidos
        
        [Authorize(Roles = "EMPLEADO, CLIENTE")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("CLIENTE"))
            {
                 
                var pedidosCliente = await _context.Pedido
                                           .Include(x => x.Carrito.Items)
                                           .ThenInclude(i => i.Producto)
                                           .Include(x => x.Carrito)
                                           .ThenInclude(c => c.Cliente)
                                           .Where(p => p.Carrito.Cliente.Email.ToUpper() == user.NormalizedEmail
                                                   && p.FechaCompra >= DateTime.Now.AddDays(-90)
                                                   )
                                           .ToListAsync();

                if (pedidosCliente == null)
                {
                    return RedirectToAction("Index", "Home");
                }


                return View(pedidosCliente);
            } else
            {


                var pedidosEmpleado = await _context.Pedido
                                                        .Include(x => x.Carrito.Items)
                                                       .ThenInclude(i => i.Producto)
                                                       .Include(x => x.Carrito)
                                                       .ThenInclude(c => c.Cliente)
                                                        .Where(p => p.Estado < 5 )
                                                        .ToListAsync();
                return View(pedidosEmpleado);
            }

        }

        // GET: Pedidos/Details/5
        // cliente solo puede ver su pedido
        // NO SE USA
        [Authorize(Roles = "CLIENTE, EMPLEADO")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Carrito)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        private async Task<decimal> calcularEnvio(int clienteId)
        {
            int estadoEntregado = 5;
            
            var pedidosCliente = await _context.Pedido.Include(p => p.Carrito).Where(p => p.Carrito.ClienteId == clienteId && p.Estado == estadoEntregado).ToListAsync();

            decimal valorAgregado = 80;

            if (pedidosCliente.Count() > 10){
                valorAgregado = 0;
            }
            return valorAgregado;
        }

        // GET: Pedidos/Create
        [Authorize]
        public async Task<IActionResult> Create(int? carritoId)
        {
            var carritoActivo = await _context.Carrito
                                                .FindAsync(carritoId);
            
            var cliente = await _context.Cliente
                                               .Where(c => c.Id == carritoActivo.ClienteId)
                                               .FirstOrDefaultAsync();
            
            var items = await _context.CarritoItem
                                               .Include(c => c.Producto)
                                               .Where(i => i.CarritoId == carritoActivo.Id)
                                               .ToListAsync();
            // 
            var gastoEnvio = calcularEnvio(cliente.Id);

            if (carritoActivo != null && items.Count > 0)
            {
                PedidoViewModel viewModel = new PedidoViewModel()
                    {
                        CarritoId = carritoActivo.Id,
                        Items = items,
                        Cliente = cliente.Nombre + " " + cliente.Apellido,
                        Direccion = cliente.Direccion,
                        Subtotal = (decimal)items.Sum(i => i.PrecioUnitarioConDescuento * i.Cantidad),
                        GastoEnvio = await gastoEnvio,
                    };

                return View(viewModel);
            }

          return NotFound();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize (Roles = "CLIENTE")] // RN47. Solo usuarios de tipo cliente pueden hacer pedidos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FechaCompra,Subtotal,GastoEnvio,Total,Estado,CarritoId")] PedidoViewModel pedido)
        {


            if (ModelState.IsValid)
            {

                Pedido newPedido = new Pedido()
                {
                    NroPedido = 2999 + _context.Pedido.Count() + 1,
                    CarritoId = pedido.CarritoId,
                    Estado = 1,
                    FechaCompra = DateTime.Now,
                    GastoEnvio = pedido.GastoEnvio,
                    Subtotal = pedido.Subtotal,
                    Total = pedido.Subtotal + pedido.GastoEnvio,

                };

                var carrito = await _context.Carrito.FindAsync(pedido.CarritoId);
                    carrito.Procesado = true;
                    _context.Update(carrito);
                    await _context.SaveChangesAsync();

                _context.Add(newPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", pedido.CarritoId);  
            return View(pedido);
        }

        //cancelar
        public async Task<IActionResult> Cancelar(int? pedidoId)
        {
            var pedido = await _context.Pedido.FindAsync(pedidoId);

            if (pedidoId == null || pedido == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (pedido.Estado == 1) { 
                pedido.Estado = 6;
                _context.Update(pedido);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", pedido.CarritoId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize (Roles = "EMPLEADO")]// RN54. Los usuarios empleados pueden editar el estado de los pedidos de forma vertical incremental. Osea, pueden pasar un pedido de estado 1 a 2 pero no de 2 a 1.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,int estadoAnt,[Bind("Id,NroPedido,FechaCompra,Subtotal,GastoEnvio,Total,Estado,CarritoId")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (pedido != null)
            {
               
                try
                {
                    //var pedidoAnt = await _context.Pedido.FindAsync(pedido.Id);


                    if (estadoAnt > pedido.Estado || pedido.Estado-estadoAnt > 1)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "Id", "Id", pedido.CarritoId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Carrito)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedido == null)
            {
                return Problem("Entity set 'DbContext.Pedido'  is null.");
            }
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedido?.Any(e => e.Id == id)).GetValueOrDefault();
        }

     
    }
}
