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
using Retailr3.Models.ProductViewModel;

namespace Retailr3.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IProductService _productService;
        private readonly IOptions<AppConfig> _appConfig;
        private readonly IBrandService _brandService;
        private readonly ISubBrandService _subBrandService;
        private readonly IPackagingService _packagingService;
        private readonly IPackagingTypeService _packagingTypeService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IUnitOfMeasureService _unitOfMeasureService;
        private readonly IUnitOfMeasureTypeService _unitOfMeasureTypeService;
        private readonly IVatService _vatService;
        private readonly IVatCategoryService _vatCategoryService;

        public ProductController(IProductService productService, IVatCategoryService vatCategoryService, IVatService vatService, IUnitOfMeasureTypeService unitOfMeasureTypeService, IUnitOfMeasureService unitOfMeasureService,
            ISubCategoryService subCategoryService, ICategoryService categoryService, IPackagingTypeService packagingTypeService,
            IPackagingService packagingService, ISubBrandService subBrandService, IBrandService brandService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _productService = productService;
            _brandService = brandService;
            _subBrandService = subBrandService;
            _packagingService = packagingService;
            _packagingTypeService = packagingTypeService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _unitOfMeasureService = unitOfMeasureService;
            _unitOfMeasureTypeService = unitOfMeasureTypeService;
            _vatCategoryService = vatCategoryService;
            _vatService = vatService;
        }

            // GET: Product
        public async Task<ActionResult> Index()
        {
            var product = new List<ListProductViewModel>();
            try
            {
                var result = await _productService.FindAllInclusive(a => a.Include(b => b.PackagingType).ThenInclude(c => c.Packaging).Include(d => d.SubBrand).ThenInclude(e=> e.Brand).Include(f => f.SubCategory).ThenInclude(g => g.Category)
                .Include(h => h.UnitOfMeasure).ThenInclude(i => i.UnitOfMeasureType).Include(j => j.Vat).ThenInclude( k =>k.VatCategory));
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(product);
                }

                foreach (var prod in result.Data)
                {
                    product.Add(new ListProductViewModel
                    {
                        Name =prod.Name,
                        Desription = prod.Description,
                        Manufacturer = prod.Manufacturer,
                        FactoryPrice = prod.FactoryPrice,
                        ImageUrl = prod.ImageUrl,
                        ReOrderLevel = prod.ReOrderLevel,
                        Brand = prod.SubBrand.Brand.Name,
                        SubBrand = prod.SubBrand.Name,
                        Packaging = prod.PackagingType.Packaging.Name,
                        PackagingType = prod.PackagingType.Name,
                        Category = prod.SubCategory.Category.Name,
                        SubCategory = prod.SubCategory.Name,
                        UnitOfMeasureType = prod.UnitOfMeasure.UnitOfMeasureType.Name,
                        UnitOfMeasure = prod.UnitOfMeasure.Name,
                        VatCategory = prod.Vat.VatCategory.Name,
                        Vat = prod.Vat.Rate.ToString(),
                        DateCreated = prod.CreatedAt,
                        DateLastUpdated = prod.LastUpdated
                    });
                }

                return View(product);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(product);
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}