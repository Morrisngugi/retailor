﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191021213144_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("FirstName");

                    b.Property<string>("IdNumber");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("MiddleName");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Core.Models.Factory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.Property<Guid>("RegionId");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Factories");
                });

            modelBuilder.Entity("Core.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<Guid>("FactoryId");

                    b.Property<int>("GroupType");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("FactoryId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Core.Models.GroupContact", b =>
                {
                    b.Property<Guid>("GroupId");

                    b.Property<Guid>("ContactId");

                    b.HasKey("GroupId", "ContactId");

                    b.HasIndex("ContactId");

                    b.ToTable("GroupContacts");
                });

            modelBuilder.Entity("Core.Models.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Core.Models.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("ConnectionString");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<Guid>("FactoryId");

                    b.Property<string>("KeyWord");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("SenderId");

                    b.Property<string>("ShortCode");

                    b.Property<bool>("SmsRequireApproval");

                    b.Property<string>("SmsUrl");

                    b.Property<string>("SubscriptionKeyWord");

                    b.Property<string>("Title");

                    b.Property<string>("UnSubscriptionKeyWord");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("FactoryId")
                        .IsUnique();

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Core.Models.Sms", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BillingType");

                    b.Property<string>("Code");

                    b.Property<Guid>("ContactId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Message");

                    b.Property<string>("MessageId");

                    b.Property<int>("MessageType");

                    b.Property<int>("SmsCategory");

                    b.Property<int>("SmsStatus");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Sms");
                });

            modelBuilder.Entity("Core.Models.Factory", b =>
                {
                    b.HasOne("Core.Models.Region", "Region")
                        .WithMany("Factories")
                        .HasForeignKey("RegionId");
                });

            modelBuilder.Entity("Core.Models.Group", b =>
                {
                    b.HasOne("Core.Models.Factory", "Factory")
                        .WithMany("Groups")
                        .HasForeignKey("FactoryId");
                });

            modelBuilder.Entity("Core.Models.GroupContact", b =>
                {
                    b.HasOne("Core.Models.Contact", "Contact")
                        .WithMany("GroupContacts")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Models.Group", "Group")
                        .WithMany("GroupContacts")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Models.Setting", b =>
                {
                    b.HasOne("Core.Models.Factory", "Factory")
                        .WithOne("FactorySetting")
                        .HasForeignKey("Core.Models.Setting", "FactoryId");
                });

            modelBuilder.Entity("Core.Models.Sms", b =>
                {
                    b.HasOne("Core.Models.Contact", "Contact")
                        .WithMany("Sms")
                        .HasForeignKey("ContactId");
                });
#pragma warning restore 612, 618
        }
    }
}