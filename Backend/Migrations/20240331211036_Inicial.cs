using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    idA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreA = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.idA);
                });

            migrationBuilder.CreateTable(
                name: "ActorPelicula",
                columns: table => new
                {
                    IdA = table.Column<int>(type: "int", nullable: false),
                    IdAsIdA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorPelicula", x => new { x.IdA, x.IdAsIdA });
                });

            migrationBuilder.CreateTable(
                name: "Butaca",
                columns: table => new
                {
                    idB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idS = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Butaca", x => x.idB);
                });

            migrationBuilder.CreateTable(
                name: "ButacaCompra",
                columns: table => new
                {
                    IdB = table.Column<int>(type: "int", nullable: false),
                    IdBsIdB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ButacaCompra", x => new { x.IdB, x.IdBsIdB });
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Ci = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    Correo = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    Confiabilidad = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Ci);
                });

            migrationBuilder.CreateTable(
                name: "CompraDescuento",
                columns: table => new
                {
                    IdD = table.Column<int>(type: "int", nullable: false),
                    IdDsIdD = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraDescuento", x => new { x.IdD, x.IdDsIdD });
                });

            migrationBuilder.CreateTable(
                name: "Descuento",
                columns: table => new
                {
                    idD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreD = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Porciento = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descuentos", x => x.idD);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    idG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreG = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.idG);
                });

            migrationBuilder.CreateTable(
                name: "GeneroPelicula",
                columns: table => new
                {
                    IdG = table.Column<int>(type: "int", nullable: false),
                    IdGsIdG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroPelicula", x => new { x.IdG, x.IdGsIdG });
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    idPg = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.idPg);
                });

            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    idP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sinopsis = table.Column<string>(type: "text", nullable: true),
                    Anno = table.Column<int>(type: "int", nullable: true),
                    Nacionalidad = table.Column<int>(type: "int", nullable: true),
                    Duración = table.Column<int>(type: "int", nullable: true),
                    Titulo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Imagen = table.Column<string>(type: "text", nullable: true),
                    Trailer = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.idP);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    idS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.idS);
                });

            migrationBuilder.CreateTable(
                name: "Tarjeta",
                columns: table => new
                {
                    codigoT = table.Column<string>(type: "varchar(18)", unicode: false, fixedLength: true, maxLength: 18, nullable: false),
                    Ci = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tarjeta__BC7B7B928CA058B7", x => x.codigoT);
                    table.ForeignKey(
                        name: "FK__Tarjeta__Ci__6B24EA82",
                        column: x => x.Ci,
                        principalTable: "Cliente",
                        principalColumn: "Ci");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Ci = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    NombreS = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Apellidos = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Puntos = table.Column<int>(type: "int", nullable: true),
                    Codigo = table.Column<string>(type: "varchar(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    Contrasena = table.Column<byte[]>(type: "binary(256)", fixedLength: true, maxLength: 256, nullable: true),
                    Rol = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Socio__32149A5BACE7E998", x => x.Ci);
                    table.ForeignKey(
                        name: "FK__Socio__Ci__656C112C",
                        column: x => x.Ci,
                        principalTable: "Cliente",
                        principalColumn: "Ci");
                });

            migrationBuilder.CreateTable(
                name: "Efectivo",
                columns: table => new
                {
                    idPg = table.Column<int>(type: "int", nullable: false),
                    CantidadE = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Efectivo__9DB8492F9CA4EC17", x => x.idPg);
                    table.ForeignKey(
                        name: "FK__Efectivo__idPg__7F2BE32F",
                        column: x => x.idPg,
                        principalTable: "Pago",
                        principalColumn: "idPg");
                });

            migrationBuilder.CreateTable(
                name: "Puntos",
                columns: table => new
                {
                    idPg = table.Column<int>(type: "int", nullable: false),
                    Gastados = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Puntos__9DB8492F0CB84D88", x => x.idPg);
                    table.ForeignKey(
                        name: "FK__Puntos__idPg__7C4F7684",
                        column: x => x.idPg,
                        principalTable: "Pago",
                        principalColumn: "idPg");
                });

            migrationBuilder.CreateTable(
                name: "Elenco",
                columns: table => new
                {
                    idP = table.Column<int>(type: "int", nullable: false),
                    idA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKelenco", x => new { x.idP, x.idA });
                    table.ForeignKey(
                        name: "FK__Elenco__idA__1DB06A4F",
                        column: x => x.idA,
                        principalTable: "Actor",
                        principalColumn: "idA");
                    table.ForeignKey(
                        name: "FK__Elenco__idP__1CBC4616",
                        column: x => x.idP,
                        principalTable: "Pelicula",
                        principalColumn: "idP");
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    idP = table.Column<int>(type: "int", nullable: false),
                    idG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKgeneros", x => new { x.idP, x.idG });
                    table.ForeignKey(
                        name: "FK__Generos__idG__2180FB33",
                        column: x => x.idG,
                        principalTable: "Genero",
                        principalColumn: "idG");
                    table.ForeignKey(
                        name: "FK__Generos__idP__208CD6FA",
                        column: x => x.idP,
                        principalTable: "Pelicula",
                        principalColumn: "idP");
                });

            migrationBuilder.CreateTable(
                name: "Sesion",
                columns: table => new
                {
                    idP = table.Column<int>(type: "int", nullable: false),
                    idS = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKsesion", x => new { x.idP, x.idS, x.fecha });
                    table.ForeignKey(
                        name: "FK__Sesion__idP__08B54D69",
                        column: x => x.idP,
                        principalTable: "Pelicula",
                        principalColumn: "idP");
                    table.ForeignKey(
                        name: "FK__Sesion__idS__09A971A2",
                        column: x => x.idS,
                        principalTable: "Sala",
                        principalColumn: "idS");
                });

            migrationBuilder.CreateTable(
                name: "Web",
                columns: table => new
                {
                    idPg = table.Column<int>(type: "int", nullable: false),
                    codigoT = table.Column<string>(type: "varchar(18)", unicode: false, fixedLength: true, maxLength: 18, nullable: true),
                    Cantidad = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Web__9DB8492FA85DDD01", x => x.idPg);
                    table.ForeignKey(
                        name: "FK__Web__codigoT__02084FDA",
                        column: x => x.codigoT,
                        principalTable: "Tarjeta",
                        principalColumn: "codigoT");
                    table.ForeignKey(
                        name: "FK__Web__idPg__02FC7413",
                        column: x => x.idPg,
                        principalTable: "Pago",
                        principalColumn: "idPg");
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    idP = table.Column<int>(type: "int", nullable: false),
                    idS = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ci = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    idPg = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FechaDeCompra = table.Column<DateTime>(type: "datetime", nullable: false),
                    MedioAd = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKCompra", x => new { x.idP, x.idS, x.fecha, x.Ci });
                    table.ForeignKey(
                        name: "FK__Compra__0E6E26BF",
                        columns: x => new { x.idP, x.idS, x.fecha },
                        principalTable: "Sesion",
                        principalColumns: new[] { "idP", "idS", "fecha" });
                    table.ForeignKey(
                        name: "FK__Compra__Ci__0C85DE4D",
                        column: x => x.Ci,
                        principalTable: "Cliente",
                        principalColumn: "Ci");
                    table.ForeignKey(
                        name: "FK__Compra__idPg__0D7A0286",
                        column: x => x.idPg,
                        principalTable: "Pago",
                        principalColumn: "idPg");
                });

            migrationBuilder.CreateTable(
                name: "ButacasReservadas",
                columns: table => new
                {
                    idP = table.Column<int>(type: "int", nullable: false),
                    idS = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ci = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    idB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKReserva", x => new { x.idP, x.idS, x.fecha, x.Ci, x.idB });
                    table.ForeignKey(
                        name: "FK__ButacasRese__idB__18EBB532",
                        column: x => x.idB,
                        principalTable: "Butaca",
                        principalColumn: "idB");
                    table.ForeignKey(
                        name: "FK__ButacasReservada__19DFD96B",
                        columns: x => new { x.idP, x.idS, x.fecha, x.Ci },
                        principalTable: "Compra",
                        principalColumns: new[] { "idP", "idS", "fecha", "Ci" });
                });

            migrationBuilder.CreateTable(
                name: "Descontado",
                columns: table => new
                {
                    idP = table.Column<int>(type: "int", nullable: false),
                    idS = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ci = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    idD = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKDescontado", x => new { x.idP, x.idS, x.fecha, x.Ci, x.idD });
                    table.ForeignKey(
                        name: "FK__Descontado__160F4887",
                        columns: x => new { x.idP, x.idS, x.fecha, x.Ci },
                        principalTable: "Compra",
                        principalColumns: new[] { "idP", "idS", "fecha", "Ci" });
                    table.ForeignKey(
                        name: "FK__Descontado__idD__151B244E",
                        column: x => x.idD,
                        principalTable: "Descuento",
                        principalColumn: "idD");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ButacasReservadas_idB",
                table: "ButacasReservadas",
                column: "idB");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_Ci",
                table: "Compra",
                column: "Ci");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_idPg",
                table: "Compra",
                column: "idPg");

            migrationBuilder.CreateIndex(
                name: "IX_Descontado_idD",
                table: "Descontado",
                column: "idD");

            migrationBuilder.CreateIndex(
                name: "IX_Elenco_idA",
                table: "Elenco",
                column: "idA");

            migrationBuilder.CreateIndex(
                name: "IX_Generos_idG",
                table: "Generos",
                column: "idG");

            migrationBuilder.CreateIndex(
                name: "IX_Sesion_idS",
                table: "Sesion",
                column: "idS");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjeta_Ci",
                table: "Tarjeta",
                column: "Ci");

            migrationBuilder.CreateIndex(
                name: "IX_Web_codigoT",
                table: "Web",
                column: "codigoT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorPelicula");

            migrationBuilder.DropTable(
                name: "ButacaCompra");

            migrationBuilder.DropTable(
                name: "ButacasReservadas");

            migrationBuilder.DropTable(
                name: "CompraDescuento");

            migrationBuilder.DropTable(
                name: "Descontado");

            migrationBuilder.DropTable(
                name: "Efectivo");

            migrationBuilder.DropTable(
                name: "Elenco");

            migrationBuilder.DropTable(
                name: "GeneroPelicula");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Puntos");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Web");

            migrationBuilder.DropTable(
                name: "Butaca");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Descuento");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Tarjeta");

            migrationBuilder.DropTable(
                name: "Sesion");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Pelicula");

            migrationBuilder.DropTable(
                name: "Sala");
        }
    }
}
