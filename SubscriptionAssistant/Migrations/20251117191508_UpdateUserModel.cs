using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SubscriptionAssistant.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 10, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4754));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 10, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4757));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2025, 10, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4760));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2024, 11, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4762));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2025, 10, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4764));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Обычный пользователь", "User" },
                    { 2, "Администратор", "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NextPaymentDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 2, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4693), new DateTime(2025, 9, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4683) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NextPaymentDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 22, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4701), new DateTime(2025, 5, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NextPaymentDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 12, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4705), new DateTime(2025, 10, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4704) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "NextPaymentDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4711), new DateTime(2024, 11, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4708) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "NextPaymentDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 27, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4716), new DateTime(2025, 8, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4715) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "RoleId", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2025, 11, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4632), 1, null, "ivanov" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "RoleId", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2025, 11, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4635), 1, null, "petrov" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "RoleId", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2025, 11, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4638), 1, null, "sidorova" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Email", "RoleId", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2025, 11, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4640), "smirnov@example.com", 1, null, "smirnov" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Email", "RoleId", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2025, 11, 17, 19, 15, 7, 700, DateTimeKind.Utc).AddTicks(4642), "kuznetsov@example.com", 1, null, "kuznetsov" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 10, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4371));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 10, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4375));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2025, 10, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4378));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2024, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4380));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2025, 10, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4384));

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NextPaymentDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 24, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4148), new DateTime(2025, 9, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4138) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NextPaymentDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 14, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4157), new DateTime(2025, 5, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4156) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NextPaymentDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 4, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4164), new DateTime(2025, 10, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4163) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "NextPaymentDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4311), new DateTime(2024, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4307) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "NextPaymentDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 19, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4316), new DateTime(2025, 8, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4314) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Username" },
                values: new object[] { new DateTime(2025, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4073), "иванов" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Username" },
                values: new object[] { new DateTime(2025, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4076), "петров" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Username" },
                values: new object[] { new DateTime(2025, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4078), "сидорова" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Email", "Username" },
                values: new object[] { new DateTime(2025, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4081), "kuznetsov@example.com", "кузнецов" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Email", "Username" },
                values: new object[] { new DateTime(2025, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4084), "smirnov@example.com", "смирнов" });
        }
    }
}
