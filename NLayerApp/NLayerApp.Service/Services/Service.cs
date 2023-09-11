using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

namespace NLayerApp.Service.Services
{
    public class Service<Entity, Dto> : IService<Entity, Dto> where Entity : BaseEntity where Dto : class
    {
        protected readonly IMapper _mapper;
        protected readonly IGenericRepository<Entity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        public Service(IMapper mapper, IGenericRepository<Entity> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDTO<Dto>> AddAsync(Dto dto)
        {
            Entity newEntity = _mapper.Map<Entity>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newdto = _mapper.Map<Dto>(newEntity);
            return CustomResponseDTO<Dto>.Success(newdto,StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDTO<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos)
        {
            var newEntities = _mapper.Map<IEnumerable<Entity>>(dtos);
            await _repository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();
            var newDtos = _mapper.Map<IEnumerable<Dto>>(newEntities);
            return CustomResponseDTO<IEnumerable<Dto>>.Success(newDtos,StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDTO<bool>> AnyAsync(Expression<Func<Entity, bool>> expression)
        {
            var anyEntity = await _repository.AnyAsync(expression);
            return CustomResponseDTO<bool>.Success(anyEntity,StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> DeleteRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _repository.Where(x=> ids.Contains(x.Id)).ToListAsync();
            _repository.DeleteRange(entities);
            await _unitOfWork.CommitAsync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<IEnumerable<Dto>>> GetAllAsync()
        {
            var entities = await _repository.GetAll().ToListAsync();
            var dtos = _mapper.Map<IEnumerable<Dto>>(entities);
            return CustomResponseDTO<IEnumerable<Dto>>.Success(dtos, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<Dto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var dto = _mapper.Map<Dto>(entity);
            return CustomResponseDTO<Dto>.Success(dto,StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(Dto dto)
        {
            var entity = _mapper.Map<Entity>(dto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression)
        {
            var entities = await _repository.Where(expression).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<Dto>>(entities);
            return CustomResponseDTO<IEnumerable<Dto>>.Success(dtos,StatusCodes.Status200OK);
        }
    }
}
