using NLayerApp.Core.DTOS;
using NLayerApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface IProductService : IService<Product,ProductDTO>
    {
        Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductCreateDTO dto);
        Task<CustomResponseDTO<IEnumerable<ProductDTO>>> AddRangeAsync(IEnumerable<ProductCreateDTO> dto);
        Task<CustomResponseDTO<ProductWithProductFeatureDTO>> GetWithProductFeature(int id);
        Task<CustomResponseDTO<IEnumerable<ProductWithProductFeatureDTO>>> GetAllWithProductFeature();

    }
}
