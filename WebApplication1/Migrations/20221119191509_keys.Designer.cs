﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20221119191509_keys")]
    partial class keys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApplication1.Models.AppTeam", b =>
                {
                    b.Property<int>("team_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("team_Id"), 1L, 1);

                    b.Property<string>("team_abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("team_city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("team_conference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("team_division")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("team_full_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("team_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("team_Id");

                    b.ToTable("AppTeam");
                });

            modelBuilder.Entity("WebApplication1.Models.Player", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<int>("AppTeam")
                        .HasColumnType("int");

                    b.Property<string>("first_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("height_feet")
                        .HasColumnType("int");

                    b.Property<int>("height_inches")
                        .HasColumnType("int");

                    b.Property<string>("last_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("weight_pounds")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("AppTeam");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("WebApplication1.Models.Player", b =>
                {
                    b.HasOne("WebApplication1.Models.AppTeam", "team")
                        .WithMany("Players")
                        .HasForeignKey("AppTeam")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("team");
                });

            modelBuilder.Entity("WebApplication1.Models.AppTeam", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
