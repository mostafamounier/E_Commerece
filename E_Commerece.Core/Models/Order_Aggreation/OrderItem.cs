using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Models.Order_Aggreation
{
    public class OrderItem :EntityBase
    {

        public ProductOrderItem Product{ get; set; }
        public int Qunatity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public OrderItem()
        {
            
        }

        public OrderItem(ProductOrderItem product, int qunatity, decimal price)
        {
            Product = product;
            Qunatity = qunatity;
            Price = price;
        }
    }
}
