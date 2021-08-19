using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Api.Migrations
{
    public partial class addAuditLogInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrowserInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutionDuration = table.Column<int>(type: "int", nullable: false),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditInfo");
        }
    }
}
