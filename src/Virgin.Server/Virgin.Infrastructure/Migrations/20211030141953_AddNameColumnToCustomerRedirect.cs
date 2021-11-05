using Microsoft.EntityFrameworkCore.Migrations;

namespace Virgin.Infrastructure.Migrations
{
    public partial class AddNameColumnToCustomerRedirect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CustomerRedirects",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CustomerRedirects");
        }
    }
}
