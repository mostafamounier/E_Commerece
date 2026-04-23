using E_Commerece.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Specifications
{
    public class ProductCountSpec :BaseSpecification<Product>
    {
        public ProductCountSpec(ProductSpecParams productSpecParams) : base(p =>

         (string.IsNullOrEmpty(productSpecParams.SearchByName)) || (p.Name.Contains(productSpecParams.SearchByName)) &&
        (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId) && (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId))
        {


            
        }
    }
}
