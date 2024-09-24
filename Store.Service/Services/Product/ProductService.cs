using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.Services.Product.Dtos;
using ProductEntity = Store.Data.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<BrandTypeDetialsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsNoTrackingAsync();

            var mappedBrands = brands.Select(x=> new BrandTypeDetialsDto 
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
            }).ToList();

            return mappedBrands;
        }

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repository<ProductEntity, int>().GetAllAsNoTrackingAsync();

            var mappedProducts = products.Select(x=>new ProductDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                PictureUrl = x.PictureUrl,
                Price = x.Price,
                CreatedAt = x.CreatedAt,
                BrandName= x.Brand.Name,
                TypeName= x.Type.Name
            }).ToList();
            return mappedProducts;
        }

        public async Task<IReadOnlyList<BrandTypeDetialsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsNoTrackingAsync();

            var mappedTypes = types.Select(x => new BrandTypeDetialsDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
            }).ToList();

            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? productId)
        {
            if (productId is null)
                throw new Exception("Id is null");

            var product = await _unitOfWork.Repository<ProductEntity, int>().GetByIdAsync(productId.Value);

            if(product is null)
                throw new Exception("Product is null");

            var mappedProduct = new ProductDetailsDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CreatedAt = product.CreatedAt,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                BrandName = product.Brand.Name,
                TypeName = product.Type.Name,
            };

            return mappedProduct;

        }
    }
}
