namespace CrudModels
{
    public class CustomerEditDetails
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public AddressDetails Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }



    public class AddressDetails
    {
        public int AddressId { get; set; }
        public string Address { get; set; }
    }
}
