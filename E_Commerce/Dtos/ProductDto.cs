using E_Commerece.Core.Models;

namespace E_Commerce.Dtos
{
    public class ProductDto :EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
    }
}
