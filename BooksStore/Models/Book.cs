using System.ComponentModel.DataAnnotations.Schema;

namespace BooksStore.Models
{
    public class Book
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }



        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
