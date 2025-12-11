using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustTip.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TipsAmountEuros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "amount",
                schema: "Jt",
                table: "tips",
                newName: "amount_euros");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "amount_euros",
                schema: "Jt",
                table: "tips",
                newName: "amount");
        }
    }
}
