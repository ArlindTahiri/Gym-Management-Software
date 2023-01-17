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
        private Article a1, a2, a3, a4;
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
            a1 = new Article("Erdbeer-Proteinshake", 4, 15, 15);
            a2 = new Article("Milkshake", 3, -10, 25);
            a3 = new Article("Vanille Proteinriegel", 2, 10, -10);
            a4 = new Article("Schoko Proteinriegel", 2, 10, 10);
            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Started article test");

            //delete all entities in the begining
            Admin.DeleteOrders();
            Admin.DeleteMembers();
            Admin.DeleteArticles();



            //add contract and member to add order
            c1 = new Contract("Premium Plan", 20);
            Admin.AddContract(c1);
            m1 = Admin.AddMember(c1.ContractID, "Martin", "Meyer", "Mohrenstrasse 54", 04161, "Leipzig", "Deutschland",
                    "martinmeyer@gmail.com", "DE94500105172327561324", new DateTime(1990, 11, 24));
        }

        [TestMethod]
        public void AddArticle()
        {
            //Add article a1
            Admin.AddArticle(a1);
            Assert.IsTrue(Query.GetArticleDetails(a1.ArticleID).CompareTo(a1) == 0);

            //check if you can add articles with an negative ActualStock or negative TargetStock
            Admin.AddArticle(a2);
            Assert.IsNull(Query.GetArticleDetails(a2.ArticleID));

            Admin.AddArticle(a3);
            Assert.IsNull(Query.GetArticleDetails(a3.ArticleID));
        }

        [TestMethod]
        public void UpdateArticle()
        {
            Admin.AddArticle(a4);
            //Test if you can update Article Properties
            Admin.UpdateArticle(a4.ArticleID,"Vanille-Schoko Proteinriegel", 3, 20, 20);
            Article newArticle = Query.GetArticleDetails(a4.ArticleID);
            Assert.IsTrue(newArticle.Price == 3 && newArticle.ArticleName.Equals("Vanille-Schoko Proteinriegel") && newArticle.ActualStock == 20 && newArticle.TargetStock == 20);
            log.Info("Updated article");
        }

        [TestMethod]
        public void ListArticles()
        {
            //add an article and than check if there are articles
            Admin.AddArticle(a1);
            IList<Article> articles = Admin.ListArticles();
            Assert.IsTrue(articles.Count > 0);

            //now delete them and check again
            Admin.DeleteArticles();
            articles = Admin.ListArticles();
            Assert.IsTrue(articles.Count == 0);
        }

        [TestMethod]
        public void DeleteArticle()
        {
            //add an article and than check if you can delete it
            Admin.AddArticle(a1);
            Assert.IsNotNull(Query.GetArticleDetails(a1.ArticleID));
            Admin.DeleteArticle(a1.ArticleID);
            Assert.IsNull(Query.GetArticleDetails(a1.ArticleID));
        }

        [TestMethod]
        public void DeleteArticles()
        {
            //add articles and orders
            Admin.AddArticle(a1);
            Admin.AddArticle(a4);
            o1 = Admin.AddOrder(m1.MemberID, a4.ArticleID, 1);

            //try to delete all articles even if there still exists orders
            Admin.DeleteArticles();
            IList<Article> articles = Admin.ListArticles();
            Assert.IsTrue(articles.Count > 0);

            //delete the order and check again
            Admin.DeleteOrders();
            Admin.DeleteArticles();
            articles = Admin.ListArticles();
            Assert.IsTrue(articles.Count == 0);
        }

    }
}
