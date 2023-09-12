using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Models
{
    public class ProductFeature : BaseEntity
    {
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Weight { get; set; }
        public int ProductId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
