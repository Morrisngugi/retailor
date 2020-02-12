using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Requests;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Retailr3.Controllers;
using Retailr3.Models.Category;
using Retailr3.Models.SubCategory;

namespace Retailr3.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IOptions<AppConfig> _appConfig;

        public CategoriesController(ICategoryService categoryService, ISubCategoryService subCategoryService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }


        // GET: Categories
        public async Task<ActionResult> Index()
        {
            var categories = new List<ListCategoriesViewModel>();
            try
            {
                var result = await _categoryService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(categories);
                }

                foreach (var brand in result.Data)
                {
                    categories.Add(new ListCategoriesViewModel
                    {
                        Id = brand.Id,
                        Code = brand.Code,
                        Description = brand.Description,
                        Name = brand.Name,
                        DateCreated = brand.CreatedAt,
                        DateLastUpdated = brand.LastUpdated
                    });
                }

                return View(categories);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(categories);
            }

        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var categories = new CategoryDetailsViewModel();
            try
            {
                var result = await _categoryService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(categories);
                }
                categories.Code = result.Data.Code;
                categories.Id = result.Data.Id;
                categories.Name = result.Data.Name;
                categories.Description = result.Data.Description;
                categories.DateCreated = result.Data.CreatedAt;
                categories.DateLastUpdated = result.Data.LastUpdated;
                return View(categories);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(categories);
            }
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddCategoryViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addCategoryRequest = new AddCategoryRequest { Name = request.Name, Description = request.Description };
                var result = await _categoryService.Create(addCategoryRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Category Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var category = new EditCategoryViewModel();
            try
            {
                var result = await _categoryService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(category);
                }
                category.Id = result.Data.Id;
                category.Name = result.Data.Name;
                category.Description = result.Data.Description;
                return View(category);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(category);
            }
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditCategoryViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            if (!id.Equals(request.Id))
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var categoryUpdateRequest = new UpdateCategoryRequest { Id = request.Id, Name = request.Name, Description = request.Description };
                var result = await _categoryService.Update(id, categoryUpdateRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Category Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var category = new CategoryDetailsViewModel();
            try
            {
                var result = await _categoryService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(category);
                }
                category.Code = result.Data.Code;
                category.Id = result.Data.Id;
                category.Name = result.Data.Name;
                category.Description = result.Data.Description;
                category.DateCreated = result.Data.CreatedAt;
                category.DateLastUpdated = result.Data.LastUpdated;
                return View(category);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(category);
            }
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, CategoryDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var result = await _categoryService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Category Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        //SUB CATEGORIES SECTION
        public async Task<ActionResult> SubCategories(Guid id)
        {
            var subBrandList = new List<ListSubCategoryViewModel>();
            try
            {
                var result = await _categoryService.FindByIdInclusive(id, x => x.Include(p => p.SubCategories));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(subBrandList);
                }

                ViewBag.Category = result.Data;

                if (result.Data.SubCategories.Count > 0)
                {
                    foreach (var item in result.Data.SubCategories)
                    {
                        subBrandList.Add(new ListSubCategoryViewModel
                        {
                            Code = item.Code,
                            DateCreated = item.CreatedAt,
                            DateLastUpdated = item.LastUpdated,
                            Description = item.Description,
                            Id = item.Id,
                            Name = item.Name,
                            CategoryName = result.Data.Name
                        });
                    }
                }
                return View(subBrandList);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(subBrandList);
            }
        }

        public ActionResult CreateSubCategory(Guid categoryId, string CategoryName)
        {
            var scategory = new AddSubCategoryViewModel { CategoryId = categoryId };
            ViewBag.CategoryName = CategoryName;
            return View(scategory);
        }

        // POST: Category/CreateSubCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSubCategory(AddSubCategoryViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubCategories), new { id = request.CategoryId });
            }
            try
            {
                var addSubCategoryRequest = new AddSubCategoryRequest { CategoryId = request.CategoryId, Name = request.Name, Description = request.Description };
                var result = await _subCategoryService.Create(addSubCategoryRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(SubCategories), new { id = request.CategoryId });
                }
                Alert($"Sub Category Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubCategories), new { id = request.CategoryId });
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubCategories), new { id = request.CategoryId });
            }
        }

        public async Task<ActionResult> SubCategoryDetails(Guid id)
        {
            var subCategory = new SubCategoryDetailsViewModel();
            try
            {
                var result = await _subCategoryService.FindByIdInclusive(id, x => x.Include(p => p.Category));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(subCategory);
                }
                subCategory.Code = result.Data.Code;
                subCategory.Id = result.Data.Id;
                subCategory.Name = result.Data.Name;
                subCategory.Description = result.Data.Description;
                subCategory.DateCreated = result.Data.CreatedAt;
                subCategory.DateLastUpdated = result.Data.LastUpdated;
                subCategory.CategoryName = result.Data.Category.Name;
                subCategory.CategoryId = result.Data.Category.Id;
                return View(subCategory);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(subCategory);
            }
        }

        public async Task<ActionResult> EditSubcategory(Guid id)
        {
            var subCategory = new EditSubCategoryViewModel();
            try
            {
                var result = await _subCategoryService.FindByIdInclusive(id, x => x.Include(p => p.Category));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(subCategory);
                }
                subCategory.Id = result.Data.Id;
                subCategory.Name = result.Data.Name;
                subCategory.Description = result.Data.Description;
                subCategory.CategoryId = result.Data.Category.Id;
                subCategory.CategoryName = result.Data.Category.Name;
                return View(subCategory);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(subCategory);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSubCategory(Guid id, EditSubCategoryViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            if (!id.Equals(request.Id))
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var subCategoryEditRequest = new UpdateSubCategoryRequest { Id = request.Id, Name = request.Name, Description = request.Description, CategoryId = request.CategoryId };
                var result = await _subCategoryService.Update(id, subCategoryEditRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Sub Category Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubCategories), new { id = request.CategoryId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        public async Task<ActionResult> DeleteSubCategory(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var subCategory = new SubCategoryDetailsViewModel();
            try
            {
                var result = await _subCategoryService.FindByIdInclusive(id, x => x.Include(p => p.Category));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(subCategory);
                }
                subCategory.Code = result.Data.Code;
                subCategory.Id = result.Data.Id;
                subCategory.Name = result.Data.Name;
                subCategory.Description = result.Data.Description;
                subCategory.DateCreated = result.Data.CreatedAt;
                subCategory.DateLastUpdated = result.Data.LastUpdated;
                subCategory.CategoryName = result.Data.Category.Name;
                subCategory.CategoryId = result.Data.Category.Id;
                return View(subCategory);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(subCategory);
            }
        }

        // POST: Brand/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSubCategory(Guid id, SubCategoryDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubCategories), new { id = request.CategoryId });
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubCategories), new { id = request.CategoryId });
            }
            try
            {
                var result = await _subCategoryService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(SubCategories), new { id = request.CategoryId });
                }
                Alert($"Sub Category Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubCategories), new { id = request.CategoryId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubCategories), new { id = request.CategoryId });
            }
        }
    }
}