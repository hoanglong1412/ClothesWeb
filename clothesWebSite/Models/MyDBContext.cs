using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace clothesWebSite.Models
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("name=MyDBContext")
        {
        }

        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banner>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.sender_phone)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.sender_email)
                .IsUnicode(false);

            modelBuilder.Entity<Discount>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Discounts)
                .Map(m => m.ToTable("ProductDiscount").MapLeftKey("discount_id").MapRightKey("product_id"));

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.PaymentDetails)
                .WithRequired(e => e.Payment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentDetail>()
                .Property(e => e.size)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.type_id)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.PaymentDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductDetail>()
                .Property(e => e.size)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.type_id)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.password)
                .IsUnicode(false);

        }
    }
}
