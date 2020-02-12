using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Requests.EntityRequest;
using Core.Services.EntityServices;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Retailr3.Models.EntityTypeViewModels;

namespace Retailr3.Controllers
{
    public class EntityTypesController : BaseController
    {

        private readonly IOptions<AppConfig> _appConfig;
        private readonly IEntityTypeService _entityTypeService;

        public EntityTypesController(IEntityTypeService entityTypeService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _entityTypeService = entityTypeService;
        }

        // GET: Tier
        public async Task<ActionResult> Index()
        {
            var entityTypes = new List<ListEntityTypeViewModel>();
            try
            {
                var result = await _entityTypeService.FindAll();
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(entityTypes);
                }

                foreach (var entityType in result.Data)
                {
                    entityTypes.Add(new ListEntityTypeViewModel
                    {
                        Id = entityType.Id,
                        Code = entityType.Code,
                        Description = entityType.Description,
                        Name = entityType.Name,
                        DateCreated = entityType.CreatedAt,
                        DateLastUpdated = entityType.LastUpdated
                    });
                }

                return View(entityTypes);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(entityTypes);
            }

        }

        // GET: Tier/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var entityType = new EntityTypeDetailsViewModel();
            try
            {
                var result = await _entityTypeService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(entityType);
                }
                entityType.Code = result.Data.Code;
                entityType.Id = result.Data.Id;
                entityType.Name = result.Data.Name;
                entityType.Description = result.Data.Description;
                entityType.DateCreated = result.Data.CreatedAt;
                entityType.DateLastUpdated = result.Data.LastUpdated;
                return View(entityType);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(entityType);
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
        public async Task<ActionResult> Create(AddEntityTypeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                Alert($"Invalid Request.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
            try
            {
                var addTierRequest = new AddEntityTypeRequest { Name = request.Name, Description = request.Description };
                var result = await _entityTypeService.Create(addTierRequest);
                if (!result.Success)
                {
                    Alert($"{result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return RedirectToAction(nameof(Create));
                }
                Alert($"Tier Created Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}.", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return RedirectToAction(nameof(Create));
            }
        }

        // GET: Tier/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {

            var entityType = new EditEntityTypeViewModel();
            try
            {
                var result = await _entityTypeService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(entityType);
                }
                entityType.Id = result.Data.Id;
                entityType.Name = result.Data.Name;
                entityType.Description = result.Data.Description;
                return View(entityType);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(entityType);
            }
        }

        // POST: Tier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditEntityTypeViewModel request)
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
                var tierUpdateRequest = new UpdateEntityTypeRequest { Id = request.Id, Name = request.Name, Description = request.Description };
                var result = await _entityTypeService.Update(id, tierUpdateRequest);
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

            var entityType = new EntityTypeDetailsViewModel();
            try
            {
                var result = await _entityTypeService.FindById(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View(entityType);
                }
                entityType.Code = result.Data.Code;
                entityType.Id = result.Data.Id;
                entityType.Name = result.Data.Name;
                entityType.Description = result.Data.Description;
                entityType.DateCreated = result.Data.CreatedAt;
                entityType.DateLastUpdated = result.Data.LastUpdated;
                return View(entityType);
            }
            catch (Exception ex)
            {
                Alert($"Error! {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View(entityType);
            }
        }

        // POST: Tier/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, EntityTypeDetailsViewModel request)
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
                var result = await _entityTypeService.Delete(id);
                if (!result.Success)
                {
                    Alert($"Error! {result.Message}", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }
                Alert($"Entity Type Deleted Successfully", NotificationType.success, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
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