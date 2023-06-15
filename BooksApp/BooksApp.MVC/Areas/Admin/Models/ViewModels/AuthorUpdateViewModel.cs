using BooksApp.Entity.Concrete;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BooksApp.MVC.Areas.Admin.Models.ViewModels
{
    public class AuthorUpdateViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsApproved { get; set; }

        [DisplayName("Yazar Adı")]
        [Required(ErrorMessage = "Yazar adı boş bırakılmamalıdır")]
        [MinLength(5, ErrorMessage = "Yazar adı en az 5 karakter olmalıdır")]
        [MaxLength(100, ErrorMessage = "Yazar adı en fazla 100 karakter olmalıdır")]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public List<Book> Books { get; set; }
    }
}
