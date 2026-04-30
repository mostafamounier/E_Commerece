using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public String ProductUrl { get; set; }
        public decimal Price { get; set; }
        public int qunatity { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
