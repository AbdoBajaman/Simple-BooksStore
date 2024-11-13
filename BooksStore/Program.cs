using BooksStore.Models.ReposteryPattern;
using BooksStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Register services for DI
builder.Services.AddSingleton<IBookStoreRepostery<Author>, AuthorRepostry>();
builder.Services.AddSingleton<IBookStoreRepostery<Book>, BookRepostry>();

// Add MVC (Controllers with Views) service
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure middleware for non-development environments
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware for handling HTTPS redirection, static files, and routing
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Define routing for AuthorController and BookController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Author}/{action=Index}/{id?}");



app.Run();
