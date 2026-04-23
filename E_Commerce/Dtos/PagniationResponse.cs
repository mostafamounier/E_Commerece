namespace E_Commerce.Dtos
{
    public class PagniationResponse<T>
    {
        public int Index { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<T> Data { get; set; }= new List<T>();

        public PagniationResponse(int index,int pagesize,int count ,List<T> data)
        {
            
            Index = index;
            PageSize = pagesize;
            Count = count;
            Data = data;
            
        }
    }
}
