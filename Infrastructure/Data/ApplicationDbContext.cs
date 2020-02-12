using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Core.Models.EntityModel;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           //relationships here
           
          
           //start one to many relationship between UnitOfMeasureType and UnitOfMeasure
            builder.Entity<UnitOfMeasureType>()
                .HasMany(c => c.UnitOfMeasures)
                .WithOne(e => e.UnitOfMeasureType)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //start many to many relationship between Products and Catalogs
            builder.Entity<CatalogProduct>().HasKey(gc => new { gc.ProductId, gc.CatalogId });

            builder.Entity<CatalogProduct>()
                .HasOne(g => g.Catalog)
                .WithMany(gc => gc.CatalogProducts)
                .HasForeignKey(gid => gid.CatalogId);

            builder.Entity<CatalogProduct>()
                .HasOne(c => c.Product)
                .WithMany(c => c.CatalogProducts)
                .HasForeignKey(cid => cid.ProductId);

        }


        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<CatalogProduct> CatalogProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<Vat> Vats { get; set; }
        public DbSet<UnitOfMeasureType> UnitOfMeasureTypes { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }


    }
}
