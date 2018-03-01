using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace historianalarmservice.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlarmCurrents",
                columns: table => new
                {
                    idAlarm = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    alarm = table.Column<string>(type: "text", nullable: true),
                    datetime = table.Column<long>(type: "int8", nullable: false),
                    thingId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmCurrents", x => x.idAlarm);
                });

            migrationBuilder.CreateTable(
                name: "HistorianAlarms",
                columns: table => new
                {
                    idHistorian = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    alarm = table.Column<string>(type: "text", nullable: true),
                    endDate = table.Column<long>(type: "int8", nullable: false),
                    idAlarm = table.Column<int>(type: "int4", nullable: false),
                    startDate = table.Column<long>(type: "int8", nullable: false),
                    thingId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorianAlarms", x => x.idHistorian);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmCurrents");

            migrationBuilder.DropTable(
                name: "HistorianAlarms");
        }
    }
}
