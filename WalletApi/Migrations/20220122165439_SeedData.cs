using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletApi.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "BirthDate", "Country", "Currency", "RegistrationDate", "UserName" },
                values: new object[] { new Guid("b3b2b52a-ab5d-4bee-a15c-cfff0f3dff02"), 0, new DateTime(1997, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 64, 68, new DateTime(2022, 1, 20, 18, 54, 39, 240, DateTimeKind.Local).AddTicks(1769), "user4567" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "BirthDate", "Country", "Currency", "RegistrationDate", "UserName" },
                values: new object[] { new Guid("ca65ca5e-3e01-4693-9324-69ade8268292"), 1000000, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 68, new DateTime(2022, 1, 21, 18, 54, 39, 240, DateTimeKind.Local).AddTicks(1733), "user1234" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "Currency", "InsertedAt", "Kind", "ReferenceTransactionId", "UserId" },
                values: new object[] { new Guid("16d2dcfe-b89e-11e7-854a-58404eea6d16"), 364000, 68, new DateTime(2022, 1, 22, 18, 54, 39, 240, DateTimeKind.Local).AddTicks(1836), 0, null, new Guid("ca65ca5e-3e01-4693-9324-69ade8268292") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("16d2dcfe-b89e-11e7-854a-58404eea6d16"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b3b2b52a-ab5d-4bee-a15c-cfff0f3dff02"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ca65ca5e-3e01-4693-9324-69ade8268292"));
        }
    }
}
