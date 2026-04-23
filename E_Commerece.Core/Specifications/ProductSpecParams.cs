namespace E_Commerece.Core.Specifications
{
    public class ProductSpecParams
    {
        private int pageSize = 10;

        public int PageSize
        {
            get => pageSize;
            set
            {
                if (value <= 0)
                    pageSize = 10;
                else if (value > 50)
                    pageSize = 50;
                else
                    pageSize = value;
            }
        }
        public int PageIndex { get; set; } = 1;
        public string ? Sort {  get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? SearchByName { get; set; }
    }
}
