using Microsoft.EntityFrameworkCore.Migrations;
using test_background_api.dbContext.Helper;
namespace test_background_api.dbContext.Quartz.Migrations
{
    public partial class QuartzInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = MigrationUtility.ReadSql(
                typeof(QuartzInit), 
                "QuartzInit_UP.sql");
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
