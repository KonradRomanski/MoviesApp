﻿// <auto-generated />
using System;
using CRUDAPP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRUDAPP.Migrations
{
    [DbContext(typeof(CRUDAPPDbContext))]
    [Migration("20221218213800_Change Type to the enum")]
    partial class ChangeTypetotheenum
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("CRUDAPP.Models.Domain.Director", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Rating")
                        .HasColumnType("REAL");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("CRUDAPP.Models.Domain.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DirectorId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Production")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Types")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("CRUDAPP.Models.Domain.Movie", b =>
                {
                    b.HasOne("CRUDAPP.Models.Domain.Director", "Director")
                        .WithMany("Movies")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("CRUDAPP.Models.Domain.Director", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
