using E_Commerece.Core.Models.Order_Aggreation;

namespace E_Commerce.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }


        public int deliveryMethodId { get; set; }

        public Address ShippingAddress { get; set; }
    }
}
