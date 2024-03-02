using Microsoft.EntityFrameworkCore.Migrations;

namespace crud_operation.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblRmpiStuInfo",
                columns: table => new
                {
                    StuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StuRoll = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    StuReg = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    StuDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StuEmail = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    StuPhoto = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRmpiStuInfo", x => x.StuID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblRmpiStuInfo");
        }
    }
}
