using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boards.Infrastructure.Migrations
{
    public partial class InitVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Dbo");

            migrationBuilder.EnsureSchema(
                name: "acl");

            migrationBuilder.CreateTable(
                name: "AuditHistory",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutoNumbers",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Minister",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StreetAddress_Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Postcode = table.Column<short>(type: "smallint", nullable: true),
                    StreetAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Postcode = table.Column<short>(type: "smallint", nullable: true),
                    PostalAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minister", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionKeys",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                schema: "acl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "acl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Secretaries",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StreetAddress_Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Postcode = table.Column<short>(type: "smallint", nullable: true),
                    StreetAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Postcode = table.Column<short>(type: "smallint", nullable: true),
                    PostalAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "acl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StreetAddress_Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Postcode = table.Column<short>(type: "smallint", nullable: true),
                    StreetAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Postcode = table.Column<short>(type: "smallint", nullable: true),
                    PostalAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionSet",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionKeyId = table.Column<long>(type: "bigint", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionSet_OptionKeys_OptionKeyId",
                        column: x => x.OptionKeyId,
                        principalSchema: "Dbo",
                        principalTable: "OptionKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MinisterTerms",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinisterId = table.Column<long>(type: "bigint", nullable: false),
                    PortfolioId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinisterTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinisterTerms_Minister_MinisterId",
                        column: x => x.MinisterId,
                        principalSchema: "Dbo",
                        principalTable: "Minister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinisterTerms_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalSchema: "Dbo",
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "acl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppResourceId = table.Column<long>(type: "bigint", nullable: false),
                    AppRoleId = table.Column<long>(type: "bigint", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    Create = table.Column<bool>(type: "bit", nullable: false),
                    Update = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    Mask = table.Column<int>(type: "int", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Resources_AppResourceId",
                        column: x => x.AppResourceId,
                        principalSchema: "acl",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalSchema: "acl",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "acl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppRoleId = table.Column<long>(type: "bigint", nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Roles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalSchema: "acl",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointee",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Biography = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    PostNominals = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ResumeLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LinkedInProfile = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsRegional = table.Column<bool>(type: "bit", nullable: true),
                    IsAboriginal = table.Column<bool>(type: "bit", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: true),
                    ExecutiveSearch = table.Column<bool>(type: "bit", nullable: true),
                    CapabilitiesId = table.Column<long>(type: "bigint", nullable: true),
                    ExperienceId = table.Column<long>(type: "bigint", nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StreetAddress_Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Postcode = table.Column<short>(type: "smallint", nullable: true),
                    StreetAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Postcode = table.Column<short>(type: "smallint", nullable: true),
                    PostalAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointee_OptionSet_CapabilitiesId",
                        column: x => x.CapabilitiesId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointee_OptionSet_ExperienceId",
                        column: x => x.ExperienceId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Board",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioId = table.Column<long>(type: "bigint", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    PendingAction = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    EstablishedByUnderText = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    AssistantSecretory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AssistantSecretoryPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NominationCommittee = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OwnerDivision = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OwnerPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LegislationReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Constitution = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ResponsibleOfficer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    QuorumRequiredText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OptimumMembers = table.Column<int>(type: "int", nullable: true),
                    MaximumTerms = table.Column<int>(type: "int", nullable: true),
                    MaximumMembers = table.Column<int>(type: "int", nullable: true),
                    MinimumMembers = table.Column<int>(type: "int", nullable: true),
                    QuorumRequired = table.Column<int>(type: "int", nullable: false),
                    ReportingApproved = table.Column<bool>(type: "bit", nullable: false),
                    ExcludeFromGenderBalance = table.Column<bool>(type: "bit", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    DivisionId = table.Column<long>(type: "bigint", nullable: true),
                    EstablishedByUnderId = table.Column<long>(type: "bigint", nullable: true),
                    StatusColorId = table.Column<long>(type: "bigint", nullable: true),
                    ApprovedAppUserId = table.Column<int>(type: "int", nullable: true),
                    ApprovedId = table.Column<long>(type: "bigint", nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Board_OptionSet_DivisionId",
                        column: x => x.DivisionId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Board_OptionSet_EstablishedByUnderId",
                        column: x => x.EstablishedByUnderId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Board_OptionSet_StatusColorId",
                        column: x => x.StatusColorId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Board_OptionSet_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Board_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalSchema: "Dbo",
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Board_Users_ApprovedId",
                        column: x => x.ApprovedId,
                        principalSchema: "acl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamRoles",
                schema: "acl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppTeamId = table.Column<long>(type: "bigint", nullable: false),
                    AppRoleId = table.Column<long>(type: "bigint", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamRoles_Roles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalSchema: "acl",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamRoles_Teams_AppTeamId",
                        column: x => x.AppTeamId,
                        principalSchema: "acl",
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamUsers",
                schema: "acl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppTeamId = table.Column<long>(type: "bigint", nullable: false),
                    AppUserId = table.Column<long>(type: "bigint", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamUsers_Teams_AppTeamId",
                        column: x => x.AppTeamId,
                        principalSchema: "acl",
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamUsers_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "acl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoardRole",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    BoardId = table.Column<long>(type: "bigint", nullable: false),
                    IncumbentId = table.Column<long>(type: "bigint", nullable: false),
                    AssistantSecretaryId = table.Column<long>(type: "bigint", nullable: false),
                    MinisterLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MinisterSubDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MinisterActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CabinetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExCoDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VacantFromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotifyLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessStatus = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Responsibilities = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    NextSteps = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MinLetterDateType = table.Column<int>(type: "int", nullable: false),
                    NotLetterDateType = table.Column<int>(type: "int", nullable: false),
                    ExCoDateType = table.Column<int>(type: "int", nullable: false),
                    CabinetDateType = table.Column<int>(type: "int", nullable: false),
                    PositionTypeId = table.Column<long>(type: "bigint", nullable: true),
                    IsApsEmployee = table.Column<bool>(type: "bit", nullable: true),
                    IsSignificant = table.Column<bool>(type: "bit", nullable: true),
                    IsExecutive = table.Column<bool>(type: "bit", nullable: true),
                    ReasonForGenderExcludeId = table.Column<long>(type: "bigint", nullable: true),
                    EstablishedByUnderId = table.Column<long>(type: "bigint", nullable: true),
                    MinSubLocationId = table.Column<long>(type: "bigint", nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentSourceId = table.Column<long>(type: "bigint", nullable: true),
                    RemunerationPeriodId = table.Column<long>(type: "bigint", nullable: true),
                    ApproverTypeId = table.Column<long>(type: "bigint", nullable: true),
                    RemunerationMethodId = table.Column<long>(type: "bigint", nullable: true),
                    AppointmentStateId = table.Column<long>(type: "bigint", nullable: true),
                    SourceId = table.Column<long>(type: "bigint", nullable: true),
                    SelectionProcessId = table.Column<long>(type: "bigint", nullable: true),
                    PositionId = table.Column<long>(type: "bigint", nullable: true),
                    PositionRemuneratedId = table.Column<long>(type: "bigint", nullable: true),
                    JudicialId = table.Column<long>(type: "bigint", nullable: true),
                    ExcludeFromOrder15 = table.Column<bool>(type: "bit", nullable: true),
                    IsSemiDisc = table.Column<bool>(type: "bit", nullable: true),
                    ExOfficio = table.Column<bool>(type: "bit", nullable: true),
                    IsFullTime = table.Column<bool>(type: "bit", nullable: true),
                    ExcludeGenderReport = table.Column<bool>(type: "bit", nullable: true),
                    TermLimit = table.Column<int>(type: "int", nullable: true),
                    LeadTimeMonths = table.Column<int>(type: "int", nullable: true),
                    TermYears = table.Column<int>(type: "int", nullable: true),
                    MaxService = table.Column<int>(type: "int", nullable: true),
                    PdAmount = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    PdRemuneration = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaAmount = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    PaRemuneration = table.Column<decimal>(type: "decimal(13,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardRole_Appointee_IncumbentId",
                        column: x => x.IncumbentId,
                        principalSchema: "Dbo",
                        principalTable: "Appointee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_Board_BoardId",
                        column: x => x.BoardId,
                        principalSchema: "Dbo",
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_AppointmentSourceId",
                        column: x => x.AppointmentSourceId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_AppointmentStateId",
                        column: x => x.AppointmentStateId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_ApproverTypeId",
                        column: x => x.ApproverTypeId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_EstablishedByUnderId",
                        column: x => x.EstablishedByUnderId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_JudicialId",
                        column: x => x.JudicialId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_MinSubLocationId",
                        column: x => x.MinSubLocationId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_PositionRemuneratedId",
                        column: x => x.PositionRemuneratedId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_PositionTypeId",
                        column: x => x.PositionTypeId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_ReasonForGenderExcludeId",
                        column: x => x.ReasonForGenderExcludeId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_RemunerationMethodId",
                        column: x => x.RemunerationMethodId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_RemunerationPeriodId",
                        column: x => x.RemunerationPeriodId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_SelectionProcessId",
                        column: x => x.SelectionProcessId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_OptionSet_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRole_Secretaries_AssistantSecretaryId",
                        column: x => x.AssistantSecretaryId,
                        principalSchema: "Dbo",
                        principalTable: "Secretaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoardRoleEvent",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardRoleId = table.Column<long>(type: "bigint", nullable: false),
                    AppointeeId = table.Column<long>(type: "bigint", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InitialStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BriefNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsRemunerated = table.Column<int>(type: "int", nullable: false),
                    TermsServedId = table.Column<long>(type: "bigint", nullable: true),
                    AppointmentStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EndDateUnknown = table.Column<bool>(type: "bit", nullable: true),
                    IsProposed = table.Column<bool>(type: "bit", nullable: true),
                    IsCurrentAppointment = table.Column<bool>(type: "bit", nullable: true),
                    IsActing = table.Column<bool>(type: "bit", nullable: true),
                    AppointedDays = table.Column<int>(type: "int", nullable: true),
                    AppointmentDays = table.Column<int>(type: "int", nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentSourceId = table.Column<long>(type: "bigint", nullable: true),
                    RemunerationPeriodId = table.Column<long>(type: "bigint", nullable: true),
                    ApproverTypeId = table.Column<long>(type: "bigint", nullable: true),
                    RemunerationMethodId = table.Column<long>(type: "bigint", nullable: true),
                    AppointmentStateId = table.Column<long>(type: "bigint", nullable: true),
                    SourceId = table.Column<long>(type: "bigint", nullable: true),
                    SelectionProcessId = table.Column<long>(type: "bigint", nullable: true),
                    PositionId = table.Column<long>(type: "bigint", nullable: true),
                    PositionRemuneratedId = table.Column<long>(type: "bigint", nullable: true),
                    JudicialId = table.Column<long>(type: "bigint", nullable: true),
                    ExcludeFromOrder15 = table.Column<bool>(type: "bit", nullable: true),
                    IsSemiDisc = table.Column<bool>(type: "bit", nullable: true),
                    ExOfficio = table.Column<bool>(type: "bit", nullable: true),
                    IsFullTime = table.Column<bool>(type: "bit", nullable: true),
                    ExcludeGenderReport = table.Column<bool>(type: "bit", nullable: true),
                    TermLimit = table.Column<int>(type: "int", nullable: true),
                    LeadTimeMonths = table.Column<int>(type: "int", nullable: true),
                    TermYears = table.Column<int>(type: "int", nullable: true),
                    MaxService = table.Column<int>(type: "int", nullable: true),
                    PdAmount = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    PdRemuneration = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaAmount = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    PaRemuneration = table.Column<decimal>(type: "decimal(13,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardRoleEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_Appointee_AppointeeId",
                        column: x => x.AppointeeId,
                        principalSchema: "Dbo",
                        principalTable: "Appointee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_BoardRole_BoardRoleId",
                        column: x => x.BoardRoleId,
                        principalSchema: "Dbo",
                        principalTable: "BoardRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_AppointmentSourceId",
                        column: x => x.AppointmentSourceId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_AppointmentStateId",
                        column: x => x.AppointmentStateId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_AppointmentStatusId",
                        column: x => x.AppointmentStatusId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_ApproverTypeId",
                        column: x => x.ApproverTypeId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_JudicialId",
                        column: x => x.JudicialId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_PositionRemuneratedId",
                        column: x => x.PositionRemuneratedId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_RemunerationMethodId",
                        column: x => x.RemunerationMethodId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_RemunerationPeriodId",
                        column: x => x.RemunerationPeriodId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_SelectionProcessId",
                        column: x => x.SelectionProcessId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardRoleEvent_OptionSet_TermsServedId",
                        column: x => x.TermsServedId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointee_CapabilitiesId",
                schema: "Dbo",
                table: "Appointee",
                column: "CapabilitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointee_ExperienceId",
                schema: "Dbo",
                table: "Appointee",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Board_ApprovedId",
                schema: "Dbo",
                table: "Board",
                column: "ApprovedId");

            migrationBuilder.CreateIndex(
                name: "IX_Board_DivisionId",
                schema: "Dbo",
                table: "Board",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Board_EstablishedByUnderId",
                schema: "Dbo",
                table: "Board",
                column: "EstablishedByUnderId");

            migrationBuilder.CreateIndex(
                name: "IX_Board_PortfolioId",
                schema: "Dbo",
                table: "Board",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Board_StatusColorId",
                schema: "Dbo",
                table: "Board",
                column: "StatusColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Board_StatusId",
                schema: "Dbo",
                table: "Board",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_AppointmentSourceId",
                schema: "Dbo",
                table: "BoardRole",
                column: "AppointmentSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_AppointmentStateId",
                schema: "Dbo",
                table: "BoardRole",
                column: "AppointmentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_ApproverTypeId",
                schema: "Dbo",
                table: "BoardRole",
                column: "ApproverTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_AssistantSecretaryId",
                schema: "Dbo",
                table: "BoardRole",
                column: "AssistantSecretaryId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_BoardId",
                schema: "Dbo",
                table: "BoardRole",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_EstablishedByUnderId",
                schema: "Dbo",
                table: "BoardRole",
                column: "EstablishedByUnderId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_IncumbentId",
                schema: "Dbo",
                table: "BoardRole",
                column: "IncumbentId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_JudicialId",
                schema: "Dbo",
                table: "BoardRole",
                column: "JudicialId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_MinSubLocationId",
                schema: "Dbo",
                table: "BoardRole",
                column: "MinSubLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_PositionId",
                schema: "Dbo",
                table: "BoardRole",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_PositionRemuneratedId",
                schema: "Dbo",
                table: "BoardRole",
                column: "PositionRemuneratedId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_PositionTypeId",
                schema: "Dbo",
                table: "BoardRole",
                column: "PositionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_ReasonForGenderExcludeId",
                schema: "Dbo",
                table: "BoardRole",
                column: "ReasonForGenderExcludeId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_RemunerationMethodId",
                schema: "Dbo",
                table: "BoardRole",
                column: "RemunerationMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_RemunerationPeriodId",
                schema: "Dbo",
                table: "BoardRole",
                column: "RemunerationPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_SelectionProcessId",
                schema: "Dbo",
                table: "BoardRole",
                column: "SelectionProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRole_SourceId",
                schema: "Dbo",
                table: "BoardRole",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_AppointeeId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "AppointeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_AppointmentSourceId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "AppointmentSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_AppointmentStateId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "AppointmentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_AppointmentStatusId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "AppointmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_ApproverTypeId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "ApproverTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_BoardRoleId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "BoardRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_JudicialId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "JudicialId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_PositionId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_PositionRemuneratedId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "PositionRemuneratedId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_RemunerationMethodId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "RemunerationMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_RemunerationPeriodId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "RemunerationPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_SelectionProcessId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "SelectionProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_SourceId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoleEvent_TermsServedId",
                schema: "Dbo",
                table: "BoardRoleEvent",
                column: "TermsServedId");

            migrationBuilder.CreateIndex(
                name: "IX_MinisterTerms_MinisterId",
                schema: "Dbo",
                table: "MinisterTerms",
                column: "MinisterId");

            migrationBuilder.CreateIndex(
                name: "IX_MinisterTerms_PortfolioId",
                schema: "Dbo",
                table: "MinisterTerms",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionKeys_Code",
                schema: "Dbo",
                table: "OptionKeys",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OptionSet_OptionKeyId",
                schema: "Dbo",
                table: "OptionSet",
                column: "OptionKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AppResourceId",
                schema: "acl",
                table: "Permissions",
                column: "AppResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AppRoleId",
                schema: "acl",
                table: "Permissions",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamRoles_AppRoleId",
                schema: "acl",
                table: "TeamRoles",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamRoles_AppTeamId",
                schema: "acl",
                table: "TeamRoles",
                column: "AppTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_AppRoleId",
                schema: "acl",
                table: "Teams",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_AppTeamId",
                schema: "acl",
                table: "TeamUsers",
                column: "AppTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_AppUserId",
                schema: "acl",
                table: "TeamUsers",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditHistory",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "AutoNumbers",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "BoardRoleEvent",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "MinisterTerms",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "TeamRoles",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "TeamUsers",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "BoardRole",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Minister",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Appointee",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Board",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Secretaries",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "OptionSet",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Portfolios",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "OptionKeys",
                schema: "Dbo");
        }
    }
}
