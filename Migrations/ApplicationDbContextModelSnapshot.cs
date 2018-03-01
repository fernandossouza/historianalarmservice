﻿// <auto-generated />
using historianalarmservice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace historianalarmservice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("historianalarmservice.Model.Alarm", b =>
                {
                    b.Property<int>("idAlarm")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("alarm");

                    b.Property<long>("datetime");

                    b.Property<int>("thingId");

                    b.HasKey("idAlarm");

                    b.ToTable("AlarmCurrents");
                });

            modelBuilder.Entity("historianalarmservice.Model.HistorianAlarm", b =>
                {
                    b.Property<int>("idHistorian")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("alarm");

                    b.Property<long>("endDate");

                    b.Property<int>("idAlarm");

                    b.Property<long>("startDate");

                    b.Property<int>("thingId");

                    b.HasKey("idHistorian");

                    b.ToTable("HistorianAlarms");
                });
#pragma warning restore 612, 618
        }
    }
}