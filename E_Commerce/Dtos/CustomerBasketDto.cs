using E_Commerece.Core.Models;

namespace E_Commerce.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClinetSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingCost { get; set; }
        public List<BasketItem> Items { get; set; }
        public CustomerBasketDto(string Id)
        {

            this.Id = Id;
        }
    }
}
