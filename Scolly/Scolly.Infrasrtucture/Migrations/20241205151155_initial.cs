using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scolly.Infrasrtucture.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeGroupId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CourseTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_AgeGroups_AgeGroupId",
                        column: x => x.AgeGroupId,
                        principalTable: "AgeGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CourseTypes_CourseTypeId",
                        column: x => x.CourseTypeId,
                        principalTable: "CourseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Children_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachersCourses",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersCourses", x => new { x.TeacherId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_TeachersCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeachersCourses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeachersSpecialties",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersSpecialties", x => new { x.TeacherId, x.SpecialtyId });
                    table.ForeignKey(
                        name: "FK_TeachersSpecialties_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersSpecialties_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRequests_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseRequests_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AgeGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "5 клас" },
                    { 2, "6 клас" },
                    { 3, "7 клас" },
                    { 4, "5-7 клас" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Бургас" },
                    { 2, "Варна" },
                    { 3, "Велико Търново" },
                    { 4, "Пловдив" },
                    { 5, "Русе" },
                    { 6, "София" },
                    { 7, "Стара Загора" },
                    { 8, "Благоевград" },
                    { 9, "Пазарджик" },
                    { 10, "Плевен" },
                    { 11, "Хасково" },
                    { 12, "Сливен" },
                    { 13, "Шумен" },
                    { 14, "Добрич" },
                    { 15, "Кърджали" },
                    { 16, "Враца" },
                    { 17, "Монтана" },
                    { 18, "Ловеч" },
                    { 19, "Перник" },
                    { 20, "Ямбол" },
                    { 21, "Кюстендил" },
                    { 22, "Търговище" },
                    { 23, "Разград" },
                    { 24, "Силистра" },
                    { 25, "Габрово" },
                    { 26, "Смолян" },
                    { 27, "Видин" }
                });

            migrationBuilder.InsertData(
                table: "CourseTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Математика" },
                    { 2, "Български език и литература" },
                    { 3, "Програмиране със C#" },
                    { 4, "Английски език" },
                    { 5, "Немски език" },
                    { 6, "Френски език" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Математика" },
                    { 2, "Информатика" },
                    { 3, "Български език и литература" },
                    { 4, "Английски език" },
                    { 5, "Немски език" },
                    { 6, "Испански език" },
                    { 7, "Руски език" },
                    { 8, "Френски език" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "CityId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3d522657-3ef9-4118-9b07-e5cd9bfb8614", 0, "ул. 'Васил Левски' 155А", 4, "9ba0b793-6707-4f92-85e2-84a6990b3323", "teacher4@gmail.com", true, "Виктор", "Стефанов", false, null, "Петров", "TEACHER4@GMAIL.COM", "TEACHER4@GMAIL.COM", "AQAAAAIAAYagAAAAEC/whK3h8Lw6ZS1TYEoB1w4fK0k+wjRk0CrP6DyCNNXKJbT8L8mws9EWXtrQOfVYWA==", "0899745997", false, "63206ccd-f010-4206-8b9b-329cceec71f4", false, "teacher4@gmail.com" },
                    { "42949812-d05e-46a5-8de6-cb3520319734", 0, "ул. 'Мусала' 12", 2, "11e705ce-d974-44cc-a97a-241df556b3e5", "teacher3@gmail.com", true, "Мария", "Димитрова", false, null, "Атанасова", "TEACHER3@GMAIL.COM", "TEACHER3@GMAIL.COM", "AQAAAAIAAYagAAAAEJJC6lRtA/qS6GhDvgh3CUA167wAotf9VqBXhgZLsR3q1HviyGfN97uU0bP+5DWz0Q==", "0899745867", false, "1a65a529-575e-422e-8555-f02663c9e1ff", false, "teacher3@gmail.com" },
                    { "4560faa9-53cd-4adc-8d65-4f7662cd30a7", 0, "ул. 'Никола Габровски' 15А", 3, "6b3bd214-ef66-4d50-b5a4-9943f532bb3b", "teacher1@gmail.com", true, "Николай", "Николов", false, null, "Иванов", "TEACHER1@GMAIL.COM", "TEACHER1@GMAIL.COM", "AQAAAAIAAYagAAAAEBEYV6pKjIzREw3HnM2LEFgr4ddDuNi9Vb7h4Tcd4of9pfqB+42+Bdk3YwtAylj7zQ==", "0884672591", false, "5bcd35e9-0ff8-4b87-93d7-1ea830e5a92c", false, "teacher1@gmail.com" },
                    { "56b171cf-06d6-4943-97d1-63b5d914a348", 0, "ул. 'Стоян Коледаров' 6", 2, "64ee49f0-1e6f-4fbe-b343-448637afbef6", "teacher2@gmail.com", true, "Преслав", "Калоянов", false, null, "Николаев", "TEACHER2@GMAIL.COM", "TEACHER2@GMAIL.COM", "AQAAAAIAAYagAAAAEN33BuykyWntohJ3DxDgpyEs0StZBXbgXfkIz9anzlGTN9BKwqjjbnurXxH6JEsYBw==", "0888967530", false, "6d3e4b42-7823-48dd-8723-fc1ecda776cc", false, "teacher2@gmail.com" },
                    { "7e124ab7-6050-49d8-853f-e51897ff536f", 0, "ул. 'Георги Бенковски' 25", 4, "3456fe06-eb84-46b4-afed-4ddf49e32c03", "parent5@gmail.com", true, "Даниела", "Петкова", false, null, "Сергеева", "PARENT5@GMAIL.COM", "PARENT5@GMAIL.COM", "AQAAAAIAAYagAAAAEAAuQSPEj+UBlMj4K8Oq43x5RXQhbS8E3au+2gF6VP1pwrdU3UzcLzUr7Rh0g9TUtA==", "0813528746", false, "68ed8bef-16eb-42ff-859f-274d5cb7f0df", false, "parent5@gmail.com" },
                    { "80cae098-d8ab-42e2-bec2-0bb11d0a441e", 0, "ул. 'Петя Дубарова' 16Б", 6, "a11b2941-8e7a-4550-a203-bb2ab7ae9e0c", "parent3@gmail.com", true, "Виктория", "Ангелова", false, null, "Николаева", "PARENT3@GMAIL.COM", "PARENT3@GMAIL.COM", "AQAAAAIAAYagAAAAEAeHsWQ+ho9GuxyqU+D39VysToNC8hqPtCYegdE0uvuUrdJ5I2D3BMs31Bot6hGjQw==", "0854789615", false, "c1e9741f-e339-4fbd-9dc4-76fb3912afb2", false, "parent3@gmail.com" },
                    { "aab83f21-bfce-46dc-b9b5-3fd6dd1d17f0", 0, "Необособен", 1, "1524c5da-b8cc-4fca-8f37-2e835e6368a2", "admin@admin.bg", true, "Admin", "Admin", false, null, "Admin", "ADMIN@ADMIN.BG", "ADMIN", "AQAAAAIAAYagAAAAEIGH7q4eHZ43NzwWvtvflw1QirA0vImrGsxBfTxOvAQpltddIHIAWt7pndu6GdNFEQ==", "0812345678", false, "7630402f-bee2-46a8-8925-ddbcfecd73bf", false, "admin" },
                    { "b79f3723-9600-499a-b640-a368f7816b47", 0, "ул. 'Освобождение' 3Б", 4, "398d4e18-fe8b-4212-97c3-912e19ed0c0d", "parent4@gmail.com", true, "Девора", "Кирилова", false, null, "Кирилова", "PARENT4@GMAIL.COM", "PARENT4@GMAIL.COM", "AQAAAAIAAYagAAAAEGqD5joC0FcPE17AwZgMhMogCIZKceGRW1uoZJOL/oNySveHoR+4ZdHUfy/Llrjzww==", "0865789488", false, "03502c92-701b-48db-bb34-dc456fddc46b", false, "parent4@gmail.com" },
                    { "ca1ee9ab-8832-464f-a197-7e64d8a4e8af", 0, "ул. 'Александър Малинов' 33", 5, "5207b8a6-a3d4-475f-a40a-762cfbd2ddbb", "teacher5@gmail.com", true, "Емил", "Долчинков", false, null, "Давидов", "TEACHER5@GMAIL.COM", "TEACHER5@GMAIL.COM", "AQAAAAIAAYagAAAAEBKBkS94HyG5PMVQ8bfWqX9gQ7DksHLKIvP8yOfuSZYAodUcKn7Bfp/Jlu3NwjXD7w==", "0899745000", false, "97be1bb6-2040-4dbb-83bc-24e21190d98e", false, "teacher5@gmail.com" },
                    { "e174d710-f687-4a09-876c-4a8690d393e5", 0, "ул. 'Стоян Коледаров' 3А", 1, "079cf32c-3fe7-4342-9b9c-815afea888f9", "parent2@gmail.com", true, "Александър", "Стратиев", false, null, "Иванов", "PARENT2@GMAIL.COM", "PARENT2@GMAIL.COM", "AQAAAAIAAYagAAAAEEj4KRTyLHe35t7XgDfCTI/dOLa/nHnI9i/mN7b76WtlDbnAQQ50unZojO1SEyLV2A==", "0854700615", false, "a0831727-df65-44b5-b416-dc7749850f17", false, "parent2@gmail.com" },
                    { "f8ea9f65-046d-47ea-86c6-9e3c7b6b7d2c", 0, "ул. 'Опълченска' 3А", 3, "57649bed-1828-4253-88d4-46cb398c828d", "parent1@gmail.com", true, "Петър", "Петров", false, null, "Иванов", "PARENT1@GMAIL.COM", "PARENT1@GMAIL.COM", "AQAAAAIAAYagAAAAEOwMvSqWjuBeMd2LWEWrEImuffu8wBgUP8PCrDwTV8zF3RHFko1Wefqo4hju044CRg==", "0859989615", false, "2591c176-e4f1-4b84-938b-facd8c054927", false, "parent1@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AgeGroupId", "CourseTypeId", "Description", "EndDate", "Price", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, 2, "Курс по български език и литература за ученици от 5 клас, насочен към подобряване на граматиката и аналитичните умения.", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 200m, new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 2, "Задълбочен курс по български език и литература за 5 клас, с акцент върху подготовката за национални изпити.", new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 250m, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 4, 5, "Интензивен курс по немски език за ученици от 5 до 7 клас, включващ говорене, слушане и писане.", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 630m, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 6, "Курс по френски език за начинаещи ученици от 5 клас, съсредоточен върху основните езикови умения.", new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 590m, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 3, 3, "Практически курс по програмиране със C# за ученици от 7 клас, включващ основи на програмирането и реални проекти.", new DateTime(2024, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 990m, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Parents",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, "f8ea9f65-046d-47ea-86c6-9e3c7b6b7d2c" },
                    { 2, "e174d710-f687-4a09-876c-4a8690d393e5" },
                    { 3, "80cae098-d8ab-42e2-bec2-0bb11d0a441e" },
                    { 4, "b79f3723-9600-499a-b640-a368f7816b47" },
                    { 5, "7e124ab7-6050-49d8-853f-e51897ff536f" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, "4560faa9-53cd-4adc-8d65-4f7662cd30a7" },
                    { 2, "56b171cf-06d6-4943-97d1-63b5d914a348" },
                    { 3, "42949812-d05e-46a5-8de6-cb3520319734" },
                    { 4, "3d522657-3ef9-4118-9b07-e5cd9bfb8614" },
                    { 5, "ca1ee9ab-8832-464f-a197-7e64d8a4e8af" }
                });

            migrationBuilder.InsertData(
                table: "Children",
                columns: new[] { "Id", "FirstName", "LastName", "ParentId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Иван", "Петров", 1, "0895741239" },
                    { 2, "Валентина", "Стратева", 2, "0895741000" },
                    { 3, "Мария", "Стратева", 2, "0895987987" },
                    { 4, "Ребека", "Стратева", 2, "0895987112" },
                    { 5, "Димитър", "Кирилов", 4, "0895965532" },
                    { 6, "Емил", "Кирилов", 4, "0887951146" },
                    { 7, "Матилда", "Кирилова", 4, "0899915858" },
                    { 8, "Мартина", "Петкова", 5, "0899915333" },
                    { 9, "Петко", "Петков", 5, "0897332255" }
                });

            migrationBuilder.InsertData(
                table: "TeachersCourses",
                columns: new[] { "CourseId", "TeacherId" },
                values: new object[,]
                {
                    { 5, 1 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 4, 4 },
                    { 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "TeachersSpecialties",
                columns: new[] { "SpecialtyId", "TeacherId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 4, 2 },
                    { 5, 2 },
                    { 3, 3 },
                    { 5, 3 },
                    { 6, 4 },
                    { 8, 4 },
                    { 5, 5 },
                    { 6, 5 },
                    { 7, 5 }
                });

            migrationBuilder.InsertData(
                table: "CourseRequests",
                columns: new[] { "Id", "ChildId", "CourseId", "Status" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 1 },
                    { 3, 3, 1, 1 },
                    { 4, 4, 1, 1 },
                    { 5, 2, 2, 1 },
                    { 6, 5, 2, 1 },
                    { 7, 5, 3, 2 },
                    { 8, 5, 4, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Children_ParentId",
                table: "Children",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequests_ChildId",
                table: "CourseRequests",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequests_CourseId",
                table: "CourseRequests",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AgeGroupId",
                table: "Courses",
                column: "AgeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseTypeId",
                table: "Courses",
                column: "CourseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersCourses_CourseId",
                table: "TeachersCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersSpecialties_SpecialtyId",
                table: "TeachersSpecialties",
                column: "SpecialtyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CourseRequests");

            migrationBuilder.DropTable(
                name: "TeachersCourses");

            migrationBuilder.DropTable(
                name: "TeachersSpecialties");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "AgeGroups");

            migrationBuilder.DropTable(
                name: "CourseTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
