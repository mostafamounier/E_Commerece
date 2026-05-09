using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Models.Order_Aggreation
{
    public class Order :EntityBase
    {
        public string BuryerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public DeliveryMethod deliveryMethod { get; set; }
        public OrderStatus Status { get; set; } =OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> Items { get; set; }=new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + deliveryMethod.Cost;
        public string PaymentIntentId { get; set; } = string.Empty;
        public Order()
        {
            
        }

        public Order(string buryerEmail, DeliveryMethod deliveryMethod, Address shippingAddress, ICollection<OrderItem> items, decimal subTotal)
        {
            BuryerEmail = buryerEmail;
            this.deliveryMethod = deliveryMethod;
            ShippingAddress = shippingAddress;
            Items = items;
            SubTotal = subTotal;
        }
    }
}
