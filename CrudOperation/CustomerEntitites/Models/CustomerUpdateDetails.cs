namespace CustomerEntities.Models
{
    public class CustomerUpdateDetails
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
