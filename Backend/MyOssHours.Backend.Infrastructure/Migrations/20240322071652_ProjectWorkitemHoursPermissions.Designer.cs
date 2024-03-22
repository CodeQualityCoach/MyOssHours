﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyOssHours.Backend.Infrastructure.Persistence;

#nullable disable

namespace MyOssHours.Backend.Infrastructure.Migrations
{
    [DbContext(typeof(MyOssHoursDbContext))]
    [Migration("20240322071652_ProjectWorkitemHoursPermissions")]
    partial class ProjectWorkitemHoursPermissions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyOssHours.Backend.Infrastructure.Persistence.Model.ProjectEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("MyOssHours.Backend.Infrastructure.Persistence.Model.ProjectHourEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("WorkItemId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkItemId");

                    b.ToTable("ProjectHour");
                });

            modelBuilder.Entity("MyOssHours.Backend.Infrastructure.Persistence.Model.ProjectPermissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ProjectEntityId")
                        .HasColumnType("bigint");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectEntityId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectPermission");
                });

            modelBuilder.Entity("MyOssHours.Backend.Infrastructure.Persistence.Model.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Nickname")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("MyOssHours.Backend.Infrastructure.Persistence.Model.WorkItemEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("WorkItem");
                });

            modelBuilder.Entity("MyOssHours.Backend.Infrastructure.Persistence.Model.ProjectHourEntity", b =>
                {
                    b.HasOne("MyOssHours.Backend.Infrastructure.Persistence.Model.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyOssHours.Backend.Infrastructure.Persistence.Model.WorkItemEntity", "WorkItem")
                        .WithMany("Hours")
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("MyOssHours.Backend.Infrastructure.Persistence.Model.ProjectPermissionEntity", b =>
                {
                    b.HasOne("MyOssHours.Backend.Infrastructure.Persistence.Model.ProjectEntity", null)
                        .WithMany("Members")
                        .HasForeignKey("ProjectEntityId");

                    b.HasOne("MyOssHours.Backend.Infrastructure.Persistence.Model.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyOssHours.Backend.Infrastructure.Persistence.Model.WorkItemEntity", b =>
                {
                    b.HasOne("MyOssHours.Backend.Infrastructure.Persistence.Model.ProjectEntity", "Project")
                        .WithMany("WorkItems")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("MyOssHours.Backend.Infrastructure.Persistence.Model.ProjectEntity", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("WorkItems");
                });

            modelBuilder.Entity("MyOssHours.Backend.Infrastructure.Persistence.Model.WorkItemEntity", b =>
                {
                    b.Navigation("Hours");
                });
#pragma warning restore 612, 618
        }
    }
}
