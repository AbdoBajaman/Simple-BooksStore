using BooksStore.Models;
using System.ComponentModel.DataAnnotations; // For validation attributes

namespace BooksStore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public List<Author>? Authors { get; set; }

        public IFormFile File { get; set; }

        public string? ImageUrl { get; set; }
    }
}
