﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories;

#nullable disable

namespace Repositories.Migrations
{
    [DbContext(typeof(PodBookingSystemDbContext))]
    [Migration("20240917183240_AddBookingStatusENumTbl")]
    partial class AddBookingStatusENumTbl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingStatusId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Models.BookingDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("ArrivalDate")
                        .HasColumnType("date");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("SlotId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("BookingsDetails");
                });

            modelBuilder.Entity("Models.BookingStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookingsStatuses");
                });

            modelBuilder.Entity("Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Models.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("Models.Method", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Methods");
                });

            modelBuilder.Entity("Models.Pod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PodTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("PodTypeId");

                    b.ToTable("Pods");
                });

            modelBuilder.Entity("Models.PodType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PodTypes");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Models.SelectedProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("ProductId");

                    b.ToTable("SelectedProducts");
                });

            modelBuilder.Entity("Models.Slot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookingDetailId")
                        .HasColumnType("int");

                    b.Property<int?>("PodId")
                        .HasColumnType("int");

                    b.Property<int?>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingDetailId")
                        .IsUnique()
                        .HasFilter("[BookingDetailId] IS NOT NULL");

                    b.HasIndex("PodId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("MethodId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("MethodId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MembershipId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MembershipId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.Booking", b =>
                {
                    b.HasOne("Models.BookingStatus", "BookingStatus")
                        .WithMany("Bookings")
                        .HasForeignKey("BookingStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BookingStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.BookingDetail", b =>
                {
                    b.HasOne("Models.Booking", "Booking")
                        .WithMany("BookingDetails")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("Models.Pod", b =>
                {
                    b.HasOne("Models.Area", "Area")
                        .WithMany("Pods")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.PodType", "PodType")
                        .WithMany("Pods")
                        .HasForeignKey("PodTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("PodType");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.HasOne("Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Models.Review", b =>
                {
                    b.HasOne("Models.Booking", "Booking")
                        .WithOne("Review")
                        .HasForeignKey("Models.Review", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("Models.SelectedProduct", b =>
                {
                    b.HasOne("Models.Booking", "Booking")
                        .WithMany("SelectedProducts")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Product", "Product")
                        .WithMany("SelectedProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Models.Slot", b =>
                {
                    b.HasOne("Models.BookingDetail", "BookingDetail")
                        .WithOne("Slot")
                        .HasForeignKey("Models.Slot", "BookingDetailId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Models.Pod", "Pod")
                        .WithMany("Slots")
                        .HasForeignKey("PodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Models.Schedule", "Schedule")
                        .WithMany("Slots")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("BookingDetail");

                    b.Navigation("Pod");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Models.Transaction", b =>
                {
                    b.HasOne("Models.Booking", "Booking")
                        .WithMany("Transactions")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Method", "Method")
                        .WithMany("Transactions")
                        .HasForeignKey("MethodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Method");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.HasOne("Models.Membership", "Membership")
                        .WithMany("Users")
                        .HasForeignKey("MembershipId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Membership");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Models.Area", b =>
                {
                    b.Navigation("Pods");
                });

            modelBuilder.Entity("Models.Booking", b =>
                {
                    b.Navigation("BookingDetails");

                    b.Navigation("Review")
                        .IsRequired();

                    b.Navigation("SelectedProducts");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Models.BookingDetail", b =>
                {
                    b.Navigation("Slot")
                        .IsRequired();
                });

            modelBuilder.Entity("Models.BookingStatus", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Models.Membership", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Models.Method", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Models.Pod", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("Models.PodType", b =>
                {
                    b.Navigation("Pods");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Navigation("SelectedProducts");
                });

            modelBuilder.Entity("Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Models.Schedule", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
