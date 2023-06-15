using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Data.Concrete.EfCore
{
    public class EfCoreBookRepository : EfCoreGenericRepository<Book>, IBookRepository
    {
        private BooksAppContext _appContext = new BooksAppContext();

        public async Task<List<Book>> GetAllBooksFullDataAsync(bool ApprovedStatus, string categoryurl = null)
        {
            var books = _appContext
                            .Books
                            .Where(b => b.IsApproved == ApprovedStatus)
                            .Include(b => b.BookCategories)
                            .ThenInclude(bc => bc.Category)
                            .AsQueryable();
            if (categoryurl != null)
            {
                books = books
                    .Where(b => b.BookCategories.Any(bc => bc.Category.Url == categoryurl));
            }

            return await books
                        .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                        .Include(b => b.Images)
                        .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByAuthor(int id)
        {
            List<Book> books = await _appContext
                .Books
                .Where(b => b.IsApproved == true)
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .Include(b => b.Images)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Where(b => b.BookAuthors.Any(x => x.AuthorId == id))
                .ToListAsync();
            return books;
        }

        public async Task CreateBook(Book book, int[] SelectedCategories, int[] SelectedAuthors, List<Image> Images)
        {

            await _appContext.Books.AddAsync(book);
            await _appContext.SaveChangesAsync();
            List<BookCategory> bookCategories = new List<BookCategory>();
            foreach (var categoryId in SelectedCategories)
            {
                bookCategories.Add(new BookCategory
                {
                    CategoryId = categoryId,
                    BookId = book.Id
                });
            }
            _appContext.BookCategories.AddRange(bookCategories);

            List<BookAuthor> bookAuthors = new List<BookAuthor>();
            foreach (var authorId in SelectedAuthors)
            {
                bookAuthors.Add(new BookAuthor
                {
                    AuthorId = authorId,
                    BookId = book.Id
                });
            }
            _appContext.BookAuthors.AddRange(bookAuthors);
            foreach (var image in Images)
            {
                image.BookId = book.Id;
            }
            _appContext.Images.AddRange(Images);
            await _appContext.SaveChangesAsync();
        }

        public async Task<Book> GetBookFullDataAsync(int id)
        {
            var book = await _appContext
                            .Books
                            .Where(b => b.Id == id)
                            .Include(b => b.BookCategories)
                            .ThenInclude(bc => bc.Category)
                            .Include(b => b.BookAuthors)
                            .ThenInclude(ba => ba.Author)
                            .Include(b => b.Images)
                            .FirstOrDefaultAsync();
            return book;
        }

        public async Task UpdateBook(Book book, int[] SelectedCategories, int[] SelectedAuthors)
        {
            Book newBook = _appContext
                .Books
                .Include(b => b.BookCategories)
                .Include(b => b.BookAuthors)
                .FirstOrDefault(b => b.Id == book.Id);
            newBook.Name = book.Name;
            newBook.CreatedDate= book.CreatedDate;
            newBook.ModifiedDate = DateTime.Now;
            newBook.PageCount=book.PageCount;
            newBook.Price = book.Price;
            newBook.EditionYear = book.EditionYear;
            newBook.EditionNumber = book.EditionNumber;
            newBook.Url = book.Url;
            newBook.IsApproved = book.IsApproved;
            newBook.Images = book.Images;

            newBook.BookCategories = SelectedCategories
                .Select(sc => new BookCategory
                {
                    BookId = newBook.Id,
                    CategoryId = sc
                }).ToList();
            newBook.BookAuthors = SelectedAuthors
                .Select(sa => new BookAuthor
                {
                    BookId = newBook.Id,
                    AuthorId = sa
                }).ToList();
            _appContext.Update(book);
            await _appContext.SaveChangesAsync();
        }
    }
}