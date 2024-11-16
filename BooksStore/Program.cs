using BooksStore.Models.ReposteryPattern;
using BooksStore.Models;
using Bookstore.Models.Repositories;
using Microsoft.EntityFrameworkCore;

using Bookstore.Data;

var builder = WebApplication.CreateBuilder(args);

// Register services for DI
//builder.Services.AddSingleton<IBookStoreRepostery<Author>, AuthorRepostry>();
//builder.Services.AddSingleton<IBookStoreRepostery<Book>, BookRepostry>();

builder.Services.AddScoped<IBookStoreRepostery<Author>, AuthorDbRepository>();
builder.Services.AddScoped<IBookStoreRepostery<Book>, BookDbRepository>();

// Add MVC (Controllers with Views) service
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookstoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQlServerCon")));
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
