using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace McWebsite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JustRenamingConversationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondParticipant",
                table: "Conversations",
                newName: "SecondParticipantId");

            migrationBuilder.RenameColumn(
                name: "FirstParticipant",
                table: "Conversations",
                newName: "FirstParticipantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondParticipantId",
                table: "Conversations",
                newName: "SecondParticipant");

            migrationBuilder.RenameColumn(
                name: "FirstParticipantId",
                table: "Conversations",
                newName: "FirstParticipant");
        }
    }
}
