using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace McWebsite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GameServerSubscriptionRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyingPlayerId",
                table: "GameServerSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionEndDate",
                table: "GameServerSubscriptions");

            migrationBuilder.RenameColumn(
                name: "SubscriptionStartDate",
                table: "GameServerSubscriptions",
                newName: "CreatedDateTime");

            migrationBuilder.AddColumn<int>(
                name: "InGameSubscriptionId",
                table: "GameServerSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "GameServerSubscriptions",
                type: "real",
                nullable: false,
                defaultValue: 50f);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SubscriptionDuration",
                table: "GameServerSubscriptions",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(1, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionType",
                table: "GameServerSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InGameSubscriptionId",
                table: "GameServerSubscriptions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "GameServerSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionDuration",
                table: "GameServerSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                table: "GameServerSubscriptions");

            migrationBuilder.RenameColumn(
                name: "CreatedDateTime",
                table: "GameServerSubscriptions",
                newName: "SubscriptionStartDate");

            migrationBuilder.AddColumn<Guid>(
                name: "BuyingPlayerId",
                table: "GameServerSubscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEndDate",
                table: "GameServerSubscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
