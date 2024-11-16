using BooksStore.Models;
using BooksStore.Models.ReposteryPattern;
using Bookstore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class AuthorDbRepository : IBookStoreRepostery<Author>
    {
        BookstoreDbContext db;

        public AuthorDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        }
        public void Create(Author entity)
        {
            db.Author.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);

            db.Author.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = db.Author.SingleOrDefault(a => a.Id == id);

            return author;
        }

        public IList<Author> List()
        {
            return db.Author.ToList();
        }

        public List<Author> Search(string term)
        {
            return db.Author.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author newAuthor)
        {
            db.Update(newAuthor);
            db.SaveChanges();
        }
    }
}