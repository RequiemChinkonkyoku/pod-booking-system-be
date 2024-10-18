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
    [Migration("20241015040638_V2_UpdateTransactionTable")]
    partial class V2_UpdateTransactionTable
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Beautiful location.",
                            Location = "Floor 1",
                            Name = "Area A"
                        });
                });

            modelBuilder.Entity("Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingPrice")
                        .HasColumnType("int");

                    b.Property<int>("BookingStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookingPrice = 10000,
                            BookingStatusId = 5,
                            CreatedTime = new DateTime(2024, 10, 15, 11, 6, 37, 307, DateTimeKind.Local).AddTicks(7553),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            BookingPrice = 10000,
                            BookingStatusId = 5,
                            CreatedTime = new DateTime(2024, 10, 15, 11, 6, 37, 307, DateTimeKind.Local).AddTicks(7573),
                            UserId = 2
                        });
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

                    b.Property<int>("SlotPrice")
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cancelled"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Reserved"
                        },
                        new
                        {
                            Id = 4,
                            Name = "On-going"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Completed"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "N/A",
                            Name = "N/A",
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "No bonuses",
                            Name = "Regular",
                            Status = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "VIPPRO",
                            Name = "VIP",
                            Status = 1
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AreaId = 1,
                            Description = "Clean Pod",
                            Name = "Pod 1",
                            PodTypeId = 1,
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            AreaId = 1,
                            Description = "Nice Pod",
                            Name = "Pod 2",
                            PodTypeId = 2,
                            Status = 1
                        },
                        new
                        {
                            Id = 3,
                            AreaId = 1,
                            Description = "Premium Pod",
                            Name = "Pod 3",
                            PodTypeId = 3,
                            Status = 1
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Pod for one.",
                            Name = "Single Pod",
                            Price = 10000
                        },
                        new
                        {
                            Id = 2,
                            Description = "Pod for two.",
                            Name = "Double Pod",
                            Price = 20000
                        },
                        new
                        {
                            Id = 3,
                            Description = "Luxurious Pod.",
                            Name = "Premium Pod",
                            Price = 50000
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Customer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Staff"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Admin"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndTime = new TimeOnly(8, 0, 0),
                            StartTime = new TimeOnly(7, 0, 0),
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            EndTime = new TimeOnly(9, 0, 0),
                            StartTime = new TimeOnly(8, 0, 0),
                            Status = 1
                        },
                        new
                        {
                            Id = 3,
                            EndTime = new TimeOnly(10, 0, 0),
                            StartTime = new TimeOnly(9, 0, 0),
                            Status = 1
                        },
                        new
                        {
                            Id = 4,
                            EndTime = new TimeOnly(11, 0, 0),
                            StartTime = new TimeOnly(10, 0, 0),
                            Status = 1
                        });
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

                    b.Property<int>("ProductPrice")
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

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

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

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("MembershipId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "customer@gmail.com",
                            MembershipId = 2,
                            Name = "CUSTOMER",
                            Password = "Customer@1234",
                            PasswordHash = "$2a$11$tLa.5HjRT.9Gnzq.zGmuL.cLuVAAxc0Ug4luLcWcbq27.x5tVGRMa",
                            RoleId = 1,
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "staff@gmail.com",
                            MembershipId = 1,
                            Name = "STAFF",
                            Password = "Staff@1234",
                            PasswordHash = "$2a$11$4T9uXOJUVYEcPPB2taeHoeuNTKfIa8HldLiTF7S3LXZFBdo1U2j8a",
                            RoleId = 2,
                            Status = 1
                        },
                        new
                        {
                            Id = 3,
                            Email = "manager@gmail.com",
                            MembershipId = 1,
                            Name = "MANAGER",
                            Password = "Manager@1234",
                            PasswordHash = "$2a$11$3Xl9A/nS.ILuCu1go/vcd.6oufzpfh4B6m7RpoJAhBcba678Pp37y",
                            RoleId = 3,
                            Status = 1
                        },
                        new
                        {
                            Id = 4,
                            Email = "admin@gmail.com",
                            MembershipId = 1,
                            Name = "ADMIN",
                            Password = "Admin@1234",
                            PasswordHash = "$2a$11$pw5j3DIRbVrjE/yyVHZEr.s4ACBwFYo9vjJjaNbtuu6zsHU7y1M0u",
                            RoleId = 4,
                            Status = 1
                        });
                });

            modelBuilder.Entity("Models.UserOtp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsExpiredOrUsed")
                        .HasColumnType("bit");

                    b.Property<string>("OtpCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserOtps");
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

            modelBuilder.Entity("Models.UserOtp", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithMany("UserOtps")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
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

                    b.Navigation("UserOtps");
                });
#pragma warning restore 612, 618
        }
    }
}
