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
        public string BuyerEmail { get; set; }
        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.Now;
        public DeliveryMethod deliveryMethod { get; set; }
        public OrderStatus status { get; set; } =OrderStatus.Pending;
        public Address shippToAddress { get; set; }
        public ICollection<OrderItem> Items { get; set; }=new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + deliveryMethod.Cost;
        public string paymentIntentId { get; set; } 
        public Order()
        {
            
        }

        public Order(string buryerEmail, DeliveryMethod deliveryMethod, Address shippingAddress, ICollection<OrderItem> items, decimal subTotal,string paymentintentid)
        {
            paymentintentid= paymentintentid ?? string.Empty;
            BuyerEmail = buryerEmail;
            this.deliveryMethod = deliveryMethod;
            shippToAddress = shippingAddress;
            Items = items;
            SubTotal = subTotal;
        }
    }
}
