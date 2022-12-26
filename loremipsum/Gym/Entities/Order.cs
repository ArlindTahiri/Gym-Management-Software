using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class Order : IComparable<Order>
    {
        public Order(int memberID, int articleID, int amount)
        {
            OrderID = ++OrderID;
            ArticleID = articleID;
            Amount = amount;
            MemberID= memberID;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; }

        [Required] public int Price { get; set; }

        [Required] public int Amount { get; set; }

        //many-to-one Relation
        public int MemberID { get; set; }
        public Member Member { get; set; }

        //many-to-one Relation
        public int ArticleID { get; set; }
        public Article Article { get; set; }

        public int CompareTo(Order other)
        {
            return OrderID.CompareTo(other.OrderID);
        }
    }
}
