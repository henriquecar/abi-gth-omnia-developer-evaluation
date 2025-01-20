using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class DefaultDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "MaxQuantity", "MinQuantity", "Percentage" },
                values: new object[,]
                {
                    { new Guid("9e1e0ca5-0ca7-4320-8a2f-b2a6c337f2b7"), (short)9, (short)4, 0.10000000000000001 },
                    { new Guid("dab6a7b6-0ec8-4590-9be2-148399fff6e3"), (short)20, (short)10, 0.20000000000000001 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("9e1e0ca5-0ca7-4320-8a2f-b2a6c337f2b7"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("dab6a7b6-0ec8-4590-9be2-148399fff6e3"));
        }
    }
}
