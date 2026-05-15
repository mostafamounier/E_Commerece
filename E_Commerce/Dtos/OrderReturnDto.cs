using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Models.Order_Aggreation
{
    public class OrderReturnDto :EntityBase
    {
        public string BuyerEmail { get; set; }
        public string ShortName { get; set; }       
        public decimal Cost { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public Address shippToAddress { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + Cost;
        public string paymentIntentId { get; set; }




    }
}
