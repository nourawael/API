using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.Services.ProductServices.Dtos;
using ProductEntity = Store.Data.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.Repository.Specification.ProductSpec;

namespace Store.Service.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetialsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsNoTrackingAsync();

            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetialsDto>>(brands);

            return mappedBrands;
        }

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new  ProductWithSpecification(input);
            var products = await _unitOfWork.Repository<ProductEntity, int>().GetAllWithSpecificationAsync(specs);

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);
            return mappedProducts;
        }

        public async Task<IReadOnlyList<BrandTypeDetialsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsNoTrackingAsync();

            var mappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetialsDto>>(types);

            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? productId)
        {
            if (productId is null)
                throw new Exception("Id is null");

            var product = await _unitOfWork.Repository<ProductEntity, int>().GetByIdAsync(productId.Value);

            if(product is null)
                throw new Exception("Product is null");

            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);

            return mappedProduct;

        }
    }
}
