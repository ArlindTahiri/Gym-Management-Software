using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace loremipsum.Gym.Entities
{
    public class Checkout
    {

        public Checkout(Member member, Contract contract)
        {
            this.CheckoutID = ++CheckoutID;
            this.Member = member;
            this.ActiveContract = contract;
            this.Sum = BuildSum();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CheckoutID { get; }

        [Required] public decimal Sum { get; set; }

        [Required] public Contract ActiveContract { get; set; }

        [Required] public Member Member { get; set; }

        [Required] private LinkedList<Order> Orders = new LinkedList<Order>();

        public void AddOrder(Order o)
        {
            Orders.AddLast(o);
        }

        public void RemoveOrder(Order o)
        {
            IEnumerator<Order> enumerator = Orders.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Equals(o))
                {
                    Orders.Remove(o);
                    break;
                }
            }
        }

        public decimal BuildSum()
        {
            decimal CurrentSum = 0;
            IEnumerator<Order> enumerator = Orders.GetEnumerator();
            while (enumerator.MoveNext())
            {
                CurrentSum += enumerator.Current.Price;
            }

            return Sum = CurrentSum + ActiveContract.Price;
        }
    }
}
