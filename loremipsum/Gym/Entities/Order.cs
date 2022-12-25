using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class Order : IComparable<Order>
    {
        public Order(int articleID, int amount)
        {
            
            OrderID = ++OrderID;
            //acutalstock query and deduction should be implemented in dispencer

            /* 
            Price = article.Price;
            if (amount <= article.ActualStock)
            {
                article.ActualStock = article.ActualStock - amount;
                
            }
            */

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; }

        [Required] public int Price { get; set; }

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
