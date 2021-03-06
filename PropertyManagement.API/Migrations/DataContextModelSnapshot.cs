﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PropertyManagement.API.Data;

namespace PropertyManagement.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("PropertyManagement.API.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateRead")
                        .HasColumnType("TEXT");

                    b.Property<bool>("FlatDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRead")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("MessageSent")
                        .HasColumnType("TEXT");

                    b.Property<int>("PropertyId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SenderDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SenderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("PropertyManagement.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("PropertyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PublicId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isMain")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("PropertyManagement.API.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<double>("FlatArea")
                        .HasColumnType("REAL");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PropertyNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomNumbers")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Street")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("PropertyManagement.API.Models.Rent", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("PropertyManagement.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PropertyManagement.API.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("PropertyManagement.API.Models.Message", b =>
                {
                    b.HasOne("PropertyManagement.API.Models.Property", "Property")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PropertyManagement.API.Models.User", "Sender")
                        .WithMany("MessageSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PropertyManagement.API.Models.Photo", b =>
                {
                    b.HasOne("PropertyManagement.API.Models.Property", null)
                        .WithMany("Photos")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PropertyManagement.API.Models.Property", b =>
                {
                    b.HasOne("PropertyManagement.API.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PropertyManagement.API.Models.Rent", b =>
                {
                    b.HasOne("PropertyManagement.API.Models.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyManagement.API.Models.User", "User")
                        .WithMany("Rents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
