using BooksStore.Models;
namespace BooksStore.Models.ReposteryPattern
{
    public class AuthorRepostry : IBookStoreRepostery<Author>
    {
        IList<Author> _authors;

       
        public AuthorRepostry()
        {
            _authors = new List<Author>()
            {
                new Author() { 
                    Id = 1,
                    FullName = "Abdulrahman Abdullah",
                },
                   new Author() {
                    Id = 2,
                    FullName = "Abdullah doctor Abdullah",
                },
                      new Author() {
                    Id = 3,
                    FullName = "Ahmed Abdullah",
                },
                   new Author() {
                    Id = 4,
                    FullName = "Mona Abdullah",
                },
                      new Author() {
                    Id = 5,
                    FullName = "Ameera Abdullah",
                },
                         new Author() {
                    Id = 6,
                    FullName = "Bashayer Abdullah",
                }

            };
        }

        public void Create(Author author)
        {
             _authors.Add(author);
          
           

        }

        public void Delete(int id)
        {
           var author = Find(id);
            if (author == null)
            {
                return;
            }
            _authors.Remove(author);
        }

        public Author Find(int id)
        {
           var author = _authors.SingleOrDefault(p=>p.Id == id);
           
            return author;
        }

        public IList<Author> List()
        {
            return _authors;
        }

        public void Update(int Id, Author NewAuthor)
        {
           var Author=Find(Id);

            if (Author == null)
            {
                return ;
                    
            }
   
            Author.FullName = NewAuthor.FullName;


        }
    }
}
