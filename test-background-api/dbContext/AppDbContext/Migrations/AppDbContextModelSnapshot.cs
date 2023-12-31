﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using test_background_api.dbContext.AppDbContext;

#nullable disable

namespace test_background_api.dbContext.AppDbContext
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("test_background_api.dbContext.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("LeadId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lead_id");

                    b.Property<string>("LeadSource")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lead_source");

                    b.Property<string>("Phone1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone1");

                    b.Property<string>("Questions")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("questions");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("service");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("street_address");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("zip");

                    b.HasKey("Id");

                    b.ToTable("message", "public");
                });
#pragma warning restore 612, 618
        }
    }
}
