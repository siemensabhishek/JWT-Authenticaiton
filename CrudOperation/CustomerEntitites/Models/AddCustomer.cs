namespace CustomerEntities.Models
{
    public class AddCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressId { get; set; }
    }
}
