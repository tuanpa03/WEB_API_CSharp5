﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Website_BanHang.Models;

#nullable disable

namespace Web_BanHang.Migrations
{
    [DbContext(typeof(BanHangContext))]
    partial class BanHangContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Website_BanHang.Models.Categroies", b =>
                {
                    b.Property<int>("CatCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatCode"), 1L, 1);

                    b.Property<string>("CatName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Image")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("CatCode");

                    b.ToTable("Categroies");
                });

            modelBuilder.Entity("Website_BanHang.Models.Customers", b =>
                {
                    b.Property<int>("CustomerCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerCode"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("CustomerCode");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Website_BanHang.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderCode")
                        .HasMaxLength(20)
                        .HasColumnType("int")
                        .HasColumnName("OrderCodeID");

                    b.Property<int>("ProductCode")
                        .HasMaxLength(20)
                        .HasColumnType("int")
                        .HasColumnName("ProductCodeID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderCode", "ProductCode");

                    b.HasIndex("ProductCode");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Website_BanHang.Models.Orders", b =>
                {
                    b.Property<int>("OrderCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderCode"), 1L, 1);

                    b.Property<int>("CustomerCode")
                        .HasMaxLength(20)
                        .HasColumnType("int")
                        .HasColumnName("CustomerCodeID");

                    b.Property<string>("Note")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<float>("TotalMoney")
                        .HasColumnType("real");

                    b.HasKey("OrderCode");

                    b.HasIndex("CustomerCode");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Website_BanHang.Models.Products", b =>
                {
                    b.Property<int>("ProductCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductCode"), 1L, 1);

                    b.Property<int>("CatCode")
                        .HasMaxLength(20)
                        .HasColumnType("int")
                        .HasColumnName("CatCodeID");

                    b.Property<string>("Image")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Note")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductCode");

                    b.HasIndex("CatCode");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Website_BanHang.Models.Roles", b =>
                {
                    b.Property<int>("RoleCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleCode"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleCode");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Website_BanHang.Models.Staffs", b =>
                {
                    b.Property<int>("StaffCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffCode"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("RoleCode")
                        .HasMaxLength(20)
                        .HasColumnType("int")
                        .HasColumnName("RoleCodeID");

                    b.Property<string>("StaffName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("StaffCode");

                    b.HasIndex("RoleCode");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("Website_BanHang.Models.OrderDetails", b =>
                {
                    b.HasOne("Website_BanHang.Models.Orders", "Orders")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_BanHang.Models.Products", "Products")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductCode")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Website_BanHang.Models.Orders", b =>
                {
                    b.HasOne("Website_BanHang.Models.Customers", "Customers")
                        .WithMany("orders")
                        .HasForeignKey("CustomerCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customers");
                });

            modelBuilder.Entity("Website_BanHang.Models.Products", b =>
                {
                    b.HasOne("Website_BanHang.Models.Categroies", "Categroies")
                        .WithMany("products")
                        .HasForeignKey("CatCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categroies");
                });

            modelBuilder.Entity("Website_BanHang.Models.Staffs", b =>
                {
                    b.HasOne("Website_BanHang.Models.Roles", "Roles")
                        .WithMany("staffs")
                        .HasForeignKey("RoleCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Website_BanHang.Models.Categroies", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("Website_BanHang.Models.Customers", b =>
                {
                    b.Navigation("orders");
                });

            modelBuilder.Entity("Website_BanHang.Models.Orders", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Website_BanHang.Models.Products", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Website_BanHang.Models.Roles", b =>
                {
                    b.Navigation("staffs");
                });
#pragma warning restore 612, 618
        }
    }
}
