using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NLayerApp.Core.Models;
using NLayerApp.Core.Repositories;
using NLayerApp.Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Repositories
{

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDBContext context) : base(context)
        {
        }

        public IQueryable<Product> GetAllWithProductFeature()
        {
            return _context.Products.Include(x=> x.ProductFeature).AsQueryable();
        }

        public async Task<Product> GetWithProductFeature(int id)
        {
            return await _context.Products.Include(x=> x.ProductFeature).SingleAsync(x=> x.Id == id);
        }
    }
}
