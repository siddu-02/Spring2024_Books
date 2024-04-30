using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spring2024_Books.Models;

namespace Spring2024_Books.Data
{
    public class BooksDBContext : IdentityDbContext //DBCOntext = inherts tfrom the DB context; BooksDB 
    {
        public BooksDBContext(DbContextOptions<BooksDBContext> options):base(options)
        { 

        }

        public DbSet<Category> Categories { get; set; }  // coreseponds to sql table will be created in the database. Each row in this table will be a category. Table will be called Catgeories

        public DbSet<Book> Books { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(

                new Category { CategoryId = 1, Name = "Travel", Description = "Going Around the World"},
                new Category { CategoryId = 2, Name = "Fiction", Description = "Good Stories that aren't True" },
                new Category { CategoryId = 3, Name = "Technolgy", Description = "The Application of Scientific Knowledge for Practical Purposes" }
                );
            modelBuilder.Entity<Book>().HasData(

                new Book { BookId = 60, BookTitle = "The Wager", Author = "David Grann", Description = "Shipreck Turns to Murder", ISBN = "123456789", Price = 19.99m, CategoryId = 1,  ImgUrl = "1" },
                new Book { BookId = 70, BookTitle = "Cat in the Hat", Author = "Dr.Susse", Description = "What is in the Cat's Hat", ISBN = "123456789", Price = 10.99m, CategoryId = 2, ImgUrl = "1" },
                new Book { BookId = 80, BookTitle = "If You Give a Mouse a Cookie", Author = "Laura Numeroff", Description = "About an Inspiring Mouse", ISBN = "123456789", Price = 15.99m, CategoryId = 3, ImgUrl = "1" }




                );

                



        }

    }
}
