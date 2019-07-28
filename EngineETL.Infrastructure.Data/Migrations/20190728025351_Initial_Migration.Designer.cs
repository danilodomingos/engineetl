﻿// <auto-generated />
using System;
using EngineETL.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EngineETL.Infrastructure.Data.Migrations
{
    [DbContext(typeof(EngineETLContext))]
    [Migration("20190728025351_Initial_Migration")]
    partial class Initial_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EngineETL.Core.Domain.Entities.ExpectedFormat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("CityPropertyHabitants")
                        .HasMaxLength(100);

                    b.Property<string>("CityPropertyName")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("NeighborhoodPropertyHabitants")
                        .HasMaxLength(100);

                    b.Property<string>("NeighborhoodPropertyName")
                        .HasMaxLength(100);

                    b.Property<string>("PropertyCity")
                        .HasMaxLength(200);

                    b.Property<string>("PropertyNeighborhood")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("ExpectedFormat");
                });
#pragma warning restore 612, 618
        }
    }
}
