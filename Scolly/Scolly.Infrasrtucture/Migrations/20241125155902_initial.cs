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
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
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
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeGroupId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Courses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseRequests_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
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
                    { "0ab7ca6b-8108-4b96-a300-f998e93c1b83", 0, "ул. 'Преслав' 10", 4, "90a1f7f1-e781-4a7a-b7b0-9a5c3b44a9f9", "employee3@gmail.com", true, "Петър", "Нешев", false, null, "Валентинов", null, null, "AQAAAAIAAYagAAAAEDUe52jaFJrRBRKRsysyU2GfWEq9kEOjItU2eUvTsu4R5LgtJDDRmIXOMZVcDxDF+w==", "0877512889", false, "2efd5431-8506-4fca-9878-ecf0c4d6ddba", false, "employee3@gmail.com" },
                    { "1d5a3070-fc27-4172-ad7c-8ef84fb88f7f", 0, "ул. 'Плиска' 22", 6, "b0b0fd31-1a1d-4a9b-b507-4c4c6a6fac83", "employee2@gmail.com", true, "Алиса", "Нешева", false, null, "Емилиянова", null, null, "AQAAAAIAAYagAAAAEJNqRFrIcBH4Z3rp4QcIFR1j8jpq2iyVnBg3IGEK0xaVpItsr9jK8ZcNRnWMxxc4Mg==", "0877512844", false, "18d30ff6-8801-471b-b6a4-75b0ede61bcf", false, "employee2@gmail.com" },
                    { "3d522657-3ef9-4118-9b07-e5cd9bfb8614", 0, "ул. 'Васил Левски' 155А", 4, "793ed8b0-beef-4f18-8475-dbaaba407b2b", "teacher4@gmail.com", true, "Виктор", "Стефанов", false, null, "Петров", null, null, "AQAAAAIAAYagAAAAEJlvI5iHHACVGQqVZZTaNbNQsC1CEFtm1fQYv3o6h5xGEGvzNBaVsfkccyeFpexsiw==", "0899745997", false, "b6a8029a-4795-4f4b-bbdd-001e5c71b7e6", false, "teacher4@gmail.com" },
                    { "42949812-d05e-46a5-8de6-cb3520319734", 0, "ул. 'Мусала' 12", 2, "11812e33-53e4-4da3-af38-02ad97097e71", "teacher3@gmail.com", true, "Мария", "Димитрова", false, null, "Атанасова", null, null, "AQAAAAIAAYagAAAAEIvbUgsc2SYqa2iAB+cUPLqll6vvVZBsg7C/JxBdhYL/HtEWjJkURrkKVZelweleEw==", "0899745867", false, "ee224a61-6d6b-45bb-9a85-6d4ece401ef3", false, "teacher3@gmail.com" },
                    { "4560faa9-53cd-4adc-8d65-4f7662cd30a7", 0, "ул. 'Никола Габровски' 15А", 3, "7df0b861-078e-413e-8e7e-39ee3b6b655a", "teacher1@gmail.com", true, "Николай", "Николов", false, null, "Иванов", null, null, "AQAAAAIAAYagAAAAELyHgIyrMqET9KRgwXSp1VjWeYra7rEkW665oWhBubejZFSpTs/Js3rY9+CrlWgv+Q==", "0884672591", false, "903056fd-d160-4df4-9fae-ff5ecb0a29a6", false, "teacher1@gmail.com" },
                    { "56b171cf-06d6-4943-97d1-63b5d914a348", 0, "ул. 'Стоян Коледаров' 6", 2, "cf097074-fdd6-4682-bd52-522d76c45df8", "teacher2@gmail.com", true, "Преслав", "Калоянов", false, null, "Николаев", null, null, "AQAAAAIAAYagAAAAEKWq6Jkbfri29IVclJIlRlmDlQJaxopQo/HYtZRFP/HwzR81P8RhGYVmX1VWSqWL0g==", "0888967530", false, "1fbcc65c-82a9-4fd9-998a-d5c3cab0ca80", false, "teacher2@gmail.com" },
                    { "7e124ab7-6050-49d8-853f-e51897ff536f", 0, "ул. 'Георги Бенковски' 25", 4, "893afaf3-eef7-4981-b921-7dc84307bba4", "parent5@gmail.com", true, "Даниела", "Петкова", false, null, "Сергеева", null, null, "AQAAAAIAAYagAAAAEN3JOx2GCjqilhdzWisWNSOLo2ZuWjvcUVMQcX4RQTTmWYFeCthHSRd39fUrZHVFUw==", "0813528746", false, "b0fa1290-654e-49db-9ace-cd1672925199", false, "parent5@gmail.com" },
                    { "80cae098-d8ab-42e2-bec2-0bb11d0a441e", 0, "ул. 'Петя Дубарова' 16Б", 6, "3ca63edd-e434-4d26-820b-a74018828b0c", "parent3@gmail.com", true, "Виктория", "Ангелова", false, null, "Николаева", null, null, "AQAAAAIAAYagAAAAEE6r1hBrpuRNFZMzV2nVtV+QuVXqnZQJ0MlNbC2I4hVi29ya+IbD28qCf9EnroqO9w==", "0854789615", false, "7887f6d7-0d06-4d30-9d2d-14b45ef4ff77", false, "parent3@gmail.com" },
                    { "9789ec17-8fe9-47c2-9577-a2e86ebc85ea", 0, "ул. 'Петко Р. Славейков' 4Б", 1, "3b5c5a92-9ff8-4c41-be95-0b559f6f7879", "employee1@gmail.com", true, "Димитър", "Иванов", false, null, "Спасов", null, null, "AQAAAAIAAYagAAAAEBE4Dp4PrcinX26ZrlFFtqwTidBP0Z6LKF0UrHwGciUKoXNEO27to2PS0M4Qhe/TEA==", "0823167589", false, "9d93c421-c0a7-4dd3-a295-e374ba275aab", false, "employee1@gmail.com" },
                    { "aab83f21-bfce-46dc-b9b5-3fd6dd1d17f0", 0, "Необособен", 1, "3d7fa0cc-aceb-484b-8b1b-d109a8e2254c", "admin@admin.bg", true, "Admin", "Admin", false, null, "Admin", null, null, "AQAAAAIAAYagAAAAEHwJxQEJqfYWBwjYrqEF6gNDuEtxt2xq5Ova7Lcwoz2OsXLTa0pRP3vlTnM61dQ0Nw==", "0812345678", false, "42f32251-d226-48b4-ad62-2f77b634a360", false, "admin" },
                    { "b79f3723-9600-499a-b640-a368f7816b47", 0, "ул. 'Освобождение' 3Б", 4, "45479299-0cd1-4e68-bca4-c89bc55a2f53", "parent4@gmail.com", true, "Девора", "Кирилова", false, null, "Кирилова", null, null, "AQAAAAIAAYagAAAAEEQvHYPMblghgpXIAbcDMqUNlvy+zH8iaBT3E4iIVvY5wjEM5ghGuQ74gw4yv2qBRw==", "0865789488", false, "c12e4d11-5862-4666-b1b4-18042d8c53be", false, "parent4@gmail.com" },
                    { "ca1ee9ab-8832-464f-a197-7e64d8a4e8af", 0, "ул. 'Александър Малинов' 33", 5, "c2622baf-f8f4-4069-8889-849f1c7097ec", "teacher5@gmail.com", true, "Емил", "Долчинков", false, null, "Давидов", null, null, "AQAAAAIAAYagAAAAEEhDrCmOXS7OUEQ4OxpJ4/LZ/MHRVVDNc+/q1wB73UIHatOwUXX9PvYtgEVdptJVgQ==", "0899745000", false, "ea5c4eb8-fe90-4ffb-b399-ced967f84709", false, "teacher5@gmail.com" },
                    { "e174d710-f687-4a09-876c-4a8690d393e5", 0, "ул. 'Стоян Коледаров' 3А", 1, "5aa003f2-c399-4f70-8ee2-91b64d8b188d", "parent2@gmail.com", true, "Александър", "Стратиев", false, null, "Иванов", null, null, "AQAAAAIAAYagAAAAEDDt264ljm7PXWX8Kk4TLbqCz2TcztYS8NCcea/DEsEc2L2QDUaMiXVnW6/yVReMkg==", "0854700615", false, "45040c90-d0f0-48c9-8e28-8502fe70d118", false, "parent2@gmail.com" },
                    { "f8ea9f65-046d-47ea-86c6-9e3c7b6b7d2c", 0, "ул. 'Опълченска' 3А", 3, "ae0e1daa-3ab3-47b7-9dd8-ea597af7852d", "parent1@gmail.com", true, "Петър", "Петров", false, null, "Иванов", null, null, "AQAAAAIAAYagAAAAECDe8m0ZJIM1dPq7gML7H4pexCdxwMpf9D9dRLVoO44MRHKSOMNwblGzzSOeAoeTZg==", "0859989615", false, "c1ff9cca-a0f1-42ed-93f7-0071ab38574b", false, "parent1@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, "9789ec17-8fe9-47c2-9577-a2e86ebc85ea" },
                    { 2, "1d5a3070-fc27-4172-ad7c-8ef84fb88f7f" },
                    { 3, "0ab7ca6b-8108-4b96-a300-f998e93c1b83" }
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
                table: "Courses",
                columns: new[] { "Id", "AgeGroupId", "CourseTypeId", "EmployeeId", "EndDate", "Price", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, 2, 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 200m, new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 2, 1, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 250m, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 4, 5, 2, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 630m, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 6, 3, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 590m, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 3, 3, 3, new DateTime(2024, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 990m, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                name: "IX_Courses_EmployeeId",
                table: "Courses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

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
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
