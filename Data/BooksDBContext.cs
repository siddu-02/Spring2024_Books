using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spring2024_Books.Models;

namespace Spring2024_Books.Data
{
    public class BooksDBContext : DbContext //DBCOntext = inherts tfrom the DB context; BooksDB 
    {
        public BooksDBContext(DbContextOptions<BooksDBContext> options):base(options)
        { 

        }

        public DbSet<Category> Categories { get; set; }  // coreseponds to sql table will be created in the database. Each row in this table will be a category. Table will be called Catgeories

        public DbSet<Category> Books { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<Category>().HasData(

                new Category { CategoryId = 1, Name = "Travel", Description = "Going Around the World"},
                new Category { CategoryId = 2, Name = "Fiction", Description = "Good Stories that aren't True" },
                new Category { CategoryId = 3, Name = "Technolgy", Description = "The Application of Scientific Knowledge for Practical Purposes" }
                );
            modelBuilder.Entity<Book>().HasData(

                new Book { BookId = 1, BookTitle = "The Wager", Author = "David Grann", Description = "Shipreck Turns to Murder", Price = 19.99m, CategoryId = 1, },
                new Book { BookId = 2, BookTitle = "Cat in the Hat", Author = "Dr.Susse", Description = "What is in the Cat's Hat", Price = 10.99m, CategoryId = 2,},
                new Book { BookId = 3, BookTitle = "If You Give a Mouse a Cookie", Author = "Laura Numeroff", Description = "About an Inspiring Mouse", Price = 15.99m, CategoryId = 3}




                );

                



        }

    }
}
