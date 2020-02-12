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
using Retailr3.Models.Packaging;
using Retailr3.Models.PackagingType;

namespace Retailr3.Controllers
{
    public class PackagingsController : BaseController
    {
        private readonly IPackagingService _packagingService;
        private readonly IPackagingTypeService _packagingTypeService;
        private readonly IOptions<AppConfig> _appConfig;

        public PackagingsController(IPackagingService packagingService, IPackagingTypeService packagingTypeService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _packagingService = packagingService;
            _packagingTypeService = packagingTypeService;
        }
        // GET: Packagings
        public async Task<ActionResult> Index()
        {
            var packagings = new List<ListPackagingViewModel>();
            try
            {
                var result = await _packagingService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(packagings);
                }

                foreach (var brand in result.Data)
                {
                    packagings.Add(new ListPackagingViewModel
                    {
                        Id = brand.Id,
                        Code = brand.Code,
                        Description = brand.Description,
                        Name = brand.Name,
                        DateCreated = brand.CreatedAt,
                        DateLastUpdated = brand.LastUpdated
                    });
                }

                return View(packagings);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(packagings);
            }
        }

        // GET: Packagings/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var packaging = new PackagingDetailsViewModel();
            try
            {
                var result = await _packagingService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(packaging);
                }
                packaging.Code = result.Data.Code;
                packaging.Id = result.Data.Id;
                packaging.Name = result.Data.Name;
                packaging.Description = result.Data.Description;
                packaging.DateCreated = result.Data.CreatedAt;
                packaging.DateLastUpdated = result.Data.LastUpdated;
                return View(packaging);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(packaging);
            }
        }

        // GET: Packagings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Packagings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddPackagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addPackagingRequest = new AddPackagingRequest { Name = request.Name, Description = request.Description };
                var result = await _packagingService.Create(addPackagingRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Packaging Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Packagings/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var packaging = new EditPackagingViewModel();
            try
            {
                var result = await _packagingService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(packaging);
                }
                packaging.Id = result.Data.Id;
                packaging.Name = result.Data.Name;
                packaging.Description = result.Data.Description;
                return View(packaging);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(packaging);
            }
        }

        // POST: Packagings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditPackagingViewModel request)
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
                var packagingUpdateRequest = new UpdatePackagingRequest { Id = request.Id, Name = request.Name, Description = request.Description };
                var result = await _packagingService.Update(id, packagingUpdateRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Packaging Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Packagings/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var packaging = new PackagingDetailsViewModel();
            try
            {
                var result = await _packagingService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(packaging);
                }
                packaging.Code = result.Data.Code;
                packaging.Id = result.Data.Id;
                packaging.Name = result.Data.Name;
                packaging.Description = result.Data.Description;
                packaging.DateCreated = result.Data.CreatedAt;
                packaging.DateLastUpdated = result.Data.LastUpdated;
                return View(packaging);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(packaging);
            }
        }

        // POST: Packagings/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, PackagingDetailsViewModel request)
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
                var result = await _packagingService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Packaging Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        //PACKAGING TYPE SECTION
        public async Task<ActionResult> PackagingTypes(Guid id)
        {
            var packagingTypeList = new List<ListPackagingTypeViewModel>();
            try
            {
                var result = await _packagingService.FindByIdInclusive(id, x => x.Include(p => p.PackagingTypes));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(packagingTypeList);
                }

                ViewBag.Packaging = result.Data;

                if (result.Data.PackagingTypes.Count > 0)
                {
                    foreach (var item in result.Data.PackagingTypes)
                    {
                        packagingTypeList.Add(new ListPackagingTypeViewModel
                        {
                            Code = item.Code,
                            DateCreated = item.CreatedAt,
                            DateLastUpdated = item.LastUpdated,
                            Description = item.Description,
                            Id = item.Id,
                            Name = item.Name,
                            PackagingName = result.Data.Name
                        });
                    }
                }
                return View(packagingTypeList);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(packagingTypeList);
            }
        }

        public ActionResult CreatePackagingType(Guid packagingId, string PackagingName)
        {
            var packagingType = new AddPackagingTypeViewModel { PackagingId = packagingId };
            ViewBag.PackagingName = PackagingName;
            return View(packagingType);
        }

        // POST: Packaging/CreatePackagingType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePackagingType(AddPackagingTypeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PackagingTypes), new { id = request.PackagingId });
            }
            try
            {
                var addpackagingTypeRequest = new AddPackagingTypeRequest { PackagingId = request.PackagingId, Name = request.Name, Description = request.Description };
                var result = await _packagingTypeService.Create(addpackagingTypeRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(PackagingTypes), new { id = request.PackagingId });
                }
                Alert($"Packaging Type Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PackagingTypes), new { id = request.PackagingId });
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PackagingTypes), new { id = request.PackagingId });
            }
        }

        public async Task<ActionResult> PackagingTypeDetails(Guid id)
        {
            var packagingType = new PackagingTypeDetailsViewModel();
            try
            {
                var result = await _packagingTypeService.FindByIdInclusive(id, x => x.Include(p => p.Packaging));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(packagingType);
                }
                packagingType.Code = result.Data.Code;
                packagingType.Id = result.Data.Id;
                packagingType.Name = result.Data.Name;
                packagingType.Description = result.Data.Description;
                packagingType.DateCreated = result.Data.CreatedAt;
                packagingType.DateLastUpdated = result.Data.LastUpdated;
                packagingType.PackagingName = result.Data.Packaging.Name;
                packagingType.PackagingId = result.Data.Packaging.Id;
                return View(packagingType);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(packagingType);
            }
        }

        public async Task<ActionResult> EditPackagingType(Guid id)
        {
            var packagingType = new EditPackagingTypeViewModel();
            try
            {
                var result = await _packagingTypeService.FindByIdInclusive(id, x => x.Include(p => p.Packaging));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(packagingType);
                }
                packagingType.Id = result.Data.Id;
                packagingType.Name = result.Data.Name;
                packagingType.Description = result.Data.Description;
                packagingType.PackagingId = result.Data.Packaging.Id;
                packagingType.PackagingName = result.Data.Packaging.Name;
                return View(packagingType);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(packagingType);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPackagingType(Guid id, EditPackagingTypeViewModel request)
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
                var packagingTypeEditRequest = new UpdatePackagingTypeRequest { Id = request.Id, Name = request.Name, Description = request.Description, PackagingId = request.PackagingId };
                var result = await _packagingTypeService.Update(id, packagingTypeEditRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Packaging Type Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PackagingTypes), new { id = request.PackagingId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        public async Task<ActionResult> DeletePackagingType(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var packagingType = new PackagingTypeDetailsViewModel();
            try
            {
                var result = await _packagingTypeService.FindByIdInclusive(id, x => x.Include(p => p.Packaging));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(packagingType);
                }
                packagingType.Code = result.Data.Code;
                packagingType.Id = result.Data.Id;
                packagingType.Name = result.Data.Name;
                packagingType.Description = result.Data.Description;
                packagingType.DateCreated = result.Data.CreatedAt;
                packagingType.DateLastUpdated = result.Data.LastUpdated;
                packagingType.PackagingName = result.Data.Packaging.Name;
                packagingType.PackagingId = result.Data.Packaging.Id;
                return View(packagingType);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(packagingType);
            }
        }

        // POST: Packaging/DeletePackagingType/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePackagingType(Guid id, PackagingTypeDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PackagingTypes), new { id = request.PackagingId });
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PackagingTypes), new { id = request.PackagingId });
            }
            try
            {
                var result = await _packagingTypeService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(PackagingTypes), new { id = request.PackagingId });
                }
                Alert($"Packaging Type Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PackagingTypes), new { id = request.PackagingId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(PackagingTypes), new { id = request.PackagingId });
            }
        }
    }
}

