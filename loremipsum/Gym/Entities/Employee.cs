using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class Employee : IComparable<Employee>
    {

        public Employee(string forename, string surname, string street, int postcalCode, string city, string country, string eMail, string iban, DateTime birthday, string status)
        {
            EmployeeID = ++EmployeeID;
            Forename = forename;
            Surname = surname;
            Street = street;
            PostcalCode = postcalCode;
            City = city;
            Country = country;
            EMail = eMail;
            Iban = iban;
            Birthday = birthday;
            Status = status;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        public string Status { get; set; }

        public int CompareTo(Employee other)
        {
            return EmployeeID.CompareTo(other.EmployeeID);
        }

        public override string ToString()
        {
            return EmployeeID + " " + Forename + " " + Surname + " " + Street + " " + PostcalCode + " " + City + " " + Country + " " + EMail + " " + Iban + " " + Birthday + " " + Status;
        }






    }
}
