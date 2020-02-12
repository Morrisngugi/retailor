using Core.Models.EntityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //relationships here
            //start many to many relationship between Anchors and Suppliers
            //builder.Entity<GroupContact>().HasKey(gc => new { gc.GroupId, gc.ContactId });

            //builder.Entity<GroupContact>()
            //    .HasOne(g => g.Group)
            //    .WithMany(gc => gc.GroupContacts)
            //    .HasForeignKey(gid => gid.GroupId);

            //builder.Entity<GroupContact>()
            //    .HasOne(c => c.Contact)
            //    .WithMany(c => c.GroupContacts)
            //    .HasForeignKey(cid => cid.ContactId);

            builder.Entity<AnchorSupplier>().HasKey(ac => new { ac.AnchorId, ac.SupplierId });

            builder.Entity<AnchorSupplier>()
                .HasOne(a => a.Anchor)
                .WithMany(ac => ac.AnchorSuppliers)
                .HasForeignKey(aid => aid.AnchorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<AnchorSupplier>()
                .HasOne(c => c.Supplier)
                .WithMany(c => c.AnchorSuppliers)
                .HasForeignKey(cid => cid.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //start many to many relationship between Suppliers and Merchants
            builder.Entity<MerchantSupplier>().HasKey(ac => new { ac.MerchantId, ac.SupplierId });

            builder.Entity<MerchantSupplier>()
                .HasOne(a => a.Merchant)
                .WithMany(ac => ac.MerchantSuppliers)
                .HasForeignKey(aid => aid.MerchantId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<MerchantSupplier>()
                .HasOne(c => c.Supplier)
                .WithMany(c => c.MerchantSuppliers)
                .HasForeignKey(cid => cid.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //start many to many relationship between Consumer and Merchants
            builder.Entity<ConsumerMerchant>().HasKey(ac => new { ac.MerchantId, ac.ConsumerId });

            builder.Entity<ConsumerMerchant>()
                .HasOne(a => a.Merchant)
                .WithMany(ac => ac.ConsumerMerchants)
                .HasForeignKey(aid => aid.MerchantId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ConsumerMerchant>()
                .HasOne(c => c.Consumer)
                .WithMany(c => c.ConsumerMerchants)
                .HasForeignKey(cid => cid.ConsumerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //start one to many relationship between Entity and Catalogs
            //builder.Entity<BaseEntity>()
            //    .HasMany(c => c.Catalogs)
            //    .WithOne(e => e.BaseEntity)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //start one to one relationship between Entity and Setting
            builder.Entity<BaseEntity>()
                .HasOne<Setting>(s => s.Setting)
                .WithOne(ad => ad.BaseEntity)
                .HasForeignKey<Setting>(ad => ad.BaseEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //start one to one relationship between Entity and ContactPerson
            builder.Entity<BaseEntity>()
                .HasOne<ContactPerson>(s => s.ContactPerson)
                .WithOne(ad => ad.BaseEntity)
                .HasForeignKey<ContactPerson>(ad => ad.BaseEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //start one to one relationship between EntityType and Entity
            //builder.Entity<EntityType>()
            //    .HasOne<BaseEntity>(s => s.BaseEntity)
            //    .WithOne(ad => ad.EntityType)
            //    .HasForeignKey<BaseEntity>(ad => ad.EntityTypeId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //start one to many relationship between Country and Region
            builder.Entity<Country>()
                .HasMany(c => c.Regions)
                .WithOne(e => e.Country)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //start one to many relationship between Region and Address
            builder.Entity<Region>()
                .HasMany(c => c.Addresses)
                .WithOne(e => e.Region)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //start one to one relationship between Entity and Address
            builder.Entity<BaseEntity>()
                .HasOne<Address>(s => s.Address)
                .WithOne(ad => ad.BaseEntity)
                .HasForeignKey<Address>(ad => ad.BaseEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        //dbsets here
        public DbSet<Address> Addresses { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Anchor> Anchors { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<AnchorSupplier> AnchorSuppliers { get; set; }
        public DbSet<MerchantSupplier> MerchantSuppliers { get; set; }
        public DbSet<ConsumerMerchant> ConsumerMerchants { get; set; }
    }
}
