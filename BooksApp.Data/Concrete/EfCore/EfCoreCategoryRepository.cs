using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {
        BooksAppContext _appContext = new BooksAppContext();
        public async Task<List<Category>> GetCategoriesAsync(bool ApprovedStatus)
        {
            return await _appContext
                .Categories
                .Where(c => c.IsApproved==ApprovedStatus)
                .ToListAsync();
        }

        public async Task<string> GetCategoryNameByUrlAsync(string url)
        {
            Category category = await _appContext
                .Categories
                .Where(c => c.Url == url)
                .FirstOrDefaultAsync();
            return category.Name;
        }
    }
}