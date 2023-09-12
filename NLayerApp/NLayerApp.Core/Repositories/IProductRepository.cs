using Microsoft.EntityFrameworkCore.Query;
using NLayerApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetWithProductFeature(int id);
        IQueryable<Product> GetAllWithProductFeature();

    }
}
