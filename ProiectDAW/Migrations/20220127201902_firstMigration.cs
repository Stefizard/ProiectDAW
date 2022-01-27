using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProiectDAW.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credentiale",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentiale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pret = table.Column<float>(type: "real", nullable: false),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stoc = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Useri",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CredentialeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Useri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Useri_Credentiale_CredentialeId",
                        column: x => x.CredentialeId,
                        principalTable: "Credentiale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comenzi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comenzi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comenzi_Useri_UserId",
                        column: x => x.UserId,
                        principalTable: "Useri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListeProduse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantitate = table.Column<int>(type: "int", nullable: false),
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeProduse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListeProduse_Comenzi_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comenzi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListeProduse_Produse_ProdusId",
                        column: x => x.ProdusId,
                        principalTable: "Produse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_UserId",
                table: "Comenzi",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeProduse_ComandaId",
                table: "ListeProduse",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeProduse_ProdusId",
                table: "ListeProduse",
                column: "ProdusId");

            migrationBuilder.CreateIndex(
                name: "IX_Useri_CredentialeId",
                table: "Useri",
                column: "CredentialeId",
                unique: true);

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[Comenzi_UPDATE] ON [dbo].[Comenzi]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.Comenzi
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[Credentiale_UPDATE] ON [dbo].[Credentiale]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.Credentiale
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[ListeProduse_UPDATE] ON [dbo].[ListeProduse]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.ListeProduse
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[Produse_UPDATE] ON [dbo].[Produse]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.Produse
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[Useri_UPDATE] ON [dbo].[Useri]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.Useri
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListeProduse");

            migrationBuilder.DropTable(
                name: "Comenzi");

            migrationBuilder.DropTable(
                name: "Produse");

            migrationBuilder.DropTable(
                name: "Useri");

            migrationBuilder.DropTable(
                name: "Credentiale");
        }
    }
}
