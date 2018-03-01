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
                    alarmId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    alarmColor = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    alarmDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    alarmName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    datetime = table.Column<long>(type: "int8", nullable: false),
                    thingId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmCurrents", x => x.alarmId);
                });

            migrationBuilder.CreateTable(
                name: "HistorianAlarms",
                columns: table => new
                {
                    historianId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    alarmColor = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    alarmDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    alarmId = table.Column<int>(type: "int4", nullable: false),
                    alarmName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    endDate = table.Column<long>(type: "int8", nullable: false),
                    startDate = table.Column<long>(type: "int8", nullable: false),
                    thingId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorianAlarms", x => x.historianId);
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
