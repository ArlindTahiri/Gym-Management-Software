using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loremipsum.Entities
{
    [Serializable]
    public class Contracts : IComparable<Contracts>
    {
        public Contracts(string contractType, TimeSpan duration, int price)
        {
            ContractID = ++ContractID;
            ContractType = contractType;
            Duration= duration;
            Price = price;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContractID { get; }

        [Required] public string ContractType { get; set; }

        [Required] public TimeSpan Duration { get; set; }

        [Required] public int Price { get; set; }

        public int CompareTo(Contracts other)
        {
            return ContractID.CompareTo(other.ContractID);
        }

        public override string ToString()
        {
            return ContractID+" "+ContractType+" "+Duration+" "+Price;
        }
    }
}
