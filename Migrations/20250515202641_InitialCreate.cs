using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "halls",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    number_of_seats = table.Column<int>(type: "INTEGER", nullable: true),
                    threedee = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__halls__3213E83FCFD57C07", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", unicode: false, maxLength: 45, nullable: false),
                    type = table.Column<string>(type: "TEXT", unicode: false, maxLength: 45, nullable: false),
                    director = table.Column<string>(type: "TEXT", unicode: false, maxLength: 45, nullable: true),
                    summary = table.Column<string>(type: "TEXT", unicode: false, maxLength: 45, nullable: true),
                    length = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__movies__3213E83F8BD5869E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", unicode: false, maxLength: 32, nullable: false),
                    password = table.Column<string>(type: "TEXT", unicode: false, maxLength: 45, nullable: false),
                    email = table.Column<string>(type: "TEXT", unicode: false, maxLength: 32, nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    role = table.Column<string>(type: "TEXT", unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83F3487FF8E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "screenings",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    movie_id = table.Column<int>(type: "INTEGER", nullable: false),
                    hall_id = table.Column<int>(type: "INTEGER", nullable: false),
                    datetime_of_play = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__screenin__3213E83FBF8903A1", x => x.id);
                    table.ForeignKey(
                        name: "FK_screenings_halls",
                        column: x => x.hall_id,
                        principalTable: "halls",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_screenings_movies",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    costumer_id = table.Column<int>(type: "INTEGER", nullable: false),
                    number_of_seats = table.Column<int>(type: "INTEGER", nullable: true),
                    scr_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reservat__3213E83F3AF22C3B", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservations_screenings",
                        column: x => x.scr_id,
                        principalTable: "screenings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_reservations_users",
                        column: x => x.costumer_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservations_costumer_id",
                table: "reservations",
                column: "costumer_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_scr_id",
                table: "reservations",
                column: "scr_id");

            migrationBuilder.CreateIndex(
                name: "IX_screenings_hall_id",
                table: "screenings",
                column: "hall_id");

            migrationBuilder.CreateIndex(
                name: "IX_screenings_movie_id",
                table: "screenings",
                column: "movie_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "screenings");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "halls");

            migrationBuilder.DropTable(
                name: "movies");
        }
    }
}
