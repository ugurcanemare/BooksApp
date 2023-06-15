using BooksApp.Core;
using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore;
using BooksApp.Entity.Concrete;
using BooksApp.MVC.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        EfCoreBookRepository bookRepository = new EfCoreBookRepository();
        EfCoreCategoryRepository categoryRepository = new EfCoreCategoryRepository();
        EfCoreImageRepository imageRepository = new EfCoreImageRepository();
        EfCoreAuthorRepository authorRepository = new EfCoreAuthorRepository();

        #region Listeleme
        public async Task<IActionResult> Index(BookListViewModel bookListViewModel)
        {
            List<Book> bookList;
            if (bookListViewModel.Books == null)
            {
                bookList = await bookRepository.GetAllBooksFullDataAsync(bookListViewModel.ApprovedStatus);
                List<BookViewModel> books = new List<BookViewModel>();
                foreach (var book in bookList)
                {
                    books.Add(new BookViewModel
                    {
                        Id = book.Id,
                        Name = book.Name,
                        Stock = book.Stock,
                        Price = book.Price,
                        PageCount = book.PageCount,
                        EditionYear = book.EditionYear,
                        EditionNumber = book.EditionNumber,
                        CreatedDate = book.CreatedDate,
                        ModifiedDate = book.ModifiedDate,
                        IsApproved = book.IsApproved,
                        Categories = book.BookCategories.Select(bc => new CategoryViewModel
                        {
                            Id = bc.Category.Id,
                            Name = bc.Category.Name,
                            Url = bc.Category.Url
                        }).ToList(),
                        Authors = book.BookAuthors.Select(ba => new AuthorViewModel
                        {
                            Id = ba.Author.Id,
                            Name = ba.Author.Name,
                            Url = ba.Author.Url
                        }).ToList(),
                        Images = book.Images
                    });
                }
                bookListViewModel.Books = books;
            }
            return View(bookListViewModel);
        }
        public async Task<IActionResult> GetBooksByAuthor(int id)
        {
            List<Book> bookList = await bookRepository.GetBooksByAuthor(id);
            List<BookViewModel> books = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                books.Add(new BookViewModel
                {
                    Id = book.Id,
                    Name = book.Name,
                    Stock = book.Stock,
                    Price = book.Price,
                    PageCount = book.PageCount,
                    EditionYear = book.EditionYear,
                    EditionNumber = book.EditionNumber,
                    CreatedDate = book.CreatedDate,
                    ModifiedDate = book.ModifiedDate,
                    IsApproved = book.IsApproved,
                    Categories = book.BookCategories.Select(bc => new CategoryViewModel
                    {
                        Id = bc.Category.Id,
                        Name = bc.Category.Name,
                        Url = bc.Category.Url
                    }).ToList(),
                    Authors = book.BookAuthors.Select(ba => new AuthorViewModel
                    {
                        Id = ba.Author.Id,
                        Name = ba.Author.Name,
                        Url = ba.Author.Url
                    }).ToList(),
                    Images = book.Images
                });
            }
            BookListViewModel bookListViewModel = new BookListViewModel
            {
                Books = books,
                ApprovedStatus = true
            };
            return View("Index", bookListViewModel);
        }

        #endregion
        #region Yeni Kayıt
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BookAddViewModel bookAddViewModel = new BookAddViewModel
            {
                Categories = await categoryRepository.GetCategoriesAsync(true),
                Authors = await authorRepository.GetAllAsync()
            };

            return View(bookAddViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookAddViewModel bookAddViewModel)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book
                {
                    Name = bookAddViewModel.Name,
                    Stock = bookAddViewModel.Stock,
                    PageCount = bookAddViewModel.PageCount,
                    Price = bookAddViewModel.Price,
                    EditionNumber = bookAddViewModel.EditionNumber,
                    EditionYear = bookAddViewModel.EditionYear,
                    Url = Jobs.GetUrl(bookAddViewModel.Name),
                    IsApproved = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                List<Image> images = bookAddViewModel.Images.Select(i => new Image
                {
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsApproved = true,
                    Url = Jobs.UploadImage(i)
                }).ToList();

                await bookRepository.CreateBook(book, bookAddViewModel.SelectedCategories, bookAddViewModel.SelectedAuthors, images);
                TempData["Message"] = Jobs.CreateMessage("Başarılı", "Kitap başarıyla kaydedilmiştir.", "success");
                return RedirectToAction("Index");
            }
            bookAddViewModel.Categories = await categoryRepository.GetCategoriesAsync(true);
            bookAddViewModel.Authors = await authorRepository.GetAllAsync();
            TempData["Message"] = Jobs.CreateMessage("HATA!", "Kayıt sırasında bir hata oluştu, kontrol edip yeniden deneyiniz!", "warning");
            return View(bookAddViewModel);
        }

        #endregion
        #region Kayıt Güncelleme
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = await bookRepository.GetBookFullDataAsync(id);
            BookUpdateViewModel bookUpdateViewModel = new BookUpdateViewModel()
            {
                Id = book.Id,
                Name = book.Name,
                Stock = book.Stock,
                PageCount = book.PageCount,
                EditionNumber = book.EditionNumber,
                EditionYear = book.EditionYear,
                Price = book.Price,
                ModifiedDate = book.ModifiedDate,
                IsApproved = book.IsApproved,
                ImageList = book.Images.Select(i => new Image
                {
                    Id = i.Id,
                    Url = i.Url
                }).ToList(),
                SelectedCategories = book.BookCategories.Select(i => i.Category.Id).ToArray(),
                SelectedAuthors = book.BookAuthors.Select(i => i.Author.Id).ToArray()
            };
            bookUpdateViewModel.Categories = await categoryRepository.GetCategoriesAsync(true);
            bookUpdateViewModel.Authors = await authorRepository.GetAllAsync();
            return View(bookUpdateViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(BookUpdateViewModel bookUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                Book book = await bookRepository.GetBookFullDataAsync(bookUpdateViewModel.Id);
                book.Name = bookUpdateViewModel.Name;
                book.Url = Jobs.GetUrl(bookUpdateViewModel.Name);
                book.IsApproved = bookUpdateViewModel.IsApproved;
                book.ModifiedDate = DateTime.Now;
                book.Stock = bookUpdateViewModel.Stock;
                book.PageCount = bookUpdateViewModel.PageCount;
                book.EditionNumber = bookUpdateViewModel.EditionNumber;
                book.EditionYear = bookUpdateViewModel.EditionYear;
                book.Price = bookUpdateViewModel.Price;

                if (bookUpdateViewModel.Images != null)
                {
                    List<Image> images = bookUpdateViewModel.Images.Select(i => new Image
                    {
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsApproved = true,
                        Url = Jobs.UploadImage(i),
                        BookId = book.Id
                    }).ToList();
                    foreach (var item in images)
                    {
                        await imageRepository.CreateAsync(item);
                    }
                }
                else if (imageRepository.ImageCount(book.Id) == 0)
                {
                    bookUpdateViewModel.Categories = await categoryRepository.GetCategoriesAsync(true);
                    bookUpdateViewModel.Images = new List<IFormFile>();
                    bookUpdateViewModel.ImageList = new List<Image>();

                    return View(bookUpdateViewModel);
                }

                await bookRepository.UpdateBook(book, bookUpdateViewModel.SelectedCategories, bookUpdateViewModel.SelectedAuthors);
                return RedirectToAction("Index");
            }
            bookUpdateViewModel.Categories = await categoryRepository.GetCategoriesAsync(true);
            bookUpdateViewModel.Authors = await authorRepository.GetAllAsync();
            if (bookUpdateViewModel.ImageList == null) bookUpdateViewModel.ImageList = new();
            bookUpdateViewModel.Images = new();
            return View(bookUpdateViewModel);
        }

        //#endregion
        //#region Kayıt Silme
        //public async Task<IActionResult> Delete(int id)
        //{
        //    Author deletedAuthor = await authorRepository.GetByIdAsync(id);
        //    if (deletedAuthor != null)
        //    {
        //        authorRepository.Delete(deletedAuthor);
        //    }

        //    return RedirectToAction("Index");
        //}
#endregion
        #region Onaylı
        public async Task<IActionResult> UpdateIsApproved(int id, bool ApprovedStatus)
        {
            Book book = await bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                book.IsApproved = !book.IsApproved;
                bookRepository.Update(book);
            }
            BookListViewModel bookListViewModel = new BookListViewModel
            {
                ApprovedStatus = ApprovedStatus
            };
            return RedirectToAction("Index", bookListViewModel);
        }
        #endregion
        #region Resim Silme
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id, int bookId)
        {
            var image = await imageRepository.GetByIdAsync(id);
            if (image != null)
            {
                imageRepository.Delete(image);
            }
            //        return RedirectToAction("Edit", new RouteValueDictionary(
            //new { area="Admin",  controller = "Books", action = "Edit", Id = bookId }));
            return RedirectToAction("Edit", new { id = bookId });
        }
        #endregion
    }
}
