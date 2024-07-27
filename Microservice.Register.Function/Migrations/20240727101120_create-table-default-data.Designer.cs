﻿// <auto-generated />
using System;
using Microservice.Register.Function.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Microservice.Register.Function.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20240727101120_create-table-default-data")]
    partial class createtabledefaultdata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microservice.Register.Function.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("VerificationToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MSOS_User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6c84d0a3-0c0c-435f-9ae0-4de09247ee15"),
                            Created = new DateTime(2024, 3, 27, 15, 31, 21, 70, DateTimeKind.Unspecified).AddTicks(3642),
                            Email = "intergration-test-user@example.com",
                            LastUpdated = new DateTime(2024, 7, 27, 11, 11, 18, 595, DateTimeKind.Local).AddTicks(5962),
                            PasswordHash = "$2a$11$K7TSYHDJaepUjxZPiE4dY.tuzpiL2JoEItsb3CVqwNkNELXIX2Ywy",
                            Role = 3,
                            VerificationToken = "-HpwWGVP5WXtxdvIH7VMNlJUyTS9_z9O7ef1BgPjhLcsjrOWxyyQNw44",
                            Verified = new DateTime(2023, 8, 18, 15, 21, 38, 875, DateTimeKind.Unspecified).AddTicks(8226)
                        },
                        new
                        {
                            Id = new Guid("929eaf82-e4fd-4efe-9cae-ce4d7e32d159"),
                            Created = new DateTime(2024, 3, 27, 15, 31, 45, 951, DateTimeKind.Unspecified).AddTicks(2476),
                            Email = "intergration-test-user2@example.com",
                            LastUpdated = new DateTime(2024, 7, 27, 11, 11, 18, 595, DateTimeKind.Local).AddTicks(6012),
                            PasswordHash = "$2a$11$1hPEhBElDwFfKDstC5j7EeGebkAKHyKEdVguvu2GOREdm8qNpbNOi",
                            Role = 3,
                            VerificationToken = "-AbTPQWp3vTaExY6q3SF9nAqsVAulzTmTkuj-gfmv_5-XDabkYa1EQ44",
                            Verified = new DateTime(2023, 8, 18, 15, 26, 46, 293, DateTimeKind.Unspecified).AddTicks(65)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
