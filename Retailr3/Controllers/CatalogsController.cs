using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Requests;
using Core.Services;
using Core.Services.EntityServices;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Retailr3.Models.CatalogViewModels;

namespace Retailr3.Controllers
{
    public class CatalogsController : BaseController
    {
        private readonly ICatalogService _catalogService;
        private readonly ISupplierService _supplierService;
        private readonly IAnchorService _anchorService;
        private readonly IMerchantService _merchantService;
        private readonly IOptions<AppConfig> _appConfig;

        public CatalogsController(IAnchorService anchorService, IMerchantService merchantService,ISupplierService supplierService,ICatalogService catalogService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _catalogService = catalogService;
            _merchantService = merchantService;
            _supplierService = supplierService;
            _anchorService = anchorService;
        }

        // GET: Catalogs
        public async Task<ActionResult> Index()
        {
            var catalogs = new List<ListCatalogViewModel>();
            try
            {
                var result = await _catalogService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(catalogs);
                }

                foreach (var catalog in result.Data)
                {
                    catalogs.Add(new ListCatalogViewModel
                    {
                        Id = catalog.Id,
                        Code = catalog.Code,
                        Description = catalog.Description,
                        Name = catalog.Name,
                        DateCreated = catalog.CreatedAt,
                        DateLastUpdated = catalog.LastUpdated,
                        EffectiveDate = catalog.EffectiveDate,
                        EndDate = catalog.EndDate,
                        Published = catalog.Published ? "Yes" : "No"
                    });
                }

                return View(catalogs);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(catalogs);
            }
        }

        // GET: Catalogs/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var catalog = new CatalogDetailsViewModel();
            try
            {
                var result = await _catalogService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(catalog);
                }

                

                catalog.Code = result.Data.Code;
                catalog.Id = result.Data.Id;
                catalog.Name = result.Data.Name;
                catalog.EffectiveDate = result.Data.EffectiveDate;
                catalog.EndDate = result.Data.EndDate;
                catalog.Published = result.Data.Published ? "Yes" : "No";
                catalog.Description = result.Data.Description;
                catalog.DateCreated = result.Data.CreatedAt;
                catalog.DateLastUpdated = result.Data.LastUpdated;


                var entityResult = await _catalogService.GetCatalogEntity(result.Data.EntityId);
                if (entityResult.Success)
                {
                    catalog.Entity = entityResult.Data.Name;
                }
                
                return View(catalog);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(catalog);
            }
        }

        // GET: Catalogs/Create
        public async Task<ActionResult> Create()
        {
            var catalog = new AddCatalogViewModel();
            var entities = new List<EntityResource>();
            var suppliers = await _supplierService.FindAll();
            if (!suppliers.Success)
            {
                Alert($"{suppliers.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
            }
            else
            {
                foreach (var supplier in suppliers.Data)
                {
                    entities.Add(new EntityResource { Id = supplier.Id, Name = supplier.Name });
                }
            }

            var merchants = await _merchantService.FindAll();
            if (!merchants.Success)
            {
                Alert($"{merchants.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
            }
            else
            {
                foreach (var merchant in merchants.Data)
                {
                    entities.Add(new EntityResource { Id = merchant.Id, Name = merchant.Name });
                }
            }

            ViewBag.Entities = entities;
            return View(catalog);
        }

        // POST: Catalogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddCatalogViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var catalogRequest = new AddCatalogRequest 
                { 
                    Name = request.Name,
                    Description = request.Description,
                    EffectiveDate = request.EffectiveDate,
                    EndDate = request.EndDate,
                    EntityId = request.EntityId,
                    Published = request.Published
                };

                var result = await _catalogService.Create(catalogRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Catalog Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Catalogs/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var catalog = new EditCatalogViewModel();
            try
            {
                var entities = new List<EntityResource>();
                var suppliers = await _supplierService.FindAll();
                if (!suppliers.Success)
                {
                    Alert($"{suppliers.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                }
                else
                {
                    foreach (var supplier in suppliers.Data)
                    {
                        entities.Add(new EntityResource { Id = supplier.Id, Name = supplier.Name });
                    }
                }

                var merchants = await _merchantService.FindAll();
                if (!merchants.Success)
                {
                    Alert($"{merchants.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                }
                else
                {
                    foreach (var merchant in merchants.Data)
                    {
                        entities.Add(new EntityResource { Id = merchant.Id, Name = merchant.Name });
                    }
                }

                ViewBag.Entities = entities;


                var result = await _catalogService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(catalog);
                }
                catalog.CatalogId = result.Data.Id;
                catalog.Name = result.Data.Name;
                catalog.EffectiveDate = result.Data.EffectiveDate;
                catalog.EndDate = result.Data.EndDate;
                catalog.Published = result.Data.Published;
                catalog.Description = result.Data.Description;
                catalog.EntityId = result.Data.EntityId;
                return View(catalog);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(catalog);
            }
        }

        // POST: Catalogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditCatalogViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            if (!id.Equals(request.CatalogId))
            {
                Alert("Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var catalogUpdateRequest = new UpdateCatalogRequest 
                {
                    CatalogId = request.CatalogId,
                    Name = request.Name, 
                    Description = request.Description,
                    EffectiveDate = request.EffectiveDate,
                    EndDate = request.EndDate,
                    EntityId = request.EntityId,
                    Published = request.Published
                };
                var result = await _catalogService.Update(id, catalogUpdateRequest);
                if (!result.Success)
                {
                    Alert($"Error: {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }

                Alert($"Catalog Updated Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Alert($"Error Occurred While processing the request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

        // GET: Catalogs/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var catalog = new CatalogDetailsViewModel();
            try
            {
                var result = await _catalogService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(catalog);
                }
                catalog.Code = result.Data.Code;
                catalog.Id = result.Data.Id;
                catalog.Name = result.Data.Name;
                catalog.EffectiveDate = result.Data.EffectiveDate;
                catalog.EndDate = result.Data.EndDate;
                catalog.Published = result.Data.Published ? "Yes" : "No";
                catalog.Description = result.Data.Description;
                catalog.DateCreated = result.Data.CreatedAt;
                catalog.DateLastUpdated = result.Data.LastUpdated;
                catalog.Entity = result.Data.EntityId.ToString();
                return View(catalog);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(catalog);
            }
        }

        // POST: Catalogs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, CatalogDetailsViewModel request)
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
                var result = await _catalogService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Catalog Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
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