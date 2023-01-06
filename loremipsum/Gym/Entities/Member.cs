using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class Member: IComparable<Member>
    {
        public Member(string forename, string surname, string street,int postcalCode, string city, string country, string eMail, string iban, DateTime birthday, int contractID)
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
            ContractID = contractID;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberID { get; set; }

        [Required] public string Forename { get; set; }

        [Required] public string Surname { get; set;}

        [Required] public string Street { get; set;}

        [Required] public int PostcalCode { get; set;}

        [Required] public string City { get; set; }

        [Required] public string Country { get; set; }

        [Required] [EmailAddress] public string EMail { get; set; }

        [Required] public string Iban { get; set; }

        [Required] public DateTime Birthday { get; set;}

        [Required] public int CurrentBill { get; set;}

        //many-to one Relation
        public int ContractID { get; set; }
        public Contract Contract { get; set;}

        //one-to-many Relation
        public ICollection<Order> Orders { get; set;}

        public int CompareTo(Member other)
        {
            if (MemberID.CompareTo(other.MemberID) == 0) { return 0; }
            else
            {
                if (Forename.Equals(other.Forename) && Surname.Equals(other.Surname) && Street.Equals(other.Street) && PostcalCode == other.PostcalCode && City.Equals(other.City) && Country.Equals(other.Country) && EMail.Equals(other.EMail) && Iban.Equals(other.Iban) && Birthday.Equals(other.Birthday)) { return 0; }
                else { return -1; }
            }
        }

        public override string ToString() 
        {
            return MemberID+" "+Forename+" "+Surname+" "+Street+" "+PostcalCode+" "+City+" "+Country+" "+EMail+" "+Iban+" "+ Birthday;
        }
    }
}
