using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace loremipsum.Gym.Entities
{
    [Serializable]
    public class Article : IComparable<Article>
    {

        public Article(string articleName, int price, int targetStock, int actualStock)
        {
            ArticleID = ++ArticleID;
            ArticleName = articleName;
            Price = price;
            TargetStock = targetStock;
            ActualStock = actualStock;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ArticleID { get; }

        [Required] public string ArticleName { get; set; }

        [Required] public int Price { get; set; }

        [Required] public int TargetStock { get; set; }

        [Required] public int ActualStock { get; set; }

        //one-to-many Relation
        public ICollection<Order> Orders { get; set; }

        public int CompareTo(Article other)
        {
            return ArticleID.CompareTo(other.ArticleID);
        }

        public override string ToString()
        {
            return ArticleID+" "+ArticleName+" "+Price+" "+TargetStock+" "+ActualStock;
        }

    }
}
