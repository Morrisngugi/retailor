using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Requests.EntityRequest;
using Core.Services;
using Core.Services.EntityServices;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Retailr3.Models.AnchorViewModels;

namespace Retailr3.Controllers
{
    public class AnchorsController : BaseController
    {
        private readonly ICountryService _countryService;
        private readonly IAnchorService _anchorService;
        private readonly IEntityTypeService _entityTypeService;
        private readonly ITierService _tierService;
        private readonly IOptions<AppConfig> _appConfig;

        public AnchorsController(IEntityTypeService entityTypeService,ITierService tierService,ICountryService countryService,IAnchorService anchorService, IOptions<AppConfig> appConfig)
        {
            _anchorService = anchorService;
            _countryService = countryService;
            _appConfig = appConfig;
            _tierService = tierService;
            _entityTypeService = entityTypeService;
        }

        // GET: Anchors
        public async Task<ActionResult> Index()
        {
            var anchorsList = new List<ListAnchorsViewModel>();
            try
            {
                var result = await _anchorService.FindAllInclusive(x => x.Include(p => p.EntityType).Include(w => w.Address).ThenInclude(f=>f.Region).Include(q => q.Setting)/*.Include(r => r.Tier)*/.Include(g => g.ContactPerson));
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(anchorsList);
                }
                
                foreach (var item in result.Data)
                {
                    anchorsList.Add(new ListAnchorsViewModel
                    {
                        Code = item.Code,
                        DateCreated = item.CreatedAt,
                        DateLastUpdated = item.LastUpdated,
                        Description = item.Description,
                        EntityType = item.EntityType?.Name,
                        Name = item.Name,
                        Id = item.Id,
                        Size = item.Size
                    });
                }
                return View(anchorsList);
            }
            catch (Exception ex)
            {

                Alert($"{ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(anchorsList);
            }
           
        }

        // GET: Anchors/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var anchor = new AnchorDetailsViewModel();
            try
            {
                var result = await _anchorService.FindByIdInclusive(id, x => x.Include(p => p.EntityType).Include(w => w.Address).ThenInclude(f => f.Region).ThenInclude(c => c.Country).Include(q => q.Setting).Include(g => g.ContactPerson));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(anchor);
                }
                var anchor1 = new AnchorDetailsViewModel
                {
                    Id = result.Data.Id,
                    Code = result.Data.Code,
                    AddressEmail = result.Data.Address?.EmailAddress,
                    AddressPhone = result.Data.Address?.PhoneNumber,
                    City = result.Data.Address.City,
                    ContactEmail = result.Data.ContactPerson?.ContactEmail,
                    ContactPhone = result.Data.ContactPerson?.ContactPhone,
                    CountryName = result.Data.Address?.Region?.Country?.Name,
                    Name = result.Data.Name,
                    DateCreated = result.Data.CreatedAt,
                    DateLastUpdated = result.Data.LastUpdated,
                    DateOfRegistration = result.Data.DateOfRegistration,
                    Description = result.Data.Description,
                    EntityTypeName = result.Data.EntityType?.Name,
                    FirstName = result.Data.ContactPerson.FirstName,
                    LastName = result.Data.ContactPerson.FirstName,
                    LicenceNumber = result.Data.LicenceNumber,
                    PurchaseOrderAutoApproval = result.Data.Setting.PurchaseOrderAutoApproval,
                    SaleOrderAutoApproval = result.Data.Setting.SaleOrderAutoApproval,
                    RegionName = result.Data.Address?.Region.Name,
                    RegistrationNumber = result.Data.RegistrationNumber,
                    SettingDescription = result.Data.Setting?.Description,
                    SettingName = result.Data.Setting?.Name,
                    Street = result.Data.Address?.Street
                };

                var tierResult = await _tierService.FindById(result.Data.TierId);
                if (tierResult.Success)
                {
                    anchor1.TierName = tierResult.Data.Name;
                }
                return View(anchor1);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(anchor);
            }
        }

        // GET: Anchors/Create
        public async Task<ActionResult> Create()
        {
            var tiersResult = await _tierService.FindAll();
            if (!tiersResult.Success)
            {
                Alert($"{tiersResult.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                ViewBag.Tiers = null;
            }
            else
            {
                ViewBag.Tiers = tiersResult.Data;
            }

            var countryResult = await _countryService.FindAll();
            if (!countryResult.Success)
            {
                Alert($"{tiersResult.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                ViewBag.Countries = null;
            }
            else
            {
                ViewBag.Countries = countryResult.Data;
            }

            var etypeResult = await _entityTypeService.FindAll();
            if (!etypeResult.Success)
            {
                Alert($"{etypeResult.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                ViewBag.EntityTypes = null;
            }
            else
            {
                ViewBag.EntityTypes = etypeResult.Data;
            }
            return View();
        }

        // POST: Anchors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddAnchorViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addAnchor = new AddAnchorRequest 
                { 
                    Name = request.Name,
                    Description = request.Description,
                    AddressEmail = request.AddressEmail,
                    AddressPhone = request.AddressPhone,
                    City = request.City,
                    ContactEmail = request.ContactEmail,
                    ContactPhone = request.ContactPhone,
                    DateOfRegistration = request.DateOfRegistration,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    LicenceNumber = request.LicenceNumber,
                    PurchaseOrderAutoApproval = request.PurchaseOrderAutoApproval,
                    SaleOrderAutoApproval = request.SaleOrderAutoApproval,
                    RegionId = request.RegionId,
                    RegistrationNumber = request.RegistrationNumber,
                    SettingDescription = request.SettingDescription,
                    SettingName = request.SettingName,
                    Street = request.Street,
                    TierId = request.TierId,
                    EntityTypeId = request.EntityTypeId
                };
                var result = await _anchorService.Create(addAnchor);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(Create));
                }
                Alert($"Anchor Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Create));
            }
        }

        // GET: Anchors/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var tiersResult = await _tierService.FindById(id);
            if (!tiersResult.Success)
            {
                Alert($"{tiersResult.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                ViewBag.Tiers = null;
            }
            else
            {
                ViewBag.Tiers = tiersResult.Data;
            }

            var countryResult = await _countryService.FindAll();
            if (!countryResult.Success)
            {
                Alert($"{tiersResult.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                ViewBag.Countries = null;
            }
            else
            {
                ViewBag.Countries = countryResult.Data;
            }

            var etypeResult = await _entityTypeService.FindAll();
            if (!etypeResult.Success)
            {
                Alert($"{etypeResult.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                ViewBag.EntityTypes = null;
            }
            else
            {
                ViewBag.EntityTypes = etypeResult.Data;
            }


            var anchor = new EditAnchorViewModel();
            try
            {
                var result = await _anchorService.FindByIdInclusive(id, x => x.Include(p => p.EntityType).Include(w => w.Address).ThenInclude(f => f.Region).Include(q => q.Setting).Include(g => g.ContactPerson));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(anchor);
                }
                var anchor1 = new EditAnchorViewModel 
                {
                    AnchorId = result.Data.Id,
                    AddressEmail = result.Data.Address?.EmailAddress,
                    AddressPhone = result.Data.Address?.PhoneNumber,
                    City = result.Data.Address.City,
                    ContactEmail = result.Data.ContactPerson?.ContactEmail,
                    ContactPhone = result.Data.ContactPerson?.ContactPhone,
                    Name = result.Data.Name,
                    DateOfRegistration = result.Data.DateOfRegistration,
                    Description = result.Data.Description,
                    FirstName = result.Data.ContactPerson.FirstName,
                    LastName = result.Data.ContactPerson.FirstName,
                    LicenceNumber = result.Data.LicenceNumber,
                    PurchaseOrderAutoApproval = result.Data.Setting.PurchaseOrderAutoApproval,
                    SaleOrderAutoApproval = result.Data.Setting.SaleOrderAutoApproval,
                    RegistrationNumber = result.Data.RegistrationNumber,
                    SettingDescription = result.Data.Setting?.Description,
                    SettingName = result.Data.Setting?.Name,
                    Street = result.Data.Address?.Street,
                    EntityTypeId = result.Data.EntityType.Id,
                    RegionId = result.Data.Address.Region.Id,
                    TierId = result.Data.TierId
                    
                };
                return View(anchor1);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(anchor);
            }
        }

        // POST: Anchors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditAnchorViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            if (!id.Equals(request.AnchorId))
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var updateAnchorRequest = new UpdateAnchorRequest 
                { 
                    Id = request.AnchorId,
                    Name = request.Name, 
                    Description = request.Description 
                };
                var result = await _anchorService.Update(id, updateAnchorRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Anchor Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Anchors/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var anchor = new AnchorDetailsViewModel();
            try
            {
                var result = await _anchorService.FindByIdInclusive(id, x => x.Include(p => p.EntityType).Include(w => w.Address).ThenInclude(f => f.Region).ThenInclude(c => c.Country).Include(q => q.Setting).Include(g => g.ContactPerson));
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(anchor);
                }
                var anchor1 = new AnchorDetailsViewModel
                {
                    Id = result.Data.Id,
                    Code = result.Data.Code,
                    AddressEmail = result.Data.Address?.EmailAddress,
                    AddressPhone = result.Data.Address?.PhoneNumber,
                    City = result.Data.Address.City,
                    ContactEmail = result.Data.ContactPerson?.ContactEmail,
                    ContactPhone = result.Data.ContactPerson?.ContactPhone,
                    CountryName = result.Data.Address?.Region?.Country?.Name,
                    Name = result.Data.Name,
                    DateCreated = result.Data.CreatedAt,
                    DateLastUpdated = result.Data.LastUpdated,
                    DateOfRegistration = result.Data.DateOfRegistration,
                    Description = result.Data.Description,
                    EntityTypeName = result.Data.EntityType?.Name,
                    FirstName = result.Data.ContactPerson.FirstName,
                    LastName = result.Data.ContactPerson.FirstName,
                    LicenceNumber = result.Data.LicenceNumber,
                    PurchaseOrderAutoApproval = result.Data.Setting.PurchaseOrderAutoApproval,
                    SaleOrderAutoApproval = result.Data.Setting.SaleOrderAutoApproval,
                    RegionName = result.Data.Address?.Region.Name,
                    RegistrationNumber = result.Data.RegistrationNumber,
                    SettingDescription = result.Data.Setting?.Description,
                    SettingName = result.Data.Setting?.Name,
                    Street = result.Data.Address?.Street
                };

                var tierResult = await _tierService.FindById(result.Data.TierId);
                if (tierResult.Success)
                {
                    anchor1.TierName = tierResult.Data.Name;
                }
                return View(anchor1);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(anchor);
            }
        }

        // POST: Anchors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, AnchorDetailsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Bad Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Delete), new { id = request.Id });
            }
            if (id == null)
            {
                Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Delete), new { id = request.Id });
            }
            try
            {
                var result = await _anchorService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(Delete), new { id = request.Id });
                }
                Alert($"Tier Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Delete), new { id = request.Id });
            }
        }
    }
}