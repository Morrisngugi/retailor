using Core.Models;
using Core.Models.Requests;
using Core.Repositories;
using Core.Services;
using Core.Services.Communications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class VatService : BaseService<Vat>, IVatService
    {
        private readonly IBaseRepository<Vat> _baseRepository;
        private readonly IVatCategoryService _vatService;

        public VatService(IVatCategoryService vatService, IBaseRepository<Vat> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _vatService = vatService;
        }

        public async Task<ServiceResponse<Vat>> Create(AddVatRequest request)
        {
            try
            {
                var vatCategory = await _vatService.FindByIdInclusive(request.VatCategoryId, x => x.Include(p => p.Vats));
                if (!vatCategory.Success)
                {
                    return new ServiceResponse<Vat>($"The Packaging type does not exist");
                }

                if (vatCategory.Data.Vats.Count > 0 && vatCategory.Data.Vats.Any(x => x.Rate.Equals(request.Rate)))
                {
                    return new ServiceResponse<Vat>($"The Packaging already exist exist");
                }

                var vat = new Vat
                {
                    Rate = request.Rate,
                    Code = GenerateCode(8),
                    EffectiveDate = request.EffectiveDate,
                    EndDate = request.EndDate,
                    VatCategoryId = vatCategory.Data.Id
                };
                var exist = await _baseRepository.GetByIdAndCode(vat.Id, vat.Code);
                if (exist != null)
                {
                    return new ServiceResponse<Vat>($"A Vat With the Provided Code and or Id Already Exist");
                }
                var exist2 = await _baseRepository.FindOneByConditions(x => x.Rate.Equals(vat.Rate));
                if (exist2 != null)
                {
                    return new ServiceResponse<Vat>($"A Vat With the Provided Name Already Exist");
                }

                await _baseRepository.Create(vat);
                return new ServiceResponse<Vat>(vat);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Vat>($"An Error Occured While Creating The Vat. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"SBR{random.ToUpper()}";
        }

        public async Task<ServiceResponse<Vat>> Update(Guid id, UpdateVatRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<Vat>($"The requested Vat Category could not be found");
                }

                result.Rate = request.Rate;
                result.EffectiveDate = request.EffectiveDate;
                result.EndDate = request.EndDate;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<Vat>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<Vat>($"An Error Occured While Updating The Vat Category. {ex.Message}");
            }
        }
    }
}
