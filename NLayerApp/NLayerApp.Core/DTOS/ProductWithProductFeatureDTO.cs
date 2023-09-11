using NLayerApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.DTOS
{
    public class ProductWithProductFeatureDTO : ProductDTO
    {
        public ProductFeatureDTO ProductFeature { get; set; }
    }
}
