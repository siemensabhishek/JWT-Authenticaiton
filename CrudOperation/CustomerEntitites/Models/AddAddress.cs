using Microsoft.EntityFrameworkCore;

namespace CustomerEntities.Models
{
    [PrimaryKey(nameof(AddressId))]
    public class AddAddress
    {
        public int AddressId { get; set; }
        public string AddressText { get; set; }
    }
}
