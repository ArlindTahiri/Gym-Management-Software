using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class Order : IComparable<Order>
    {
        public Order(int memberID, int articleID, int amount)
        {
            ArticleID = articleID;
            Amount = amount;
            MemberID= memberID;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [Required] public int Amount { get; set; }

        //many-to-one Relation
        public int MemberID { get; set; }
        public Member Member { get; set; }

        //many-to-one Relation
        public int ArticleID { get; set; }
        public Article Article { get; set; }

        public int CompareTo(Order other)
        {
            if(OrderID.CompareTo(other.OrderID) == 0) { return 0; }
            else
            {
                if(MemberID==other.MemberID && ArticleID==other.ArticleID && Amount==other.Amount) { return 0; }
                else { return -1; }
            }
            
        }
    }
}
