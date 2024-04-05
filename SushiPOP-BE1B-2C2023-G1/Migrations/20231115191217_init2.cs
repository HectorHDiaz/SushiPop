using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SushiPOP_BE1B_2C2023_G1.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrito_Usuario_ClienteId",
                table: "Carrito");

            migrationBuilder.DropForeignKey(
                name: "FK_CarritoItem_Carrito_CarritoId",
                table: "CarritoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CarritoItem_Producto_ProductoId",
                table: "CarritoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Descuento_Producto_ProductoId",
                table: "Descuento");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Carrito_CarritoId",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Categoria_CategoriaId",
                table: "Producto");

            migrationBuilder.DropForeignKey(
                name: "FK_Reclamo_Pedido_PedidoId",
                table: "Reclamo");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Usuario_ClienteId",
                table: "Reserva");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reserva",
                table: "Reserva");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reclamo",
                table: "Reclamo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producto",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Descuento",
                table: "Descuento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacto",
                table: "Contacto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarritoItem",
                table: "CarritoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrito",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Legajo",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "NumeroCliente",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "T_USUARIO");

            migrationBuilder.RenameTable(
                name: "Reserva",
                newName: "T_RESERVA");

            migrationBuilder.RenameTable(
                name: "Reclamo",
                newName: "T_RECLAMO");

            migrationBuilder.RenameTable(
                name: "Producto",
                newName: "T_PRODUCTO");

            migrationBuilder.RenameTable(
                name: "Pedido",
                newName: "T_PEDIDO");

            migrationBuilder.RenameTable(
                name: "Descuento",
                newName: "T_DESCUENTO");

            migrationBuilder.RenameTable(
                name: "Contacto",
                newName: "T_CONTACTO");

            migrationBuilder.RenameTable(
                name: "Categoria",
                newName: "T_CATEGORIA");

            migrationBuilder.RenameTable(
                name: "CarritoItem",
                newName: "T_CARRITO_ITEM");

            migrationBuilder.RenameTable(
                name: "Carrito",
                newName: "T_CARRITO");

            migrationBuilder.RenameIndex(
                name: "IX_Reserva_ClienteId",
                table: "T_RESERVA",
                newName: "IX_T_RESERVA_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Reclamo_PedidoId",
                table: "T_RECLAMO",
                newName: "IX_T_RECLAMO_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_CategoriaId",
                table: "T_PRODUCTO",
                newName: "IX_T_PRODUCTO_CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_CarritoId",
                table: "T_PEDIDO",
                newName: "IX_T_PEDIDO_CarritoId");

            migrationBuilder.RenameIndex(
                name: "IX_Descuento_ProductoId",
                table: "T_DESCUENTO",
                newName: "IX_T_DESCUENTO_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_CarritoItem_ProductoId",
                table: "T_CARRITO_ITEM",
                newName: "IX_T_CARRITO_ITEM_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_CarritoItem_CarritoId",
                table: "T_CARRITO_ITEM",
                newName: "IX_T_CARRITO_ITEM_CarritoId");

            migrationBuilder.RenameIndex(
                name: "IX_Carrito_ClienteId",
                table: "T_CARRITO",
                newName: "IX_T_CARRITO_ClienteId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCompra",
                table: "T_PEDIDO",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_USUARIO",
                table: "T_USUARIO",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_RESERVA",
                table: "T_RESERVA",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_RECLAMO",
                table: "T_RECLAMO",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_PRODUCTO",
                table: "T_PRODUCTO",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_PEDIDO",
                table: "T_PEDIDO",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_DESCUENTO",
                table: "T_DESCUENTO",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_CONTACTO",
                table: "T_CONTACTO",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_CATEGORIA",
                table: "T_CATEGORIA",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_CARRITO_ITEM",
                table: "T_CARRITO_ITEM",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_CARRITO",
                table: "T_CARRITO",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "T_CLIENTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NumeroCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CLIENTE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_CLIENTE_T_USUARIO_Id",
                        column: x => x.Id,
                        principalTable: "T_USUARIO",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "T_EMPLEADO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Legajo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_EMPLEADO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_EMPLEADO_T_USUARIO_Id",
                        column: x => x.Id,
                        principalTable: "T_USUARIO",
                        principalColumn: "Id");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_T_CARRITO_T_CLIENTE_ClienteId",
                table: "T_CARRITO",
                column: "ClienteId",
                principalTable: "T_CLIENTE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CARRITO_ITEM_T_CARRITO_CarritoId",
                table: "T_CARRITO_ITEM",
                column: "CarritoId",
                principalTable: "T_CARRITO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CARRITO_ITEM_T_PRODUCTO_ProductoId",
                table: "T_CARRITO_ITEM",
                column: "ProductoId",
                principalTable: "T_PRODUCTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_DESCUENTO_T_PRODUCTO_ProductoId",
                table: "T_DESCUENTO",
                column: "ProductoId",
                principalTable: "T_PRODUCTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_PEDIDO_T_CARRITO_CarritoId",
                table: "T_PEDIDO",
                column: "CarritoId",
                principalTable: "T_CARRITO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_PRODUCTO_T_CATEGORIA_CategoriaId",
                table: "T_PRODUCTO",
                column: "CategoriaId",
                principalTable: "T_CATEGORIA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_RECLAMO_T_PEDIDO_PedidoId",
                table: "T_RECLAMO",
                column: "PedidoId",
                principalTable: "T_PEDIDO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_RESERVA_T_CLIENTE_ClienteId",
                table: "T_RESERVA",
                column: "ClienteId",
                principalTable: "T_CLIENTE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_CARRITO_T_CLIENTE_ClienteId",
                table: "T_CARRITO");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CARRITO_ITEM_T_CARRITO_CarritoId",
                table: "T_CARRITO_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CARRITO_ITEM_T_PRODUCTO_ProductoId",
                table: "T_CARRITO_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_T_DESCUENTO_T_PRODUCTO_ProductoId",
                table: "T_DESCUENTO");

            migrationBuilder.DropForeignKey(
                name: "FK_T_PEDIDO_T_CARRITO_CarritoId",
                table: "T_PEDIDO");

            migrationBuilder.DropForeignKey(
                name: "FK_T_PRODUCTO_T_CATEGORIA_CategoriaId",
                table: "T_PRODUCTO");

            migrationBuilder.DropForeignKey(
                name: "FK_T_RECLAMO_T_PEDIDO_PedidoId",
                table: "T_RECLAMO");

            migrationBuilder.DropForeignKey(
                name: "FK_T_RESERVA_T_CLIENTE_ClienteId",
                table: "T_RESERVA");

            migrationBuilder.DropTable(
                name: "T_CLIENTE");

            migrationBuilder.DropTable(
                name: "T_EMPLEADO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_USUARIO",
                table: "T_USUARIO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_RESERVA",
                table: "T_RESERVA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_RECLAMO",
                table: "T_RECLAMO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_PRODUCTO",
                table: "T_PRODUCTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_PEDIDO",
                table: "T_PEDIDO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_DESCUENTO",
                table: "T_DESCUENTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_CONTACTO",
                table: "T_CONTACTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_CATEGORIA",
                table: "T_CATEGORIA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_CARRITO_ITEM",
                table: "T_CARRITO_ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_CARRITO",
                table: "T_CARRITO");

            migrationBuilder.RenameTable(
                name: "T_USUARIO",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "T_RESERVA",
                newName: "Reserva");

            migrationBuilder.RenameTable(
                name: "T_RECLAMO",
                newName: "Reclamo");

            migrationBuilder.RenameTable(
                name: "T_PRODUCTO",
                newName: "Producto");

            migrationBuilder.RenameTable(
                name: "T_PEDIDO",
                newName: "Pedido");

            migrationBuilder.RenameTable(
                name: "T_DESCUENTO",
                newName: "Descuento");

            migrationBuilder.RenameTable(
                name: "T_CONTACTO",
                newName: "Contacto");

            migrationBuilder.RenameTable(
                name: "T_CATEGORIA",
                newName: "Categoria");

            migrationBuilder.RenameTable(
                name: "T_CARRITO_ITEM",
                newName: "CarritoItem");

            migrationBuilder.RenameTable(
                name: "T_CARRITO",
                newName: "Carrito");

            migrationBuilder.RenameIndex(
                name: "IX_T_RESERVA_ClienteId",
                table: "Reserva",
                newName: "IX_Reserva_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_T_RECLAMO_PedidoId",
                table: "Reclamo",
                newName: "IX_Reclamo_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_T_PRODUCTO_CategoriaId",
                table: "Producto",
                newName: "IX_Producto_CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_T_PEDIDO_CarritoId",
                table: "Pedido",
                newName: "IX_Pedido_CarritoId");

            migrationBuilder.RenameIndex(
                name: "IX_T_DESCUENTO_ProductoId",
                table: "Descuento",
                newName: "IX_Descuento_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_T_CARRITO_ITEM_ProductoId",
                table: "CarritoItem",
                newName: "IX_CarritoItem_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_T_CARRITO_ITEM_CarritoId",
                table: "CarritoItem",
                newName: "IX_CarritoItem_CarritoId");

            migrationBuilder.RenameIndex(
                name: "IX_T_CARRITO_ClienteId",
                table: "Carrito",
                newName: "IX_Carrito_ClienteId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Legajo",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroCliente",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCompra",
                table: "Pedido",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reserva",
                table: "Reserva",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reclamo",
                table: "Reclamo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producto",
                table: "Producto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Descuento",
                table: "Descuento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacto",
                table: "Contacto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarritoItem",
                table: "CarritoItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrito",
                table: "Carrito",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrito_Usuario_ClienteId",
                table: "Carrito",
                column: "ClienteId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoItem_Carrito_CarritoId",
                table: "CarritoItem",
                column: "CarritoId",
                principalTable: "Carrito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoItem_Producto_ProductoId",
                table: "CarritoItem",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Descuento_Producto_ProductoId",
                table: "Descuento",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Carrito_CarritoId",
                table: "Pedido",
                column: "CarritoId",
                principalTable: "Carrito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Categoria_CategoriaId",
                table: "Producto",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamo_Pedido_PedidoId",
                table: "Reclamo",
                column: "PedidoId",
                principalTable: "Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Usuario_ClienteId",
                table: "Reserva",
                column: "ClienteId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
