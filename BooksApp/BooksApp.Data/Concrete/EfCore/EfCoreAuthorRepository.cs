using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Data.Concrete.EfCore
{
    public class EfCoreAuthorRepository : EfCoreGenericRepository<Author>, IAuthorRepository
    {
        BooksAppContext _appContext = new BooksAppContext();

        public async Task<List<Author>> GetAllAuthorsWithBooksAsync(bool ApprovedStatus)
        {
            List<Author> result = await _appContext
                    .Authors
                    .Where(a => a.IsApproved == ApprovedStatus)
                    .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                    .ToListAsync();
            return result;
        }

        public async Task<List<Author>> GetAuthorsByBook(int id)
        {
            List<Author> result = await _appContext
                .Authors
                .Where(a => a.IsApproved == true)
                .Include(a => a.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .Where(a => a.BookAuthors.Any(x => x.BookId == id))
                .ToListAsync();
            return result;
        }
    }
}