using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorLaboratory.GraphQL.Migrations
{
    /// <inheritdoc />
    public partial class CreatorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Courses");
        }
    }
}
