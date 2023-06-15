using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BooksApp.MVC.Models;
using BooksApp.Data.Concrete.EfCore;
using BooksApp.Entity.Concrete;
using BooksApp.MVC.Models.ViewModels;

namespace BooksApp.MVC.Controllers;

public class HomeController : Controller
{
    EfCoreBookRepository bookRepository = new EfCoreBookRepository();
    EfCoreCategoryRepository categoryRepository = new EfCoreCategoryRepository();
    public async Task<IActionResult> Index(string categoryurl)
    {
        List<Book> books = await bookRepository.GetAllBooksFullDataAsync(true, categoryurl);

        List<BookModel> bookModelList = new List<BookModel>();
        bookModelList = books.Select(b => new BookModel
        {
            Id=b.Id,
            Name=b.Name,
            Stock=b.Stock,
            Price=b.Price,
            PageCount=b.PageCount,
            EditionYear=b.EditionYear,
            EditionNumber=b.EditionNumber,
            CreatedDate=b.CreatedDate,
            ModifiedDate=b.ModifiedDate,
            IsApproved=b.IsApproved,
            Categories=b.BookCategories.Select(bc=>bc.Category).ToList(),
            Authors = b.BookAuthors.Select(ba=>ba.Author).ToList(),
            Images=b.Images
        }).ToList();
        if (RouteData.Values["categoryurl"] != null)
        {
            ViewBag.SelectedCategoryName = await categoryRepository.GetCategoryNameByUrlAsync(RouteData.Values["categoryurl"].ToString());
        }
        return View(bookModelList);
    }
}
