using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeededTestUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "acb4a184-78ce-47aa-93f2-82c28f4eaf05", "test@test.com", "AQAAAAIAAYagAAAAEISYrJ8o0wuh7e+VYEa5DzCfWL5HrSEfY5u9hUakGbhq6GLt5vtocnH/BYWQDZ2/IQ==", "1188ac56-0cae-47ed-bc2b-481b16fe6116" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f2bb564-1ed1-45fc-9e13-174e7ae7d951", null, "AQAAAAIAAYagAAAAELKs0DFHUTtwO4ARJVwlf66CWLeWsVmT6hi6IqnMXqQn1PilAgHCZkZ5ALceBxqsww==", null });
        }
    }
}
