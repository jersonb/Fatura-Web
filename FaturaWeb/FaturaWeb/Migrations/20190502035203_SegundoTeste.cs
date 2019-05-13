using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FaturaWeb.Migrations
{
    public partial class SegundoTeste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cnpj = table.Column<string>(type: "char(14)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    InscMunicipal = table.Column<string>(type: "char(14)", nullable: true),
                    Fone = table.Column<string>(type: "char(12)", nullable: true),
                    Email = table.Column<string>(type: "varchar(120)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(120)", nullable: false),
                    Uf = table.Column<string>(type: "char(2)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: false),
                    Cep = table.Column<string>(type: "char(9)", nullable: false),
                    Municipio = table.Column<string>(type: "varchar(120)", nullable: false),
                    Bairro = table.Column<string>(nullable: false),
                    NomeBanco = table.Column<string>(type: "varchar(50)", nullable: false),
                    Agencia = table.Column<string>(type: "varchar(20)", nullable: false),
                    Conta = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faturas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Numero = table.Column<int>(nullable: false),
                    EmissorId = table.Column<int>(nullable: true),
                    ClienteId = table.Column<int>(nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "Date", nullable: false),
                    Empenho = table.Column<string>(type: "varchar(20)", nullable: true),
                    ValorTotal = table.Column<decimal>(nullable: false),
                    TempoPrestacao = table.Column<string>(nullable: true),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faturas_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faturas_Cliente_EmissorId",
                        column: x => x.EmissorId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    FaturaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Itens_Faturas_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Faturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_ClienteId",
                table: "Faturas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_EmissorId",
                table: "Faturas",
                column: "EmissorId");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_FaturaId",
                table: "Itens",
                column: "FaturaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "Faturas");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
