using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boards.Infrastructure.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Dbo");

            migrationBuilder.EnsureSchema(
                name: "acl");

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

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
                name: "DashboardItems",
                schema: "Dbo",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "DeleteRecords",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeleteRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ministers",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MigratedId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_Ministers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionKeys",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    MigratedId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
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
                name: "Teams",
                schema: "acl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "acl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    NameId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Upn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobRole = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccessRequest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccessGranted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
                    MigratedId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                        principalColumn: "Id");
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
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                        name: "FK_MinisterTerms_Ministers_MinisterId",
                        column: x => x.MinisterId,
                        principalSchema: "Dbo",
                        principalTable: "Ministers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MinisterTerms_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalSchema: "Dbo",
                        principalTable: "Portfolios",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalSchema: "acl",
                        principalTable: "Roles",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamRoles_Teams_AppTeamId",
                        column: x => x.AppTeamId,
                        principalSchema: "acl",
                        principalTable: "Teams",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamUsers_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "acl",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "acl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<long>(type: "bigint", nullable: false),
                    AppRoleId = table.Column<long>(type: "bigint", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalSchema: "acl",
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "acl",
                        principalTable: "Users",
                        principalColumn: "Id");
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
                    MigratedId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointee_OptionSet_ExperienceId",
                        column: x => x.ExperienceId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioId = table.Column<long>(type: "bigint", nullable: false),
                    AppTeamId = table.Column<long>(type: "bigint", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    PendingAction = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    EstablishedByUnderText = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    NominationCommittee = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OwnerDivisionId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerPositionId = table.Column<long>(type: "bigint", nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LegislationReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Constitution = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    QuorumRequiredText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OptimumMembers = table.Column<int>(type: "int", nullable: true),
                    MaximumTerms = table.Column<int>(type: "int", nullable: true),
                    MaximumMembers = table.Column<int>(type: "int", nullable: true),
                    MinimumMembers = table.Column<int>(type: "int", nullable: true),
                    QuorumRequired = table.Column<int>(type: "int", nullable: false),
                    ReportingApproved = table.Column<bool>(type: "bit", nullable: false),
                    ExcludeFromGenderBalance = table.Column<bool>(type: "bit", nullable: false),
                    BoardStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EstablishedByUnderId = table.Column<long>(type: "bigint", nullable: true),
                    ApprovedUserId = table.Column<long>(type: "bigint", nullable: true),
                    ResponsibleUserId = table.Column<long>(type: "bigint", nullable: true),
                    AsstSecretaryId = table.Column<long>(type: "bigint", nullable: true),
                    AsstSecretaryPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaxServicePeriod = table.Column<int>(type: "int", nullable: true),
                    MigratedId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_Boards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boards_OptionSet_BoardStatusId",
                        column: x => x.BoardStatusId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boards_OptionSet_EstablishedByUnderId",
                        column: x => x.EstablishedByUnderId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boards_OptionSet_OwnerDivisionId",
                        column: x => x.OwnerDivisionId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boards_OptionSet_OwnerPositionId",
                        column: x => x.OwnerPositionId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boards_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalSchema: "Dbo",
                        principalTable: "Portfolios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boards_Secretaries_AsstSecretaryId",
                        column: x => x.AsstSecretaryId,
                        principalSchema: "Dbo",
                        principalTable: "Secretaries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boards_Teams_AppTeamId",
                        column: x => x.AppTeamId,
                        principalSchema: "acl",
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boards_Users_ApprovedUserId",
                        column: x => x.ApprovedUserId,
                        principalSchema: "acl",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boards_Users_ResponsibleUserId",
                        column: x => x.ResponsibleUserId,
                        principalSchema: "acl",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillTypeId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_OptionSet_SkillTypeId",
                        column: x => x.SkillTypeId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BoardRoles",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BoardId = table.Column<long>(type: "bigint", nullable: false),
                    IncumbentId = table.Column<long>(type: "bigint", nullable: true),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    RoleAppointerId = table.Column<long>(type: "bigint", nullable: false),
                    IsFullTime = table.Column<int>(type: "int", nullable: false),
                    IsExecutive = table.Column<bool>(type: "bit", nullable: true),
                    IsExOfficio = table.Column<bool>(type: "bit", nullable: true),
                    IsApsEmployee = table.Column<bool>(type: "bit", nullable: true),
                    IsExNominated = table.Column<bool>(type: "bit", nullable: true),
                    Term = table.Column<int>(type: "int", nullable: true),
                    PositionRemunerated = table.Column<int>(type: "int", nullable: false),
                    PaAmount = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    RemunerationMethodId = table.Column<long>(type: "bigint", nullable: false),
                    RemunerationTribunal = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VacantFromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExcludeFromOrder15 = table.Column<bool>(type: "bit", nullable: true),
                    ExcludeGenderReport = table.Column<bool>(type: "bit", nullable: true),
                    IsSignAppointment = table.Column<bool>(type: "bit", nullable: true),
                    NextSteps = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    InstrumentLink = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    PDMSNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinSubLocationId = table.Column<long>(type: "bigint", nullable: true),
                    MinisterOfficeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MinisterActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LetterToPmDateType = table.Column<int>(type: "int", nullable: false),
                    LetterToPmDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExCoDateType = table.Column<int>(type: "int", nullable: false),
                    ExCoDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotifyLetterDateType = table.Column<int>(type: "int", nullable: false),
                    NotifyLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CabinetDateType = table.Column<int>(type: "int", nullable: false),
                    CabinetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InternalNotes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ProcessStatus = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    LeadTimeToAppoint = table.Column<int>(type: "int", nullable: true),
                    MinSubDateType = table.Column<int>(type: "int", nullable: false),
                    MinSubDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IncumbentName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IncumbentStartDate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IncumbentEndDate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MigratedId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AssistantSecretaryId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_BoardRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardRoles_Appointee_IncumbentId",
                        column: x => x.IncumbentId,
                        principalSchema: "Dbo",
                        principalTable: "Appointee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardRoles_Boards_BoardId",
                        column: x => x.BoardId,
                        principalSchema: "Dbo",
                        principalTable: "Boards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardRoles_OptionSet_MinSubLocationId",
                        column: x => x.MinSubLocationId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardRoles_OptionSet_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardRoles_OptionSet_RemunerationMethodId",
                        column: x => x.RemunerationMethodId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardRoles_OptionSet_RoleAppointerId",
                        column: x => x.RoleAppointerId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardRoles_Secretaries_AssistantSecretaryId",
                        column: x => x.AssistantSecretaryId,
                        principalSchema: "Dbo",
                        principalTable: "Secretaries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppointeeSkill",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointeeId = table.Column<long>(type: "bigint", nullable: false),
                    SkillId = table.Column<long>(type: "bigint", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointeeSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointeeSkill_Appointee_AppointeeId",
                        column: x => x.AppointeeId,
                        principalSchema: "Dbo",
                        principalTable: "Appointee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppointeeSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalSchema: "Dbo",
                        principalTable: "Skill",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BoardAppointments",
                schema: "Dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BoardId = table.Column<long>(type: "bigint", nullable: false),
                    BoardRoleId = table.Column<long>(type: "bigint", nullable: false),
                    AppointeeId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateUnknown = table.Column<bool>(type: "bit", nullable: false),
                    BriefNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: true),
                    IsExOfficio = table.Column<bool>(type: "bit", nullable: true),
                    ActingInRole = table.Column<bool>(type: "bit", nullable: false),
                    ExclGenderReport = table.Column<bool>(type: "bit", nullable: true),
                    IsFullTime = table.Column<int>(type: "int", nullable: false),
                    AnnumAmount = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    RemunerationPeriodId = table.Column<long>(type: "bigint", nullable: true),
                    AppointmentSourceId = table.Column<long>(type: "bigint", nullable: true),
                    SelectionProcessId = table.Column<long>(type: "bigint", nullable: true),
                    JudicialId = table.Column<long>(type: "bigint", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InitialStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrevTerms = table.Column<int>(type: "int", nullable: true),
                    IsSemiDiscretionary = table.Column<bool>(type: "bit", nullable: true),
                    Proposed = table.Column<bool>(type: "bit", nullable: true),
                    AppointerId = table.Column<long>(type: "bigint", nullable: true),
                    MigratedId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_BoardAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardAppointments_Appointee_AppointeeId",
                        column: x => x.AppointeeId,
                        principalSchema: "Dbo",
                        principalTable: "Appointee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardAppointments_BoardRoles_BoardRoleId",
                        column: x => x.BoardRoleId,
                        principalSchema: "Dbo",
                        principalTable: "BoardRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardAppointments_Boards_BoardId",
                        column: x => x.BoardId,
                        principalSchema: "Dbo",
                        principalTable: "Boards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardAppointments_OptionSet_AppointerId",
                        column: x => x.AppointerId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardAppointments_OptionSet_AppointmentSourceId",
                        column: x => x.AppointmentSourceId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardAppointments_OptionSet_JudicialId",
                        column: x => x.JudicialId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardAppointments_OptionSet_RemunerationPeriodId",
                        column: x => x.RemunerationPeriodId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardAppointments_OptionSet_SelectionProcessId",
                        column: x => x.SelectionProcessId,
                        principalSchema: "Dbo",
                        principalTable: "OptionSet",
                        principalColumn: "Id");
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
                name: "IX_AppointeeSkill_AppointeeId",
                schema: "Dbo",
                table: "AppointeeSkill",
                column: "AppointeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointeeSkill_SkillId",
                schema: "Dbo",
                table: "AppointeeSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardAppointments_AppointeeId",
                schema: "Dbo",
                table: "BoardAppointments",
                column: "AppointeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardAppointments_AppointerId",
                schema: "Dbo",
                table: "BoardAppointments",
                column: "AppointerId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardAppointments_AppointmentSourceId",
                schema: "Dbo",
                table: "BoardAppointments",
                column: "AppointmentSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardAppointments_BoardId",
                schema: "Dbo",
                table: "BoardAppointments",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardAppointments_BoardRoleId",
                schema: "Dbo",
                table: "BoardAppointments",
                column: "BoardRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardAppointments_JudicialId",
                schema: "Dbo",
                table: "BoardAppointments",
                column: "JudicialId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardAppointments_RemunerationPeriodId",
                schema: "Dbo",
                table: "BoardAppointments",
                column: "RemunerationPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardAppointments_SelectionProcessId",
                schema: "Dbo",
                table: "BoardAppointments",
                column: "SelectionProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoles_AssistantSecretaryId",
                schema: "Dbo",
                table: "BoardRoles",
                column: "AssistantSecretaryId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoles_BoardId",
                schema: "Dbo",
                table: "BoardRoles",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoles_IncumbentId",
                schema: "Dbo",
                table: "BoardRoles",
                column: "IncumbentId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoles_MinSubLocationId",
                schema: "Dbo",
                table: "BoardRoles",
                column: "MinSubLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoles_PositionId",
                schema: "Dbo",
                table: "BoardRoles",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoles_RemunerationMethodId",
                schema: "Dbo",
                table: "BoardRoles",
                column: "RemunerationMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardRoles_RoleAppointerId",
                schema: "Dbo",
                table: "BoardRoles",
                column: "RoleAppointerId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_ApprovedUserId",
                schema: "Dbo",
                table: "Boards",
                column: "ApprovedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_AppTeamId",
                schema: "Dbo",
                table: "Boards",
                column: "AppTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_AsstSecretaryId",
                schema: "Dbo",
                table: "Boards",
                column: "AsstSecretaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_BoardStatusId",
                schema: "Dbo",
                table: "Boards",
                column: "BoardStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_EstablishedByUnderId",
                schema: "Dbo",
                table: "Boards",
                column: "EstablishedByUnderId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_OwnerDivisionId",
                schema: "Dbo",
                table: "Boards",
                column: "OwnerDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_OwnerPositionId",
                schema: "Dbo",
                table: "Boards",
                column: "OwnerPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_PortfolioId",
                schema: "Dbo",
                table: "Boards",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_ResponsibleUserId",
                schema: "Dbo",
                table: "Boards",
                column: "ResponsibleUserId");

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
                name: "IX_Roles_Code",
                schema: "acl",
                table: "Roles",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_SkillTypeId",
                schema: "Dbo",
                table: "Skill",
                column: "SkillTypeId");

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
                name: "IX_TeamUsers_AppTeamId",
                schema: "acl",
                table: "TeamUsers",
                column: "AppTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_AppUserId",
                schema: "acl",
                table: "TeamUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_AppRoleId",
                schema: "acl",
                table: "UserRoles",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_AppUserId",
                schema: "acl",
                table: "UserRoles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                schema: "acl",
                table: "Users",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "AppointeeSkill",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "AuditHistory",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "AutoNumbers",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "BoardAppointments",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "DashboardItems",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "DeleteRecords",
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
                name: "UserRoles",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Skill",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "BoardRoles",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Ministers",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Appointee",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Boards",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "OptionSet",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Portfolios",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Secretaries",
                schema: "Dbo");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "OptionKeys",
                schema: "Dbo");
        }
    }
}
