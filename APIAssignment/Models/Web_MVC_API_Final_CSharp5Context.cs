using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIAssignment.Models
{
    public partial class Web_MVC_API_Final_CSharp5Context : DbContext
    {
        public Web_MVC_API_Final_CSharp5Context()
        {
        }

        public Web_MVC_API_Final_CSharp5Context(DbContextOptions<Web_MVC_API_Final_CSharp5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Categroie> Categroies { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Staff> Staffs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-1D6NN35;Initial Catalog=Web_MVC_API_Final_CSharp5;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categroie>(entity =>
            {
                entity.HasKey(e => e.CatCode);

                entity.Property(e => e.CatName).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Image).HasMaxLength(250);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerCode);

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(250);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.PhoneNumber).HasMaxLength(10);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderCode);

                entity.HasIndex(e => e.CustomerCodeId, "IX_Orders_CustomerCodeID");

                entity.Property(e => e.CustomerCodeId).HasColumnName("CustomerCodeID");

                entity.Property(e => e.Note).HasMaxLength(100);

                entity.HasOne(d => d.CustomerCode)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerCodeId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderCodeId, e.ProductCodeId });

                entity.HasIndex(e => e.ProductCodeId, "IX_OrderDetails_ProductCodeID");

                entity.Property(e => e.OrderCodeId).HasColumnName("OrderCodeID");

                entity.Property(e => e.ProductCodeId).HasColumnName("ProductCodeID");

                entity.HasOne(d => d.OrderCode)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderCodeId);

                entity.HasOne(d => d.ProductCode)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductCode);

                entity.HasIndex(e => e.CatCodeId, "IX_Products_CatCodeID");

                entity.Property(e => e.CatCodeId).HasColumnName("CatCodeID");

                entity.Property(e => e.Image).HasMaxLength(250);

                entity.Property(e => e.Note).HasMaxLength(150);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.HasOne(d => d.CatCode)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatCodeId);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleCode);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.StaffCode);

                entity.HasIndex(e => e.RoleCodeId, "IX_Staffs_RoleCodeID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.PhoneNumber).HasMaxLength(10);

                entity.Property(e => e.RoleCodeId).HasColumnName("RoleCodeID");

                entity.Property(e => e.StaffName).HasMaxLength(50);

                entity.HasOne(d => d.RoleCode)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.RoleCodeId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
