using Core.Models;
using Core.Models.Requests;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Retailr3.Models.ProductDiscount;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Retailr3.Controllers
{
    public class ProductDiscountController : BaseController
    {
        private readonly IProductDiscountService _productDiscountService;
        private readonly IOptions<AppConfig> _appConfig;

        public ProductDiscountController(IProductDiscountService productDiscountService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _productDiscountService = productDiscountService;
        }

        // GET: ProductDiscount
        public async Task<ActionResult> Index()
        {
            var productDiscounts = new List<ListProductDiscountViewModel>();
            try
            {
                var result = await _productDiscountService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(productDiscounts);
                }

                foreach (var productDiscount in result.Data)
                {
                    productDiscounts.Add(new ListProductDiscountViewModel
                    {
                        Id = productDiscount.Id,
                        Name = productDiscount.Name,
                        CatalogueName = productDiscount.CatalogueName,
                        CategoryName = productDiscount.CategoryName,
                        BrandName = productDiscount.BrandName,
                        PackagingName = productDiscount.PackagingName,
                        ProductName = productDiscount.ProductName,
                        EffectiveDate = productDiscount.EffectiveDate,
                        EndDate = productDiscount.EndDate,
                        DateCreated = productDiscount.CreatedAt,
                        DateLastUpdated = productDiscount.LastUpdated
                    });
                }

                return View(productDiscounts);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(productDiscounts);
            }

        }

        // GET: ProductDiscount/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var productDiscount = new ProductDiscountDetailsViewModel();
            try
            {
                var result = await _productDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(productDiscount);
                }
                productDiscount.CatalogueName = result.Data.CatalogueName;
                productDiscount.Id = result.Data.Id;
                productDiscount.Name = result.Data.Name;
                productDiscount.CategoryName = result.Data.CategoryName;
                productDiscount.BrandName = result.Data.BrandName;
                productDiscount.PackagingName = result.Data.PackagingName;
                productDiscount.ProductName = result.Data.ProductName;
                productDiscount.EffectiveDate = result.Data.EffectiveDate;
                productDiscount.EndDate = result.Data.EndDate;
                productDiscount.DateCreated = result.Data.CreatedAt;
                productDiscount.DateLastUpdated = result.Data.LastUpdated;
                return View(productDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(productDiscount);
            }
        }

        // GET: ProductDiscount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductDiscount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddProductDiscountViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addProductDiscountRequest = new AddProductDiscountRequest { Name = request.Name, CatalogueId = request.CatalogueId,CategoryId = request.CategoryId,BrandId = request.BrandId,PackagingId = request.PackagingId, ProductId = request.ProductId, EffectiveDate = request.EffectiveDate, EndDate = request.EndDate };
                var result = await _productDiscountService.Create(addProductDiscountRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Product Discount Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: ProductDiscount/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var productDiscount = new EditProductDiscountViewModel();
            try
            {
                var result = await _productDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(productDiscount);
                }
                productDiscount.Id = result.Data.Id;
                productDiscount.Name = result.Data.Name;
                productDiscount.CatalogueId = result.Data.CatalogueId;
                productDiscount.CategoryId = result.Data.CategoryId;
                productDiscount.BrandId = result.Data.BrandId;
                productDiscount.PackagingId = result.Data.PackagingId;
                productDiscount.ProductId = result.Data.ProductId;
                productDiscount.EffectiveDate = result.Data.EffectiveDate;
                productDiscount.EndDate = result.Data.EndDate;
                return View(productDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(productDiscount);
            }
        }

        // POST: ProductDiscount/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditProductDiscountViewModel request)
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
                var productDiscountUpdateRequest = new UpdateProductDiscountRequest { Id = request.Id, Name = request.Name, CatalogueId = request.CatalogueId, CategoryId = request.CategoryId, BrandId = request.BrandId, PackagingId = request.PackagingId, ProductId = request.ProductId, EffectiveDate = request.EffectiveDate, EndDate = request.EndDate };
                var result = await _productDiscountService.Update(id, productDiscountUpdateRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Product Discount Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: ProductDiscount/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var productDiscount = new ProductDiscountDetailsViewModel();
            try
            {
                var result = await _productDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(productDiscount);
                }
                productDiscount.CatalogueName = result.Data.CatalogueName;
                productDiscount.Id = result.Data.Id;
                productDiscount.Name = result.Data.Name;
                productDiscount.CategoryName = result.Data.CategoryName;
                productDiscount.BrandName = result.Data.BrandName;
                productDiscount.PackagingName = result.Data.PackagingName;
                productDiscount.ProductName = result.Data.ProductName;
                productDiscount.EffectiveDate = result.Data.EffectiveDate;
                productDiscount.EndDate = result.Data.EndDate;
                productDiscount.DateCreated = result.Data.CreatedAt;
                productDiscount.DateLastUpdated = result.Data.LastUpdated;
                return View(productDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(productDiscount);
            }
        }

        // POST: ProductDiscount/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, ProductDiscountDetailsViewModel request)
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
                var result = await _productDiscountService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Product Discount Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }
    }
}