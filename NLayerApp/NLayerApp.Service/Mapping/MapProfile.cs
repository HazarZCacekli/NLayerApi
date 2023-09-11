using AutoMapper;
using NLayerApp.Core.DTOS;
using NLayerApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<Product, ProductWithProductFeatureDTO>();
            CreateMap<ProductCreateDTO,Product>();
            CreateMap<ProductFeature,ProductFeatureDTO>().ReverseMap();

        }
    }
}
