using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class AddTagsNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Products_ProductID",
                table: "Tag");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Tag",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_ProductID",
                table: "Tag",
                newName: "IX_Tag_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Products_ProductId",
                table: "Tag",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Products_ProductId",
                table: "Tag");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Tag",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_ProductId",
                table: "Tag",
                newName: "IX_Tag_ProductID");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "Tag",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Products_ProductID",
                table: "Tag",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID");
        }
    }
}
