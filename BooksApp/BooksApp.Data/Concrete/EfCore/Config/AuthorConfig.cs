using BooksApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksApp.Data.Concrete.EfCore.Config
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.Property(x => x.BirthDate).IsRequired();

            builder.Property(x => x.Gender).IsRequired();

            builder.HasData(
            new Author { Id = 1, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Saniye Bencik Kangal", Gender = "K", Url = "saniye-bencik-kangal" },
new Author { Id = 2, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Osamu Dazai", Gender = "E", Url = "osamu-dazai" },
new Author { Id = 3, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "Mark Wolynn", Gender = "E", Url = "mark-wolynn" },
new Author { Id = 4, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, IsApproved = true, Name = "İlber Ortaylı", Gender = "E", Url = "ilber-ortaylı" }
            );
        }
    }
}