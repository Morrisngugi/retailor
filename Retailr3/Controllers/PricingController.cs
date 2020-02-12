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
using Retailr3.Models.Pricing;

namespace Retailr3.Controllers
{
    public class PricingController : BaseController
    {
        private readonly IPricingService _pricingService;
        private readonly IOptions<AppConfig> _appConfig;
        public PricingController(IPricingService pricingService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _pricingService = pricingService;
           
        }

        // GET: Pricing
        public async Task<ActionResult> Index()
        {
            var pricing = new List<ListPricingViewModel>();
            try
            {
                var result = await _pricingService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(pricing);
                }

                foreach (var price in result.Data)
                {
                    pricing.Add(new ListPricingViewModel
                    {
                        Id = price.Id,
                        Code = price.Code,
                        Tier = price.Tier.Name,
                        Product = price.Product.Name,
                        DateCreated = price.CreatedAt,
                        DateLastUpdated = price.LastUpdated
                    });
                }

                return View(pricing);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(pricing);
            }
        }

        // GET: Pricing/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var price = new PricingDetailsViewModel();
            try
            {
                var result = await _pricingService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(price);
                }
                price.Code = result.Data.Code;
                price.Id = result.Data.Id;
                price.Price = result.Data.Price;
                price.Product = result.Data.Product.Name;
                price.Tier = result.Data.Tier.Name;
                price.DateCreated = result.Data.CreatedAt;
                price.DateLastUpdated = result.Data.LastUpdated;
                return View(price);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(price);
            }
        }

        // GET: Pricing/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pricing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddPricingViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addPricingRequest = new AddPricingRequest { Price = request.Price, TierId = request.TierId, ProductId = request.TierId };
                var result = await _pricingService.Create(addPricingRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Pricing Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Pricing/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var price = new EditPricingViewModel();
            try
            {
                var result = await _pricingService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(price);
                }
                price.Id = result.Data.Id;
                price.Tier = result.Data.Tier.Name;
                price.Product = result.Data.Product.Name;
                return View(price);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(price);
            }
        }

        // POST: Pricing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditPricingViewModel request)
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
                var priceUpdateRequest = new UpdatePricingRequest { Id = request.Id, Price = request.Price, ProductId = request.ProuctId };
                var result = await _pricingService.Update(id, priceUpdateRequest);
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

        // GET: Pricing/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var price = new PricingDetailsViewModel();
            try
            {
                var result = await _pricingService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(price);
                }
                price.Code = result.Data.Code;
                price.Id = result.Data.Id;
                price.Price = result.Data.Price;
                price.Product = result.Data.Product.Name;
                price.Tier = result.Data.Tier.Name;
                price.DateCreated = result.Data.CreatedAt;
                price.DateLastUpdated = result.Data.LastUpdated;
                return View(price);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(price);
            }
        }

        // POST: Pricing/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, PricingDetailsViewModel request)
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
                var result = await _pricingService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Pricing Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
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