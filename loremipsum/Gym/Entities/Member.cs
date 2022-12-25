using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class Member: IComparable<Member>
    {
        public Member(string forename,string surname,Address address,string eMail, int iban, DateTime birthday)
        {
            MemberID = ++MemberID;
            Forename = forename;
            Surname = surname;
            Address = address;
            EMail = eMail;
            Iban = iban;
            Birthday = birthday;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MemberID { get; }

        [Required] public string Forename { get; set; }

        [Required] public string Surname { get; set;}

        [Required] public Address Address { get; set; }

        [Required] [EmailAddress] public string EMail { get; set; }

        [Required] public int Iban { get; set; }

        [Required] public DateTime Birthday { get; set;}

        //many-to one Relation
        public int ContractID { get; set; }
        public Contract Contract { get; set;}

        //one-to-many Relation
        public ICollection<Order> Orders { get; set;}

        public int CompareTo(Member other)
        {
            return MemberID.CompareTo(other.MemberID);
        }

        public override string ToString() 
        {
            return MemberID+" "+Forename+" "+Surname+" "+Address+" "+EMail+" "+Iban+" "+ Birthday;
        }
    }
}
