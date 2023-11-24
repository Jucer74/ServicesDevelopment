using System;
using AutoMapper;
using ProductService.Domain.Dtos;
using ProductServiceAPI.Domain.Entities;

namespace ProductService.Api.Mapping
{
	public class Mapper : Profile
	{
		public Mapper()
		{
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductPutDto, Product>();
            CreateMap<Product, ProductPutDto>();
        }
	}
}

