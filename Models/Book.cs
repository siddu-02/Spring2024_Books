using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spring2024_Books.Models
{
    public class Book


    {
        [Key]
        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

       // public  string? ImgURL { get; set; }

        //THis is the Foregin key from Category

        public int CategoryId { get; set; }

        //navagation property
        [ForeignKey("CategoryID")]

        public Category? category  { get; set; }







    }
}
