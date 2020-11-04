using Microsoft.EntityFrameworkCore.Migrations;

namespace RentResidence.Repository.Migrations
{
    public partial class AjusteFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    ResidenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bairro = table.Column<string>(maxLength: 70, nullable: false),
                    CEP = table.Column<string>(maxLength: 20, nullable: false),
                    Cidade = table.Column<string>(maxLength: 70, nullable: false),
                    Complemento = table.Column<string>(maxLength: 20, nullable: false),
                    Estado = table.Column<string>(maxLength: 70, nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    Rua = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.ResidenceId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(maxLength: 14, nullable: false),
                    Email = table.Column<string>(maxLength: 70, nullable: false),
                    NomeCompleto = table.Column<string>(maxLength: 250, nullable: false),
                    Sexo = table.Column<string>(maxLength: 20, nullable: false),
                    Telefone = table.Column<string>(maxLength: 20, nullable: false),
                    ResidenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Residences_ResidenceId",
                        column: x => x.ResidenceId,
                        principalTable: "Residences",
                        principalColumn: "ResidenceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ResidenceId",
                table: "Clients",
                column: "ResidenceId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Residences");
        }
    }
}
