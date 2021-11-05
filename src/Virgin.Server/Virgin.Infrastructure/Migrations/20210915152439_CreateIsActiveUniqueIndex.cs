using Microsoft.EntityFrameworkCore.Migrations;

namespace Virgin.Infrastructure.Migrations
{
    public partial class CreateIsActiveUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "CustomerRedirects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRedirects_CustomerId_IsActive",
                table: "CustomerRedirects",
                columns: new[] { "CustomerId", "IsActive" },
                unique: true,
                filter: "[IsActive]=1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerRedirects_CustomerId_IsActive",
                table: "CustomerRedirects");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "CustomerRedirects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
