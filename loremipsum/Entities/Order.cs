
namespace loremipsum.Entities
{
    public class Order
    {
        public int Price { get; set; }

        public int OrderID { get; }

        public Order(Article article, int amount)
        {
            OrderID = ++OrderID;
            Price = article.Price;
            if (amount <= article.ActualStock)
            {
                article.ActualStock = article.ActualStock - amount;
                //SaveChanges()
            }

        }
    }
}
