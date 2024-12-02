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
                    { "3d522657-3ef9-4118-9b07-e5cd9bfb8614", 0, "ул. 'Васил Левски' 155А", 4, "41f5ed4d-d2d8-4f69-9857-815f834d9709", "teacher4@gmail.com", true, "Виктор", "Стефанов", false, null, "Петров", "TEACHER4@GMAIL.COM", "TEACHER4@GMAIL.COM", "AQAAAAIAAYagAAAAEJyZf1ou0E164agbGsNDplWG69vm0lwyeKyb+u99yKjxJqE3VK38Z5z/FX4ug4ynkg==", "0899745997", false, "5a04e254-05d2-40df-9c2b-f18a59210ef5", false, "teacher4@gmail.com" },
                    { "42949812-d05e-46a5-8de6-cb3520319734", 0, "ул. 'Мусала' 12", 2, "2979670a-786f-4fbb-ae0f-d90a89a3e10b", "teacher3@gmail.com", true, "Мария", "Димитрова", false, null, "Атанасова", "TEACHER3@GMAIL.COM", "TEACHER3@GMAIL.COM", "AQAAAAIAAYagAAAAEHTqhJupUN+XeMUGHBq46EXlvSqQcuxH+k2dYoV4lAK888r3bswPuShkUEcC1TNzYw==", "0899745867", false, "ac92daf5-44cc-463e-9345-6d0fc012849e", false, "teacher3@gmail.com" },
                    { "4560faa9-53cd-4adc-8d65-4f7662cd30a7", 0, "ул. 'Никола Габровски' 15А", 3, "61e04a7d-2a00-4ec8-a157-28cda6db4a57", "teacher1@gmail.com", true, "Николай", "Николов", false, null, "Иванов", "TEACHER1@GMAIL.COM", "TEACHER1@GMAIL.COM", "AQAAAAIAAYagAAAAEJcWoKZxzvwcLTP0GLRJjtbSPBsGTcLp19z8T6+gd2dZmmCyR8qmEZqRCbmfjuoyoA==", "0884672591", false, "9503bdbe-fa74-44bf-9e79-28d6b36b9fe8", false, "teacher1@gmail.com" },
                    { "56b171cf-06d6-4943-97d1-63b5d914a348", 0, "ул. 'Стоян Коледаров' 6", 2, "51f6fd07-9152-49cb-9ed6-2295d60b6f97", "teacher2@gmail.com", true, "Преслав", "Калоянов", false, null, "Николаев", "TEACHER2@GMAIL.COM", "TEACHER2@GMAIL.COM", "AQAAAAIAAYagAAAAEI4FfpBVWzwzaZmnfEf+/+xUDBZWkpNkT7hS6d9pEXBm7uySMfne+OtAOAkcwvHwxA==", "0888967530", false, "6a811705-5478-4a7d-b1ed-7869650ca13b", false, "teacher2@gmail.com" },
                    { "7e124ab7-6050-49d8-853f-e51897ff536f", 0, "ул. 'Георги Бенковски' 25", 4, "a3de3569-1e53-40a0-8b34-07ed1555b97e", "parent5@gmail.com", true, "Даниела", "Петкова", false, null, "Сергеева", "PARENT5@GMAIL.COM", "PARENT5@GMAIL.COM", "AQAAAAIAAYagAAAAENwdxLw2KJ3qUgxu1MBw9oYQjFD1PZcQCiqEwNYsAJhU00D9C+3rQz6E0NkrQsaJ9w==", "0813528746", false, "9e105f7f-d566-4505-a956-a9b3e769fcfb", false, "parent5@gmail.com" },
                    { "80cae098-d8ab-42e2-bec2-0bb11d0a441e", 0, "ул. 'Петя Дубарова' 16Б", 6, "6fe423eb-c560-48f0-ac6b-7a62334db61c", "parent3@gmail.com", true, "Виктория", "Ангелова", false, null, "Николаева", "PARENT3@GMAIL.COM", "PARENT3@GMAIL.COM", "AQAAAAIAAYagAAAAEOTpUMahO9ncVzFOtmAlbW6j615m2Gu90jmgP93CR1IJUNEv0d0/UyiVG7vGogKZTg==", "0854789615", false, "e4564128-8626-4d5c-ac02-c2b016fa3b6f", false, "parent3@gmail.com" },
                    { "aab83f21-bfce-46dc-b9b5-3fd6dd1d17f0", 0, "Необособен", 1, "791a205a-3ca7-4156-a7cb-ccf5a7602e6b", "admin@admin.bg", true, "Admin", "Admin", false, null, "Admin", "ADMIN@ADMIN.BG", "ADMIN", "AQAAAAIAAYagAAAAECH4Hk8jxcx4yzyftLGi6LWYGS2EvaS/iLxInK9CNDIjXFRoZZY7yBHw56dlZRyLrA==", "0812345678", false, "97c3c327-5a0b-4082-a2fb-806eab196f45", false, "admin" },
                    { "b79f3723-9600-499a-b640-a368f7816b47", 0, "ул. 'Освобождение' 3Б", 4, "c63e78df-0edd-4fa9-9053-c7ea3bf2a220", "parent4@gmail.com", true, "Девора", "Кирилова", false, null, "Кирилова", "PARENT4@GMAIL.COM", "PARENT4@GMAIL.COM", "AQAAAAIAAYagAAAAEG4r1X2pc9wKicXdLspAMa4LeCoanelnOuSbLJ2Q1pPAL+SudAzYSKQG1J3zyFUHvQ==", "0865789488", false, "b7449628-4736-4d01-91c8-3a5b70e57d70", false, "parent4@gmail.com" },
                    { "ca1ee9ab-8832-464f-a197-7e64d8a4e8af", 0, "ул. 'Александър Малинов' 33", 5, "cf285580-0c1d-42be-b974-dcc00f336d27", "teacher5@gmail.com", true, "Емил", "Долчинков", false, null, "Давидов", "TEACHER5@GMAIL.COM", "TEACHER5@GMAIL.COM", "AQAAAAIAAYagAAAAEG+e43y900M0zk0v3XiP2QL3sFz1jceJN+dfLcVgRg9n3FFv4/4rtez/8fZhjoZmMQ==", "0899745000", false, "a1918373-aca8-4fb8-a4c0-6df5ea5c4e31", false, "teacher5@gmail.com" },
                    { "e174d710-f687-4a09-876c-4a8690d393e5", 0, "ул. 'Стоян Коледаров' 3А", 1, "055caa41-c834-480c-8a9e-eb480e470d9a", "parent2@gmail.com", true, "Александър", "Стратиев", false, null, "Иванов", "PARENT2@GMAIL.COM", "PARENT2@GMAIL.COM", "AQAAAAIAAYagAAAAEO57dd4fZcIwdDFtF620mxRc9gmBIRYT4UWIFZ//kxpz13YXKxiN7X/yeoT/JD/N4Q==", "0854700615", false, "f8d33b5f-71fb-412c-9dff-fc4c3f6615d1", false, "parent2@gmail.com" },
                    { "f8ea9f65-046d-47ea-86c6-9e3c7b6b7d2c", 0, "ул. 'Опълченска' 3А", 3, "b1ec09c2-ab0a-422a-886e-ef31d8f967ff", "parent1@gmail.com", true, "Петър", "Петров", false, null, "Иванов", "PARENT1@GMAIL.COM", "PARENT1@GMAIL.COM", "AQAAAAIAAYagAAAAEIzUpIYS4lYlHEHBVCQ+yNOW3f1a4Nnab9T8UWEJUMxnL5S/BpnZsyDsAWE+BVHAOg==", "0859989615", false, "0a8fa5f5-0274-49f5-8db6-030db80b821a", false, "parent1@gmail.com" }
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
