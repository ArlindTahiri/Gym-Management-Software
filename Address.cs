using System;

public class Address
{
    public string street { get; private set; }
    public int postalCode { get; private set; }
    public string city { get; private set; }
    public string country { get; private set; }

    public Addresse(string street, int postalCode, string city, string country)
	{
        street = street;
        postalCode = postalCode;
        city = city;
        country = country;
    }

    public override bool Equals(object obj)
    {
        return obj is Address address &&
               street == address.street &&
               postalCode == address.postalCode &&
               city == address.city &&
               country == address.country;
    }
}
