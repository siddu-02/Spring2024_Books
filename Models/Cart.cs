using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spring2024_Books.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        [ValidateNever]
        public Book Book { get; set; } //navigational property
        public string UserId { get; set; } 

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; } //navigational property
        public int Quantity { get; set; }

        [NotMapped]
        public decimal Subtotal { get; set; }
    }
}
