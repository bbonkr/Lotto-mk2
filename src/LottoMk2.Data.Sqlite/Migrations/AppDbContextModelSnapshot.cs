﻿// <auto-generated />
using System;
using LottoMk2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LottoMk2.Data.Sqlite.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("LottoMk2.Entities.Lotto", b =>
                {
                    b.Property<int>("Round")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LotteryDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Num1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Num2")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Num3")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Num4")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Num5")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Num6")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumBonus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Round");

                    b.ToTable("Lotto", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
