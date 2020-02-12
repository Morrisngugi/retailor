using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Core.Models;
using Core.Models.Requests.EntityRequest;
using Core.Services.EntityServices;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Retailr3.Models.CountryModels;
using Retailr3.Models.RegionModels;

namespace Retailr3.Controllers
{
    public class CountriesController : BaseController
    {
        private readonly ICountryService _countryService;
        private readonly IRegionService _regionService;
        private readonly IOptions<AppConfig> _appConfig;

        public CountriesController(IRegionService regionService, ICountryService countryService, IOptions<AppConfig> appConfig)
        {
            _countryService = countryService;
            _regionService = regionService;
            _appConfig = appConfig;
        }
        // GET: Countries
        public async Task<ActionResult> Index()
        {
            var countries = new List<ListCountryViewModel>();
            try
            {
                var result = await _countryService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(countries);
                }

                if (result.Data.Count > 0)
                {
                    foreach (var item in result.Data)
                    {
                        countries.Add(new ListCountryViewModel
                        {
                            CountryCode = item.Code,
                            CountryDescription = item.Description,
                            CountryId = item.Id,
                            CountryName = item.Name,
                            DateCreated = item.CreatedAt,
                            DateLastUpdated = item.LastUpdated
                        });
                    }
                }
                
                return View(countries);
            }
            catch (Exception ex)
            {
                Alert($"{ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(countries);
            }
            
        }

        // GET: Countries/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var country = new CountryDetailsViewModel();
            try
            {
                var result = await _countryService.FindById(id);

                if(!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(country);
                }

                country.CountryCode = result.Data.Code;
                country.CountryName = result.Data.Name;
                country.CountryDescription = result.Data.Description;
                country.CountryId = result.Data.Id;
                country.DateCreated = result.Data.CreatedAt;
                country.DateLastUpdated = result.Data.LastUpdated;

                return View(country);
            }
            catch (Exception ex)
            {
                Alert($"{ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(country);
            }
            
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddCountryViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addCountryRequest = new AddCountryRequest { Name = request.Name, Description = request.Description };
                var result = await _countryService.Create(addCountryRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Country Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Countries/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var country = new EditCountryViewModel();
            try
            {
                var result = await _countryService.FindById(id);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(country);
                }
                country.CountryId = result.Data.Id;
                country.Name = result.Data.Name;
                country.Description = result.Data.Description;
                return View(country);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(country);
            }
        }

        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditCountryViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(request);
            }
            if (!id.Equals(request.CountryId))
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(request);
            }
            try
            {
                var countryUpdateRequest = new UpdateCountryRequest { Id = request.CountryId, Name = request.Name, Description = request.Description };
                var result = await _countryService.Update(id, countryUpdateRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(request);
                }

                Alert($"Country Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(request);
            }
        }

        // GET: Countries/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var country = new CountryDetailsViewModel();
            try
            {
                var result = await _countryService.FindById(id);

                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(country);
                }

                country.CountryCode = result.Data.Code;
                country.CountryName = result.Data.Name;
                country.CountryDescription = result.Data.Description;
                country.CountryId = result.Data.Id;
                country.DateCreated = result.Data.CreatedAt;
                country.DateLastUpdated = result.Data.LastUpdated;

                return View(country);
            }
            catch (Exception ex)
            {
                Alert($"{ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(country);
            }


        }

        // POST: Countries/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, CountryDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(request);
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(request);
            }
            try
            {
                var result = await _countryService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(request);
                }
                Alert($"Country Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                Alert($"{ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(request);
            }
        }





        //Region  SECTION
        public async Task<ActionResult> Regions(Guid id)
        {
            var regionsList = new List<ListRegionViewModel>();
            try
            {
                var result = await _countryService.FindByIdInclusive(id, x => x.Include(p => p.Regions));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(regionsList);
                }

                ViewBag.Country = result.Data;

                if (result.Data.Regions.Count > 0)
                {
                    foreach (var item in result.Data.Regions)
                    {
                        regionsList.Add(new ListRegionViewModel
                        {
                            CountryName = result.Data.Name,
                            RegionCode = item.Code,
                            RegionDescription = item.Description,
                            RegionId = item.Id,
                            RegionName = item.Name,
                            DateCreated = item.CreatedAt,
                            DateLastUpdated = item.LastUpdated
                        });
                    }
                }
                return View(regionsList);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(regionsList);
            }
        }

        public ActionResult CreateRegion(Guid countryId, string countryName)
        {
            var regionVm = new AddRegionViewModel { CountryId = countryId };
            ViewBag.CountryName = countryName;
            return View(regionVm);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRegion(AddRegionViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Regions), new { id = request.CountryId });
            }
            try
            {
                var addRegionRequest = new AddRegionRequest { CountryId = request.CountryId, Name = request.Name, Description = request.Description };
                var result = await _regionService.Create(addRegionRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(Regions), new { id = request.CountryId });
                }
                Alert($"Region Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Regions), new { id = request.CountryId });
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Regions), new { id = request.CountryId });
            }
        }

        public async Task<ActionResult> RegionDetails(Guid id)
        {
            var region = new RegionDetailsViewModel();
            try
            {
                var result = await _regionService.FindByIdInclusive(id, x => x.Include(p => p.Country));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(region);
                }
                region.RegionCode = result.Data.Code;
                region.RegionId = result.Data.Id;
                region.RegionName = result.Data.Name;
                region.RegionDescription = result.Data.Description;
                region.DateCreated = result.Data.CreatedAt;
                region.DateLastUpdated = result.Data.LastUpdated;
                region.CountryName = result.Data.Country.Name;
                region.CountryId = result.Data.Country.Id;
                return View(region);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(region);
            }
        }

        public async Task<ActionResult> EditRegion(Guid id)
        {
            var region = new EditRegionViewModel();
            try
            {
                var result = await _regionService.FindByIdInclusive(id, x => x.Include(p => p.Country));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(region);
                }
                region.RegionId = result.Data.Id;
                region.RegionName = result.Data.Name;
                region.RegionDescription = result.Data.Description;
                region.CountryId = result.Data.Country.Id;
                region.CountryName = result.Data.Country.Name;
                return View(region);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(region);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRegion(Guid id, EditRegionViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            if (!id.Equals(request.RegionId))
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var regionEditRequest = new UpdateRegionRequest { CountryId = request.CountryId, Description = request.RegionDescription, Name = request.RegionName, Id = request.RegionId };
                var result = await _regionService.Update(id, regionEditRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Unit Of Measure Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Regions), new { id = request.CountryId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        public async Task<ActionResult> DeleteRegion(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var region = new RegionDetailsViewModel();
            try
            {
                var result = await _regionService.FindByIdInclusive(id, x => x.Include(p => p.Country));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(region);
                }
                region.RegionCode = result.Data.Code;
                region.RegionId = result.Data.Id;
                region.RegionName = result.Data.Name;
                region.RegionDescription = result.Data.Description;
                region.DateCreated = result.Data.CreatedAt;
                region.DateLastUpdated = result.Data.LastUpdated;
                region.CountryName = result.Data.Country.Name;
                region.CountryId = result.Data.Country.Id;
                return View(region);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(region);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRegion(Guid id, RegionDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Regions), new { id = request.CountryId });
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Regions), new { id = request.CountryId });
            }
            try
            {
                var result = await _regionService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(Regions), new { id = request.CountryId });
                }
                Alert($"Region {request.RegionName} Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Regions), new { id = request.CountryId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Regions), new { id = request.CountryId });
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetRegions([FromBody] GetRegionsRequest request)
        {
            var regionsList = new List<RegionResource>();
            try
            {
                var result = await _countryService.FindByIdInclusive(request.CountryId, x => x.Include(p => p.Regions));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return Json(regionsList);
                }

                var res = result.Data.Regions;
                foreach (var item in res)
                {
                    regionsList.Add(new RegionResource { Id = item.Id, Name = item.Name });
                }
                return Json(regionsList);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return Json(regionsList);
            }
        }

    }

    
}