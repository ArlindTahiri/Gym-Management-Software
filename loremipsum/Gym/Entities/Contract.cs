using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class Contract : IComparable<Contract>
    {
        public Contract(string contractType, int price)
        {
            ContractType = contractType;
            Price = price;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractID { get; set; }

        [Required] public string ContractType { get; set; }

        [Required] public int Price { get; set; }

        //one-to-many Relation
        public ICollection<Member> Members { get; set; }

        public int CompareTo(Contract other)
        {
            return ContractID.CompareTo(other.ContractID);
        }

        public override string ToString()
        {
            return ContractID+" "+ContractType+" "+Price;
        }
    }
}
