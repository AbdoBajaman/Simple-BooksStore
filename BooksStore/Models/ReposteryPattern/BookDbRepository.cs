using BooksStore.Models;
using BooksStore.Models.ReposteryPattern;
using Bookstore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class BookDbRepository : IBookStoreRepostery<Book>
    {
        BookstoreDbContext db;

        public BookDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        }
        public void Create(Book entity)
        {
            db.Book.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);

            db.Book.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Book.Include(a => a.Author).SingleOrDefault(b => b.Id == id);

            return book;
        }

        public IList<Book> List()
        {
            return db.Book.Include(a=>a.Author).ToList();
        }

        public void Update(int id, Book newBook)
        {
            db.Update(newBook);
            db.SaveChanges();
        }

        public List<Book> Search(string term)
        {
            var result = db.Book.Include(a => a.Author)
                .Where(b => b.Title.Contains(term)
                        || b.Description.Contains(term) 
                        || b.Author.FullName.Contains(term)).ToList();

            return result;
        }
    }
}
