using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.DTOS
{
    public class ProductFeatureDTO : BaseEntityDTO
    {
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Weight { get; set; }
    }
}
