using Microsoft.EntityFrameworkCore.Migrations;

namespace SaveASoul.Migrations
{
    public partial class Favorite_key_order_aghain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adoptions",
                table: "Adoptions");

            migrationBuilder.DropIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                columns: new[] { "UserId", "AnimalId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adoptions",
                table: "Adoptions",
                columns: new[] { "UserId", "AnimalId" });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AnimalId",
                table: "Favorites",
                column: "AnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_AnimalId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adoptions",
                table: "Adoptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                columns: new[] { "AnimalId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adoptions",
                table: "Adoptions",
                columns: new[] { "AnimalId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoptions",
                column: "UserId");
        }
    }
}
