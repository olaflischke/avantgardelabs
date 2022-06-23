﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TradingDayDal;

#nullable disable

namespace TradingDayDal.Migrations
{
    [DbContext(typeof(TradingDayContext))]
    [Migration("20220623141549_FlagSymbolAdded")]
    partial class FlagSymbolAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TradingDayDal.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Flagsymbol")
                        .HasColumnType("bytea");

                    b.Property<double>("Rate")
                        .HasColumnType("double precision");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TradingDayId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TradingDayId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("TradingDayDal.TradingDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TradingDays");
                });

            modelBuilder.Entity("TradingDayDal.ExchangeRate", b =>
                {
                    b.HasOne("TradingDayDal.TradingDay", "TradingDay")
                        .WithMany("ExchangeRates")
                        .HasForeignKey("TradingDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TradingDay");
                });

            modelBuilder.Entity("TradingDayDal.TradingDay", b =>
                {
                    b.Navigation("ExchangeRates");
                });
#pragma warning restore 612, 618
        }
    }
}