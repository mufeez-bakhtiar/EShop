using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EShop.Models
{
    public partial class EshopContext : DbContext
    {
        public EshopContext()
        {
        }

        public EshopContext(DbContextOptions<EshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<FeaturedProduct> FeaturedProducts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<SystemUser> SystemUsers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-PG0EN0V\\SQLEXPRESS;Database=Eshop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contact");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("email");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name");

                entity.Property(e => e.Subject).HasColumnName("subject");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Cnic)
                    .HasMaxLength(250)
                    .HasColumnName("cnic");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(250)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Purchased).HasColumnName("purchased");
            });

            modelBuilder.Entity<FeaturedProduct>(entity =>
            {
                entity.ToTable("featured_products");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasMaxLength(250)
                    .HasColumnName("price");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(250)
                    .HasColumnName("category_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasMaxLength(250)
                    .HasColumnName("price");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(250)
                    .HasColumnName("quantity");

                entity.Property(e => e.Sku)
                    .HasMaxLength(250)
                    .HasColumnName("sku");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("purchase");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category)
                    .HasMaxLength(250)
                    .HasColumnName("category");

                entity.Property(e => e.DateOfPurchase)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_purchase");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(250)
                    .HasColumnName("vendor_name");
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.ToTable("system_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password)
                    .HasMaxLength(500)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(500)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Cnic)
                    .HasMaxLength(250)
                    .HasColumnName("cnic");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("email");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(250)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Role)
                    .HasMaxLength(250)
                    .HasColumnName("role");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("email");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(250)
                    .HasColumnName("phone_number");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
