using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class Article : IComparable<Article>
    {

        public Article(string articleName, int price, int targetStock, int actualStock)
        {
            ArticleName = articleName;
            Price = price;
            TargetStock = targetStock;
            ActualStock = actualStock;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleID { get; set; }

        [Required] public string ArticleName { get; set; }

        [Required] public int Price { get; set; }

        [Required] public int TargetStock { get; set; }

        [Required] public int ActualStock { get; set; }

        //one-to-many Relation
        public ICollection<Order> Orders { get; set; }

        public int CompareTo(Article other)
        {
            if (ArticleID.CompareTo(other.ArticleID) == 0) { return 0; }
            else
            {
                if (ArticleName.Equals(other.ArticleName) && Price == other.Price && TargetStock == other.TargetStock && ActualStock == other.ActualStock) { return 0; }
                else { return -1; }
            }
        }

        public override string ToString()
        {
            return ArticleName+" | Preis: "+(double)Price/100+"€ | Derzeitiger Bestand: "+ActualStock;
        }

    }
}
