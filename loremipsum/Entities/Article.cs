using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loremipsum.Entities
{
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

        public int ArticleID { get; }

        public string ArticleName { get; set; }

        public int Price { get; set; }

        public int TargetStock { get; set; }

        public int ActualStock { get; set; }

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
