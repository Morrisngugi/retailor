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
using Microsoft.Extensions.Options;
using Retailr3.Controllers;
using Retailr3.Models.Tier;

namespace Retailr3.Controllers
{
    public class TiersController : BaseController
    {
        private readonly ITierService _tierService;
        private readonly IOptions<AppConfig> _appConfig;

        public TiersController(ITierService tierService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _tierService = tierService;
        }

        // GET: Tier
        public async Task<ActionResult> Index()
        {
            var tiers = new List<ListTierViewModel>();
            try
            {
                var result = await _tierService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(tiers);
                }

                foreach (var tier in result.Data)
                {
                    tiers.Add(new ListTierViewModel
                    {
                        Id = tier.Id,
                        Code = tier.Code,
                        Description = tier.Description,
                        Name = tier.Name,
                        DateCreated = tier.CreatedAt,
                        DateLastUpdated = tier.LastUpdated
                    });
                }

                return View(tiers);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(tiers);
            }
            
        }

        // GET: Tier/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var tier = new TierDetailsViewModel();
            try
            {
                var result = await _tierService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(tier);
                }
                tier.Code = result.Data.Code;
                tier.Id = result.Data.Id;
                tier.Name = result.Data.Name;
                tier.Description = result.Data.Description;
                tier.DateCreated = result.Data.CreatedAt;
                tier.DateLastUpdated = result.Data.LastUpdated;
                return View(tier);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(tier);
            }
            
        }

        // GET: Tier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddTierViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addTierRequest = new AddTierRequest {Name = request.Name, Description = request.Description };
                var result = await _tierService.Create(addTierRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Tier Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Tier/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {

            var tier = new EditTierViewModel();
            try
            {
                var result = await _tierService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(tier);
                }
                tier.Id = result.Data.Id;
                tier.Name = result.Data.Name;
                tier.Description = result.Data.Description;
                return View(tier);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(tier);
            }
        }

        // POST: Tier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditTierViewModel request)
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
                var tierUpdateRequest = new UpdateTierRequest { Id = request.Id, Name = request.Name, Description = request.Description };
                var result = await _tierService.Update(id, tierUpdateRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Tier Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Tier/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var tier = new TierDetailsViewModel();
            try
            {
                var result = await _tierService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(tier);
                }
                tier.Code = result.Data.Code;
                tier.Id = result.Data.Id;
                tier.Name = result.Data.Name;
                tier.Description = result.Data.Description;
                tier.DateCreated = result.Data.CreatedAt;
                tier.DateLastUpdated = result.Data.LastUpdated;
                return View(tier);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(tier);
            }
        }

        // POST: Tier/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, TierDetailsViewModel request)
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
                var result = await _tierService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Tier Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
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