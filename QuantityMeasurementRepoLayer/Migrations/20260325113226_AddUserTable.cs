using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementRepoLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MeasurementHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementHistory_UserId",
                table: "MeasurementHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementHistory_Users_UserId",
                table: "MeasurementHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementHistory_Users_UserId",
                table: "MeasurementHistory");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementHistory_UserId",
                table: "MeasurementHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MeasurementHistory");
        }
    }
}
