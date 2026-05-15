namespace E_Commerece.Core.Models.Order_Aggreation
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public int Qunatity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
    }
}