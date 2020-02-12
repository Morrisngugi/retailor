using Core.Models;
using Core.Models.Requests;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Retailr3.Models.PromotionDiscount;
using Retailr3.Models.PromotionDiscountItem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Retailr3.Controllers
{
    public class PromotionDiscountsController : BaseController
    {
        private readonly IPromotionDiscountService _promotionDiscountService;
        private readonly IPromotionDiscountItemService _promotionDiscountItemService;
        private readonly IOptions<AppConfig> _appConfig;

        public PromotionDiscountsController(IPromotionDiscountItemService promotionDiscountItemService, IPromotionDiscountService promotionDiscountService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _promotionDiscountService = promotionDiscountService;
            _promotionDiscountItemService = promotionDiscountItemService;
        }

        // GET: PromotionDiscounts
        public async Task<ActionResult> Index()
        {
            var promotionDiscount = new List<ListPromotionDiscountViewModel>();
            try
            {
                var result = await _promotionDiscountService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(promotionDiscount);
                }

                foreach (var pDiscount in result.Data)
                {
                    promotionDiscount.Add(new ListPromotionDiscountViewModel
                    {
                        Id = pDiscount.Id,
                        Code = pDiscount.Code,
                        Description = pDiscount.Description,
                        ProductName = pDiscount.Product.Name,
                        DateCreated = pDiscount.CreatedAt,
                        DateLastUpdated = pDiscount.LastUpdated
                    });
                }

                return View(promotionDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(promotionDiscount);
            }
        }

        // GET: PromotionDiscounts/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var promotionDiscount = new PromotionDiscountDetailsViewModel();
            try
            {
                var result = await _promotionDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(promotionDiscount);
                }
                promotionDiscount.Code = result.Data.Code;
                promotionDiscount.Id = result.Data.Id;
                promotionDiscount.ProductId = result.Data.ProductId;
                promotionDiscount.Description = result.Data.Description;
                promotionDiscount.DateCreated = result.Data.CreatedAt;
                promotionDiscount.DateLastUpdated = result.Data.LastUpdated;
                return View(promotionDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(promotionDiscount);
            }
        }

        // GET: PromotionDiscounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PromotionDiscounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddPromotionDiscountViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addPromotionRequest = new AddPromotionDiscountRequest { ProductId = request.ProductId, Description = request.Description };
                var result = await _promotionDiscountService.Create(addPromotionRequest);
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

        // GET: PromotionDiscounts/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var productDiscount = new EditPromotionDiscountViewModel();
            try
            {
                var result = await _promotionDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(productDiscount);
                }
                productDiscount.Id = result.Data.Id;
                productDiscount.ProductId = result.Data.ProductId;
                productDiscount.Description = result.Data.Description;
                return View(productDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(productDiscount);
            }
        }

        // POST: PromotionDiscounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditPromotionDiscountViewModel request)
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
                var promotionDiscountUpdateRequest = new UpdatePromotionDiscountRequest { Id = request.Id, ProductId = request.ProductId, Description = request.Description };
                var result = await _promotionDiscountService.Update(id, promotionDiscountUpdateRequest);
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

        // GET: PromotionDiscounts/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var PromotionDiscount = new PromotionDiscountDetailsViewModel();
            try
            {
                var result = await _promotionDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(PromotionDiscount);
                }
                PromotionDiscount.Code = result.Data.Code;
                PromotionDiscount.Id = result.Data.Id;
                PromotionDiscount.ProductId = result.Data.ProductId;
                PromotionDiscount.Description = result.Data.Description;
                PromotionDiscount.DateCreated = result.Data.CreatedAt;
                PromotionDiscount.DateLastUpdated = result.Data.LastUpdated;
                return View(PromotionDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(PromotionDiscount);
            }
        }

        // POST: PromotionDiscounts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, PromotionDiscountDetailsViewModel request)
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
                var result = await _promotionDiscountService.Delete(id);
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

        //PROMOTION DISCOUNT ITEM SECTION
        public async Task<ActionResult> PromotionDiscountItems(Guid id)
        {
            var promotionDiscountList = new List<ListPromotionDiscountItemViewModel>();
            try
            {
                var result = await _promotionDiscountService.FindByIdInclusive(id, x => x.Include(p => p.PromotionDiscountItems));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(promotionDiscountList);
                }

                ViewBag.PromotionDiscount = result.Data;

                if (result.Data.PromotionDiscountItems.Count > 0)
                {
                    foreach (var item in result.Data.PromotionDiscountItems)
                    {
                        promotionDiscountList.Add(new ListPromotionDiscountItemViewModel
                        {
                            ParentProductQuantity = item.ParentProductQuantity,
                            DiscountRate = item.DiscountRate,
                            EffectiveDate = item.EffectiveDate,
                            EndDate = item.EndDate,
                            DateCreated = item.CreatedAt,
                            DateLastUpdated = item.LastUpdated,
                            FreeOfChargeQuantity = item.FreeOfChargeQuantity,
                            Id = item.Id,
                            ProductName = result.Data.Product.Name,
                            // PromotionDiscountId = item.PromotionDiscountId
                        });
                    }
                }
                return View(promotionDiscountList);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(promotionDiscountList);
            }
        }
        public ActionResult CreateSaleValueDiscountItem(Guid promotionDiscountId, string promotionDiscountName)
        {
            var promotionDiscount = new AddPromotionDiscountItemViewModel { PromotionDiscountId = promotionDiscountId };
            ViewBag.PromotionDiscountName = promotionDiscountName;
            return View(promotionDiscount);
        }

        // POST: PromotionDiscount/CreatePromotionDiscountItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePromotionDiscountItem(AddPromotionDiscountItemViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PromotionDiscountItems), new { id = request.PromotionDiscountId });
            }
            try
            {
                var addPromotionDiscountRequest = new AddPromotionDiscountItemRequest { PromotionDiscountId = request.PromotionDiscountId, DiscountRate = request.DiscountRate, FreeOfChargeQuantity = request.FreeOfChargeQuantity, ParentProductQuantity = request.ParentProductQuantity, EffectiveDate = request.EffectiveDate, EndDate = request.EndDate };
                var result = await _promotionDiscountItemService.Create(addPromotionDiscountRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(PromotionDiscountItems), new { id = request.PromotionDiscountId });
                }
                Alert($"Discount Item Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PromotionDiscountItems), new { id = request.PromotionDiscountId });
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PromotionDiscountItems), new { id = request.PromotionDiscountId });
            }
        }

        public async Task<ActionResult> PromotionDiscountItemDetails(Guid id)
        {
            var promotionDiscountItem = new ListPromotionDiscountItemViewModel();
            try
            {
                var result = await _promotionDiscountItemService.FindByIdInclusive(id, x => x.Include(p => p.PromotionDiscount));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(promotionDiscountItem);
                }
                promotionDiscountItem.ParentProductQuantity = result.Data.ParentProductQuantity;
                promotionDiscountItem.FreeOfChargeQuantity = result.Data.FreeOfChargeQuantity;
                promotionDiscountItem.Id = result.Data.Id;
                promotionDiscountItem.DiscountRate = result.Data.DiscountRate;
                promotionDiscountItem.DateCreated = result.Data.CreatedAt;
                promotionDiscountItem.DateLastUpdated = result.Data.LastUpdated;
                promotionDiscountItem.ProductName = result.Data.PromotionDiscount.Product.Name;
                promotionDiscountItem.PromotionDiscountId = result.Data.PromotionDiscountId;
                return View(promotionDiscountItem);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(promotionDiscountItem);
            }
        }

        public async Task<ActionResult> EditPromotionDiscountItem(Guid id)
        {
            var promotionDiscountItem = new EditPromotionDiscountItemViewModel();
            try
            {
                var result = await _promotionDiscountItemService.FindByIdInclusive(id, x => x.Include(p => p.PromotionDiscount));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(promotionDiscountItem);
                }
                promotionDiscountItem.Id = result.Data.Id;
                promotionDiscountItem.ParentProductQuantity = result.Data.ParentProductQuantity;
                promotionDiscountItem.FreeOfChargeQuantity  = result.Data.FreeOfChargeQuantity;
                promotionDiscountItem.EffectiveDate = result.Data.EffectiveDate;
                promotionDiscountItem.EndDate = result.Data.EndDate;
                promotionDiscountItem.DiscountRate = result.Data.DiscountRate;
                promotionDiscountItem.PromotionDiscountId = result.Data.PromotionDiscount.Id;
                return View(promotionDiscountItem);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(promotionDiscountItem);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPromotionDiscountItem(Guid id, EditPromotionDiscountItemViewModel request)
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
                var promotionDiscountEditRequest = new UpdatePromotionDiscountItemRequest { Id = request.Id, PromotionDiscountId= request.PromotionDiscountId, ParentProductQuantity = request.ParentProductQuantity, DiscountRate = request.DiscountRate, FreeOfChargeQuantity  = request.FreeOfChargeQuantity, EffectiveDate = request.EffectiveDate, EndDate = request.EndDate };
                var result = await _promotionDiscountItemService.Update(id, promotionDiscountEditRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Discount Details Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PromotionDiscountItemDetails), new { id = request.PromotionDiscountId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        public async Task<ActionResult> DeletePromotionDiscountItem(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var promotionDiscountItem = new PromotionDiscountItemDetailsViewModel();
            try
            {
                var result = await _promotionDiscountItemService.FindByIdInclusive(id, x => x.Include(p => p.PromotionDiscount));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(promotionDiscountItem);
                }
                promotionDiscountItem.ParentProductQuantity = result.Data.ParentProductQuantity;
                promotionDiscountItem.FreeOfChargeQuantity  = result.Data.FreeOfChargeQuantity;
                promotionDiscountItem.Id = result.Data.Id;
                promotionDiscountItem.DiscountRate = result.Data.DiscountRate;
                promotionDiscountItem.EffectiveDate = result.Data.EffectiveDate;
                promotionDiscountItem.EndDate = result.Data.EndDate;
                promotionDiscountItem.DateCreated = result.Data.CreatedAt;
                promotionDiscountItem.DateLastUpdated = result.Data.LastUpdated;
                promotionDiscountItem.ProductName = result.Data.PromotionDiscount.Product.Name;
                promotionDiscountItem.PromotionDiscountId = result.Data.PromotionDiscountId;
                return View(promotionDiscountItem);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(promotionDiscountItem);
            }
        }

        // POST: PromotionDiscount/DeletePromotionDiscountItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePromotionDiscountItem(Guid id, PromotionDiscountItemDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PromotionDiscountItemDetails), new { id = request.PromotionDiscountId });
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PromotionDiscountItemDetails), new { id = request.PromotionDiscountId });
            }
            try
            {
                var result = await _promotionDiscountItemService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(PromotionDiscountItemDetails), new { id = request.PromotionDiscountId });
                }
                Alert($"Discount Item Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PromotionDiscountItemDetails), new { id = request.PromotionDiscountId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PromotionDiscountItemDetails), new { id = request.PromotionDiscountId });
            }
        }
    }
}