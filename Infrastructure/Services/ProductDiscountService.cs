using Core.Models;
using Core.Models.Requests;
using Core.Repositories;
using Core.Repositories.EntityRepositories;
using Core.Services;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductDiscountService : BaseService<ProductDiscount>, IProductDiscountService
    {
        private readonly IBaseRepository<ProductDiscount> _baseRepository;

        public ProductDiscountService(IBaseRepository<ProductDiscount> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<ProductDiscount>> Create(AddProductDiscountRequest request)
        {
            try
            {
                var productDiscount = new ProductDiscount
                {
                    Name = request.Name,
                    Code = GenerateCode(8),
                    CatalogueId = request.CatalogueId,
                    BrandId = request.BrandId,
                    CategoryId = request.CategoryId,
                    PackagingId = request.PackagingId,
                    ProductId = request.ProductId,
                    EffectiveDate = request.EffectiveDate,
                    EndDate = request.EndDate
                    
                };

                var exist = await _baseRepository.GetByIdAndCode(productDiscount.Id, productDiscount.Code);
                if (exist != null)
                {
                    return new ServiceResponse<ProductDiscount>($"A Product Discount With the Provided Code and or Id Already Exist");
                }

                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(productDiscount.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<ProductDiscount>($"A Product Discount With the Provided Name Already Exist");
                }

                await _baseRepository.Create(productDiscount);
                return new ServiceResponse<ProductDiscount>(productDiscount);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<ProductDiscount>($"An Error Occured While Creating The Product Discount. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"PRODISC{random.ToUpper()}";
        }

        public async Task<ServiceResponse<ProductDiscount>> Update(Guid id, UpdateProductDiscountRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<ProductDiscount>($"The requested Product Discount could not be found");
                }

                result.Name = request.Name;
                result.ProductId = request.ProductId;
                result.CatalogueId = request.CatalogueId;
                result.CategoryId = request.CategoryId;
                result.BrandId = request.BrandId;
                result.PackagingId = request.PackagingId;
                result.EffectiveDate = request.EffectiveDate;
                result.EndDate = request.EndDate;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<ProductDiscount>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<ProductDiscount>($"An Error Occured While Updating The Product Discount. {ex.Message}");
            }
        }
    }
}
