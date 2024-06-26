﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using byteStream.JobSeeker.Api.Data;

#nullable disable

namespace byteStream.JobSeeker.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240310121642_datatypesChanged2")]
    partial class datatypesChanged2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("byteStream.JobSeeker.Api.Models.Experience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartYear")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("byteStream.JobSeeker.Api.Models.JobSeekers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExpectedSalary")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImgURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResumeURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalExperience")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("JobSeekerss");
                });

            modelBuilder.Entity("byteStream.JobSeeker.Api.Models.Qualification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GradeOrScore")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QualificationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("YearOfCompletion")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("byteStream.JobSeeker.Api.Models.Experience", b =>
                {
                    b.HasOne("byteStream.JobSeeker.Api.Models.JobSeekers", "JobSeekers")
                        .WithMany("Experience")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeekers");
                });

            modelBuilder.Entity("byteStream.JobSeeker.Api.Models.Qualification", b =>
                {
                    b.HasOne("byteStream.JobSeeker.Api.Models.JobSeekers", "JobSeekers")
                        .WithMany("Qualification")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeekers");
                });

            modelBuilder.Entity("byteStream.JobSeeker.Api.Models.JobSeekers", b =>
                {
                    b.Navigation("Experience");

                    b.Navigation("Qualification");
                });
#pragma warning restore 612, 618
        }
    }
}
