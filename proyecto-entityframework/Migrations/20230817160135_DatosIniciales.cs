using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace proyectoentityframework.Migrations
{
    /// <inheritdoc />
    public partial class DatosIniciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c602"), null, "Personal activities", 50 },
                    { new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"), null, "Pending activities", 20 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "Id", "CategoriaId", "Descripcion", "FechaCreacion", "Prioridad", "Titulo" },
                values: new object[,]
                {
                    { new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c100"), new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"), null, new DateTime(2023, 8, 17, 11, 1, 35, 705, DateTimeKind.Local).AddTicks(3815), 1, "Payment of public services" },
                    { new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c101"), new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c602"), null, new DateTime(2023, 8, 17, 11, 1, 35, 705, DateTimeKind.Local).AddTicks(3834), 0, "Finish watching movie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "Id",
                keyValue: new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c100"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "Id",
                keyValue: new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c101"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c602"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: new Guid("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
