﻿// <auto-generated />
using System;
using EscapeRoomPlanner.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EscapeRoomPlanner.Data.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190316104740_CustomerReservationTypo")]
    partial class CustomerReservationTypo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EscapeRoomPlanner.Data.EntityFramework.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("SecondName");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("EscapeRoomPlanner.Data.EntityFramework.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("DateReservation");

                    b.Property<string>("Description");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("RoomId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("EscapeRoomPlanner.Data.EntityFramework.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClosingTime");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("OpeningTime");

                    b.HasKey("Id");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("EscapeRoomPlanner.Data.EntityFramework.Models.Reservation", b =>
                {
                    b.HasOne("EscapeRoomPlanner.Data.EntityFramework.Models.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EscapeRoomPlanner.Data.EntityFramework.Models.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
