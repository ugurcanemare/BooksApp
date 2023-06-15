using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore.Context;
using BooksApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Data.Concrete.EfCore
{
    public class EfCoreImageRepository : EfCoreGenericRepository<Image>, IImageRepository
    {
        BooksAppContext _appContext = new BooksAppContext();
        public int ImageCount(int bookId)
        {
            return _appContext.Images.Count(i=>i.BookId== bookId);
        }
    }
}