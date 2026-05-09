using E_Commerece.Core.Models;
using E_Commerece.Core.Models.Identity;

namespace E_Commerce.Dtos
{
    public class AddressDto :EntityBase
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AppUserId { get; set; }

    }
}