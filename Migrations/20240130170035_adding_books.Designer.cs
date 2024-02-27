﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spring2024_Books.Data;

#nullable disable

namespace Spring2024_Books.Migrations
{
    [DbContext(typeof(BooksDBContext))]
    [Migration("20240130170035_adding_books")]
    partial class adding_books
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Spring2024_Books.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BookId");

                    b.HasIndex("CategoryID");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Author = "David Grann",
                            BookTitle = "The Wager",
                            CategoryID = 1,
                            Description = "Shipreck Turns to Murder",
                            Price = 19.99m
                        },
                        new
                        {
                            BookId = 2,
                            Author = "Dr.Susse",
                            BookTitle = "Cat in the Hat",
                            CategoryID = 2,
                            Description = "What is in the Cat's Hat",
                            Price = 10.99m
                        },
                        new
                        {
                            BookId = 3,
                            Author = "Laura Numeroff",
                            BookTitle = "If You Give a Mouse a Cookie",
                            CategoryID = 3,
                            Description = "About an Inspiring Mouse",
                            Price = 15.99m
                        });
                });

            modelBuilder.Entity("Spring2024_Books.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Description = "Going Around the World",
                            Name = "Travel"
                        },
                        new
                        {
                            CategoryId = 2,
                            Description = "Good Stories that aren't True",
                            Name = "Fiction"
                        },
                        new
                        {
                            CategoryId = 3,
                            Description = "The Application of Scientific Knowledge for Practical Purposes",
                            Name = "Technolgy"
                        });
                });

            modelBuilder.Entity("Spring2024_Books.Models.Book", b =>
                {
                    b.HasOne("Spring2024_Books.Models.Category", "category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });
#pragma warning restore 612, 618
        }
    }
}
