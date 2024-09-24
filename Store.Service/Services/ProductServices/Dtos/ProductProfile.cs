﻿using AutoMapper;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.ProductServices.Dtos
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetailsDto>()
            .ForMember(dest=> dest.BrandName, options => options.MapFrom(src=> src.Brand.Name))
            .ForMember(dest=> dest.TypeName, options => options.MapFrom(src=> src.Type.Name));

            CreateMap<ProductBrand, BrandTypeDetialsDto>();
            CreateMap<ProductType, BrandTypeDetialsDto>();
        }
    }
}
