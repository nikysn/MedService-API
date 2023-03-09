﻿// <auto-generated />
using System;
using MedServiceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedServiceAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MedServiceAPI.Model.AppointmentDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("AppointmentDates");
                });

            modelBuilder.Entity("MedServiceAPI.Model.AppointmentTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentDateId")
                        .HasColumnType("int");

                    b.Property<bool>("Occupied")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentDateId");

                    b.ToTable("AppointmentTimes");
                });

            modelBuilder.Entity("MedServiceAPI.Model.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speciality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("MedServiceAPI.Model.AppointmentDate", b =>
                {
                    b.HasOne("MedServiceAPI.Model.Doctor", "Doctor")
                        .WithMany("AppointmentDate")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("MedServiceAPI.Model.AppointmentTime", b =>
                {
                    b.HasOne("MedServiceAPI.Model.AppointmentDate", "appointmentDate")
                        .WithMany("AppointmentTimes")
                        .HasForeignKey("AppointmentDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appointmentDate");
                });

            modelBuilder.Entity("MedServiceAPI.Model.AppointmentDate", b =>
                {
                    b.Navigation("AppointmentTimes");
                });

            modelBuilder.Entity("MedServiceAPI.Model.Doctor", b =>
                {
                    b.Navigation("AppointmentDate");
                });
#pragma warning restore 612, 618
        }
    }
}
