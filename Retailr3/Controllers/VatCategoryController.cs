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
using Retailr3.Models.Vat;
using Retailr3.Models.VatCategory;

namespace Retailr3.Controllers
{
    public class VatCategoryController : BaseController
    {
        private readonly IVatCategoryService _vatCategoryService;
        private readonly IVatService _vatService;
        private readonly IOptions<AppConfig> _appConfig;

        public VatCategoryController(IVatCategoryService vatCategoryService, IVatService vatService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _vatCategoryService = vatCategoryService;
            _vatService = vatService;
        }
        // GET: VatCategory
        public async Task<ActionResult> Index()
        {
            var vatCategory = new List<ListVatCategoryViewModel>();
            try
            {
                var result = await _vatCategoryService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(vatCategory);
                }

                foreach (var brand in result.Data)
                {
                    vatCategory.Add(new ListVatCategoryViewModel
                    {
                        Id = brand.Id,
                        Code = brand.Code,
                        Description = brand.Description,
                        Name = brand.Name,
                        DateCreated = brand.CreatedAt,
                        DateLastUpdated = brand.LastUpdated
                    });
                }

                return View(vatCategory);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(vatCategory);
            }

        }

        // GET: VatCategory/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var vatCategory = new VatCategoryDetailsViewModel();
            try
            {
                var result = await _vatCategoryService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(vatCategory);
                }
                vatCategory.Code = result.Data.Code;
                vatCategory.Id = result.Data.Id;
                vatCategory.Name = result.Data.Name;
                vatCategory.Description = result.Data.Description;
                vatCategory.DateCreated = result.Data.CreatedAt;
                vatCategory.DateLastUpdated = result.Data.LastUpdated;
                return View(vatCategory);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(vatCategory);
            }
        }

        // GET: VatCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VatCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddVatCategoryViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addVatCategoryRequest = new AddVatCategoryRequest { Name = request.Name, Description = request.Description };
                var result = await _vatCategoryService.Create(addVatCategoryRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Vat Category Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

    // GET: VatCategory/Edit/5
        public async Task<ActionResult> Edit(Guid id)
    {
        var vatCategory = new EditVatCategoryViewModel();
        try
        {
            var result = await _vatCategoryService.FindById(id);
            if (!result.Success)
            {
                Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(vatCategory);
            }
            vatCategory.Id = result.Data.Id;
            vatCategory.Name = result.Data.Name;
            vatCategory.Description = result.Data.Description;
            return View(vatCategory);
        }
        catch (Exception ex)
        {
            Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
            return View(vatCategory);
        }
    }

        // POST: VatCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditVatCategoryViewModel request)
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
                var vatCategoryUpdateRequest = new UpdateVatCategoryRequest { Id = request.Id, Name = request.Name, Description = request.Description };
                var result = await _vatCategoryService.Update(id, vatCategoryUpdateRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Vat Category Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: VatCategory/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var vatCategory = new VatCategoryDetailsViewModel();
            try
            {
                var result = await _vatCategoryService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(vatCategory);
                }
                vatCategory.Code = result.Data.Code;
                vatCategory.Id = result.Data.Id;
                vatCategory.Name = result.Data.Name;
                vatCategory.Description = result.Data.Description;
                vatCategory.DateCreated = result.Data.CreatedAt;
                vatCategory.DateLastUpdated = result.Data.LastUpdated;
                return View(vatCategory);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(vatCategory);
            }
        }

        // POST: VatCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, VatCategoryDetailsViewModel request)
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
                var result = await _vatCategoryService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Vat Category Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        //VAT SECTION
        public async Task<ActionResult> Vats(Guid id)
        {
            var vatList = new List<ListVatViewModel>();
            try
            {
                var result = await _vatCategoryService.FindByIdInclusive(id, x => x.Include(p => p.Vats));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(vatList);
                }

                ViewBag.VatCategory = result.Data;

                if (result.Data.Vats.Count > 0)
                {
                    foreach (var item in result.Data.Vats)
                    {
                        vatList.Add(new ListVatViewModel
                        {
                            Rate = item.Rate,
                            DateCreated = item.CreatedAt,
                            DateLastUpdated = item.LastUpdated,
                            EffectiveDate = item.EffectiveDate,
                            Id = item.Id,
                            EndDate = item.EndDate,
                            VatCategoryName = result.Data.Name
                        });
                    }
                }
                return View(vatList);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(vatList);
            }
        }

        public ActionResult CreateVat(Guid vatCategoryId, string vatCategoryName)
        {
            var vat = new AddVatViewModel { VatCategoryId = vatCategoryId };
            ViewBag.vatCategoryName = vatCategoryName;
            return View(vat);
        }

        // POST: VatCategory/CreateVat
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateVat(AddVatViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Vats), new { id = request.VatCategoryId });
            }
            try
            {
                var addVatRequest = new AddVatRequest { VatCategoryId = request.VatCategoryId, Rate = request.Rate, EffectiveDate = request.EffectiveDate, EndDate = request.EndDate };
                var result = await _vatService.Create(addVatRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(Vats), new { id = request.VatCategoryId });
                }
                Alert($"Vat Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Vats), new { id = request.VatCategoryId });
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Vats), new { id = request.VatCategoryId });
            }
        }

        public async Task<ActionResult> VatDetails(Guid id)
        {
            var vat = new VatDetailsViewModel();
            try
            {
                var result = await _vatService.FindByIdInclusive(id, x => x.Include(p => p.VatCategory));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(vat);
                }
                vat.Rate = result.Data.Rate;
                vat.Id = result.Data.Id;
                vat.EffectiveDate = result.Data.EffectiveDate;
                vat.EndDate = result.Data.EndDate;
                vat.DateCreated = result.Data.CreatedAt;
                vat.DateLastUpdated = result.Data.LastUpdated;
                vat.VatCategoryName = result.Data.VatCategory.Name;
                vat.VatCategoryId = result.Data.VatCategory.Id;
                return View(vat);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(vat);
            }
        }

        public async Task<ActionResult> EditVat(Guid id)
        {
            var vat = new EditVatViewModel();
            try
            {
                var result = await _vatService.FindByIdInclusive(id, x => x.Include(p => p.VatCategory));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(vat);
                }
                vat.Id = result.Data.Id;
                vat.Rate = result.Data.Rate;
                vat.EffectiveDate = result.Data.EffectiveDate;
                vat.EndDate = result.Data.EndDate;
                vat.VatCategoryId = result.Data.VatCategory.Id;
                vat.VatCategoryName = result.Data.VatCategory.Name;
                return View(vat);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(vat);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditVat(Guid id, EditVatViewModel request)
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
                var vatEditRequest = new UpdateVatRequest { Id = request.Id, Rate = request.Rate, EffectiveDate = request.EffectiveDate, EndDate = request.EndDate, VatCategoryId = request.VatCategoryId };
                var result = await _vatService.Update(id, vatEditRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Vat Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Vats), new { id = request.VatCategoryId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        public async Task<ActionResult> DeleteVat(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var vat = new VatDetailsViewModel();
            try
            {
                var result = await _vatService.FindByIdInclusive(id, x => x.Include(p => p.VatCategory));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(vat);
                }
                vat.Rate = result.Data.Rate;
                vat.Id = result.Data.Id;
                vat.EffectiveDate = result.Data.EffectiveDate;
                vat.EndDate = result.Data.EndDate;
                vat.DateCreated = result.Data.CreatedAt;
                vat.DateLastUpdated = result.Data.LastUpdated;
                vat.VatCategoryName = result.Data.VatCategory.Name;
                vat.VatCategoryId = result.Data.VatCategory.Id;
                return View(vat);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(vat);
            }
        }

        // POST: VatCategory/DeleteVat/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteVat(Guid id, VatDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Vats), new { id = request.VatCategoryId });
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Vats), new { id = request.VatCategoryId });
            }
            try
            {
                var result = await _vatService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(Vats), new { id = request.VatCategoryId });
                }
                Alert($"Vat Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Vats), new { id = request.VatCategoryId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Vats), new { id = request.VatCategoryId });
            }
        }
    }
}