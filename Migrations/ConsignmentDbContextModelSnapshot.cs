﻿// <auto-generated />
using System;
using CsdsShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CsdsShop.Migrations
{
    [DbContext(typeof(ConsignmentDbContext))]
    partial class ConsignmentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.16");

            modelBuilder.Entity("CsdsShop.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("CreditAmount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("FeePercentage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSold")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ListDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("SaleDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("SellerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Size")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("CsdsShop.Models.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountToCredit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CellNum")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("CsdsShop.Models.Item", b =>
                {
                    b.HasOne("CsdsShop.Models.Seller", null)
                        .WithMany("ItemsList")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CsdsShop.Models.Seller", b =>
                {
                    b.Navigation("ItemsList");
                });
#pragma warning restore 612, 618
        }
    }
}
