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
        public ProductSpecification(ProductSpecParams productSpecParams ) : base(p =>
        (string.IsNullOrEmpty(productSpecParams.SearchByName)) ||(p.Name.Contains(productSpecParams.SearchByName))&&

        (!productSpecParams.BrandId.HasValue||p.ProductBrandId== productSpecParams.BrandId) &&(!productSpecParams.TypeId.HasValue||p.ProductTypeId== productSpecParams.TypeId))
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
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
            ApplyPagniation(productSpecParams.PageSize*(productSpecParams.PageIndex-1),productSpecParams.PageSize);
            }

        public ProductSpecification(int id) : base(p => p.Id == id)
        { 
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }

        
    }
}
