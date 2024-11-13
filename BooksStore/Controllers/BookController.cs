using BooksStore.Models;
using BooksStore.Models.ReposteryPattern;
using BooksStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepostery<Book> _BookRepostery;

        private readonly IBookStoreRepostery<Author> _AuthorRepostry;
        public BookController(IBookStoreRepostery<Book> book,IBookStoreRepostery<Author> author)
        {

            _BookRepostery = book;
            _AuthorRepostry = author;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books= _BookRepostery.List();

            return View(books);
        }

    

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            
            var book = _BookRepostery.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var authors = _AuthorRepostry.List();
            var model = new BookAuthorViewModel
            {
                authors =  _AuthorRepostry.List().ToList(),


        };

            //ViewBag.Author = new SelectList(authors, "Id", "FullName");
            ViewBag.Author = model;


            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            //return Content("Book auhor id :" + book.AuthorId);
            var bookId = _BookRepostery.List().Max(p => p.Id) + 1;

            try
            {
                var author = _AuthorRepostry.Find(model.AuthorId);
                Book book=new Book()
                {
                    Id = bookId,
                    Title = model.Title,
                    Description = model.Description,
                    AuthorId = model.AuthorId,
                    Author = author
                };
                // book.Id = _BookRepostery.List().Max(p=>p.Id) + 1;

                _BookRepostery.Create(book);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _BookRepostery.Find(id);
            var model = new BookAuthorViewModel
            {
                BookId = book.Id,
                AuthorId = book.Author?.Id ?? 0, 
                Title = book.Title,
                Description = book.Description,
                authors = _AuthorRepostry.List().ToList(),


            };
            ViewBag.Authors = new SelectList(model.authors, "Id", "FullName");
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel Author)
        {
            try
            {
                // Get the existing book to update
                var author = _AuthorRepostry.Find(Author.AuthorId);

                // Update the book details
                if (author != null)
                {
                    Book updatedBook = new Book()
                    {
                        Title = Author.Title,
                        Description = Author.Description,
                        AuthorId =Author.AuthorId,
                        Author = author
                    };

                    // Call the Update method
                    _BookRepostery.Update(Author.BookId, updatedBook);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Return the model back to the view in case of an error
                return View(Author);
            }
        }
        // GET: BookController/Delete/5
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            // Find the book by ID
            var book = _BookRepostery.Find(id);

            // If the book is not found, return a NotFound view or an appropriate response
            if (book == null)
            {
                return NotFound(); // Return a 404 error if the book is not found
            }

            try
            {
                // Attempt to delete the book
                _BookRepostery.Delete(book.Id);
                // Optionally, you could display a success message here if needed
            }
            catch (Exception ex) // Catch specific exceptions if needed
            {
                // Log the exception (optional)
                // You can use a logging framework to log the error

                // Return the view with an error message
                TempData["FailDeleted"] = "An error occurred while trying to delete the book.";
                return View(book); // Return the book details back to the view
            }

            TempData["Deleted"] = "Book Deleted Successfully";
            // Redirect to the Index action after successful deletion
            return RedirectToAction("Index");
        }
    }
}
