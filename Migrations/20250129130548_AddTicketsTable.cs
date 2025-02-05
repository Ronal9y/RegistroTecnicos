using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clientes_clientesClienteId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Tecnicos_TecnicoId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_clientesClienteId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "clientesClienteId",
                table: "Tickets");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ClienteId",
                table: "Tickets",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clientes_ClienteId",
                table: "Tickets",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Tecnicos_TecnicoId",
                table: "Tickets",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clientes_ClienteId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Tecnicos_TecnicoId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ClienteId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "clientesClienteId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_clientesClienteId",
                table: "Tickets",
                column: "clientesClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clientes_clientesClienteId",
                table: "Tickets",
                column: "clientesClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Tecnicos_TecnicoId",
                table: "Tickets",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
