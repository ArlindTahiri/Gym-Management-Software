using loremipsum.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loremipsum
{
    public class Checkout
    {

        public Checkout(Member Member, Contracts Contract)
        {

            this.Member = Member;
            this.ActiveContract = Contract;
            this.Sum = BuildSum();
            this.CheckoutID = ++CheckoutID;

        }



        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int CheckoutID { get; }
        [Required] public decimal Sum { get; set; }

        [Required] public Contracts ActiveContract { get; set; }

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
