using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.DTOS;
using NLayerApp.Core.Models;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;

namespace NLayerApp.Service.Services
{
    public class ProductService : Service<Product, ProductDTO>, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IMapper mapper, IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository) : base(mapper, repository, unitOfWork)
        {
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductCreateDTO dto)
        {
            var newEntity = _mapper.Map<Product>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newdto = _mapper.Map<ProductDTO>(newEntity);
            return CustomResponseDTO<ProductDTO>.Success(newdto, StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDTO<IEnumerable<ProductDTO>>> AddRangeAsync(IEnumerable<ProductCreateDTO> dtos)
        {
            var newEntities = _mapper.Map<IEnumerable<Product>>(dtos);
            await _repository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();
            var newdtos = _mapper.Map<IEnumerable<ProductDTO>>(newEntities);
            return CustomResponseDTO<IEnumerable<ProductDTO>>.Success(newdtos,StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDTO<ProductWithProductFeatureDTO>> GetWithProductFeature(int id)
        {
            var entity = await _productRepository.GetWithProductFeature(id);
            var mappedEntity = _mapper.Map<ProductWithProductFeatureDTO>(entity);
            return CustomResponseDTO<ProductWithProductFeatureDTO>.Success(mappedEntity,StatusCodes.Status200OK);
        }
    }
}
