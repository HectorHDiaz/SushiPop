using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SushiPOP_BE1B_2C2023_G1.Models;

    public class DbContext : IdentityDbContext
    {
        public DbContext (DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Carrito> Carrito { get; set; } = default!;

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.CarritoItem>? CarritoItem { get; set; }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Categoria>? Categoria { get; set; }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Cliente>? Cliente { get; set; }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Contacto>? Contacto { get; set; }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Descuento>? Descuento { get; set; }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Empleado>? Empleado { get; set; }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Pedido>? Pedido { get; set; }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Producto>? Producto { get; set; }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Reclamo>? Reclamo { get; set; }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Reserva>? Reserva { get; set; }

        public DbSet<SushiPOP_BE1B_2C2023_G1.Models.Usuario>? Usuario { get; set; }
    }
