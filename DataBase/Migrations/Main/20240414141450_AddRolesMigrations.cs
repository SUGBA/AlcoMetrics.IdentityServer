using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.DataBase.Migrations.Main
{
    public partial class AddRolesMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Добавляем роли, в качестве Id сгенерированный GUID
            migrationBuilder.Sql(@"
                    INSERT INTO ""Roles"" VALUES ('e10464f2-1e86-4807-9a8e-e0384491dfd6','Admin','ADMIN','');
                    INSERT INTO ""Roles"" VALUES ('bec08365-6684-47ed-a10a-80e58b93e50f','WineMaker','WINEMAKER','');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    DELETE FROM ""Roles"" WHERE ""Id"" = 'e10464f2-1e86-4807-9a8e-e0384491dfd6';
                    DELETE FROM ""Roles"" WHERE ""Id"" = 'bec08365-6684-47ed-a10a-80e58b93e50f';");
        }
    }
}
