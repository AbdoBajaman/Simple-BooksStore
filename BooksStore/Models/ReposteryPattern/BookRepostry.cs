//using BooksStore.Controllers;
using BooksStore.Models;
using System.Linq;

namespace BooksStore.Models.ReposteryPattern
{
    public class BookRepostry : IBookStoreRepostery<Book>
    {
        List<Book> books;

        public BookRepostry()
        {
            books = new List<Book>()
            {
                   new Book()
                {
                    Id = 1,
                    Title ="Prrogramming",
                    Description = "From 0 to hero"

                },
                          new Book()
                {
                    Id = 2,
                    Title ="C#",
                    Description = "From 0 to hero"

                },
                                        new Book()
                {
                    Id = 3,
                    Title ="C++",
                    Description = "From 0 to hero"

                }
            };


        }
        public void Create(Book book)
        {
            books.Add(book);

        }

        public void Delete(int id)
        {
            var book = Find(id);
            //var book = books.SingleOrDefault(p => p.Id == id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            
            var book = books.SingleOrDefault(p => p.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Book newBook)
        {
            var book = Find(id);
            if (book != null)
            {
                // Update the existing book's properties
                book.Title = newBook.Title;
                book.Description = newBook.Description;
                book.Author = newBook.Author;

                // If using a database context, save changes here, e.g.:
                // _context.SaveChanges();
            }
            else
            {
                // Handle the case where the book was not found (optional)
                throw new Exception("Book not found");
            }
        }
    }
}
