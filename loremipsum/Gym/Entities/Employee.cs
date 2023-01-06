using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class Employee : IComparable<Employee>
    {

        public Employee(string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday)
        {
            Forename = forename;
            Surname = surname;
            Street = street;
            PostcalCode = postcalCode;
            City = city;
            Country = country;
            EMail = eMail;
            Iban = iban;
            Birthday = birthday;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }

        [Required] public string Forename { get; set; }

        [Required] public string Surname { get; set; }

        [Required] public string Street { get; set; }

        [Required] public int PostcalCode { get; set; }

        [Required] public string City { get; set; }

        [Required] public string Country { get; set; }

        [Required] [EmailAddress] public string EMail { get; set; }

        [Required] public string Iban { get; set; }

        [Required] public DateTime Birthday { get; set; }

        public int CompareTo(Employee other)
        {
            if (EmployeeID.CompareTo(other.EmployeeID) == 0) { return 0; }
            else
            {
                if (Forename.Equals(other.Forename) && Surname.Equals(other.Surname) && Street.Equals(other.Street) && PostcalCode == other.PostcalCode && City.Equals(other.City) && Country.Equals(other.Country) && EMail.Equals(other.EMail) && Iban.Equals(other.Iban) && Birthday.Equals(other.Birthday)) { return 0; }
                else { return -1; }
            }
        }

        public override string ToString()
        {
            return EmployeeID + " " + Forename + " " + Surname + " " + Street + " " + PostcalCode + " " + City + " " + Country + " " + EMail + " " + Iban + " " + Birthday;
        }






    }
}
