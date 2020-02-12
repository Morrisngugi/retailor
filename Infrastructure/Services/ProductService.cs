using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Core.Models.Requests;
using Core.Repositories;
using Core.Services;
using Core.Services.Communications;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IBaseRepository<Product> _baseRepository;

        public ProductService(IBaseRepository<Product> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<Product>> Create(AddProductRequest request)
        {
            try
            {
                var product = new Product
                {
                    Code = GenerateCode(8),
                    Name = request.Name,
                    Description = request.Description,
                    SubBrandId = request.SubBrandId,
                    SubCategoryId = request.SubCategoryId,
                    UnitOfMeasureId = request.UnitOfMeasureId,
                    FactoryPrice = request.FactoryPrice,
                    PackagingTypeId = request.PackagingTypeId,
                    Manufacturer = request.Manufacturer,
                    StockType = request.StockType,
                    ImageUrl = request.ImageUrl,
                    VatId = request.VatId,
                    ReOrderLevel = request.ReOrderLevel
                };

                var exist = await _baseRepository.GetByIdAndCode(product.Id, product.Code);
                if (exist != null)
                {
                    return new ServiceResponse<Product>($"A Product With the Provided Code and or Id Already Exist");
                }

                await _baseRepository.Create(product);
                return new ServiceResponse<Product>(product);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Product>($"An Error Occured While Creating The Product. {ex.Message}");
            }
        }
        public async Task<ServiceResponse<Product>> Update(Guid id, UpdateProduct request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<Product>($"The requested Product could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.Manufacturer = request.Manufacturer;
                result.ReOrderLevel = request.ReOrderLevel;
                result.StockType = request.StockType;
                result.FactoryPrice = request.FactoryPrice;
                result.SubBrandId = request.SubBrandId;
                result.SubCategoryId = request.SubCategoryId;
                result.ImageUrl = request.ImageUrl;
                result.VatId = request.VatId;
                result.UnitOfMeasureId = request.UnitOfMeasureId;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<Product>(result);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Product>($"An Error Occured While Updating The Product. {ex.Message}");
            }
        }
        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"PROD{random.ToUpper()}";
        }

    }
}
