using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayerApp.Core.DTOS;
using NLayerApp.Core.Models;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Caching.Services
{
    public class ProductServiceWithCaching : IProductServiceWithCaching
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private const string CacheProductKey = "productsCache";

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;

            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _repository.GetAllWithProductFeature().ToList());
            }
        }

        public async Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductCreateDTO dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            var returndto = _mapper.Map<ProductDTO>(entity);
            return CustomResponseDTO<ProductDTO>.Success(returndto, StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductDTO dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            var returndto = _mapper.Map<ProductDTO>(entity);
            return CustomResponseDTO<ProductDTO>.Success(returndto, StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDTO<IEnumerable<ProductDTO>>> AddRangeAsync(IEnumerable<ProductCreateDTO> dtos)
        {
            var entities = _mapper.Map<IEnumerable<Product>>(dtos);
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            var returndtos = _mapper.Map<IEnumerable<ProductDTO>>(entities);
            return CustomResponseDTO<IEnumerable<ProductDTO>>.Success(returndtos, StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDTO<IEnumerable<ProductDTO>>> AddRangeAsync(IEnumerable<ProductDTO> dtos)
        {
            var entities = _mapper.Map<IEnumerable<Product>>(dtos);
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            var returndtos = _mapper.Map<IEnumerable<ProductDTO>>(entities);
            return CustomResponseDTO<IEnumerable<ProductDTO>>.Success(returndtos, StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDTO<bool>> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            bool any = await _repository.AnyAsync(expression);
            return CustomResponseDTO<bool>.Success(any, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            _repository.Delete(product);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> DeleteRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _repository.Where(x => ids.Contains(x.Id)).ToListAsync();
            _repository.DeleteRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<IEnumerable<ProductDTO>>> GetAllAsync()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var mappedProducts = _mapper.Map<List<ProductDTO>>(products);
            return CustomResponseDTO<IEnumerable<ProductDTO>>.Success(mappedProducts, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<ProductDTO>> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x=> x.Id == id);
            if (product == null)
            {
                return CustomResponseDTO<ProductDTO>.Fail(StatusCodes.Status404NotFound, $"{typeof(Product).Name}({id}) not found.");
            }
            var dto = _mapper.Map<ProductDTO>(product);
            return CustomResponseDTO<ProductDTO>.Success(dto, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<ProductWithProductFeatureDTO>> GetWithProductFeature(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).SingleOrDefault(x=> x.Id == id);
            if (product == null)
            {
                return CustomResponseDTO<ProductWithProductFeatureDTO>.Fail(StatusCodes.Status404NotFound, $"{typeof(Product).Name}({id}) not found.");
            }
            var dto = _mapper.Map<ProductWithProductFeatureDTO>(product);
            return CustomResponseDTO<ProductWithProductFeatureDTO>.Success(dto, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(ProductDTO dto)
        {
            var product = _mapper.Map<Product>(dto);
            _repository.Update(product);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<IEnumerable<ProductDTO>>> Where(Expression<Func<Product, bool>> expression)
        {
            var products = _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile());
            var mappedProducts = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return CustomResponseDTO<IEnumerable<ProductDTO>>.Success(mappedProducts, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<IEnumerable<ProductWithProductFeatureDTO>>> GetAllWithProductFeature()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var mappedProducts = _mapper.Map<IEnumerable<ProductWithProductFeatureDTO>>(products);
            return CustomResponseDTO<IEnumerable<ProductWithProductFeatureDTO>>.Success(mappedProducts,StatusCodes.Status200OK);
        }

        public async Task CacheAllProductsAsync()
        {
            _memoryCache.Set(CacheProductKey, await _repository.GetAll().ToListAsync());
        }
    }
}
