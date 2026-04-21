using E_Commerece.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(string? Sort ,int? brandid,int? typeid ) : base(p =>

        (!brandid.HasValue||p.ProductBrandId==brandid)&&(!typeid.HasValue||p.ProductTypeId==typeid)        )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if (!string.IsNullOrEmpty(Sort))
            {
                switch (Sort)
                {
                    case "PriceAsnc":
                        this.OrderBy = p => p.Price;
                        break;

                    case "PriceDesnc":
                        this.OrderByDesnc = p => p.Price;
                        break;


                    default:
                        this.OrderBy = p => p.Name;
                        break;



                }

            }
            }
        public ProductSpecification(int id) : base(p => p.Id == id)
        { 
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }

    }
}
