using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductViewer.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Products");
        }
    }
}
