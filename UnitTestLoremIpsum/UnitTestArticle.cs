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
            a1 = new Article("Erdbeer-Proteinshake", 399, 15, 15);
            a2 = new Article("Erdbeer-Proteinshake", 399, 15, 15);
            a3 = new Article("Vanille Proteinriegel", 199, 10, 10);
            a4 = new Article("Schoko Proteinriegel", 199, 10, 10);

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            var fileInfo = new FileInfo(@"log4net.config");
            XmlConfigurator.Configure(repository, fileInfo);

            log.Info("Started article test");
        }

            [TestMethod]
        public void CreateArticle()
        {
            //Add the article a1
            Admin.AddArticle(a1);


            //Test if the Article is in the database
            Assert.IsTrue(Query.GetArticleDetails(a1.ArticleID).CompareTo(a1) == 0);


            //Test if you can upload the same article multiple times:
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() =>Admin.AddArticle(a1));


            //Test if you can upload different article object but same properties.
            Assert.ThrowsException<Microsoft.EntityFrameworkCore.DbUpdateException>(() => Admin.AddArticle(a2));
            

            //Add also the other articles
            Admin.AddArticle(a3);
            Admin.AddArticle(a4);

            log.Info("Created articles"); 
        }

        [TestMethod]
        public void UpdateArticle()
        {
            //Test if you can update Article Properties
            Admin.UpdateArticle(a3.ArticleID,"Vanille-Schoko Proteinriegel", 219, 11, 11);
            Article newArticle = Query.GetArticleDetails(a3.ArticleID);
            Assert.IsTrue(newArticle.Price == 219 && newArticle.ArticleName.Equals("Vanille-Schoko Proteinriegel") && newArticle.ActualStock == 11 && newArticle.TargetStock == 11);
            log.Info("Updated article");
        }

        [TestMethod]
        public void DeleteArticle()
        {
            //Test if you can delete one of the articles
            Admin.DeleteArticle(a1.ArticleID);
            Assert.IsNull(Query.GetArticleDetails(a1.ArticleID));

            //Test if you can delete all of the rest articles
            Admin.DeleteArticles();
            Assert.IsNull(Query.GetArticleDetails(a3.ArticleID));
            Assert.IsNull(Query.GetArticleDetails(a4.ArticleID));

            log.Info("Deleted article");
            log.Info("Article test end");
        }

    }
}
