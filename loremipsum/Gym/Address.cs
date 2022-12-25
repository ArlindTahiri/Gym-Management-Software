using System;

namespace loremipsum.Gym
{
    public class Address
    {
        public string Street { get; private set; }
        public int PostalCode { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        public Address(string street, int postalCode, string city, string country)
	    {
            Street = street;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public override bool Equals(object obj)
        {
            return obj is Address address &&
                   Street == address.Street &&
                   PostalCode == address.PostalCode &&
                   City == address.City &&
                   Country == address.Country;
        }
    }

}
