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
using Retailr3.Models.Brand;
using Retailr3.Models.SubBrand;

namespace Retailr3.Controllers
{
    public class BrandsController : BaseController

    {
        private readonly IBrandService _brandService;
        private readonly ISubBrandService _subBrandService;
        private readonly IOptions<AppConfig> _appConfig;

        public BrandsController(ISubBrandService subBrandService, IBrandService brandService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _brandService = brandService;
            _subBrandService = subBrandService;
        }
        // GET: Brands
        public async Task<ActionResult> Index()
        {
            var brands = new List<ListBrandViewModel>();
            try
            {
                var result = await _brandService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(brands);
                }

                foreach (var brand in result.Data)
                {
                    brands.Add(new ListBrandViewModel
                    {
                        Id = brand.Id,
                        Code = brand.Code,
                        Description = brand.Description,
                        Name = brand.Name,
                        DateCreated = brand.CreatedAt,
                        DateLastUpdated = brand.LastUpdated
                    });
                }

                return View(brands);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(brands);
            }

        }

        // GET: Brands/Details/5
        public async Task<ActionResult> Details(Guid id)
    {
            var productDiscount = new BrandDetailsViewModel();
            try
            {
                var result = await _brandService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(productDiscount);
                }
                productDiscount.Code = result.Data.Code;
                productDiscount.Id = result.Data.Id;
                productDiscount.Name = result.Data.Name;
                productDiscount.Description = result.Data.Description;
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

        // GET: Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddBrandViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addBrandRequest = new AddBrandRequest { Name = request.Name, Description = request.Description };
                var result = await _brandService.Create(addBrandRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Brand Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Brands/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var brand = new EditBrandViewModel();
            try
            {
                var result = await _brandService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(brand);
                }
                brand.Id = result.Data.Id;
                brand.Name = result.Data.Name;
                brand.Description = result.Data.Description;
                return View(brand);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(brand);
            }
        }

        // POST: Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditBrandViewModel request)
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
                var brandUpdateRequest = new UpdateBrandRequest { Id = request.Id, Name = request.Name, Description = request.Description };
                var result = await _brandService.Update(id, brandUpdateRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Brand Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Brands/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var brand = new BrandDetailsViewModel();
            try
            {
                var result = await _brandService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(brand);
                }
                brand.Code = result.Data.Code;
                brand.Id = result.Data.Id;
                brand.Name = result.Data.Name;
                brand.Description = result.Data.Description;
                brand.DateCreated = result.Data.CreatedAt;
                brand.DateLastUpdated = result.Data.LastUpdated;
                return View(brand);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(brand);
            }
        }

        // POST: Brands/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, BrandDetailsViewModel request)
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
                var result = await _brandService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Brand Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }


        //SUB BRAND SECTION
        public async Task<ActionResult> SubBrands(Guid id)
        {
            var subBrandList = new List<ListSubBrandViewModel>();
            try
            {
                var result = await _brandService.FindByIdInclusive(id, x => x.Include(p => p.SubBrands));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(subBrandList);
                }

                ViewBag.Brand = result.Data;

                if (result.Data.SubBrands.Count > 0)
                {
                    foreach (var item in result.Data.SubBrands)
                    {
                        subBrandList.Add(new ListSubBrandViewModel
                        {
                            Code = item.Code,
                            DateCreated = item.CreatedAt,
                            DateLastUpdated = item.LastUpdated,
                            Description = item.Description,
                            Id = item.Id,
                            Name = item.Name,
                            BrandName = result.Data.Name
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

        public ActionResult CreateSubBrand(Guid brandId, string BrandName)
        {
            var sb = new AddSubBrandViewModel { BrandId = brandId };
            ViewBag.brandName = BrandName;
            return View(sb);
        }

        // POST: Brand/CreateSubBrand
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSubBrand(AddSubBrandViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubBrands), new { id = request.BrandId });
            }
            try
            {
                var addSubBrandRequest = new AddSubBrandRequest { BrandId = request.BrandId, Name = request.Name, Description = request.Description };
                var result = await _subBrandService.Create(addSubBrandRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(SubBrands), new { id = request.BrandId });
                }
                Alert($"Sub Brand Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubBrands), new { id = request.BrandId });
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubBrands), new { id = request.BrandId });
            }
        }

        public async Task<ActionResult> SubBrandDetails(Guid id)
        {
            var subBrand = new SubBrandDetailsViewModel();
            try
            {
                var result = await _subBrandService.FindByIdInclusive(id, x => x.Include(p => p.Brand));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(subBrand);
                }
                subBrand.Code = result.Data.Code;
                subBrand.Id = result.Data.Id;
                subBrand.Name = result.Data.Name;
                subBrand.Description = result.Data.Description;
                subBrand.DateCreated = result.Data.CreatedAt;
                subBrand.DateLastUpdated = result.Data.LastUpdated;
                subBrand.BrandName = result.Data.Brand.Name;
                subBrand.BrandId = result.Data.Brand.Id;
                return View(subBrand);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(subBrand);
            }
        }

        public async Task<ActionResult> EditSubBrand(Guid id)
        {
            var subBrand = new EditSubBrandViewModel();
            try
            {
                var result = await _subBrandService.FindByIdInclusive(id, x => x.Include(p => p.Brand));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(subBrand);
                }
                subBrand.Id = result.Data.Id;
                subBrand.Name = result.Data.Name;
                subBrand.Description = result.Data.Description;
                subBrand.BrandId = result.Data.Brand.Id;
                subBrand.BrandName = result.Data.Brand.Name;
                return View(subBrand);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(subBrand);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSubBrand(Guid id, EditSubBrandViewModel request)
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
                var subBrandEditRequest = new UpdateSubBrandRequest { Id = request.Id, Name = request.Name, Description = request.Description, BrandId = request.BrandId };
                var result = await _subBrandService.Update(id, subBrandEditRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Sub Brand Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubBrands), new { id = request.BrandId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        public async Task<ActionResult> DeleteSubBrand(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var subBrand = new SubBrandDetailsViewModel();
            try
            {
                var result = await _subBrandService.FindByIdInclusive(id, x => x.Include(p => p.Brand));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(subBrand);
                }
                subBrand.Code = result.Data.Code;
                subBrand.Id = result.Data.Id;
                subBrand.Name = result.Data.Name;
                subBrand.Description = result.Data.Description;
                subBrand.DateCreated = result.Data.CreatedAt;
                subBrand.DateLastUpdated = result.Data.LastUpdated;
                subBrand.BrandName = result.Data.Brand.Name;
                subBrand.BrandId = result.Data.Brand.Id;
                return View(subBrand);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(subBrand);
            }
        }

        // POST: Brand/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSubBrand(Guid id, SubBrandDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubBrands), new { id = request.BrandId });
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubBrands), new { id = request.BrandId });
            }
            try
            {
                var result = await _subBrandService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(SubBrands), new { id = request.BrandId });
                }
                Alert($"Sub Brand Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubBrands), new { id = request.BrandId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SubBrands), new { id = request.BrandId });
            }
        }
    }
}