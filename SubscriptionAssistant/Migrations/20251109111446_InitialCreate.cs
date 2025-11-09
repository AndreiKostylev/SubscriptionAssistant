using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SubscriptionAssistant.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LogoUrl = table.Column<string>(type: "text", nullable: true),
                    BasePrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NextPaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BillingCycle = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "boolean", nullable: false),
                    SubscriptionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Видео и музыкальные платформы", "Стриминговые сервисы" },
                    { 2, "Подписки на ПО и приложения", "Программное обеспечение" },
                    { 3, "Хранилища и облачные решения", "Облачные сервисы" },
                    { 4, "Онлайн-курсы и обучающие платформы", "Образование" },
                    { 5, "Игровые подписки и сервисы", "Игры" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "BasePrice", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 1, 599m, "/logos/netflix.png", "Netflix" },
                    { 2, 299m, "/logos/spotify.png", "Spotify" },
                    { 3, 399m, "/logos/youtube.png", "YouTube Premium" },
                    { 4, 799m, "/logos/microsoft.png", "Microsoft 365" },
                    { 5, 2499m, "/logos/adobe.png", "Adobe Creative Cloud" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4073), "ivanov@example.com", "hashed_password_1", "иванов" },
                    { 2, new DateTime(2025, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4076), "petrov@example.com", "hashed_password_2", "петров" },
                    { 3, new DateTime(2025, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4078), "sidorova@example.com", "hashed_password_3", "сидорова" },
                    { 4, new DateTime(2025, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4081), "kuznetsov@example.com", "hashed_password_4", "кузнецов" },
                    { 5, new DateTime(2025, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4084), "smirnov@example.com", "hashed_password_5", "смирнов" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "BillingCycle", "CategoryId", "IsActive", "Name", "NextPaymentDate", "Price", "ServiceId", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, "ежемесячно", 1, true, "Мой Netflix Премиум", new DateTime(2025, 11, 24, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4148), 599m, 1, new DateTime(2025, 9, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4138), 1 },
                    { 2, "ежемесячно", 1, true, "Spotify Премиум", new DateTime(2025, 11, 14, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4157), 299m, 2, new DateTime(2025, 5, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4156), 1 },
                    { 3, "ежемесячно", 1, true, "YouTube Premium Семейный", new DateTime(2025, 12, 4, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4164), 699m, 3, new DateTime(2025, 10, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4163), 2 },
                    { 4, "ежегодно", 2, true, "Microsoft 365 Личный", new DateTime(2025, 12, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4311), 799m, 4, new DateTime(2024, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4307), 3 },
                    { 5, "ежемесячно", 2, true, "Adobe Creative Cloud", new DateTime(2025, 11, 19, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4316), 2499m, 5, new DateTime(2025, 8, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4314), 4 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "IsSuccessful", "PaymentDate", "SubscriptionId" },
                values: new object[,]
                {
                    { 1, 599m, true, new DateTime(2025, 10, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4371), 1 },
                    { 2, 299m, true, new DateTime(2025, 10, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4375), 2 },
                    { 3, 699m, true, new DateTime(2025, 10, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4378), 3 },
                    { 4, 799m, true, new DateTime(2024, 11, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4380), 4 },
                    { 5, 2499m, true, new DateTime(2025, 10, 9, 11, 14, 46, 142, DateTimeKind.Utc).AddTicks(4384), 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SubscriptionId",
                table: "Payments",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CategoryId",
                table: "Subscriptions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ServiceId",
                table: "Subscriptions",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
