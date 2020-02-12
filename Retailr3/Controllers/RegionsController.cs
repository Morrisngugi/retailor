using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Services.EntityServices;
using Core.Utilities;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Retailr3.Models.RegionModels;
using Retailr3.Controllers;
using Serilog;

namespace Retailr3.Controllers
{
    public class RegionsController : BaseController
    {
        private readonly IRegionService _regionService;
        private readonly IOptions<AppConfig> _appConfig;
       

        public RegionsController(IOptions<AppConfig> appConfig, IRegionService regionService)
        {
            _regionService = regionService;
            _appConfig = appConfig;
        }

        // GET: Regions
        public async Task<ActionResult> Index()
        {
            

            List<ListRegionViewModel> regions = new List<ListRegionViewModel>();
            var result = await _regionService.FindAll();

            if (result.Success)
            {
                if (result.Data.Count() > 0)
                {
                    foreach (var region in result.Data)
                    {
                        regions.Add(new ListRegionViewModel
                        {
                            RegionId = region.Id,
                            RegionName = region.Name,
                            RegionCode = region.Code,
                            DateCreated = region.CreatedAt,
                            DateLastUpdated = region.LastUpdated
                        });
                    }
                }
                else
                {
                    Alert("No Regions Found", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                }
                Log.Warning("All Regions were found!");
                return View(regions);
            }
            else
            {
                Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                
                return View(regions);
            }
        }

 


        // GET: Regions/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {

                var result = await _regionService.FindById(id);
                if (result.Success)
                {
                    var regionResource = new ListRegionViewModel
                    {
                        RegionId = result.Data.Id,
                        RegionCode = result.Data.Code,
                        RegionName = result.Data.Name,
                        DateCreated = result.Data.CreatedAt,
                        DateLastUpdated = result.Data.LastUpdated
                    };

                    return View(regionResource);
                }
                else
                {
                    Alert($"Error! : {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

               
            }
            catch (Exception ex)
            {

                Alert($"An Error Occurred While Fetching The Requested Resource. {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            
        }

        // GET: Regions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Regions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddRegionViewModel addRegionVm)
        {
            if (!ModelState.IsValid)
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                // TODO: Add insert logic here
                var addRegionRequest = new AddRegionRequest { Name = addRegionVm.Name, Description = addRegionVm.Description };
                var result = await _regionService.Create(addRegionRequest);
                if (result.Success)
                {
                    Alert($"Region Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Regions/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {

                var result = await _regionService.FindById(id);
                if (result.Success)
                {
                    var regionResource = new EditRegionViewModel
                    {
                        RegionId = result.Data.Id,
                        RegionDescription = result.Data.Description,
                        RegionName = result.Data.Name
                    };

                    return View(regionResource);
                }
                else
                {
                    Alert($"Error! : {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }


            }
            catch (Exception ex)
            {

                Alert($"An Error Occurred While Fetching The Requested Resource. {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // POST: Regions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditRegionViewModel editRegionRequest)
        {
            if (!ModelState.IsValid)
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            if (!id.Equals(editRegionRequest.RegionId))
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                UpdateRegionRequest updateRegionRequest = new UpdateRegionRequest { Id = editRegionRequest.RegionId, Name = editRegionRequest.RegionName, Description = editRegionRequest.RegionDescription };
                var result = await _regionService.Update(id, updateRegionRequest);
                if (result.Success)
                {
                    Alert($"Region Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Regions/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {

                var result = await _regionService.FindById(id);
                if (result.Success)
                {
                    var regionResource = new ListRegionViewModel
                    {
                        RegionId = result.Data.Id,
                        RegionCode = result.Data.Code,
                        RegionDescription = result.Data.Description,
                        RegionName = result.Data.Name,
                        DateCreated = result.Data.CreatedAt,
                        DateLastUpdated = result.Data.LastUpdated
                    };

                    return View(regionResource);
                }
                else
                {
                    Alert($"Error! : {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }


            }
            catch (Exception ex)
            {

                Alert($"An Error Occurred While Fetching The Requested Resource. {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // POST: Regions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, ListRegionViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            if (id== null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var result = await _regionService.Delete(id);
                if (result.Success)
                {
                    Alert($"Region Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }
    }
}