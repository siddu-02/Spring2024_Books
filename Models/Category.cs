using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spring2024_Books.Models
{
    public class Category
    {


        //get set = allows to get the ID and set will place the variable
        public int CategoryId { get; set; } //Primary key; System knows because it says "Id". ID being a keyword

        [DisplayName("Category Name: ")]
        [Required(ErrorMessage = "The Name for the Category Must be Required"), MinLength(2, ErrorMessage = "Too short buddy")]
        public string Name { get; set; }

        
        [DisplayName("Category Description:")]
        [Required(ErrorMessage = "The Description for the Category Must be Required"),]
        public string Description { get; set; }
       
        
    }
}
