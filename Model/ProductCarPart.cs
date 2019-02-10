using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeus;

namespace Dohko
{
    public class ProductCarPart : ProductBase
    {
        public override IProduct CreateNewItem(string description, string category, decimal soldPrice, int lastQuantitySold)
        {
            return new ProductCarPart()
            {
                Description = description,
                Category = category,
                Price = soldPrice,
                LastQuantitySold = lastQuantitySold
            };
        }
    }
}
