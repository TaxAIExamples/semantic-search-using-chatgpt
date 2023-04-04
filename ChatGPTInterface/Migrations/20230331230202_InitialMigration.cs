using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace ChatGPTInterface.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KnowledgeRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tokens = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeVectorItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VectorValue = table.Column<double>(type: "float", nullable: false),
                    KnowledgeRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeVectorItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowledgeVectorItems_KnowledgeRecords_KnowledgeRecordId",
                        column: x => x.KnowledgeRecordId,
                        principalTable: "KnowledgeRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeVectorItems_KnowledgeRecordId",
                table: "KnowledgeVectorItems",
                column: "KnowledgeRecordId");

            var sqlFile = "sql/CalculateLargestConsineSimilarities.sql";
            string sqlProc = File.ReadAllText(sqlFile, Encoding.UTF8);
            migrationBuilder.Sql(sqlProc);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KnowledgeVectorItems");

            migrationBuilder.DropTable(
                name: "KnowledgeRecords");

            migrationBuilder.Sql("DROP PROCEDURE [dbo].[sp_CalculateLargestCosineSimilarities]");
        }
    }
}
