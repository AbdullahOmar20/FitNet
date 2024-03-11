
namespace Core.Entities.OrderAggregate
{
    //this class is different from the Address calss in Identity as its the address the order will own
    // not the main address the user owns
    public class Address
    {
        public Address(string firstname, string lastname, string street, string city, string state, string zipcode)
        {
            FirstName = firstname;
            LastName = lastname;
            Street = street;
            City = city;
            State = state;
            ZipCode = zipcode;
        }
        //this parameterless constructor for Entity FrameWork to work as intended 
        public Address()
        {
            
        }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}