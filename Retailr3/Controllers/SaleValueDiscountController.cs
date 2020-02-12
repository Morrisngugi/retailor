using Core.Models;
using Core.Models.Requests;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Retailr3.Models.SaleValueDiscount;
using Retailr3.Models.SaleValueDiscountItem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Retailr3.Controllers
{
    public class SaleValueDiscountController : BaseController
    {
        private readonly ISaleValueDiscountService _saleValueDiscountService;
        private readonly ISaleValueDiscountItemService _saleValueDiscountItemService;
        private readonly IOptions<AppConfig> _appConfig;

        public SaleValueDiscountController(ISaleValueDiscountItemService saleValueDiscountItemService, ISaleValueDiscountService saleValueDiscountService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _saleValueDiscountService = saleValueDiscountService;
            _saleValueDiscountItemService = saleValueDiscountItemService;
        }



        // GET: SaleValueDiscount
        public async Task<ActionResult> Index()
        {

            var saleValueDiscount = new List<ListSaleValueDiscountViewModel>();
            try
            {
                var result = await _saleValueDiscountService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(saleValueDiscount);
                }

                foreach (var saleValue in result.Data)
                {
                    saleValueDiscount.Add(new ListSaleValueDiscountViewModel
                    {
                        Id = saleValue.Id,
                        Code = saleValue.Code,
                        Description = saleValue.Description,
                        Tier = saleValue.Tier.Name,
                        DateCreated = saleValue.CreatedAt,
                        DateLastUpdated = saleValue.LastUpdated
                    });
                }

                return View(saleValueDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(saleValueDiscount);
            }
        }

        // GET: SaleValueDiscount/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var saleValueDiscount = new SaleValueDiscountDetailsViewModel();
            try
            {
                var result = await _saleValueDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(saleValueDiscount);
                }
                saleValueDiscount.Code = result.Data.Code;
                saleValueDiscount.Id = result.Data.Id;
                saleValueDiscount.TierId = result.Data.TierId;
                saleValueDiscount.Description = result.Data.Description;
                saleValueDiscount.DateCreated = result.Data.CreatedAt;
                saleValueDiscount.DateLastUpdated = result.Data.LastUpdated;
                return View(saleValueDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(saleValueDiscount);
            }
        }

        // GET: SaleValueDiscount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleValueDiscount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddSaleValueDiscountViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addSaleValueRequest = new AddSaleValueDiscountRequest { TierId = request.TierId, Description = request.Description };
                var result = await _saleValueDiscountService.Create(addSaleValueRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Discount Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: SaleValueDiscount/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var saleValueDiscount = new EditSaleValueDiscountViewModel();
            try
            {
                var result = await _saleValueDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(saleValueDiscount);
                }
                saleValueDiscount.Id = result.Data.Id;
                saleValueDiscount.TierId = result.Data.TierId;
                saleValueDiscount.Description = result.Data.Description;
                return View(saleValueDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(saleValueDiscount);
            }
        }

        // POST: SaleValueDiscount/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit(Guid id, EditSaleValueDiscountViewModel request)
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
                var saleValueDiscountUpdateRequest = new UpdateSaleValueDiscountRequest { Id = request.Id, TierId = request.TierId, Description = request.Description };
                var result = await _saleValueDiscountService.Update(id, saleValueDiscountUpdateRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Discount Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: SaleValueDiscount/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var saleValueDiscount = new SaleValueDiscountDetailsViewModel();
            try
            {
                var result = await _saleValueDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(saleValueDiscount);
                }
                saleValueDiscount.Code = result.Data.Code;
                saleValueDiscount.Id = result.Data.Id;
                saleValueDiscount.TierId = result.Data.TierId;
                saleValueDiscount.Description = result.Data.Description;
                saleValueDiscount.DateCreated = result.Data.CreatedAt;
                saleValueDiscount.DateLastUpdated = result.Data.LastUpdated;
                return View(saleValueDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(saleValueDiscount);
            }
        }

        // POST: SaleValueDiscount/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, SaleValueDiscountDetailsViewModel request)
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
                var result = await _saleValueDiscountService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Discount Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        //SALE VALUE DISCOUNT ITEM SECTION
        public async Task<ActionResult> SaleValueDiscountItems(Guid id)
        {
            var saleValueDiscountList = new List<ListSaleValueDiscountItemViewModel>();
            try
            {
                var result = await _saleValueDiscountService.FindByIdInclusive(id, x => x.Include(p => p.SaleValueDiscountItems));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(saleValueDiscountList);
                }

                ViewBag.SaleValueDiscount = result.Data;

                if (result.Data.SaleValueDiscountItems.Count > 0)
                {
                    foreach (var item in result.Data.SaleValueDiscountItems)
                    {
                        saleValueDiscountList.Add(new ListSaleValueDiscountItemViewModel
                        {
                            SaleValue = item.SaleValue,
                            DiscountRate = item.DiscountRate,
                            EffectiveDate = item.EffectiveDate,
                            EndDate = item.EndDate,
                            DateCreated = item.CreatedAt,
                            DateLastUpdated = item.LastUpdated,
                            Description = item.SaleValueDiscount.Description,
                            Id = item.Id,
                            TierName = result.Data.Tier.Name
                        });
                    }
                }
                return View(saleValueDiscountList);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(saleValueDiscountList);
            }
        }

        public ActionResult CreateSaleValueDiscountItem(Guid saleValueDiscountId, string saleValueDiscountName)
        {
            var saleValueDiscount = new AddSaleValueDiscountItemViewModel { SaleValueDiscountId = saleValueDiscountId };
            ViewBag.SaleValueDiscountName = saleValueDiscountName;
            return View(saleValueDiscount);
        }

        // POST: SaleValueDiscount/CreateSaleValueDiscountItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSaleValueDiscountItem(AddSaleValueDiscountItemViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SaleValueDiscountItems), new { id = request.SaleValueDiscountId });
            }
            try
            {
                var addSubBrandRequest = new AddSaleValueDiscountItemRequest { SaleValueDiscountId = request.SaleValueDiscountId, DiscountRate = request.DiscountRate, Description = request.Description, SaleValue = request.SaleValue, EffectiveDate = request.EffectiveDate, EndDate = request.EndDate };
                var result = await _saleValueDiscountItemService.Create(addSubBrandRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(SaleValueDiscountItems), new { id = request.SaleValueDiscountId });
                }
                Alert($"Discount Item Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SaleValueDiscountItems), new { id = request.SaleValueDiscountId });
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SaleValueDiscountItems), new { id = request.SaleValueDiscountId });
            }
        }

        public async Task<ActionResult> SaleValueDiscountItemDetails(Guid id)
        {
            var saleValueDiscountItem = new SaleValueDiscountItemDetailsViewModel();
            try
            {
                var result = await _saleValueDiscountItemService.FindByIdInclusive(id, x => x.Include(p => p.SaleValueDiscount));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(saleValueDiscountItem);
                }
                saleValueDiscountItem.SaleValue = result.Data.SaleValue;
                saleValueDiscountItem.Id = result.Data.Id;
                saleValueDiscountItem.DiscountRate = result.Data.DiscountRate;
                saleValueDiscountItem.EffectiveDate = result.Data.EffectiveDate;
                saleValueDiscountItem.EndDate = result.Data.EndDate;
                saleValueDiscountItem.DateCreated = result.Data.CreatedAt;
                saleValueDiscountItem.DateLastUpdated = result.Data.LastUpdated;
                saleValueDiscountItem.Tier = result.Data.SaleValueDiscount.Tier.Name;
                saleValueDiscountItem.SaleValueDiscountId = result.Data.SaleValueDiscount.Id;
                return View(saleValueDiscountItem);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(saleValueDiscountItem);
            }
        }

        public async Task<ActionResult> EditSaleValueDiscountItem(Guid id)
        {
            var saleValueDiscountItem = new EditSaleValueDiscountItemViewModel();
            try
            {
                var result = await _saleValueDiscountItemService.FindByIdInclusive(id, x => x.Include(p => p.SaleValueDiscount));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(saleValueDiscountItem);
                }
                saleValueDiscountItem.Id = result.Data.Id;
                saleValueDiscountItem.SaleValue = result.Data.SaleValue;
                saleValueDiscountItem.DiscountRate = result.Data.DiscountRate;
                saleValueDiscountItem.SaleValueDiscountId = result.Data.SaleValueDiscount.Id;
                saleValueDiscountItem.Tier = result.Data.SaleValueDiscount.Tier.Name;
                return View(saleValueDiscountItem);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(saleValueDiscountItem);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSaleValueDiscountItem(Guid id, EditSaleValueDiscountItemViewModel request)
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
                var saleValueDiscountEditRequest = new UpdateSaleValueDiscountItemRequest { Id = request.Id, SaleValue = request.SaleValue, DiscountRate  = request.DiscountRate, SaleValueDiscountId = request.SaleValueDiscountId, EffectiveDate = request.EffectiveDate, EndDate = request.EndDate };
                var result = await _saleValueDiscountItemService.Update(id, saleValueDiscountEditRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Discount Details Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SaleValueDiscountItems), new { id = request.SaleValueDiscountId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        public async Task<ActionResult> DeleteSaleValueDiscountItem(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var saleValueDiscountItem = new SaleValueDiscountItemDetailsViewModel();
            try
            {
                var result = await _saleValueDiscountItemService.FindByIdInclusive(id, x => x.Include(p => p.SaleValueDiscount));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(saleValueDiscountItem);
                }
                saleValueDiscountItem.SaleValue = result.Data.SaleValue;
                saleValueDiscountItem.Id = result.Data.Id;
                saleValueDiscountItem.DiscountRate = result.Data.DiscountRate;
                saleValueDiscountItem.EffectiveDate = result.Data.EffectiveDate;
                saleValueDiscountItem.EndDate = result.Data.EndDate;
                saleValueDiscountItem.DateCreated = result.Data.CreatedAt;
                saleValueDiscountItem.DateLastUpdated = result.Data.LastUpdated;
                saleValueDiscountItem.Tier = result.Data.SaleValueDiscount.Tier.Name;
                saleValueDiscountItem.SaleValueDiscountId = result.Data.SaleValueDiscount.Id;
                return View(saleValueDiscountItem);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(saleValueDiscountItem);
            }
        }

        // POST: Brand/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSaleValueDiscountItem(Guid id, SaleValueDiscountItemDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SaleValueDiscountItemDetails), new { id = request.SaleValueDiscountId });
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SaleValueDiscountItemDetails), new { id = request.SaleValueDiscountId });
            }
            try
            {
                var result = await _saleValueDiscountItemService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(SaleValueDiscountItemDetails), new { id = request.SaleValueDiscountId });
                }
                Alert($"Discount Item Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SaleValueDiscountItemDetails), new { id = request.SaleValueDiscountId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(SaleValueDiscountItemDetails), new { id = request.SaleValueDiscountId });
            }
        }
    }
}