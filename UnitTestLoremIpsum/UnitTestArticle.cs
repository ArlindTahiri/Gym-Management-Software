using log4net;
using log4net.Config;
using log4net.Repository;
using loremipsum.Gym;
using loremipsum.Gym.Entities;
using loremipsum.Gym.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestLoremIpsum
{
    [TestClass]
    public class UnitTestArticle
    {
        private IProductAdmin Admin;
        private IProductModule Query;
        private Article a1, a2, a3;
        private Member m1;
        private Order o1;
        private Contract c1;
        private static readonly ILog log = LogManager.GetLogger(typeof(UnitTestArticle));


        [TestInitialize()]
        public void SetUp()
        {
            IGymPersistence persistence = new GymPersistenceEF();
            GymFactory factory = new GymFactory(persistence);
            Admin = factory.GetProductAdmin();
            Query = factory.GetProductModule();
            GenerateTestData();
        }

        public void GenerateTestData()
        {
            a1 = new Article("Erdbeer-Proteinshake_test", 4, 15, 15);
            a2 = new Article("Milkshake_test", 3, -10, 25);
            a3 = new Article("Vanille Proteinriegel_test", 2, 10, -10);

            c1 = new Contract("Normal Plan_test", 1999);

        }

        [TestMethod]
        public void TestArticle()
        {
            //Add article a1
            Admin.AddArticle(a1);
            Assert.IsTrue(Query.GetArticleDetails(a1.ArticleID).CompareTo(a1) == 0);

            //check if you can add articles with an negative ActualStock or negative TargetStock
            Admin.AddArticle(a2);
            Assert.IsNull(Query.GetArticleDetails(a2.ArticleID));
            Admin.AddArticle(a3);
            Assert.IsNull(Query.GetArticleDetails(a3.ArticleID));


            //Test if you can update article properties
            Admin.UpdateArticle(a1.ArticleID, "Vanille-Schoko Proteinriegel_test", 3, 20, 20);
            Article newArticle = Query.GetArticleDetails(a1.ArticleID);
            Assert.IsTrue(newArticle.Price == 3 && newArticle.ArticleName.Equals("Vanille-Schoko Proteinriegel_test") && newArticle.ActualStock == 20 && newArticle.TargetStock == 20);


            //check if there are articles in IList
            IList<Article> articles = Admin.ListArticles();
            Assert.IsTrue(articles.Count > 0);


            //add contract and member to create an order
            Admin.AddContract(c1);
            Assert.IsTrue(Query.GetContractDetails(c1.ContractID).CompareTo(c1) == 0);
            m1 = Admin.AddMember(c1.ContractID, "Martin_test", "Meyer", "Mohrenstrasse 54", 04161, "Leipzig", "Deutschland",
                    "martinmeyer@gmail.com", "DE94500105172327561324", new DateTime(1990, 11, 24));
            Assert.IsTrue(Query.GetMemberDetails(m1.MemberID).CompareTo(m1) ==0);
            o1 = Admin.AddOrder(m1.MemberID, a1.ArticleID, 2);

            //try to delete article a1 which has an order
            Admin.DeleteArticle(a1.ArticleID);
            Assert.IsNotNull(Query.GetArticleDetails(a1.ArticleID));

            //delete the order, contract and member
            Admin.DeleteOrder(o1.OrderID);
            Admin.DeleteMember(m1.MemberID);
            Admin.DeleteContract(c1.ContractID);

            //delete article a1
            Admin.DeleteArticle(a1.ArticleID);
            Assert.IsNull(Query.GetArticleDetails(a1.ArticleID));
        }
    }
}
