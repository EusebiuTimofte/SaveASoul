using Microsoft.EntityFrameworkCore.Migrations;

namespace SaveASoul.Migrations
{
    public partial class PrimaryKeyOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalDTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Age = table.Column<float>(nullable: false),
                    Species = table.Column<string>(nullable: true),
                    Breed = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Weight = table.Column<float>(nullable: false),
                    ShelterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalDTO", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalDTO");
        }
    }
}
