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
using Retailr3.Models.UnitOfMeasure;
using Retailr3.Models.UnitOfMeasureType;

namespace Retailr3.Controllers
{
    public class UnitOfMeasureTypeController : BaseController
    {
        private readonly IUnitOfMeasureTypeService _unitOfMeasureTypeService;
        private readonly IUnitOfMeasureService _unitOfMeasureService;
        private readonly IOptions<AppConfig> _appConfig;

        public UnitOfMeasureTypeController(IUnitOfMeasureService unitOfMeasureService, IUnitOfMeasureTypeService unitOfMeasureTypeService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _unitOfMeasureTypeService = unitOfMeasureTypeService;
            _unitOfMeasureService = unitOfMeasureService;
        }

        // GET: UnitOfMeasureType
        public async Task<ActionResult> Index()
        {
            var unitOfMeasureTypes = new List<ListUnitOfMeasureTypeViewModel>();
            try
            {
                var result = await _unitOfMeasureTypeService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(unitOfMeasureTypes);
                }

                foreach (var unitOfMeasureType in result.Data)
                {
                    unitOfMeasureTypes.Add(new ListUnitOfMeasureTypeViewModel
                    {
                        Id = unitOfMeasureType.Id,
                        Name  =unitOfMeasureType.Name,
                        Code = unitOfMeasureType.Code,
                        Description = unitOfMeasureType.Description,
                        DateCreated = unitOfMeasureType.CreatedAt,
                        DateLastUpdated = unitOfMeasureType.LastUpdated

                    });
                }
                return View(unitOfMeasureTypes);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(unitOfMeasureTypes);
            }
        }

        // GET: UnitOfMeasureType/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var unitOfMeasureType = new UnitOfMeasureTypeDetailsViewModel();
            try
            {
                var result = await _unitOfMeasureTypeService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(unitOfMeasureType);
                }
                unitOfMeasureType.Code = result.Data.Code;
                unitOfMeasureType.Id = result.Data.Id;
                unitOfMeasureType.Name = result.Data.Name;
                unitOfMeasureType.Description = result.Data.Description;
                unitOfMeasureType.DateCreated = result.Data.CreatedAt;
                unitOfMeasureType.DateLastUpdated = result.Data.LastUpdated;
                return View(unitOfMeasureType);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(unitOfMeasureType);
            }
        }

        // GET: UnitOfMeasureType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitOfMeasureType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddUnitOfMeasureTypeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addUnitOfMeasureTypeRequest = new AddUnitOfMeasureTypeRequest { Name = request.Name, Description = request.Description };
                var result = await _unitOfMeasureTypeService.Create(addUnitOfMeasureTypeRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Unit Of Measure Type Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: UnitOfMeasureType/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var unitOfMeasureType = new EditUnitOfMeasureTypeViewModel();
            try
            {
                var result = await _unitOfMeasureTypeService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(unitOfMeasureType);
                }
                unitOfMeasureType.Id = result.Data.Id;
                unitOfMeasureType.Name = result.Data.Name;
                unitOfMeasureType.Description = result.Data.Description;
                return View(unitOfMeasureType);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(unitOfMeasureType);
            }
        }

        // POST: UnitOfMeasureType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditUnitOfMeasureTypeViewModel request)
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
                var unitOfMeasureTypeUpdateRequest = new UpdateUnitOfMeasureTypeRequest { Id = request.Id, Name = request.Name, Description = request.Description };
                var result = await _unitOfMeasureTypeService.Update(id, unitOfMeasureTypeUpdateRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Unit Of Measure Type Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: UnitOfMeasureType/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var unitOfMeasureType = new UnitOfMeasureTypeDetailsViewModel();
            try
            {
                var result = await _unitOfMeasureTypeService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(unitOfMeasureType);
                }
                unitOfMeasureType.Code = result.Data.Code;
                unitOfMeasureType.Id = result.Data.Id;
                unitOfMeasureType.Name = result.Data.Name;
                unitOfMeasureType.Description = result.Data.Description;
                unitOfMeasureType.DateCreated = result.Data.CreatedAt;
                unitOfMeasureType.DateLastUpdated = result.Data.LastUpdated;
                return View(unitOfMeasureType);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(unitOfMeasureType);
            }
        }

        // POST: UnitOfMeasureType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, UnitOfMeasureTypeDetailsViewModel request)
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
                var result = await _unitOfMeasureTypeService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Unit Of Measure Type Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }




        //UOM  SECTION
        public async Task<ActionResult> UnitOfMeasures(Guid id)
        {
            var unitOfMeasureList = new List<ListUnitOfMeasureViewModel>();
            try
            {
                var result = await _unitOfMeasureTypeService.FindByIdInclusive(id, x => x.Include(p => p.UnitOfMeasures));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(unitOfMeasureList);
                }

                ViewBag.UomType = result.Data;

                if (result.Data.UnitOfMeasures.Count > 0)
                {
                    foreach (var item in result.Data.UnitOfMeasures)
                    {
                        unitOfMeasureList.Add(new ListUnitOfMeasureViewModel
                        {
                            Code = item.Code,
                            DateCreated = item.CreatedAt,
                            DateLastUpdated = item.LastUpdated,
                            Description = item.Description,
                            Id = item.Id,
                            Name = item.Name,
                            UOMTypeName = result.Data.Name,
                            Grammage = item.Grammage,
                            PackSize = item.PackSize,
                            PalletSize = item.PalletSize
                        });
                    }
                }
                return View(unitOfMeasureList);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(unitOfMeasureList);
            }
        }

        // GET: UnitOfMeasureType/CreateUOM
        public ActionResult CreateUOM(Guid uomTypeId, string UomTypeName)
        {
            var uomVm = new AddUOMViewModel { UomTypeId = uomTypeId };
            ViewBag.UomTypeName = UomTypeName;
            return View(uomVm);
        }

        // POST: UnitOfMeasureType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUOM(AddUOMViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(UnitOfMeasures), new { id = request.UomTypeId });
            }
            try
            {
                var addUOMRequest = new AddUnitOfMeasureRequest { UnitOfMeasureTypeId = request.UomTypeId, Name = request.Name, Description = request.Description, Grammage = request.Grammage, PackSize = request.PackSize, PalletSize = request.PalletSize };
                var result = await _unitOfMeasureService.Create(addUOMRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(UnitOfMeasures), new { id = request.UomTypeId });
                }
                Alert($"Unit Of Measure Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(UnitOfMeasures), new { id = request.UomTypeId });
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(UnitOfMeasures), new { id = request.UomTypeId });
            }
        }

        public async Task<ActionResult> UOMDetails(Guid id)
        {
            var uom = new UOMDetailsViewModel();
            try
            {
                var result = await _unitOfMeasureService.FindByIdInclusive(id, x => x.Include(p => p.UnitOfMeasureType));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(uom);
                }
                uom.Code = result.Data.Code;
                uom.Id = result.Data.Id;
                uom.Name = result.Data.Name;
                uom.Description = result.Data.Description;
                uom.DateCreated = result.Data.CreatedAt;
                uom.DateLastUpdated = result.Data.LastUpdated;
                uom.UOMTypeName = result.Data.UnitOfMeasureType.Name;
                uom.UOMTypeId = result.Data.UnitOfMeasureType.Id;
                uom.Grammage = result.Data.Grammage;
                uom.PackSize = result.Data.PackSize;
                uom.PalletSize = result.Data.PalletSize;
                return View(uom);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(uom);
            }
        }

        public async Task<ActionResult> EditUOM(Guid id)
        {
            var uom = new EditUOMViewModel();
            try
            {
                var result = await _unitOfMeasureService.FindByIdInclusive(id, x => x.Include(p => p.UnitOfMeasureType));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(uom);
                }
                uom.Id = result.Data.Id;
                uom.Name = result.Data.Name;
                uom.Description = result.Data.Description;
                uom.UOMTypeId = result.Data.UnitOfMeasureType.Id;
                uom.UOMTypeName = result.Data.UnitOfMeasureType.Name;
                uom.Grammage = result.Data.Grammage;
                uom.PackSize = result.Data.PackSize;
                uom.PalletSize = result.Data.PalletSize;
                return View(uom);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(uom);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUOM(Guid id, EditUOMViewModel request)
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
                var uomEditRequest = new UpdateUnitOfMeasureRequest { Id = request.Id, Name = request.Name, Description = request.Description, UnitOfMeasureTypeId = request.UOMTypeId, Grammage = request.Grammage, PackSize = request.PackSize, PalletSize = request.PalletSize };
                var result = await _unitOfMeasureService.Update(id, uomEditRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Unit Of Measure Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(UnitOfMeasures), new { id = request.UOMTypeId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        public async Task<ActionResult> DeleteUOM(Guid id)
        {
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }

            var uom = new UOMDetailsViewModel();
            try
            {
                var result = await _unitOfMeasureService.FindByIdInclusive(id, x => x.Include(p => p.UnitOfMeasureType));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(uom);
                }
                uom.Code = result.Data.Code;
                uom.Id = result.Data.Id;
                uom.Name = result.Data.Name;
                uom.Description = result.Data.Description;
                uom.DateCreated = result.Data.CreatedAt;
                uom.DateLastUpdated = result.Data.LastUpdated;
                uom.UOMTypeName = result.Data.UnitOfMeasureType.Name;
                uom.UOMTypeId = result.Data.UnitOfMeasureType.Id;
                uom.Grammage = result.Data.Grammage;
                uom.PackSize = result.Data.PackSize;
                uom.PalletSize = result.Data.PalletSize;
                return View(uom);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(uom);
            }
        }

        // POST: UnitOfMeasureType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUOM(Guid id, UOMDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(UnitOfMeasures), new { id = request.UOMTypeId });
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(UnitOfMeasures), new { id = request.UOMTypeId });
            }
            try
            {
                var result = await _unitOfMeasureService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(UnitOfMeasures), new { id = request.UOMTypeId });
                }
                Alert($"Unit Of Measure Type Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(UnitOfMeasures), new { id = request.UOMTypeId });
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(UnitOfMeasures), new { id = request.UOMTypeId });
            }
        }

        //productRepository.GetById(2, x => x.Include(p => p.Orders).ThenInclude(o => o.LineItems).Include(p => p.Parts))

       






    }
}