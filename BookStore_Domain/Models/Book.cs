using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore_Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Author is Required")]
        public string Author { get; set; }
        public string CoverImage { get; set; }
        [Required(ErrorMessage = "Price cannot be empty")]
        public decimal Price { get; set; }
    }
}
