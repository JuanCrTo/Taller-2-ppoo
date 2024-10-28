using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusControl.Data.Migrations
{
    /// <inheritdoc />
    public partial class _9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Matriculas",
                newName: "MatriculaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatriculaId",
                table: "Matriculas",
                newName: "Id");
        }
    }
}
