using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.DTOS
{
    public class ProductDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public int ProductFeatureId { get; set; }
    }
}
