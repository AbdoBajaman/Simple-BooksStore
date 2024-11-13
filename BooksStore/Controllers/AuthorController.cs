using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BooksStore.Models;
using BooksStore.Models.ReposteryPattern;

namespace BooksStore.Controllers
{
    public class AuthorController : Controller
    {
        //cus in program we use singelton this interface implement the class Author Repo
        private readonly IBookStoreRepostery<Author> authorRepostry;

        public AuthorController(IBookStoreRepostery<Author> AuthorRepostry)
        {
            this.authorRepostry = AuthorRepostry;
        }

        // GET: AuthorController
        public IActionResult Index()
        {
            var authors = authorRepostry.List();  // Fetch all authors
            return View(authors);  // Return authors to view
        }


        // GET: AuthorController/Create
        public ActionResult Create()
        {
            
            return View();  // Return the view for Create
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            try
            {
                // Manually assign the Id if it's not auto-generated
                author.Id = authorRepostry.List().Max(a => a.Id) + 1;

                // Add the new author to the repository
                authorRepostry.Create(author);

                TempData["Created"] = "Created Author successfully";
                return RedirectToAction(nameof(Index));  // Redirect to Index after success
            }
            catch
            {
                return View();  // Return the view again if something goes wrong
            }
        }


        //GET:AuthController/Details/5


        public IActionResult Details(int id)
        {
            var author = authorRepostry.Find(id);
            if (author == null)
            {
                return NotFound(); // Return 404 if the author is not found
            }
            return View(author); // Pass the author to the view
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author=authorRepostry.Find(id);
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            authorRepostry.Update(id,author);
            TempData["Updated"] = "Author updated successfully";
            return RedirectToAction(nameof(Index));
        }

        // GET: AuthorController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: AuthorController/Delete/5
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
          
            authorRepostry.Delete(id);

            TempData["Deleted"] = "Author deleted successfully";
            return RedirectToAction("Index");


          
        }

    }
}
