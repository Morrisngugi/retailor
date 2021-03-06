﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations.EntityDb
{
    [DbContext(typeof(EntityDbContext))]
    partial class EntityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Models.EntityModel.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BaseEntityId");

                    b.Property<string>("City");

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("EmailAddress");

                    b.Property<int>("EntityStatus");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("PhoneNumber");

                    b.Property<Guid>("RegionId");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.HasIndex("BaseEntityId")
                        .IsUnique();

                    b.HasIndex("RegionId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Core.Models.EntityModel.AnchorSupplier", b =>
                {
                    b.Property<Guid>("AnchorId");

                    b.Property<Guid>("SupplierId");

                    b.HasKey("AnchorId", "SupplierId");

                    b.HasIndex("SupplierId");

                    b.ToTable("AnchorSuppliers");
                });

            modelBuilder.Entity("Core.Models.EntityModel.BaseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("DateOfRegistration");

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("EntityStatus");

                    b.Property<Guid?>("EntityTypeId");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("LicenceNumber");

                    b.Property<string>("Name");

                    b.Property<string>("RegistrationNumber");

                    b.Property<int>("Size");

                    b.Property<Guid>("TierId");

                    b.HasKey("Id");

                    b.HasIndex("EntityTypeId");

                    b.ToTable("BaseEntity");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseEntity");
                });

            modelBuilder.Entity("Core.Models.EntityModel.ConsumerMerchant", b =>
                {
                    b.Property<Guid>("MerchantId");

                    b.Property<Guid>("ConsumerId");

                    b.HasKey("MerchantId", "ConsumerId");

                    b.HasIndex("ConsumerId");

                    b.ToTable("ConsumerMerchants");
                });

            modelBuilder.Entity("Core.Models.EntityModel.ContactPerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BaseEntityId");

                    b.Property<string>("Code");

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactPhone");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastUpdated");

                    b.HasKey("Id");

                    b.HasIndex("BaseEntityId")
                        .IsUnique();

                    b.ToTable("ContactPerson");
                });

            modelBuilder.Entity("Core.Models.EntityModel.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("EntityStatus");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Core.Models.EntityModel.EntityType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("EntityStatus");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("EntityTypes");
                });

            modelBuilder.Entity("Core.Models.EntityModel.MerchantSupplier", b =>
                {
                    b.Property<Guid>("MerchantId");

                    b.Property<Guid>("SupplierId");

                    b.HasKey("MerchantId", "SupplierId");

                    b.HasIndex("SupplierId");

                    b.ToTable("MerchantSuppliers");
                });

            modelBuilder.Entity("Core.Models.EntityModel.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<Guid>("CountryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("EntityStatus");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Core.Models.EntityModel.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BaseEntityId");

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("EntityStatus");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.Property<bool>("PurchaseOrderAutoApproval");

                    b.Property<bool>("SaleOrderAutoApproval");

                    b.HasKey("Id");

                    b.HasIndex("BaseEntityId")
                        .IsUnique();

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Core.Models.EntityModel.Anchor", b =>
                {
                    b.HasBaseType("Core.Models.EntityModel.BaseEntity");

                    b.HasDiscriminator().HasValue("Anchor");
                });

            modelBuilder.Entity("Core.Models.EntityModel.Consumer", b =>
                {
                    b.HasBaseType("Core.Models.EntityModel.BaseEntity");

                    b.HasDiscriminator().HasValue("Consumer");
                });

            modelBuilder.Entity("Core.Models.EntityModel.Merchant", b =>
                {
                    b.HasBaseType("Core.Models.EntityModel.BaseEntity");

                    b.HasDiscriminator().HasValue("Merchant");
                });

            modelBuilder.Entity("Core.Models.EntityModel.Supplier", b =>
                {
                    b.HasBaseType("Core.Models.EntityModel.BaseEntity");

                    b.HasDiscriminator().HasValue("Supplier");
                });

            modelBuilder.Entity("Core.Models.EntityModel.Address", b =>
                {
                    b.HasOne("Core.Models.EntityModel.BaseEntity", "BaseEntity")
                        .WithOne("Address")
                        .HasForeignKey("Core.Models.EntityModel.Address", "BaseEntityId");

                    b.HasOne("Core.Models.EntityModel.Region", "Region")
                        .WithMany("Addresses")
                        .HasForeignKey("RegionId");
                });

            modelBuilder.Entity("Core.Models.EntityModel.AnchorSupplier", b =>
                {
                    b.HasOne("Core.Models.EntityModel.Anchor", "Anchor")
                        .WithMany("AnchorSuppliers")
                        .HasForeignKey("AnchorId");

                    b.HasOne("Core.Models.EntityModel.Supplier", "Supplier")
                        .WithMany("AnchorSuppliers")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("Core.Models.EntityModel.BaseEntity", b =>
                {
                    b.HasOne("Core.Models.EntityModel.EntityType", "EntityType")
                        .WithMany()
                        .HasForeignKey("EntityTypeId");
                });

            modelBuilder.Entity("Core.Models.EntityModel.ConsumerMerchant", b =>
                {
                    b.HasOne("Core.Models.EntityModel.Consumer", "Consumer")
                        .WithMany("ConsumerMerchants")
                        .HasForeignKey("ConsumerId");

                    b.HasOne("Core.Models.EntityModel.Merchant", "Merchant")
                        .WithMany("ConsumerMerchants")
                        .HasForeignKey("MerchantId");
                });

            modelBuilder.Entity("Core.Models.EntityModel.ContactPerson", b =>
                {
                    b.HasOne("Core.Models.EntityModel.BaseEntity", "BaseEntity")
                        .WithOne("ContactPerson")
                        .HasForeignKey("Core.Models.EntityModel.ContactPerson", "BaseEntityId");
                });

            modelBuilder.Entity("Core.Models.EntityModel.MerchantSupplier", b =>
                {
                    b.HasOne("Core.Models.EntityModel.Merchant", "Merchant")
                        .WithMany("MerchantSuppliers")
                        .HasForeignKey("MerchantId");

                    b.HasOne("Core.Models.EntityModel.Supplier", "Supplier")
                        .WithMany("MerchantSuppliers")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("Core.Models.EntityModel.Region", b =>
                {
                    b.HasOne("Core.Models.EntityModel.Country", "Country")
                        .WithMany("Regions")
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("Core.Models.EntityModel.Setting", b =>
                {
                    b.HasOne("Core.Models.EntityModel.BaseEntity", "BaseEntity")
                        .WithOne("Setting")
                        .HasForeignKey("Core.Models.EntityModel.Setting", "BaseEntityId");
                });
#pragma warning restore 612, 618
        }
    }
}
