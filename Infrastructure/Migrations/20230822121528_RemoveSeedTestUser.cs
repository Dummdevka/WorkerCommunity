using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeedTestUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Position", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, 23, "acb4a184-78ce-47aa-93f2-82c28f4eaf05", "test@test.com", false, "Test", "Test", false, null, "test@test.com", "admin", "AQAAAAIAAYagAAAAEISYrJ8o0wuh7e+VYEa5DzCfWL5HrSEfY5u9hUakGbhq6GLt5vtocnH/BYWQDZ2/IQ==", null, false, "Tester", "1188ac56-0cae-47ed-bc2b-481b16fe6116", false, "admin" });
        }
    }
}
