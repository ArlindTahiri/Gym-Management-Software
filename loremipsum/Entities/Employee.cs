using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loremipsum.Entities
{
    [Serializable]
    public class Employee : IComparable<Employee>
    {

        public Employee(string forename, string surname, Address address, string eMail, int iban, DateTime birthday, string status)
        {
            EmployeeID = ++EmployeeID;
            Forename = forename;
            Surname = surname;
            Address = address;
            EMail = eMail;
            Iban = iban;
            Birthday = birthday;
            Status = status;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; }

        [Required] public string Forename { get; set; }

        [Required] public string Surname { get; set; }

        [Required] public Address Address { get; set; }

        [Required] [EmailAddress] public string EMail { get; set; }

        [Required] public int Iban { get; set; }

        [Required] public DateTime Birthday { get; set; }

        public string Status { get; set; }

        public int CompareTo(Employee other)
        {
            return EmployeeID.CompareTo(other.EmployeeID);
        }

        public override string ToString()
        {
            return EmployeeID+" "+Forename + " " + Surname + " " + Address + " " + EMail + " " + Iban + " " + Birthday+" "+Status;
        }






    }
}
