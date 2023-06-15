using BooksApp.Core;
using BooksApp.Data.Concrete.EfCore;
using BooksApp.Entity.Concrete;
using BooksApp.MVC.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BooksApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        EfCoreCategoryRepository categoryRepository = new EfCoreCategoryRepository();

        #region Listeleme
        public async Task<IActionResult> Index(CategoryListViewModel categoryListViewModel)
        {
            List<Category> categoryList = await categoryRepository.GetCategoriesAsync(categoryListViewModel.ApprovedStatus);
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            foreach (var category in categoryList)
            {
                categories.Add(new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    IsApproved = category.IsApproved,
                    Url = category.Url,
                });
            }
            categoryListViewModel.Categories = categories;

            return View(categoryListViewModel);
        }

        #endregion
        #region Yeni Kayıt
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddViewModel categoryAddViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Name = categoryAddViewModel.Name,
                    Description = categoryAddViewModel.Description,
                    Url = Jobs.GetUrl(categoryAddViewModel.Name),
                    IsApproved = categoryAddViewModel.IsApproved,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                await categoryRepository.CreateAsync(category);
                return RedirectToAction("Index");
            }
            return View(categoryAddViewModel);
        }

        #endregion
        #region Kayıt Güncelleme
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await categoryRepository.GetByIdAsync(id);
            CategoryUpdateViewModel categoryUpdateViewModel = new CategoryUpdateViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Url = category.Url,
                ModifiedDate = category.ModifiedDate,
                IsApproved = category.IsApproved
            };

            return View(categoryUpdateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryUpdateViewModel categoryUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = await categoryRepository.GetByIdAsync(categoryUpdateViewModel.Id);
                category.Name = categoryUpdateViewModel.Name;
                category.Description = categoryUpdateViewModel.Description;
                category.Url = Jobs.GetUrl(categoryUpdateViewModel.Name);
                category.IsApproved = categoryUpdateViewModel.IsApproved;
                category.ModifiedDate = DateTime.Now;
                categoryRepository.Update(category);

                return RedirectToAction("Index");
            }
            return View(categoryUpdateViewModel);
        }

        #endregion
        #region Kayıt Silme
        public async Task<IActionResult> Delete(int id)
        {
            Category deletedCategory = await categoryRepository.GetByIdAsync(id);
            if (deletedCategory != null)
            {
                categoryRepository.Delete(deletedCategory);
            }

            return RedirectToAction("Index");
        }

        #endregion
        #region Onaylı
        public async Task<IActionResult> UpdateIsApproved(int id, bool ApprovedStatus)
        {
            Category category = await categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                category.IsApproved = !category.IsApproved;
                categoryRepository.Update(category);
            }
            CategoryListViewModel categoryListViewModel = new CategoryListViewModel
            {
                ApprovedStatus = ApprovedStatus
            };
            return RedirectToAction("Index", categoryListViewModel);
        }
        #endregion
    }
}
