using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Website_BanHang.Models;

namespace Website_BanHang.Models
{
    public class BanHangContext : DbContext
    {
        private DbSet<Categroies> _categroies;
        private DbSet<Customers> _customers;
        private DbSet<OrderDetails> _orderDetails;
        private DbSet<Orders> _orders;
        private DbSet<Products> _products;
        private DbSet<Roles> _roles;
        private DbSet<Staffs> _staffs;


        public BanHangContext(DbContextOptions<BanHangContext> options)
            : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-1D6NN35;Initial Catalog=Web_MVC_API_Final_CSharp5;Integrated Security=True");
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-V4BEME9\\SQLEXPRESS01;Initial Catalog=QLBH_WebAPI;Persist Security Info=True;User ID=tuanpa03;Password=2002");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Set Primary Key
            modelBuilder.Entity<Categroies>().HasKey(c => c.CatCode);
            modelBuilder.Entity<Customers>().HasKey(c => c.CustomerCode);
            modelBuilder.Entity<OrderDetails>().HasKey(c => new { c.OrderCode, c.ProductCode });
            modelBuilder.Entity<Orders>().HasKey(c => c.OrderCode);
            modelBuilder.Entity<Products>().HasKey(c => c.ProductCode);
            modelBuilder.Entity<Roles>().HasKey(c => c.RoleCode);
            modelBuilder.Entity<Staffs>().HasKey(c => c.StaffCode);

            //Set Property table Categroies
            modelBuilder.Entity<Categroies>().Property(c => c.CatName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Categroies>().Property(c => c.Image)
                .HasMaxLength(250)
                .IsRequired(false);
            modelBuilder.Entity<Categroies>().Property(c => c.Description)
                .HasMaxLength(100)
                .IsRequired(false);

            //Set Property table Customers
            modelBuilder.Entity<Customers>().Property(c => c.CustomerCode)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Customers>().Property(c => c.Email)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Customers>().Property(c => c.Password)
                .HasMaxLength(250)
                .IsRequired();
            modelBuilder.Entity<Customers>().Property(c => c.FullName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Customers>().Property(c => c.Address)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Customers>().Property(c => c.PhoneNumber)
                .HasMaxLength(10)
                .IsRequired();
            modelBuilder.Entity<Customers>().Property(c => c.Image)
                            .HasMaxLength(250)
                            .IsRequired(false);


            //Set Property table OrderDetails
            modelBuilder.Entity<OrderDetails>().Property(c => c.OrderCode)
                .HasColumnName("OrderCodeID")
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<OrderDetails>().Property(c => c.ProductCode)
                .HasColumnName("ProductCodeID")
                .HasMaxLength(20)
                .IsRequired();

            //Set Property table Orders
            modelBuilder.Entity<Orders>().Property(c => c.OrderCode)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Orders>().Property(c => c.CustomerCode)
                .HasColumnName("CustomerCodeID")
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<Orders>().Property(c => c.Note)
                .HasMaxLength(100)
                .IsRequired(false);


            //Set Property table Products
            modelBuilder.Entity<Products>().Property(c => c.ProductCode)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Products>().Property(c => c.CatCode)
                .HasColumnName("CatCodeID")
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<Products>().Property(c => c.ProductName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Products>().Property(c => c.Image)
                .HasMaxLength(250)
                .IsRequired(false);
            modelBuilder.Entity<Products>().Property(c => c.Note)
                .HasMaxLength(150)
                .IsRequired(false);


            //Set Property table Roles
            modelBuilder.Entity<Roles>().Property(c => c.RoleCode)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Roles>().Property(c => c.RoleName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Roles>().Property(c => c.Description)
                .HasMaxLength(50)
                .IsRequired(false);

            //Set Property table Staffs
            modelBuilder.Entity<Staffs>().Property(c => c.StaffCode)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Staffs>().Property(c => c.RoleCode)
                .HasColumnName("RoleCodeID")
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<Staffs>().Property(c => c.Email)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Staffs>().Property(c => c.Password)
                .HasMaxLength(250)
                .IsRequired();
            modelBuilder.Entity<Staffs>().Property(c => c.StaffName)
.HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Staffs>().Property(c => c.Address)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Staffs>().Property(c => c.PhoneNumber)
                .HasMaxLength(10)
                .IsRequired();
            modelBuilder.Entity<Staffs>().Property(c => c.Note)
                .HasMaxLength(100)
                .IsRequired(false);

            //Set Foreign Key
            modelBuilder.Entity<Staffs>()
                .HasOne<Roles>(c => c.Roles)
                .WithMany(c => c.staffs)
                .HasForeignKey(c => c.RoleCode);
            modelBuilder.Entity<Products>()
                .HasOne<Categroies>(c => c.Categroies)
                .WithMany(c => c.products)
                .HasForeignKey(c => c.CatCode);
            modelBuilder.Entity<Orders>()
                .HasOne<Customers>(c => c.Customers)
                .WithMany(c => c.orders)
                .HasForeignKey(c => c.CustomerCode);

            //Set Foreign Key OrderDetails
            modelBuilder.Entity<OrderDetails>()
                .HasOne<Products>(c => c.Products)
                .WithMany(c => c.OrderDetails)
                .HasForeignKey(c => c.ProductCode).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderDetails>()
                .HasOne<Orders>(c => c.Orders)
                .WithMany(c => c.OrderDetails)
                .HasForeignKey(c => c.OrderCode);
        }

        public DbSet<Website_BanHang.Models.Categroies>? Categroies { get; set; }

        public DbSet<Website_BanHang.Models.Customers>? Customers { get; set; }

        public DbSet<Website_BanHang.Models.OrderDetails>? OrderDetails { get; set; }
    }
}