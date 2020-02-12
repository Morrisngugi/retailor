using Core.Models;
using Core.Models.Requests;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Retailr3.Models.CertainValueCertainProductDiscount;
using Retailr3.Models.CertainValueCertainProductDiscountItem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Retailr3.Controllers
{
    public class CertainValueCertainProductDiscountController : BaseController
    {
        private readonly ICertainValueCertainProductDiscountService _certainValueCertainProductDiscountService;
        private readonly ICertainValueCertainProductDiscountItemService _certainValueCertainProductDiscountItemService;
        private readonly IOptions<AppConfig> _appConfig;

        public CertainValueCertainProductDiscountController(ICertainValueCertainProductDiscountItemService certainValueCertainProductDiscountItemService, ICertainValueCertainProductDiscountService certainValueCertainProductDiscountService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _certainValueCertainProductDiscountService = certainValueCertainProductDiscountService;
            _certainValueCertainProductDiscountItemService = certainValueCertainProductDiscountItemService;
        }

        // GET: CertainValueCertainProductDiscount
        public async Task<ActionResult> Index()
        {
            var cvDiscount = new List<ListCertainValueCertainProductDiscountViewModel>();
            try
            {
                var result = await _certainValueCertainProductDiscountService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(cvDiscount);
                }

                foreach (var cvcpDiscount in result.Data)
                {
                    cvDiscount.Add(new ListCertainValueCertainProductDiscountViewModel
                    {
                        Id = cvcpDiscount.Id,
                        Code = cvcpDiscount.Code,
                        Description = cvcpDiscount.Description,
                        Name = cvcpDiscount.Name,
                        DateCreated = cvcpDiscount.CreatedAt,
                        DateLastUpdated = cvcpDiscount.LastUpdated
                    });
                }

                return View(cvDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(cvDiscount);
            }
        }

        // GET: CertainValueCertainProductDiscount/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var cvDiscount = new CertainValueCertainProductDiscountDetailsViewModel();
            try
            {
                var result = await _certainValueCertainProductDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(cvDiscount);
                }
                cvDiscount.Code = result.Data.Code;
                cvDiscount.Id = result.Data.Id;
                cvDiscount.Name = result.Data.Name;
                cvDiscount.Description = result.Data.Description;
                cvDiscount.DateCreated = result.Data.CreatedAt;
                cvDiscount.DateLastUpdated = result.Data.LastUpdated;
                return View(cvDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(cvDiscount);
            }
        }

        // GET: CertainValueCertainProductDiscount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CertainValueCertainProductDiscount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddCertainValueCertainProductDiscountViewModel request)
        {

            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addcvDiscountRequest = new AddCertainValueCertainProductDiscount { Name = request.Name, Description = request.Description };
                var result = await _certainValueCertainProductDiscountService.Create(addcvDiscountRequest);
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

        // GET: CertainValueCertainProductDiscount/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var cvDiscount = new EditCertainValueCertainProductDiscountViewModel();
            try
            {
                var result = await _certainValueCertainProductDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(cvDiscount);
                }
                cvDiscount.Id = result.Data.Id;
                cvDiscount.Name = result.Data.Name;
                cvDiscount.Description = result.Data.Description;
                return View(cvDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(cvDiscount);
            }
        }

        // POST: CertainValueCertainProductDiscount/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditCertainValueCertainProductDiscountViewModel request)
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
                var cvDiscountUpdateRequest = new UpdateCertainValueCertainProductDiscountRequest { Id = request.Id, Name = request.Name, Description = request.Description };
                var result = await _certainValueCertainProductDiscountService.Update(id, cvDiscountUpdateRequest);
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

        // GET: CertainValueCertainProductDiscount/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var cvDiscount = new CertainValueCertainProductDiscountDetailsViewModel();
            try
            {
                var result = await _certainValueCertainProductDiscountService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(cvDiscount);
                }
                cvDiscount.Code = result.Data.Code;
                cvDiscount.Id = result.Data.Id;
                cvDiscount.Name = result.Data.Name;
                cvDiscount.Description = result.Data.Description;
                cvDiscount.DateCreated = result.Data.CreatedAt;
                cvDiscount.DateLastUpdated = result.Data.LastUpdated;
                return View(cvDiscount);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(cvDiscount);
            }
        }

        // POST: CertainValueCertainProductDiscount/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, CertainValueCertainProductDiscountDetailsViewModel request)
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
                var result = await _certainValueCertainProductDiscountService.Delete(id);
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

        //DISCOUNT ITEMS SECTION
        public async Task<ActionResult> CertainValueCertainProductDiscountItems(Guid id)
        {
            var cvDiscountItemList = new List<ListCertainValueCertainProductDiscountItemViewModel>();
            try
            {
                var result = await _certainValueCertainProductDiscountService.FindByIdInclusive(id, x => x.Include(p => p.CertainValueCertainProductDiscountItems));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(cvDiscountItemList);
                }

                ViewBag.CertainValueDiscount = result.Data;

                if (result.Data.CertainValueCertainProductDiscountItems.Count > 0)
                {
                    foreach (var item in result.Data.CertainValueCertainProductDiscountItems)
                    {
                        cvDiscountItemList.Add(new ListCertainValueCertainProductDiscountItemViewModel
                        {
                            ProductName = item.Product.Name,
                            DateCreated = item.CreatedAt,
                            DateLastUpdated = item.LastUpdated,
                            Description = item.Description,
                            Value = item.Value,
                            Quantity = item.Quantity,
                            EffectiveDate = item.EffectiveDate,
                            EndDate = item.EndDate,
                            Id = item.Id
                        });
                    }
                }
                return View(cvDiscountItemList);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(cvDiscountItemList);
            }
        }

        public ActionResult CreateCertainValueCertainProductDiscountItem(Guid certainValuediscountId, string certainValuediscountName)
        {
            var cvDiscountItem = new AddCertainValueCertainProductDiscountItemViewModel { CertainValueCertainProductDiscountId = certainValuediscountId };
            ViewBag.ProductName = certainValuediscountName;
            return View(cvDiscountItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCertainValueCertainProductDiscountItem(AddCertainValueCertainProductDiscountItemViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(CertainValueCertainProductDiscountItems), new { id = request.CertainValueCertainProductDiscountId });
            }
            try
            {
                var addcvDiscountItemRequest = new AddCertainValueCertainProductDiscountItem { CertainValueCertainProductDiscountId = request.CertainValueCertainProductDiscountId, Value = request.Value,  Quantity = request.Quantity, EffectiveDate =request.EffectiveDate, EndDate =request.EffectiveDate, ProductId = request.ProductId };
                var result = await _certainValueCertainProductDiscountItemService.Create(addcvDiscountItemRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(CertainValueCertainProductDiscountItems), new { id = request.CertainValueCertainProductDiscountId });
                }
                Alert($"Discount Item Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(CertainValueCertainProductDiscountItems), new { id = request.CertainValueCertainProductDiscountId });
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(CertainValueCertainProductDiscountItems), new { id = request.CertainValueCertainProductDiscountId });
            }
        }

        public async Task<ActionResult> CertainValueCertainProductDiscountItemDetails(Guid id)
        {
            var cvDiscountItem = new CertainValueCertainProductDiscountItemDetailsViewModel();
            try
            {
                var result = await _certainValueCertainProductDiscountItemService.FindByIdInclusive(id, x => x.Include(p => p.CertainValueCertainProductDiscount));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(cvDiscountItem);
                }
                cvDiscountItem.Value = result.Data.Value;
                cvDiscountItem.Id = result.Data.Id;
                cvDiscountItem.Quantity = result.Data.Quantity;
                cvDiscountItem.Description = result.Data.Description;
                cvDiscountItem.EffectiveDate  = result.Data.EffectiveDate;
                cvDiscountItem.EndDate = result.Data.EndDate;
                cvDiscountItem.DateCreated = result.Data.CreatedAt;
                cvDiscountItem.DateLastUpdated = result.Data.LastUpdated;
                cvDiscountItem.ProductId = result.Data.ProductId;
                cvDiscountItem.ProductName = result.Data.Product.Name;
                return View(cvDiscountItem);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(cvDiscountItem);
            }
        }

        public async Task<ActionResult> EditCertainValueCertainProductDiscountItem(Guid id)
        {
            var cvDiscountItem = new EditCertainValueCertainProductDiscountItemViewModel();
            try
            {
                var result = await _certainValueCertainProductDiscountItemService.FindByIdInclusive(id, x => x.Include(p => p.CertainValueCertainProductDiscount));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(cvDiscountItem);
                }
                cvDiscountItem.Id = result.Data.Id;
                cvDiscountItem.Value = result.Data.Value;
                cvDiscountItem.Quantity = result.Data.Quantity;
                cvDiscountItem.EffectiveDate = result.Data.EffectiveDate;
                cvDiscountItem.EndDate = result.Data.EndDate;
                cvDiscountItem.CertainValueCertainProductDiscountId = result.Data.CertainValueCertainProductDiscount.Id;
                cvDiscountItem.ProductId = result.Data.ProductId;
                return View(cvDiscountItem);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(cvDiscountItem);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCertainValueCertainProductDiscountItem(Guid id, EditCertainValueCertainProductDiscountItemViewModel request)
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
                var cvDiscountItemEditRequest = new UpdateCertainValueCertainProductDiscountItemRequest { Id = request.Id, Value = request.Value, Quantity = request.Quantity, CertainValueCertainProductDiscountId = request.CertainValueCertainProductDiscountId, EffectiveDate =request.EffectiveDate, EndDate = request.EndDate, ProductId = request.ProductId };
                var result = await _certainValueCertainProductDiscountItemService.Update(id, cvDiscountItemEditRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Discount Item Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(CertainValueCertainProductDiscountItemDetails), new { id = request.CertainValueCertainProductDiscountId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        public async Task<ActionResult> DeleteCertainValueCertainProductDiscountItem(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var cvDiscountItem = new CertainValueCertainProductDiscountItemDetailsViewModel();
            try
            {
                var result = await _certainValueCertainProductDiscountItemService.FindByIdInclusive(id, x => x.Include(p => p.CertainValueCertainProductDiscount));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(cvDiscountItem);
                }
                cvDiscountItem.Value = result.Data.Value;
                cvDiscountItem.Id = result.Data.Id;
                cvDiscountItem.Quantity = result.Data.Quantity;
                cvDiscountItem.Description = result.Data.Description;
                cvDiscountItem.EffectiveDate = result.Data.EffectiveDate;
                cvDiscountItem.EndDate = result.Data.EndDate;
                cvDiscountItem.DateCreated = result.Data.CreatedAt;
                cvDiscountItem.DateLastUpdated = result.Data.LastUpdated;
                cvDiscountItem.ProductId = result.Data.ProductId;
                cvDiscountItem.ProductName = result.Data.Product.Name;
                return View(cvDiscountItem);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(cvDiscountItem);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCertainValueCertainProductDiscountItem(Guid id, CertainValueCertainProductDiscountItemDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(CertainValueCertainProductDiscountItemDetails), new { id = request.CertainValueCertainProductDiscountId });
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(CertainValueCertainProductDiscountItemDetails), new { id = request.CertainValueCertainProductDiscountId });
            }
            try
            {
                var result = await _certainValueCertainProductDiscountItemService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(CertainValueCertainProductDiscountItemDetails), new { id = request.CertainValueCertainProductDiscountId });
                }
                Alert($"Sub Brand Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(CertainValueCertainProductDiscountItemDetails), new { id = request.CertainValueCertainProductDiscountId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(CertainValueCertainProductDiscountItemDetails), new { id = request.CertainValueCertainProductDiscountId });
            }
        }
    }
}