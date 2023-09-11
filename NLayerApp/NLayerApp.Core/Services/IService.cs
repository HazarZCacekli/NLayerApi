using NLayerApp.Core.DTOS;
using NLayerApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface IService<Entity,Dto> where Entity : BaseEntity where Dto : class
    {
        Task<CustomResponseDTO<Dto>> GetByIdAsync(int id);
        Task<CustomResponseDTO<IEnumerable<Dto>>> GetAllAsync();
        Task<CustomResponseDTO<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDTO<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDTO<Dto>> AddAsync(Dto dto);
        Task<CustomResponseDTO<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos);
        Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(Dto dto);
        Task<CustomResponseDTO<NoContentDTO>> DeleteAsync(int id);
        Task<CustomResponseDTO<NoContentDTO>> DeleteRangeAsync(IEnumerable<int> ids);

    }
}
