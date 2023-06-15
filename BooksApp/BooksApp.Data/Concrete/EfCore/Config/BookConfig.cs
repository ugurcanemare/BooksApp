using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksApp.Data.Concrete.EfCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Stock).IsRequired();

            builder.Property(x => x.Price).IsRequired();

            builder.Property(x => x.PageCount).IsRequired();

            builder.Property(x => x.EditionYear).IsRequired();

            builder.Property(x => x.EditionNumber).IsRequired();

            builder.HasData(
                new Book { Id = 1, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Balyanaklar İçin Mahremiyet Kitabı", Stock = 10, Price = 65, PageCount = 32, EditionYear = 2023, EditionNumber = 1, Url = "balyanaklar-icin-mahremiyet-kitabi" },
new Book { Id = 2, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "İnsanlığımı Yitirirken", Stock = 15, Price = 30, PageCount = 128, EditionYear = 2022, EditionNumber = 2, Url = "insanligimi-yitirirken" },
new Book { Id = 3, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Seninle Başlamadı", Stock = 15, Price = 30, PageCount = 280, EditionYear = 2021, EditionNumber = 38, Url = "seninle-baslamadi" },
new Book { Id = 4, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Osmanlıyı Yeniden Keşfetmek", Stock = 15, Price = 125, PageCount = 240, EditionYear = 2021, EditionNumber = 5, Url = "osmanliyi-yeniden-kesfetmek" }
            );
        }
    }
}