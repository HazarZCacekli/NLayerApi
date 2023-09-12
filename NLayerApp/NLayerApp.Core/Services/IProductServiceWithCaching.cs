using NLayerApp.Core.DTOS;
using NLayerApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface IProductServiceWithCaching : IProductService , IService<Product,ProductDTO>
    {
    }
}
